using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectActivator : MonoBehaviour
{
    // Variables
    [SerializeField] private float renderDistance;

    private GameObject playerObject;
    public List<GameObject> activatorObjects;

    void Awake()
    {
        activatorObjects = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerObject = PlayerSingleton.instance.gameObject;
        StartCoroutine("CheckActivation");
    }

    IEnumerator CheckActivation()
    {

        if (activatorObjects.Count > 0)
        {

            for (int i = activatorObjects.Count - 1; i > -1; i--)
            {
                if (activatorObjects[i].GetComponent<RenderDistance>().isSpawner)
                {
                    if (Vector2.Distance(playerObject.transform.position, activatorObjects[i].transform.position) > renderDistance)
                    {

                        activatorObjects[i].SetActive(false);
                        activatorObjects[i].GetComponent<EnemySpawner>().ResetPosition();

                    }
                    else
                    {

                        activatorObjects[i].SetActive(true);


                    }

                    if (activatorObjects[i].GetComponent<EnemySpawner>().enemiesDead)
                    {
                        activatorObjects[i].SetActive(false);
                    }
                }
                else
                {
                    if (!activatorObjects[i].GetComponent<RenderDistance>().isDead)
                    {
                        if (Vector2.Distance(playerObject.transform.position, activatorObjects[i].transform.position) > renderDistance)
                        {

                            activatorObjects[i].SetActive(false);

                        }
                        else
                        {

                            activatorObjects[i].SetActive(true);


                        }
                    }
                    
                }
            }
        }




        yield return new WaitForSeconds(0.05f);

        StartCoroutine("CheckActivation");
    }


   
}

