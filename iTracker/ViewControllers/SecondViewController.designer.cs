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
	[Register ("SecondViewController")]
	partial class SecondViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView FirstActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIImageView FirstImage { get; set; }

		[Outlet]
		UIKit.UIActivityIndicatorView SecondActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIImageView SecondImage { get; set; }

		[Outlet]
		UIKit.UIButton TestCaptureButton { get; set; }

		[Outlet]
		UIKit.UILabel TrackingMessage { get; set; }

		[Action ("TestCaptureTapped:")]
		partial void TestCaptureTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (FirstImage != null) {
				FirstImage.Dispose ();
				FirstImage = null;
			}

			if (SecondImage != null) {
				SecondImage.Dispose ();
				SecondImage = null;
			}

			if (TestCaptureButton != null) {
				TestCaptureButton.Dispose ();
				TestCaptureButton = null;
			}

			if (TrackingMessage != null) {
				TrackingMessage.Dispose ();
				TrackingMessage = null;
			}

			if (FirstActivityIndicator != null) {
				FirstActivityIndicator.Dispose ();
				FirstActivityIndicator = null;
			}

			if (SecondActivityIndicator != null) {
				SecondActivityIndicator.Dispose ();
				SecondActivityIndicator = null;
			}
		}
	}
}
