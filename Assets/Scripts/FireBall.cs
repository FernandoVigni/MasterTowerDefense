using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject Tower;
    public bool translate = false;
    public Vector3 target;
    public int damage;
    public float speed = 5f;

    public void SetTarget(Vector3 target)
    { 
        this.target = target;
    }

    public void LookAt()
    {
        transform.LookAt(target); // mira hacia el objetivo
    }

    public void TranslateOn()
    {
        translate = true;       
    }

    void Update()
    {
        if(translate)
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // se mueve hacia el objetivo
    } 

    public void SetDestination()
    {
        TranslateOn();
        LookAt(); 
    }
}
