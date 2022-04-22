using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; // Required when using Event data.

public class Button : MonoBehaviour, IPointerDownHandler // required interface when using the OnPointerDown method.
{
    public Image buttonON;
    float time = 0.5f;


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonON.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (buttonON.enabled)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0)
        {
            //SceneManager.LoadScene("Map");
        }
    }

}
