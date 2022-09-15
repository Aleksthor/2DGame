using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityDashBehindEnemy : Ability
{

    private GameObject[] enemies;
    private Camera mainCam;
    private Movement movement;

    public float closest = 150;

 

    public override void Activate(GameObject parent)
    {
        
        parent.GetComponent<PolygonCollider2D>().enabled = false;
        movement = FindObjectOfType<Movement>();
        movement.iFrames = true;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy);
            float distance = Vector2.Distance(enemy.transform.position, mainCam.ScreenToWorldPoint(Input.mousePosition));
            
            if (distance < closest)
            {
                closest = distance;
                closestEnemy = enemy;
            }
            
        }
        Debug.Log(closestEnemy);
        if (closestEnemy != null)
        {
            if (closestEnemy.GetComponent<EnemySpriteDirection>() != null)
            {
                if (closestEnemy.GetComponent<EnemySpriteDirection>().flipLastDirection)
                {
                    parent.transform.position = new Vector2(closestEnemy.transform.position.x - 1.5f, closestEnemy.transform.position.y);
                }
                else
                {
                    parent.transform.position = new Vector2(closestEnemy.transform.position.x + 1.5f, closestEnemy.transform.position.y);
                }
            }
            if (closestEnemy.GetComponent<MageSpriteDirection>() != null)
            {
                if (closestEnemy.GetComponent<MageSpriteDirection>().flipLastDirection)
                {
                    parent.transform.position = new Vector2(closestEnemy.transform.position.x - 1.5f, closestEnemy.transform.position.y);
                }
                else
                {
                    parent.transform.position = new Vector2(closestEnemy.transform.position.x + 1.5f, closestEnemy.transform.position.y);
                }
            }

            GameEvents.current.LowerPlayerOpacity();
            
        }


    }


    public override void DeActivate(GameObject parent)
    {
        parent.GetComponent<PolygonCollider2D>().enabled = true;
        closest = 150f;
        movement.iFrames = false;
        GameEvents.current.NormalPlayerOpacity();
    }
}
