using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPScript : MonoBehaviour
{
    public BaseStatus myste;
    public BaseStatus alyste;
    public BaseStatus eneste;

    public float myhp;

    EffectGenerator effegene;

    EnemyController enecon;
    CivController civcon;

    AlliesController alicon;

    public Image hpbarmain;
    public Image hpbardelay;
    private float delayhpcam;
    private bool delaystate;

    GameDirector gamdir;

    AudioSource damagesound;

    void Start()
    {
        myhp = myste.hp;
        effegene = GameObject.Find("DEATHICONGENERATOR").GetComponent<EffectGenerator>();
        gamdir = GameObject.Find("GAMEDIRECTOR").GetComponent<GameDirector>();

        damagesound = GetComponent<AudioSource>();

        if (myste.type == "enemy") { enecon = this.GetComponent<EnemyController>(); gamdir.CountMaxEnemy(); }
        if (myste.type == "civ") { civcon = this.GetComponent<CivController>(); gamdir.CountMaxCiv(); }
        if (myste.type == "ally") { delayhpcam = myhp; alicon = this.GetComponent<AlliesController>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (myhp <= 0)
        {
            if (myste.type == "enemy") { gamdir.DeathCountEnemy(); }
            if (myste.type == "civ") { gamdir.DeathCountCiv(); }
            if (myste.type == "ally") { gamdir.AllyDead(); }

            effegene.GenerateDeadIcon(this.transform.position);
            this.gameObject.SetActive(false);
           // Destroy(this.gameObject);
            

        }

        //リジェネ用
        if (myhp < myste.hp) myhp += Time.deltaTime * 5;

        if (myste.type == "ally") ShowHP();
    }

    public void Damage()
    {
        damagesound.Play();
        if (myste.type == "ally")
        {
            delaystate = true;
            myhp -= eneste.atk;
            if (alicon != null) alicon.Search();
        }
        if (myste.type == "enemy" || myste.type == "civ")
        {
            if (enecon != null) enecon.Search();
            if (civcon != null) civcon.Search();
            myhp -= alyste.atk;
        }
    }

    public void ShowHP()
    {
        hpbarmain.fillAmount = (myhp / myste.hp);
        hpbardelay.fillAmount = (delayhpcam / myste.hp);
        if (delaystate) { StartCoroutine("DelayHP"); } else { hpbardelay.fillAmount = (myhp / myste.hp); delayhpcam = myhp; }

    }

    IEnumerator DelayHP()
    {
        yield return new WaitForSeconds(0.5f);
        var delaysum = new WaitForSeconds(0.1f);
        while (hpbarmain.fillAmount <= hpbardelay.fillAmount)
        {
            delayhpcam -= 0.05f;
            yield return delaysum;
        }

        if (hpbarmain.fillAmount >= hpbardelay.fillAmount)
        {
            StopCoroutine("DelayHP");
            delaystate = false;
        }
    }
}
