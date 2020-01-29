using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float verticalVelocity;

    
    private bool onNode = false;
    private Node node;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(onNode)
        {
            currentHealth -= node.healthPerSecond * Time.deltaTime;
        }
        if(currentHealth<=0)
        {
            onPlayerDead();
        }
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Vertical") > 0.001f)
        {
            rb.velocity = new Vector3(0, verticalVelocity * 1, 0);
        }
        else if (Input.GetAxisRaw("Vertical") < -0.001f)
        {
            rb.velocity = new Vector3(0, verticalVelocity * -1, 0);
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
            GameManager.onModuleEnter.Invoke();
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
    }
    
    public void onPlayerDead()
    {
        ScrollMovement.current.velocity = 0;
        rb.velocity = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        ScrollMovement.current.velocity = 10;
        onNode = false;
        node = null;

    }

}
