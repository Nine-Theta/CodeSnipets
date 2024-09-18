using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public partial class UnityPrimitiveAdditions
{
    [MenuItem("GameObject/3D Object/Icosahedron")]
    private static void CreateIcosahedron()
    {
        const int arraySize = 60;

        #region vertices

        float size = 0.5f;

        float sqrtFive = Mathf.Sqrt(5f);

        float midHeight = 1f / sqrtFive * size;

        float depthSmall = (5f - sqrtFive) / 10f;
        float depthLarge = (5f + sqrtFive) / 10f;

        float widthSmall = Mathf.Sqrt(depthSmall) * size;
        float widthLarge = Mathf.Sqrt(depthLarge) * size;

        depthSmall *= size;
        depthLarge *= size;

        Vector3 top = new Vector3(0, size, 0);

        Vector3 topa = new Vector3(0, midHeight, midHeight*2f);
        Vector3 topb = new Vector3(-widthLarge, midHeight, depthSmall);
        Vector3 topc = new Vector3(-widthSmall, midHeight, -depthLarge);
        Vector3 topd = new Vector3(widthSmall, midHeight, -depthLarge);
        Vector3 tope = new Vector3(widthLarge, midHeight, depthSmall);

        Vector3 bota = new Vector3(0, -midHeight, -midHeight*2f);
        Vector3 botb = new Vector3(widthLarge, -midHeight, -depthSmall);
        Vector3 botc = new Vector3(widthSmall, -midHeight, depthLarge);
        Vector3 botd = new Vector3(-widthSmall, -midHeight, depthLarge);
        Vector3 bote = new Vector3(-widthLarge, -midHeight, -depthSmall);

        Vector3 bot = new Vector3(0, -size, 0);

        Vector3[] verts = new Vector3[arraySize]
        {
            topa, top, topb,
            topb, top, topc,
            topc, top, topd,
            topd, top, tope,
            tope, top, topa,

            botc, topa, botd,
            botd, topa, topb,
            botd, topb, bote,
            bote, topb, topc,
            bote, topc, bota,
            bota, topc, topd,
            bota, topd, botb,
            botb, topd, tope,
            botb, tope, botc,
            botc, tope, topa,

            bot, botc, botd,
            bot, botd, bote,
            bot, bote, bota,
            bot, bota, botb,
            bot, botb, botc
        };

        int[] triangles = new int[arraySize]
        {
            0, 1,2,
            3,4,5,
            6,7, 8,
            9, 10, 11,
            12, 13, 14,

            15, 16, 17,
            18, 19, 20,
            21, 22, 23,
            24,25,26,
            27,28,29,
            30,31, 32,
            33,34,35,
            36,37,38,
            39,40,41,
            42,43,44,

            45,46,47,
            48,49,50,
            51,52,53,
            54,55,56,
            57,58,59
        };

        #endregion vertices

        #region normals

        Vector3[] normals = new Vector3[arraySize];

        for (int i=0; i< verts.Length; i+=3)
        {
            Vector3 A = verts[i+1]-verts[i];
            Vector3 B = verts[i+2]-verts[i];

            float nX = A.y * B.z - A.z * B.y;
            float nY = A.z * B.x - A.x * B.z;
            float nZ = A.x * B.y - A.y * B.x;

            Vector3 normal = new Vector3(nX, nY, nZ);

            normals[i] = normal;
            normals[i+1] = normal;
            normals[i+2] = normal;
        }

        #endregion normals


        //todo
        #region uv
        Vector2[] uvs = new Vector2[]
        {

        };
        #endregion


        CreatePrimitive("Icosahedron", verts, triangles, normals);
    }
}
