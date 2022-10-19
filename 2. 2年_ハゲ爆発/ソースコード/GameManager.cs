using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Score score = null;
    [SerializeField] Timer timer = null;
    [SerializeField] GameObject ModelRoot;
    [SerializeField] GameObject HageOriginal;
    [SerializeField] GameObject Hage;

    GameObject hageClone;
    private Vector3 hagePos = Vector3.zero;

    // �����Ǘ��t���O
    public bool isGenerate = false;

    // Start is called before the first frame update
    void Awake()
    {
        hagePos = HageOriginal.transform.position;
                            
        // �X�R�A������
        Score._hageScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HageCreate();
    }

    /// <summary>
    /// �n�Q�����֐�
    /// </summary>
    void HageCreate()
    {
        timer.TimerCount();

        if (isGenerate && !timer.TimerCount())
        {
            if (HageOriginal.active)
            {
                HageOriginal.SetActive(false);
            }

            score.HageScoreManager();


            hageClone = Instantiate(Hage, hagePos, Quaternion.identity);
            hageClone.transform.parent = ModelRoot.transform;


            Destroy(hageClone, 1);

            isGenerate = false;
        }
        else if (timer.TimerCount())
        {
            // �Q�[���̏I����̏���
            SceneManager.LoadScene("ResultScene");
        }
    }
}