using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
   private void OnTriggerEnter(Collider other)
   {
      Debug.Log("Collided");
      if (other.gameObject.CompareTag("Player"))
      {
         other.gameObject.SendMessage("Respawn");
      }
   }

   


   // Update is called once per frame
   void Update()
    {
        
    }
}
