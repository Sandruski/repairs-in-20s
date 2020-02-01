using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ObjectController objectController;
    public Text text;
    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.CeilToInt(objectController.GameplayTimer).ToString();
    }
}
