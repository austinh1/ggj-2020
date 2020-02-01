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
    
    public void Collect(GameObject player)
    {
        playerHead = player;
        path.Add(player);
        collect = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        path.Add(firstPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (collect)
        {
            if (Vector3.Distance(transform.position, path[index].transform.position) > 0.01f)
            {
                transform.position = Vector3.Lerp(startingPos, path[index].transform.position, Time.deltaTime * speed);
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
            }

            if (index > path.Count - 1)
            {
                gameObject.SetActive(false);
                collect = false;
                index = 0;
            }
            
        }
    }
}
