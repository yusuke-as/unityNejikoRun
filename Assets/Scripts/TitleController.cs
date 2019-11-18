using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Text highScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        highScoreLabel.text = $"HighScore:{PlayerPrefs.GetInt("HighScore")}m";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
