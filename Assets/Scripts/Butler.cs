using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Planet { get; set; }
    private PlayerScore Player { get; set; }
    [SerializeField] private Animator animator;

    void Start()
    {
        Planet = GameObject.FindWithTag("Planet");
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Player.startGame);
        animator.SetBool("Talk", !Player.startGame);
    }
}
