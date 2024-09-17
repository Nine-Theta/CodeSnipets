using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
public partial class UnityPrimitiveAdditions
{
    [MenuItem("GameObject/3D Object/Octahedron")]
    private static void CreateOctahedron()
    {
        float sqrt = Mathf.Sqrt(.5f);

        #region verticies 

        Vector3 top = new Vector3(0, sqrt, 0);

        Vector3 right = new Vector3(sqrt, 0, 0);
        Vector3 left = new Vector3(-sqrt, 0, 0);
        Vector3 front = new Vector3(0, 0, sqrt);
        Vector3 back = new Vector3(0, 0, -sqrt);

        Vector3 bot = new Vector3(0, -sqrt, 0);

        Vector3[] verts = new Vector3[]
        {
            right, top, front,
            front, top, left,
            left, top, back,
            back, top, right,

            front, bot, right,
            right, bot, back,
            back, bot, left,
            left, bot, front
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

        float third = 1f / 3f;

        Vector3 na = new Vector3(third, third, third);
        Vector3 nb = new Vector3(-third, third, third);
        Vector3 nc = new Vector3(-third, third, -third);
        Vector3 nd = new Vector3(third, third, -third);

        Vector3 ne = new Vector3(third, -third, third);
        Vector3 nf = new Vector3(third, -third, -third);
        Vector3 ng = new Vector3(-third, -third, -third);
        Vector3 nh = new Vector3(-third, -third, third);

        Vector3[] normals = new Vector3[]
        {
            na, na, na,
            nb, nb, nb,
            nc, nc, nc,
            nd, nd, nd,

            ne, ne, ne,
            nf, nf, nf,
            ng, ng, ng,
            nh, nh, nh
        };
        #endregion

        #region uv
        float height = Mathf.Sqrt(0.0625f - 0.015625f);

        Vector2 uv05 = new Vector2(0f, .5f);
        Vector2 uv255 = new Vector2(.25f, .5f);
        Vector2 uv55 = new Vector2(.5f, .5f);
        Vector2 uv755 = new Vector2(.5f, .25f);
        Vector2 uv15 = new Vector2(1f, .5f);

        Vector2 uv125h = new Vector2(.125f, height);
        Vector2 uv375h = new Vector2(.375f, height);
        Vector2 uv625h = new Vector2(.625f, height);
        Vector2 uv875h = new Vector2(.875f, height);

        Vector2 uv125_h = new Vector2(.125f, -height);
        Vector2 uv375_h = new Vector2(.375f, -height);
        Vector2 uv625_h = new Vector2(.625f, -height);
        Vector2 uv875_h = new Vector2(.875f, -height);

        Vector2[] uvs = new Vector2[]
        {
            uv05, uv125h, uv255,
            uv255, uv375h, uv55,
            uv55, uv625h, uv755,
            uv755, uv875h, uv15,

            uv255, uv125_h, uv05,
            uv15, uv875_h, uv755,
            uv755, uv625_h, uv55,
            uv55, uv375_h, uv255
        };
        #endregion

        Mesh mesh = new Mesh();
        mesh.name = "Octahedron";
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.Optimize();

        GameObject octaHedron = new GameObject("Octahedron", new System.Type[] { typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider) });
        octaHedron.transform.position = SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward * 10;

        octaHedron.GetComponent<MeshFilter>().mesh = mesh;
        octaHedron.GetComponent<MeshRenderer>().material = new Material(GraphicsSettings.defaultRenderPipeline.defaultShader);
        octaHedron.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
