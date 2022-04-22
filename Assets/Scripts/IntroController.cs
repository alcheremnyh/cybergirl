using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
