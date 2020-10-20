using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnPoint : MonoBehaviour
{
   public float spawnDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         collision.gameObject.GetComponent<PlayerController>().spawnPosition = gameObject.transform.position + Vector3.up * spawnDistance;
      }
   }

   // Update is called once per frame
   void Update()
    {
        
    }
}
