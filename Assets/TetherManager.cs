using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherManager : MonoBehaviour
{
    public float RANGE_1_MAX = 4.0f;
    public float RANGE_2_MAX = 16.0f;
    public float RANGE_3_MAX = 32.0f;

    public float SHAPE_SCALE = 0.5f;

    public AvatarController player1, player2;
    private ParticleSystem ps;
    public ParticleSystem tendrils;

    private Vector3 pos1, pos2;
    private float sqrDistance;
    private Vector3 direction;
    private Vector3 centroid;

    private bool BuddyJump = false;

    void Start()
    {
        pos1 = Vector3.zero;
        pos2 = Vector3.zero;
        direction = Vector3.zero;
        centroid = Vector3.zero;

        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (player1 != null && player2 != null) {
            pos1 = player1.transform.position;
            pos2 = player2.transform.position;
        }
        direction = pos1 - pos2;
        centroid = (pos1 + pos2)/2.0f;
        sqrDistance = Vector3.SqrMagnitude(pos1 - pos2);

        if (sqrDistance < RANGE_1_MAX) {
            Debug.DrawLine(pos1, pos2, Color.green);
            BuddyJump = true;
        }
        else if (sqrDistance < RANGE_2_MAX) {
            Debug.DrawLine(pos1, pos2, Color.yellow);
        }
        else if (sqrDistance < RANGE_3_MAX) {
            Debug.DrawLine(pos1, pos2, Color.red);
        }

        var shape = ps.shape;
        shape.scale = direction * SHAPE_SCALE;
        transform.position = centroid;
        transform.right = direction;
        
    }

    public bool attemptUseBuddyJump() {
        bool temp = BuddyJump;
        BuddyJump = false;
        return temp;
    }
}
