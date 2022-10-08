using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string SceneName;
    public Vector2 spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSingleton.instance.transform.position = spawnPosition;
            SceneManagerComponent.Instance.respawnScene = SceneName;
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene(SceneName);
            LoadingScreen.Instance.LoadScene(SceneName);
        }
    }
}
