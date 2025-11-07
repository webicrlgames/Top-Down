using UnityEngine;

public class PlayerInvulnerableState : IPlayerState
{
    private float invulnerableTime = 15f;
    private float timer;

    public void EnterState(PlayerController player)
    {
        timer = invulnerableTime;
        Debug.Log("Jugador en estado INVULNERABLE");
        player.SetInvulnerableVisual(true); // efecto visual opcional
    }

    public void UpdateState(PlayerController player)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            player.SwitchState(new PlayerNormalState());
        }
    }

    public void OnTriggerEnter(PlayerController player, Collider other)
    {
        // No pasa nada si toca enemigos
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Jugador invulnerable: enemigo ignorado");
        }
    }
}