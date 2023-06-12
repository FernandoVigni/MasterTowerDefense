using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AdvicomIntro : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // MainMenu.Instance.FirstEnterInMainMenu();
        //EnterMainMenu();
        AdvicomFadeOut();
    }
    public async Task EnterMainMenu()
    {
        await Task.Delay(7000);
        MainMenu.Instance.FirstEnterInMainMenu();
    }

    public async Task AdvicomFadeOut() 
    {
        await Task.Delay(4000);
        animator.SetBool("AdvicomFadeOut", true);
    }

}
