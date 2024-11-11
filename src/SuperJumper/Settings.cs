using SharpGDX;
using SharpGDX.Files;

namespace SuperJumper
{
	internal class Settings
	{
		public static bool soundEnabled = true;
		public static int[] highscores = new int[] { 100, 80, 50, 30, 10 };
		public readonly static String file = ".superjumper";

		public static void load()
		{
			try
			{
				FileHandle filehandle = Gdx.Files.External(file);

				String[] strings = filehandle.readString().Split("\n");

				soundEnabled = Boolean.Parse(strings[0]);
				for (int i = 0; i < 5; i++)
				{
					highscores[i] = Int32.Parse(strings[i + 1]);
				}
			}
			catch (Exception e)
			{
				// :( It's ok we have defaults
			}
		}

		public static void save()
		{
			try
			{
				FileHandle filehandle = Gdx.Files.External(file);

				filehandle.writeString((soundEnabled.ToString()) + "\n", false);
				for (int i = 0; i < 5; i++)
				{
					filehandle.writeString((highscores[i]).ToString() + "\n", true);
				}
			}
			catch (Exception e)
			{
			}
		}

		public static void addScore(int score)
		{
			for (int i = 0; i < 5; i++)
			{
				if (highscores[i] < score)
				{
					for (int j = 4; j > i; j--)
						highscores[j] = highscores[j - 1];
					highscores[i] = score;
					break;
				}
			}
		}
	}
}
