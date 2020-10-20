using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillController : MonoBehaviour
{

   public enum Axis {X, Y, Z};
   public Axis rotateAxis;
   public float rotateSpeed;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float xAmount = 0f;
      float yAmount = 0f;
      float zAmount = 0f;
      if (rotateAxis == Axis.X)
      {
         xAmount = rotateSpeed;
      }
      else if (rotateAxis == Axis.Y)
      {
         yAmount = rotateSpeed;
      }
      else
      {
         zAmount = rotateSpeed;
      }
      transform.Rotate(xAmount * Time.deltaTime, yAmount * Time.deltaTime, zAmount * Time.deltaTime);
    }
}
