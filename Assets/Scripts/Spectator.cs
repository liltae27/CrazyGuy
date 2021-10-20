using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spectator : MonoBehaviour
{
    public Material[] colors;
   private Material color;
   public float spawnChance = .7f;

   private float startYLevel; 
   private float jumpTimer;   // tracks time between jump attempts
   public float jumpInterval = 2f;  // time length in seoonds between jump attempts
   public float jumpIntervalDeviance = 0.1f; //
   public float jumpChance = .5f;   // chance of jump occuring 
   private bool isJumping;

   private float jumpArcTime;
   public float jumpDuration = 1f;
   public float jumpHeight = 1f;


    // Start is called before the first frame update
    void Start()
    {
      if (Random.Range(0f, 1f) > spawnChance)
      {
         Destroy(this.gameObject);
         
      }


      startYLevel = gameObject.transform.position.y;
      jumpTimer = 0;
      isJumping = false;
      jumpArcTime = 0f;

      color = colors[Random.Range(0, colors.Length)];
      MeshRenderer ms = GetComponent<MeshRenderer>();
      ms.material = color;
    }

    // Update is called once per frame
    void Update()
    {
      jumpTimer += Time.deltaTime;
      if(jumpTimer > jumpInterval && !isJumping)
      {
         // attempt jump
         if(jumpChance > Random.Range(0f, 1f))
         {
            // if jump succeed
            isJumping = true;
         }
         else
         {
            // jump attempt fails reset timer
            jumpTimer = 0;
            // randomize jump timer
            float randomizer = jumpInterval * jumpIntervalDeviance;
            jumpTimer += Random.Range(-randomizer, randomizer);
         }
      }
      if (isJumping)
      {
         float yAdjust = Mathf.Sin(jumpArcTime/jumpDuration) * jumpHeight;
         
         transform.position = new Vector3(transform.position.x, startYLevel + yAdjust, transform.position.z);
         jumpArcTime += Time.deltaTime;
         if (yAdjust < 0)
         {
            // reset once jump is complete
            isJumping = false;
            jumpTimer = 0;
            jumpArcTime = 0;
            transform.position = new Vector3(transform.position.x, startYLevel, transform.position.z);


         }

      }

    }
}
