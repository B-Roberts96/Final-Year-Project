using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Walls : MonoBehaviour {
    List<Vector3> wallPos;
    void Start()
    {
        wallPos = new List<Vector3>();
    }

 
	public void AddToList(Vector3 inWallPos)
    {
        wallPos.Add(inWallPos);
    }
}
