using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlienAttackUniversal
{
	public class Sprite
	{
		// all textures in animation set
		protected Texture2D[] Frames { get; set; }

		// current frame to draw
		protected int FrameIndex { get; set; }

		// total number of frames
		protected int FrameCount { get; set; }

		// size of sprite
		public Vector2 Size { get { return new Vector2(Width, Height); } }
		public int Width { get; set; }
		public int Height { get; set; }

		public Vector2 Position { get; set; }
		public Vector2 Velocity { get; set; }
		public float Rotation { get; set; }
		public Vector2 Scale { get; set; }

		// variable to track number of millieconds for animations
		private double _time;

		// bounding box of sprite...used for collisions
		private Rectangle _boundingBox;
		private int _animationDirection = 1;

		public Sprite()
		{
			Scale = Vector2.One;
		}

		public virtual void LoadContent(ContentManager contentManager, string name)
		{
			// load single frame
			Frames = new Texture2D[1];
			Frames[0] = contentManager.Load<Texture2D>(name);
			Width = Frames[0].Width;
			Height = Frames[0].Height;
		}

		public virtual void LoadContent(ContentManager contentManager, string name, int count)
		{
			// load multiple frames
			Frames = new Texture2D[count];
			for(int i = 0; i < count; i++)
				Frames[i] = contentManager.Load<Texture2D>(string.Format(name, i));
			FrameCount = count;
			Width = Frames[0].Width;
			Height = Frames[0].Height;
		}

		public virtual void AnimateLoop(GameTime gameTime, double frameTime)
		{
			// count number of milliseconds
			_time += gameTime.ElapsedGameTime.TotalMilliseconds;

			// if we're over the time for the next frame, move on
			if(_time > frameTime)
			{
				FrameIndex++;
				_time -= frameTime;
			}

			// if we're past the # of frames, start back at 0
			if(FrameIndex > FrameCount-1)
				FrameIndex = 0;
		}

		public virtual void AnimateReverse(GameTime gameTime, double frameTime)
		{
			// same as above, but reverse direction instead of starting back at 0
			_time += gameTime.ElapsedGameTime.TotalMilliseconds;
			if(_time > frameTime)
			{
				FrameIndex += _animationDirection;
				_time -= frameTime;

				if(FrameIndex == 0 || FrameIndex > FrameCount-1)
				{
					_animationDirection *= -1;
					FrameIndex += _animationDirection;
				}
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			// move the sprite 1 velocity unit
			Position += Velocity * gameTime.ElapsedGameTime.Milliseconds;
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if(Frames == null)
				return;

			spriteBatch.Draw(Frames[FrameIndex], position:Position, color:Color.White, scale:Scale);
		}

		public virtual Rectangle BoundingBox
		{
			get 
			{
				// only need to assign this once
				if(_boundingBox == Rectangle.Empty)
				{
					_boundingBox.Width = Width;
					_boundingBox.Height = Height;
				}
				_boundingBox.X = (int)Position.X;
				_boundingBox.Y = (int)Position.Y;

				return _boundingBox;
			}
		}
	}
}
