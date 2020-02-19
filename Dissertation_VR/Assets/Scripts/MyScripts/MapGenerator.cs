using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap };
    public DrawMode drawMode;

    const int mapChunkSize = 61;
    [Range(0,6)]
    public int levelOfDetail;

    public TerrainData terrainData;
    public NoiseData noiseData;
    
    public bool autoUpdate;

    public TerrainType[] regions;

    //public MeshCollider meshCollider;

    float[,] falloffMap;

    [Range(0.0001f, 0.5f)]
    public float smooth = 0.5f;

    public float currentNSV;
    public float newNSV;

    public float currentPV;
    public float newPV;

    public float currentLV;
    public float newLV;

    public int currentSV;
    public int newSV;

    public float currentOXV;
    public float currentOYV;
    public float newOXV;
    public float newOYV;

    public float currentMHMV;
    public float newMHMV;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
        currentNSV = noiseData.noiseScale;
        currentPV = noiseData.persistance;
        currentLV = noiseData.lacunarity;
        currentSV = noiseData.seed;
        currentOXV = noiseData.offset.x;
        currentOYV = noiseData.offset.y;
        currentMHMV = terrainData.meshHeightMultiplier;
    }

    private void Update()
    {
        MakeRandomVals();
    }

    void OnValuesUpdated()
    {
        GenerateMap();
    }
    /*
    private void LateUpdate()
    {
        GenerateMap();
    }
    */
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, newSV, newNSV, noiseData.octaves, newPV, newLV, noiseData.offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (terrainData.useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, newMHMV, terrainData.meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize)));
        }
    }

    private void OnValidate()
    {
        if(terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }

        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }

    public void MakeRandomVals()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            newNSV = Random.Range(5f, 10f);
            newPV = Random.Range(0.15f, 0.65f);
            newLV = Random.Range(1f, 3f);
            newSV = Random.Range(0, 100);
            newOXV = Random.Range(-10f, 10f);
            newOYV = Random.Range(-10f, 10f);
            newMHMV = Random.Range(-0.5f, 0.5f);
            noiseData.seed = newSV;
        }
        
        noiseData.noiseScale = Mathf.Lerp(currentNSV, newNSV, Time.deltaTime * smooth);
        noiseData.persistance = Mathf.Lerp(currentPV, newPV, Time.deltaTime * smooth);
        noiseData.lacunarity = Mathf.Lerp(currentLV, newLV, Time.deltaTime * smooth);
        noiseData.offset.x = Mathf.Lerp(currentOXV, newOXV, Time.deltaTime * smooth);
        noiseData.offset.y = Mathf.Lerp(currentOYV, newOYV, Time.deltaTime * smooth);
        terrainData.meshHeightMultiplier = Mathf.Lerp(currentMHMV, newMHMV, Time.deltaTime * smooth);
        GenerateMap();
        currentNSV = newNSV;
        currentPV = newPV;
        currentLV = newLV;
        currentSV = newSV;
        currentOXV = newOXV;
        currentOYV = newOYV;
        currentMHMV = newMHMV;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}