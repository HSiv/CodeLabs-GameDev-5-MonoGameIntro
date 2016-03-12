namespace AlienAttackUniversal
{
	public class Player : Sprite
	{
		public Player()
		{
			// TODO: Animation for left/right!
			LoadContent(AlienAttackGame.Instance.Content,  "gfx\\player\\player");
		}
	}
}
