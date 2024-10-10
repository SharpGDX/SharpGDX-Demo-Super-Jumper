using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX;
using SharpGDX.Graphics.G2D;

namespace SuperJumper
{
	public class SuperJumper : Game
	{
	// used by all screens
	public SpriteBatch batcher;

	public override void Create()
	{
		batcher = new SpriteBatch();
		Settings.load();
		Assets.load();
		SetScreen(new MainMenuScreen(this));
	}

	public override void Render()
	{
		base.Render();
	}
	}
}
