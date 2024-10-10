using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Mathematics;

namespace SuperJumper
{
	public class World {
	public interface WorldListener {
		public void jump ();

		public void highJump ();

		public void hit ();

		public void coin ();
	}

	public static readonly float WORLD_WIDTH = 10;
	public static readonly float WORLD_HEIGHT = 15 * 20;
	public static readonly int WORLD_STATE_RUNNING = 0;
	public static readonly int WORLD_STATE_NEXT_LEVEL = 1;
	public static readonly int WORLD_STATE_GAME_OVER = 2;
	public static readonly Vector2 gravity = new Vector2(0, -12);

	public readonly Bob bob;
	public readonly List<Platform> platforms;
	public readonly List<Spring> springs;
	public readonly List<Squirrel> squirrels;
	public readonly List<Coin> coins;
	public Castle castle;
	public readonly WorldListener listener;
	public readonly SharpGDX.Mathematics.RandomXS128 rand;

	public float heightSoFar;
	public int score;
	public int state;

	public World (WorldListener listener) {
		this.bob = new Bob(5, 1);
		this.platforms = new List<Platform>();
		this.springs = new List<Spring>();
		this.squirrels = new List<Squirrel>();
		this.coins = new List<Coin>();
		this.listener = listener;
		rand = new ();
		generateLevel();

		this.heightSoFar = 0;
		this.score = 0;
		this.state = WORLD_STATE_RUNNING;
	}

	private void generateLevel () {
		float y = Platform.PLATFORM_HEIGHT / 2;
		float maxJumpHeight = Bob.BOB_JUMP_VELOCITY * Bob.BOB_JUMP_VELOCITY / (2 * -gravity.y);
		while (y < WORLD_HEIGHT - WORLD_WIDTH / 2) {
			int type = rand.nextFloat() > 0.8f ? Platform.PLATFORM_TYPE_MOVING : Platform.PLATFORM_TYPE_STATIC;
			float x = rand.nextFloat() * (WORLD_WIDTH - Platform.PLATFORM_WIDTH) + Platform.PLATFORM_WIDTH / 2;

			Platform platform = new Platform(type, x, y);
			platforms.Add(platform);

			if (rand.nextFloat() > 0.9f && type != Platform.PLATFORM_TYPE_MOVING) {
				Spring spring = new Spring(platform.position.x, platform.position.y + Platform.PLATFORM_HEIGHT / 2
					+ Spring.SPRING_HEIGHT / 2);
				springs.Add(spring);
			}

			if (y > WORLD_HEIGHT / 3 && rand.nextFloat() > 0.8f) {
				Squirrel squirrel = new Squirrel(platform.position.x + rand.nextFloat(), platform.position.y
					+ Squirrel.SQUIRREL_HEIGHT + rand.nextFloat() * 2);
				squirrels.Add(squirrel);
			}

			if (rand.nextFloat() > 0.6f) {
				Coin coin = new Coin(platform.position.x + rand.nextFloat(), platform.position.y + Coin.COIN_HEIGHT
					+ rand.nextFloat() * 3);
				coins.Add(coin);
			}

			y += (maxJumpHeight - 0.5f);
			y -= rand.nextFloat() * (maxJumpHeight / 3);
		}

		castle = new Castle(WORLD_WIDTH / 2, y);
	}

	public void update (float deltaTime, float accelX) {
		updateBob(deltaTime, accelX);
		updatePlatforms(deltaTime);
		updateSquirrels(deltaTime);
		updateCoins(deltaTime);
		if (bob.state != Bob.BOB_STATE_HIT) checkCollisions();
		checkGameOver();
	}

	private void updateBob (float deltaTime, float accelX) {
		if (bob.state != Bob.BOB_STATE_HIT && bob.position.y <= 0.5f) bob.hitPlatform();
		if (bob.state != Bob.BOB_STATE_HIT) bob.velocity.x = -accelX / 10 * Bob.BOB_MOVE_VELOCITY;
		bob.update(deltaTime);
		heightSoFar = Math.Max(bob.position.y, heightSoFar);
	}

	private void updatePlatforms (float deltaTime) {
		int len = platforms.Count();
		for (int i = 0; i < len; i++) {
			Platform platform = platforms[i];
			platform.update(deltaTime);
			if (platform.state == Platform.PLATFORM_STATE_PULVERIZING && platform.stateTime > Platform.PLATFORM_PULVERIZE_TIME) {
				platforms.Remove(platform);
				len = platforms.Count();
			}
		}
	}

	private void updateSquirrels (float deltaTime) {
		int len = squirrels.Count();
		for (int i = 0; i < len; i++) {
			Squirrel squirrel = squirrels[i];
			squirrel.update(deltaTime);
		}
	}

	private void updateCoins (float deltaTime) {
		int len = coins.Count();
		for (int i = 0; i < len; i++) {
			Coin coin = coins[i];
			coin.update(deltaTime);
		}
	}

	private void checkCollisions () {
		checkPlatformCollisions();
		checkSquirrelCollisions();
		checkItemCollisions();
		checkCastleCollisions();
	}

	private void checkPlatformCollisions () {
		if (bob.velocity.y > 0) return;

		int len = platforms.Count();
		for (int i = 0; i < len; i++) {
			Platform platform = platforms[i];
			if (bob.position.y > platform.position.y) {
				if (bob.bounds.overlaps(platform.bounds)) {
					bob.hitPlatform();
					listener.jump();
					if (rand.nextFloat() > 0.5f) {
						platform.pulverize();
					}
					break;
				}
			}
		}
	}

	private void checkSquirrelCollisions () {
		int len = squirrels.Count();
		for (int i = 0; i < len; i++) {
			Squirrel squirrel = squirrels[i];
			if (squirrel.bounds.overlaps(bob.bounds)) {
				bob.hitSquirrel();
				listener.hit();
			}
		}
	}

	private void checkItemCollisions () {
		int len = coins.Count();
		for (int i = 0; i < len; i++) {
			Coin coin = coins[i];
			if (bob.bounds.overlaps(coin.bounds)) {
				coins.Remove(coin);
				len = coins.Count();
				listener.coin();
				score += Coin.COIN_SCORE;
			}

		}

		if (bob.velocity.y > 0) return;

		len = springs.Count();
		for (int i = 0; i < len; i++) {
			Spring spring = springs[i];
			if (bob.position.y > spring.position.y) {
				if (bob.bounds.overlaps(spring.bounds)) {
					bob.hitSpring();
					listener.highJump();
				}
			}
		}
	}

	private void checkCastleCollisions () {
		if (castle.bounds.overlaps(bob.bounds)) {
			state = WORLD_STATE_NEXT_LEVEL;
		}
	}

	private void checkGameOver () {
		if (heightSoFar - 7.5f > bob.position.y) {
			state = WORLD_STATE_GAME_OVER;
		}
	}
}
}
