using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
    [SerializeField]
    RectTransform RingBack;

    public float TopLimit = 536f;
    public float BottomLimit = -536f;

     float TopAngle = 15f;
     float BottomAngle = -15f;
     float angleStep;

     float speed = 400f;

    public bool isMoveDown = false;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

        float wayLength = TopLimit - BottomLimit;
        float wayAngle = TopAngle - BottomAngle;
        angleStep = wayAngle / wayLength;
        switch(rect.anchoredPosition.y)
        {
            case 536f:
                rect.eulerAngles = new Vector3(TopAngle, 0, 0);
                break;
            case 0:
                rect.eulerAngles = new Vector3(0, 0, 0);
                break;
            case -536f:
                rect.eulerAngles = new Vector3(BottomAngle, 0, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveDown)
        {
            float y = rect.anchoredPosition.y - Time.deltaTime * speed;
            float a = rect.eulerAngles.x - Time.deltaTime * angleStep * speed;
            if (y < BottomLimit)
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, BottomLimit);
                rect.eulerAngles = new Vector3(BottomAngle, 0, 0);
                isMoveDown = !isMoveDown;
            }
            else
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
                rect.eulerAngles = new Vector3(a, 0, 0);
            }
        }
        else
        {
            float y = rect.anchoredPosition.y + Time.deltaTime * speed;
            float a = rect.eulerAngles.x + Time.deltaTime * angleStep * speed;
            if (y > TopLimit)
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, TopLimit);
                rect.eulerAngles = new Vector3(TopAngle, 0, 0);
                isMoveDown = !isMoveDown;
            }
            else
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y);
                rect.eulerAngles = new Vector3(a, 0, 0);
            }
        }
        RingBack.anchoredPosition = rect.anchoredPosition;
        RingBack.eulerAngles = rect.eulerAngles;
    }
}
