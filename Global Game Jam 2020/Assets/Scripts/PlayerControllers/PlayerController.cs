﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public bool ObjectReady
    {
        get { return objectReady; }
    }
    public bool ScrewdriverReady
    {
        get { return screwdriverReady; }
    }

    public ScrewdriverController screwdriverController;
    public ObjectController objectController;
    public InputManager inputManager;
    #endregion

    #region PRIVATE_VARIABLES
    private bool objectReady;
    private bool screwdriverReady;
    private bool animate;
    #endregion

    void Update()
    {
        if (animate)
        {
            // If animation is finished...
            // TODO: animate = false;
        }
        else
        {
            if (!objectController.Interpolate)
            {
                if (Input.GetKeyDown(KeyCode.Return)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.A)) // TODO ALL GAMEPAD BUTTONS
                {
                    objectReady = !objectReady;
                }
            }

            if (!screwdriverController.Interpolate)
            {
                if (Input.GetKeyDown(KeyCode.Space)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.A)) // TODO ALL GAMEPAD BUTTONS
                {
                    screwdriverReady = !screwdriverReady;
                }
            }

            if (objectReady && screwdriverReady)
            {
                animate = true;
            }
        }
    }
}
