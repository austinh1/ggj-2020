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
    public GameObject firstPoint;
    public int levelNeededToCollect = 1;
    

    public void Collect(GameObject player, int gastroLevel)
    {
        GastroLevel = gastroLevel;
        if (!Collected)
        {
            playerHead = player;
            Collected = true;
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().AddForce(transform.up * 10f * GastroLevel, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(playerHead.transform.forward * 10f * GastroLevel, ForceMode.Impulse);
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
        transform.localPosition = Vector3.zero;

        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (Collected)
        {
            // if (Vector3.Distance(transform.position, path[index].transform.position) > 0.01f)
            // {
            //     count += Time.deltaTime;
            //     transform.position = Vector3.Lerp(startingPos, path[index].transform.position, count * speed);
            //     if (index == path.Count - 1)
            //     {
            //         transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime,
            //             transform.localScale.y - Time.deltaTime, transform.localScale.z - Time.deltaTime);
            //     }
            //     
            // }
            // else if (Vector3.Distance(transform.position, path[0].transform.position) < 0.01f)
            // {
            //     index++;
            //     startingPos = transform.position;
            //     count = 0;
            // }

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
