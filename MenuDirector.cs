using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDirector : MonoBehaviour
{

    public GameObject[] CommandMenu = new GameObject[0];

    [SerializeField]
    public bool slowmo;
    [SerializeField]
    public float slowtime;
    private bool slownow;

    Image NowSlow;


    void Start()
    {
        slowmo = false;
        NowSlow = GameObject.Find("NowSlow").GetComponent<Image>();
    }
    void Update()
    {
        if (slowmo)
        {
            Time.timeScale = slowtime;
            NowSlow.enabled = true;
        }
        else
        {
           Time.timeScale = 1;
            NowSlow.enabled = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Time.timeScale == 1) slowmo = true;
            if (Time.timeScale == slowtime) slowmo = false;
        }
    }
    public void ActivateCommandMenu_A(bool onoff)
    {
        CommandMenu[0].SetActive(onoff);
        slowmo = onoff;
    }

    public void SlowMoOn() { slowmo = true; }
    public void SlowMoOff() { slowmo = false; }
    


}
