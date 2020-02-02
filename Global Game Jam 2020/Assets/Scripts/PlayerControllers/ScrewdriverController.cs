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
    public GameController gameController;

    public GameObject redScrewdriver;
    public GameObject blueScrewdriver;

    public AudioClip drillDownClip;
    public AudioClip drillUpClip;

    public LayerMask redHoleLayerMask;
    public LayerMask blueHoleLayerMask;
    #endregion

    #region PRIVATE_VARIABLES
    private Vector3 fromPosition1;
    private Vector3 toPosition1;
    private Vector3 fromPosition2;
    private Vector3 toPosition2;
    private float timer;
    private bool interpolate;
    private uint currentHeight;
    private AudioSource audioSource;
    private Transform child1;
    private Transform child2;
    #endregion

    private void Start()
    {
        child1 = transform.Find("Left_robot_arm_mesh").Find("guide_1").Find("clawTube_1");
        child2 = transform.Find("Right_robot_arm_mesh").Find("guide_2").Find("clawTube_2");
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (playerController.Animate)
        {
            return;
        }
        if (gameController.gameState != GameController.GameState.play)
        {
            return;
        }

        if (interpolate)
        {
            float t = timer / interpolateSeconds;
            child1.transform.position = Vector3.Lerp(fromPosition1, toPosition1, t);
            child2.transform.position = Vector3.Lerp(fromPosition2, toPosition2, t);

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
                    fromPosition1 = child1.transform.position;
                    toPosition1 = child1.transform.position + new Vector3(0.0f, heightDistance, 0.0f);
                    fromPosition2 = child2.transform.position;
                    toPosition2 = child2.transform.position + new Vector3(0.0f, heightDistance, 0.0f);
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
                    fromPosition1 = child1.transform.position;
                    toPosition1 = child1.transform.position - new Vector3(0.0f, heightDistance, 0.0f);
                    fromPosition2 = child2.transform.position;
                    toPosition2 = child2.transform.position - new Vector3(0.0f, heightDistance, 0.0f);
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
