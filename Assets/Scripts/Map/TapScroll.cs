using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapScroll : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]
    private PlayerData player;

    [SerializeField]
    private MapControllerStartPoints mapControllerStart;

    [SerializeField]
    private GameObject UnfocusButton;

    [SerializeField]
    private PartsController[] Parts;

    private float upLimit = -2048f;
    private float downLimit = 2048f;
    private float leftLimit = -470f;
    private float rightLimit = 470f;

    bool isAutoScroll = false;
    bool isScroll = true;
    bool isFocusing = false;
    bool isShowBack = false;
    Vector2 scrollStep;
    RectTransform rect;

    Vector2 wayPoint;
    Vector3 scalePoint;

    Vector2 backWayPoint;
    Vector3 backScalePoint;
    int activePartId=-1;


    public void OnPointerDown(PointerEventData eventData)
    {
        isAutoScroll = false;
        if (mapControllerStart.enabled)
            mapControllerStart.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isAutoScroll = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isAutoScroll = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isScroll)
        {
            scrollStep = eventData.delta / Time.deltaTime;
            Move(scrollStep * Time.deltaTime);
        }
    }

    Vector2 SpeedCheck()
    {
        if (scrollStep.x != 0.0f)
        {
            if (scrollStep.x > 50f)
            {
                scrollStep.x -= 50f;
            }
            else if (scrollStep.x < -50f)
            {
                scrollStep.x += 50f;
            }
            else
            {
                scrollStep.x = 0;
            }
        }

        if (scrollStep.y != 0.0f)
        {
            if (scrollStep.y > 50f)
            {
                scrollStep.y -= 50f;
            }
            else if (scrollStep.y < -50f)
            {
                scrollStep.y += 50f;
            }
            else
            {
                scrollStep.y = 0;
            }
        }
        return scrollStep * Time.deltaTime;
    }

    void Move(Vector2 step)
    {
        rect.anchoredPosition += step;
        if (rect.anchoredPosition.y < upLimit * rect.localScale.y)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, upLimit * rect.localScale.y);
        }
        else if (rect.anchoredPosition.y > downLimit * rect.localScale.y)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, downLimit * rect.localScale.y);
        }

        if (rect.anchoredPosition.x < leftLimit * rect.localScale.x)
        {
            rect.anchoredPosition = new Vector2(leftLimit * rect.localScale.x, rect.anchoredPosition.y);
        }
        else if (rect.anchoredPosition.x > rightLimit * rect.localScale.x)
        {
            rect.anchoredPosition = new Vector2(rightLimit * rect.localScale.x, rect.anchoredPosition.y);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        if (player.IsFromGame())
        {
            isFocusing = true;
            isShowBack = true;
            SaveBackPoints();
            StartCoroutine(DelayedShowPart(0, 0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activePartId < 0)
        {
            if (!isFocusing)
            {
                if (isAutoScroll)
                    Move(SpeedCheck());
                checkTouchOnScreen();
            }
            else
            {
                MoveTo();
            }
        }
    }

    void MoveTo()
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, wayPoint, 3f * Time.deltaTime);
        rect.localScale = Vector2.Lerp(rect.localScale, scalePoint, 3f * Time.deltaTime);
        if (rect.anchoredPosition.x + 1f > wayPoint.x && rect.anchoredPosition.x - 1f < wayPoint.x)
        { 
            isFocusing = false;
            isAutoScroll = false;
        }
    }

    void checkTouchOnScreen()
    {
        #region Touch Mobile


        if (Input.touchCount == 1)
        {
            isScroll = true;
        }

        #endregion
    }

    public void GotoBrain()
    {
        if (!isFocusing)
        {
            if (mapControllerStart.enabled)
                mapControllerStart.enabled = false;
            scrollStep = Vector2.zero;
            isFocusing = true;
            isShowBack = true;
            wayPoint = new Vector2(-1916, -17230);
            scalePoint = new Vector3(8f, 8f, 1f);
            SaveBackPoints();
            StartCoroutine(DelayedShowPart(0, 1.5f));
        }
    }

    IEnumerator DelayedShowPart(int partId, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        activePartId = partId;
        Parts[activePartId].SetActive(true);
        UnfocusButton.SetActive(isShowBack);
    }

    void SaveBackPoints()
    {
        backWayPoint = rect.anchoredPosition;
        backScalePoint = rect.localScale;
    }

    public void GoBack()
    {
        Parts[activePartId].SetActive(false);
        activePartId = -1;
        isFocusing = true;
        isShowBack = false;
        wayPoint = backWayPoint;
        scalePoint = backScalePoint;
    }

    public void GoToWorkshop()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(1);
    }

}
