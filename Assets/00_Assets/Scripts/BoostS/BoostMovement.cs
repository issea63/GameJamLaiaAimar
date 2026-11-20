using UnityEngine;

public class BoostMovement : BoostStats
{
    public PlayerMovement playerMovement;

    public AudioSource boostObtained;

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
        boostObtained.Play();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement.Instance es null.");
            return;
        }
        boostAmount = Random.Range(1, 4);
        int selectBoost = Random.Range(0, 2);
        if(selectBoost > 0)
        {
            playerMovement.SetStat(ref playerMovement.speed, boostAmount);
            BoostCanvas.Instance.UpdateText(1, "Speed: " + playerMovement.speed);
        }
        else
        {
            playerMovement.SetStat(ref playerMovement.jumpForce, boostAmount);
            BoostCanvas.Instance.UpdateText(2, "Jump Force: " + playerMovement.jumpForce);
        }

        gameObject.SetActive(false);
    }
}