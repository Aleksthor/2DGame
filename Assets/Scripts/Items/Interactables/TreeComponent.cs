using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeComponent : MonoBehaviour
{
    public Item item;
    public int health;
    public int maxHealth;
    public int min;
    public int max;


    private Slider healthBar;

    private void Start()
    {
        healthBar = transform.Find("Canvas").transform.Find("HealthBar").GetComponent<Slider>();
    }

    private void Update()
    {
        if (health == maxHealth)
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = (float)health / (float)maxHealth;
            
        }
    }

    public void PickupHit()
    {
        health -= 1;
        if (health <= 0)
        {
            transform.GetComponent<RenderDistance>().isDead = true;
            InventoryManager.Instance.AddItemToStack(item, Random.Range(min, max));
            gameObject.SetActive(false);
        }
    }
}
