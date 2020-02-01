using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public bool Interpolate
    {
        get { return interpolate; }
    }

    public float rotateDegrees;
    public float interpolateSeconds;

    public InputManager inputManager;
    public PlayerController playerController;
    #endregion

    #region PRIVATE_VARIABLES
    private Quaternion fromRotation;
    private Quaternion toRotation;
    private float timer;
    private bool interpolate;
    #endregion

    void Update()
    {
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
        else if (!playerController.ObjectReady)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)
                || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Dpad_left))
            {
                fromRotation = transform.rotation;
                toRotation = transform.rotation * Quaternion.AngleAxis(rotateDegrees, Vector3.up);
                timer = 0.0f;
                interpolate = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)
                || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Dpad_right))
            {
                fromRotation = transform.rotation;
                toRotation = transform.rotation * Quaternion.AngleAxis(rotateDegrees, -Vector3.up);
                timer = 0.0f;
                interpolate = true;
            }
        }
    }
}
