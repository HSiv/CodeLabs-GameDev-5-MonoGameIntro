using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AlienAttackUniversal
{
	public static class AudioManager
	{
		// the different fx that can be played
		public enum Cue
		{
			EnemyShot,
			PlayerShot,
			Explosion
		};

		// instances of the effects
		private static readonly Song _theme;
		private static readonly SoundEffect _enemyShot;
		private	static readonly SoundEffect _playerShot;
		private static readonly SoundEffect _explosion;

		static AudioManager()
		{
			// load 'em up
			_theme = AlienAttackGame.Instance.Content.Load<Song>("sfx\\theme");           
			_enemyShot = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\enemyShot");
			_playerShot = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\playerShot");
			_explosion = AlienAttackGame.Instance.Content.Load<SoundEffect>("sfx\\explosion");
		}

		public static void PlayCue(Cue cue)
		{
			// play the effect requested
			switch(cue)
			{
				case Cue.EnemyShot:
					_enemyShot.Play();
					break;
				case Cue.PlayerShot:
					_playerShot.Play();
					break;
				case Cue.Explosion:
					_explosion.Play();
					break;
			}
		}

		public static void StartTheme()
		{
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(_theme);
		}

		public static void StopTheme()
		{
			MediaPlayer.Stop();
		}
	}
}
