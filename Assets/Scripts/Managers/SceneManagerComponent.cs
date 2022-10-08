using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerComponent : SingletonMonoBehaviour<SceneManagerComponent>, IDataPersistence
{
    private Button respawnButton;
    public string respawnScene;
    private Transform deathScreen;
    private Animator playerAnimator;
    private void Start()
    {
        deathScreen = HUDSingleton.instance.transform.Find("DeathScreen").transform;
        respawnButton = deathScreen.Find("PausePanel").transform.Find("Button").GetComponent<Button>();

        respawnButton.onClick.AddListener(Respawn);

        playerAnimator = PlayerSingleton.instance.GetComponent<Animator>();
    }

    private void Respawn()
    {
        DataPersistenceManager.instance.LoadGame();
        PlayerManager.Instance.Respawn();
        deathScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(respawnScene);
        playerAnimator.SetBool("Dead", false);
    }

    public void SaveData(GameData data)
    {
        data.currentScene = respawnScene;
    }
    public void LoadData(GameData data)
    {
        respawnScene = data.currentScene;
    }
}
