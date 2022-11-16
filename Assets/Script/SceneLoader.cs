using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    private static string sceneToLoad;

    public static string SceneToLoad {get => sceneToLoad;}

    //Load
   public static void Load (string sceneName) {
        SceneManager.LoadScene(sceneName);
   }

    //Load dengan progress
    public static void ProgressLoad (string sceneName) {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingProgress");
   }

    //ReloadLevel
    public static void ReloadLoad () {
        var currentScene = SceneManager.GetActiveScene().name;
        ProgressLoad(currentScene);
   }

    //LoadNextLevel
    public static void LoadNextLevel () {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int nextLevel = int.Parse(currentSceneName.Split("Level")[1] + 1);
        string nextSceneName = "Level" + nextLevel;

        if(SceneUtility.GetBuildIndexByScenePath(nextSceneName) == - 1) {
            Debug.LogError(nextSceneName + "Doesn't Exists");
            return;
        }

        ProgressLoad(nextSceneName);
   }

}
