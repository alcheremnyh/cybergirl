using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyStatus : MonoBehaviour
{
    public float complete = 40f;
    public float done = 30f;

    [SerializeField]
    private Image Normal;
    [SerializeField]
    private Image Good;
    [SerializeField]
    private Image Bad;

    // Start is called before the first frame update
    void Start()
    {
        Draw();
    }

    void Draw()
    {
        float doneCount = done / 100;
        float completeCount = complete / 100;

        Normal.fillAmount = 1f - completeCount;
        Bad.fillAmount = 1f - doneCount;
        Good.fillAmount = doneCount;

    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }
}
