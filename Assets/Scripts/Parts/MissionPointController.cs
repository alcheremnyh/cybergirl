using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(PulsarEffect))]
public class MissionPointController : MonoBehaviour
{
    [SerializeField]
    private int Id;

    PulsarEffect pulsar;

    // Start is called before the first frame update
    void Start()
    {
        pulsar = GetComponent<PulsarEffect>();
        pulsar.SetActive(false);
    }

    public void Pulsar(bool isOn)
    {
        pulsar.SetActive(isOn);
    }

    public void SetType(Sprite sprite)
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }

    public float GetScale()
    {
        return gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
