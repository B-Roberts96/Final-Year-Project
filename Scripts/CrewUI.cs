using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CrewUI : MonoBehaviour
{
    public ControlClass control;
    public GameObject crewPanel;

    List<CrewScript> crew;
    public GameObject crewPanelTrans;
    List<CrewText> crewTexts;
    public CrewText startCrewText;
    public Camera gameCam;
    bool firstTime = true;

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

    public void Start()
    {

        crew = new List<CrewScript>();
        crew = control.GetCrew();
        crewTexts = new List<CrewText>();
        crewTexts.Add(startCrewText);
        if (!firstTime)
        {
            for (int i = 1; i < 3; i++)
            {

                crewTexts.Add((CrewText)Instantiate(startCrewText, new Vector3(startCrewText.transform.position.x, startCrewText.transform.position.y + 15 * i), Quaternion.identity));
                crewTexts[i].transform.parent = crewPanel.transform;


            }

            UpdatePanel();
        }
        firstTime = false;

    }

    public void UpdatePanel()
    {
        for (int i = 0; i < crewTexts.Count; i++)
        {
            crewTexts[i].name.text = crew[i].name;
            if (crew[i].actions.Count != 0)
            {
                crewTexts[i].job.text = crew[i].actions[0];

                int timerHold = (11 - crew[i].mineSkill) - (int)crew[i].timestep;
                if (crewTexts[i].job.text != "moving")
                {
                    crewTexts[i].timeRemaining.text = timerHold.ToString() + " seconds";
                }
            }
            else
            {
                crewTexts[i].job.text = "idle";
                crewTexts[i].timeRemaining.text = "";
            }
        }

    }


}
