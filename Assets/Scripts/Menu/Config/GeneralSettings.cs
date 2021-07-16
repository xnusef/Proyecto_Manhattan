using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GeneralSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioControl;

    int width;
    int height;
    [SerializeField] int newResolution = 2;
    [SerializeField] bool fullScreen;
    [SerializeField] Text resolutionText;

    void Start()
    {
        Resolutions();
        Apply();
    }

    public void Volume(float sliderValue)
    {
        audioControl.SetFloat("MasterSound", Mathf.Log10(sliderValue) * 20);
    }

    public void NextResolution()
    {
        newResolution++;
        Resolutions();
    }

    public void PreviousResolution()
    {
        newResolution--;
        Resolutions();
    }

    public void FullScreen()
    {
        fullScreen = !fullScreen;
    }

    public void Apply()
    {
        Screen.SetResolution(width, height, fullScreen);
    }

    private void Resolutions()
    {
        newResolution = Mathf.Clamp(newResolution, 0, 7);
        switch (newResolution)
        {
            case 0:
                width = 1024;
                height = 576;
                break;
            case 1:
                width = 1152;
                height = 648;
                break;
            case 2:
                width = 1280;
                height = 720;
                break;
            case 3:
                width = 1366;
                height = 768;
                break;
            case 4:
                width = 1600;
                height = 900;
                break;
            case 5:
                width = 1920;
                height = 1080;
                break;
            case 6:
                width = 2560;
                height = 1440;
                break;
            case 7:
                width = 3840;
                height = 2160;
                break;
        }
        resolutionText.text = width.ToString() + " x " + height.ToString();
    }
}
