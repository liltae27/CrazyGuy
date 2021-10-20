using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveBoopController : MonoBehaviour
{
      public GameObject player;
      public float detectionRange = 2;
      public float punchSpeed;
      public float punchDelay;
      private bool isPunching;
      public float uncapDuration = .2f;
      public float punchForce; 
      // Start is called before the first frame update
    void Start()
    {
      isPunching = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //detects if player passes glove 
      if (player.transform.position.z > transform.position.z - detectionRange && player.transform.position.z < transform.position.z + detectionRange )
      {
//player in range do delay
         StartCoroutine(Punch(punchDelay));
      }
      //if isPunching then go 
      if (isPunching)
      {
         float movAmt = punchSpeed * Time.fixedDeltaTime;
         transform.position = new Vector3(transform.position.x + movAmt, transform.position.y, transform.position.z);
      }
    }

   //punchDelay then Punch
   IEnumerator Punch(float punchDelay)
   {
      yield return new WaitForSeconds(punchDelay);

      isPunching = true;
   }

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.collider.tag == "Player") 
      { 
    
         Debug.Log(collision.collider.tag);
         player.GetComponent<PlayerController>().Boop(new Vector3(punchForce, 0, 0), uncapDuration);
      }

      
    
   }




}
