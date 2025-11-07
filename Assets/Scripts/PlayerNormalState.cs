using UnityEngine;

public class PlayerNormalState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log("Jugador en estado NORMAL");
        player.SetInvulnerableVisual(false);
    }

    public void UpdateState(PlayerController player)
    {
        // Aquí no hace falta nada por ahora
    }

    public void OnTriggerEnter(PlayerController player, Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Time.timeScale = 0f;
            player.gameOverPanel.SetActive(true);
        }
    }
}