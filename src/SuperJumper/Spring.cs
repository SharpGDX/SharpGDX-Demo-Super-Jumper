using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJumper
{
	public class Spring : GameObject
	{
		public static float SPRING_WIDTH = 0.3f;
		public static float SPRING_HEIGHT = 0.3f;

		public Spring(float x, float y)
			: base(x, y, SPRING_WIDTH, SPRING_HEIGHT)
		{
		}
	}
}