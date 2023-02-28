using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    private float bulletforce;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        if (Score.stagestate > 2) { this.transform.localScale = new Vector3(0.5f,0.5f,0.5f);bulletforce = 2.5f; }
        else if (Score.stagestate<3)
        {
            bulletforce = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * bulletforce;

        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Field")
        {
            Destroy(this.gameObject);
        }
            other.gameObject.GetComponent<HPScript>().Damage();
            Destroy(this.gameObject);
        
        

    }
}
