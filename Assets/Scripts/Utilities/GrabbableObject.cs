using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] [Tooltip("Le rigidbody de l'objet")]
    private Rigidbody rb;

    [SerializeField] [Tooltip("Le collider de l'objet")]
    private Collider col;

    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
    }

    /// <summary>
    /// Fonction appelé quand l'objet est attrapé, enlève toutes les contraintes de physique
    /// </summary>
    public void Grab()
    {
        // Debug.Log("Object grabbed");
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = false;
        col.isTrigger = true;
    }

    /// <summary>
    /// Fonction appelé quand l'objet est relaché, remet les contraintes de physique
    /// </summary>
    public void Ungrab()
    {
        //Debug.Log("Object Ungrabbed");
        rb.useGravity = true;
        col.isTrigger = true;
    }

    /// <summary>
    /// Fonction pour vérifier la collision avec un autre objet
    /// </summary>
    /// <param name="other">L'objet avec lequel on est entré en collision</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob")) {
            other.GetComponent<MobController>().kill();

            // TODO: ajouté les animations de destruction d'objet
            Destroy(this.gameObject, 0.5f);
        } else if (other.gameObject.layer == 6) { // Si on touche le layer du sol, destruction de l'objet (Probablement à enlever)
            // TODO: ajouté les animations de destruction d'objet
            col.isTrigger = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
