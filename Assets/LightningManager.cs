using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public float RANGE_1_MAX = 4.0f;
    public float RANGE_2_MAX = 16.0f;
    public float RANGE_3_MAX = 32.0f;

    public float SHAPE_SCALE = 0.5f;

    public AvatarController player1, player2;
    private LineRenderer lr;

    private Vector2 pos1, pos2;
    private float sqrDistance;
    private Vector2 direction;
    private Vector2 centroid;
    private Vector2 offset; 

    public float sqrRadius = 10.0f;

    private int i = 0;
    private bool player1turn = true;

    public int MAX_WAIT_TICKS = 5;
    private int waitTicks = 0;

    private Vector3[] Points;

    private bool BuddyJump = false;

    void Start()
    {
        Points = new Vector3[128];

        pos1 = Vector2.zero;
        pos2 = Vector2.zero;
        direction = Vector2.zero;
        centroid = Vector2.zero;
        offset = Vector2.zero;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Points != null){
             lr.positionCount = Points.Length;
             for(int i = 0; i < Points.Length; i++){
                 lr.SetPosition(i, Points[i]);    
             }
         }
         else {
             lr.positionCount = 0;
             i = 0;
         }

        if (waitTicks > 0) {
            waitTicks--;
            if (waitTicks == 0) {
                Points = new Vector3[128];
                lr.SetPositions(Points);
                lr.SetPosition(i++, (player1turn? new Vector3(pos1.x, pos1.y, 0.0f) : new Vector3(pos2.x, pos2.y, 0.0f)));
            }
        } else {
            LightningUpdate();
        }
    }

    void LightningUpdate() {
        if (player1 != null && player2 != null) {
            pos1 = new Vector2(player1.transform.position.x, player1.transform.position.y);
            pos2 = new Vector2(player2.transform.position.x, player2.transform.position.y);
        }
        offset = player1turn? pos2 - pos1 : pos1 - pos2;
        direction = offset.normalized;
        centroid = (pos1 + pos2)/2.0f;
        sqrDistance = Vector2.SqrMagnitude(pos1 - pos2);

        Vector2 u = Random.insideUnitCircle;
        if (sqrRadius > offset.sqrMagnitude) {
            lr.SetPosition(i++, (player1turn? new Vector3(pos2.x, pos2.y, 0.0f) : new Vector3(pos1.x, pos1.y, 0.0f)));
            player1turn = !player1turn;
            waitTicks = MAX_WAIT_TICKS;
            i = 0;
        }
        Vector2.Dot(u, direction.normalized);
    }

    public bool attemptUseBuddyJump() {
        bool temp = BuddyJump;
        BuddyJump = false;
        return temp;
    }
}
