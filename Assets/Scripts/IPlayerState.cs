using UnityEngine;

public interface IPlayerState
{
    void EnterState(PlayerController player);
    void UpdateState(PlayerController player);
    void OnTriggerEnter(PlayerController player, Collider other);
}