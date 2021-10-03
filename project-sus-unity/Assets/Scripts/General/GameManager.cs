using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{    
    private List<Character> _charcters = new List<Character>{Character.Doctor, Character.Engineer, Character.Pilot};
    public List<Character> Characters => _charcters;
    public Difficulty Difficulty;


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
        if(Input.GetKeyDown(KeyCode.F7))
        {
            PlayerPrefs.DeleteAll();
        }
        if(Input.GetKeyDown(KeyCode.F8))
        {
            PlayerPrefs.SetInt("MAX_DIFF", 888);
        }
    }


    public void LandingSceneOk(Difficulty difficulty)
    {
        Difficulty = difficulty;
        SceneManager.LoadScene("CharacterSelect");
    }

    public void SelectedCharacters(List<Character> characters)
    {
        _charcters = characters;
        StopAllCoroutines();
        StartCoroutine(LoadMainScene());
    }

    public IEnumerator LoadMainScene()
    {
        var load = SceneManager.LoadSceneAsync("Main");
        // load.allowSceneActivation = false;
        LoadingScreen.SetContext("Now Loading Main Scene");
        while(!load.isDone)
        {
            LoadingScreen.SetProgress(load.progress);            
            yield return load;
        }

        // load.allowSceneActivation = true;
        LoadingScreen.SetProgress(1);
    }
}