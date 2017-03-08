using UnityEngine;
using System.Collections;

public class CreateMapColl : MonoBehaviour {

    public MeshFilter meshFilter;

    // create collider in the map
    public void CreateColl()
    {
        GetComponent<MeshCollider>().sharedMesh = null;
        GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
    }

}
