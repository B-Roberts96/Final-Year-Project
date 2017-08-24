using UnityEngine;
using System.Collections;

public class SmokeProducer : MonoBehaviour {

    public ControlClass control;
    public Timer timer;
    Timer m_timer;
    float timestep;
    void Start() {
        m_timer = Instantiate(timer);
        m_timer.Start();
    }
    void Update() {

        timestep += m_timer.GetElapsedSeconds();
        if(timestep >= 10 && control.power >= 10)
        {
            control.globalTerraformingProgression++;
            timestep = 0;
            control.power -= 10;
        }

    }

}
