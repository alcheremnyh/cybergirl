using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonController : MonoBehaviour
{
    [SerializeField]
    private PlayerData player;

    [SerializeField]
    private AudioSource sound;

    [SerializeField]
    private GameObject green;

    [SerializeField]
    private bool isVisibleOnStart;

    private bool isStarted = false;

    public void SetSound(bool isSound)
    {
        player.IsSound = isSound;
        UpdateSound();
    }

    public void Hide()
    {
        if (!isVisibleOnStart)
        {
            gameObject.GetComponent<Image>().enabled = false;
            green.SetActive(false);
        }
    }

    public void Show()
    {
        if (!isVisibleOnStart)
        {
            gameObject.GetComponent<Image>().enabled = true;
            green.SetActive(player.IsSound);
        }
    }


    void UpdateSound()
    {
        sound.enabled = player.IsSound;
        
        if (isVisibleOnStart || (isStarted && !isVisibleOnStart))
            green.SetActive(player.IsSound);

        isStarted = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSound();
    }

}
