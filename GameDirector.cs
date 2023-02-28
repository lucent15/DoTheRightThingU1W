using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameDirector : MonoBehaviour
{
    //次やること
    //次のシーンへ行く　シーンマネージャ用意
    //シーンリロード

    //マップマス目

    //チュートリアルステージとステージ１を作る。構成は決まってる？

    //別シーンに値を移すための奴

    //ステージごとにミッション開始中のテキスト。
    //チュトリ：操作説明。１～最後：ミッション内容。

    public int maxenemy;//シーンの敵最大数
    public int maxenesol;//シーンの敵兵最大数
    public int maxeneciv;//シーンの民間人最大数
    //最大数がゼロになったらゲームクリア。

    public int killenemy;//殺した敵兵の数
    public int killciv;//殺した民間人の数

    public Text infowindowtext;

    public Text killcountgui;
    private int totalkillcount;

    FadeInOutScript fade;
    private bool oncetrigger = false;

    private string result = null;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField] public float mastervol;
    public Slider sliderofmas;

    private Text gametitle;
    private bool endtitle;
    private bool once = false;

    void Start()
    {
        fade = GameObject.Find("Fader").GetComponent<FadeInOutScript>();
        // infowindowtext.text = "どんぱち";
        totalkillcount = Score.totalenemykillcount + Score.totalcivkillcount;
        Debug.Log("今のステート：" + Score.stagestate);
        sliderofmas.value = Score.volume;
        if (Score.stagestate == 0) gametitle = GameObject.Find("GameTitle").GetComponent<Text>(); gametitle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        killcountgui.text = "x" + totalkillcount;
        if (maxenemy == 0 && !oncetrigger)
        {
            StageClear(); oncetrigger = true;
        }

        if (fade.Getendfade() && result == "lose")
        {
            if (Score.stagestate == 0) LoadMission1();
            else if (Score.stagestate == 1) LoadMission1();
            else if (Score.stagestate == 2) LoadMission2();
            else if (Score.stagestate == 3) LoadMission3();
            else if (Score.stagestate == 4) LoadMission4();
            else if (Score.stagestate == 5) LoadMission5();
        }

        if (fade.Getendfade() && result == "win")//フェードアウトが終わり、結果がかった時
        {
            if (Score.stagestate == 0)
            {
                if (gametitle != null && !once)
                {
                    gametitle.enabled = true;
                    StartCoroutine(FadeInText(gametitle));
                    once = true;
                }

                if (endtitle)
                {
                    Score.stagestate = 1;
                    LoadMission1();
                }
            }
            else if (Score.stagestate == 1)
            {
                LoadMission2();
                Score.stagestate = 2;
            }
            else if (Score.stagestate == 2)
            {
                LoadMission3();
                Score.stagestate = 3;
            }
            else if (Score.stagestate == 3)
            {
                LoadMission4();
                Score.stagestate = 4;
            }
            else if (Score.stagestate == 4)
            {
                LoadMission5();
                Score.stagestate = 5;
            }
            else if (Score.stagestate == 5)
            {
                //クリアシーン。
                LoadClearScene();
                Score.stagestate = 6;
            }
        }
    }

    public void CountMaxEnemy()//スターと時に使用。敵の最大数カウント
    {
        maxenemy++;
        maxenesol++;
    }
    public void CountMaxCiv()//スタート時使用。市民の最大数カウント
    {
        maxenemy++;
        maxeneciv++;
    }
    public void DeathCountEnemy()//敵兵が死ぬときに総数を1減らし、キルカウント追加
    {
        killenemy++;
        totalkillcount++;
        maxenemy--;
        maxenesol--;
    }
    public void DeathCountCiv()//市民が死ぬとき総数１減らし、キルカウント追加
    {
        killciv++;
        totalkillcount++;
        maxenemy--;
        maxenesol--;
    }
    public void EscapeCountCiv()//市民がエリア離脱した時使用。総数が減る。
    {
        maxenemy--;
        maxeneciv--;
    }
    public void AllyDead()
    {
        //ミッション失敗表示。2秒後フェードアウトからの最初のシーンへ
        infowindowtext.text = "味方がやられた、ミッションは失敗だ。貴官の処分は追って下す、自室にて待機せよ。";
        fade.StartCoroutine("FadeOut");
        result = "lose";
    }

    public void LoadMission1() { SceneManager.LoadScene("GameScene 1"); }
    public void LoadMission2() { SceneManager.LoadScene("GameScene 2"); }
    public void LoadMission3() { SceneManager.LoadScene("GameScene 3"); }
    public void LoadMission4() { SceneManager.LoadScene("GameScene 4"); }
    public void LoadMission5() { SceneManager.LoadScene("GameScene 5"); }
    public void LoadClearScene() { SceneManager.LoadScene("ClearScene"); }


    public void StageClear()
    {
        if (Score.stagestate == 0) infowindowtext.text = "これであなたも指揮官の一人です。これからも正しい指揮を。";
        if (Score.stagestate == 1 || Score.stagestate == 2 || Score.stagestate == 4)
        {
            infowindowtext.text = "エリア内の敵性勢力の殲滅を確認。ミッション完了。";
        }
        if (Score.stagestate == 3 || Score.stagestate == 5)
        {
            infowindowtext.text = "エリア内の敵性勢力の排除を確認。ミッション完了。";
        }
        fade.StartCoroutine("FadeOut");
        result = "win";
        Score.totalcivkillcount += killciv;
        Score.totalenemykillcount += killenemy;
    }

    public void GoToNextStage()
    {
        //スコア保存。別のシーン
        /*Tutorialシーン読みこまれたらステージステートを０へ
         * ステートの値に対応したテキストを値に応じていれる。
         * ステートがxならゲームシーンx+1を呼び出す。一通り処理を終えたらステージステートに１追加。*/
        Score.totalcivkillcount = killciv;
        Score.totalenemykillcount = killenemy;

        if (Score.stagestate == 0)
        {
            SceneManager.LoadScene("GameScene 1");
            //Score.stagestate = 1;
        }
    }

    public void SetMaster(float volume) //すらいだにつけるやつ
    {
        audioMixer.SetFloat("MasterVol", volume);
        mastervol = volume;
        Score.volume = volume;
    }

    IEnumerator FadeInText(Text textbox)
    {
        textbox.enabled = true;


        Color c = textbox.color;
        c.a = 1f;
        textbox.color = c; // 画像の不透明度を1にする
        yield return new WaitForSeconds(3);

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
        yield return new WaitForSeconds(2);
        endtitle = true;
    }
}
