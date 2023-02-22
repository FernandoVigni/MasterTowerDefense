using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public EnemyManager enemyManager;
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

    public Vector3 resetPosition = new Vector3(-40f, 0f, -40f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Entro a la colision con el enemigo");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyManager.DealDamage(enemy, damage);
                //Debe transportarse a la zona de reposo,
                // debe quedar inactiva o quieta
            }
        }
        transform.position = resetPosition;
    }

    /*
    ver el tema de la FireBall, decidir los diferentes efectos que seran las tarjetas de mejora 
(Offensive: Speed, Damage, AreaOfExplosion, salpicaduras, quemadura, )
(Defensive: Armor(reduce % physical damage),MagicArmor(reduce % magic Damage), life, circleOfBrea(activable), )
(Income: IncomePerWave, IncomeXn )

    */
}
