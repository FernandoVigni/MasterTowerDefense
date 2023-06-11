using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

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
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        textOneVisible.SetActive(false);
        textTwoVisible.SetActive(false);
        textThreeVisible.SetActive(false);
        letsGoButtonVisible.SetActive(false);
    }

    public void StartProphecyScene()
    {
        PlayTextOne();
    }
    
    public async Task PlayTextOne()
    {
        animator.SetBool("AppearTextOne", true);
        await Task.Delay(3500);
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
        await Task.Delay(3500);
        letsGoButtonVisible.SetActive(true);
        animator.SetBool("AppearLetsGoButton", false);

    }

    public void StartGame() 
    {
        textOneVisible.SetActive(false); 
        textTwoVisible.SetActive(false); 
        textThreeVisible.SetActive(false);
        letsGoButtonVisible.SetActive(false);
        MainMenu.Instance.game.SetActive(true);
        MainCamera.instance.SetCameraLookingToPortalOne();
        MainMenu.Instance.prophecyScreen.gameObject.SetActive(false);
        
        PhaseManager.Instance.StartPhase();
    }
}
