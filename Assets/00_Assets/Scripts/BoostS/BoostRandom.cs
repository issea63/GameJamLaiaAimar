using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BoostRandom : MonoBehaviour
{
    [SerializeField] GameObject[] boostPrefabs;
    [SerializeField] GameObject canvasBoost;
    [SerializeField] GameObject canvasBoostPrefab;
    private void Start()
    {
        canvasBoost = GameObject.Find("StatsCanvas");
        if (canvasBoost == null)
        {
            Instantiate(canvasBoostPrefab);
            canvasBoostPrefab.GetComponent<BoostCanvas>().TurnOnTexts();
        }
        boostPrefabs = GameObject.FindGameObjectsWithTag("Boost");
        AddBoostScript();
    }
    public void AddBoostScript()
    {
        foreach (GameObject boost in boostPrefabs)
        {
            int randomIndex = UnityEngine.Random.Range(0, 3);
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
