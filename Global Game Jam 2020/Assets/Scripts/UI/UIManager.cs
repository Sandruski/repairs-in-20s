using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public ObjectController objectController;
    public GameController gameController;
    public Text text;

    public DollyMovement dolly;

    void Update()
    {
       // text.gameObject.SetActive(false);
        text.text = Mathf.CeilToInt(objectController.GameplayTimer).ToString();
    }

    public void Play()
    {
        menu.SetActive(false);
        text.gameObject.SetActive(true);

        dolly.OutofTV();
    }

    public void Howtoplay()
    {
        SceneManager.LoadScene("How To Play", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnClickCreditsButton()
    {
        SceneManager.LoadScene("CreditsScene", LoadSceneMode.Single);
    }
}
