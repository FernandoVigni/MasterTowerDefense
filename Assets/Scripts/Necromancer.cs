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

    void Update()
    {

    }

    public async Task Atack()
    {
        animator.SetBool("Atack", true);
        await Task.Delay(6000);
        animator.SetBool("Atack", false);
        animator.SetBool("Idle", true);
    }

    public void Idle() 
    {
        animator.SetBool("Atack", false);
        animator.SetBool("Idle", true);
    }
}   






