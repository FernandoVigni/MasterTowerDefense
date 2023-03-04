using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetLife(500);
        SetSpeed(5);
        transform.localScale = new Vector3(transform.localScale.z * 1.5f, transform.localScale.y * 2f, transform.localScale.x * 1.5f);
    }
}
