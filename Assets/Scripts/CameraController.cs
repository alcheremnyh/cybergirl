using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private MapController Map;

    public bool canRotate = false;

    public bool isPaused = false;

    Vector2 Size = Vector2.zero;

    private float xSpeed = 0f;
    private float ySpeed = 0f;
    float TouchZoomSpeed = 0.01f;
    float ZoomMinBound = 3f;
    float ZoomMaxBound = 10f;

    float touchTimer = 0;

    Vector2 startTouch;

    public void SetPause(bool isActive)
    {
        isPaused = isActive;
        if (isPaused)
            canRotate = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 30;  // test, Limit FPS to battery safe
    }

    // Update is called once per frame

    void CheckSize()
    {
        if(Size == Vector2.zero)
        {
            Size = Map.GetSize();
            Debug.Log(Size);
        }
    }

    void Update()
    {
        #region Touch Mobile
        if (Input.touchCount == 0)
            touchTimer = 0;

        if (Input.touchCount > 0)
        {
            touchTimer += Time.deltaTime;
            if (touchTimer > 0.3f)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                        float k = 5f - (Camera.main.orthographicSize - 5f) / 3f;
                        xSpeed = touchDeltaPosition.x / k;
                        ySpeed = touchDeltaPosition.y / k;
                        if (Mathf.Abs(xSpeed) < 0.2f)
                            xSpeed = 0;

                        if (Mathf.Abs(ySpeed) < 0.5f)
                            ySpeed = 0;

                    }
                }

                if (Input.touchCount == 2)
                {
                    canRotate = false;
                    // get current touch positions
                    Touch tZero = Input.GetTouch(0);
                    Touch tOne = Input.GetTouch(1);
                    // get touch position from the previous frame
                    Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                    Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
                    float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                    float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);
                    // get offset value
                    float deltaDistance = oldTouchDistance - currentTouchDistance;
                    Zoom(deltaDistance, TouchZoomSpeed);
                }
            }
        }
        #endregion

        CheckSize();
        void Zoom(float deltaMagnitudeDiff, float speed)
        {
            Camera cam = Camera.main;
            cam.orthographicSize += deltaMagnitudeDiff * speed;
            // set min and max value of Clamp function upon your requirement
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, ZoomMinBound, ZoomMaxBound);
        }
        if (Mathf.Abs(xSpeed) > .3f || Mathf.Abs(ySpeed) > .3f)
        {
            canRotate = false;
            transform.Translate(-xSpeed * Time.deltaTime, -ySpeed * Time.deltaTime, 0);
        }
        if (xSpeed < -.3f)
        {
            xSpeed += .1f;
        }
        else if (xSpeed > .3f)
        {
            xSpeed -= .1f;
        }
        else
        {
            xSpeed = 0;
        }
        if (ySpeed < -.3f)
        {
            ySpeed += .1f;
        }
        else if (ySpeed > .3f)
        {
            ySpeed -= .1f;
        }
        else
        {
            ySpeed = 0;
        }
        if (Size.x / 2 < transform.position.x)
            transform.position = new Vector3(Size.x / 2, transform.position.y, transform.position.z);
        else if (-Size.x / 2 > transform.position.x)
            transform.position = new Vector3(-Size.x / 2, transform.position.y, transform.position.z);
        if (Size.y / 2 < transform.position.y)
            transform.position = new Vector3(transform.position.x, Size.y / 2, transform.position.z);
        else if (-Size.y / 2 > transform.position.y)
            transform.position = new Vector3(transform.position.x, -Size.y / 2, transform.position.z);
    }

}
