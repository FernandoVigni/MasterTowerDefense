using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    private Animator animator;

    public Warrior warrior1;
    public Warrior warrior2;
    public Warrior warrior3;
    public Warrior warrior4;
    public Warrior warrior5;
    public Warrior warrior6;
    public Warrior warrior7;


    public Mage mage1;
    public Mage mage2;
    public Mage mage3;
    public Mage mage4;

    public Giant giant1;
    public Giant giant2;

    //--------------------------------------

    public Warrior warriorLvlUp1;
    public Warrior warriorLvlUp2;
    public Warrior warriorLvlUp3;
    public Warrior warriorLvlUp4;
    public Warrior warriorLvlUp5;
    public Warrior warriorLvlUp6;
    public Warrior warriorLvlUp7;

    public Mage mageLvlUp1;
    public Mage mageLvlUp2;
    public Mage mageLvlUp3;
    public Mage mageLvlUp4;

    public Giant giantLvlUp1;
    public Giant giantLvlUp2;

    void Start()
    {
        animator = GetComponent<Animator>();
        AllIdle();
    }

    public void Idle(Enemy enemy) 
    {
        enemy.animator.SetBool("Atack 0", false);
        enemy.animator.SetBool("Idle", true);
        enemy.animator.SetBool("Run", false);
    }

    public void AllIdle() 
    {
        Idle(warrior1);
        Idle(warrior2);
        Idle(warrior3);
        Idle(warrior4);
        Idle(warrior5);
        Idle(warrior6);
        Idle(warrior7);

        Idle(mage1);
        Idle(mage2);
        Idle(mage3);
        Idle(mage4);

        Idle(giant1);
        Idle(giant2);

        Idle(warriorLvlUp1);
        Idle(warriorLvlUp2);
        Idle(warriorLvlUp3);
        Idle(warriorLvlUp4);
        Idle(warriorLvlUp5);
        Idle(warriorLvlUp6);
        Idle(warriorLvlUp7);

        Idle(mageLvlUp1);
        Idle(mageLvlUp2);
        Idle(mageLvlUp3);
        Idle(mageLvlUp4);

        Idle(giantLvlUp1);
        Idle(giantLvlUp2);
    }
}   






