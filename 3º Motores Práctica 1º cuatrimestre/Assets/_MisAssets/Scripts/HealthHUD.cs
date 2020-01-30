using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public PlayerController player;
    public Image healthbar;

    private float startPosition;
    private float minPosition;
    // Start is called before the first frame update
    void Start()
    {
        minPosition = -healthbar.rectTransform.rect.width;
        startPosition = healthbar.rectTransform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = healthbar.rectTransform.localPosition;
        pos.x = Mathf.Lerp(minPosition, startPosition, healthProportion);
        healthbar.rectTransform.localPosition = pos;
    }



    public float healthProportion
    {
        get { return player.currentHealth / player.maxHealth; }
    }

}
