using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;

    public bool autoUpdate;
    public int octaves;
    public float noiseScale;
    public int seed;
    public Vector2 offset;

    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public bool cliffs;
    [Range(0, 1)]
    public float cliffFactor;

    public bool forests;

    private float[,] noiseMap;
    private Forest forestGenerator;
    private GeometryGenerator geometryGenerator;

    public void Start()
    {
        geometryGenerator = FindObjectOfType<GeometryGenerator>();
        forestGenerator = FindObjectOfType<Forest>();
        GenerateMap();
    }

    public void GenerateMap()
    {
        if (cliffs)
        {
            noiseMap = Noise.GenerateCliffedNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset, cliffFactor);
        } else
        {
            noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        }
        
        geometryGenerator.GenerateMesh(noiseMap);

        if (forests)
        {
            forestGenerator.GenerateForest(geometryGenerator.addedPoints);
        } else
        {
            forestGenerator.Clean();
        }    
    }
    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
