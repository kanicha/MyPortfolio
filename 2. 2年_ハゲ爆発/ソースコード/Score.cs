using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text hageScoreText;

    // スコア
    public static int _hageScore = 0;


    /// <summary>
    /// スコア生成関数
    /// </summary>
    public void HageScoreManager()
    {

        // スコア倍率設定
        if (_hageScore >= 13000)
        {
            _hageScore += 3873;
        }
        else if (_hageScore >= 5000)
        {
            _hageScore += 1358;
        }
        else if (_hageScore >= 1000)
        {
            _hageScore += 500;
        }
        else if (_hageScore >= 100)
        {
            _hageScore += 200;
        }
        else if (_hageScore <= 99)
        {
            _hageScore += 20;
        }

        // スコアを文字表示
        hageScoreText.text = _hageScore.ToString();
    }
}
