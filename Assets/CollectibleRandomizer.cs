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
        var pivot = sphereCollider.bounds.center;
        var radiusInMeters = sphereCollider.radius * transform.localScale.x; 
        var point = new Vector3(pivot.x, pivot.y + radiusInMeters, pivot.z);
        var direction = pivot - point;
        Debug.Log("Radius: " + radiusInMeters);
        Debug.Log("Center: " + sphereCollider.bounds.center);
        var rotated = Quaternion.AngleAxis(-180, Vector3.up) * direction;
        Debug.Log("Result: " + rotated);

        collectible.transform.position = rotated * radiusInMeters;
    }
}
