using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
public partial class UnityPrimitiveAdditions
{
    [MenuItem("GameObject/3D Object/Prism")]
    private static void CreatePrism()
    {
        float halfSqrt = Mathf.Sqrt(3f)*0.25f;

        #region verticies 

        Vector3 topa = new Vector3(0, 0.5f, 0.5f);
        Vector3 topb = new Vector3(halfSqrt, 0.5f, -0.25f);
        Vector3 topc = new Vector3(-halfSqrt, 0.5f, -0.25f);

        Vector3 bota = new Vector3(0, -0.5f, 0.5f);
        Vector3 botb = new Vector3(halfSqrt, -0.5f, -0.25f);
        Vector3 botc = new Vector3(-halfSqrt, -0.5f, -0.25f);

        Vector3[] verts = new Vector3[]
        {
            topa, topb, topc,

            topa,bota, botb,
            topa,botb,topb,
            topb, botb, botc,
            topb, botc, topc,
            topc,botc, bota,
            topc,bota, topa,

            bota,botb,botc
        };

        int[] triangles = new int[]
        {
            0, 1,2,

            3,4,5,
            6,7, 8,
            9, 10, 11,
            12, 13, 14,
            15, 16, 17,
            18, 19, 20,

            21, 22, 23
        };

        #endregion

        #region normals
               

        Vector3 ntop = new Vector3(0,1,0);

        Vector3 nab = new Vector3(halfSqrt, 0, 0.5f);
        Vector3 nbc = new Vector3(0,0,-1);
        Vector3 nca = new Vector3(-halfSqrt,0,0.5f);

        Vector3 nbot = new Vector3(0,-1,0);

        Vector3[] normals = new Vector3[]
        {
            ntop,ntop,ntop,

            nab,nab,nab,
            nab,nab,nab,
            nbc,nbc,nbc,
            nbc,nbc,nbc,
            nca,nca,nca,
            nca,nca,nca,

            nbot,nbot,nbot
        };
        #endregion

        //todo
        #region uv

        Vector2[] uvs = new Vector2[]
        {
        };
        #endregion

        CreatePrimitive("Prism", verts, triangles, normals);
    }
}
