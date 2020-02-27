using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

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

    float[,] falloffMap;

    public float lerpTime;
    public float currentLerpTime;
    float lerpy;

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

    bool terrainLoop;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
        noiseData.seed = Random.Range(0, 100);
        noiseData.noiseScale = 30f;
        noiseData.persistance = 0.5f;
        noiseData.lacunarity = 2f;
        noiseData.offset = new Vector2(0f, 0f);
        terrainData.meshHeightMultiplier = 0.1f;
        currentNSV = noiseData.noiseScale;
        currentPV = noiseData.persistance;
        currentLV = noiseData.lacunarity;
        currentOXV = noiseData.offset.x;
        currentOYV = noiseData.offset.y;
        currentMHMV = terrainData.meshHeightMultiplier;
    }

    private void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
            terrainLoop = false;
            currentNSV = newNSV;
            currentPV = newPV;
            currentLV = newLV;
            currentOXV = newOXV;
            currentOYV = newOYV;
            currentMHMV = newMHMV;
        }

        lerpy = currentLerpTime / lerpTime;

        if (!terrainLoop)
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                MakeRandomVals();
                currentLerpTime = 0f;
                terrainLoop = true;
            }
        }

        noiseData.NotifyOfUpdatedValues();
        noiseData.noiseScale = Mathf.Lerp(currentNSV, newNSV, lerpy);
        noiseData.persistance = Mathf.Lerp(currentPV, newPV, lerpy);
        noiseData.lacunarity = Mathf.Lerp(currentLV, newLV, lerpy);
        noiseData.offset.x = Mathf.Lerp(currentOXV, newOXV, lerpy);
        noiseData.offset.y = Mathf.Lerp(currentOYV, newOYV, lerpy);
        terrainData.meshHeightMultiplier = Mathf.Lerp(currentMHMV, newMHMV, lerpy);
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
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);

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
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
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
        newNSV = Random.Range(5f, 10f);
        newPV = Random.Range(0.15f, 0.65f);
        newLV = Random.Range(1f, 3f);
        newOXV = Random.Range(-10f, 10f);
        newOYV = Random.Range(-10f, 10f);
        newMHMV = Random.Range(-0.5f, 0.5f);
        lerpTime = Random.Range(4f, 20f);
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}