using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGDX.Graphics.G2D;

namespace SuperJumper
{
	public class Animation
	{
		public static readonly int ANIMATION_LOOPING = 0;
		public static readonly int ANIMATION_NONLOOPING = 1;

		readonly TextureRegion[] keyFrames;
		readonly float frameDuration;

		public Animation(float frameDuration, params TextureRegion[] keyFrames)
		{
			this.frameDuration = frameDuration;
			this.keyFrames = keyFrames;
		}

		public TextureRegion getKeyFrame(float stateTime, int mode)
		{
			int frameNumber = (int)(stateTime / frameDuration);

			if (mode == ANIMATION_NONLOOPING)
			{
				frameNumber = Math.Min(keyFrames.Length - 1, frameNumber);
			}
			else
			{
				frameNumber = frameNumber % keyFrames.Length;
			}
			return keyFrames[frameNumber];
		}
	}
}
