using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStarsController : MonoBehaviour
{
    [SerializeField]
    private GameObject Star;

    private void Start()
    {

    }

    public void Show(int winCount)
    {
        gameObject.SetActive(true);
        float offset = -284f * (winCount-1) / 2;
        for(int i = 0; i<winCount; i++)
        {
            GameObject star = Instantiate(Star, transform);
            RectTransform starRect = star.GetComponent<RectTransform>();
            starRect.sizeDelta = new Vector2(256f, 256f);
            starRect.anchoredPosition = new Vector2(offset + i * 284f, 0);
        }
    }
}
