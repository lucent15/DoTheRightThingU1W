using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearSceneScript : MonoBehaviour
{
    // Start is called before the first frame update


    [TextArea]
    public string newstextarea1;
    [TextArea]
    public string newstextarea2;
    [TextArea]
    public string truenewstextarea;
    [TextArea]
    public string introstring;

    Text introtextbox;//ニュース開く描写入れる
    Text newspaper;//ニュース記事表示入れる
    Text cleartitle1;//とぅるーえんど表示入れる
    Text cleartitle2;//ばっどえんど表示入れる
    Text clicktonext;//クリックトゥネクスト入れる

    FadeInOutScript fadersc;
    private int scenestate = 0;

    private bool completefadein;

    void Start()
    {
        newspaper = GameObject.Find("News Text").GetComponent<Text>();
        introtextbox = GameObject.Find("IntroText").GetComponent<Text>();
        clicktonext = GameObject.Find("ClickToNext").GetComponent<Text>();
        fadersc = GameObject.Find("Fader").GetComponent<FadeInOutScript>();
        cleartitle1 = GameObject.Find("End1").GetComponent<Text>();
        cleartitle2 = GameObject.Find("End2").GetComponent<Text>();
        cleartitle1.enabled = false;
        cleartitle2.enabled = false;
        clicktonext.enabled = false;


        if (Score.totalcivkillcount != 0)//一人でも市民が死んでいた場合
        {
            newspaper.text = newstextarea1 + Score.totalenemykillcount +
          "名が死亡しただけなく、戦闘に巻き込まれて村民" + Score.totalcivkillcount + newstextarea2;
        }
        else if (Score.totalcivkillcount == 0)//一人も殺してない場合
        { newspaper.text = truenewstextarea; }

        introtextbox.text = introstring;
        Color c = introtextbox.color; c.a = 0f; introtextbox.color = c;

        StartCoroutine(FadeOutText(introtextbox));//イントロテキスト表示
    }
    private void Update()
    {
        if (scenestate == 1 && clicktonext.enabled == false)//クリックトゥネクストが表示される
        {
            clicktonext.enabled = true;
            StartCoroutine(FadeOutText(clicktonext));
        }
        if (scenestate == 2 && Input.GetMouseButtonDown(0))//クリックするとイントロテキストとクリック案内が消え、ニュース記事のフェードが消える。
        {
            StartCoroutine(FadeInText(clicktonext));
            StartCoroutine(FadeInText(introtextbox));
            fadersc.StartCoroutine("FadeIn");
            scenestate = 3;
        }
        if (scenestate == 3)//ニュース表示
        {
            newspaper.enabled = true;
            StartCoroutine(NewFadeInText(clicktonext, 6f));
            scenestate = 4;
        }
        if (scenestate == 4 && Input.GetMouseButtonDown(0)&&completefadein==true)
        {
            StartCoroutine(FadeInText(clicktonext));
            StartCoroutine(FadeInText(newspaper));
            fadersc.StartCoroutine("NewFadeOut");
            scenestate = 5;
        }
        if (scenestate == 5)
        {
            if (Score.totalcivkillcount == 0)  StartCoroutine(NewFadeInText(cleartitle1, 2));
            if (Score.totalcivkillcount != 0) StartCoroutine(NewFadeInText(cleartitle2, 2));
            scenestate = 6;
        }

        //Debug.Log(scenestate);
    }

    IEnumerator FadeInText(Text textbox)
    {
        textbox.enabled = true;


        Color c = textbox.color;
        c.a = 1f;
        textbox.color = c; // 画像の不透明度を1にする
        yield return null;

        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a -= 0.02f;
            textbox.color = c; // 画像の不透明度を下げる

            if (c.a <= 0f) // 不透明度が0以下のとき
            {
                c.a = 0f;
                textbox.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }
    }

    public IEnumerator FadeOutText(Text textbox)
    {
        yield return new WaitForSeconds(3);

        Color c = textbox.color;
        c.a = 0;
        textbox.color = c; // 画像の不透明度を1にする
        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a += 0.02f;
            textbox.color = c; // 画像の不透明度を下げる

            if (c.a >= 1) // 不透明度が0以下のとき
            {
                c.a = 1;
                textbox.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }
        scenestate++;

    }

    public IEnumerator NewFadeInText(Text textbox, float time)
    {
        yield return new WaitForSeconds(time);
        textbox.enabled = true;
        Color c = textbox.color;
        c.a = 0;
        textbox.color = c; // 画像の不透明度を1にする
        while (true)
        {
            yield return null; // 1フレーム待つ
            c.a += 0.02f;
            textbox.color = c; // 画像の不透明度を下げる

            if (c.a >= 1) // 不透明度が0以下のとき
            {
                c.a = 1;
                textbox.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }
        completefadein = true;
    }
}
