using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int MissionId;

    [SerializeField]
    private int score0;
    [SerializeField]
    private int score1;
    [SerializeField]
    private int good;
    [SerializeField]
    private int bad;
    [SerializeField]
    private bool isFromGame;
    [SerializeField]
    private int missionType;
    [SerializeField]
    private bool isSound;

    
    public bool IsSound
    {
        get
        {
            return isSound;
        }
        set
        {
            isSound = value;
        }
    }

    public void IsFromGame(bool fromGame)
    {
        isFromGame = fromGame;
        Debug.Log("IsFromGame" + isFromGame);
    }

    public bool IsFromGame()
    {
        return isFromGame;
    }

    public void SetMission(int missionId)
    {
        MissionId = missionId;
    }

    public int GetMission()
    {
        return MissionId;
    }

    public void SetMissionType(int id)
    {
        missionType = id;
    }

    public int GetMissionType()
    {
        return missionType;
    }

    public int GetStars()
    {
        return score0;
    }

    public void SetStars(int value)
    {
        score0 = value;
    }

}
