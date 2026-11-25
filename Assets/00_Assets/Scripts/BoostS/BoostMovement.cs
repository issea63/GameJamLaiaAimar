using UnityEngine;

public class BoostMovement : BoostStats
{
    public PlayerMovement playerMovement;
    int randomBoost;

    private void Start()
    {
        randomBoost = Random.Range(0, 2);
        if(randomBoost>0)
        {
            boostAmount = 3;
            textSet = ("Speed + " + boostAmount);
        }
        else
        {
            boostAmount = 2;
            textSet = ("Jump Force + " + boostAmount);
        }
        playerMovement = PlayerMovement.Instance;
    }
    private void Update()
    {
        if(playerMovement == null)
        {
            playerMovement = PlayerMovement.Instance;
        }
    }
    public override void ApplyBoost()
    {
        base.ApplyBoost();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement.Instance es null.");
            return;
        }
        boostAmount = Random.Range(1, 4);
        if (randomBoost > 0)
        {
            boostAmount = 3;
            playerMovement.SetStat(ref playerMovement.speed, boostAmount);
            boostAmount = 0;
            BoostCanvas.Instance.UpdateText(1, "<b>Speed:</b> " + playerMovement.speed);
        }
        else
        {
            boostAmount = 2;
            playerMovement.SetStat(ref playerMovement.jumpForce, boostAmount);
            boostAmount = 0;
            BoostCanvas.Instance.UpdateText(2, "<b>Jump Force:</b> " + playerMovement.jumpForce);
        }

        gameObject.SetActive(false);
    }

}