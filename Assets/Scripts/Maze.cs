using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Maze : MonoBehaviour {
    
    //Maze Properties
    [Range(5,50)]
    public int mazeWidth = 20, mazeHeight = 20;
    public int[,] mazeCells;
    public Vector2 startCell, endCell;


    //Generation Properties
    public enum GenerationType { NewestFirst, Random, NewestRandom, NewestOldest, OldestRandom };
    public GenerationType generationType = GenerationType.NewestFirst;
    BaseGenerator generator;

    [Range(0, 100)]
    public int splitPercent = 25;
    public bool generated = false;
    public bool isSkipping = false;

    //Seeding
    public string seed;
    public bool useRandomSeed = true;
    public System.Random rand;

    //Meshing
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    public Material material;
    public bool rendered = false;

    //Player
    public Camera mainCamera;
    public Player playerScript;
    GameObject playerObject;

    //Rendering Properties
    public enum RenderType { Normal };
    public RenderType renderType = RenderType.Normal;
    new BaseMazeRenderer renderer;
    
    void Awake () {
        Screen.orientation = ScreenOrientation.Portrait;

        //Load Player Prefs
        mazeWidth = PlayerPrefs.GetInt("CurrentMazeWidth");
        mazeHeight = PlayerPrefs.GetInt("CurrentMazeHeight");

        //Mesh Initialization
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        material = meshRenderer.material;

        //Seeding Initialization
        if (useRandomSeed)
            seed = System.DateTime.Now.ToString();
        rand = new System.Random(seed.GetHashCode());

        //Player Initialization
        playerObject = new GameObject();
        playerObject.name = "Player";
        playerScript = playerObject.AddComponent<Player>();
        playerScript.maze = this;

        //Camera Initialization
        mainCamera = Camera.main;
        mainCamera.transform.position = new Vector3(mazeWidth / 2f, mazeHeight / 2f, -1);

        //Generator Initialization
        SelectGenerator();

        //Renderer Initialization
        SelectRenderer();

        //Maze Initialization
        InitializeMaze();
    }

    void InitializeMaze()
    {
        mazeCells = new int[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++) 
        {
            for (int y = 0; y < mazeHeight; y++) 
            {
                mazeCells[x, y] = 0;
            }
        }
    }

    void SelectRenderer()
    {
        switch(renderType)
        {
            case RenderType.Normal:
                renderer = new PlainMazeRenderer(this);
                break;
        }
    }

    void SelectGenerator()
    {
        switch(generationType)
        {
            case GenerationType.NewestFirst:
                generator = new NewestGenerator(this);
                break;
            case GenerationType.Random:
                generator = new RandomGenerator(this);
                break;
            case GenerationType.NewestRandom:
                generator = new NewestRandomGenerator(this, splitPercent);
                break;
            case GenerationType.NewestOldest:
                generator = new NewestOldestGenerator(this, splitPercent);
                break;
            case GenerationType.OldestRandom:
                generator = new OldestRandomGenerator(this, splitPercent);
                break;
        }
    }
	
	void Update () {
        if(!generated)
        {
            if (isSkipping)
            {
                while(true)
                    generator.Generate();
            }
            else
            {
                generator.Generate();
            }
        }
        if(!rendered)
            BuildMesh();
    }

    void BuildMesh()
    {
        renderer.Render();
        meshFilter.sharedMesh = renderer.ToMesh(meshFilter.sharedMesh);
        meshRenderer.material.SetTexture("_MainTex", renderer.GetTexture());
        if (generated)
            rendered = true;
    }
}
