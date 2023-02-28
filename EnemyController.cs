using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public BaseStatus eneste;

    NavMeshAgent agent;

    float atkrate;
    private float atkinterval;
    public bool enemyinrange;

    public GameObject bullets;

    public GameObject barrel;

    public Transform[] PatrolPos;
    [SerializeField] int destPoint = 0; //巡回ルートの数

    public string state;

    public GameObject nowtarget;

    void Start()
    {
        atkrate = eneste.atkrate;
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
        state = "patrol";
        StartCoroutine("AnimatedRotate");

        if (Score.stagestate < 3)
        {
            agent.speed = eneste.mpvespeed;
        }
        else if (Score.stagestate > 2)
        {
            agent.speed = (eneste.mpvespeed / 2);
        }

    }
    void Update()
    {
        if (atkinterval <= atkrate) atkinterval += Time.deltaTime;


        if (state == "patrol")
        {
            Patrol();
        }
        else if (state == "chase")
        {
            Chase();
            if (atkinterval > atkrate)
            {
                if (enemyinrange)
                {
                    OpenFire();
                    atkinterval = 0;
                    enemyinrange = false;
                }
            }
        }

        if (state == "chase" && nowtarget == null) { state = "patrol"; }

    }
    void GotoNextPoint()
    {
        // 地点がなにも設定されていないときに返します
        if (PatrolPos.Length == 0)
            return;

        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = PatrolPos[destPoint].position;
        StartCoroutine(PatrolWait());

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        destPoint = (destPoint + 1) % PatrolPos.Length;
    }
    IEnumerator PatrolWait()
    {
        agent.speed = 0;
        yield return new WaitForSeconds(3f);
        agent.speed = eneste.mpvespeed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ally")
        {
            StopCoroutine("AnimatedRotate");
            agent.enabled = true;
            nowtarget = other.gameObject;
            state = "chase";
            Vector3 targetpos = other.transform.position;
            targetpos.y = transform.position.y;
            transform.LookAt(targetpos);
            enemyinrange = true;
            agent.destination = other.transform.position;
        }
    }
    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 1)
        {
            GotoNextPoint();
        }
        //視界内に自軍が入ったらChaseへ
        //他の敵の視界に自軍が入ったらChaseへ
    }
    void Chase()
    {
        agent.destination = nowtarget.transform.position;
        //常に敵の位置を取得し、一定距離まで近づくと停止。その後一定間隔で射撃
        //彼我の射線が消えると、
        if (agent.remainingDistance <= 3 && nowtarget)
        {
            agent.speed = 0;
        }
        else { agent.speed = 1.5f; }

        if (agent.remainingDistance >= 3.5)
        {
            nowtarget = null;
            state = "patrol";
        }
    }

    public void Search()
    {
        if (state == "patrol") StartCoroutine("AnimatedRotate");
    }

    IEnumerator AnimatedRotate()
    {
        float totalrotate = 0;
        float middlerotate = 0;
        while (totalrotate <= 360)
        {
            while (middlerotate <= 90)
            {
                transform.Rotate(new Vector3(0, 5, 0));
                yield return null;
                totalrotate += 5;
                middlerotate += 5;
            }
            yield return new WaitForSeconds(0.3f);
            middlerotate = 0;
        }
        /*for (int i = 0; i < 10; i++)
        {
            transform.Rotate(new Vector3(0, 18, 0));
            yield return null;
        }*/
    }

    public void OpenFire()
    {
        Instantiate(bullets, barrel.transform.position, barrel.transform.rotation);
    }
}
