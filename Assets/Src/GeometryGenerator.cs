using SharpVoronoiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeometryGenerator : MonoBehaviour
{
    public List<Vector3> addedPoints;

    float[,] noiseMap;
    Mesh mesh;
    VoronoiPlane plane;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        plane = new VoronoiPlane(0, 0, 100, 100);
        plane.GenerateRandomSites(4000, PointGenerationMethod.Uniform);

        plane.Tessellate();
        plane.Relax(3, 0.7f); //???
    }

    public void GenerateMesh(float[,] _noiseMap)
    {
        noiseMap = _noiseMap;

        mesh.Clear();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        setMeshValues(mesh, plane);
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    void setMeshValues(Mesh mesh, VoronoiPlane plane)
    {
        var cellPoints = getAllCellPoints(plane.Sites);
        addedPoints = new List<Vector3>();
        var addedIndices = new List<int>();
        foreach (var points in cellPoints)
        {
            var triangulatedIndices = getTriangulationIndices(points, addedPoints.Count);
            Array.Reverse(triangulatedIndices); // we reverse the order to reverse the normals
            addedIndices.AddRange(triangulatedIndices.ToList());
            addedPoints.AddRange(points.ToList());
            setWall(points, addedPoints, addedIndices);
        }

        var vertices = new Vector3[addedPoints.Count];
        for (int pos = 0; pos < vertices.Length; pos++)
        {
            vertices[pos] = new Vector3(addedPoints[pos].x, addedPoints[pos].y, addedPoints[pos].z);
        }

        mesh.vertices = vertices;
        mesh.triangles = addedIndices.ToArray();
    }

    void addWall(Vector3 start, Vector3 end, List<Vector3> addedPoints, List<int> addedIndices)
    {
        addedPoints.Add(start);
        addedPoints.Add(end);
        addedPoints.Add(new Vector3(start.x, 0.0f, start.z));
        addedPoints.Add(new Vector3(end.x, 0.0f, end.z));

        addedIndices.Add(addedPoints.Count - 4);
        addedIndices.Add(addedPoints.Count - 2);
        addedIndices.Add(addedPoints.Count - 3);

        addedIndices.Add(addedPoints.Count - 3);
        addedIndices.Add(addedPoints.Count - 2);
        addedIndices.Add(addedPoints.Count - 1);
    }

    private void setWall(Vector3[] points, List<Vector3> addedPoints, List<int> addedIndices)
    {
        for (int pos = 0; pos < points.Length - 1; pos++)
        {
            addWall(points[pos], points[pos + 1], addedPoints, addedIndices);
        }
        addWall(points[points.Length - 1], points[0], addedPoints, addedIndices);
    }

    int[] getTriangulationIndices(Vector3[] points, int offset)
    {
        Vector2[] flatPoints = new Vector2[points.Length];
        for (int pos = 0; pos < points.Length; pos++)
        {
            flatPoints[pos] = new Vector2(points[pos].x, points[pos].z);
        }

        Triangulator tr = new Triangulator(flatPoints);
        int[] indices = tr.Triangulate();
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] += offset;
        }
        return indices;
    }
    List<Vector3[]> getAllCellPoints(List<VoronoiSite> sites)
    {
        var pts = new List<Vector3[]>();
        foreach (var site in sites)
        {
            pts.Add(getCellPoints(site));
        }

        return pts;
    }

    float getPointHeight(int x, int y)
    {
        return (float)Math.Floor(noiseMap[x, y] * 32);
    }

    Vector3[] getCellPoints(VoronoiSite site)
    {
        var points = new Vector3[site.ClockwiseCell.Count()];
        var clockwisePoints = site.ClockwisePoints.ToList();
        for (int pos = 0; pos < clockwisePoints.Count; pos++)
        {
            points[pos] = new Vector3((float)clockwisePoints.ElementAt(pos).X
                , getPointHeight((int)site.X, (int)site.Y)
                , (float)clockwisePoints.ElementAt(pos).Y);
        }
        return points;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
