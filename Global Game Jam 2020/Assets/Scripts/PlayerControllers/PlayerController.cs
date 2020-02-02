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
    public bool Animate
    {
        get { return animate; }
    }

    public ScrewdriverController screwdriverController;
    public ObjectController objectController;
    public ObjectBehaviour objectBehaviour;
    public InputManager inputManager;
    public GameController gameController;

    public AudioSource audioSrc;
    public AudioClip readySfx;

    public GameObject B_Ready_1;
    public GameObject B_Ready_2;

    public ParticleSystem sparks;
    public int score = 0;
    #endregion

    #region PRIVATE_VARIABLES
    private bool objectReady;
    private bool screwdriverReady;
    private bool animate;
    private ParticleSystem currentRedSparks = null;
    private ParticleSystem currentBlueSparks = null;
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
            if (!ClawRotation.instance.drill_L && !ClawRotation.instance.drill_R)
            {
                if (objectBehaviour.AreAllHolesRemoved())
                {
                    score++;
                    Debug.Log("All holes have been removed!");
                    ++gameController.objectsRepaired;
                    gameController.gameState = GameController.GameState.moveScrewdriverUp;
                    objectController.GameplayTimer += 5.0f;
                }

                B_Ready_2.active = B_Ready_1.active = false;
                animate = false;
            }
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
                    audioSrc.clip = readySfx;
                    audioSrc.Play();
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
                    audioSrc.clip = readySfx;
                    audioSrc.Play();
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
                        // TODO: el gameobject es el tornillo red. CHISPAS
                        currentRedSparks = Instantiate(sparks, raycastHit.transform.transform.position, Quaternion.identity);
                        currentRedSparks.Play();

                        ClawRotation.instance.DrillRight();

                        Debug.Log("Red hole hit");
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
                        // TODO: tornillo blue quan es fica. CHISPAS
                        currentBlueSparks = Instantiate(sparks, raycastHit.transform.position, Quaternion.identity);
                        currentBlueSparks.Play();

                        ClawRotation.instance.DrillLeft();

                        Debug.Log("Blue hole hit");
                        objectBehaviour.RemoveHole(raycastHit.transform.gameObject);
                        animate = true;
                    }
                }

                objectReady = screwdriverReady = false;
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
