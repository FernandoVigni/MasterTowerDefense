using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButonMovement : MonoBehaviour
{
    public RectTransform myRectTransform;
    public float deltaY;
    public float deltaX;
    public float speed;
    public bool goDownDone;
    public bool goUpDone;
    public bool goRightDone;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = myRectTransform.transform.localPosition;
    }

    public void ButtonPressed()
    {
        goDownDone = false;
        goUpDone = false;
        StartCoroutine(MoveUpAndDown());
    }

    private IEnumerator MoveUpAndDown()
    {       
        while (myRectTransform.localPosition.y > initialPosition.y - deltaY)
        {
            Vector3 newPosition = myRectTransform.localPosition;
            newPosition.y -= speed * Time.deltaTime;
            myRectTransform.localPosition = newPosition;
            yield return null;
        }
        goUpDone = true;
        myRectTransform.transform.localPosition = initialPosition;
    }

    public IEnumerator MoveRightAndLeft() 
    {
        goRightDone = false;
        while (myRectTransform.localPosition.x > initialPosition.x - deltaX)
        {
            Vector3 newPosition = myRectTransform.localPosition;
            newPosition.x -= speed * Time.deltaTime;
            myRectTransform.localPosition = newPosition;
            yield return null;
        }
        goRightDone = true;
        myRectTransform.transform.localPosition = initialPosition;
    }
   

    public void StartRightMove() 
    {
        StartCoroutine(MoveRightAndLeft());
    }
}
