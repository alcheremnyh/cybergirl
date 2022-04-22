using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopController : MonoBehaviour
{
    bool isGoingToMap = false;

    [SerializeField]
    private Vector2 wayPoint;
    [SerializeField]
    private Vector3 scalePoint;

    RectTransform rect;

    public void GotoMap()
    {
        if (!isGoingToMap)
            StartCoroutine(DelayedGotoMap());
    }

    IEnumerator DelayedGotoMap()
    {
        rect = GetComponent<RectTransform>();
        isGoingToMap = true;
        yield return new WaitForSeconds(1f);
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<AudioSource>().time = 70f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingToMap)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, wayPoint, 3f * Time.deltaTime);
            rect.localScale = Vector2.Lerp(rect.localScale, scalePoint, 3f * Time.deltaTime);
        }
    }
}
