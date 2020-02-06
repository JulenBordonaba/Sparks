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
    public TextMeshProUGUI recordText;


    // Start is called before the first frame update
    void Start()
    {
        current = this;
        LoadRecord();
        baseVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(velocity *  Time.deltaTime, 0, 0);
        score += velocity * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();
        if(score>int.Parse(recordText.text))
        {
            recordText.text= Mathf.FloorToInt(score).ToString();
        }
    }

    public void LoadRecord()
    {
        if (!PlayerPrefs.HasKey("Record"))
        {
            PlayerPrefs.SetInt("Record", 0);
            recordText.text ="0";
        }
        else
        {
            recordText.text = PlayerPrefs.GetInt("Record").ToString();
        }
    }

    public void SaveRecord()
    {
        if(score>PlayerPrefs.GetInt("Record"))
        {
            PlayerPrefs.SetInt("Record", Mathf.FloorToInt(score));
        }
    }

    [ContextMenu("Reset Record")]
    public void ResetRecord()
    {
        PlayerPrefs.DeleteKey("Record");
    }

    [ContextMenu("Set Record")]
    public void SetRecord()
    {
        PlayerPrefs.SetInt("Record",0);
    }

}
