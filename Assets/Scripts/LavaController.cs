using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
   private void OnTriggerEnter(Collider other)
   {
     
      if (other.gameObject.CompareTag("Player"))
      {
         // call respawn function that resets player position back to original when hit lava 
         //reset velocity and makes zombies let go of player
          //other.gameObject.SendMessage("Respawn");

         //reset everything when die                                                                                                                                                              
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);


         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


      }
   }

   


   // Update is called once per frame
   void Update()
    {
        
    }
}
