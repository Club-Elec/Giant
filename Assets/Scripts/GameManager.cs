using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] PlayGameObjects;

    private SpawnerManager spawnerManager;
    private UIManager UImanager;

    private void Start() 
    {
        // Hide all the GameObjects needed for the game
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(false);
        }
        
        UImanager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        spawnerManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
    }


    /// <summary>
    /// Function to stop the game, called by a button on the menu
    /// </summary>
    public void Stop()
    {
        UImanager.Stop();
        spawnerManager.Stop();

        // Hide all the GameObjects needed for the game
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(false);
        } 
    }

    /// <summary>
    /// Function to reset the game to it's default state
    /// </summary>
    public void Play()
    {
        UImanager.Play();

        // Show all the GameObjects needed for the game
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(true);
        }

        spawnerManager.ResetWave();
        spawnerManager.Play();
    }

    /// <summary>
    /// Function called when the game is won
    /// </summary>
    public void Win() 
    {
        spawnerManager.Stop();

        // Hide all the GameObjects needed for the game
        foreach (var gameObject in PlayGameObjects) {
            gameObject.SetActive(false);
        }

        UImanager.Win();
    }
}
