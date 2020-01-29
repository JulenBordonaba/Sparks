using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMovement : MonoBehaviour
{
    public static ScrollMovement current;

    public float velocity = 10f;


    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(velocity *  Time.deltaTime, 0, 0);
    }
}
