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
            Debug.Log("SCREWING...");
            // If animation is finished...
            // TODO: animate = false;
            inputManager.SetVibration(InputManager.Gamepads.Gamepad_1, 0.5f, 0.5f);
            inputManager.SetVibration(InputManager.Gamepads.Gamepad_2, 0.5f, 0.5f);
        }
        else
        {
            if (!objectController.Interpolate)
            {
                if (Input.GetKeyDown(KeyCode.Return)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.A)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.B)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Y)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.X))
                {
                    objectReady = !objectReady;
                }
            }

            if (!screwdriverController.Interpolate)
            {
                if (Input.GetKeyDown(KeyCode.Space)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.A)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.B)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.Y)
                    || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.X))
                {
                    screwdriverReady = !screwdriverReady;
                }
            }

            if (objectReady && screwdriverReady)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(
                    screwdriverController.redScrewdriver.transform.position,
                    (objectController.transform.position - screwdriverController.redScrewdriver.transform.position).normalized, 
                    out raycastHit,
                    Mathf.Infinity))
                {
                    if (raycastHit.transform.gameObject.name == "RedHole(Clone)")
                    {
                        Debug.Log("RED HOLE HIT");
                        // TODO: red screwdriver animation
                        animate = true;
                    }
                }

                if (Physics.Raycast(
                    screwdriverController.blueScrewdriver.transform.position,
                    (objectController.transform.position - screwdriverController.blueScrewdriver.transform.position).normalized,
                    out raycastHit,
                    Mathf.Infinity))
                {
                    if (raycastHit.transform.gameObject.name == "BlueHole(Clone)")
                    {
                        Debug.Log("BLUE HOLE HIT");
                        // TODO: blue screwdriver animation
                        animate = true;
                    }
                }
            }
        }
    }
}
