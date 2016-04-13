using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Maze : MonoBehaviour {
    
    //Maze Properties
    [Range(0,50)]
    public int mazeWidth = 20, mazeHeight = 20;
    public int[,] mazeCells;

    //Generation Properties
    public enum GenerationType { NewestFirst, Random, NewestRandom, NewestOldest, OldestRandom };
    public GenerationType generationType = GenerationType.NewestFirst;
    BaseGenerator generator;

    [Range(0, 100)]
    public int splitPercent = 25;
    public bool generated = false;

    //Seeding
    public string seed;
    public bool useRandomSeed = true;
    public System.Random rand;

    //Meshing
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    //Camera
    Camera mainCamera;

    //Rendering Properties
    public enum RenderType { Normal };
    public RenderType renderType = RenderType.Normal;
    new BaseRenderer renderer;

	void Awake () {
        //Mesh Initialization
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        //Seeding Initialization
        if (useRandomSeed)
            seed = System.DateTime.Now.ToString();
        rand = new System.Random(seed.GetHashCode());

        //Camera Initialization
        mainCamera = Camera.main;

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
                renderer = new PlainRenderer();
                break;
        }
    }

    void SelectGenerator()
    {
        switch(generationType)
        {
            case GenerationType.NewestFirst:
                generator = new NewestFirstGenerator(this);
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

    void Start()
    {
        mainCamera.transform.position = new Vector3(mazeWidth/2, mazeHeight/2, -1);
        mainCamera.orthographicSize = (mazeWidth > mazeHeight) ? mazeWidth/2 : mazeHeight/2;

    }
	
	void Update () {
	    if(!generated)
        {
            StartCoroutine(generator.Generate());
            BuildMesh();
        }
	}

    void BuildMesh()
    {
        renderer.Render(mazeCells);
        meshFilter.sharedMesh = renderer.ToMesh(meshFilter.sharedMesh);
        meshCollider.sharedMesh = meshFilter.sharedMesh;
    }
}
