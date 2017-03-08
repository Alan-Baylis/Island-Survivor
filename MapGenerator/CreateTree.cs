using UnityEngine;
using System.Collections;

public class CreateTree : MonoBehaviour {

    MeshFilter bodyMeshFilter;
    MeshFilter leavesMeshFilter;

    Mesh leavesMesh;
    Mesh bodyMesh;

    MeshCollider bodyColl;
    MeshCollider leavesColl;

    System.Random rand;

    void Awake()
    {
        //creating colliders
        bodyColl = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        leavesColl = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;

        // starting body mesh
        // get by the name is a mistake <<
        bodyMeshFilter = transform.FindChild("Body").GetComponent<MeshFilter>();
        bodyMesh = bodyMeshFilter.mesh;

        // starting leaves mesh
        leavesMeshFilter = transform.FindChild("Leaves").GetComponent<MeshFilter>();
        leavesMesh = leavesMeshFilter.mesh;

    }

    // Use this for initialization
    void Start()
    {

        rand = new System.Random((int)transform.position.x);
        float scale = 1f + 0.2f * rand.Next(25);


        // tree body vertices
        Vector3[] bodyVertices = new Vector3[]
        {        
            //front base
            new Vector3(-1, 0, 1) * scale,          //0
            new Vector3(0, 0, 0.6f) * scale,        //1
            new Vector3(1, 0, 1) * scale,           //2
            new Vector3(-0.5f, 1, 0.5f) * scale,    //3
            new Vector3(0.5f, 1, 0.5f) * scale,     //4
        
            // left base
            new Vector3(0.6f, 0, 0) * scale,        //5
            new Vector3(1, 0, -1) * scale,          //6
            new Vector3(0.5f, 1, -0.5f) * scale,    //7

            //right base
            new Vector3(0, 0, -0.6f) * scale,        //8
            new Vector3(-1, 0, -1) * scale,          //9
            new Vector3(-0.5f, 1, -0.5f) * scale,    //10

            // back base
            new Vector3(-0.6f, 0, 0) * scale,          //11
            new Vector3(-1, 0, 1) * scale,          //12
            new Vector3(-0.5f, 1, 0.5f) * scale,     //13

            // --- mid

            //front base
            new Vector3(-0.5f, 3, 0.5f) * scale,    //14
            new Vector3(0.5f, 3, 0.5f) * scale,     //15
            new Vector3(-0.7f, 3.5f, 0.7f) * scale,    //16
            new Vector3(0.7f, 3.5f, 0.7f) * scale,     //17
            new Vector3(0, 3.5f, 0.7f) * scale,        //18

            //left base
            new Vector3(0.5f, 3, -0.5f) * scale,     //19
            new Vector3(0.7f, 3.5f, -0.7f) * scale,      //20
            new Vector3(0.7f, 3.5f, 0) * scale,          //21

            // right base
            new Vector3(-0.5f, 3, -0.5f) * scale,     //22
            new Vector3(-0.7f, 3.5f, -0.7f) * scale,     //23
            new Vector3(0, 3.5f, -0.7f) * scale,       //24

            // back base
            new Vector3(-0.7f, 3.5f, 0) * scale       //25
        };

        // tree body triangles
        int[] bodyTriangles = new int[]
        {
           //front
           0, 1, 3,
           1, 2, 4,
           1, 4, 3,

           // left
           2, 5, 4,
           5, 6, 7,
           5, 7, 4,

           //right
           6, 8, 7,
           8, 9, 10,
           7, 8, 10,

           // back
           9, 11, 10,
           11, 12, 13,
           10, 11, 13,

           //front
           3, 4, 14,
           4, 15, 14,
           14, 18, 16,
           15, 17, 18,
           14, 15, 18,
        
           // left
           4, 7, 19,
           15, 4, 19,
           15, 19, 21,
           15, 21, 17,
           19, 17, 21,
           19, 20, 21,

           // right
           7, 10, 22,
           22, 19, 7,
           22, 23, 24,
           19, 22, 24,
           19, 24, 20,

           //back
           22, 10, 14,
           10, 3, 14,
           23, 22, 25,
           14, 16, 25,
           22, 14, 25
        };

        // body UVs
        Vector2[] bodyUVs = new Vector2[]
        {

        };



        // tree leaves vertices
        Vector3[] leavesVertices = new Vector3[]
        {
            // front
            new Vector3(-0.7f, 3.5f, 0.7f) * scale,//0
            new Vector3(0.7f, 3.5f, 0.7f) * scale, //1
            new Vector3(0.7f, 3.5f, -0.7f) * scale, //2
            new Vector3(-0.7f, 3.5f, -0.7f) * scale,//3

            //first row
            new Vector3(-0.7f, 3.6f, 1.5f) * scale, //4
            new Vector3(0.7f, 3.6f, 1.5f) * scale,  //5

            new Vector3(1.5f, 3.6f, 0.7f) * scale,  //6
            new Vector3(1.5f, 3.6f, -0.7f) * scale, //7

            new Vector3(0.7f, 3.6f, -1.5f) * scale,//8
            new Vector3(-0.7f, 3.6f, -1.5f) * scale,//9

            new Vector3(-1.5f, 3.6f, 0.7f) * scale,//10
            new Vector3(-1.5f, 3.6f, -0.7f) *scale,//11

            //second row
            new Vector3(-0.7f, 3.9f, 2) * scale,//12
            new Vector3(0.7f, 3.9f, 2) * scale,//13
            new Vector3(1.8f, 3.9f, 0.7f) * scale, //14
            new Vector3(1.8f, 3.9f, -0.7f) * scale, //15
            new Vector3(-1.8f, 3.9f, 0.7f) * scale,  //16
            new Vector3(-1.8f, 3.9f, -0.7f) * scale, //17
            new Vector3(-0.7f, 3.9f, -1.8f) * scale, //18
            new Vector3(0.7f, 3.9f, -1.8f) * scale, //19

            //third row
            new Vector3(-0.7f, 4.3f, 2.4f) * scale,//20
            new Vector3(0.7f, 4.3f, 2.4f) * scale,//21
            new Vector3(2.2f, 4.3f, 0.7f) * scale,//22
            new Vector3(2.2f, 4.3f, -0.7f) * scale,//23
            new Vector3(-0.7f, 4.3f, -2.2f) * scale,//24
            new Vector3(0.7f, 4.3f, -2.2f) * scale,//25
            new Vector3(-2.2f, 4.3f, -0.7f) * scale,//26
            new Vector3(-2.2f, 4.3f, 0.7f) * scale,//27

            //forth row
            new Vector3(-0.7f, 4.8f, 2.7f) * scale,//28
            new Vector3(0.7f, 4.8f, 2.7f) * scale,//29
            new Vector3(2.5f, 4.8f, 0.7f) * scale,//30
            new Vector3(2.5f, 4.8f, -0.7f) * scale,//31
            new Vector3(0.7f, 4.8f, -2.5f) * scale,//32
            new Vector3(-0.7f, 4.8f, -2.5f) * scale,//33
            new Vector3(-2.5f, 4.8f, -0.7f) * scale,//34
            new Vector3(-2.5f, 4.8f, 0.7f) * scale,//35

            //fifth row
            new Vector3(-0.7f, 5.5f, 2.7f) * scale,//36
            new Vector3(0.7f, 5.5f, 2.7f) * scale,//37
            new Vector3(2.5f, 5.5f, 0.7f) * scale,//38
            new Vector3(2.5f, 5.5f, -0.7f) * scale,//39
            new Vector3(0.7f, 5.5f, -2.5f) * scale,//40
            new Vector3(-0.7f, 5.5f, -2.5f) * scale,//41
            new Vector3(-2.5f, 5.5f, -0.7f) * scale,//42
            new Vector3(-2.5f, 5.5f, 0.7f) * scale,//43

            //sixth row
            new Vector3(-0.7f, 6.2f, 2.4f) * scale, //44
            new Vector3(0.7f, 6.2f, 2.4f) * scale, //45
            new Vector3(2.2f, 6.2f, 0.7f) * scale,//46
            new Vector3(2.2f, 6.2f, -0.7f) * scale,//47
            new Vector3(0.7f, 6.2f, -2.4f) * scale,//48
            new Vector3(-0.7f, 6.2f, -2.4f) * scale,//49
            new Vector3(-2.2f, 6.2f, -0.7f) * scale,//50
            new Vector3(-2.2f, 6.2f, 0.7f) * scale, //51

            //seventh row
            new Vector3(-0.7f, 6.6f, 2f) * scale,//52
            new Vector3(0.7f, 6.6f, 2f) * scale,//53
            new Vector3(2f, 6.6f, 0.7f) * scale,//54
            new Vector3(2f, 6.6f, -0.7f) * scale,//55
            new Vector3(0.7f, 6.6f, -2f) * scale,//56
            new Vector3(-0.7f, 6.6f, -2f) * scale,//57
            new Vector3(-2, 6.6f, -0.7f) * scale,//58
            new Vector3(-2, 6.6f, 0.7f) * scale,//59

            //top
            new Vector3(-0.7f, 7f, 0.7f) * scale,//60
            new Vector3(0.7f, 7f, 0.7f) * scale, //61
            new Vector3(0.7f, 7f, -0.7f) * scale, //62
            new Vector3(-0.7f, 7f, -0.7f) * scale,//63
        };

        // leaves triangles
        int[] leavesTriangles = new int[]
        {   
            //first row
           0, 1, 4,
           1, 5, 4,
           3, 0, 11,
           0, 10, 11,
           3, 9, 2,
           2, 9, 8,
           2, 6, 1,
           2, 7, 6,

            //closing borders
            2, 8, 7,
            3, 11, 9,
            1, 6, 5,
            0, 4, 10,

            //second row
            4, 5, 12,
            5, 13, 12,
            7, 15, 6,
            6, 15, 14,
            11, 10, 17,
            10, 16, 17,
            9, 18, 8,
            8, 18, 19,

            //closing borders
            13, 5, 14,
            5, 6, 14,
            8, 19, 7,
            19, 15, 7,
            11, 18, 9,
            17, 18, 11,
            16, 10, 4,
            12, 16, 4,

            //third row
            12, 13, 20,
            20, 13, 21,
            15, 23, 14,
            14, 23, 22,
            24, 25, 19,
            19, 18, 24,
            26, 17, 27,
            27, 17, 16,

            //closing borders
            21, 13, 14,
            14, 22, 21,
            27, 16, 12,
            27, 12, 20,
            26, 18, 17,
            26, 24, 18,
            19, 25, 15,
            25, 23, 15,

            //forth row
            20, 21, 28,
            28, 21, 29,
            23, 31, 22,
            22, 31, 30,
            25, 24, 33,
            25, 33, 32,
            34, 26, 27,
            34, 27, 35,

            //closing borders
            21, 22, 29,
            22, 30, 29,
            25, 32, 23,
            32, 31, 23,
            24, 26, 34,
            34, 33, 24,
            35, 27, 20,
            35, 20, 28,

            //fifth row
            28, 29, 36,
            36, 29, 37,
            31, 39, 30,
            30, 39, 38,
            42, 34, 43,
            43, 34, 35,
            41, 40, 33,
            33, 40, 32,

            //closing borders
            37, 29, 30,
            30, 38, 37,
            32, 39, 31,
            40, 39, 32,
            41, 33, 34,
            42, 41, 34,
            43, 35, 28,
            43, 28, 36,

            //sixth row
            44, 36, 37,
            44, 37, 45,
            47, 38, 39,
            47, 46, 38,
            49, 48, 41,
            41, 48, 40,
            42, 43, 50,
            43, 51, 50,

            //closing borders
            45, 37, 46,
            37, 38, 46,
            48, 47, 40,
            40, 47, 39,
            43, 44, 51,
            43, 36, 44,
            41, 50, 49,
            42, 50, 41,

            //seventh row
            52, 44, 53,
            44, 45, 53,
            46, 47, 55,
            54, 46, 55,
            49, 57, 48,
            48, 57, 56,
            58, 50, 51,
            51, 59, 58,

            //closing borders
            50, 58, 49,
            58, 57, 49,
            48, 56, 55,
            48, 55, 47,
            53, 46, 54,
            45, 46, 53,
            59, 51, 44,
            44, 52, 59,

            //top
            60, 53, 61,
            60, 52, 53,
            59, 52, 60,
            63, 59, 60,
            58, 63, 57,
            57, 63, 62,
            59, 63, 58,
            62, 56, 57,
            62, 55, 56,
            62, 61, 55,
            61, 54, 55,
            53, 54, 61,

          //final
            63, 60, 62,
            60, 61, 62


        };


        // leaves UVs
        Vector2[] leavesUVs = new Vector2[]
        {

        };


        //creating the mesh

        // leaves mesh
        leavesMesh.Clear();
        leavesMesh.vertices = leavesVertices;
        leavesMesh.triangles = leavesTriangles;
        leavesMesh.uv = leavesUVs;
        leavesMesh.Optimize();
        leavesMesh.RecalculateNormals(); //how light will affect this object

        // body mesh
        bodyMesh.Clear();
        bodyMesh.vertices = bodyVertices;
        bodyMesh.triangles = bodyTriangles;
        bodyMesh.uv = bodyUVs;
        bodyMesh.Optimize();
        bodyMesh.RecalculateNormals();

        bodyColl.sharedMesh = bodyMesh; // body collider
        leavesColl.sharedMesh = leavesMesh; // leaves collider

        // changes the tree color if the map genre is shooter
        if(MapGenerator.mapGameGenre == 9)
            ChangeColor();

    }

    void ChangeColor()
    {
        Renderer[] m;

        m = GetComponentsInChildren<Renderer>();

        m[1].material.color = new Color(185, 0, 5, 0.95f);
        m[0].material.color = new Color(0, 0, 150);
    }
}
