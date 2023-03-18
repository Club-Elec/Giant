using UnityEngine;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    [Header("Mobs settings")]
    [SerializeField] [Tooltip("Liste des spawners pour les mobs")]
    private List<GameObject> mobsSpawners = new List<GameObject>();

    [SerializeField] [Tooltip("Nombre de mobs par vague")]
    private int[] waveMobsAmount;

    [SerializeField] [Tooltip("Prefab du mob")] 
    private GameObject mob;

    [Header("Objects Settings")]
    [SerializeField] [Tooltip("Liste des spawners pour les objets")]
    private List<GameObject> objectsSpawners = new List<GameObject>();

    [SerializeField] [Tooltip("Liste des objets à spawn")]
    private List<GameObject> objects = new List<GameObject>();


    [Header("Debug")]
    [SerializeField] [Tooltip("Vague actuelle")]
    private int wave = 0;

    [SerializeField] [Tooltip("Jeu en route ?")]
    private bool isPlaying = false;
    public int Wave { get => wave; }

    private GameManager gameManager; // Référence vers le GameManager

    private void Start() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void Update() 
    {
        // Vérifier si tous les mobs de la vague sont tués
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Mob");
        if (monsters.Length == 0) {
            GenerateWave();
        }

    }

    /// <summary>
    /// Généré les mobs et les objets de la vague
    /// </summary>
    void GenerateWave()
    {
        if (!isPlaying) {
            return;
        }

        // Vérification de si le joueur a gagné
        if (wave == waveMobsAmount.Length) {
            gameManager.Win();
            return;
        }

        // Génération des mobs
        for (int i = 0; i < waveMobsAmount[wave]; i++) {
            Instantiate(mob, GetRandomPosition("Mob", mobsSpawners), new Quaternion());
        }
        wave++;

        // Génération des objets
        if (GameObject.FindGameObjectsWithTag("GrabableObjects").Length == 0) {
            int objectsAmount = Random.Range(2, objectsSpawners.Count - 1);
            Debug.Log(objectsAmount);
            List<GameObject> objectsSpawnersUsed = new List<GameObject>(objectsSpawners);
            for (int i = 0; i < objectsAmount; i++) {
                Instantiate(objects[Random.Range(0, objects.Count - 1)], GetRandomPosition("Object", objectsSpawnersUsed), new Quaternion());
            }
        }
    }


    /// <summary>
    /// Génère une position aléatoire dans une zone de spawn d'un spawner
    /// </summary>
    /// <param name="spawnerType">Type de spawner</param>
    /// <param name="spawners">Liste de spawner</param>
    /// <returns>Position aléatoire</returns>
    public Vector3 GetRandomPosition(string spawnerType, List<GameObject> spawners = null)
    {
        Vector3 position = Vector3.zero;

        // Spaghetti time
        if (spawnerType == "Mob") {
            int spawnerId = Random.Range(0, spawners.Count - 1);
            Vector3 randomCircle = Random.insideUnitCircle * spawners[spawnerId].GetComponent<Spawner>().SpawningZone;
            position = spawners[spawnerId].transform.position + randomCircle;

        } else if (spawnerType == "Object") {
            int spawnerId = Random.Range(0, spawners.Count - 1);
            Vector3 randomCircle = Random.insideUnitCircle * spawners[spawnerId].GetComponent<Spawner>().SpawningZone;
            position = spawners[spawnerId].transform.position + randomCircle;
            position.y = 1f;
            spawners.RemoveAt(spawnerId);

        } else {
            Debug.LogError("Spawner type not found");
            return Vector3.zero;
        }

        return position;
    }

    /// <summary>
    /// Fonction pour lancer le système de spawn
    /// </summary>
    public void Play()
    {
        isPlaying = true;
    }

    /// <summary>
    /// Fonction pour arrêter le système de spawn et détruire les mobs et objets
    /// </summary>
    public void Stop()
    {
        isPlaying = false;
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Mob");
        foreach (var monster in monsters)
        {
            Destroy(monster);
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("GrabableObjects");
        foreach (var obj in objects)
        {
            Destroy(obj);
        }

    }

    /// <summary>
    /// Fonction pour réinitialiser le numéro de vague actuelle
    /// </summary>
    public void ResetWave()
    {
        wave = 0;
    }
}
