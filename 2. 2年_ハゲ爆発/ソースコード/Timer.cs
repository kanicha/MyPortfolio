using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text text;
    private float totaltime = 20f;
    private float _timerCount = 0f;

    public bool TimerCount(bool timerStop = false)
    {
        totaltime -= Time.deltaTime;
        _timerCount = totaltime;

        if (_timerCount <= 0f)
        {
            _timerCount = 0f;
            timerStop = true;
        }
        else
        {
            _timerCount /= 2; 
            text.text  = _timerCount.ToString("N1") ;
        }

        return timerStop;
    }
}
