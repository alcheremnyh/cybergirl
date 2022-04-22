using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class StarEffect : MonoBehaviour
{

    RectTransform rect;
    CanvasGroup cg;

    Vector2 defaultPos;
    Quaternion defaultRot;
    Vector2 defaultScale;

    Vector2 wayPoint;
    Quaternion rotPoint;
    Vector2 scalePoint;
    float alphaPoint;


    void ResetWayPoint()
    {
        wayPoint = rect.anchoredPosition;
        rotPoint = rect.localRotation;
        scalePoint = rect.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        defaultPos = rect.anchoredPosition;
        defaultRot = rect.localRotation;
        defaultScale = rect.localScale;
        ResetWayPoint();
    }

    public void OutScore()
    {
        rect.anchoredPosition = defaultPos;
        rect.localRotation = defaultRot;
        rect.localScale = defaultScale;
        cg.alpha = 1f;
        wayPoint = new Vector3(rect.anchoredPosition.x+100f, rect.anchoredPosition.y - 100f);
        rotPoint = transform.parent.rotation;
        scalePoint = new Vector2(3f, 3f);
    }

    void Move()
    {
        if (cg.alpha != alphaPoint)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, wayPoint, 12f * Time.deltaTime);
            rect.localScale = Vector2.Lerp(rect.localScale, scalePoint, 12f * Time.deltaTime);
            rect.localRotation = Quaternion.Lerp(rect.localRotation, rotPoint, 6f * Time.deltaTime);
            cg.alpha = Mathf.Lerp(cg.alpha, alphaPoint, 3f * Time.deltaTime);
            if (cg.alpha < .1)
            {
                ResetWayPoint();
                cg.alpha = 0;
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
