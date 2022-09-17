using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityDashBehindEnemy : Ability
{

    private GameObject[] enemies;
    private Camera mainCam;
    private Movement movement;

    private float closest = 150;
    public float range;
    public float damageBoost;
    public GameObject effect;
    public GameObject buffEffect;


    private LayerMask layerMask;
    private int layer = 3;



    public override void Activate(GameObject parent)
    {
        layerMask = (1 << layer);
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

        if (closestEnemy != null)
        {

            if (Vector2.Distance(parent.transform.position, closestEnemy.transform.position) < range)
            {
                GameObject spawnedObject = Instantiate(effect, parent.transform.position, parent.transform.rotation);
                Instantiate(buffEffect, parent.transform);
                spawnedObject.transform.right = closestEnemy.transform.position - parent.transform.position;

                #region Goblin
                if (closestEnemy.GetComponent<GoblinSpriteDirection>() != null)
                {
                    if (closestEnemy.GetComponent<GoblinSpriteDirection>().flipLastDirection)
                    {


                        Vector2 newPos = new Vector2(closestEnemy.transform.position.x - 1.5f, closestEnemy.transform.position.y);
                        Collider2D collider = Physics2D.OverlapCircle(newPos, 0.5f, layerMask);

                        Debug.Log(collider);

                        parent.transform.position = new Vector2(closestEnemy.transform.position.x - 1.5f, closestEnemy.transform.position.y);
                    }
                    else
                    {
                        parent.transform.position = new Vector2(closestEnemy.transform.position.x + 1.5f, closestEnemy.transform.position.y);
                    }
                }
                #endregion

                #region MageGoblin
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
                #endregion

                #region Enemy
                if (closestEnemy.GetComponent<EnemySpriteManager>() != null)
                {
                    if (closestEnemy.GetComponent<EnemySpriteManager>().flipLastDirection)
                    {
                        parent.transform.position = new Vector2(closestEnemy.transform.position.x - 1.5f, closestEnemy.transform.position.y);
                    }
                    else
                    {
                        parent.transform.position = new Vector2(closestEnemy.transform.position.x + 1.5f, closestEnemy.transform.position.y);
                    }
                }
                #endregion

                GameEvents.current.BoostNextAttack(damageBoost);
                GameEvents.current.LowerPlayerOpacity();
                GameEvents.current.PlayerInvisible();
            }



        }


    }


    public override void DeActivate(GameObject parent)
    {
        parent.GetComponent<PolygonCollider2D>().enabled = true;
        closest = 150f;
        movement.iFrames = false;
        GameEvents.current.NormalPlayerOpacity();
        GameEvents.current.PlayerNotInvisible();
        GameEvents.current.DontBoostNextAttack();
    }


    public override void Trigger(GameObject parent)
    {
    }
}
