using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosticsController : MonoBehaviour
{
    bool isGoingToMap = false;

    [SerializeField]
    private PlayerData player;

    [SerializeField]
    private Vector2 wayPoint;
    [SerializeField]
    private Vector3 scalePoint;

    RectTransform rect;

    public void ShowHide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void GotoMap()
    {
        player.IsFromGame(false);
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

    private void Start()
    {
     
    }

    private void Update()
    {
        if (isGoingToMap)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, wayPoint, 3f * Time.deltaTime);
            rect.localScale = Vector2.Lerp(rect.localScale, scalePoint, 3f * Time.deltaTime);
        }

    }
}
