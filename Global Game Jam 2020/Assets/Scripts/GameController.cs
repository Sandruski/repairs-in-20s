using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public enum GameState { startscreen, play, moveScrewdriverDown, spawnObject, moveScrewdriverUp, moveObject, endscreen };
    public GameState gameState;

    public InputManager inputManager;
    #endregion

    #region PRIVATE_VARIABLES
    private bool objectReady;
    private bool screwdriverReady;
    #endregion

    void Update()
    {
        switch(gameState)
        {
            case GameState.startscreen:
                break;
            case GameState.play:
                break;
            case GameState.moveScrewdriverDown:
                break;
            case GameState.spawnObject:
                break;
            case GameState.moveScrewdriverUp:
                break;
            case GameState.endscreen:
                break;
        }
    }

    void UpdateStartScreen()
    {
        if (Input.GetKeyDown(KeyCode.Return)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.A)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.B)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Y)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.X))
        {
            objectReady = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.A)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.B)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.Y)
            || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.X))
        {
            screwdriverReady = true;
        }

        if (objectReady && screwdriverReady)
        {
            gameState = GameState.play;
        }
    }
}
