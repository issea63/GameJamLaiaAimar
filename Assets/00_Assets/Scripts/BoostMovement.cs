using UnityEngine;

public class BoostMovement : BoostStats
{
    public PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = PlayerMovement.Instance;
    }
    private void Update()
    {
        if(playerMovement == null)
        {
            playerMovement = PlayerMovement.Instance;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMovement playerMovement = PlayerMovement.Instance;

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement.Instance es null.");
            return;
        }

        playerMovement.SetStat(ref playerMovement.speed, boostAmount);
        gameObject.SetActive(false);
    }
}