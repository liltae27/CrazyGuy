using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   // Reference to player GameObject
   public GameObject player;

   // Smooth Camera Movement Vars
   public float smoothTime = 0.3F;
   private Vector3 velocity = Vector3.zero;

   public float smoothTimeR = 0.2F;
   private Vector3 velocityR = Vector3.zero;

   // Initial Position of Camera relative to player
   private Vector3 camDistToPlayer;

   // Position tracker
   private Vector3 posTracker;

   private void Start()
   {
      camDistToPlayer = player.transform.position - transform.position;
      posTracker = player.transform.position;
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      // if no player to track skip the follow code 
      if (!GetPlayerTarget())
      {
         return;
      }

      // Get player's current position
      Vector3 playerPos = player.transform.position;

      // Smoothly follow player's rotation
      Quaternion newRot = Quaternion.Slerp(transform.rotation, player.transform.rotation, smoothTimeR);

      // Update camera's rotation
      transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newRot.eulerAngles.y, transform.rotation.eulerAngles.z);

      // Smoothly follow player's movement
      Vector3 newPos = Vector3.SmoothDamp(posTracker, playerPos, ref velocity, smoothTime);
      posTracker = newPos;

      // Update camera's position, bearing in mind rotation of initial distance from player
      Vector3 rotatedCamDist = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0) * camDistToPlayer;
      transform.position = newPos - rotatedCamDist;


      // Follow the player's rotation - janky way
      //transform.RotateAround(playerPos, Vector3.up, player.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y);


   }



   bool GetPlayerTarget()
   {
      if(player == null)
      {
         player = GameObject.FindGameObjectWithTag("Player");
      }
      return player != null;
   }



   /*
    // Reference to player GameObject
    public GameObject player;

    // Smooth Camera Movement Vars
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

   private Vector3 initialPos;
   private Vector3 currentPos;
   private void Start()
   {
      initialPos = transform.position - player.transform.position;
      currentPos = initialPos;
   }

   // Update is called once per frame
   void FixedUpdate()
    {
        // Get player's current position
        Vector3 playerPos = player.transform.position;

        Vector3 newPos = Vector3.SmoothDamp(currentPos, playerPos, ref velocity, smoothTime);
      currentPos = newPos; 

      transform.position = newPos +  transform.rotation.normalized * initialPos;
      transform.RotateAround(playerPos, Vector3.up, player.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y);
    }
   */
}