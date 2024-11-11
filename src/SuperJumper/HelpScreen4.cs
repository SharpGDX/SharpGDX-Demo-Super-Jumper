using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX;
using SharpGDX.Graphics;
using SharpGDX.Graphics.G2D;
using SharpGDX.Mathematics;

namespace SuperJumper
{
	public class HelpScreen4 : ScreenAdapter
	{
	SuperJumper game;

	OrthographicCamera guiCam;
	Rectangle nextBounds;
	Vector3 touchPoint;
	Texture helpImage;
	TextureRegion helpRegion;

	public HelpScreen4(SuperJumper game)
	{
		this.game = game;

		guiCam = new OrthographicCamera(320, 480);
		guiCam.position.set(320 / 2, 480 / 2, 0);
		nextBounds = new Rectangle(320 - 64, 0, 64, 64);
		touchPoint = new Vector3();
		helpImage = Assets.loadTexture("assets/data/help4.png");
		helpRegion = new TextureRegion(helpImage, 0, 0, 320, 480);
	}

	public void update()
	{
		if (Gdx.Input.justTouched())
		{
			guiCam.unproject(touchPoint.set(Gdx.Input.getX(), Gdx.Input.getY(), 0));

			if (nextBounds.contains(touchPoint.x, touchPoint.y))
			{
				Assets.playSound(Assets.clickSound);
				game.SetScreen(new HelpScreen5(game));
			}
		}
	}

	public void draw()
	{
		IGL20 gl = Gdx.GL;
		gl.glClear(IGL20.GL_COLOR_BUFFER_BIT);
		guiCam.update();

		game.batcher.setProjectionMatrix(guiCam.combined);
		game.batcher.disableBlending();
		game.batcher.begin();
		game.batcher.draw(helpRegion, 0, 0, 320, 480);
		game.batcher.end();

		game.batcher.enableBlending();
		game.batcher.begin();
		game.batcher.draw(Assets.arrow, 320, 0, -64, 64);
		game.batcher.end();

		gl.glDisable(IGL20.GL_BLEND);
	}

	public override void Render(float delta)
	{
		draw();
		update();
	}

	public override void Hide()
	{
		helpImage.Dispose();
	}
	}
}
