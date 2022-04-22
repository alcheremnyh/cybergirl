using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCameraController : MonoBehaviour
{

    public float speed = 0.01f;
    Vector3 defaultPosition;
    Vector3 targetPosition;
    bool move = false;
    bool mapLoader = false;

    public void Move(Vector3 pos, bool loadMap)
    {
        if (!move)
        {
            mapLoader = loadMap;
            targetPosition = pos;
            move = true;
        }
    }

    public void Back()
    {
        if (!move) { 
            targetPosition = defaultPosition;
            move = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            if(ExtensionMethods.Round(transform.position, 2) == ExtensionMethods.Round(targetPosition, 2))
            {
                move = false;
                if (mapLoader) { 
                    SceneManager.LoadScene("Game"); 
                }
            }
        }
    }
}
