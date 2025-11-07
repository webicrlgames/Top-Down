using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int vidaMax = 10;
    private int vidaActual;
    public int dañoColision = 1;

    private NavMeshAgent agent;
    private Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        vidaActual = vidaMax;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (agent != null && agent.isOnNavMesh)
        {
            InvokeRepeating(nameof(SetDestination), 0.5f, 1f);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    void Muerte()
    {
        Debug.Log("¡El enemigo murió!");
        gameObject.SetActive(false);
        ScoreManager.Instance.AddScore(1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
        }
    }

    public void SetDestination()
    {
        if (agent != null && agent.isOnNavMesh && player != null)
        {
            agent.destination = player.position;
        }
    }
}