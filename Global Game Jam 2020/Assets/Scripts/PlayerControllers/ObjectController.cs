using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameController gameController;

    public AudioClip flipAudioClipLeft;
    public AudioClip flipAudioClipRight;

    public float GameplayTimer = 20.0f;

    public GameObject endScreenObj;
    public Text scoreText;
    #endregion

    #region PRIVATE_VARIABLES
    private Quaternion fromRotation;
    private Quaternion toRotation;
    private float timer;
    private bool interpolate;

    private AudioSource audioSource;
    #endregion

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameController.gameState != GameController.GameState.play)
        {
            return;
        }

        if (gameController.gameState == GameController.GameState.play)
        {
            GameplayTimer -= Time.deltaTime;
            if (GameplayTimer <= 0.0f)
            {
                GameplayTimer = 0.0f;
                gameController.gameState = GameController.GameState.endscreen;
                // TODO: AQUI ANEM AL ESTAT DE ACABAR EL JOC. passa 1 cop nomes per aqui
                endScreenObj.SetActive(true);
                scoreText.text = "you have repaired " + playerController.score + " screens in 20s!"; 

            }
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
        else if (!playerController.ObjectReady)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)
                || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Dpad_left))
            {
                fromRotation = transform.rotation;
                toRotation = transform.rotation * Quaternion.AngleAxis(rotateDegrees, Vector3.up);
                timer = 0.0f;
                interpolate = true;

                //PLAY SOUND FLIP Left
                audioSource.PlayOneShot(flipAudioClipLeft);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)
                || inputManager.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.Dpad_right))
            {
                fromRotation = transform.rotation;
                toRotation = transform.rotation * Quaternion.AngleAxis(rotateDegrees, -Vector3.up);
                timer = 0.0f;
                interpolate = true;

                //PLAY SOUND FLIP Left
                audioSource.PlayOneShot(flipAudioClipRight);
            }
        }
    }
}
