using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class CivController : MonoBehaviour
{
    public BaseStatus civste;
    NavMeshAgent agent;

    public Transform escapepoint;
    public string state;

    GameDirector gamdir;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "idle";
        gamdir = GameObject.Find("GAMEDIRECTOR").GetComponent<GameDirector>();
        escapepoint = GameObject.Find("EscapePoint").transform;
        if (Score.stagestate < 3)
        {
            agent.speed = civste.mpvespeed;
        }
        else if (Score.stagestate > 2)
        {
            agent.speed = (civste.mpvespeed / 2);
        }
        //脱出ポイント自動取得
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "idle")
        {
            //Idle();
        }
        if (state == "escape")
        {
            Escape();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ally")
        {
            state = "escape";
        }
        if (other.tag == "escape") { Debug.Log("だっしゅつ"); StartCoroutine(EscapefromKillingField()); }

    }

    public void Search()
    {
        if (state != "escape") StartCoroutine("AnimatedRotate");
    }

    IEnumerator AnimatedRotate()
    {
        float totalrotate = 0;
        while (totalrotate <= 180)
        {
            transform.Rotate(new Vector3(0, 10, 0));
            yield return null;
            totalrotate += 10;
        }
    }
    void Escape()
    {
        agent.destination = escapepoint.transform.position;
        /*if (agent.remainingDistance < 0.2f)
        {
            StartCoroutine(EscapefromKillingField());
        }*/
    }
    IEnumerator EscapefromKillingField()
    {
        yield return new WaitForSeconds(5);
        //脱出カウント追加
        gamdir.EscapeCountCiv();
        Destroy(this.gameObject);
    }


}
