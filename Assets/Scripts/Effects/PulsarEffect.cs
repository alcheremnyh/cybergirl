using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PulsarEffect : MonoBehaviour
{
    public float min = 0.7f;
    public float max = 1f;
    public float speed = 1;
    CanvasGroup pulsar;
    bool isFadeIn = true;

    // Start is called before the first frame update
    void Start()
    {
        pulsar = GetComponent<CanvasGroup>();
        pulsar.alpha = min;
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = pulsar.alpha;
        if (isFadeIn)
        {
            alpha += Time.deltaTime * speed;
            if (alpha > max)
            {
                alpha = max;
                isFadeIn = !isFadeIn;
            }
        }
        else
        {
            alpha -= Time.deltaTime;
            if (alpha < min)
            {
                alpha = min;
                isFadeIn = !isFadeIn;
            }
        }
        pulsar.alpha = alpha;
    }

    public void SetActive(bool isActive)
    {
        this.enabled = isActive;
    }
}
