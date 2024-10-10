using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJumper
{
	public class Squirrel : DynamicGameObject
	{
	public static readonly float SQUIRREL_WIDTH = 1;
	public static readonly float SQUIRREL_HEIGHT = 0.6f;
	public static readonly float SQUIRREL_VELOCITY = 3f;

	internal float stateTime = 0;

	public Squirrel(float x, float y)
	: base(x, y, SQUIRREL_WIDTH, SQUIRREL_HEIGHT)
		{
		velocity.set(SQUIRREL_VELOCITY, 0);
	}

	public void update(float deltaTime)
	{
		position.add(velocity.x * deltaTime, velocity.y * deltaTime);
		bounds.x = position.x - SQUIRREL_WIDTH / 2;
		bounds.y = position.y - SQUIRREL_HEIGHT / 2;

		if (position.x < SQUIRREL_WIDTH / 2)
		{
			position.x = SQUIRREL_WIDTH / 2;
			velocity.x = SQUIRREL_VELOCITY;
		}
		if (position.x > World.WORLD_WIDTH - SQUIRREL_WIDTH / 2)
		{
			position.x = World.WORLD_WIDTH - SQUIRREL_WIDTH / 2;
			velocity.x = -SQUIRREL_VELOCITY;
		}
		stateTime += deltaTime;
	}
	}
}
