using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Leap.Unity.Interaction;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject[] MainMenuGameObjects;

    [SerializeField]
    private GameObject[] ExitGameObjects;

    [SerializeField]
    private GameObject ExitButton;

    [SerializeField]
    private GameObject[] WonGameObjects;

    [Header("Settings")]
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private InteractionSlider volumeSlider;

    [SerializeField]
    private TextMeshPro volumeText;

    [SerializeField]
    private InteractionSlider resolutionSlider;

    [SerializeField]
    private TextMeshPro resolutionText;

    private Resolution[] resolutions = { new Resolution { width = 1920, height = 1080 }, new Resolution { width = 1280, height = 720 }, new Resolution { width = 800, height = 600 } };


    private void Start() 
    {
        // Hide all the GameObjects needed for the won
        foreach (var gameObject in WonGameObjects) {
            gameObject.SetActive(false);
        }

        // Show all the GameObjects needed for the UI
        MainMenuGameObjects[0].SetActive(true);
    }

    /// <summary>
    /// Function to launch the game, called by a button on the menu
    /// </summary>
    public void Play()
    {
        // Hide all the GameObjects needed for the won
        foreach (var gameObject in WonGameObjects) {
            gameObject.SetActive(false);
        }

        // Hide all the UI GameObjects
        MainMenuGameObjects[0].SetActive(false);

        UICancelExit();

    }

    /// <summary>
    /// Function to stop the game, called by a button on the menu
    /// </summary>
    public void Stop()
    {
        // Show all the UI GameObjects
        MainMenuGameObjects[0].SetActive(true);
    }

    /// <summary>
    /// Function to exit the game, called by a button on the menu
    /// </summary>
    public void UIExit()
    {
        ExitButton.SetActive(false);
        foreach (var gameObject in ExitGameObjects) {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Function to cancel the exit
    /// </summary>
    public void UICancelExit()
    {
        ExitButton.SetActive(true);
        // Hide all the UI GameObjects
        foreach (var gameObject in ExitGameObjects) {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Function called when the game is won
    /// </summary>
    public void Win() 
    {
        // Show all the GameObjects needed for the won
        foreach (var gameObject in WonGameObjects) {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Function to open the settings menu, called by a button
    /// </summary>
    public void OpenSettings() 
    {
        // Hide the Main Menu GameObject
        MainMenuGameObjects[2].SetActive(false);

        // Show the Settings Menu GameObject
        MainMenuGameObjects[3].SetActive(true);
    } 

    /// <summary>
    /// Function to close the settings menu, called by a button
    /// </summary>
    public void CloseSettings() 
    {
        // Hide the Settings Menu GameObject
        MainMenuGameObjects[3].SetActive(false);

        // Show the Main Menu GameObject
        MainMenuGameObjects[2].SetActive(true);
    }

    /// <summary>
    /// Function to change the volume in the game
    /// </summary>
    public void SetVolume()
    {
        Debug.Log("Volume: "+ volumeSlider.HorizontalSliderValue);
        audioMixer.SetFloat("Volume", volumeSlider.HorizontalSliderValue);
        volumeText.text = "Volume: " + volumeSlider.HorizontalSliderValue * 100;
    }

    /// <summary>
    /// Function to put the game in fullscreen
    /// </summary>
    public void SetFullscreen()
    {
        Debug.Log("Fullscreen ? " + Screen.fullScreen);
        Screen.fullScreen = !Screen.fullScreen;
    }

    /// <summary>
    /// Function to change the resolution of the game
    /// </summary>
    public void SetResolution()
    {
        Debug.Log("Resolution: " + resolutions[resolutionSlider.horizontalStepValue - 1].width + "x" + resolutions[resolutionSlider.horizontalStepValue - 1].height);
        Resolution resolution = resolutions[resolutionSlider.horizontalStepValue - 1];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionText.text = "RESOLUTION: " + resolution.width + "x" + resolution.height;
    }
}