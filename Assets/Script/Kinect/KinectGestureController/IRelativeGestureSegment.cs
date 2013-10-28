using System;

namespace Kinect.Gestures
{
	/// <summary>
	/// Interface to a Gesture Segment
	/// </summary>
	public interface IRelativeGestureSegment
	{
		GesturePartResult CheckGesture();//(NuiSkeletonData skeleton);
	}
}

