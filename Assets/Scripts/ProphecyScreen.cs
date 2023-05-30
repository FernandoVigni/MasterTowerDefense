using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ProphecyScreen : MonoBehaviour
{
    [SerializeField] public GameObject TextOne;
    [SerializeField] public GameObject TextTwo;
    [SerializeField] public GameObject TextThree;
    [SerializeField] public GameObject skipButton;
    [SerializeField] public GameObject letsGoButton;

    public void StartProphecyScene() 
    {
        TextOne.SetActive(false);
        TextTwo.SetActive(false);
        TextThree.SetActive(false);
        skipButton.SetActive(false);
        letsGoButton.SetActive(false);
        PlayTextOne();
    }

    public async Task PlayTextOne() 
    {
        await Task.Delay(2500);
        TextOne.SetActive(true);
        await PlayTextTwo();
    }
    
    public async Task PlayTextTwo() 
    {
        await Task.Delay(3500);
        skipButton.SetActive(true);
        TextTwo.SetActive(true);
        await PlayTextThree();
    }

    public async Task PlayTextThree() 
    {
        await Task.Delay(2000);
        TextThree.SetActive(true);
        await Task.Delay(0500);
        ChangeButton();
    }

    public void ChangeButton() 
    {
        skipButton.SetActive(false);
        letsGoButton.SetActive(true);
    }

    public void StartGame() 
    {
        TextOne.SetActive(false);
        TextTwo.SetActive(false);
        TextThree.SetActive(false);
        skipButton.SetActive(false);
        letsGoButton.SetActive(false);
        MainMenu.Instance.prophecyScreen.gameObject.SetActive(false);
        PhaseManager.Instance.StartPhase();
    }
}
