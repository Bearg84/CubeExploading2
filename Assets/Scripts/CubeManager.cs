using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class CubeManager : MonoBehaviour
{
    private float _splitChance;

    [SerializeField] private ExplosionManager _explosionManager;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Start()
    {
        SetRandomColor();

        if (_splitChance == 0)
        {
            _splitChance = 1.0f;
        }
    }

    private void OnMouseDown()
    {
        if (_cubeSpawner != null && Random.value <= _splitChance)
        {
            var newCubes = _cubeSpawner.SpawnCubes(transform.position, transform.localScale / 2f, _splitChance);

            if (_explosionManager != null)
            {
                _explosionManager.ApplyExplosionForce(transform.position, newCubes);
            }
        }
        else
        {
            if (_explosionManager != null)
            {
                _explosionManager.ApplyExplosionForce(transform.position, null, transform.localScale.magnitude);
            }
        }

        Destroy(gameObject);
    }

    public void Initialize(float splitChance, CubeSpawner cubeSpawner, ExplosionManager explosionManager)
    {
        _splitChance = splitChance;
        _cubeSpawner = cubeSpawner;
        _explosionManager = explosionManager;
    }

    private void SetRandomColor()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}