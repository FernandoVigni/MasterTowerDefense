using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AdvicomIntro : MonoBehaviour
{
    public Animator animator;
    public GameObject SkineEffectLogo;
    public GameObject mainMenuTurnOff;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("AdvicomFadeIn", true);
        SkineEffectLogo.SetActive(false);
        mainMenuTurnOff.SetActive(false);
        AdvicomFadeOut();
    }

    public void ShineLogo() 
    {
        SkineEffectLogo.SetActive(true);
    }

    public async Task AdvicomFadeOut() 
    {
        await Task.Delay(3200);
        ShineLogo();
        await Task.Delay(1000);
        AudioManager.Instance.PlaySFX("ShineLogo");
        await Task.Delay(1000);
        animator.SetBool("AdvicomFadeOut", true);
        OpenMainMenu();
    }

    public async Task OpenMainMenu()
    {
        await Task.Delay(3000);
        animator.SetBool("AdvicomFadeOut", false);
        MainMenu.Instance.EnterInMainMenu();
        animator.SetBool("BlackBanckgroundFadeOut", true);
        this.gameObject.SetActive(false);
    }

}
