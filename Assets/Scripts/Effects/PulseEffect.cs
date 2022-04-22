using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulseEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Pulse;
    private RectTransform[] pulseRect;
    private Image[] pulseImage;

    float width = 480f;
    float status = 0;

    // Start is called before the first frame update
    void Start()
    {
        pulseRect = new RectTransform[Pulse.Length];
        pulseImage = new Image[Pulse.Length];
        for (int i = 0; i<Pulse.Length; i++)
        {
            pulseRect[i] = Pulse[i].GetComponent<RectTransform>();
            pulseImage[i] = Pulse[i].GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        status += Time.deltaTime;
        if (status > 1)
        {
            status = 0;
        }
        DrawPulse();
    }
    void DrawPulse()
    {
        pulseRect[0].anchoredPosition = new Vector2(-status*width,0);
        pulseImage[0].fillAmount = 1f - status;
        pulseRect[1].anchoredPosition = new Vector2(width - status * width, 0);
        pulseImage[1].fillAmount = status;
    }

}
