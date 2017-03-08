using UnityEngine;

using System.Collections;

public class Display : MonoBehaviour {

    //those objects are placed in the inspector

    public Renderer textureRender;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;


    public void DrawTexture(Texture2D texture)
    {
        textureRender.sharedMaterial.mainTexture = texture;
        //setting the texture size
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        //sharedMesh because it's used outside the game mode
        meshFilter.sharedMesh = meshData.CreateMesh();

        // material.texture = texture
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
