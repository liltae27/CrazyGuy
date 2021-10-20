using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float runningSpeed;
    public PlayerController player;
   [HideInInspector]
    public bool grabbing;
    public float pushAmount;
    private Vector3 distanceToPlayer;
   public float boopForce;
   public float killHeight;
    public void LetGo()
   {
      grabbing = false;
   }
    void UpdateMovement()
    {
        Vector3 direction = Vector3.zero;

        direction = player.transform.position - this.transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        direction = direction.normalized;

        transform.Translate(direction * runningSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
      PlayerController PC = collision.gameObject.GetComponent<PlayerController>();
      //if hit player  
        if (PC !=null)
        {
         //player higher than zombie and hit on head then die
         if (PC.gameObject.transform.position.y > transform.position.y + killHeight)
         {
            Destroy(gameObject);

            PC.rb.AddForce(new Vector3(0, boopForce, 0), ForceMode.Impulse);
         }
         //grab player
         else
         {
            
           // gameObject.tag = "Player"; //zombie grab player on fall floor it falls

            grabbing = true;
            PC.enemyList.Add(this);
            distanceToPlayer = this.transform.position - player.transform.position;
         }
   
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        grabbing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbing)
        {
            UpdateMovement();
        }
        else
        {
            float x = player.transform.position.x;
            float w = player.currentPlatform.transform.localScale.x;
            float z = player.transform.position.z;
            float h = player.currentPlatform.transform.localScale.z;
            float Bx = player.currentPlatform.transform.position.x;
            float Bz = player.currentPlatform.transform.position.z;

            float leftDistance = x - (Bx - w/2);
            float topDistance = (Bz + h/2) - z;


            Vector3 pushVariable = new Vector3(0, 0, 0);
            if (topDistance < h - topDistance)
            {
                if(leftDistance < w - leftDistance)
                {
                    if (topDistance < leftDistance)
                    {
                        // closest to top   
                        pushVariable = new Vector3(0f, 0f, 1f);
                    }
                    else
                    {
                        // closest to left
                        pushVariable = new Vector3(-1f, 0f, 0f);
                    }
                }
                else
                {
                    if (topDistance < w - leftDistance)
                    {
                        // closest to top
                        pushVariable = new Vector3(0f, 0f, 1f);
                    }
                    else
                    {
                        // closest to right
                        pushVariable = new Vector3(1f, 0f, 0f);
                    }
                }
            }
            else
            {
                if (leftDistance < w - leftDistance)
                {
                    if (h - topDistance < leftDistance)
                    {
                        // closest to bottom
                        pushVariable = new Vector3(0f, 0f, -1f);
                    }
                    else
                    {
                        // closest to left
                        pushVariable = new Vector3(-1f, 0f, 0f);
                    }
                }
                else
                {
                    if (h - topDistance < w - leftDistance)
                    {
                        // closest to bottom
                        pushVariable = new Vector3(0f, 0f, -1f);
                    }
                    else
                    {
                        // closest to right
                        pushVariable = new Vector3(1f, 0f, 0f);
                    }
                }
            }
            player.gameObject.GetComponent<Rigidbody>().AddForce(pushVariable * pushAmount);
         Vector3 enemyMovement = player.transform.position + distanceToPlayer;
         this.transform.position = new Vector3(enemyMovement.x, transform.position.y, enemyMovement.z);


        }

    }
}
