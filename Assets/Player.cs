using Rewired;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rewired.Player Input { get; set; }
    private CapsuleCollider CapsuleCollider { get; set; }
    [SerializeField] private LayerMask WhatIsGround;
    
    private void Start()
    {
        Input = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        // RaycastHit[] raycastHits;
        // var size = Physics.RaycastNonAlloc(CapsuleCollider.bounds.center, -transform.up, out raycastHits, CapsuleCollider.height * 0.5f + .1f, WhatIsGround);
    }
}
