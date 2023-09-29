using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFdeIn : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("fadeIn", true);
    }
}
