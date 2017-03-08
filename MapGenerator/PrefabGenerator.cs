using UnityEngine;
using System.Collections;

public class PrefabGenerator : MonoBehaviour {

    // differents GameObjects
    public GameObject house;
    public GameObject tree;
    
    // "Map translated" size
    int xLength;
    int yLength;

    float topLeftX;
    float topLeftZ;

    AnimationCurve curve;
    float mapHeight;

    System.Random rand;
    int seed;

    // noise map to know the height of the terrain in this current position
    // mapmultiplier and height curve used in the mesh creator, so it'll be used to find the acctually height
    // seed to find the same "random" always
    public void addPrefabs(float[,] noisemap, int seed, float mapHeightMultiplier, AnimationCurve heightCurve)
    {
        //translating the size from noise map
        xLength = noisemap.GetLength(0);
        yLength = noisemap.GetLength(1);

        topLeftX = (xLength - 1) / -2f;
        topLeftZ = (yLength - 1) / 2f;

        mapHeight = mapHeightMultiplier;
        curve = heightCurve;
  
        this.seed = seed;

        // this will create trees in the island
        PlaceTrees(noisemap);  
    }

    // placing trees in the map
    void PlaceTrees(float[,] noiseMap)
    {
        rand = new System.Random((int)noiseMap[50, 50]);

        //number of trees in this map
        int nTree = rand.Next(200);

        for (int i = 0; i < nTree; i++)
        {
            int x = rand.Next(0, xLength - 1);
            int z = rand.Next(0, yLength - 1);

            if (noiseMap[x, z] > 0.31f && noiseMap[x, z] < 0.7f)
            {
                // the mesh creator creates one vertice for each 10 unitys (NEARLY) so, *10
                // since it's getting the position from one vertice, it's important to subtract 10 to make the house touch the floor
                tree.transform.position = new Vector3((topLeftX + x) * 10, curve.Evaluate(noiseMap[x, z]) * mapHeight - 0.25f, (topLeftZ - z) * 10);
                tree.name = "Tree";
                tree.tag = "Tree";

                Instantiate(tree);
            }
        }
    }
    
}

