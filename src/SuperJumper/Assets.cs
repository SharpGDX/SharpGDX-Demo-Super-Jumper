using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX;
using SharpGDX.Audio;
using SharpGDX.Graphics;
using SharpGDX.Graphics.G2D;
using SuperJumper;

namespace SuperJumper
{
	public class Assets {
	public static Texture background;
	public static TextureRegion backgroundRegion;

	public static Texture items;
	public static TextureRegion mainMenu;
	public static TextureRegion pauseMenu;
	public static TextureRegion ready;
	public static TextureRegion gameOver;
	public static TextureRegion highScoresRegion;
	public static TextureRegion logo;
	public static TextureRegion soundOn;
	public static TextureRegion soundOff;
	public static TextureRegion arrow;
	public static TextureRegion pause;
	public static TextureRegion spring;
	public static TextureRegion castle;
	public static Animation coinAnim;
	public static Animation bobJump;
	public static Animation bobFall;
	public static TextureRegion bobHit;
	public static Animation squirrelFly;
	public static TextureRegion platform;
	public static Animation brakingPlatform;
	public static BitmapFont font;

	public static IMusic music;
	public static ISound jumpSound;
	public static ISound highJumpSound;
	public static ISound hitSound;
	public static ISound coinSound;
	public static ISound clickSound;

	public static Texture loadTexture (String file) {
		return new Texture(Gdx.files.@internal(file));
	}

	public static void load () {
		background = loadTexture("assets/data/background.png");
		backgroundRegion = new TextureRegion(background, 0, 0, 320, 480);

		items = loadTexture("assets/data/items.png");
		mainMenu = new TextureRegion(items, 0, 224, 300, 110);
		pauseMenu = new TextureRegion(items, 224, 128, 192, 96);
		ready = new TextureRegion(items, 320, 224, 192, 32);
		gameOver = new TextureRegion(items, 352, 256, 160, 96);
		highScoresRegion = new TextureRegion(Assets.items, 0, 257, 300, 110 / 3);
		logo = new TextureRegion(items, 0, 352, 274, 142);
		soundOff = new TextureRegion(items, 0, 0, 64, 64);
		soundOn = new TextureRegion(items, 64, 0, 64, 64);
		arrow = new TextureRegion(items, 0, 64, 64, 64);
		pause = new TextureRegion(items, 64, 64, 64, 64);

		spring = new TextureRegion(items, 128, 0, 32, 32);
		castle = new TextureRegion(items, 128, 64, 64, 64);
		coinAnim = new Animation(0.2f, new TextureRegion(items, 128, 32, 32, 32), new TextureRegion(items, 160, 32, 32, 32),
			new TextureRegion(items, 192, 32, 32, 32), new TextureRegion(items, 160, 32, 32, 32));
		bobJump = new Animation(0.2f, new TextureRegion(items, 0, 128, 32, 32), new TextureRegion(items, 32, 128, 32, 32));
		bobFall = new Animation(0.2f, new TextureRegion(items, 64, 128, 32, 32), new TextureRegion(items, 96, 128, 32, 32));
		bobHit = new TextureRegion(items, 128, 128, 32, 32);
		squirrelFly = new Animation(0.2f, new TextureRegion(items, 0, 160, 32, 32), new TextureRegion(items, 32, 160, 32, 32));
		platform = new TextureRegion(items, 64, 160, 64, 16);
		brakingPlatform = new Animation(0.2f, new TextureRegion(items, 64, 160, 64, 16), new TextureRegion(items, 64, 176, 64, 16),
			new TextureRegion(items, 64, 192, 64, 16), new TextureRegion(items, 64, 208, 64, 16));

		font = new BitmapFont(Gdx.files.@internal("assets/data/font.fnt"), Gdx.files.@internal("assets/data/font.png"), false);

			music = Gdx.audio.NewMusic(Gdx.files.@internal("assets/data/music.wav"));
			music.SetLooping(true);
			music.Volume = 0.5f;
			if (Settings.soundEnabled) music.Play();
			jumpSound = Gdx.audio.NewSound(Gdx.files.@internal("assets/data/jump.wav"));
		highJumpSound = Gdx.audio.NewSound(Gdx.files.@internal("assets/data/highjump.wav"));
		hitSound = Gdx.audio.NewSound(Gdx.files.@internal("assets/data/hit.wav"));
		coinSound = Gdx.audio.NewSound(Gdx.files.@internal("assets/data/coin.wav"));
		clickSound = Gdx.audio.NewSound(Gdx.files.@internal("assets/data/click.wav"));
	}

	public static void playSound (ISound sound) {
		if (Settings.soundEnabled) sound.Play(1);
	}
}
}
