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

    }


    public void InstantiateModule(bool isStart)
    {
        int module = Random.Range((int)0, (int)modules.Length);
        int connectionModule = Random.Range((int)0, (int)conectionModules.Length);
        

        float modulePos = isStart ? 16 : connectionModuleLength + moduleLength;
        float connectionModulePos = modulePos + modules[module].GetComponent<Module>().length;

        Instantiate(modules[module], new Vector3(modulePos, 0, 0), Quaternion.identity).transform.parent = escenario.transform;
        Instantiate(conectionModules[connectionModule], new Vector3(connectionModulePos, 0, 0), Quaternion.identity).transform.parent = escenario.transform;

        connectionModuleLength = conectionModules[connectionModule].GetComponent<Module>().length;
        moduleLength = modules[module].GetComponent<Module>().length;
    }
}
