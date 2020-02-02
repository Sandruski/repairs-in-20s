using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    InputManager input;
    private void Start()
    {
        input = GetComponent<InputManager>();
    }
    private void Update()
    {
        if (input.GetButtonDown(InputManager.Gamepads.Gamepad_1, InputManager.Buttons.B))
        {
            OnClickBackButton();
        }
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
