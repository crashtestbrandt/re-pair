using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float ySpeedMax = 0.5f;
    public float yHeightMax = 1.0f;
    public float yHeightMin = -1.0f;

    private float absRange;
    // Start is called before the first frame update
    void Start()
    {
        absRange = Mathf.Abs(yHeightMax - yHeightMin);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yVelocity = ySpeedMax * ((transform.position.y-yHeightMin)/absRange) * 2 * Mathf.PI;
        transform.position += new Vector3(0.0f, Time.fixedDeltaTime * yVelocity, 0.0f);
    }
}
