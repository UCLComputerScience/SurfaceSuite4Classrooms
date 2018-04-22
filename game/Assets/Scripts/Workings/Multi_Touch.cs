//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Multi_Touch : MonoBehaviour {
//
//	switch (gi.dwID)
//	{
//	case GID_BEGIN:
//	case GID_END:
//		break;
//		(...)
//	case GID_PAN:
//		switch (gi.dwFlags)
//		{
//		case GF_BEGIN:
//			_ptFirst.X = gi.ptsLocation.x;
//			_ptFirst.Y = gi.ptsLocation.y;
//			_ptFirst = PointToClient(_ptFirst);
//			break;
//
//		default:
//			// We read the second point of this gesture. It is a
//			// middle point between fingers in this new position
//			_ptSecond.X = gi.ptsLocation.x;
//			_ptSecond.Y = gi.ptsLocation.y;
//			_ptSecond = PointToClient(_ptSecond);
//
//			// We apply move operation of the object
//			_dwo.Move(_ptSecond.X - _ptFirst.X, _ptSecond.Y - _ptFirst.Y);
//
//			Invalidate();
//
//			// We have to copy second point into first one to
//			// prepare for the next step of this gesture.
//			_ptFirst = _ptSecond;
//			break;
//		}
//		break;
//}
