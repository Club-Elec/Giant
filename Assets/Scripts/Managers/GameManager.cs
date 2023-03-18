using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Un array de GameObjects à activer lors du lancement du jeu")]
    private GameObject[] PlayGameObjects;

    private SpawnerManager spawnerManager; // Référence vers le SpawnerManager
    private UIManager UImanager; // Référence vers l'UIManager

    private void Start() 
    {
        // Cacher tous les GameObjects nécessaires au jeu
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(false);
        }
        
        UImanager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        spawnerManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
    }


    /// <summary>
    /// Fonction pour arrêter le jeu, appelée par un bouton du menu
    /// </summary>
    public void Stop()
    {
        UImanager.Stop();
        spawnerManager.Stop();

        // Cacher tous les GameObjects nécessaires au jeu
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(false);
        } 
    }

    /// <summary>
    /// Fonction pour réinitialiser le jeu à son état par défaut
    /// </summary>
    public void Play()
    {
        UImanager.Play();

        // Afficher tous les GameObjects nécessaires au jeu
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(true);
        }

        spawnerManager.ResetWave();
        spawnerManager.Play();
    }

    /// <summary>
    /// Fonction appelée lorsque le jeu est gagné
    /// </summary>
    public void Win() 
    {
        spawnerManager.Stop();

        // Cacher tous les GameObjects nécessaires au jeu
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(false);
        }

        UImanager.Win();
    }
}
