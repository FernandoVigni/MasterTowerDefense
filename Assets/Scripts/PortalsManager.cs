using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalsManager : MonoBehaviour
{
    [SerializeField] public GameObject leftPortal;
    [SerializeField] public GameObject rightPortal;

    public void TurnOnLeftPortal() 
    {
        leftPortal.SetActive(true);
    }

    public void TurnOnRightPortal() 
    {
        rightPortal.SetActive(true);
    }

    public void TurnOffLeftPortal()
    {
        leftPortal.SetActive(false);
    }

    public void TurnOffRightPortal()
    {
        rightPortal.SetActive(false);
    }
}
