using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class MobController : MonoBehaviour
{
    [Header("Mob movement")]
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Vector3 destination;

    [SerializeField]
    private float minWaitTime = 0.5f;

    [SerializeField]
    private float maxWaitTime = 2f;

    [SerializeField]
    private float distDestination = .4f;

/*
    [Header("Player Detection")]

    [SerializeField]
    private float detectZone = 5f;

    [SerializeField]
    private bool playerDetected = false;*/

    [Header("Dying")]
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject particle;

    [Header("Debug")]

    [SerializeField]
    private bool isMoving = true;

    [SerializeField]
    private bool isHooked = false;

    private SpawnerManager spawnerManager;
    private GameObject player;
    
    private void Start() 
    {
        // Getting the spawner manager instance
        spawnerManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Desactivating the ragdoll
        Rigidbody[] childs = transform.GetComponentsInChildren<Rigidbody>();
        foreach (var child in childs) {
            child.useGravity = false;
        }

        Collider[] colChilds = transform.GetComponentsInChildren<Collider>();
        foreach (var child in colChilds) {
            child.enabled = false;
        }
        this.GetComponent<Collider>().enabled = true;

        destination = this.transform.position;
    }


    void Update()
    {
        if (!isHooked) {
            // If the mob is near it's destination we stop it for a certain time and give a new destination
            if (agent.remainingDistance < distDestination && isMoving) {
                isMoving = false;
                agent.isStopped = true;
                agent.ResetPath();
                StartCoroutine(WaitingTillNextMove());
            }
            animator.SetFloat("Speed", agent.velocity.magnitude/agent.speed);

            /*if (!playerDetected) {
                // If the player is in the detect zone of the mob
                if (Vector3.Distance(player.transform.position, this.transform.position) <= detectZone) {
                    //playerDetected = true;
                    Debug.Log("Player detected by "+this.name);
                }
            }*/
        }
    }

    /// <summary>
    /// Waiting coroutine before mob can move
    /// </summary>
    IEnumerator WaitingTillNextMove()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        agent.isStopped = false;
        GetNewDestination();
    }

    /// <summary>
    /// Give the mob a new destination to go
    /// </summary>
    private void GetNewDestination()
    {
        if (!isHooked) {
            destination = Random.insideUnitSphere * 15f;
            destination += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(destination, out hit, 15f, NavMesh.AllAreas)) {
                destination = hit.position;
                agent.SetDestination(destination);
                transform.LookAt(destination);
                isMoving = true;
            }
        }
    }

    /// <summary>
    /// Disable the Agent, only when grabbed by player
    /// </summary>
    public void Grab()
    {
        StopCoroutine(WaitingTillNextMove());
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
        isHooked = true;
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// Enable the ragdoll mode, only when ungrabbed by player
    /// </summary>
    public void Ungrab()
    {
        this.GetComponent<CapsuleCollider>().isTrigger = true;
        Rigidbody[] childs = transform.GetComponentsInChildren<Rigidbody>();
        foreach (var child in childs) {
            child.useGravity = true;
        }

        Collider[] colChilds = transform.GetComponentsInChildren<Collider>();
        foreach (var child in colChilds) {
            child.enabled = true;
        }
    }


    /// <summary>
    /// Checking if the mob collide with the ground
    /// </summary>
    /// <param name="other">Collider which whom the mobs collide</param>
    private void OnTriggerEnter(Collider other)
    {
        // TODO: Call this on each member of the ragdoll..
        if (other.CompareTag("Ground") && isHooked) { // If we touch the ground layer, then destroy
            Rigidbody[] childs = transform.GetComponentsInChildren<Rigidbody>();
            foreach (var child in childs) {
                child.useGravity = false;
            }
            this.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(Dying());
        }
    }

    /// <summary>
    /// public function to kill the mob
    /// </summary>
    public void kill()
    {
        StartCoroutine(Dying());
    }

    /// <summary>
    /// Corroutine to play the mob death animation
    /// </summary>
    IEnumerator Dying()
    {
        body.SetActive(false);
        particle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Shows the detecting area of the mob in the scene editor
    /// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, detectZone);
        Gizmos.DrawCube(destination, new Vector3(.1f, 1f, .1f));
    }
}
