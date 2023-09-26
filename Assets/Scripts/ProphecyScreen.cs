using System.Threading.Tasks;
using UnityEngine;

public class ProphecyScreen : MonoBehaviour
{
    [SerializeField] public GameObject textOne;
    [SerializeField] public GameObject textTwo;
    [SerializeField] public GameObject textThree;
    [SerializeField] public GameObject textOneVisible;
    [SerializeField] public GameObject textTwoVisible;
    [SerializeField] public GameObject textThreeVisible;
    [SerializeField] public GameObject letsGoButton;
    [SerializeField] public GameObject letsGoButtonVisible;
    [SerializeField] public GameObject SparksSystem;
    [SerializeField] public GoldStatus goldStatus;
    [SerializeField] public Tower tower;
    [SerializeField] private GameObject LetsGoClickAnimation;

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        textOneVisible.SetActive(false);
        textTwoVisible.SetActive(false);
        textThreeVisible.SetActive(false);
        letsGoButtonVisible.SetActive(false);
        SparksSystem.SetActive(false);
        LetsGoClickAnimation.SetActive(false);
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
    }

    public void StartProphecyScene()
    {
        PlayTextOne();
    }

    public async Task PlayTextOne()
    {
        await Task.Delay(1500);
        animator.SetBool("AppearTextOne", true);
        await Task.Delay(2500);
        textOneVisible.SetActive(true);
        animator.SetBool("AppearTextOne", false);
        await PlayTextTwo();
    }

    public async Task PlayTextTwo()
    {
        animator.SetBool("AppearTextTwo", true);
        await Task.Delay(1500);

        await Task.Delay(1500);
        textTwoVisible.SetActive(true);
        animator.SetBool("AppearTextTwo", false);
        await PlayTextThree();
    }

    public async Task PlayTextThree()
    {
        animator.SetBool("AppearTextThree", true);
        await Task.Delay(2500);
        textThreeVisible.SetActive(true);
        animator.SetBool("AppearTextThree", false);
        TurnOnLetsGoButton();
    }

    public async Task TurnOnLetsGoButton()
    {
        animator.SetBool("AppearLetsGoButton", true);
        await Task.Delay(1000);
        letsGoButtonVisible.SetActive(true);
        SparksSystem.SetActive(true);
        animator.SetBool("AppearLetsGoButton", false);
    }

    public void SetInvisibleTexts()
    {
        textOneVisible.SetActive(false);
        textTwoVisible.SetActive(false);
        textThreeVisible.SetActive(false);
    }

    public void StartGame()
    {
        ManageSounds();
    }

    public async Task ManageSounds() 
    {
        AudioManager.Instance.PlaySFX("ProphecyScreenButton");
        AudioManager.Instance.StopMusic();
        await Task.Delay(2500);
        ActivateStartFunctions();
    }

    public void ActivateStartFunctions() 
    {
        SetInvisibleTexts();
        SparksSystem.SetActive(false);
        letsGoButtonVisible.SetActive(false);
        MainMenu.Instance.game.SetActive(true);
        PhaseManager.instance.blueDragon.SetActive(false);
        MainMenu.Instance.prophecyScreen.gameObject.SetActive(false);
        PhaseManager.instance.SetCurrentPhase0();
        PhaseManager.instance.TurnOnRocksToDestroy();
        PhaseManager.instance.StartPhase();
        ValidatePowerUpActive();
    }

    public void ValidatePowerUpActive()
    {
        if (goldStatus.GetPowerUpValue() == 1) 
        {
            tower.ActivatePowerUp();
        }
    }

    public void TurnOnLetsGoAnimationOnCLick() 
    {
        LetsGoClickAnimation.SetActive(true);
    }

    public void TurnOffLetsGoAnimationOnCLick()
    {
        LetsGoClickAnimation.SetActive(false);
    }
}
