using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    [SerializeField]
    private PlayerData player;

    [SerializeField]
    private Sprite[] PointTypes;

    [SerializeField]
    MissionPointController[] missions;

    int missionId;

    IEnumerator DelayedActivateMissionPoint()
    {
        yield return new WaitForSeconds(1f);
        ActivateMissionPoint();
    }

    IEnumerator DelayedSetPointTypes(int id)
    {
        yield return new WaitForSeconds(0.1f);
        if (id != missionId)
        {
            if (player.GetMission() > id)
                missions[id].SetType(PointTypes[3]);
            else
            {
                if (missions[id].GetScale() == 0.9f)
                    missions[id].SetType(PointTypes[4]);
                else if (missions[id].GetScale() == 0.6f)
                    missions[id].SetType(PointTypes[2]);
            }
        }
        id++;
        if (id < missions.Length)
            StartCoroutine(DelayedSetPointTypes(id));
    }

    private void Awake()
    {
        missionId = player.GetMission();

        StartCoroutine(DelayedActivateMissionPoint());
        StartCoroutine(DelayedSetPointTypes(0));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ActivateMissionPoint()
    {
        Debug.Log("missions[missionId].GetScale() " + missions[missionId].GetScale());
        missions[missionId].Pulsar(true);
        missions[missionId].SetType(PointTypes[1]);
        if (missions[missionId].GetScale() == 0.9f)
            player.SetMissionType(2);
        else if (missions[missionId].GetScale() == 0.6f)
            player.SetMissionType(1);
        else
            player.SetMissionType(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public void StartMission()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(3);
    }
}
