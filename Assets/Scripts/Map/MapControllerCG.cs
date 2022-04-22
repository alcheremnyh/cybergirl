using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControllerCG : MonoBehaviour
{

    public void GotoMission()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
