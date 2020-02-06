using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController current;

    public Rigidbody rb;

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float verticalVelocity;
    public float healthPerJump = 1;

    
    private bool onNode = false;
    private Node node;
    private float accelerationTime = 0;
    private bool inAcceleration = false;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (PauseManager.inPause) return;
        if(onNode)
        {
            currentHealth -= node.healthPerSecond * Time.deltaTime;
        }
        if(currentHealth<=0)
        {
            onPlayerDead();
        }
        Accelerate();
    }

    private void Move()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && rb.velocity.y<=0)
        {
            rb.velocity = new Vector3(0, verticalVelocity * 1, 0);
            currentHealth -= healthPerJump;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && rb.velocity.y>=0)
        {
            rb.velocity = new Vector3(0, verticalVelocity * -1, 0);
            currentHealth -= healthPerJump;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Cable"))
        {
            Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cable"))
        {
            Vector3 pos = transform.position;
            pos.y = other.transform.position.y;
            transform.position = pos;
            rb.velocity = Vector3.zero;
        }
        else if(other.CompareTag("Obstacle"))
        {
            onPlayerDead();
        }
        else if(other.CompareTag("ModuleStart"))
        {
            print("pasa_por_module_start");
            //GameManager.onModuleEnter.Invoke();
        }
        else if (other.CompareTag("Node"))
        {
            node = other.GetComponent<Node>();
            StartCoroutine( OnNodeEnter(node));
        }
        else if (other.CompareTag("Resistance"))
        {
            currentHealth -= other.GetComponent<Resistencia>().damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
        else if (other.CompareTag("Batery"))
        {
            currentHealth += other.GetComponent<Batery>().health;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
        else if (other.CompareTag("Accelerator"))
        {
            Accelerator ac = other.GetComponent<Accelerator>();
            accelerationTime = ac.duration;
            ScrollMovement.current.velocity = ac.velocity;
            inAcceleration = true;
        }
    }
    
    public void onPlayerDead()
    {
        ScrollMovement.current.velocity = 0;
        rb.velocity = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Accelerate()
    {
        

        if(accelerationTime>0)
        {
            accelerationTime -= Time.deltaTime;
        }
        else if (accelerationTime<=0 && inAcceleration==true)
        {
            accelerationTime = 0;
            inAcceleration = false;
            ScrollMovement.current.velocity = ScrollMovement.current.baseVelocity;
        }

        
    }

    public IEnumerator OnNodeEnter(Node n)
    {
        onNode = true;
        ScrollMovement.current.velocity = 0;
        transform.position = n.transform.position;
        while(Input.GetAxisRaw("Vertical")==0 && Input.GetAxisRaw("Horizontal")<=0)
        {
            yield return null;
        }
        ScrollMovement.current.velocity = ScrollMovement.current.baseVelocity;
        onNode = false;
        node = null;

    }

}
