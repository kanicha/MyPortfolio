using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool startButton = false;

    public bool OnClickStart(bool startButton = false)
    {
        startButton = true;

        this.gameObject.SetActive(false);

        return startButton;
    }
}
