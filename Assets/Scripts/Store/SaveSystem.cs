using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    [SerializeField]
    private PlayerData player;

 /*   private string rsaEncrypt(string s)
    {
        //byte[] b = RSA.Encrypt( System.Text.Encoding.UTF8.GetBytes(s), true);
        return System.Convert.ToBase64String(b);
    }*/


    public void Save()
    {
        PlayerPrefs.SetInt("mission", player.GetMission());
        PlayerPrefs.SetInt("score0", player.GetStars()) ;
    }

    public void Load()
    {
        player.SetMission(PlayerPrefs.GetInt("mission"));
        player.SetStars(PlayerPrefs.GetInt("score0"));
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
