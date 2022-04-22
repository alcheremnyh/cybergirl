using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    [SerializeField]
    private PlayerData player;
    [SerializeField]
    private CanvasGroup Black;
    [SerializeField]
    private CanvasGroup[] Frames;

    [SerializeField]
    private Slider TimeSlider;

    [SerializeField]
    private GameObject ButtonPrevious;
    [SerializeField]
    private GameObject ButtonNext;
    [SerializeField]
    private GameObject ButtonSkip;

    public float secondToStart;
    bool isPlay = false;


    int slideId = 0;

    // Start is called before the first frame update
    void Start()
    {
        player.IsSound = true;
        StartCoroutine(StartPlay());
    }

    void Update()
    {
        if (isPlay && Black.alpha < 1f)
            Black.alpha += 1f * Time.deltaTime;
        else if (isPlay && Frames[slideId].alpha < 1f)
            Frames[slideId].alpha += 1f * Time.deltaTime;
    }

    public void ButtonsUpdate()
    {
        if (slideId < Frames.Length - 1)
            ButtonNext.SetActive(true);
        else
        if (slideId == Frames.Length - 1)
            ButtonNext.SetActive(false);


        if (slideId == 0)
            ButtonPrevious.SetActive(false);
        else if (slideId == 1)
            ButtonPrevious.SetActive(true);
    }

    public void ButtonsPrevious()
    {
        TimeSlider.value = 0;
        slideId--;
        ButtonsUpdate();

        if (slideId < 0)
            ShowSlide();
        else
        {
            Frames[slideId + 1].alpha = 0f;
        }
    }

    public void NextSlide()
    {
        TimeSlider.value = 0;
        slideId++;
        ButtonsUpdate();

        if (slideId < Frames.Length)
            ShowSlide();
        else
        {
            Frames[slideId - 1].alpha = 1f;
        }
    }

    void ShowSlide()
    {
        TimeSlider.value = 0;
        if (slideId > 1)
            Frames[slideId - 2].alpha = 0;
    }

    IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(secondToStart);
        ShowSlide();
        ButtonsUpdate();
        ButtonSkip.SetActive(true);
        isPlay = true;
    }

    public void StartGame()
    {
        GameObject.Find("DarkScreen").GetComponent<FadeInOut>().GotoScene(1);
    }

    private void FixedUpdate()
    {
        if (TimeSlider.value + Time.fixedDeltaTime*1000f > TimeSlider.maxValue)
        {
            TimeSlider.value = TimeSlider.maxValue;
            if (slideId == Frames.Length - 1)
                StartGame();
            else
                NextSlide();
        }
        else
        {
            TimeSlider.value += Time.fixedDeltaTime * 1000f;
        }
    }
}
