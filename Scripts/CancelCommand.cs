using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CancelCommand : MonoBehaviour
{
    public GameObject commandPanel;
    public Button button;


    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => { closePanel(); });
    }
    void closePanel()
    {
        if (commandPanel.activeSelf)
        {
            commandPanel.SetActive(false);

        }
    }
}
