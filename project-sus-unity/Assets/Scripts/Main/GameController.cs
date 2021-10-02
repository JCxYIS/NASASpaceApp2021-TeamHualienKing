using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoSingleton<GameController>
{
    [Header("Bindings")]

    /// <summary>
    /// Characters that player controls
    /// </summary>
    [SerializeField] Player[] _players;

    [SerializeField] PlayerButton[] _playerButtons;

    [SerializeField] Cinemachine.CinemachineVirtualCamera _virtualCam;


    [Header("Runtime")]
    /// <summary>
    /// Which character is the player now controlling?
    /// </summary>
    [ReadOnly]
    public int ControllingPlayerIndex = 0;



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
            _players[i].Init(GameManager.Instance.Characters[i]);
            _playerButtons[i].Init(_players[i], i);
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
        for(int i = 0; i < _players.Length; i++) // player movement
        {
            if(i == ControllingPlayerIndex)
                _players[i].PlayerController.SetCommand(moveCmd);
            else
                _players[i].PlayerController.SetCommand(Vector2.zero);
        }
    }

    /// <summary>
    /// Singel player: Change the player control
    /// </summary>
    public void SwitchPlayer(int index)
    {
        if(index >= _players.Length)
            index = 0;
        else if(index < 0)
            index = _players.Length - 1;
        
        ControllingPlayerIndex = index;
        _virtualCam.Follow = _players[index].transform;
    }
}