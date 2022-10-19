using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HageController : MonoBehaviour
{
    [SerializeField] GameObject ExplosionEffects;

    private GameObject gameManager;
    private GameManager gm = null;
    private Timer timer;
    private Vector3 explosionPos = Vector3.zero;
    GameObject _exprosion;

    // Update is called once per frame
    void Update()
    {
        HageDestroy();
    }

    private void Start()
    {
        // サーチして呼び出し
        gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        timer = gameManager.GetComponent<Timer>();
    }

    private void HageDestroy()
    {
        // ハゲの座標と爆発effect発生ポジション同期
        explosionPos = this.transform.position;
        explosionPos.y += 1;

        // エンターキーを押されたら
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gm.isGenerate = true;
            // 爆発!!!!!!!!!!!
            if (!timer.TimerCount())
            {
                _exprosion = Instantiate(ExplosionEffects, explosionPos, Quaternion.identity);
                SoundManager.Instance.PlaySE(0);
            }
            
            Destroy(_exprosion, 1);
        }
    }
}
