using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 20211002 14:00 I think it may be better to call it "CHALLENGE"
/// </summary>
public class MissionManager : MonoSingleton<MissionManager>
{
    [Header("DB")]
    [SerializeField, ReadOnly]
    List<Mission> MissionPrefabs = new List<Mission>();

    [Header("Bindings")]

    [SerializeField] 
    GameObject _missionButton;

    Transform _missionContainer;
    
    [Header("Var")]

    [ReadOnly]
    public List<Mission> ActiveMissions = new List<Mission>();

    [ReadOnly]
    public int NowSec = 0;

    public static int SolvedMissions = 0;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _missionContainer = _missionButton.transform.parent;
        _missionButton.transform.parent = transform.root;
        _missionButton.SetActive(false);

        var ms  = Resources.LoadAll("Missions");
        foreach(var m in ms)
        {
            MissionPrefabs.Add(((GameObject)m).GetComponent<Mission>());
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SolvedMissions = 0;
        StartCoroutine(Tick());
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F7))
        {
            StartMission();
        }
    }

    IEnumerator Tick()
    {
        int lastMission = -12;
        while(true)
        {
            float chance = Random.Range(0.8f, 1.11f) 
                * GameManager.Instance.Difficulty.Challenge_BasePercent 
                * Mathf.Pow(0.8f, lastMission+GameManager.Instance.Difficulty.Challenge_BaseInterval-NowSec);
            
            if(lastMission < 0 && NowSec > 3)
                chance = chance * 1.9487f + 0.048763f;
            else if(NowSec <= 3)
                chance = 0.01f;
            
            float fate = Random.Range(0f, 1f);
            if(fate <= chance)
            {
                print($"<color=yellow>DT={-lastMission+NowSec}  MISSION CHANCE = {chance*100}%</color> FATE="+fate*100);
                StartMission();
                lastMission = NowSec;
            }
            else
            {
                print($"DT={-lastMission+NowSec}  MISSION CHANCE = {chance*100}%");
            }
            yield return new WaitForSeconds(1);
            NowSec++;
        }
    }

    public void StartMission()
    {
        Mission finalMission = null;
        for(int attempt = 0; attempt < 1000; attempt++)
        {
            var m = MissionPrefabs[Random.Range(0, MissionPrefabs.Count)];            
            if(ActiveMissions.Contains(m))
                continue;
            else
                finalMission = m;
        }


        if(finalMission != null)
        {
            var mm = Instantiate(finalMission.gameObject).GetComponent<Mission>();
            ActiveMissions.Add(mm);
            RefreshButtons();
            print($"Add challenge #{mm.Id} / {mm}");
            mm.ReadInfo();
        }
        else
        {
            Debug.LogError("bad");
        }
    }

    public void RemoveMission(Mission mission)
    {
        ActiveMissions.Remove(mission);
        RefreshButtons();
    }

    void RefreshButtons()
    {
        for(int i = 0; i < _missionContainer.childCount; i++)
        {
            Destroy(_missionContainer.GetChild(i).gameObject);
        }

        // sort fatalness
        ActiveMissions.Sort((s1, s2) => (int)s2.Fatalness - (int)s1.Fatalness);

        // print
        foreach(var m in ActiveMissions)
        {
            var v = Instantiate(_missionButton, _missionContainer);
            v.SetActive(true);
            v.GetComponent<Button>().onClick.AddListener(()=>m.ReadInfo());
            v.transform.GetChild(0).GetComponent<TMP_Text>().text = m.Title;
            
            Color color = Color.white;
            switch(m.Fatalness)
            {
                case Fatalness.Information: color = Color.white; break;
                case Fatalness.Warning: color = Color.yellow; break;
                case Fatalness.Danger: color = Color.red; break;
                case Fatalness.Fatal: color = Color.magenta; break;
            }
            v.GetComponent<Image>().color = color;
        }
    }
    
}