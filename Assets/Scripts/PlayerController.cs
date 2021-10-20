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
   private bool controlsReversed;
   public float powerUpDuration;
   public float rotationSpeed = 100f;
   public ForceMode horizMovFM = ForceMode.Force;
   [HideInInspector]
   public Rigidbody rb;
   private bool movementAllowed;
  
   public GameObject invertFlamePrefab;

   public float maxHorizSpeed = 50;

   [Tooltip("Value between 0 and 1, used to control movement levels while airborne/on manipulated material.")]
   public float movCtrlMult = 1;
   public bool toggle = false;

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

   private void OnCollisionExit(Collision collision)
   {
      if (collision.gameObject.CompareTag("Ground"))
      {
         currentPlatform = collision.gameObject;
         jumpAllowed = false;
      }
   }

// Start is called before the first frame update
   void Start()
   {
      movementAllowed = true;
      rb = GetComponent<Rigidbody>();

      controlsReversed = false;

      spawnPosition = gameObject.transform.position;
      jumpAllowed = true;

      enemyList = new List<EnemyController>();
      
   }

    // Update is called once per frame
   void Update()
   {

      // Raycast to check movement control multiplier 
      RaycastHit hit;

      if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f))
      {
        //Debug.Log("Found an object - distance: " + hit.distance);
         Debug.Log(hit.collider.material.dynamicFriction);
         //movCtrlMult = hit.collider.material.dynamicFriction/0.36f;
      }
        

      


      //Jump when space bar is pressed and jumping is allowed, disallow mid air jump 
      if (Input.GetKeyDown(KeyCode.Space) && jumpAllowed)
      {
         
         rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
         
      }
      
      // Rotation
      if (Input.GetKey(KeyCode.E)) {transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); }
     
      if (Input.GetKey(KeyCode.Q)) {transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);}

      // WASD movement
      float horizInp = Input.GetAxisRaw("Horizontal");
      float vertInp = Input.GetAxisRaw("Vertical");


      // Apply the character's current rotation to the movement force
      Vector3 moveForce = new Vector3(horizInp * runningSpeed, 0f, vertInp * runningSpeed);

      // modifies aerial/modified movement tolerance levels (e.g. ice)
      moveForce *= movCtrlMult;
      if (Input.GetKeyDown(KeyCode.T))
      {
         Debug.Log(moveForce);
      }
      if (Input.GetKeyDown(KeyCode.Y)  && Random.Range(0f, 1.0f) < 0.5f)
      {
         toggle = !toggle;
         runningSpeed = toggle ? runningSpeed * 2 : runningSpeed / 2;
         movCtrlMult = toggle ? movCtrlMult / 2 : movCtrlMult * 2;
      }
      //reverse controls powerup active 
      if (controlsReversed)
      {

         moveForce *= -1;

      }
      if (movementAllowed)
      {
         rb.AddForce(transform.rotation.normalized * moveForce, horizMovFM);

      }


      /*
      // Movement 
      Vector3 direction = Vector3.zero;


      direction += Input.GetAxis("Vertical") * Vector3.forward;
      direction += Input.GetAxis("Horizontal") * Vector3.right;

      if(direction.magnitude > 1) 
      {
         direction = direction.normalized;
      }
     

      transform.Translate (transform.rotation.normalized * direction * runningSpeed * Time.deltaTime);
      */
   }
   private void FixedUpdate()
   {
      //caps speed 
      Vector3 horizVect = new Vector3(rb.velocity.x, 0, rb.velocity.z);
      float horizMag = horizVect.magnitude;
      if ( horizMag > maxHorizSpeed)
      {
         Vector3 newHorizVect = horizVect.normalized * maxHorizSpeed;
         rb.velocity = new Vector3(newHorizVect.x, rb.velocity.y, newHorizVect.z);
      }
   }
   private void OnTriggerEnter(Collider other)
   {
      //Collision with reverse control powerup 
      if (other.gameObject.tag == "Poweup")
      {
         //Removes powerup from scene and applies effect for powerUpDuration
         Destroy(other.gameObject);
         StartCoroutine(ApplyReverseControlPowerup(powerUpDuration));
      }
      
   }
   //Inverts player controls for powerUpDuration 
   private IEnumerator ApplyReverseControlPowerup(float powerUpDuration)
   {
      //Apply powerup visual effect
      GameObject go = Instantiate(invertFlamePrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
      go.transform.parent = gameObject.transform;

      //reverse control flag 
      controlsReversed = true;

      //manipulate player speed
      
      rb.velocity = rb.velocity * -1;

      //powerup duration
      yield return new WaitForSeconds(powerUpDuration);

      //reverse control flag back to normal 
      controlsReversed = false;

      //Remove visual effect
      Destroy(go);

      yield break;
   }

   public void Boop(Vector3 boopForce, float uncapDuration)
   {
      rb.AddForce(boopForce, ForceMode.Impulse);
      StartCoroutine(UncapMaxSpeed(uncapDuration));
      Debug.Log("boop function called");
   }




   //temporarily uncaps maxHorizSpeed
   public IEnumerator UncapMaxSpeed(float uncapDuration)
   {
      if (!movementAllowed)
      {
         yield break;
      }
      movementAllowed = false;
      float currMaxSpeed = maxHorizSpeed;
      maxHorizSpeed = float.PositiveInfinity;
      yield return new WaitForSeconds(uncapDuration);
      maxHorizSpeed = currMaxSpeed;
      movementAllowed = true;
   }
}
