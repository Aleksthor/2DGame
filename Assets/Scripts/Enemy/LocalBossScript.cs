using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBossScript : MonoBehaviour
{
    public enum BossName
    {
        GoblinSmallBoss,
        GoblinArcMage

    };

    public BossName bossName;


    [Header("Reward")]
    [Header("Chest")]
    [SerializeField] public GameObject Chest;
    [SerializeField] public Vector2 chestLocation;
    [Header("High Chance Reward")]
    [SerializeField] public List<Item> highChanceItem = new List<Item>();
    [SerializeField] public float highChance = 80f;
    [Header("Mid Chance Reward")]
    [SerializeField] public List<Item> midChanceItem = new List<Item>();
    [SerializeField] public float midChance = 80f;
    [Header("Low Chance Reward")]
    [SerializeField] public List<Item> lowChanceItem = new List<Item>();
    [SerializeField] public float lowChance = 80f;



    public void Die()
    {
        switch (bossName)
        {
            case BossName.GoblinSmallBoss:
                BossManager.Instance.goblinSmallBossDead = true;
                break;
            case BossName.GoblinArcMage:
                BossManager.Instance.goblinArcMageDead = true;
                break;

            default:
                break;

        }
    }

    public void SpawnChest()
    {
        GameObject chest = Instantiate(Chest, chestLocation, transform.rotation);
        ChestComponent chestComponent = chest.GetComponent<ChestComponent>();

        float random1 = Random.Range(1, 100);
        float random2 = Random.Range(1, 100);
        float random3 = Random.Range(1, 100);

        // Low Chance Items
        if (random1 > lowChance)
        {
            if (lowChanceItem != null)
            {
                if (lowChanceItem.Count > 0)
                {
                    foreach (Item item in lowChanceItem)
                    {
                        chestComponent.items.Add(item);
                    }
                }
            }
        }
        // Mid Chance Items
        if (random2 > midChance)
        {
            if (midChanceItem != null)
            {
                if (midChanceItem.Count > 0)
                {
                    foreach (Item item in midChanceItem)
                    {
                        chestComponent.items.Add(item);
                    }
                }
            }
        }
        // High Chance Items
        if (random3 > highChance)
        {
            if (highChanceItem != null)
            {
                if (highChanceItem.Count > 0)
                {
                    foreach (Item item in highChanceItem)
                    {
                        chestComponent.items.Add(item);
                    }
                }
            }
        }
    }

}



