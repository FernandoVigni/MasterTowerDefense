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
        ShineLogo();
    }

    public async Task ShineLogo()
    {
        await Task.Delay(3200);
        SkineEffectLogo.SetActive(true);
        PlaySFXShineLogo();
    }

    public async Task PlaySFXShineLogo()
    {
        await Task.Delay(1000);
        AudioManager.Instance.PlaySFX("ShineLogo");
        AdvicomFadeOut();
    }
    public async Task AdvicomFadeOut()
    {
        await Task.Delay(1000);
        animator.SetBool("AdvicomFadeOut", true);
        OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        SetBoolAdvicomFadeOutFalse();
    }

    public async Task SetBoolAdvicomFadeOutFalse()
    {
        await Task.Delay(3000);
        animator.SetBool("AdvicomFadeOut", false);
        SetBoolBlackBanckgroundFadeOutTrue();
    }

    public async Task SetBoolBlackBanckgroundFadeOutTrue()
    {
        animator.SetBool("BlackBanckgroundFadeOut", true);
        SkineEffectLogo.SetActive(false);
        mainMenuTurnOff.SetActive(false);
        this.gameObject.SetActive(false);
        MainMenu.Instance.EnterInMainMenu();
    }
}
