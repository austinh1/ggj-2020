using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour
{
    private GameObject playerHead;
    public bool Collected { get; private set; }
    private int GastroLevel { get; set; }
    private Rigidbody body;
    
    [FormerlySerializedAs("speed")] public float force = 1;
    public int levelNeededToCollect = 1;
    public bool freezeAll = true;
    public int pointValue = 1;
    
    public void Collect(GameObject player, int gastroLevel)
    {
        GastroLevel = gastroLevel;
        if (!Collected)
        {
            playerHead = player;
            transform.parent.SetParent(player.transform);
            Collected = true;
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().AddForce(transform.up * 10f, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(playerHead.transform.forward * 10f, ForceMode.Impulse);
            StartCoroutine(DelayedColliderDisable());
        }
    }

    private IEnumerator DelayedColliderDisable()
    {
        yield return new WaitForSeconds(.1f);
        
        foreach (var col in GetComponentsInChildren<Collider>(true))
            col.enabled = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        if (freezeAll)
        {
            body.constraints = RigidbodyConstraints.FreezeAll;
            var tr = transform;
            tr.localPosition = Vector3.zero;
            tr.localEulerAngles = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Collected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime);

            var directionToPlayer =  playerHead.transform.position - transform.position;
            body.velocity = Vector3.Lerp(body.velocity, directionToPlayer * force, force * Time.deltaTime);
            
            if (transform.localScale.x < 0.01f)
            {
                gameObject.SetActive(false);
            }
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!Collected && other.gameObject.CompareTag("Collectible"))
            Destroy(gameObject);
    }

    public void CollectEnd(GameObject player)
    {
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

        if (!Collected)
        {
            playerHead = player;
            Collected = true;

            force = 1;
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().AddForce(transform.up * 100f, ForceMode.Impulse);
        }
    }
}
