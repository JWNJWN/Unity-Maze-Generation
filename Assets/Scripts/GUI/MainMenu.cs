using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using Facebook.MiniJSON;

using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

    public GameStateManager gameStateManager;

    //GUI
    private string currentMenu;

    //Panels
    public Canvas mainMenu;
    public Canvas levelSelect;

    //Buttons
    public Button playButton;

    public Transform levelObject;

    private List<Transform> levelObjects;

    //FB
    List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
    

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        gameStateManager = GameStateManager.gameStateManager;

        levelObjects = new List<Transform>();



        /*if(!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }else
        {
            FB.ActivateApp();
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }*/
    }

    private void InitCallback()
    {
        if(FB.IsInitialized)
        {
            FB.ActivateApp();
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        else
        {
            Debug.Log("Failed To Initialize the facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if(FB.IsLoggedIn)
        {
            var aToken = AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);

            foreach (string perm in aToken.Permissions)
                Debug.Log(perm);
        }else
        {
            Debug.Log("User Cancelled Login.");
        }
    }

    void Start()
    {
        InitiateButtons();
        AddLevelsToUI();
    }

    void Update()
    {
        currentMenu = gameStateManager.gameState.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentMenu)
            {
                case "MainMenu":
                    Application.Quit();
                    break;
                case "LevelSelect":
                    currentMenu = "MainMenu";
                    break;
            }
        }
        ChangeMenu();
    }

    void InitiateButtons()
    {
        playButton.onClick.AddListener(delegate { gameStateManager.gameState = GameStateManager.GameStates.LevelSelect; });
    }

    void AddLevelsToUI()
    {
        foreach(GameStateManager.Level level in GameStateManager.gameLevels)
        {
            GameStateManager.Level tempLevel = level;

            Transform tempLevels = (Transform)Instantiate(levelObject);
            tempLevels.SetParent(levelSelect.transform);
            tempLevels.localScale = new Vector3(1, 1, 1);
            tempLevels.localPosition = new Vector3(-350, 600 - ((tempLevel.id - 1) * 200), 0);
            tempLevels.name = tempLevel.name;

            Button tempLeaderboardButton = tempLevels.GetChild(0).GetComponent<Button>();
            Button tempLevelButton = tempLevels.GetChild(1).GetComponent<Button>();

            Text tempLevelButtonText = tempLevelButton.transform.GetChild(0).GetComponent<Text>();
            tempLevelButtonText.text = "Personal Best: \n" + tempLevel.personalBest.ToString();

            tempLeaderboardButton.onClick.AddListener(delegate { Debug.Log("Add Leaderboard GUI PLz Mate"); });
            tempLevelButton.onClick.AddListener(delegate { gameStateManager.LoadLevel(tempLevel); });
        }
    }

    public void ChangeMenu()
    {
        switch(currentMenu)
        {
            case "MainMenu":
                levelSelect.enabled = false;
                mainMenu.enabled = true;
                break;
            case "LevelSelect":
                mainMenu.enabled = false;
                levelSelect.enabled = true;
                break;
        }
    }
}
