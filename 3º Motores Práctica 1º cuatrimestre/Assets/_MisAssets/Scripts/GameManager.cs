using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static UnityEvent onModuleEnter;

    public PlayerController player;
    public GameObject[] modules;
    public GameObject[] conectionModules;
    public GameObject escenario;
    public Text healthText;

    private float moduleLength = 10f;
    private float connectionModuleLength = 10f;


    // Start is called before the first frame update
    void Start()
    {
        onModuleEnter = new UnityEvent();
        onModuleEnter.AddListener(delegate{ InstantiateModule(false); });
        InstantiateModule(true);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.currentHealth + "/" + player.maxHealth;
    }


    public void InstantiateModule(bool isStart)
    {
        int module = Random.Range((int)0, (int)modules.Length);
        int connectionModule = Random.Range((int)0, (int)conectionModules.Length);

        connectionModuleLength = conectionModules[connectionModule].GetComponent<Module>().length -2;
        moduleLength = modules[module].GetComponent<Module>().length -2;

        float modulePos = isStart ? connectionModuleLength : connectionModuleLength + moduleLength;

        Instantiate(modules[module], new Vector3(modulePos, 0, 0), Quaternion.identity).transform.parent = escenario.transform;
        Instantiate(conectionModules[connectionModule], new Vector3(modulePos+moduleLength, 0, 0), Quaternion.identity).transform.parent = escenario.transform;
    }
}
