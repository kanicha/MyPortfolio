using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        SceneChanger();
    }

    void SceneChanger()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        SceneManager.LoadScene("GameScene");
    }
}
