using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public float rotateDegrees;
    public float interpolateSeconds;

    public InputManager inputManager;
    #endregion

    #region PRIVATE_VARIABLES
    private Quaternion fromRotation;
    private Quaternion toRotation;
    private float timer;
    private bool interpolate;
    #endregion

    void Start()
    {
        fromRotation = Quaternion.identity;
        toRotation = Quaternion.identity;
        timer = 0.0f;
        interpolate = false;
    }

    void Update()
    {
        if (inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Dpad_left))
        {
            fromRotation = transform.rotation;
            toRotation = transform.rotation * Quaternion.AngleAxis(rotateDegrees, Vector3.up);
            timer = 0.0f;
            interpolate = true;
        }
        else if (inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Dpad_right))
        {
            fromRotation = transform.rotation;
            toRotation = transform.rotation * Quaternion.AngleAxis(rotateDegrees, -Vector3.up);
            timer = 0.0f;
            interpolate = true;
        }

        if (interpolate)
        {
            float t = timer / interpolateSeconds;
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);

            if (timer >= interpolateSeconds)
            {
                interpolate = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
