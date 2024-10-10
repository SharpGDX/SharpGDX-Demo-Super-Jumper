using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Mathematics;

namespace SuperJumper
{
	public class DynamicGameObject : GameObject
	{
		public readonly Vector2 velocity;
		public readonly Vector2 accel;

		public DynamicGameObject(float x, float y, float width, float height)
			: base(x, y, width, height)
		{
			velocity = new Vector2();
			accel = new Vector2();
		}
	}
}
