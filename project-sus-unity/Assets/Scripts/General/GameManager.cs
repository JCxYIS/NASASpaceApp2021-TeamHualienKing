using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{    
    public List<Character> Characters;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }


    public void LandingSceneOk()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void SelectedCharacters(List<Character> characters)
    {
        Characters = characters;
        StopAllCoroutines();
        StartCoroutine(LoadMainScene());
    }

    public IEnumerator LoadMainScene()
    {
        var load = SceneManager.LoadSceneAsync("Main");
        LoadingScreen.SetContext("Now Loading Main Scene");
        while(!load.isDone)
        {
            LoadingScreen.SetProgress(load.progress);            
            yield return load;
        }

        LoadingScreen.SetProgress(1);
    }
}