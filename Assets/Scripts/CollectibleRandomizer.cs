using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CollectibleRandomizer : MonoBehaviour
{
    [SerializeField] private List<CollectibleSpawner> collectiblePrefabs;
    [SerializeField] private Transform rotater;
    [SerializeField] private Transform point;
    
    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        var currentHeight = point.position.y;

        foreach (var collectibleSpawner in collectiblePrefabs)
        {
            rotater.rotation = Quaternion.Euler(0,0,0);
            point.position = new Vector3(0, currentHeight + collectibleSpawner.height, 0);
            
            var i = 0f;
            while (i < collectibleSpawner.amount)
            {
                var y = Random.Range(0, 360);
                var x = Random.Range(0, 360);
                var z = Random.Range(0, 360);
                rotater.rotation = Quaternion.Euler(x, y, z);
                if (Vector3.Distance(player.transform.position, point.position) > 5)
                {
                    var collectible = Instantiate(collectibleSpawner.gameObject);
                    collectible.gameObject.transform.position = point.position;
                    i++;
                }
            }    
        }

        
    }
    
    [Serializable]
    public class CollectibleSpawner
    {
        public int amount;
        public GameObject gameObject;
        public float height;
    }    
}
