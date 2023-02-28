using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInOutScript : MonoBehaviour
{

    [SerializeField] Image img; // まっくらな画像


    Text missionname;
    // ---
    private bool endfade = false;

    void Start()
    {
        img = this.GetComponent<Image>();
        if (Score.stagestate != 6) StartCoroutine("FadeIn"); // フェードインを開始


        missionname = GameObject.Find("MissionName").GetComponent<Text>();
        missionname.enabled = true;


        if (Score.stagestate == 6)
        {
            Color c = img.color;
            c.a = 1;
            img.color = c; // 画像の不透明度を1にする }
        }
    }

    public bool Getendfade() { return endfade; }

    IEnumerator FadeIn()
    {
        Debug.Log("ふぇーど");
        img.gameObject.SetActive(true); // 画像をアクティブにする
        img.raycastTarget = true;
        //Time.timeScale = 0;


        Color c = img.color;
        c.a = 1f;
        img.color = c; // 画像の不透明度を1にする
        yield return new WaitForSeconds(3);
        missionname.enabled = false;

        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a -= 0.02f;
            img.color = c; // 画像の不透明度を下げる

            if (c.a <= 0f) // 不透明度が0以下のとき
            {
                c.a = 0f;
                img.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }

        img.raycastTarget = false;
        Time.timeScale = 1.0f;
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3);
        img.raycastTarget = true;
        Color c = img.color;
        c.a = 0;
        img.color = c; // 画像の不透明度を1にする
        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a += 0.02f;
            img.color = c; // 画像の不透明度を下げる

            if (c.a >= 1) // 不透明度が0以下のとき
            {
                c.a = 1;
                img.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }
        endfade = true;
    }

    public IEnumerator NewFadeOut()
    {
        //  yield return new WaitForSeconds(time);
        yield return null;
        img.raycastTarget = true;
        Color c = img.color;
        c.a = 0;
        img.color = c; // 画像の不透明度を1にする
        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a += 0.02f;
            img.color = c; // 画像の不透明度を下げる

            if (c.a >= 1) // 不透明度が0以下のとき
            {
                c.a = 1;
                img.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }
        endfade = true;
    }
}
