using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllerStart : MonoBehaviour
{

    [SerializeField]
    private Vector2 wayPoint;
    [SerializeField]
    private float scalePoint;


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
            if (rect.anchoredPosition.y - 1f > wayPoint.y || rect.anchoredPosition.y + 1f < wayPoint.y)
            {
                rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, wayPoint, Time.deltaTime );
                rect.localScale = Vector3.Lerp(rect.localScale, new Vector3(scalePoint, scalePoint, 1f), Time.deltaTime);
            }
            else
            isOnStart = false;

        }
    }
}
