using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoresController : MonoBehaviour
{
    [SerializeField]
    private PlayerData player;

    [SerializeField]
    private StarEffect starEffect;

    [SerializeField]
    private TMP_Text StarText;
    
    [SerializeField]
    private TMP_Text CupText;


    private void FixedUpdate()
    {
        if(player.GetStars().ToString() != StarText.text)
        {
            if (player.GetStars() < int.Parse(StarText.text))
                starEffect.OutScore();
            StarText.text = player.GetStars().ToString();
        }
    }
}
