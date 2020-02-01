using System.Collections;
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
    public ObjectBehaviour objectBehaviour;
    public InputManager inputManager;
    public GameController gameController;

    public GameObject B_Ready_1;
    public GameObject B_Ready_2;
    #endregion

    #region PRIVATE_VARIABLES
    private bool objectReady;
    private bool screwdriverReady;
    private bool animate;
    #endregion

    private void Start()
    {
        B_Ready_1.active = B_Ready_2.active = false;
    }

void Update()
    {
        if (gameController.gameState != GameController.GameState.play)
        {
            return;
        }

        if (animate)
        {
            Debug.Log("SCREWING...");

            if (objectBehaviour.AreAllHolesRemoved())
            {
                ++gameController.objectsRepaired;
                gameController.gameState = GameController.GameState.moveObjectSide;
            }

            objectReady = screwdriverReady = false;

            animate = false;
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
                    B_Ready_1.active = !B_Ready_1.active;
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
                    B_Ready_2.active = !B_Ready_2.active;
                }
            }

            if (objectReady && screwdriverReady)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(
                    screwdriverController.redScrewdriver.transform.position,
                    Vector3.left, 
                    out raycastHit,
                    Mathf.Infinity))
                {
                    if (raycastHit.transform.gameObject.name == "RedHole(Clone)")
                    {
                        ClawRotation.instance.DrillRight();
                        B_Ready_2.active = B_Ready_1.active = false;

                        Debug.Log("RED HOLE HIT");
                        // TODO: red screwdriver animation
                        objectBehaviour.RemoveHole(raycastHit.transform.gameObject);
                        animate = true;
                    }
                }

                if (Physics.Raycast(
                    screwdriverController.blueScrewdriver.transform.position,
                    Vector3.right,
                    out raycastHit,
                    Mathf.Infinity))
                {
                    if (raycastHit.transform.gameObject.name == "BlueHole(Clone)")
                    {
                        ClawRotation.instance.DrillLeft();
                        B_Ready_1.active = B_Ready_2.active = false;

                        Debug.Log("BLUE HOLE HIT");
                        // TODO: blue screwdriver animation
                        objectBehaviour.RemoveHole(raycastHit.transform.gameObject);
                        animate = true;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(screwdriverController.redScrewdriver.transform.position,
            screwdriverController.redScrewdriver.transform.position + Vector3.left * 5.0f, Color.red);
        Debug.DrawLine(screwdriverController.blueScrewdriver.transform.position,
            screwdriverController.blueScrewdriver.transform.position + Vector3.right * 5.0f, Color.blue);
    }
}
