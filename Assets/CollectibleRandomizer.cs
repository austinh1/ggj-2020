using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectibleRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectiblePrefabs;
    [SerializeField] private Transform rotater;
    [SerializeField] private Transform point;
    private void Start()
    {
        var prefab = collectiblePrefabs[0];

        for (var i = 0; i < 100; i++)
        {
            var collectible = Instantiate(prefab);

            var y = Random.Range(0, 360);
            var x = Random.Range(0, 360);
            rotater.rotation = Quaternion.Euler(x, y, 0);
        
            collectible.transform.position = point.position;    
        }
    }
}
