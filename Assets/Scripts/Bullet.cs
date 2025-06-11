using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private float lifetime = 5f;
    private float speed = 25f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
