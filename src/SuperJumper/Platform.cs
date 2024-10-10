using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJumper
{
	public class Platform : DynamicGameObject
	{
		public static readonly float PLATFORM_WIDTH = 2;
		public static readonly float PLATFORM_HEIGHT = 0.5f;
		public static readonly int PLATFORM_TYPE_STATIC = 0;
		public static readonly int PLATFORM_TYPE_MOVING = 1;
		public static readonly int PLATFORM_STATE_NORMAL = 0;
		public static readonly int PLATFORM_STATE_PULVERIZING = 1;
		public static readonly float PLATFORM_PULVERIZE_TIME = 0.2f * 4;
		public static readonly float PLATFORM_VELOCITY = 2;

		int type;
		internal int state;
		internal float stateTime;

		public Platform(int type, float x, float y)
			: base(x, y, PLATFORM_WIDTH, PLATFORM_HEIGHT)
		{
			this.type = type;
			this.state = PLATFORM_STATE_NORMAL;
			this.stateTime = 0;
			if (type == PLATFORM_TYPE_MOVING)
			{
				velocity.x = PLATFORM_VELOCITY;
			}
		}

		public void update(float deltaTime)
		{
			if (type == PLATFORM_TYPE_MOVING)
			{
				position.add(velocity.x * deltaTime, 0);
				bounds.x = position.x - PLATFORM_WIDTH / 2;
				bounds.y = position.y - PLATFORM_HEIGHT / 2;

				if (position.x < PLATFORM_WIDTH / 2)
				{
					velocity.x = -velocity.x;
					position.x = PLATFORM_WIDTH / 2;
				}

				if (position.x > World.WORLD_WIDTH - PLATFORM_WIDTH / 2)
				{
					velocity.x = -velocity.x;
					position.x = World.WORLD_WIDTH - PLATFORM_WIDTH / 2;
				}
			}

			stateTime += deltaTime;
		}

		public void pulverize()
		{
			state = PLATFORM_STATE_PULVERIZING;
			stateTime = 0;
			velocity.x = 0;
		}
	}
}