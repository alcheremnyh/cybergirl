using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct WayPointsStruct
{
    public Vector2 wayPoint;
    public Vector3 scalePoint;
    public float secondsAfter;
    public bool isLerp;
}

public class MapControllerStartPoints : MonoBehaviour
{
    [SerializeField]
    private WayPointsStruct[] WayPoints;
    [SerializeField]
    private GameObject PlayPointButton;


    int wayPointId = 0;

    RectTransform rect;
    public bool isOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isOnStart)
        {
            if (WayPoints[wayPointId].isLerp)
            {
                Vector3 scale = rect.localScale - WayPoints[wayPointId].scalePoint;
                Vector2 pos = rect.anchoredPosition - WayPoints[wayPointId].wayPoint;


                if (pos.magnitude > 30f || scale.magnitude > 1f)
                {
                    rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, WayPoints[wayPointId].wayPoint, Time.deltaTime);
                    rect.localScale = Vector3.Lerp(rect.localScale, WayPoints[wayPointId].scalePoint, Time.deltaTime);
                }
                else
                {
                    isOnStart = false;
                    StartCoroutine(DelayedNext());
                }
            }
        }
    }


    IEnumerator DelayedNext()
    {
        yield return new WaitForSeconds(WayPoints[wayPointId].secondsAfter);
        wayPointId++;

        if(wayPointId == WayPoints.Length-1)
            PlayPointButton.SetActive(true);

        if (wayPointId >= WayPoints.Length)
            isOnStart = false;
        else
            isOnStart = true;
    }

    private void OnDisable()
    {
        if (!PlayPointButton.activeSelf)
            PlayPointButton.SetActive(true);
    }
}
