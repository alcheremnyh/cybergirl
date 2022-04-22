using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapController : MonoBehaviour
{
    [SerializeField]
    private PlayerData player;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject CompleteText;

    [SerializeField]
    private GameObject LevelText;

    [SerializeField]
    private WinStarsController winStars;

    [SerializeField]
    private GameObject TryAgainText;

    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    private GameObject Scores;

    [SerializeField]
    private GameObject Help;

    public GameObject[] walls;
    public GameObject[] wires;
    public GameObject lighting;

    public WireController lastWire;

    public int width = 0;           // map width
    public int height = 0;          // map height

    public bool closed = false;    // when the scheme is closed

    int wireCount;                  // number wires on map
    byte[,] map;            // map array

    [SerializeField]
    private List<WireController> wire;

    int sliderStep = 10;
    int wiresCount = 0;
    float circutCount = 0;
    


    public Vector2 GetSize()
    {
        return new Vector2(width, height);
    }

    void CreateBlock(GameObject block, int x, int y)
    {
        GameObject tmp = Instantiate(block, new Vector3(x - width / 2, height / 2 - y, 0), Quaternion.identity);
        tmp.transform.parent = transform;
        if(tmp.tag == "wire")
        { 
            tmp.name = "wire [" + x + ":" + y + "]";
            wire.Add(tmp.GetComponent<WireController>());
            wiresCount++;
        }
    }

    byte ScanWalls(int x, int y)
    {
        byte[] around = {0,0,0,0,0,0,0,0};
        byte result;
        if (y <= 0)
        {
            around[7] = 2;
            around[0] = 2;
            around[1] = 2;
        }

        if (y >= height-1)
        {
            around[5] = 2;
            around[4] = 2;
            around[3] = 2;
        }

        if (x <= 0)
        {
            around[7] = 2;
            around[6] = 2;
            around[5] = 2;
        }

        if (x >= width-1)
        {
            around[1] = 2;
            around[2] = 2;
            around[3] = 2;
        }
        result = 0;
        for (int i=0; i<8; i++)
        {
            if (around[i] == 0)
            {
                switch (i)
                {
                    case 0:
                        if (map[x, y - 1] == 100) result+= 0b10000000;
                        break;
                    case 1:
                        if (map[x + 1, y - 1] == 100) result += 0b1000000;
                        break;
                    case 2:
                        if (map[x + 1, y] == 100) result += 0b100000;
                        break;
                    case 3:
                        if (map[x + 1, y + 1] == 100) result += 0b10000; 
                        break;
                    case 4:
                        if (map[x, y + 1] == 100) result += 0b1000;
                        break;
                    case 5:
                        if (map[x - 1, y + 1] == 100) result += 0b100;
                        break;
                    case 6:
                        if (map[x - 1, y] == 100) result += 0b10; 
                        break;
                    case 7:
                        if (map[x - 1, y - 1] == 100) result += 0b1;
                        break;
                }
            }
        }


        return result;
    }

    void CreateWall(int x, int y)
    {
        GameObject block = walls[0];
        Vector3 eulers = new Vector3(0, 0, 0);

        byte aroundBlocks = ScanWalls(x, y);

        if (aroundBlocks == 46 || aroundBlocks == 58 || aroundBlocks == 34 || aroundBlocks == 38 || aroundBlocks == 35 || aroundBlocks == 98 || aroundBlocks == 50 || aroundBlocks == 2 || aroundBlocks == 32)
        {
            block = walls[0];
        }
        else if (aroundBlocks == 8 || aroundBlocks == 128 || aroundBlocks == 141 || aroundBlocks == 216 || aroundBlocks == 139 || aroundBlocks == 143 || aroundBlocks == 142 || aroundBlocks == 232 || aroundBlocks == 184 || aroundBlocks == 248 || aroundBlocks == 136 || aroundBlocks == 152 || aroundBlocks == 137 || aroundBlocks == 200 || aroundBlocks == 140)
        {
            if (x < width - 1)
            {
                
                    block = walls[0];
                    if(y>height/2) block = walls[4];
            }
            else
            {
                if (y < height / 2)
                {
                    block = walls[4];
                }
                else
                {
                    block = walls[0];
                }
            }

    
            eulers = -90 * Vector3.forward;
        }
        else if (aroundBlocks == 10)
        {
            block = walls[5];
        }
        else if (aroundBlocks == 40 ||  aroundBlocks == 239 )
        {
            block = walls[3];
            eulers = -270 * Vector3.forward;
        }
        else if (aroundBlocks == 160 ||  aroundBlocks == 31)
        {
            if (y < height - 1)
            {
                block = walls[3];
            }
            else
            {
                block = walls[5];
            }
            
            eulers = -180 * Vector3.forward;
        }
        else if (aroundBlocks == 130 || aroundBlocks == 199)
        {
            block = walls[3];
            eulers = -90 * Vector3.forward;
        }
        else
        {
            Debug.Log(x + " : " + y);
            Debug.Log(aroundBlocks);
        }

        GameObject tmp = Instantiate(block, new Vector3(x - width / 2, height / 2 - y, 0), Quaternion.identity);
        tmp.GetComponent<WallController>().aroundBlocks = aroundBlocks;
        tmp.GetComponent<WallController>().SetType(player.GetMissionType());
                tmp.transform.Rotate(eulers);
                tmp.transform.parent = transform;
    }

    public void Build(int mapId)
    {
        levelText.text = "LVL "+(mapId+1);
        wire = new List<WireController>();
        wiresCount = 0;
        MapData mapData = new MapData();
        map = mapData.Get(mapId);
        wireCount = 0;
        width = map.GetLength(0);
        height = map.GetLength(1);

        for (int i=0; i<width; i++)
            for(int j=0; j<height; j++)
            {
                if(map[i, j]<100) wireCount++;
                switch(map[i, j])
                {
                    case 0:
                        CreateBlock(wires[0], i, j);
                        break;
                    case 1:
                        CreateBlock(wires[1], i, j);
                        break;
                    case 2:
                        CreateBlock(wires[2], i, j);
                        break;
                    case 3:
                        CreateBlock(wires[3], i, j);
                        break;
                    case 4:
                        CreateBlock(wires[4], i, j);
                        break;
                    case 100:
                        CreateWall(i, j);
                        break;
                    case 101:
                        CreateBlock(walls[1], i, j);
                        break;
                    case 102:
                        CreateBlock(walls[2], i, j);
                        break;
                }
            }
    }


    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<SaveSystem>().Load();
        player.IsFromGame(true);
        Build(player.GetMission());
        StartCoroutine(DelayedInit());
        InitWires();
        if(player.GetMission()==0 && player.GetStars() == 0)
        {
            player.SetStars(3);
            StartCoroutine(DelayedHelp(0.5f));
            Camera.main.GetComponent<SaveSystem>().Save();
        }
    }

    void InitWires()
    {
        slider.maxValue = wireCount* sliderStep;
        slider.value = 0;
    }

    void OpenCircuit()     // Open circuit
    {
        wire.ForEach(delegate (WireController w)
        {
            w.OpenCircuit();
        });
    }

    int InvertWay(int way)
    {
        way += 2;
        if (way > 3)
            way -= 4;
        return way;
    }
    bool IsConnected(int way, bool[] connections)
    {
        way = InvertWay(way);
        return connections[way];
    }

    void Discharge(Vector3 pos, byte way)
    {
        Vector2 vector = Vector2.up;
        switch (way)
        {
            case 0:
                pos.y += 1;
                break;
            case 1:
                pos.x += 1;
                break;
            case 2:
                pos.y -= 1;
                break;
            case 3:
                pos.x -= 1;
                break;
        }

        RaycastHit2D hit = Physics2D.Raycast(pos, vector, 0.3f);
        if (hit.collider != null)
        {
            if (hit.transform.tag == "wire")
            {
                Debug.Log(hit.transform.name + pos.x + " : " + pos.y);
                WireController wc = hit.transform.GetComponent<WireController>();
                bool[] connections = wc.GetConnections();
                if (IsConnected(way, connections))
                {
                    if (!wc.ClosedCircuit())
                    {
                        circutCount++;
                        ShowLighting(pos, 0.2f);
                        if (circutCount <= wireCount)
                        {
                            NextLook(way, connections, hit.transform.position);
                        }else
                        {
                            Debug.LogError("LOOOOOOP");
                        }
                    }
                }
            }
            else if (hit.transform.tag == "plus")
            {
                closed = true;
            }
        }
    }

    void ShowLighting(Vector3 pos, float killTimer)
    {
        GameObject tmp = Instantiate(lighting, pos, Quaternion.identity);
        tmp.transform.parent = transform;
        tmp.GetComponent<Lighting>().killTimer = killTimer;
    }

    void NextLook(int way, bool[] connections, Vector3 pos)
    {
        way = InvertWay(way);
        for (byte i = 0; i < connections.Length; i++)
            if (i != way && connections[i])
                Discharge(pos, i);
    }

    void PowerOn()
    {
        circutCount = 0;
        GameObject[] minus = GameObject.FindGameObjectsWithTag("minus");
        for (int i = 0; i < minus.Length; i++)
        {
            Discharge(minus[i].transform.position, 1);
        }

        if (closed)
        {
            if (circutCount == wireCount)
                MissionComplete();
            else
                MissionTryAgain();
        }
        Debug.Log(circutCount + " / " +  wireCount);
    }

    void MissionTryAgain()
    {
        if (player.GetStars()<=0) {
            StartCoroutine(DelayedMissionTryAgain());
        }
        else
        {
            if (lastWire != null)
            {
                player.SetStars(player.GetStars() - 1);
                StartCoroutine(DelayedUnClosed());
            }
        }
        Camera.main.GetComponent<SaveSystem>().Save();
    }

    IEnumerator DelayedMissionTryAgain()
    {
        yield return new WaitForSeconds(1.5f);
        TryAgainText.SetActive(true);
        ShowLevelNumber();
        StartCoroutine(DelayedRestart());
    }

    IEnumerator DelayedUnClosed()
    {
        yield return new WaitForSeconds(0.5f);
        lastWire.UndoRotate();
        closed = false;
    }

    IEnumerator DelayedInit()
    {
        yield return new WaitForSeconds(1f);
        if (player.GetMission() > 0)
            Scores.SetActive(true);
    }

    void ShowLevelNumber()
    {
        LevelText.GetComponent<TMP_Text>().text = "LEVEL " + (player.GetMission() + 1);
    }

    void MissionComplete()
    {
        StartCoroutine(DelayedMissionComplete());
    }

    void LightOn()
    {
        wire.ForEach(delegate (WireController w)
        {
            ShowLighting(w.transform.position, 3f);
        });
    }

    IEnumerator DelayedMissionComplete()
    {
        LightOn();
        yield return new WaitForSeconds(1.5f);
        CompleteText.SetActive(true);
        ShowLevelNumber();
        LevelText.SetActive(true);
        if (player.GetMission() == 0)
            Scores.SetActive(true);
        int winStarsCount = player.GetMissionType()+1;//(int)((wireCount) / 16);
        winStars.Show(winStarsCount);
        player.SetStars(player.GetStars() + winStarsCount);
        player.SetMission(player.GetMission() + 1);
        StartCoroutine(DelayedGotoMap());
        Camera.main.GetComponent<SaveSystem>().Save();
    }


    public void Search()
    {
        OpenCircuit();

        Debug.Log("===== Power ON =====");
        PowerOn();
        Debug.Log("===== Power OFF ====");
    }

    public void GotoMap()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(2);
    }

    public void GotoStartScreen()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(1);
    }

    IEnumerator DelayedGotoMap()
    {
        yield return new WaitForSeconds(3f);
        GotoMap();
    }

    public void Restart()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(3);
    }

    IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(3);
    }

    IEnumerator DelayedHelp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Help.SetActive(true);
    }

    private void Update()
    {
        if (slider.value < circutCount * sliderStep)
            slider.value++;
        else if (slider.value > circutCount * sliderStep)
            slider.value--;
    }
}
