using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLevelsController : MonoBehaviour
{
    [SerializeField]
    private PlayerData player;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TMP_Text>().text = player.GetMission().ToString()+"/60";
    }

}
