using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class MobController : MonoBehaviour
{
    [Header("Mouvement du Mob")]
    [SerializeField] [Tooltip("Le NavMeshAgent du mob")]
    private NavMeshAgent agent;

    [SerializeField] [Tooltip("L'Animator du mob")]
    private Animator animator;

    [SerializeField] [Tooltip("Le temps minimum d'attente avant de bouger")]
    private float minWaitTime = 0.5f; 

    [SerializeField] [Tooltip("Le temps maximum d'attente avant de bouger")]
    private float maxWaitTime = 2f;

    [SerializeField] [Tooltip("La distance à laquelle le mob considère qu'il est arrivé à destination")]
    private float distDestination = .4f;

/*
    [Header("Player Detection")]

    [SerializeField]
    private float detectZone = 5f;

    [SerializeField]
    private bool playerDetected = false;*/

    [Header("Dying")]
    [SerializeField] [Tooltip("Gameboject contenant le mesh du mob")]
    private GameObject body;
    [SerializeField] [Tooltip("Gameboject contenant les particules de mort du mob")]
    private GameObject particle;

    [Header("Debug")]

    [SerializeField] [Tooltip("La destination du mob")]
    private Vector3 destination;

    [SerializeField] [Tooltip("Mob en mouvement ?")]
    private bool isMoving = true;

    [SerializeField] [Tooltip("Mob attrapé par le joueur ?")]
    private bool isHooked = false;

    private SpawnerManager spawnerManager; // Référence du SpawnerManager
    private GameObject player; // Référence du joueur
    
    private void Start() 
    {
        spawnerManager = GameObject.FindGameObjectWithTag("SpawnerManager").GetComponent<SpawnerManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Désactivation du ragdoll
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
            // Si le mob est proche de sa destination, on l'arrête un certain temps et on lui donne une nouvelle destination
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

        // Si le mob est sous le sol, on le tue
        if (this.transform.position.y < -5f) {
            StartCoroutine(Dying());
        }
    }

    /// <summary>
    /// Coroutine d'attente avant que le mob puisse bouger
    /// </summary>
    IEnumerator WaitingTillNextMove()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        agent.isStopped = false;
        GetNewDestination();
    }

    /// <summary>
    /// Donner une destination au mob
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
    /// Desactive le NavMeshAgent, seulement quand le mob est attrapé par le joueur
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
    /// Activer le ragdoll, seulement quand le mob est relaché par le joueur
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
    /// Vérification si le mob touche le sol
    /// </summary>
    /// <param name="other">Collider avec qui le mob est entré en collision</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") && isHooked) { // Si le mob touche le sol alors on le tue
            Rigidbody[] childs = transform.GetComponentsInChildren<Rigidbody>();
            foreach (var child in childs) {
                child.useGravity = false;
            }
            this.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(Dying());
        }
    }

    /// <summary>
    /// Fonction publique pour tuer le mob
    /// </summary>
    public void kill()
    {
        StartCoroutine(Dying());
    }

    /// <summary>
    /// Coroutine pour jouer l'animation de mort du mob
    /// </summary>
    IEnumerator Dying()
    {
        body.SetActive(false);
        particle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Affiche la zone de détection du mob & la destination dans l'éditeur de scène
    /// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, detectZone);
        Gizmos.DrawCube(destination, new Vector3(.1f, 1f, .1f));
    }
}
