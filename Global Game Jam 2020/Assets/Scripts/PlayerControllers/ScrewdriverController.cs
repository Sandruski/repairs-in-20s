using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public uint totalHeights;
    public float heightDistance;
    public float interpolateSeconds;

    public InputManager inputManager;
    #endregion

    #region PRIVATE_VARIABLES
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private float timer;
    private bool interpolate;
    private uint currentHeight;
    #endregion

    void Update()
    {
        if (interpolate)
        {
            float t = timer / interpolateSeconds;
            transform.position = Vector3.Lerp(fromPosition, toPosition, t);

            if (timer >= interpolateSeconds)
            {
                interpolate = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.DPad_up))
            {
                uint desiredHeight = currentHeight + 1;
                if (desiredHeight >= 0 && desiredHeight < totalHeights)
                {
                    fromPosition = transform.position;
                    toPosition = transform.position + new Vector3(0.0f, heightDistance, 0.0f);
                    timer = 0.0f;
                    interpolate = true;
                    currentHeight = desiredHeight;
                }
            }
            else if (inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.Dpad_down))
            {
                uint desiredHeight = currentHeight - 1;
                if (desiredHeight >= 0 && desiredHeight < totalHeights)
                {
                    fromPosition = transform.position;
                    toPosition = transform.position - new Vector3(0.0f, heightDistance, 0.0f);
                    timer = 0.0f;
                    interpolate = true;
                    currentHeight = desiredHeight;
                }
            }
        }
    }
}
