using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class GameManager : MonoSingleton<GameManager>
{    
    private List<Character> _charcters = new List<Character>{Character.Doctor, Character.Engineer, Character.Pilot};
    public List<Character> Characters => _charcters;
    public Difficulty Difficulty = new Difficulty();


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
        if(Input.GetKeyDown(KeyCode.F9) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerPrefs.DeleteAll();
        }
        if(Input.GetKeyDown(KeyCode.F9))
        {
            PlayerPrefs.SetInt("MAX_DIFF", 888);
        }
    }


    public void LandingSceneOk(Difficulty difficulty)
    {
        Difficulty = difficulty;
        SceneManager.LoadScene("CharacterSelect");
    }

    public void SelectedCharacters(List<Character> characters, bool forceBadEnd)
    {
        _charcters = characters;
        StopAllCoroutines();

        int playCount = PlayerPrefs.GetInt("GAME_COUNT", 0);
        playCount++;
        PlayerPrefs.SetInt("GAME_COUNT", playCount);
        float rdn = Random.value;
        print("RDN="+rdn+", PLAYCOUNT="+playCount);
        if( (rdn < 0.9f && playCount != 2 && !forceBadEnd) || playCount == 1)
            StartCoroutine(LoadMainScene("Main"));
        else
            StartCoroutine(LoadMainScene("BAD END"));
    }

    public IEnumerator LoadMainScene(string sceneName)
    {
        var load = SceneManager.LoadSceneAsync(sceneName);
        // load.allowSceneActivation = false;
        LoadingScreen.SetContext("Now Loading...");
        while(!load.isDone)
        {
            LoadingScreen.SetProgress(load.progress);            
            yield return load;
        }

        // load.allowSceneActivation = true;
        LoadingScreen.SetProgress(1);
    }
}