using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestComponent : MonoBehaviour
{
    
    public List<Item> items = new List<Item>();
    private Animator animator;
    public GameObject SpawnableItem;
    private bool open = false;
    private float openTime = 0f;
    private GameObject playerObject;
    public float ChestRange = 2f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = PlayerSingleton.instance.gameObject;
    }

    private void Update()
    {
        //if (open)
        //{
        //    openTime -= Time.deltaTime;
        //    if(openTime < 0f)
        //    {
        //        animator.SetTrigger("Close");
        //    }
        //}

        if (Vector2.Distance(playerObject.transform.position,transform.position) < ChestRange && !open)
        {
            if (Input.GetButton("Use"))
            {
                animator.SetTrigger("Open");
                open = true;
                openTime = 2f;
            }
        }

    }

    public void OpenChest()
    {
        foreach(Item item in items)
        {
            float x = Random.Range(-2, 2);
            float y = Random.Range(-2, 2);
            GameObject spawnableItem = Instantiate(SpawnableItem, new Vector2(transform.position.x+x , transform.position.y+y), transform.rotation);
            spawnableItem.GetComponent<ItemGameObject>().item = item;
            spawnableItem.GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        }
    }


}
