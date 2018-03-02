// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iTracker
{
	[Register ("FirstViewController")]
	partial class FirstViewController
	{
		[Outlet]
		UIKit.UIButton StartTrackingButton { get; set; }

		[Outlet]
		UIKit.UIView TrackingGuideBox { get; set; }

		[Action ("NextClicked:")]
		partial void NextClicked (Foundation.NSObject sender);

		[Action ("StartTrackingClicked:")]
		partial void StartTrackingClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (StartTrackingButton != null) {
				StartTrackingButton.Dispose ();
				StartTrackingButton = null;
			}

			if (TrackingGuideBox != null) {
				TrackingGuideBox.Dispose ();
				TrackingGuideBox = null;
			}
		}
	}
}
