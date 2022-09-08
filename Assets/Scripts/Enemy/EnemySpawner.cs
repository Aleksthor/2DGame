using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    [SerializeField] List<GameObject> enemies;
    public int numerOfEnemies;
    public bool enemiesDead;


    // Start is called before the first frame update
    void Start()
    {


        float X = transform.position.x + Random.Range(-2f, 2f);
        float Y = transform.position.y + Random.Range(-2f, 2f);

        for (int i = 0; i < numerOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemyToSpawn, new Vector2(X, Y), transform.rotation);
            enemies.Add(newEnemy);
            newEnemy.transform.parent = gameObject.transform;
        }

        StartCoroutine("CheckEnemies");
        
    }

    public void ResetPosition()
    {

        float X = transform.position.x + Random.Range(-2f, 2f);
        float Y = transform.position.y + Random.Range(-2f, 2f);
        for (int i = enemies.Count - 1; i > -1; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
            else
            {
                enemies[i].transform.position = new Vector2(X, Y);
                enemies[i].GetComponent<LocalEnemyScript>().health = enemies[i].GetComponent<LocalEnemyScript>().maxHealth;
            }
            
        }

    }

    IEnumerator CheckEnemies()
    {
        //Debug.Log(gameObject);
        if (enemies.Count > 0)
        {
            for (int i = enemies.Count - 1; i > -1;i--)
            {
                
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
                else
                {
                   
                }
            }
        }
        else
        {
            enemiesDead = true;
        }


        yield return new WaitForSeconds(0.05f);

        StartCoroutine("CheckEnemies");
    }




  
}
