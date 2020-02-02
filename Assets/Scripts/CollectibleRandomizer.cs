using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CollectibleRandomizer : MonoBehaviour
{
    private bool Collected { get; set; }
    private GameObject PlayerHead { get; set; }
    
    private Rigidbody Body { get; set; }
    
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

    private void Update()
    {
        if (Collected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime);
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.deltaTime);

            var directionToPlayer =  PlayerHead.transform.position - transform.position;
            Body.velocity = Vector3.Lerp(Body.velocity, directionToPlayer * 10, Time.deltaTime);
            
            if (transform.localScale.x < 0.01f)
                gameObject.SetActive(false);
        }
    }

    [Serializable]
    public class CollectibleSpawner
    {
        public int amount;
        public GameObject gameObject;
        public float height;
    }

    public void Collect(GameObject playerHead, Vector3 down, Vector3 right)
    {
        Collected = true;
        PlayerHead = playerHead;
        Body = gameObject.AddComponent<Rigidbody>();
        Body.interpolation = RigidbodyInterpolation.Interpolate;
        Body.AddForce(down * 100f, ForceMode.Impulse);
        Body.AddForce(-right * 100f, ForceMode.Impulse);
        
        var collider = GetComponent<SphereCollider>();
        collider.enabled = false;

        var collectables = FindObjectsOfType<Collectable>();
        foreach (var collectible in collectables)
            collectible.CollectEnd(playerHead);

        Time.timeScale = .1f;
    }
}
