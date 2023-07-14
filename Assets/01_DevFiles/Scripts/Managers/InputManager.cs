using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 initPos;
    private bool isDragging;
    private Coroutine calculateCR;
    private float rotValue;

    public float RotValue
    {
        get { return rotValue; }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        initPos = eventData.position;

        calculateCR = StartCoroutine(CalculateCR());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (calculateCR != null)
            StopCoroutine(calculateCR);

        rotValue = 0;
    }

    // if you still pressing screen, this function run
    IEnumerator CalculateCR()
    {
        while (isDragging)
        {
            rotValue = SetRotationValue();
            yield return null;
        }
        calculateCR = null;
    }

    // Check your turn right or left
    // if return 1 = turn right, else turn left
    private int SetRotationValue()
    {
        if (initPos.x - Screen.width / 2 > 0)
            return 1;
        else
            return -1;
    }
}
