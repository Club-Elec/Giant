using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawningZone = 3f;

    public float SpawningZone { get => spawningZone; }

    /// <summary>
    /// Shows the spawning area in the scene editor
    /// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawningZone);
    }
}
