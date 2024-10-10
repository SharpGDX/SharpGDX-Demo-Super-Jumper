using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Mathematics;

namespace SuperJumper
{
	public class GameObject
	{
		public readonly Vector2 position;
		public readonly Rectangle bounds;

		public GameObject(float x, float y, float width, float height)
		{
			this.position = new Vector2(x, y);
			this.bounds = new Rectangle(x - width / 2, y - height / 2, width, height);
		}
	}
}
