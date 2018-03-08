using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourScript : MonoBehaviour {

    //Bool Which is on by default, it allows simulation with Mouse Touches;
    //public static bool simulateMouseWithTouches;

    //This code if for Touchscreen Controller
    
//    public float speed = 0.1F;
//    void Update()
//    {
//        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
//        {
//            // Get movement of the finger since last frame
//            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//            // Move object across XY plane
//            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
//        }
//    } 
//}


    //Temporary Controller for Mouse
    public float moveSpeed;
    public float offset = 0.05f;
    private bool checkval;

        // initialization
    void Start(){
            checkval = false;
            offset += 10;
           
        }

        // Update is called once per frame
      void Update(){
          if (Input.GetMouseButtonDown(0) && ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude <= offset)){
            if (checkval){
                  checkval = false;
               }
               else{
                  checkval = true;
               }
            }

            if (checkval){
                transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
           }

        }


}
