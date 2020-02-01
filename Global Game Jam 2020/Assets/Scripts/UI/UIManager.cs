using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menu;
    public ObjectController objectController;
    public Text text;
    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.CeilToInt(objectController.GameplayTimer).ToString();
    }

    public void Play()
    {
        menu.SetActive(false);
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
