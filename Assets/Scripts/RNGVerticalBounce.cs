using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGVerticalBounce : MonoBehaviour
{
      public float bounceRange;
      public float bounceSpeed;
   public float bounceTime;
   private float RNGTime;
   private List<float> RNGList;
   private int yDirection;
   private float startingY;
    // Start is called before the first frame update
    void Start()
    {
      startingY = transform.position.y;
      yDirection = 0;

      RNGList = new List<float>() { };
      for(int i = 0; i < 1000; i++)
      {
         RNGList.Add(Random.Range(0f, bounceTime));
      }
      RNGTime = RNGList[0];
      RNGList.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
      
      RNGTime -= Time.deltaTime;
      transform.Translate(new Vector3(0, bounceSpeed * yDirection * Time.deltaTime, 0));
      //Know when you're at the top of range by the y value of the wall > than original y value + bouncerange//
      //when you are at top of the range tell the wall to start decreasing y again until it gets back to original y value//
      if(RNGTime < 0)
      {
         RNGTime = RNGList[0];
         RNGList.RemoveAt(0);
         //Keep track of where we started and tell computer we're going to start going up until we get to the top of that range//
         yDirection = 1;
          
      }
      if(transform.position.y > startingY + bounceRange)
      {
         yDirection = -1;
      }
      if(transform.position.y < startingY)
      {
         yDirection = 0;
         transform.position = new Vector3(transform.position.x, startingY, transform.position.z);
      }
    }
}
