using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] Types;
    public byte aroundBlocks = 0;

    public void SetType(int id)
    {
        gameObject.transform.GetComponent<SpriteRenderer>().sprite = Types[id];
    }
}
