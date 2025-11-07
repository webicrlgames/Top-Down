using UnityEngine;

public class ShootCommand : ICommand
{
    private PlayerController player;
    private ObjectPool bulletPool;

    public ShootCommand(PlayerController player, ObjectPool pool)
    {
        this.player = player;
        this.bulletPool = pool;
    }

    public void Execute()
    {
        GameObject bullet = bulletPool.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = player.bulletSpawn.position;
            bullet.transform.rotation = player.bulletSpawn.rotation;
            bullet.SetActive(true);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = player.bulletSpawn.forward * 25f;
        }
        else
        {
            Debug.LogWarning("No hay balas disponibles en el pool.");
        }
    }
}