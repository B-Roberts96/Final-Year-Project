using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wallbutton : MonoBehaviour {

    public Button button;
    public GameObject panel;
    public GameObject wallGhost;
    public Inventory inventory;
    public string buttonType;
    public int cost;
    void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => { placeWall(); });
    }

    void placeWall()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        if (!wallGhost.activeSelf && inventory.Contains(buttonType))
        {
            for (int i = 0; i < inventory.inventoryCount.Length; i++)
            {
                if (inventory.inventoryString[i] == buttonType && inventory.inventoryCount[i] > cost)
                {
                    wallGhost.SetActive(true);
                }
            }
        }
       

    }
   
}
