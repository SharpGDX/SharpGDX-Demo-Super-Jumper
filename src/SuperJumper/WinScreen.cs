﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX;
using SharpGDX.Graphics;
using SharpGDX.Graphics.G2D;
using SharpGDX.Utils;

namespace SuperJumper
{
	public class WinScreen : ScreenAdapter {
	SuperJumper game;
	OrthographicCamera cam;
	TextureRegion princess;
	String[] messages = { "Princess: Oh dear!\n What have you done?",
						  "Bob: I came to \nrescue you!",
						  "Princess: you are\n mistaken\nI need no rescueing",
						  "Bob: So all this \nwork for nothing?",
						  "Princess: I have \ncake and tea!\nWould you like some?",
						  "Bob: I'd be my \npleasure!",
						  "And they ate cake\nand drank tea\nhappily ever \nafter\n\n\n\n\n\n\nKära Emma!\nDu är fantastisk!\nDu blev ferdig\n med spelet!"
			};
	int currentMessage = 0;
	
	public WinScreen(SuperJumper game) {
		this.game = game;
		cam = new OrthographicCamera();
		cam.setToOrtho(false, 320, 480);
		princess = new TextureRegion(Assets.arrow.getTexture(), 210, 122, -40, 38);
	}
	
	public override void Render(float delta) {
		if(Gdx.Input.justTouched()) {
			currentMessage++;
			if(currentMessage == messages.Length) {
				currentMessage--;
				game.SetScreen(new MainMenuScreen(game));
			}
		}
		
		Gdx.GL.glClear(IGL20.GL_COLOR_BUFFER_BIT);
		cam.update();
		game.batcher.setProjectionMatrix(cam.combined);
		game.batcher.begin();
		game.batcher.draw(Assets.backgroundRegion, 0, 0);
		game.batcher.draw(Assets.castle, 60, 120, 200, 200);
		game.batcher.draw(Assets.bobFall.getKeyFrame(0, Animation.ANIMATION_LOOPING), 120, 200);
		Assets.font.draw(game.batcher, messages[currentMessage], 0, 400, 320, Align.center, false);
		game.batcher.draw(princess,150, 200);
		game.batcher.end();
	}
}
}
