using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int vida = 10;
    public int dañoColision = 1;
    public NavMeshAgent agent;
    public Transform player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("SetDestination", 5f, 1f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            vida -= dañoColision;
            Destroy(other.gameObject);
            if (vida <= 0)
            {
                Muerte();
            }
        }
    }

    void Muerte()
    {
        Debug.Log("¡El objeto ha muerto!");
        Destroy(this.gameObject);

    }
    public void SetDestination()
    {
        agent.destination = player.position;
    }
}

