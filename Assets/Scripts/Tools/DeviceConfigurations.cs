using UnityEngine;

/// <summary>
/// Some instructions to configure user device
/// </summary>

public class DeviceConfigurations : MonoBehaviour
{

    float width=0;
    float height=0;


    void SaveDimensions()
    {
        width = Screen.width;
        height = Screen.height;
    }

    void UpdateCamera()
    {
        float aspectRatio = width / height;
        Camera.main.orthographicSize = 5.1f * (2/aspectRatio);
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveDimensions();
        UpdateCamera();

        Screen.sleepTimeout = SleepTimeout.NeverSleep; // Don't turn off display
        Application.targetFrameRate = 30;  // test, Limit FPS to battery safe
    }

    private void Update()
    {
        if (width != Screen.width || height != Screen.height)
        {
            SaveDimensions();
            UpdateCamera();
        }
    }

}
