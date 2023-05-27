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

    public async Task PlayTextOne() 
    {
        await Task.Delay(3500);
        TextOne.SetActive(true);
        await PlayTextTwo();
    }
    
    public async Task PlayTextTwo() 
    {
        await Task.Delay(4500);
        skipButton.SetActive(true);
        TextTwo.SetActive(true);
        await PlayTextThree();
    }

    public async Task PlayTextThree() 
    {
        await Task.Delay(4500);
        TextThree.SetActive(true);
        await Task.Delay(4000);
        ChangeButton();
    }

    public void ChangeButton() 
    {
        skipButton.SetActive(false);
        letsGoButton.SetActive(true);
    }
}
