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
	public class HelpScreen : ScreenAdapter {
	SuperJumper game;

	OrthographicCamera guiCam;
	Rectangle nextBounds;
	Vector3 touchPoint;
	Texture helpImage;
	TextureRegion helpRegion;

	public HelpScreen (SuperJumper game) {
		this.game = game;
		guiCam = new OrthographicCamera();
		guiCam.setToOrtho(false, 320, 480);
		nextBounds = new Rectangle(320 - 64, 0, 64, 64);
		touchPoint = new Vector3();
		helpImage = Assets.loadTexture("assets/data/help1.png");
		helpRegion = new TextureRegion(helpImage, 0, 0, 320, 480);
	}

	public void update () {
		if (Gdx.input.justTouched()) {
			guiCam.unproject(touchPoint.set(Gdx.input.getX(), Gdx.input.getY(), 0));

			if (nextBounds.contains(touchPoint.x, touchPoint.y)) {
				Assets.playSound(Assets.clickSound);
				game.SetScreen(new HelpScreen2(game));
			}
		}
	}

	public void draw () {
		GL20 gl = Gdx.gl;
		gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
		gl.glViewport(0, 0, Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
		
		guiCam.update();
		game.batcher.setProjectionMatrix(guiCam.combined);
		game.batcher.disableBlending();
		game.batcher.begin();
		game.batcher.draw(helpRegion, 0, 0);
		game.batcher.end();

		game.batcher.enableBlending();
		game.batcher.begin();
		game.batcher.draw(Assets.arrow, 320, 0, -64, 64);
		game.batcher.end();
	}

	public override void Render (float delta) {
		draw();
		update();
	}

	public override void Hide () {
		helpImage.Dispose();
	}
}
}
