using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGameScene", 5.0f);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }
}
