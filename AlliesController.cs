using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AlliesController : MonoBehaviour
{

    public BaseStatus alyste;

    NavMeshAgent agent;


    float atkrate;
    private float atkinterval;
    public bool orderatk;
    public bool enemyinrange;

    public GameObject tespos;

    public GameObject attackarea;
    Collider atkcol;

    public GameObject bullets;


    public GameObject barrel;

    private bool tesm;
    
    private Vector3 mouse;

    private Vector3 target;

    MenuDirector mendir;


    void Start()
    {
        atkrate = alyste.atkrate;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = tespos.transform.position;
        atkcol = attackarea.GetComponent<BoxCollider>();

        orderatk = false;
        atkinterval = 0;


        tesm = false;

        mendir = GameObject.Find("MenuDirector").GetComponent<MenuDirector>();

        if (Score.stagestate < 3) { agent.speed = alyste.mpvespeed; }
        else if (Score.stagestate > 2)
        {
            agent.speed = (alyste.mpvespeed/2);
        }
    }

    void Update()
    {
       // agent.destination = tespos.transform.position;


        if (atkinterval <= atkrate) atkinterval+=Time.deltaTime;

        if (atkinterval > atkrate)
        {
            if (orderatk && enemyinrange) OpenFire(); atkinterval = 0;enemyinrange = false;
        }

        if (tesm)
        {
            mouse = Input.mousePosition;
            mendir.SlowMoOn();
            target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x,mouse.y, 4));
            target.y = tespos.transform.position.y;
            tespos.transform.position = target;
            
                }

        if (tesm&&Input.GetMouseButtonDown(0))
        {
            tesm = false;
            mendir.SlowMoOff();
            agent.destination = tespos.transform.position;
        }

        if (agent.remainingDistance<0.5f&&!tesm) { tespos.SetActive(false); } else { tespos.SetActive(true); }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Vector3 targetpos = other.transform.position;
            targetpos.y = transform.position.y;
            transform.LookAt(targetpos);
            enemyinrange = true;
        }
    }

    public void OpenFire()
    {
        Instantiate(bullets, barrel.transform.position, barrel.transform.rotation);
    }

    public void ShootThisMotherfucker(bool order)
    {
        orderatk = order;
    }

    public void Search()
    {
        if (!enemyinrange) { StartCoroutine("AnimatedRotate"); }
    }

    public void DecideDestination()
    {
        tesm = true;
        agent.destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            yield return new WaitForSeconds(0.1f);
            middlerotate = 0;
        }
    }
}
