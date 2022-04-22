using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    float rotTimer = 0;
    public float killTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (killTimer > 0)
        {
            killTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        if (rotTimer < 0.05f) {
            rotTimer += Time.deltaTime;
        }
        else
        {
            rotTimer = 0;
            transform.rotation = Random.rotation;
        }
        
    }
}
