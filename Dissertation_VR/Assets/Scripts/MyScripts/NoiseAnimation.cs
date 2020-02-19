using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseAnimation : MonoBehaviour
{
    MapGenerator mapGen;

    [Range(0.1f, 2f)]
    public float smooth = 1f;

    [Range(5f, 10f)]
    public float noiseScaleVal;
    public float currentNSV;
    public float newNSV;

    [Range(0.15f, 0.65f)]
    public float persistanceVal;
    public float currentPV;
    public float newPV;

    [Range(1f, 3f)]
    public float lacunarityVal;
    public float currentLV;
    public float newLV;
    
    [Range(0, 100)]
    public int seedVal;
    public int currentSV;
    public int newSV;

    [Range(-10f, 10f)]
    public float offsetXVal;
    [Range(-10f, 10f)]
    public float offsetYVal;
    public float currentOXV;
    public float currentOYV;
    public float newOXV;
    public float newOYV;
    
    [Range(-0.5f, 0.5f)]
    public float meshHeightMultiplierVal;
    public float currentMHMV;
    public float newMHMV;

    private void Awake()
    {
        mapGen = GetComponent<MapGenerator>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            MakeRandomVals();
        }
    }

    public void MakeRandomVals()
    {
        newNSV = Random.Range(5f, 10f);
        newPV = Random.Range(0.15f, 0.65f);
        newLV = Random.Range(1f, 3f);
        newSV = Random.Range(0, 100);
        newOXV = Random.Range(-10f, 10f);
        newOYV = Random.Range(-10f, 10f);
        newMHMV = Random.Range(-0.5f, 0.5f);

        mapGen.noiseData.noiseScale = Mathf.Lerp(currentNSV, newNSV, Time.deltaTime * smooth);
        persistanceVal = Mathf.Lerp(currentPV, newPV, Time.deltaTime * smooth);
        lacunarityVal = Mathf.Lerp(currentLV, newLV, Time.deltaTime * smooth);
        offsetXVal = Mathf.Lerp(currentOXV, newOXV, Time.deltaTime * smooth);
        offsetYVal = Mathf.Lerp(currentOYV, newOYV, Time.deltaTime * smooth);
        meshHeightMultiplierVal = Mathf.Lerp(currentMHMV, newMHMV, Time.deltaTime * smooth);

        seedVal = newSV;

        currentNSV = newNSV;
        currentPV = newPV;
        currentLV = newLV;
        currentSV = newSV;
        currentOXV = newOXV;
        currentOYV = newOYV;
        currentMHMV = newMHMV;
    }
}
