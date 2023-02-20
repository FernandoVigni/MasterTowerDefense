using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public bool translate = false;
    public int damage;
    public float speed = 5f;

    public void AbleToTranslate()
    {
        translate = true;       
    }

    public void LookAt(Vector3 objetive)
    {
        transform.LookAt(objetive); // mira hacia el objetivo
    }

    // Todo agregar .Init metodo que se le pasa como parametro la direccion y la velocidad.

    void Update()
    {
        if(translate)
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // se mueve hacia el objetivo
    } 
}
