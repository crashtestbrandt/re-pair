using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePairManager : MonoBehaviour
{
    public int maxJumps = 1;
    public Vector3 playerRegions;
    private Vector2 lastPlayerProjections = Vector2.zero;
    private bool[] playerRequestMask;

    // Start is called before the first frame update
    void Start()
    {
        playerRequestMask = new bool[2];
        playerRequestMask[0] = true;
        playerRequestMask[1] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*
    public int GetJumps(int player, Vector3 pos) {
        if (playerRequestMask[player-1]) {
            
        }
    }
*/
    // TODO: Make this a subscribable event
    private void IssueJumps() {

    }
}
