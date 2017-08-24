using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mineUIText : MonoBehaviour
{

    public Text text;
    private int mine;
    public void textChange(int mineValue)
    {
        text.text = mineValue.ToString();
        mine = mineValue;
    }
    public int getMine()
    {
        int holdMine = mine;
        return holdMine;
    }
}