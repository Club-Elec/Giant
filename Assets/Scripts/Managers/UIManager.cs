using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Leap.Unity.Interaction;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] [Tooltip("Un array de GameObjects à activer pour le menu principal")]
    private GameObject[] MainMenuGameObjects; 

    [SerializeField] [Tooltip("Un array de GameObjects à activer pour le menu de sortie")]
    private GameObject[] ExitGameObjects;

    [SerializeField] [Tooltip("Un bouton pour ouvrir le menu de sortie du jeu")]
    private GameObject ExitButton;

    [SerializeField] [Tooltip("Un array de GameObjects à activer quand le jeu est gagné")]
    private GameObject[] WonGameObjects;

    [Header("Settings")]
    [SerializeField] [Tooltip("Le mixeur audio pour changer le volume")]
    private AudioMixer audioMixer;

    [SerializeField] [Tooltip("Le slider pour changer le volume")]
    private InteractionSlider volumeSlider;

    [SerializeField] [Tooltip("Le texte pour afficher le volume")]
    private TextMeshPro volumeText;

    [SerializeField] [Tooltip("Le slider pour changer la résolution")]
    private InteractionSlider resolutionSlider;

    [SerializeField] [Tooltip("Le texte pour afficher la résolution")]
    private TextMeshPro resolutionText;

    // Un array des résolutions possibles
    private Resolution[] resolutions = { new Resolution { width = 1920, height = 1080 }, new Resolution { width = 1280, height = 720 }, new Resolution { width = 800, height = 600 } };


    private void Start() 
    {
        // Cacher tous les GameObjects du menu de victoire
        foreach (var gameObject in WonGameObjects) {
            gameObject.SetActive(false);
        }

        // Afficher tous les GameObjects du menu principal
        MainMenuGameObjects[0].SetActive(true);
    }

    /// <summary>
    /// Fonction pour lancer le jeu, appelée par un bouton du menu
    /// </summary>
    public void Play()
    {
        // Cacher tous les GameObjects du menu de victoire
        foreach (var gameObject in WonGameObjects) {
            gameObject.SetActive(false);
        }

        // Cacher tous les GameObjects du menu principal
        MainMenuGameObjects[0].SetActive(false);

        UICancelExit();

    }

    /// <summary>
    /// Fonction pour arrêter le jeu, appelée par un bouton du menu
    /// </summary>
    public void Stop()
    {
        // Afficher tous les objets du menu principal
        MainMenuGameObjects[0].SetActive(true);
    }

    /// <summary>
    /// Fonction pour quitter le jeu, appelée par un bouton du menu
    /// </summary>
    public void UIExit()
    {
        ExitButton.SetActive(false);
        foreach (var gameObject in ExitGameObjects) {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Fonction pour annuler la sortie du jeu
    /// </summary>
    public void UICancelExit()
    {
        ExitButton.SetActive(true);

        // Cacher tous les objets du menu de sortie
        foreach (var gameObject in ExitGameObjects) {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Fonction appelée quand le jeu est gagné
    /// </summary>
    public void Win() 
    {
        // Afficher tous les objets du menu de victoire
        foreach (var gameObject in WonGameObjects) {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Fonction pour ouvrir le menu de paramètres, appelée par un bouton
    /// </summary>
    public void OpenSettings() 
    {
        // Cacher le menu principal (Jouer / Paramètres)
        MainMenuGameObjects[2].SetActive(false);

        // Afficher le menu de paramètres
        MainMenuGameObjects[3].SetActive(true);
    } 

    /// <summary>
    /// Fonction pour fermer le menu de paramètres, appelée par un bouton
    /// </summary>
    public void CloseSettings() 
    {
        // Cacher le menu de paramètres
        MainMenuGameObjects[3].SetActive(false);

        // Afficher le menu principal (Jouer / Paramètres)
        MainMenuGameObjects[2].SetActive(true);
    }

    /// <summary>
    /// Fonction pour changer le volume dans le jeu
    /// </summary>
    public void SetVolume()
    {
        //Debug.Log("Volume: "+ volumeSlider.HorizontalSliderValue);
        audioMixer.SetFloat("Volume", volumeSlider.HorizontalSliderValue);
        volumeText.text = "Volume: " + volumeSlider.HorizontalSliderValue * 100;
    }

    /// <summary>
    /// Fonction pour mettre le jeu en plein écran
    /// </summary>
    public void SetFullscreen()
    {
        Debug.Log("Fullscreen ? " + Screen.fullScreen);
        Screen.fullScreen = !Screen.fullScreen;
    }

    /// <summary>
    /// Fonction pour changer la résolution du jeu
    /// </summary>
    public void SetResolution()
    {
        //Debug.Log("Resolution: " + resolutions[resolutionSlider.horizontalStepValue - 1].width + "x" + resolutions[resolutionSlider.horizontalStepValue - 1].height);
        Resolution resolution = resolutions[resolutionSlider.horizontalStepValue - 1];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionText.text = "RESOLUTION: " + resolution.width + "x" + resolution.height;
    }
}