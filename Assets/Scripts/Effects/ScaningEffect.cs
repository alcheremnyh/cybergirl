using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaningEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject ScanLine;

    public float upLimit = 247f;
    public float downLimit = -290f;
    public float speed = 100f;

    bool isMoveDown = true;

    RectTransform scanRect;

    // Start is called before the first frame update
    void Start()
    {
        scanRect = ScanLine.GetComponent<RectTransform>();
        scanRect.anchoredPosition = new Vector2(0, upLimit);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveDown)
        {
            scanRect.anchoredPosition += Vector2.down * speed * Time.deltaTime;

            if (scanRect.anchoredPosition.y < downLimit)
            {
                scanRect.anchoredPosition = new Vector2(0, downLimit);
                isMoveDown = !isMoveDown;
            }
        }
        else
        {
            scanRect.anchoredPosition += Vector2.up * speed * Time.deltaTime;

            if (scanRect.anchoredPosition.y > upLimit)
            {
                scanRect.anchoredPosition = new Vector2(0, upLimit);
                isMoveDown = !isMoveDown;
            }
        }

    }
}
