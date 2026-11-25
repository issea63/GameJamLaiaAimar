using UnityEngine;

public class BoostStats : Stats
{
    public string textSet;
    public float boostAmount = 5f;
    public AudioSource boostObtained;
    public virtual void ApplyBoost()
    {
        boostObtained = gameObject.GetComponent<AudioSource>();
        boostObtained.Play();
        Debug.Log("No hay info");
    }
}
