using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegionCollider : MonoBehaviour
{
    public bool reset = false;
    public bool loadMap = false;

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        if (!reset)
        {
            Camera.main.GetComponent<MapCameraController>().Move(gameObject.transform.position, loadMap);
        }
        else
        {
            Camera.main.GetComponent<MapCameraController>().Back();
        }
    }
}
