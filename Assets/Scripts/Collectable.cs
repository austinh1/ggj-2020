using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameObject playerHead;
    public bool Collected { get; private set; }
    private List<GameObject> path = new List<GameObject>();
    private Vector3 startingPos;
    private int index;
    private bool collected;
    private float timer;
    private float count;
    
    public float speed = 1;
    public GameObject firstPoint;
    public int levelNeededToCollect = 1;

    private GameObject Planet { get; set; }
    
    public void Collect(GameObject player)
    {
        if (!collected)
        {
            playerHead = player;
            path.Add(player);
            Collected = true;
            collected = true;
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            StartCoroutine(DelayedColliderDisable());
        }
    }
    
    

    private IEnumerator DelayedColliderDisable()
    {
        yield return new WaitForSeconds(.5f);
        
        foreach (var colliders in GetComponentsInChildren<Collider>())
            colliders.enabled = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        firstPoint.transform.position = gameObject.transform.position + new Vector3(0, 4, 0);
        path.Add(firstPoint);
        
        Planet = GameObject.FindWithTag("Planet");

        var tr = transform;
        var down = (Planet.transform.position - tr.position).normalized;
        var forward = Vector3.Cross(tr.right, down);
        transform.rotation = Quaternion.LookRotation(-forward, -down);
        transform.localPosition = Vector3.zero;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (Collected)
        {
            if (Vector3.Distance(transform.position, path[index].transform.position) > 0.01f)
            {
                count += Time.deltaTime;
                transform.position = Vector3.Lerp(startingPos, path[index].transform.position, count * speed);
                if (index == path.Count - 1)
                {
                    transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime,
                        transform.localScale.y - Time.deltaTime, transform.localScale.z - Time.deltaTime);
                }
                
            }
            else if (Vector3.Distance(transform.position, path[0].transform.position) < 0.01f)
            {
                index++;
                startingPos = transform.position;
                count = 0;
            }

            if (index > path.Count - 1 || transform.localScale.x < 0.01f)
            {
                gameObject.SetActive(false);
                Collected = false;
                index = 0;
            }
            
        }
    }
}
