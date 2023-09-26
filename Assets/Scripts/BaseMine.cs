using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMine : MonoBehaviour
{
    public ExplosiveMine explosive;

    private void OnCollisionEnter(Collision collision)
    {
        BaseMine baseMineScript = collision.gameObject.GetComponent<BaseMine>();

        if (baseMineScript != null)
        {
            explosive.Explode();
        }
    }
}