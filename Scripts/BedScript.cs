using UnityEngine;
using System.Collections;

public class Beds : MonoBehaviour {

    public ControlClass control;
    public Vector2 bed1;
    public Vector2 bed2;
    void Start() {
        bed1 = transform.position;
        bed2 = new Vector2(transform.position.x + 1, transform.position.y);
    }
    public void Sleep() {

    }
}
