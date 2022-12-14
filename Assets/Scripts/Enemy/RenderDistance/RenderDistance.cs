using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDistance : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawnManager;
    [SerializeField] private ObjectActivator objectActivator;
    public bool isSpawner;
    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawnManager = GameObject.Find("EnemySpawnManager").gameObject;
        objectActivator = enemySpawnManager.GetComponent<ObjectActivator>();

        objectActivator.activatorObjects.Add(gameObject);
    }


}
