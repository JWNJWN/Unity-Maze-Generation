using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class GameStateManager : MonoBehaviour {

    public static  GameStateManager gameStateManager;

    public static List<Level> gameLevels;

    public static TextAsset levelFile;

    public struct Level
    {
        public int id;
        public string name;
        public string seed;
        public int width;
        public int height;
        public Maze.GenerationType generationType;
        public float personalBest;

        public override string ToString()
        {
            return "ID: " + id + " Name: " + name + " Seed: " + seed + " Width: " + width + " Height: " + height;
        }
    }
    
    public enum GameStates { MainMenu, LevelSelect, Play, Complete };
    public GameStates gameState = GameStates.MainMenu;

    public Level currentLevel;

    public static bool levelsFetched = false;

    void Awake()
    {
        gameStateManager = this;

        gameLevels = new List<Level>();

        levelFile = Resources.Load("Levels") as TextAsset;
        GetLevels();

        GameObject.DontDestroyOnLoad(this);
    }



    void Update()
    {

        GetLevels();

        if (gameStateManager != this)
            Destroy(this.gameObject);

        CheckState();
    }

    void CheckState()
    {
        switch(gameState)
        {
            case GameStates.MainMenu:
                if (!levelsFetched)
                    GetLevels();
                break;
            case GameStates.LevelSelect:
                break;
            case GameStates.Play:
                break;
            case GameStates.Complete:
                UpdateCurrentLevel();
                Setlevels();
                levelsFetched = false;
                Application.LoadLevel(0);
                break;
        }
    }

    private void UpdateCurrentLevel()
    {
        gameLevels[currentLevel.id - 1] = currentLevel;
    }

    public void LoadLevel(Level level)
    {
        currentLevel = level;
        Application.LoadLevel(1);
    }

    private static void Setlevels()
    {
        XmlWriterSettings setting = new XmlWriterSettings { Indent = true, IndentChars = "  ", NewLineOnAttributes = true, OmitXmlDeclaration = true};

        using (XmlWriter writer = XmlWriter.Create("Assets/Resources/Levels.xml", setting))
        {

            writer.WriteStartDocument();
            writer.WriteStartElement("Levels");
            foreach(Level level in gameLevels)
            {
                writer.WriteStartElement("Level");

                writer.WriteElementString("id", level.id.ToString());
                writer.WriteElementString("name", level.name);
                writer.WriteElementString("seed", level.seed);
                writer.WriteElementString("width", level.width.ToString());
                writer.WriteElementString("height", level.height.ToString());
                writer.WriteElementString("generationType", level.generationType.ToString());
                writer.WriteElementString("personalBest", level.personalBest.ToString());

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    private static void GetLevels()
    {
        levelsFetched = true;

        XmlDocument levelDoc = new XmlDocument();
        levelDoc.LoadXml(levelFile.text);
        XmlNodeList levelsList = levelDoc.GetElementsByTagName("Level");

        foreach (XmlNode levelInfo in levelsList)
        {
            XmlNodeList levelContent = levelInfo.ChildNodes;
            Level tempLevel = new Level();
            foreach (XmlNode levelItems in levelContent)
            {
                switch (levelItems.Name)
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
                    case "generationType":
                        switch (levelItems.InnerText)
                        {
                            case "NewestFirst":
                                tempLevel.generationType = Maze.GenerationType.NewestFirst;
                                break;
                            case "NewestOldest":
                                tempLevel.generationType = Maze.GenerationType.NewestOldest;
                                break;
                            case "NewestRandom":
                                tempLevel.generationType = Maze.GenerationType.NewestRandom;
                                break;
                            case "OldestRandom":
                                tempLevel.generationType = Maze.GenerationType.OldestRandom;
                                break;
                            case "Random":
                                tempLevel.generationType = Maze.GenerationType.Random;
                                break;
                        }
                        break;
                    case "personalBest":
                        tempLevel.personalBest = float.Parse(levelItems.InnerText);
                        break;
                }
            }
            gameLevels.Add(tempLevel);
        }
    }
}
