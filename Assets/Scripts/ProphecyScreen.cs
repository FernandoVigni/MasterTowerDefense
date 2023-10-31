using System.Threading;
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
    [SerializeField] public GameObject prophecyScreen;
    public bool isClickedStart;

    public Animator animator;
    private CancellationTokenSource cancellationTokenSource;


    private void Start()
    {
        animator = GetComponent<Animator>();
        LetsGoClickAnimation.SetActive(false);
        SparksSystem.SetActive(false);
        MainMenu.Instance.optionsButton.SetActive(false);
        MainMenu.Instance.goldStatusBox.SetActive(false);
        // Crea un token de cancelación que estará vinculado al ciclo de vida de este objeto.
        cancellationTokenSource = new CancellationTokenSource();
    }

    public void SetAllProphecyScreenOn() 
    {
        textOneVisible.SetActive(true);
        textTwoVisible.SetActive(true);
        textThreeVisible.SetActive(true);
        letsGoButtonVisible.SetActive(true);
    }

    public void ResetAnimationTexts() 
    {
        animator.SetBool("AppearTextOne", false);
        animator.SetBool("AppearTextTwo", false);
        animator.SetBool("AppearTextThree", false);
        animator.SetBool("AppearLetsGoButton", false);
    }

    public async Task PlayTextOne(CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        animator.SetBool("AppearTextOne", true);
        await Task.Delay(2500, cancellationToken);
        textOneVisible.SetActive(true);
        animator.SetBool("AppearTextOne", false);
        await PlayTextTwo(cancellationToken);
    }

    public async Task PlayTextTwo(CancellationToken cancellationToken)
    {
        animator.SetBool("AppearTextTwo", true);
        await Task.Delay(1500, cancellationToken);
        await Task.Delay(1500, cancellationToken);
        textTwoVisible.SetActive(true);
        animator.SetBool("AppearTextTwo", false);
        await PlayTextThree(cancellationToken);
    }

    public async Task PlayTextThree(CancellationToken cancellationToken)
    {
        animator.SetBool("AppearTextThree", true);
        await Task.Delay(2500, cancellationToken);
        textThreeVisible.SetActive(true);
        animator.SetBool("AppearTextThree", false);
        TurnOnLetsGoButton(cancellationToken);
        goldStatus.SetIsFirstProphecyTrue();
    }

    public async Task TurnOnLetsGoButton(CancellationToken cancellationToken)
    {
        animator.SetBool("AppearLetsGoButton", true);
        await Task.Delay(1000, cancellationToken);
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

    public async void StartGame()
    {
        if (!isClickedStart)
        {
            isClickedStart = true;
            try
            {
                await ManageSounds(cancellationTokenSource.Token);
            }
            catch (TaskCanceledException){}
        }
    }

    public async Task ManageSounds(CancellationToken cancellationToken)
    {
        AudioManager.Instance.PlaySFX("ProphecyScreenButton");
        AudioManager.Instance.StopMusic();
        await Task.Delay(2500, cancellationToken);
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
