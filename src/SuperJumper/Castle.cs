using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJumper
{
	public class Castle : GameObject
	{
		public static float CASTLE_WIDTH = 1.7f;
		public static float CASTLE_HEIGHT = 1.7f;

		public Castle(float x, float y)
			: base(x, y, CASTLE_WIDTH, CASTLE_HEIGHT)
		{
		}

	}
}