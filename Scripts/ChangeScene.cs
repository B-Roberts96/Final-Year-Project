using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public Button button;
    void Awake() {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => { SceneChange(); });
    }

    public void SceneChange() {
        SceneManager.LoadScene(1);
    }
}
