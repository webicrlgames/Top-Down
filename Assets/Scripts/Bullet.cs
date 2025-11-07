using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    private float lifetime = 5f;
    private float speed = 25f;

    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Disable), lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.RecibirDaño(damage);
            }

            Disable();
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Bullet"))
        {
            Disable();
        }
    }

    void Disable()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
