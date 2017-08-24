using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonClick : MonoBehaviour
{
    public GameObject panel;
    public Button button;
    public bool clicked;
    void Start()
    {
        clicked = false;
    }
    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => { displayPanel(); });
    }

    void displayPanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            if (Input.GetMouseButtonUp(0))
            {
                clicked = true;
            }
        }
        else if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    public void panelInit()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }

}
