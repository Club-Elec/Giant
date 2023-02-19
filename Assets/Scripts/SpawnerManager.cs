using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private GameObject[] spawners;

    [SerializeField]
    private int[] waveMobsAmount;

    [SerializeField] 
    private GameObject mob;

    [Header("Debug")]
    [SerializeField] 
    private int wave = 0;

    [SerializeField]
    private bool isPlaying = false;
    public int Wave { get => wave; }

    private GameManager gameManager;

    private void Start() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update() 
    {
        // Checking if all the mobs of the wave are killed
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Mob");
        if (monsters.Length == 0) {
            GenerateWave();
        }
    }

    /// <summary>
    /// Generate mobs of the wave
    /// </summary>
    void GenerateWave()
    {
        if (!isPlaying) {
            return;
        }

        if (wave == waveMobsAmount.Length) {
            gameManager.Win();
            return;
        }

        for (int i = 0; i < waveMobsAmount[wave]; i++) {
            Instantiate(mob, GetRandomPosition(), new Quaternion());
        }
        wave++;
    }

    /// <summary>
    /// Generate a random Vector3 position inside one spawning area of one spawner
    /// </summary>
    /// <returns>Random position</returns>
    public Vector3 GetRandomPosition()
    {
        int spawnerId = Random.Range(0, spawners.Length);
        Vector3 randomCircle = Random.insideUnitCircle*spawners[spawnerId].GetComponent<Spawner>().SpawningZone;
        Vector3 position = spawners[spawnerId].transform.position + randomCircle;
        return position;
    }

    /// <summary>
    /// Function to start the spawning system
    /// </summary>
    public void Play()
    {
        isPlaying = true;
    }

    /// <summary>
    /// Function to stop the spawning system
    /// </summary>
    public void Stop()
    {
        isPlaying = false;
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Mob");
        foreach (var monster in monsters)
        {
            Destroy(monster);
        }
    }

    /// <summary>
    /// Function to reset the wave number
    /// </summary>
    public void ResetWave()
    {
        wave = 0;
    }
}
