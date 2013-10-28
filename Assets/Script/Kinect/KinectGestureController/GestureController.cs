/*
 * GestureController.cs - Contains Gestures, in charge of Updating Gesture data.
 * 					Adapted from Fizbin.Kinect.Gestures and MSDN's article for Kinect.
 * 	http://blogs.msdn.com/b/mcsuksoldev/archive/2011/08/08/writing-a-gesture-service-with-the-kinect-for-windows-sdk.aspx
 * 
 * 		Developed by Kit Chan -- 28/10/2013
 * 
 */
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Kinect.Gestures
{
	public class GestureController : MonoBehaviour
	{
		public KinectPointController pointController;
		public KinectSensor sensor;
		public DeviceOrEmulator devOrEmu;
			
		/// <summary>
		/// List of gestures to check for every interval
		/// </summary>
		private List<Gesture> gestureCollection = new List<Gesture>();
		
		/// <summary>
		/// Occurs when gesture recognized.
		/// </summary>
		public event EventHandler<GestureEventArgs> Controller_GestureRecognized;
				
		/// <summary>
		/// Start this instance.
		/// </summary>
		void Start()
		{
			if (devOrEmu.useEmulator)
			{
				throw new Exception("There needs to be an active KinectSensor component!");
			}
			
			Gesture.pointController = pointController;
		}
		
		
		#region Frame Updaters
		public void SkeletonFrameReady()
		{
			NuiSkeletonFrame frame = sensor.getSkeleton_gestures();
			NuiSkeletonData[] skeletons = frame.SkeletonData;
			
			foreach (var skeleton in skeletons)
			{
				// we're only interested in tracked skeletons
				if (skeleton.eTrackingState != NuiSkeletonTrackingState.SkeletonTracked)
				{
					continue;
				}
				
				UpdateAllGestures(skeleton);
			}				
		}
		
		/// <summary>
		/// Updates all gestures.
		/// </summary>
		/// <param name='data'>
		/// Data.
		/// </param>
		public void UpdateAllGestures(NuiSkeletonData data)
		{
			foreach (Gesture gesture in gestureCollection)
			{
				gesture.UpdateGesture(data);
			}
		}
		#endregion Frame Updaters
		
		/// <summary>
		/// Adds the gesture.
		/// </summary>
		/// <param name='type'>
		/// Type.
		/// </param>
		/// <param name='gestureDefinition'>
		/// Gesture definition.
		/// </param>
		public void AddGesture(string name, IRelativeGestureSegment[] gestureDefinition)
		{
			Gesture gesture = new Gesture(name, gestureDefinition);
			gesture.GestureRecognized += new EventHandler<GestureEventArgs>(OnGestureRecognized);
			gestureCollection.Add(gesture);
		}
		
		#region Event Handlers
		/// <summary>
		/// Fires when a gesture is recognized.
		/// </summary>
		/// <param name='sender'>
		/// Sender.
		/// </param>
		/// <param name='e'>
		/// Args for this event
		/// </param>
		private void OnGestureRecognized(object sender, GestureEventArgs e)
		{
			if (Controller_GestureRecognized != null)
			{
				Controller_GestureRecognized(this, e);
			}
			
			foreach (Gesture g in gestureCollection)
			{
				g.Reset();
			}
		}
		#endregion Event Handlers
		
		#region Helper Functions
		private void RegisterGestures()
		{
		}
		#endregion Helper Functions
	}
}