using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour, IDataPersistence
{
    public static BossManager instance;

    [Header("Goblin Small Boss")]
    [Header("Boss")]
    [SerializeField] public bool goblinSmallBossDead = false;
    [SerializeField] public GameObject goblinSmallBoss;
    [SerializeField] public Vector2 goblinSmallBossLocation;
    [Header("Other Objects Attached")]
    [SerializeField] public GameObject rock;
    [SerializeField] public Vector2 rockLocation;



    [Header("Goblin Arc Mage")]
    [Header("Boss")]
    [SerializeField] public bool goblinArcMageDead = false;
    [SerializeField] public GameObject goblinArcMage;
    [SerializeField] public Vector2 goblinArcMagePosition;



    private IEnumerator coroutine;

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
        StartCoroutine(SpawnBoss(scene.name));

    }


    IEnumerator SpawnBoss(string sceneName)
    {
        yield return new WaitForSeconds(0.3f);
        switch (sceneName)
        {
            case "FirstArena":
                if (goblinSmallBossDead != true)
                {
                    GameObject GoblinSmallBoss = Instantiate(goblinSmallBoss, goblinSmallBossLocation, transform.rotation);
                    GoblinSmallBoss.GetComponent<RemoveItemOnDeath>().objectToRemove = Instantiate(rock, rockLocation, transform.rotation);
                }
                break;
            case "GoblinCave":
                if (goblinArcMageDead != true)
                {
                    Instantiate(goblinArcMage, goblinArcMagePosition, transform.rotation);
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
        goblinArcMageDead = data.goblinArcMageDead;
    }

    public void SaveData(ref GameData data)
    {
        data.goblinSmallBossDead = goblinSmallBossDead;
        data.goblinArcMageDead = goblinArcMageDead;
    }


}
