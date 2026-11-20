using System;
using Unity.Mathematics;
using UnityEngine;

public class BoostRandom : MonoBehaviour
{
    [SerializeField] GameObject[] boostPrefabs;

    private void Start()
    {
        boostPrefabs = GameObject.FindGameObjectsWithTag("Boost");
        AddBoostScript();
    }
    public void AddBoostScript()
    {
        foreach (GameObject boost in boostPrefabs)
        {
            int randomIndex = UnityEngine.Random.Range(0, 2);
            switch (randomIndex)
            {
                case 0:
                    if (boost.GetComponent<BoostCombat>() == null)
                    {
                        boost.AddComponent<BoostCombat>();
                    }
                    break;
                case 1:
                    if (boost.GetComponent<BoostMovement>() == null)
                    {
                        boost.AddComponent<BoostMovement>();
                    }
                    break;
                case 2:
                    if (boost.GetComponent<BoostFinalLevelProb>() == null)
                    {
                        boost.AddComponent<BoostFinalLevelProb>();
                    }
                    break;
            }
        }
    }
}
