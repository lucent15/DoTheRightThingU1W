using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_Overlay : MonoBehaviour
{
    private Transform targetTfm;

    private RectTransform myRectTfm;
    [Header("オフセット値")]
    public float osx;
    public float osz;
    //private Vector3 offset = new Vector3(osx, osy, osz);

    [Header("味方")]
    public Transform[] allies=new Transform[0];

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        myRectTfm.position
            = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + new Vector3(osx,-10,osz));
    }

    public void HighLightAllies(int i)
    {
        targetTfm = allies[i];
        this.gameObject.SetActive(true);
    }

    public void HighLightAlliesOff()
    {
        this.gameObject.SetActive(false);
    }
}
