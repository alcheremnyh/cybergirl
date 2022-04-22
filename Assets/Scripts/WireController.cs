using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField]
    private GameObject Red;
    public byte rotation = 0;
    public byte wireId = 0;
    public bool[] connections;
    public bool closed = false;
    bool rotated = true;

    MapController mapController;



    public bool[] GetConnections()
    {
        bool[] result = new bool[connections.Length];
        for (int i = 0; i < connections.Length; i++)
        {
            int id = rotation + i;
            Debug.Log(gameObject.name + "<< " + rotation + " << " + id + " : " + i);
            if (id > connections.Length - 1)
                id -= connections.Length;
            Debug.Log(gameObject.name+">>> " + connections.Length + " : " + result.Length);
            Debug.Log(gameObject.name + ">>> " + id + " : " + i);
            result[id] = connections[i];
        }
        return result;
    }
    private void OnMouseDown()
    {
        if(!Camera.main.GetComponent<CameraController>().isPaused)
            Camera.main.GetComponent<CameraController>().canRotate = true;
    }
    private void OnMouseUp()
    {
        if (!mapController.closed)
            if (Camera.main.GetComponent<CameraController>().canRotate)
                Rotate();
    }

    void Rotate()
    {
        rotation++;
        if (rotation > 3) rotation = 0;
        rotated = false;
        mapController.lastWire = this;
    }

    public void UndoRotate()
    {
        rotation--;
        if (rotation == 255) rotation = 3;
        rotated = false;
    }

    void SetRandomRotation()
    {
        rotation = (byte)Random.Range(0, 4);
        transform.Rotate(rotation * -90 * Vector3.forward);
    }

    public void OpenCircuit()
    {
        if(closed)
            Debug.Log(this.name + " open circut");
        closed = false;
    }

    public bool ClosedCircuit()
    {
        if (!closed)
            closed = true;
        return !closed;
    }

    // Start is called before the first frame update
    void Start()
    {
        mapController = GameObject.Find("Map").GetComponent<MapController>();
        SetRandomRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotated)
        {
            Quaternion newRotation = Quaternion.AngleAxis(rotation * -90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 400f * Time.deltaTime);
            if (transform.rotation == newRotation)
            {
                rotated = true;
                mapController.Search();
            }
        }
    }

    private void FixedUpdate()
    {
        //Red.SetActive(!closed);
        if (mapController.closed && !closed && !Red.activeSelf)
            StartCoroutine(DelayedRed());
    }

    IEnumerator DelayedRed()
    {
        Red.SetActive(true);
        yield return new WaitForSeconds(3f);
        Red.SetActive(false);

    }

}
