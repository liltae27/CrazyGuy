using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         int current = SceneManager.GetActiveScene().buildIndex;
         if(current + 1 < SceneManager.sceneCountInBuildSettings)
             SceneManager.LoadScene(current + 1);
      }
   }

  
   // Update is called once per frame
   void Update()
    {
      
    }
}
