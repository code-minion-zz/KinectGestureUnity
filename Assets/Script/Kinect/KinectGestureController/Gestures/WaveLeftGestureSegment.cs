/*
 * Wave Left
 * 		Adapted from Fizbin.Kinect.Gestures project
 * 
 * 		Developed by Kit Chan -- 28/10/2013 
 */

using System;

namespace Kinect.Gestures
{
	public class WaveLeftGestureSegment1 : IRelativeGestureSegment
	{		
		public GesturePartResult CheckGesture()
		{
			KinectPointController kpc = Gesture.pointController;
			
            // hand above elbow
            if (kpc.Hand_Left.transform.position.y > kpc.Elbow_Left.transform.position.y)
            {
                // hand right of elbow
                if (kpc.Hand_Left.transform.position.x > kpc.Elbow_Left.transform.position.x)
                {
                    return GesturePartResult.Succeed;
                }

                // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                return GesturePartResult.Pausing;
            }

            // hand dropped or no gesture detected
            return GesturePartResult.Fail;
        }				
	}
	
	public class WaveLeftGestureSegment2 : IRelativeGestureSegment
	{		
		public GesturePartResult CheckGesture()
		{
			KinectPointController kpc = Gesture.pointController;
            // hand above elbow
            if (kpc.Hand_Left.transform.position.y > kpc.Elbow_Left.transform.position.y)
            {
                // hand left of elbow
                if (kpc.Hand_Left.transform.position.x < kpc.Elbow_Left.transform.position.x)
                {
                    return GesturePartResult.Succeed;
                }

                // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                return GesturePartResult.Pausing;
            }

            // hand dropped or no gesture detected
            return GesturePartResult.Fail;
		}		
	}
}

