using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] Text ScoreText;

    void Start()
    {
        ResultScorePlaySE();
    }

    // Update is called once per frame
    void Update()
    {
        ResultScore();
        SceneChanger();
    }

    void ResultScorePlaySE()
    {
        if (Score._hageScore >= 500000)
        {
            SoundManager.Instance.PlaySE(6);
        }
        else if (Score._hageScore >= 150000)
        {
            SoundManager.Instance.PlaySE(1);
        }
        else if (Score._hageScore >= 100000)
        {
            SoundManager.Instance.PlaySE(2);
        }
        else if (Score._hageScore >= 50000)
        {
            SoundManager.Instance.PlaySE(3);
        }
        else
        {
            SoundManager.Instance.PlaySE(4);
        }
    }

    void ResultScore()
    {
        ScoreText.text = Score._hageScore.ToString();
    }

    void SceneChanger()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("TitleScene");
    }
}