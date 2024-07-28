using UnityEngine;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 500f;
    [SerializeField] private float _baseExplosionRadius = 5f;

    public void ApplyExplosionForce(Vector3 explosionPosition, List<CubeManager> newCubes, float explosionScale = 1f)
    {
        float explosionForce = _baseExplosionForce * explosionScale;
        float explosionRadius = _baseExplosionRadius * explosionScale;

        if (newCubes != null)
        {
            foreach (CubeManager cube in newCubes)
            {
                if (cube != null)
                {
                    Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();

                    if (cubeRigidbody != null)
                    {
                        cubeRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                    }
                }
            }
        }

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody hitRigidbody = hit.GetComponent<Rigidbody>();

            if (hitRigidbody != null && hit.gameObject != gameObject)
            {
                hitRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }
}