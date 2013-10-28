/*
 * Wave Right
 * 		Adapted from Fizbin.Kinect.Gestures project
 * 
 * 		Developed by Kit Chan -- 28/10/2013 
 */

using System;

namespace Kinect.Gestures
{
	public class WaveRightGestureSegment1 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture()
		{
            // hand above elbow
         //   if (data.SkeletonPositions[0].Position.Y > data.Joints[JointType.ElbowLeft].Position.Y)
            {
                // hand right of elbow
             //   if (data.Joints[JointType.HandLeft].Position.X > data.Joints[JointType.ElbowLeft].Position.X)
                {
                    return GesturePartResult.Succeed;
                }

                // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                return GesturePartResult.Pausing;
            }

            // hand dropped - no gesture fails
            return GesturePartResult.Fail;
		}		
	}
	public class WaveRightGestureSegment2 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture()
		{
			return 0;
		}		
	}
}

