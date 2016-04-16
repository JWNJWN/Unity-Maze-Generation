using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGame : MonoBehaviour {

    public Text levelText;

    public Button mainMenuButton;
    public Button resetLevelButton;

    public Slider widthSlider;
    public Slider heightSlider;

    public Text personalBest;
    public Text currentTime;

    // Use this for initialization
    void Start()
    {
        levelText.text = "Level " + (PlayerPrefs.GetInt("CurrentMazeID"));

        mainMenuButton.onClick.AddListener(delegate { ChangeScene(0); });
        resetLevelButton.onClick.AddListener(delegate { ChangeScene(1); });
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        personalBest.text = "Best: " + PlayerPrefs.GetFloat("LevelBestTime");
        currentTime.text = PlayerPrefs.GetFloat("LevelCurrentTime") + " :Time";
    }

    public void ChangeScene(int sceneValue)
    {
        Application.LoadLevel(sceneValue);
    }
}
