using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   // public  player;
   Vector2 startpos;
   public int pixelDisttoDetect = 50;
   private bool fingerdown;
    bool isMoving = false;
   public int speed = 300;
    public bool UnlockLeft = true;
     public bool UnlockRight = true;
      public bool UnlockDown = true;
       public bool UnlockUp = true;

    void Update() {
        if (isMoving) {
            return;
        }
    if (fingerdown == false && Input.GetMouseButtonDown(0))
    {
        startpos = Input.mousePosition;         
        fingerdown = true;
    }

    if (fingerdown)
    {
        if (Input.mousePosition.y >= startpos.y + pixelDisttoDetect)
        {
            if(UnlockUp == true)
            {
            fingerdown = false;
            StartCoroutine(Roll(Vector3.forward));
            Debug.Log("UP");
        }
        }
         else if (Input.mousePosition.x <= startpos.x - pixelDisttoDetect) 
         {
            if(UnlockLeft == true)
            {
             fingerdown = false;
            StartCoroutine(Roll(Vector3.left));
            Debug.Log("LEFT");
            }
        }else if (Input.mousePosition.x >= startpos.x + pixelDisttoDetect)
         {
          if(UnlockRight == true)
            {
            fingerdown = false;
            StartCoroutine(Roll(Vector3.right));
            Debug.Log("Right");
            }
        } else if (Input.mousePosition.y <= startpos.y - pixelDisttoDetect) 
        {
            if(UnlockDown == true)
           {
            fingerdown = false;
            StartCoroutine(Roll(Vector3.back));
            Debug.Log("DOWN");
        }
        }
         if(fingerdown && Input.GetMouseButtonUp(0))
        {
            fingerdown = false;
        }
    }
    /*
      // Mobile Input
     if (fingerdown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    {
        startpos = Input.touches[0].position;         
        fingerdown = true;
    }
    if (fingerdown)
    {
        if (Input.touches[0].position.y >= startpos.y + pixelDisttoDetect)
        {
             if(UnlockUp == true)
            {
            fingerdown = false;
            StartCoroutine(Roll(Vector3.forward));
        }
        }
         else if (Input.touches[0].position.x <= startpos.x - pixelDisttoDetect) 
         {
             if(UnlockLeft == true)
            {
             fingerdown = false;
            StartCoroutine(Roll(Vector3.left));
            }
        } else if (Input.touches[0].position.x >= startpos.x + pixelDisttoDetect)
         {
              if(UnlockRight == true)
            {
            fingerdown = false;
            StartCoroutine(Roll(Vector3.right));
            }
        } else if (Input.touches[0].position.y <= startpos.y - pixelDisttoDetect) 
        {
            if(UnlockDown == true)
            {
            fingerdown = false;
            StartCoroutine(Roll(Vector3.back));
            }
        }
         if(fingerdown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerdown = false;
        }
    }
    */
    

    IEnumerator Roll(Vector3 direction) {
        isMoving = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + Vector3.left / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, Vector3.left);

        while (remainingAngle > 0) {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        isMoving = false;

    }
}
}



