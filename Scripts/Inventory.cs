using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    public int[] inventoryCount;
    public Image[] inventoryImages;
    public string[] inventoryString;
    public Text slot1;
    public Text slot2;
    public Text slot3;
    public Text slot4;
    public Text slot5;
    public Text slot6;
    public Text slot7;
    public Text slot8;
    public Text slot9;
    public Text slot10;
    public Text slot11;
    public Text slot12;
    public Text slot13;
    public Text slot14;
    public Text slot15;
    public Text slot16;
    public Image ironImage;
    public Image waterImage;
    Text[] inventorySlots;
    public Inventory inventory;

    public buttonClick buttonClick
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
        }
    }

    void Start()
    {
        inventoryCount = new int[16];
        inventoryImages = new Image[16];
        inventoryString = new string[16];
        inventorySlots = new Text[16];
        PopulateSlots();

    }

    public void StoreInInventory(int inMine, string inType)
    {
        for (int i = 0; i < inventoryCount.Length; i++)
        {
            if (inventoryCount[i] == 0)
            {
                inventoryCount[i] += inMine;
                inventoryString[i] = inType;
                if (inType == "iron")
                {
                    inventoryImages[i] = Instantiate(ironImage);
                    inventoryImages[i].transform.SetParent(inventory.transform);
                    inventoryImages[i].transform.position = inventorySlots[i].transform.position;
                    inventoryImages[i].transform.position = new Vector2(inventoryImages[i].transform.position.x - 55, inventoryImages[i].transform.position.y + 5);
                }
                else if (inType == "water")
                {
                    inventoryImages[i] = Instantiate(waterImage);
                    inventoryImages[i].transform.SetParent(inventory.transform);
                    inventoryImages[i].transform.position = inventorySlots[i].transform.position;
                    inventoryImages[i].transform.position = new Vector2(inventoryImages[i].transform.position.x - 55, inventoryImages[i].transform.position.y + 5);
                }
                UpdateText(i);
                break;
            }
            else if (inventoryCount[i] != 0 && inType == inventoryString[i] && inventoryCount[i] < 15)
            {
                inventoryCount[i] += inMine;
                UpdateText(i);
                break;
            }
        }
    }

    public void UpdateText(int i)
    {

        if (inventoryCount[i] != 0)
        {
            inventorySlots[i].text = inventoryCount[i].ToString();

        }
        else if (inventoryCount[i] == 0)
        {
            inventorySlots[i].text = "";
        }



    }

    public bool Contains(string inString)
    {
        for (int i = 0; i < inventoryString.Length; i++)
        {
            if (inventoryString[i] == inString)
            {
                return true;
            }
        }
        return false;
    }

    public void TakeItems(string inString, int amount)
    {
        for (int i = 0; i < inventoryString.Length; i++)
        {
            if (inventoryString[i] == inString && inventoryCount[i] != 0)
            {
                inventoryCount[i] -= amount;
                if (inventoryCount[i] < 0)
                {
                    inventoryCount[i] = 0;
                }
                if (inventoryCount[i] == 0)
                {
                    inventoryString[i].Remove(i);
                    inventorySlots[i].text = "";

                    Destroy(inventoryImages[i].gameObject);


                }
                UpdateText(i);
                break;
            }
        }

    }

    void PopulateSlots()
    {
        inventorySlots[0] = slot1;
        inventorySlots[1] = slot2;
        inventorySlots[2] = slot3;
        inventorySlots[3] = slot4;
        inventorySlots[4] = slot5;
        inventorySlots[5] = slot6;
        inventorySlots[6] = slot7;
        inventorySlots[7] = slot8;
        inventorySlots[8] = slot9;
        inventorySlots[9] = slot10;
        inventorySlots[10] = slot11;
        inventorySlots[11] = slot12;
        inventorySlots[12] = slot13;
        inventorySlots[13] = slot14;
        inventorySlots[14] = slot15;
        inventorySlots[15] = slot16;
    }


}
