using UnityEngine;
using XInputDotNetPure;
public class InputManager : MonoBehaviour
{
    #region publicEnums
    public enum Gamepads : int
    {
        Gamepad_1 = 0,
        Gamepad_2
    }

    public enum Buttons
    {
        A = 0,
        B,
        X,
        Y,
        DPad_up,
        Dpad_down,
        Dpad_left,
        Dpad_right
    }
    #endregion

    private class Gamepad
    {
        public bool playerIndexSet;
        public PlayerIndex playerIndex = PlayerIndex.Four;
        public GamePadState state;
        public GamePadState prevState;
    }

    #region Private
    Gamepad[] gamepads;
    #endregion

    private void Start()
    {
        gamepads = new Gamepad[2];
        gamepads[0] = new Gamepad();
        gamepads[1] = new Gamepad();
    }

    private void Update()
    {
        if (!gamepads[(int)Gamepads.Gamepad_1].playerIndexSet || !gamepads[(int)Gamepads.Gamepad_2].playerIndexSet)
        {
            QueryGamepads();
        }

        foreach (Gamepad gp in gamepads)
        {
            if (gp.playerIndexSet)
            {
                gp.prevState = gp.state;
                gp.state = GamePad.GetState(gp.playerIndex);
            }
        }
    }

    private void QueryGamepads()
    {
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected && testPlayerIndex != gamepads[(int)Gamepads.Gamepad_1].playerIndex
                || testPlayerIndex != gamepads[(int)Gamepads.Gamepad_2].playerIndex)
            {

                foreach (Gamepad gp in gamepads)
                {
                    if (!gp.playerIndexSet)
                    {
                        gp.state = testState;
                        gp.playerIndex = testPlayerIndex;
                        gp.playerIndexSet = true;
                        break;
                    }
                }
            }
        }
    }

    public bool SetVibration(Gamepads gamepad, float leftMotor, float rightMotor)
    {
        if (!gamepads[(int)gamepad].playerIndexSet) return false;
        GamePad.SetVibration(gamepads[(int)gamepad].playerIndex, leftMotor, rightMotor);
        return true;
    }

    public bool GetButtonDown(Gamepads gamepad, Buttons button)
    {
        if (!gamepads[(int)gamepad].playerIndexSet) return false;
        bool ret = false;

        switch (button)
        {
            case Buttons.A:
                ret = gamepads[(int)gamepad].prevState.Buttons.A == ButtonState.Released
                    && gamepads[(int)gamepad].state.Buttons.A == ButtonState.Pressed;
                break;
            case Buttons.B:
                ret = gamepads[(int)gamepad].prevState.Buttons.B == ButtonState.Released
                    && gamepads[(int)gamepad].state.Buttons.B == ButtonState.Pressed;
                break;
            case Buttons.X:
                ret = gamepads[(int)gamepad].prevState.Buttons.X == ButtonState.Released
                    && gamepads[(int)gamepad].state.Buttons.X == ButtonState.Pressed;
                break;
            case Buttons.Y:
                ret = gamepads[(int)gamepad].prevState.Buttons.Y == ButtonState.Released
                    && gamepads[(int)gamepad].state.Buttons.Y == ButtonState.Pressed;
                break;
            case Buttons.DPad_up:
                ret = gamepads[(int)gamepad].prevState.DPad.Up == ButtonState.Released
                    && gamepads[(int)gamepad].state.DPad.Up == ButtonState.Pressed;
                break;
            case Buttons.Dpad_down:
                ret = gamepads[(int)gamepad].prevState.DPad.Down == ButtonState.Released
                    && gamepads[(int)gamepad].state.DPad.Down == ButtonState.Pressed;
                break;
            case Buttons.Dpad_left:
                ret = gamepads[(int)gamepad].prevState.DPad.Left == ButtonState.Released
                    && gamepads[(int)gamepad].state.DPad.Left == ButtonState.Pressed;
                break;
            case Buttons.Dpad_right:
                ret = gamepads[(int)gamepad].prevState.DPad.Right == ButtonState.Released
                    && gamepads[(int)gamepad].state.DPad.Right == ButtonState.Pressed;
                break;
        }
        return ret;
    }

    public bool GetButtonUp(Gamepads gamepad, Buttons button)
    {
        if (!gamepads[(int)gamepad].playerIndexSet) return false;
        bool ret = false;

        switch (button)
        {
            case Buttons.A:
                ret = gamepads[(int)gamepad].prevState.Buttons.A == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.Buttons.A == ButtonState.Released;
                break;
            case Buttons.B:
                ret = gamepads[(int)gamepad].prevState.Buttons.B == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.Buttons.B == ButtonState.Released;
                break;
            case Buttons.X:
                ret = gamepads[(int)gamepad].prevState.Buttons.X == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.Buttons.X == ButtonState.Released;
                break;
            case Buttons.Y:
                ret = gamepads[(int)gamepad].prevState.Buttons.Y == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.Buttons.Y == ButtonState.Released;
                break;
            case Buttons.DPad_up:
                ret = gamepads[(int)gamepad].prevState.DPad.Up == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.DPad.Up == ButtonState.Released;
                break;
            case Buttons.Dpad_down:
                ret = gamepads[(int)gamepad].prevState.DPad.Down == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.DPad.Down == ButtonState.Released;
                break;
            case Buttons.Dpad_left:
                ret = gamepads[(int)gamepad].prevState.DPad.Left == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.DPad.Left == ButtonState.Released;
                break;
            case Buttons.Dpad_right:
                ret = gamepads[(int)gamepad].prevState.DPad.Right == ButtonState.Pressed
                    && gamepads[(int)gamepad].state.DPad.Right == ButtonState.Released;
                break;
        }
        return ret;
    }
}