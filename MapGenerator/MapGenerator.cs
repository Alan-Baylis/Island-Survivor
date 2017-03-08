using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    Display display;
    PrefabGenerator prefabCreator;
    LoadGameGenres loadGenre;

    // the layers start at 8
    public static int mapGameGenre = 8;

    // how the map will be rendered
    public enum DrawMode { NoiseMap, ColourMap, Mesh };
    // class to create the map
    public DrawMode drawMode;

    // map size
    public int mapWidth;
    public int mapHeight;

    public float noiseScale;

    public int octaves;
    [Tooltip("Change aplitude")]
    [Range(0, 1)]
    public float persistance;
    [Tooltip("Change frequency")]
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float mapHeightMultiplier;
    public AnimationCurve animationCurve;

    public bool autoUpdate;

    // regions avaiable in the island
    public TerrainType[] region;

    // texture resolution | more pixels
    public static int resolution = 2;
    public static int quality = 2;



    // random to change the seed
    System.Random rand = new System.Random();

    // script that create a collider
    CreateMapColl mapColl;

    void Awake()
    {

        display = FindObjectOfType<Display>();
        prefabCreator = FindObjectOfType<PrefabGenerator>();
        loadGenre = FindObjectOfType<LoadGameGenres>();

        // initializate the mapcoll script
        mapColl = GameObject.Find("Map").GetComponent<CreateMapColl>();

        // this will generate the game first map
        GenerateMap();

    }

    void Update()
    {
        
    }

    // to regenerate the map, the older map must be deleted along with their gameobjects
    public void RegenerateMap(int lastLayer, int? newLayer)
    { 
       // destroying objects before regenerate the map
        foreach(GameObject g in FindObjectsOfType<GameObject>())
        {
            // destroy every object from last layer
            if(g.layer == lastLayer)
            {
                Destroy(g);
            }
            if (g.tag == "Tree")
            {
                Destroy(g);
            }
        }   

        // generating the new map and changing the seed to generate a new one
        seed = rand.Next();
        if (newLayer == null)
            mapGameGenre = newGameGenre(mapGameGenre); // give a new genre to this game
        else
            mapGameGenre = (int)newLayer;
        PlayerInteractions.gameChange = true;

        GenerateMap();
        
    }

    // the new game genre CAN'T be the same as the last one
    int newGameGenre(int lastGameGenre)
    {
        // can't be 8 again
        int newGenre = 9 + rand.Next(2);

        // can't repeat genres
        while (newGenre == lastGameGenre)
            newGenre = 9 + rand.Next(2);

        return newGenre;
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, seed, octaves, persistance, lacunarity, offset);

        // must be bigger than texture array size
        Color[] colourMap = new Color[(mapWidth * resolution) * (mapHeight * resolution) + 1];

        // adding the color using the current height
        for (int y = 0; y < mapWidth * resolution; y++)
        {
            for (int x = 0; x < mapHeight * resolution; x++)
            {
                float currentHeight = noiseMap[x / resolution, y / resolution];

                for (int i = 0; i < region.Length; i++)
                {
                    if (currentHeight <= region[i].height)
                    {
                        if(mapGameGenre != 9)
                        {
                            colourMap[y * (mapWidth * resolution) + x] = region[i].color;
                        }
                        else
                        {
                            colourMap[y * (mapWidth * resolution) + x] = region[i].shooterColor;
                        }

                        break;
                    }
                }
            }
        }

        colourMap = colourMapSmoothness(colourMap);

        if (drawMode == DrawMode.NoiseMap)                       //draw noise map
        {
            display.DrawTexture(TextureGenerator.textureFromHeightMap(noiseMap));

        }
        else if (drawMode == DrawMode.ColourMap)               //draw colour map
        {
            display.DrawTexture(TextureGenerator.textureFromColourMap(colourMap, mapWidth, mapHeight));

        }
        else if (drawMode == DrawMode.Mesh)                   //draw mesh map
        {
            // passing the mesh and the texture that will be applied
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, mapHeightMultiplier, animationCurve), TextureGenerator.textureFromColourMap(colourMap, mapWidth, mapHeight));
            prefabCreator.addPrefabs(noiseMap, seed, mapHeightMultiplier, animationCurve);
            loadGenre.PerpareNewGenre(mapGameGenre);
        }

        // create coll
        mapColl.CreateColl();
    }

    // turn into a coroutine - maybe
    Color[] colourMapSmoothness(Color[] colourMap)
    {
        //how many times the color will be changed
        for (int times = 0; times < quality; times++)
        {
            // decreasing for
            for (int y = mapWidth * resolution - 1; y > 0; y--)
            {
                for (int x = mapHeight * resolution - 2; x > 0; x--)
                {
                    if (colourMap[y * (mapWidth * resolution) + x] != colourMap[y * (mapWidth * resolution) + x - 1])
                    {
                        colourMap[y * (mapWidth * resolution) + x - 1] = (colourMap[y * (mapWidth * resolution) + x] + colourMap[y * (mapWidth * resolution) + x - 1]) / 2;

                        if (y > 0)
                            colourMap[(y - 1) * (mapWidth * resolution) + x] = (colourMap[(y - 1) * (mapWidth * resolution) + x] + colourMap[y * (mapWidth * resolution) + x]) / 2;

                    }
                }
            }
            // increasing for
            for (int y = 0; y < mapWidth * resolution; y++)
            {
                for (int x = 0; x < mapHeight * resolution - 1; x++)
                {
                    if (colourMap[y * (mapWidth * resolution) + x] != colourMap[y * (mapWidth * resolution) + x + 1])
                    {
                        colourMap[y * (mapWidth * resolution) + x + 1] = (colourMap[y * (mapWidth * resolution) + x] + colourMap[y * (mapWidth * resolution) + x + 1]) / 2;

                        if (y < mapWidth * resolution - 1)
                            colourMap[(y + 1) * (mapWidth * resolution) + x] = (colourMap[(y + 1) * (mapWidth * resolution) + x] + colourMap[y * (mapWidth * resolution) + x]) / 2;

                    }
                }
            }
        }

        return colourMap;
    }

    //called every time something in the inspector change
    void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;

        if (octaves < 0)
            octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
    public Color shooterColor;
}
