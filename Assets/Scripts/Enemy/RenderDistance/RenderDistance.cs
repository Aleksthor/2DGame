using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDistance : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawnManager;
    [SerializeField] private ObjectActivator objectActivator;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawnManager = GameObject.Find("Manager").transform.Find("EnemySpawnManager").gameObject;
        objectActivator = enemySpawnManager.GetComponent<ObjectActivator>();

        objectActivator.activatorObjects.Add(gameObject);
    }


}
