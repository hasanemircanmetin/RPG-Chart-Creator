using System.Collections.Generic;
using UnityEngine;

public static class ChartCreator
{
    public static void Create(float[] distances, CanvasRenderer canvasRenderer, Material material, Texture2D texture2D)
    {
        Mesh mesh = CreateMesh(distances);
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, texture2D);
    }

    public static Mesh CreateMesh(float[] distances)
    {
        Mesh mesh = new Mesh
        {
            vertices = CreateVertices(distances),
            triangles = CreateTriangles(distances),
            uv = CreateUVs(distances)
        };
        return mesh;
    }

    private static Vector3[] CreateVertices(IReadOnlyList<float> distances)
    {
        Vector3 center = Vector3.zero;

        Vector3[] vertices = new Vector3[distances.Count + 1];
        vertices[0] = center;

        for (int i = 0; i < distances.Count; i++)
        {
            float angle = 2 * Mathf.PI * i / distances.Count;
            float x = distances[i] * Mathf.Cos(angle);
            float y = distances[i] * Mathf.Sin(angle);
            vertices[i + 1] = new Vector3(x, y, 0);
        }

        return vertices;
    }

    private static int[] CreateTriangles(IReadOnlyCollection<float> distances)
    {
        int[] triangles = new int[3 * distances.Count];
        for (int i = 0; i < distances.Count; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = (i + 1) % distances.Count + 1;
        }

        return triangles;
    }

    private static Vector2[] CreateUVs(IReadOnlyCollection<float> distances)
    {
        Vector2[] uv = new Vector2[distances.Count + 1];
        uv[0] = Vector2.zero;

        for (int i = 1; i < distances.Count + 1; i++)
        {
            uv[i] = Vector2.one;
        }

        return uv;
    }
}