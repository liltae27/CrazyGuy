using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
   public float fallDelay = .3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.tag == "Player")
      {
        StartCoroutine(FallDelay(fallDelay));
   
      }
   }
   IEnumerator FallDelay(float delay)
   {
      yield return new WaitForSeconds(delay);
      GetComponent<Rigidbody>().isKinematic = false;

   }
}
