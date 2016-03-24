using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace AlienAttackUniversal
{
	public struct ControlState
	{
		public bool Left;
		public bool Right;
		public bool Start;
		public bool Quit;
		public bool Fire;
		public float FingerPosition;
	}

	public static class InputManager
	{
		private static KeyboardState _keyboardState, _lastKeyboard;
		private static GamePadState _gamePadState, _lastGamePad;
		private static ControlState _controlState;

		static InputManager()
		{
			TouchPanel.EnabledGestures = GestureType.Tap | GestureType.HorizontalDrag;
		}

		public static void Update()
		{
			_keyboardState = Keyboard.GetState();
			_gamePadState = GamePad.GetState(PlayerIndex.One);

			_controlState.Quit	= (_gamePadState.Buttons.Back== ButtonState.Pressed || _keyboardState.IsKeyDown(Keys.Escape));
			_controlState.Start	= (_gamePadState.Buttons.B	== ButtonState.Pressed 
									|| _keyboardState.IsKeyDown(Keys.Enter) || _keyboardState.IsKeyDown(Keys.Space) || _gamePadState.IsButtonDown(Buttons.Start)); 
			_controlState.Left	= (_gamePadState.DPad.Left	== ButtonState.Pressed || _gamePadState.ThumbSticks.Left.X < -0.1f || _keyboardState.IsKeyDown(Keys.Left));
			_controlState.Right	= (_gamePadState.DPad.Right	== ButtonState.Pressed || _gamePadState.ThumbSticks.Left.X > 0.1f || _keyboardState.IsKeyDown(Keys.Right));
			_controlState.Fire	= ((_gamePadState.Buttons.B	== ButtonState.Pressed && _lastGamePad.Buttons.B == ButtonState.Released) 
									|| (_keyboardState.IsKeyDown(Keys.Space) && !_lastKeyboard.IsKeyDown(Keys.Space)));

			while(TouchPanel.IsGestureAvailable)
			{
				GestureSample gs = TouchPanel.ReadGesture();
				_controlState.Fire |= (gs.GestureType == GestureType.Tap);
				_controlState.Start |= (gs.GestureType == GestureType.Tap);
				if(gs.GestureType == GestureType.HorizontalDrag)
				{
					_controlState.Left = (gs.Delta.X < 0);
					_controlState.Right = (gs.Delta.X > 0);
					_controlState.FingerPosition = gs.Position.X;
				}
			}

			_lastGamePad = _gamePadState;
			_lastKeyboard = _keyboardState;
		}

		public static ControlState ControlState
		{
			get { return _controlState; }
		}
	}
}
