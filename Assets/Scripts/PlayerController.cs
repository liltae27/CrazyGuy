using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentPlatform;
    public float runningSpeed;
   [HideInInspector]
   public Vector3 spawnPosition;
   public float jumpForce;
   private bool jumpAllowed;
   [HideInInspector]
   public List<EnemyController> enemyList;

   public void Respawn()
   {
      transform.position = spawnPosition;
      GetComponent<Rigidbody>().velocity = Vector3.zero;
      foreach(EnemyController enemy in enemyList)
      {
         enemy.SendMessage("LetGo");
      }
   }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentPlatform = collision.gameObject;
         jumpAllowed = true;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
      spawnPosition = gameObject.transform.position;
      jumpAllowed = true;

      enemyList = new List<EnemyController>();
      
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space) && jumpAllowed)
      {
         Rigidbody rb = GetComponent<Rigidbody>();
         rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
         jumpAllowed = false;
      }
      
      Vector3 direction = Vector3.zero;
      if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
            if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        if(direction.magnitude > 1)
        {
            direction = direction.normalized;
        }
        
        transform.Translate(direction * runningSpeed * Time.deltaTime);
    }
}
