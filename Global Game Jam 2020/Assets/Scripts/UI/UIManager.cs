using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public ObjectController objectController;
    public GameController gameController;
    public Text text;

    void Update()
    {
        text.text = Mathf.CeilToInt(objectController.GameplayTimer).ToString();
    }

    public void Play()
    {
        menu.SetActive(false);
        gameController.gameState = GameController.GameState.play;
    }

    public void Howtoplay()
    {

    }

    public void Exit()
    {

    }

    public void Credits()
    {

    }
}
