using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public bool Interpolate
    {
        get { return interpolate; }
    }

    public uint totalHeights;
    public float heightDistance;
    public float interpolateSeconds;

    public InputManager inputManager;
    public PlayerController playerController;

    public GameObject redScrewdriver;
    public GameObject blueScrewdriver;

    public AudioClip drillDownClip;
    public AudioClip drillUpClip;

    public LayerMask redHoleLayerMask;
    public LayerMask blueHoleLayerMask;
    #endregion

    #region PRIVATE_VARIABLES
    private Vector3 fromPosition;
    private Vector3 toPosition;
    private float timer;
    private bool interpolate;
    private uint currentHeight;
    private AudioSource audioSource;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
        else if (!playerController.ScrewdriverReady)
        {
            if (Input.GetKeyDown(KeyCode.W)
                || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.DPad_up))
            {
                uint desiredHeight = currentHeight + 1;
                if (desiredHeight >= 0 && desiredHeight < totalHeights)
                {
                    fromPosition = transform.position;
                    toPosition = transform.position + new Vector3(0.0f, heightDistance, 0.0f);
                    timer = 0.0f;
                    interpolate = true;
                    currentHeight = desiredHeight;

                    //PLAY DRILL UP
                    audioSource.PlayOneShot(drillUpClip);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S)
                || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_2, InputManager.Buttons.Dpad_down))
            {
                uint desiredHeight = currentHeight - 1;
                if (desiredHeight >= 0 && desiredHeight < totalHeights)
                {
                    fromPosition = transform.position;
                    toPosition = transform.position - new Vector3(0.0f, heightDistance, 0.0f);
                    timer = 0.0f;
                    interpolate = true;
                    currentHeight = desiredHeight;

                    //PLAY DRILL UP
                    audioSource.PlayOneShot(drillDownClip);
                }
            }
        }
    }
}
