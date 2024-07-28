using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeManager _cubePrefab;
    [SerializeField] private ExplosionManager _explosionManager;
    [SerializeField] private float _splitChanceReductionFactor = 0.5f;
    [SerializeField] private int _minCubesToSpawn = 2;
    [SerializeField] private int _maxCubesToSpawn = 6;

    public List<CubeManager> SpawnCubes(Vector3 position, Vector3 scale, float parentSplitChance)
    {
        List<CubeManager> newCubes = new List<CubeManager>();

        if (_cubePrefab == null)
        {
            return newCubes;
        }

        float newSplitChance = parentSplitChance * _splitChanceReductionFactor;
        int numberOfNewCubes = Random.Range(_minCubesToSpawn, _maxCubesToSpawn + 1);

        for (int i = 0; i < numberOfNewCubes; i++)
        {
            CubeManager newCubeInstance = Instantiate(_cubePrefab, position, Quaternion.identity);
            newCubeInstance.transform.localScale = scale;

            newCubeInstance.Initialize(newSplitChance, this, _explosionManager);

            newCubes.Add(newCubeInstance);
        }

        return newCubes;
    }
}