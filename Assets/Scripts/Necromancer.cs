using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Atack()
    {
        animator.SetBool("Atack", true);
    }

    public void Idle() 
    {
        animator.SetBool("Atack", false);
        animator.SetBool("Idle", true);
    }
}   






