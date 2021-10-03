using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class BADEND : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        button.enabled = false;

        #if UNITY_WEBGL
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "challenger_disaster_cut.mp4");
        videoPlayer.url = filePath;
        #endif

        videoPlayer.Play();
        for(int i = 5; i > 0; i--)
        {
            buttonText.text = $"You can skip in {i}...";
            yield return new WaitForSeconds(1);
        }

        buttonText.text = $"Skip Video";
        button.enabled = true;
    }


    public void SkipAd()
    {
        PromptBoxSettings pbs = null;
        pbs = new PromptBoxSettings{
            Title = "Catastrophic Failure",
            Content = "This was the part of Challenger disaster on January 28, 1986.\n"+
                    "Due to the failure of the O-ring seal of the solid rocket thruster, the entire shuttle was disintegrated in the end and all crew members were killed.\n"+
                    "However, this is not only a accident, but also an result caused by neglecting safety.\n"+
                    "Hope that all space missions in future will pay more attention to safety and never make the same mistake again.",
            ConfirmButtonText = "OK",
            CancelButtonText = "Learn More...",
            ConfirmCallback = ()=>{
                SceneManager.LoadScene("Landing");
            },
            CancelCallback = ()=>{
                PromptBox.Create(pbs);
                Application.OpenURL("https://history.nasa.gov/rogersrep/v1ch5.htm");
            },
        };

        PromptBox.Create(pbs);
    }
}
