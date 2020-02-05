using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollMovement : MonoBehaviour
{
    public static ScrollMovement current;

    public float velocity = 10f;

    public float baseVelocity;

    public float score = 0;

    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        current = this;
        baseVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(velocity *  Time.deltaTime, 0, 0);
        score += velocity * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
