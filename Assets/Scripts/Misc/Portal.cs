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
        Debug.Log("Enter");
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneName);
            LoadingScreen.Instance.LoadScene(SceneName);
            PlayerSingleton.instance.transform.position = spawnPosition;
            DataPersistenceManager.instance.SaveGame();
        }
    }
}
