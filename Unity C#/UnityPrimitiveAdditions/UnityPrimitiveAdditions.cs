using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public partial class UnityPrimitiveAdditions
{
    private static void CreatePrimitive(string pName, Vector3[] pVerticies, int[] pTriangles, Vector3[] pNormals = null, Vector2[] pUVs = null)
    {
        Mesh mesh = new Mesh();
        mesh.name = pName;
        mesh.vertices = pVerticies;
        mesh.triangles = pTriangles;
        mesh.normals = pNormals;
        mesh.uv = pUVs;

        mesh.Optimize();

        GameObject primitive = new GameObject(pName, new System.Type[] { typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider) });

        if(Selection.activeGameObject != null)
        {
            primitive.transform.parent = Selection.activeGameObject.transform;
            primitive.transform.localPosition = Vector3.zero;
        }
        else
        {
            primitive.transform.position = SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward * 10;
        }

        primitive.GetComponent<MeshFilter>().mesh = mesh;
        primitive.GetComponent<MeshRenderer>().material = new Material(GraphicsSettings.defaultRenderPipeline.defaultShader);
        primitive.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
