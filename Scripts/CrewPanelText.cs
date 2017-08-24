using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrewPanelText : MonoBehaviour {

    public Text name;
    public Text mineSkill;
    public Text hydration;
    public Text health;
    public Button button;

    void Start() {
        name.text = "";
        mineSkill.text = "";
        hydration.text = "";
        health.text = "";
        button.interactable = false;
    }
    public void UpdateText(CrewScript activeCrew) {
        name.text = activeCrew.name;
        mineSkill.text ="Mine Level: " + activeCrew.mineSkill.ToString();
        hydration.text ="H20: " + activeCrew.hydration.ToString() + " / 100";
        health.text =   "Health: " + activeCrew.health.ToString() + " / 100";
    }

    public void DisplayCancelButton() {
        if (!button.interactable)
        {
            button.interactable = true;
        }
    }

    public void HideCancelButton() {
        if (button.interactable)
        {
            button.interactable = false;
        }
    }
}
