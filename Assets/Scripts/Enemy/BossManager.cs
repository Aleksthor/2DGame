using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour, IDataPersistence
{
    public static BossManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {

        }
    }



#pragma warning disable 414
    [Header("Goblin Small Boss")]
    [Header("Boss")]
    [SerializeField] public bool goblinSmallBossDead = false;
    [SerializeField] public GameObject goblinSmallBoss;
    [SerializeField] public Vector2 goblinSmallBossLocation;
    [Header("Other Objects Attached")]
    [SerializeField] public GameObject rock;
    [SerializeField] public Vector2 rockLocation;
#pragma warning restore 414

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "FirstArena":
                if (goblinSmallBossDead != true)
                {
                    GameObject GoblinSmallBoss = Instantiate(goblinSmallBoss, goblinSmallBossLocation, transform.rotation);
                    GoblinSmallBoss.GetComponent<RemoveItemOnDeath>().objectToRemove = Instantiate(rock, rockLocation, transform.rotation);
                    
                }
                
                break;

            default:
                break;
        }

    }



    // DataPersistence

    public void LoadData(GameData data)
    {
        goblinSmallBossDead = data.goblinSmallBossDead;
    }

    public void SaveData(ref GameData data)
    {
        data.goblinSmallBossDead = goblinSmallBossDead;
        
    }


}
