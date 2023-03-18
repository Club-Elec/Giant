using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] [Tooltip("La zone de spawn autour du spawner")]
    private float spawningZone = 3f;

    [SerializeField] [Tooltip("La couleur du Gizmo")]
    private Color32 customColor;

    public float SpawningZone { get => spawningZone; }

    /// <summary>
    /// Afficher la zone de spawn dans l'éditeur de scène
    /// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.color = customColor;
        Gizmos.DrawWireSphere(transform.position, spawningZone);
    }
}
