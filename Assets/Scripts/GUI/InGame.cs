using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGame : MonoBehaviour {

    public GameStateManager gameStateManager;

    public Text levelText;

    public Button mainMenuButton;
    public Button resetLevelButton;

    public Slider widthSlider;
    public Slider heightSlider;

    public Text personalBest;
    public Text currentTime;

    void Awake()
    {
        gameStateManager = GameStateManager.gameStateManager;
    }

    // Use this for initialization
    void Start()
    {
        mainMenuButton.onClick.AddListener(delegate { ChangeScene(0); });
        resetLevelButton.onClick.AddListener(delegate { ChangeScene(1); });
    }

    void Update()
    {
        levelText.text = "Level " + gameStateManager.currentLevel.id;
        UpdateTimer();
    }

    void UpdateTimer()
    {
        personalBest.text = "Best: " + PlayerPrefs.GetFloat(gameStateManager.currentLevel.id + "_PersonalBest");
        currentTime.text = PlayerPrefs.GetFloat("LevelCurrentTime") + " :Time";
    }

    public void ChangeScene(int sceneValue)
    {
        Application.LoadLevel(sceneValue);
    }
}
