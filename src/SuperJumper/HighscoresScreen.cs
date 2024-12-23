﻿using System;
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
	public class HighscoresScreen : ScreenAdapter {
	SuperJumper game;
	OrthographicCamera guiCam;
	Rectangle backBounds;
	Vector3 touchPoint;
	String[] highScores;
	float xOffset = 0;
	GlyphLayout glyphLayout = new GlyphLayout();

	public HighscoresScreen (SuperJumper game) {
		this.game = game;

		guiCam = new OrthographicCamera(320, 480);
		guiCam.position.set(320 / 2, 480 / 2, 0);
		backBounds = new Rectangle(0, 0, 64, 64);
		touchPoint = new Vector3();
		highScores = new String[5];
		for (int i = 0; i < 5; i++) {
			highScores[i] = i + 1 + ". " + Settings.highscores[i];
			glyphLayout.setText(Assets.font, highScores[i]);
			xOffset = Math.Max(glyphLayout.width, xOffset);
		}
		xOffset = 160 - xOffset / 2 + Assets.font.getSpaceXadvance() / 2;
	}

	public void update () {
		if (Gdx.Input.justTouched()) {
			guiCam.unproject(touchPoint.set(Gdx.Input.getX(), Gdx.Input.getY(), 0));

			if (backBounds.contains(touchPoint.x, touchPoint.y)) {
				Assets.playSound(Assets.clickSound);
				game.SetScreen(new MainMenuScreen(game));
				return;
			}
		}
	}

	public void draw () {
        IGL20 gl = Gdx.GL;
		gl.glClear(IGL20.GL_COLOR_BUFFER_BIT);
		guiCam.update();

		game.batcher.setProjectionMatrix(guiCam.combined);
		game.batcher.disableBlending();
		game.batcher.begin();
		game.batcher.draw(Assets.backgroundRegion, 0, 0, 320, 480);
		game.batcher.end();

		game.batcher.enableBlending();
		game.batcher.begin();
		game.batcher.draw(Assets.highScoresRegion, 10, 360 - 16, 300, 33);

		float y = 230;
		for (int i = 4; i >= 0; i--) {
			Assets.font.draw(game.batcher, highScores[i], xOffset, y);
			y += Assets.font.getLineHeight();
		}

		game.batcher.draw(Assets.arrow, 0, 0, 64, 64);
		game.batcher.end();
	}

	public override void Render (float delta) {
		update();
		draw();
	}
}
}
