using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillController : MonoBehaviour
{
   public float launchAmount;
   public float playerSpeedUncapDur = 2f;
   public float rotateSpeed;
   public void launchPlayer(GameObject player)
   {
      Vector3 WindmillForce = new Vector3(launchAmount * (rotateSpeed >= 0 ? 1 : -1), 0, 0);
      WindmillForce = Quaternion.Euler(0, transform.eulerAngles.y, 0) * WindmillForce;
      player.GetComponent<PlayerController>().Boop(WindmillForce, playerSpeedUncapDur);
   }
   

   

    // Update is called once per frame
    void Update()
    {
      transform.RotateAround(transform.position, transform.forward, Time.deltaTime * rotateSpeed);
    }
}
