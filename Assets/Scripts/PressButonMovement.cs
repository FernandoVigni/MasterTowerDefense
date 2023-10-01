using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButonMovement : MonoBehaviour
{
    public RectTransform myRectTransform;
    public float deltaY; // Distance to move up and down
    public float speed; // Movement speed
    public bool goDownDone;
    public bool goUpDone;

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
     /*   // Move down
        while (myRectTransform.localPosition.y > initialPosition.y && !goDownDone)
        {
            Vector3 newPosition = myRectTransform.localPosition;
            newPosition.y -= speed * Time.deltaTime;
            myRectTransform.localPosition = newPosition;
            yield return null;
        }
        goDownDone = true;*/

        // Move up
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
}
