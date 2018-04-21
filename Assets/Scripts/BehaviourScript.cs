using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class BehaviourScript : MonoBehaviour {
//    //Temporary Controller for Mouse
//    public float moveSpeed;
//    public float offset = 0.05f;
//    private bool checkval;
//
//        // initialization
////    void Start(){
////            checkval = false;
////            offset += 10;
////           
////        }
//
//        // Update is called once per frame
//	public Renderer rend;
//	void Start() {
//		rend = GetComponent<Renderer>();
//	}
//	void OnMouseDrag() {
//		transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
//	}
////      void Update(){
////		
////          if (Input.GetMouseButtonDown(0) && ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude <= offset)){
////            if (checkval){
////                  checkval = false;
////               }
////               else{
////                  checkval = true;
////               }
////            }
////
////            if (checkval){
////                transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
////           }
////
////        }
//
//
//}


public class BehaviourScript : MonoBehaviour
{
	public LayerMask touchInputMask;
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;

	private RaycastHit hit;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		#if UNITY_EDITOR
		// if unity editor
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
		touchesOld = new GameObject[touchList.Count];
		touchList.CopyTo(touchesOld);
		touchList.Clear();


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


		if (Physics.Raycast(ray, out hit, touchInputMask))
		{
		GameObject recipient = hit.transform.gameObject;
		touchList.Add(recipient);

		if (Input.GetMouseButtonDown(0))
		{
		recipient.SendMessage("onTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
		}
		if (Input.GetMouseButtonUp(0))
		{
		recipient.SendMessage("onTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
		}
		if (Input.GetMouseButton(0))
		{
		recipient.SendMessage("onTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
		}

		}


		foreach (GameObject g in touchesOld)
		{
		if (!touchList.Contains(g))
		{
		g.SendMessage("onTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
		}
		}
		}

		#endif

		if (Input.touchCount > 0)
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();

			foreach (Touch touch in Input.touches)
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);


				if (Physics.Raycast(ray, out hit, touchInputMask))
				{
					GameObject recipient = hit.transform.gameObject;
					//touchList.Add (recipient);

					if (touch.phase == TouchPhase.Began)
					{
						recipient.SendMessage("onTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Ended)
					{
						recipient.SendMessage("onTouchUp", hit.point, SendMessageOptions.DontRequireReceiver); //orignal
					}
					if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
					{
						recipient.SendMessage("onTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (touch.phase == TouchPhase.Canceled)
					{
						recipient.SendMessage("onTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}
			}

			foreach (GameObject g in touchesOld)
			{
				if (!touchList.Contains(g))
				{
					g.SendMessage("onTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}﻿
