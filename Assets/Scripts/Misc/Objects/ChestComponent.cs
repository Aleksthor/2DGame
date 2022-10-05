using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestComponent : MonoBehaviour
{
    
    public List<GameObject> items = new List<GameObject>();
    private Animator animator;
    private bool open = false;
#pragma warning disable 414
    private float openTime = 0f;
#pragma warning restore 414
    private GameObject playerObject;
    public float ChestRange = 2f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerObject = PlayerSingleton.instance.gameObject;
    }

    private void Update()
    {


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
        foreach(GameObject item in items)
        {
            float x = Random.Range(-2, 2);
            float y = Random.Range(-2, 2);
            GameObject spawnableItem = Instantiate(item, new Vector2(transform.position.x+x , transform.position.y+y), transform.rotation);

        }
    }


}
