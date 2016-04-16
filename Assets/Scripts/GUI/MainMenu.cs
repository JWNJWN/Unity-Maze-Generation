using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Xml;

public class MainMenu : MonoBehaviour {

    //GUI
    private string currentMenu = "Main";

    //Panels
    public Canvas mainMenu;
    public Canvas levelSelect;

    //Buttons
    public Button playButton;

    public Transform levelObject;

    private List<Transform> levelObjects;

    //Level Loading
    public TextAsset levelFile;

    //FB
    List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
    
    struct Level
    {
        public int id;
        public string name;
        public string seed;
        public int width;
        public int height;
        public float personalBest;

        public override string ToString()
        {
            return "ID: " + id + " Name: " + name + " Seed: " + seed + " Width: " + width + " Height: " + height;
        }
    }

    List<Level> levels;

    void Awake()
    {
        levels = new List<Level>();
        levelObjects = new List<Transform>();

        if(!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }else
        {
            FB.ActivateApp();
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
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
        Screen.orientation = ScreenOrientation.Portrait;
        InitiateButtons();
        GetLevels();
        AddLevelsToUI();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentMenu)
            {
                case "Main":
                    Application.Quit();
                    break;
                case "LevelSelect":
                    currentMenu = "Main";
                    break;
            }
        }
        ChangeMenu();
    }

    void InitiateButtons()
    {
        playButton.onClick.AddListener(delegate { currentMenu = "LevelSelect"; });
    }

    void GetLevels()
    {
        XmlDocument levelDoc = new XmlDocument();
        levelDoc.LoadXml(levelFile.text);
        XmlNodeList levelsList = levelDoc.GetElementsByTagName("Level");

        foreach(XmlNode levelInfo in levelsList)
        {
            XmlNodeList levelContent = levelInfo.ChildNodes;
            Level tempLevel = new Level();
            foreach(XmlNode levelItems in levelContent)
            {
                switch(levelItems.Name)
                {
                    case "id":
                        tempLevel.id = int.Parse(levelItems.InnerText);
                        break;
                    case "name":
                        tempLevel.name = levelItems.InnerText;
                        break;
                    case "seed":
                        tempLevel.seed = levelItems.InnerText;  
                        break;
                    case "width":
                        tempLevel.width = int.Parse(levelItems.InnerText);
                        break;
                    case "height":
                        tempLevel.height = int.Parse(levelItems.InnerText);
                        break;
                }
            }
            levels.Add(tempLevel);
        }
    }

    void AddLevelsToUI()
    {
        foreach(Level level in levels)
        {
            Transform tempLevel = (Transform)Instantiate(levelObject);
            tempLevel.SetParent(levelSelect.transform);
            tempLevel.localScale = new Vector3(1, 1, 1);
            tempLevel.localPosition = new Vector3(-350, 600 - ((level.id - 1) * 200), 0);
            tempLevel.name = level.name;

            Button tempLeaderboardButton = tempLevel.GetChild(0).GetComponent<Button>();
            Button tempLevelButton = tempLevel.GetChild(1).GetComponent<Button>();

            int tempId = level.id;
            tempLeaderboardButton.onClick.AddListener(delegate { Debug.Log("Add Leaderboard GUI PLz Mate"); });
            tempLevelButton.onClick.AddListener(delegate { ChangeScene(tempId); });
        }
    }

    public void ChangeMenu()
    {
        switch(currentMenu)
        {
            case "Main":
                levelSelect.enabled = false;
                mainMenu.enabled = true;
                break;
            case "LevelSelect":
                mainMenu.enabled = false;
                levelSelect.enabled = true;
                break;
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        PlayerPrefs.SetInt("CurrentMazeID", sceneIndex);

        PlayerPrefs.SetInt("CurrentMazeWidth", levels[sceneIndex].width);
        PlayerPrefs.SetInt("CurrentMazeHeight", levels[sceneIndex].width);
        Application.LoadLevel(1);
    }   
}
