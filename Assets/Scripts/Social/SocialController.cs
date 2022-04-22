using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using TMPro;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class SocialController : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    [HideInInspector] private const string leaderBoard = "CgkI-JDV8fAGEAIQAQ";

    private void Awake()
    {
 /*       PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                text.text = userInfo;
                Debug.Log(userInfo);
            }
            else
                text.text = "Authentication failed";
            Debug.Log("Authentication failed");
        });*/
    }

    void Start()
    {
        

       
    }

    public void ShowLeaderBoard()
    {
  //      Social.ShowLeaderboardUI();
    }

    public void Login()
    {
/*        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                text.text = userInfo;
                Debug.Log(userInfo);
            }
            else
                text.text = "Authentication failed";
            Debug.Log("Authentication failed");
        });*/
    }


}

