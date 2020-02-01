using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameObject playerHead;
    public bool collect;
    public List<GameObject> path;
    public GameObject firstPoint;
    public Vector3 startingPos;
    public float speed;
    public int index;
    public bool collected;
    public float timer;
    float count;
    
    public void Collect(GameObject player)
    {
        if (!collected)
        {
            playerHead = player;
            path.Add(player);
            collect = true;
            collected = true;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        firstPoint.transform.position = gameObject.transform.position + new Vector3(0, 4, 0);
        path.Add(firstPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (collect)
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
                collect = false;
                index = 0;
            }
            
        }
    }
}
