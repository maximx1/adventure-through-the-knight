using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace adventure_through_the_knight.Input
{
    public class InputController
    {
        //Active input functions
        private bool[] ButtonList;
        private Vector2 LeftTS;
        private Vector2 RightTS;
        private Vector2 MousePosition;
        private Dictionary<G_key.G_KEY, Vector2> KeyDictionary;

        //Constant states for all input devices
        private InputDeviceType InputType;
        private KeyboardState InputKeyboard;
        private GamePadState InputGamepad;
        private MouseState InputMouse;

        //Input Type register
        public enum InputDeviceType { KEYBOARD, GAMEPAD };

        //TODO: Add state for xbox 360 controller

        /// <summary>
        /// Initializes a new instance of the <see cref="RPGGame.InputController"/> class.
        /// </summary>
        /// <param name="inputType">The input device type. Defaults the keyboard if you send empty or null.</param>
        public InputController(InputDeviceType inputType)
        {
            InputType = inputType;

            List<bool> buttonListMaker = new List<bool>();
            for (int i = 0; i < Enum.GetNames(typeof(G_key.G_KEY)).Length; i++)
            {
                buttonListMaker.Add(false);
            }
            ButtonList = buttonListMaker.ToArray();

            //First Player keyboard
            InputKeyboard = InputType == InputDeviceType.KEYBOARD ? Keyboard.GetState() : new KeyboardState();
            //First Player mouse
            InputMouse = inputType == InputDeviceType.KEYBOARD ? Mouse.GetState() : new MouseState();
            //First Player gamepad
            InputGamepad = InputType == InputDeviceType.GAMEPAD ? GamePad.GetState(PlayerIndex.One) : new GamePadState();


            KeyDictionary = new Dictionary<G_key.G_KEY, Vector2>
            {
                {G_key.G_KEY.LEFT, new Vector2(-1, 0)},
                {G_key.G_KEY.RIGHT, new Vector2(1, 0)},
                {G_key.G_KEY.UP, new Vector2(0, -1)},
                {G_key.G_KEY.DOWN, new Vector2(0, 1)}
            };
        }

        /// <summary>
        /// Gets the state of the user's input device.
        /// </summary>
        public void GetState()
        {
            if (InputType == InputDeviceType.KEYBOARD)
            {
                //Keyboard input
                LeftTS = Vector2.Zero;
                InputKeyboard = Keyboard.GetState();
                ButtonList[0] = InputKeyboard.IsKeyDown(Keys.Escape) ? true : false;
                ButtonList[1] = InputKeyboard.IsKeyDown(Keys.W) ? true : false;
                ButtonList[2] = InputKeyboard.IsKeyDown(Keys.S) ? true : false;
                ButtonList[3] = InputKeyboard.IsKeyDown(Keys.A) ? true : false;
                ButtonList[4] = InputKeyboard.IsKeyDown(Keys.D) ? true : false;
                ButtonList[5] = InputKeyboard.IsKeyDown(Keys.LeftShift) ? true : false;

                //Generate a vector based on what keys are pressed.
                foreach (var key in KeyDictionary)
                {
                    if (this.IsPressed(key.Key))
                        LeftTS += key.Value;
                }
                //RightTS = LeftTS;

                //Mouse input
                InputMouse = Mouse.GetState();
                MousePosition = new Vector2(InputMouse.X, InputMouse.Y);

            }

            if (InputType == InputDeviceType.GAMEPAD)
            {
                InputGamepad = GamePad.GetState(PlayerIndex.One);

                ButtonList[0] = InputGamepad.Buttons.Start == ButtonState.Pressed ? true : false;

                //Reset the Button inputs
                for (int i = 1; i < ButtonList.Length; i++)
                {
                    ButtonList[i] = false;
                }

                //Set the button states.
                //up
                if (InputGamepad.ThumbSticks.Left.Y > .1f)
                {
                    ButtonList[1] = true;
                }

                //down
                if (InputGamepad.ThumbSticks.Left.Y < -.1f)
                {
                    ButtonList[2] = true;
                }

                //left
                if (InputGamepad.ThumbSticks.Left.X < -.1f)
                {
                    ButtonList[3] = true;
                }

                //right
                if (InputGamepad.ThumbSticks.Left.X > .1f)
                {
                    ButtonList[4] = true;
                }

                //shift
                if (InputGamepad.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    ButtonList[5] = true;
                }

                LeftTS = InputGamepad.ThumbSticks.Left;
                LeftTS.Y *= -1;
                RightTS = InputGamepad.ThumbSticks.Right;
                RightTS.Y *= -1;
            }
        }

        /// <summary>
        /// Detects if a specific key is pressed.
        /// </summary>
        /// <param name="key">Key to be detected.</param>
        /// <returns>true if the key is pressed.</returns>
        public bool IsPressed(G_key.G_KEY key)
        {
            return ButtonList[(int)key];
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is PAUS.
        /// </summary>
        /// <value>
        /// <c>true</c> if PAUS; otherwise, <c>false</c>.
        /// </value>
        public bool PAUSE { get { return ButtonList[0]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is FORWAR.
        /// </summary>
        /// <value>
        /// <c>true</c> if FORWAR; otherwise, <c>false</c>.
        /// </value>
        public bool UP { get { return ButtonList[1]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is BACKWARD.
        /// </summary>
        /// <value>
        /// <c>true</c> if BACKWARD; otherwise, <c>false</c>.
        /// </value>
        public bool DOWN { get { return ButtonList[2]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is LEF.
        /// </summary>
        /// <value>
        /// <c>true</c> if LEF; otherwise, <c>false</c>.
        /// </value>
        public bool LEFT { get { return ButtonList[3]; } }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RPGGame.InputController"/> is RIGH.
        /// </summary>
        /// <value>
        /// <c>true</c> if RIGH; otherwise, <c>false</c>.
        /// </value>
        public bool RIGHT { get { return ButtonList[4]; } }

        /// <summary>
        /// Gets the Left Shift key.
        /// </summary>
        public bool LSHIFT { get { return ButtonList[5]; } }

        /// <summary>
        /// Gets the Vector value of the left thumbstick.
        /// </summary>
        public Vector2 LEFT_THUMBSTICK { get { return LeftTS; } }

        /// <summary>
        /// Gets the Vector value of the right thumbstick.
        /// </summary>
        public Vector2 RIGHT_THUMBSTICK { get { return RightTS; } }

        /// <summary>
        /// Gets the position of the mouse.
        /// </summary>
        /// <value>
        /// The mouse Position
        /// </value>
        public Vector2 MOUSE_POSITION { get { return MousePosition; } }
    }
}