using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : SingletonMonoBehaviour<LoadingScreen>
{
    public GameObject loadingScreen;

    private void Start()
    {
        loadingScreen = HUDSingleton.instance.transform.Find("LoadingPanel").gameObject;
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            // TODO - Fill Slider etc.

            yield return null;
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

        loadingScreen.SetActive(false);
    }
}
