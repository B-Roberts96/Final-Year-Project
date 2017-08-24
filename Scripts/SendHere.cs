using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SendHere :MonoBehaviour
{

    public GameObject commandPanel;
    public Button button;
    public Camera gameCam;
    public bool clicked;

    void Awake()
    {
        button = GetComponent<Button>();
        clicked = false;
        button.onClick.AddListener(() => { sendHere(); });
    }
    public bool sendHere()
    {

        commandPanel.SetActive(false);
        clicked = true;
        return clicked;
    }
    public void SetClicked(bool inBool)
    {
        clicked = inBool;
    }
}
