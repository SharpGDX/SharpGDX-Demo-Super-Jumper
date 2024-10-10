using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJumper
{
	public class Coin : GameObject
	{
	public static readonly float COIN_WIDTH = 0.5f;
	public static readonly float COIN_HEIGHT = 0.8f;
	public static readonly int COIN_SCORE = 10;

	internal float stateTime;

	public Coin(float x, float y)
	:base(x, y, COIN_WIDTH, COIN_HEIGHT)
	{
		
		stateTime = 0;
	}

	public void update(float deltaTime)
	{
		stateTime += deltaTime;
	}
	}
}
