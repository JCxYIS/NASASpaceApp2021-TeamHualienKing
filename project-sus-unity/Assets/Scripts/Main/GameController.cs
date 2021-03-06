using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    [Header("Bindings")]

    /// <summary>
    /// Characters that player controls
    /// </summary>
    public List<Player> Players;

    [SerializeField] PlayerButton[] _playerButtons;

    [SerializeField] Cinemachine.CinemachineVirtualCamera _virtualCam;



    [Header("Runtime")]
    /// <summary>
    /// Which character is the player now controlling?
    /// </summary>
    [ReadOnly]
    public int ControllingPlayerIndex = 0;

    [ReadOnly]
    public float Time = 0;

    /// <summary>
    /// progress
    /// </summary>
    public float Progress => Time / GameManager.Instance.Difficulty.Game_Length;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // init players
        for(int i = 0; i < GameManager.Instance.Characters.Count; i++)
        {
            Players[i].Init(GameManager.Instance.Characters[i]);
            _playerButtons[i].Init(Players[i], i);
        }
        SwitchPlayer(0);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // input handler
        Vector2 moveCmd = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        for(int i = 0; i < Players.Count; i++) // player movement
        {
            if(i == ControllingPlayerIndex)
            {
                Players[i].PlayerController.SetCommand(moveCmd);
                Players[i].IsControlling = true;
            }
            else
            {
                Players[i].PlayerController.SetCommand(Vector2.zero);
                Players[i].IsControlling = false;
            }
        }
        for(int i = 0; i <= 9; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0 + i + 1))
            {
                SwitchPlayer(i);
            }
        }

        // update time
        Time += UnityEngine.Time.deltaTime;

        // victory condition
        if(Progress >= 1)
        {
            Victory();
        }
        
        // cheat
        if(Input.GetKeyDown(KeyCode.F9))
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
                Victory();
            else
                GameOver();
        }
    }

    /// <summary>
    /// Singel player: Change the player control
    /// </summary>
    public void SwitchPlayer(int index)
    {
        if(index >= Players.Count)
            index = 0;
        else if(index < 0)
            index = Players.Count - 1;
        
        ControllingPlayerIndex = index;
        _virtualCam.Follow = Players[index].transform;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }
}