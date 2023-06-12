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
        animator.SetBool("AdvicomFadeIn", true);
        AdvicomFadeOut();

    }

    public async Task AdvicomFadeOut() 
    {
        await Task.Delay(8000);
        animator.SetBool("AdvicomFadeOut", true);
        OpenMainMenu();
    }

    public async Task OpenMainMenu()
    {
        await Task.Delay(3000);
        animator.SetBool("AdvicomFadeOut", false);
        MainMenu.Instance.EnterInMainMenu();
        animator.SetBool("BlackBanckgroundFadeOut", true);
        await Task.Delay(3000);
        this.gameObject.SetActive(false);
    }

}
