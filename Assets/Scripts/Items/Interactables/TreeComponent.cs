using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TreeData
{
    public TreeState treeState;
    public string uniqueID;
}
public enum TreeState
{
    Chopped,
    Grown
};

public class TreeComponent : MonoBehaviour, IDataPersistence
{

    public Item item;
    public int health;
    public int maxHealth;
    public int min;
    public int max;

    public TreeData treeData;


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

    public void SaveData(GameData data)
    {
        //bool found = false;
        //TreeData dataToRemove = null;
        //foreach(TreeData trees in data.trees)
        //{
        //    if (trees.uniqueID == treeData.uniqueID)
        //    {
        //        dataToRemove = trees;
        //        found = true;
        //    }
        //}
        //if(found)
        //{
        //    data.trees.Remove(dataToRemove);
        //    data.trees.Add(treeData);
        //}
        //else
        //{
        //    data.trees.Add(treeData);
        //}
       
        //found = false;
    }

    public void LoadData(GameData data)
    {


        //foreach (TreeData trees in data.trees)
        //{
        //    if (trees.uniqueID == treeData.uniqueID)
        //    {

        //        treeData = trees;

        //    }
        //}

    }
}
