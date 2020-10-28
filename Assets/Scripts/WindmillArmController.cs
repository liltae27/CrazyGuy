using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillArmController : MonoBehaviour
{
   private WindmillController Parent;
    // Start is called before the first frame update
    void Start()
    {
      Parent = this.transform.parent.gameObject.GetComponent<WindmillController>();
    }

   private void OnCollisionEnter(Collision collision)
   {
      Debug.Log("We collided with it");
      if (collision.gameObject.CompareTag("Player"))
      {
         Parent.launchPlayer(collision.gameObject);
      }
   }
}
