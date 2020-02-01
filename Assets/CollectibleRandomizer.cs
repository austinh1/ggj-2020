using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectibleRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectiblePrefabs;
    private void Start()
    {
        var prefab = collectiblePrefabs[0];
        var collectible = Instantiate(prefab);

        var sphereCollider = GetComponent<SphereCollider>();
        var toSurface = sphereCollider.bounds.center;
        toSurface.y += sphereCollider.radius;
        // var rotated = Quaternion.Euler(0, 0, 0) * toSurface;

        collectible.transform.position = toSurface;
    }
}
