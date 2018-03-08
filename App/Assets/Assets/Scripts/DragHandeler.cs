//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class DragHandeler : MonoBehaviour, IBeginDragHandler , IDragHandler, IEndDragHandler
//{
//    public static GameObject itemBeingDragged;
//    Vector3 startPosition;

//    #region IBeginDragHandler implementation    

//    public void onBeingDrag(PointerEventData eventData)
//    {
//        itemBeingDragged = gameObject;
//        startPosition = transform.position;
//        startParent = transform.parent;
//        GetComponent<CanvasGroup>().blocksRaycasts = false;
//    }

//    #endregion

//    #region IDragHandler Implementation

//    public void OnDrag(PointerEventData eventData)
//    {
//        throw new System.NotImplementedException();
//    }

//    #endregion

//    #region IEndDragHandler implementation

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        itemBeingDragged = null;
//        GetComponent<CanvasGroup>().blocksRaycasts = true;
//        if (transform.parent != startParent)
//        {
//            transform.position = startPosition;
//        }
//    }


//    #endregion


//}
