using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject commandPanel;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            mainCamera.transform.position += new Vector3(0, 0.5f, 0);
            if (commandPanel.activeSelf)
            {
                commandPanel.SetActive(false);
            }

        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            mainCamera.transform.position += new Vector3(0, -0.5f, 0);
            if (commandPanel.activeSelf)
            {
                commandPanel.SetActive(false);
            }

        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            mainCamera.transform.position += new Vector3(-0.5f, 0, 0);
            if (commandPanel.activeSelf)
            {
                commandPanel.SetActive(false);
            }

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            mainCamera.transform.position += new Vector3(0.5f, 0, 0);
            if (commandPanel.activeSelf)
            {
                commandPanel.SetActive(false);
            }

        }    
      else  if(Input.GetKey(KeyCode.R) && mainCamera.transform.position.z < -10 || Input.GetKey(KeyCode.KeypadPlus) && mainCamera.transform.position.z < -10)
        {
            mainCamera.transform.position += new Vector3(0, 0, 0.5f);
            if (commandPanel.activeSelf)
            {
                commandPanel.SetActive(false);
            }
        }
        else if (Input.GetKey(KeyCode.F) && mainCamera.transform.position.z > -30 || Input.GetKey(KeyCode.KeypadMinus) && mainCamera.transform.position.z > -30)
        {
            mainCamera.transform.position += new Vector3(0, 0, -0.5f);
            if (commandPanel.activeSelf)
            {
                commandPanel.SetActive(false);
            }
        }
    }
}
