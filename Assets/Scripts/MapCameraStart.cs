using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOnStart : MonoBehaviour
{
    [SerializeField]
    private Vector3 wayPoint;
    [SerializeField]
    private float scalePoint;

    bool isOnStart = true;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnStart)
        {
            //if (transform.position.y + 0.02 < wayPoint.x && transform.position.y - 0.02 > wayPoint.x)
            {
                transform.position = Vector3.Lerp(transform.position, wayPoint, Time.deltaTime);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, scalePoint, Time.deltaTime);
            }
            //else
                //isOnStart = false;

        }
    }
}
