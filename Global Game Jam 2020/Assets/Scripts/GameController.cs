using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public enum GameState { 
        startscreen,
        moveScrewdriverDown, 
        moveObjectDown,
        play,
        moveScrewdriverUp, 
        moveObjectSide,
        prepareNextObject,
        endscreen
    };
    public GameState gameState;

    public InputManager inputManager;
    public ScrewdriverController screwdriverController;
    public ObjectController objectController;
    public ObjectBehaviour objectBehaviour;
    public DollyMovement dollyMovement;

    public float objectInitialHeight;

    [HideInInspector]
    public uint objectsRepaired;
    #endregion

    #region PRIVATE_VARIABLES
    //private bool objectReady;
    //private bool screwdriverReady;
    private bool firstGame;
    #endregion

    Transform guide1;
    Transform guide2;


    void Start()
    {
        firstGame = true;
        guide1 = screwdriverController.transform.Find("Left_robot_arm_mesh").Find("guide_1");
        guide2 = screwdriverController.transform.Find("Right_robot_arm_mesh").Find("guide_2");
    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.startscreen:
                UpdateStartScreen();
                break;
            case GameState.moveScrewdriverDown:
                UpdateMoveScrewdriverDown();
                break;
            case GameState.moveObjectDown:
                MoveObjectDown();
                break;
            case GameState.moveScrewdriverUp:
                UpdateMoveScrewdriverUp();
                break;
            case GameState.play:
                break;
            case GameState.moveObjectSide:
                MoveObjectSide();
                break;
            case GameState.prepareNextObject:
                PrepareNextObject();
                break;
            case GameState.endscreen:
                UpdateEndScreen();
                break;
        }
    }

    void UpdateStartScreen()
    {
        /*
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
        }*/

        if (dollyMovement.isAtEnd)
        {
            gameState = GameState.moveScrewdriverDown;
        }
    }

    public float screwdriverDownInterpolateSeconds;
    public float screwdriverUpInterpolateSeconds;

    private Vector3 fromPosition1;
    private Vector3 toPosition1;
    private Vector3 fromPosition2;
    private Vector3 toPosition2;
    private float timer;
    private bool interpolate;

    void UpdateMoveScrewdriverDown()
    {
        if (interpolate)
        {
            float t = timer / screwdriverDownInterpolateSeconds;
            guide1.transform.localPosition = Vector3.Lerp(fromPosition1, toPosition1, t);
            guide2.transform.localPosition = Vector3.Lerp(fromPosition2, toPosition2, t);

            if (timer >= screwdriverDownInterpolateSeconds)
            {
                interpolate = false;
                if (!firstGame)
                {
                    objectBehaviour.RemoveHoles();
                    objectBehaviour.SpawnHoles();
                    objectController.GetComponent<Rigidbody>().isKinematic = false;
                    gameState = GameState.moveObjectDown;
                }
                else
                {
                    gameState = GameState.play;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            fromPosition1 = guide1.transform.localPosition;
            toPosition1 = new Vector3(guide1.transform.localPosition.x, -0.58f, guide1.transform.localPosition.z);
            fromPosition2 = guide2.transform.localPosition;
            toPosition2 = new Vector3(guide2.transform.localPosition.x, -0.58f, guide2.transform.localPosition.z);
            timer = 0.0f;
            interpolate = true;
        }
    }

    void UpdateMoveScrewdriverUp()
    {
        if (interpolate)
        {
            float t = timer / screwdriverUpInterpolateSeconds;
            guide1.transform.localPosition = Vector3.Lerp(fromPosition1, toPosition1, t);
            guide2.transform.localPosition = Vector3.Lerp(fromPosition2, toPosition2, t);

            if (timer >= screwdriverUpInterpolateSeconds)
            {
                interpolate = false;
                objectController.GetComponent<Rigidbody>().isKinematic = false;
                gameState = GameState.moveObjectSide;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            fromPosition1 = guide1.transform.localPosition;
            toPosition1 = new Vector3(guide1.transform.localPosition.x, 0.025f, guide1.transform.localPosition.z);
            fromPosition2 = guide2.transform.localPosition;
            toPosition2 = new Vector3(guide2.transform.localPosition.x, 0.025f, guide2.transform.localPosition.z);
            timer = 0.0f;
            interpolate = true;
        }
    }

    void MoveObjectDown()
    {
        if (objectBehaviour.TouchGround)
        {
            gameState = GameState.play;
        }
    }

    void MoveObjectSide()
    {      
        objectController.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse);
    }

    void PrepareNextObject()
    {
        uint nextObjectHeight = (uint)Random.Range(0, (int)screwdriverController.totalHeights);
        if (nextObjectHeight == objectBehaviour.height)
        {
            if (nextObjectHeight > 0)
            {
                --nextObjectHeight;
            }
            else
            {
                ++nextObjectHeight;
            }
        }
        objectBehaviour.height = nextObjectHeight;
        switch (objectBehaviour.height)
        {
            case 0:
                objectBehaviour.GetComponent<MeshFilter>().mesh = objectBehaviour.mesh1;
                return;
            case 1:
                objectBehaviour.GetComponent<MeshFilter>().mesh = objectBehaviour.mesh2;
                return;
            case 2:
                objectBehaviour.GetComponent<MeshFilter>().mesh = objectBehaviour.mesh3;
                return;
        }

        Debug.Log("Destroy and add box collider");
        Destroy(objectBehaviour.transform.GetComponent<BoxCollider>());
        objectBehaviour.transform.gameObject.AddComponent<BoxCollider>();
        objectBehaviour.transform.position = new Vector3(0.0f, objectInitialHeight, 0.0f); // TODO: finish positioning...

        gameState = GameState.moveScrewdriverDown;
    }

    void UpdateEndScreen()
    {
        // TODO: AQUI ESTA LA END SCREEN
    }
}
