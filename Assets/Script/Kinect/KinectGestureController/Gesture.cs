/*
 * Gesture.cs - A Hand gesture, segments represent different parts of a gesture if they are too long.
 * 	http://blogs.msdn.com/b/mcsuksoldev/archive/2011/08/08/writing-a-gesture-service-with-the-kinect-for-windows-sdk.aspx
 * 
 * 		Developed by Kit Chan -- 28/10/2013
 * 
 */

using System;

namespace Kinect.Gestures
{
	/// <summary>
	/// Gesture. A collection of 
	/// </summary>
	public class Gesture
	{
		public static KinectPointController pointController;
		
		/// <summary>
		/// The gesture parts.
		/// </summary>
		private IRelativeGestureSegment[] gestureParts;
		
		private int currentGesturePart = 0;
		
		/// <summary>
		/// The paused frame count.
		/// </summary>
		private int pausedFrameCount = 10;
    
	    /// <summary>
	    /// The current frame that we are on
	    /// </summary>
	    private int frameCount = 0;
	 
	    /// <summary>
	    /// Are we paused?
	    /// </summary>
	    private bool paused = false;
	 
	    /// <summary>
	    /// The type of gesture that this is
	    /// </summary>
	    private string name;
	 
	    /// <summary>
	    /// Initializes a new instance of the <see cref="Gesture"/> class.
	    /// </summary>
	    /// <param name="type">The type of gesture.</param>
	    /// <param name="gestureParts">The gesture parts.</param>
	    public Gesture(string name, IRelativeGestureSegment[] gestureParts)
	    {
	        this.gestureParts = gestureParts;
	        this.name = name;
	    }
	 
	    /// <summary>
	    /// Occurs when [gesture recognised].
	    /// </summary>
	    public event EventHandler<GestureEventArgs> GestureRecognized;
	 
	    /// <summary>
	    /// Updates the gesture.
	    /// </summary>
	    /// <param name="data">The skeleton data.</param>
	    public void UpdateGesture(NuiSkeletonData data)
	    {
	        if (this.paused)
	        {
	            if (this.frameCount == this.pausedFrameCount)
	            {
	                this.paused = false;
	            }
	 
	            this.frameCount++;
	        }
	 
	        //GesturePartResult result = this.gestureParts[this.currentGesturePart].CheckGesture(data);
	        GesturePartResult result = this.gestureParts[this.currentGesturePart].CheckGesture();
	        if (result == GesturePartResult.Succeed)
	        {
	            if (this.currentGesturePart + 1 < this.gestureParts.Length)
	            {
	                this.currentGesturePart++;
	                this.frameCount = 0;
	                this.pausedFrameCount = 10;
	                this.paused = true;
	            }
	            else
	            {
	                if (this.GestureRecognized != null)
	                {
	                    this.GestureRecognized(this, new GestureEventArgs(this.name, data.dwTrackingID));
	                    this.Reset();
	                }
	            }
	        }
	        else if (result == GesturePartResult.Fail || this.frameCount == 50)
	        {
	            this.currentGesturePart = 0;
	            this.frameCount = 0;
	            this.pausedFrameCount = 5;
	            this.paused = true;
	        }
	        else
	         {
	             this.frameCount++;
	             this.pausedFrameCount = 5;
	             this.paused = true;
	         }
	     }
	  
	     /// <summary>
	     /// Resets this instance.
	     /// </summary>
	     public void Reset()
	     {
	         this.currentGesturePart = 0;
	         this.frameCount = 0;
	         this.pausedFrameCount = 5;
	         this.paused = true;
	     }
	}
}

