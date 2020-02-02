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
        prepareNextObject2,
        endscreen
    };
    public GameState gameState;

    public InputManager inputManager;
    public ScrewdriverController screwdriverController;
    public ObjectController objectController;
    public ObjectBehaviour objectBehaviour;
    public DollyMovement dollyMovement;
    public AudioSource audioSrc;
    public AudioClip audioClip;

    public float objectInitialHeight;

    public GameObject endScreenObj;

    public UIManager uimanager;

    [HideInInspector]
    public uint objectsRepaired;
    public float spinTime = 1.0f;
    public bool startSpinning = false;

    public AudioClip SFXGround;
    #endregion

    #region PRIVATE_VARIABLES
    //private bool objectReady;
    //private bool screwdriverReady;
    private bool firstGame;
    private float spinTimer = 0.0f;
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
            case GameState.prepareNextObject2:
                PrepareNextObject2();
                break;
            case GameState.endscreen:
                UpdateEndScreen();
                break;
        }

        if (startSpinning)
        {
            spinTimer += Time.deltaTime;

            if (spinTimer >= spinTime)
            {
                startSpinning = false;
                spinTimer = 0.0f;
            }
        }
    }

    void UpdateStartScreen()
    {
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
                timer = 0.0f;
                gameState = GameState.play;
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
                timer = 0.0f;
                objectController.GetComponent<Rigidbody>().isKinematic = false;
                objectBehaviour.transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                gameState = GameState.moveObjectSide;
                audioSrc.clip = audioClip;
                audioSrc.Play();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            fromPosition1 = guide1.transform.localPosition;
            toPosition1 = new Vector3(guide1.transform.localPosition.x, 1.0f, guide1.transform.localPosition.z);
            fromPosition2 = guide2.transform.localPosition;
            toPosition2 = new Vector3(guide2.transform.localPosition.x, 1.0f, guide2.transform.localPosition.z);
            timer = 0.0f;
            interpolate = true;
        }
    }

    void MoveObjectDown()
    {
        if (objectBehaviour.TouchGround)
        {
            gameState = GameState.moveScrewdriverDown;
            audioSrc.PlayOneShot(SFXGround);
        }
    }

    void MoveObjectSide()
    {      
        objectController.GetComponent<Rigidbody>().AddForce(1.0f, 0.0f, 0.0f, ForceMode.Impulse);
        if (objectBehaviour.Invisible)
        {
            gameState = GameState.prepareNextObject;
        }
    }

    void PrepareNextObject()
    {
        Destroy(objectBehaviour.transform.GetComponent<Rigidbody>());
        Destroy(objectBehaviour.transform.GetComponent<BoxCollider>());

        gameState = GameState.prepareNextObject2;
    }

    void PrepareNextObject2()
    {
        uint nextObjectHeight = (uint)Random.Range(1, (int)screwdriverController.totalHeights + 1);
        if (nextObjectHeight == objectBehaviour.height)
        {
            if (nextObjectHeight > 1)
            {
                --nextObjectHeight;
            }
            else
            {
                ++nextObjectHeight;
            }
        }
        objectBehaviour.height = nextObjectHeight;
        Debug.Log("Next object height: " + nextObjectHeight);
        switch (objectBehaviour.height)
        {
            case 1:
                objectBehaviour.GetComponent<MeshFilter>().mesh = objectBehaviour.mesh1;
                objectBehaviour.GetComponent<MeshRenderer>().material = objectBehaviour.material1;
                break;
            case 2:
                objectBehaviour.GetComponent<MeshFilter>().mesh = objectBehaviour.mesh2;
                objectBehaviour.GetComponent<MeshRenderer>().material = objectBehaviour.material2;
                break;
            case 3:
                objectBehaviour.GetComponent<MeshFilter>().mesh = objectBehaviour.mesh3;
                objectBehaviour.GetComponent<MeshRenderer>().material = objectBehaviour.material3;
                break;
        }

        objectBehaviour.transform.gameObject.AddComponent<BoxCollider>();
        objectBehaviour.transform.gameObject.AddComponent<Rigidbody>();
        objectBehaviour.RemoveHoles();
        objectBehaviour.SpawnHoles();
        objectBehaviour.transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        objectBehaviour.transform.position = new Vector3(0.0f, objectInitialHeight, 0.0f); // TODO: finish positioning...

        gameState = GameState.moveObjectDown;
    }

    void UpdateEndScreen()
    {
        // TODO: AQUI ESTA LA END SCREEN
    }
}
