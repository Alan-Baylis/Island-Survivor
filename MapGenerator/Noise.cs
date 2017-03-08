using UnityEngine;
using System.Collections;

/// <summary>
/// This class creates the noise map that will be used in unity to generate random maps
/// The map is created here and the MapGenerator.cs starts it, then the display pass the values to a plane with the new texture
/// The height is created in the actual mesh, where the positions of     every triangle are set up in the same height as the noise map (times) the height scale
/// </summary>
public static class Noise {

	public static float [,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int seed,  int octaves, float persistance, float lacunarity, Vector2 offset)
    {

        // Pseudo random number - the same seed will return the same value
        System.Random prng = new System.Random (seed);

        Vector2[] octaveOffsets = new Vector2[octaves];

        // more octaves = more details | the "wave" change more
        for(int i = 0; i < octaves; i++)
        {
            //offset can be used to add randomness or to save the last position from the player in this current seed
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        //the actual noise map
        float[,] noiseMap = new float [mapWidth, mapHeight];

        //max and min height
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        //to zoom in the center
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        if (scale <= 0)
            scale = 0.0001f;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                //starting noiseHeight
                float noiseHeight = 0;

                for(int i = 0; i < octaves; i++)
                {
                    // more scale = more zoom
                    // adding the octave var to enable more changes
                    //frequency starts at 1 and increase each octave
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;
          
                    //the function return a value between 0 and 1, with the aditional will return between -1 and 1
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    //amplitude starts at 1 and increase each octave
                    noiseHeight += perlinValue *= amplitude;

                    //decrease each octave | less persistance = bigger areas
                    amplitude *= persistance;
                    //increase each octave | more lacunarity = more changes
                    frequency *= lacunarity;
                }

                
                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;

                
                

                noiseHeight = islandRules(noiseHeight, x, y, 0);
                noiseHeight = islandRules(noiseHeight, x, y, 1);

                //raw noise map, with numbers between -1 and 1
                noiseMap[x, y] = noiseHeight;
            }
        }

        //returning the values to the range of 0 - 1
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

    // making the map looks like an island || more close to the borders = less height
    static float islandRules(float currentHeight, int x, int y, int time)
    {
        int decrease = 0;

       if(time == 0)
        {
            if (x < 100 && y < 25)
            {
                decrease = 25 - y;
                return currentHeight - (decrease / 10f);
            }
            
            if (x < 100 && y > 75)
            {
                decrease = y - 75;
                return currentHeight - (decrease / 10f);
            }
        }
        else
        {
            
            if (x < 25 && y < 100)
            {
                decrease = 25 - x;
                return currentHeight - (decrease / 10f);
            }

            if (x > 75 && y < 100)
            {
                decrease = x - 75;
                return currentHeight - (decrease / 10f);
            }
            

        }


        return currentHeight;
    }
}
