using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButonMovement : MonoBehaviour
{
    public RectTransform myRectTransform;
    public float deltaY = 15f; // Distance to move up and down
    public float speed = 1f; // Movement speed
    public bool goDownDone;
    public bool goUpDone;

    private Vector3 initialPosition;

    private void Start()
    {

    }

    public void ButtonPressed()
    {
        goDownDone = false;
        goUpDone = false;
        StartCoroutine(MoveUpAndDown());
    }

    private IEnumerator MoveUpAndDown()
    {
        // Move up
        while (myRectTransform.localPosition.y < initialPosition.y + deltaY && !goUpDone)
        {
            Vector3 newPosition = myRectTransform.localPosition;
            newPosition.y += speed * Time.deltaTime;
            myRectTransform.localPosition = newPosition;
            yield return null;
        }
        goUpDone = true;

        // Move down
        while (myRectTransform.localPosition.y > initialPosition.y && !goDownDone)
        {
            Vector3 newPosition = myRectTransform.localPosition;
            newPosition.y -= speed * Time.deltaTime;
            myRectTransform.localPosition = newPosition;
            yield return null;
        }
        goDownDone = true;
    }
}
