using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Collider col;

    void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
    }

    /// <summary>
    /// Function called when the object is grabbed, remove all the physics constraints
    /// </summary>
    public void Grab()
    {
        Debug.Log("Object grabbed");
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = false;
        col.isTrigger = true;
    }

    /// <summary>
    /// Function called when the object is throwed, setting the object to collider mode
    /// </summary>
    public void Ungrab()
    {
        Debug.Log("Object Ungrabbed");
        rb.useGravity = true;
        col.isTrigger = true;
    }

    /// <summary>
    /// Collision checking function
    /// </summary>
    /// <param name="other">The object that enter collision with this object</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob")) {
            other.GetComponent<MobController>().kill();
            // Adding animation & graphics of crate destroy
            Destroy(this.gameObject, 0.5f);
        } else if (other.gameObject.layer == 6) { // If we touch the ground layer, then destroy
            // Adding animation & graphics of crate destroy
            col.isTrigger = false;
            Destroy(this.gameObject, 0.5f);
        }
    }
}
