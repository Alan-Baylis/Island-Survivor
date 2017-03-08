using UnityEngine;
using System.Collections;

public static class MeshGenerator {

	public static MeshData GenerateTerrainMesh(float [,] heightMap, float heightMultiplier, AnimationCurve heigthCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        //make the mesh be centralized
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;
       
        //starting triangle and vertice vectors to add values later
        MeshData meshData = new MeshData(width, height);

        int vertexIndex = 0;

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                // x and z are the same value, but the Y axis represents the HEIGHT, so it will be the same as calculated before in the PerlinNoise
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heigthCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                //the percentage - where it is in relation to the map
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                // the vertices on the corner do not have squares to render
                if(x < width - 1 && y < height - 1)
                {
                    // 2 triangles, one square...
                    meshData.addTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.addTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);

                }

                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    // Each square has 4 vertices
    public Vector3[] vertices;
    // Each triangle has 2 squares
    public int[] triangles;
    // One for each vertice - to create texture : Where it in in relation to the map as a % between 0 - 1
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        // vertices in this map
        vertices = new Vector3[meshHeight * meshWidth];

        /* triangles in this map
            
            for every 3 vertices in a row there are 2 squares, so if vertices = 3 : squares = 2
            1 square = 2 triangles
            each triangle has 3 vertices, so 2 * 3 for one square
        */
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];

        uvs = new Vector2[meshHeight * meshWidth];
    }

    // Add triangle in the vector
    public void addTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;

        triangleIndex += 3;
    }

    // Get this mesh
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        //to fix light
        mesh.RecalculateNormals();

        return mesh;
    }
}
