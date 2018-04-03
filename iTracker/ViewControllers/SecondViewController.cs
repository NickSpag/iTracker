using System;
using ARKit;
using Foundation;
using SceneKit;
using UIKit;
using System.Linq;
using CoreGraphics;
using System.Threading.Tasks;
using CoreVideo;
using VideoToolbox;
using CoreMedia;
using CoreImage;
using Plugin.Permissions.Abstractions;

namespace iTracker
{
    public partial class SecondViewController : UIViewController, IARSessionDelegate
    {
        private ARSession currentSession;
        private ARFaceAnchor currentFaceAnchor;

        protected SecondViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            TestCaptureButton.Hidden = true;
            TrackingMessage.Hidden = true;
            FirstActivityIndicator.Hidden = true;
            SecondActivityIndicator.Hidden = true;

            //var cameraPermissionGranted = await Help.Permissions.IsCameraPermissionGranted();

            //if (!await Help.Permissions.IsGranted(Permission.Camera))
            //{
            //    var hasCameraAccess = await Help.Permissions.RequestCameraPermission();
            //}
            //cameraPermissionGranted = await Help.Permissions.RequestCameraPermission();

            //if (cameraPermissionGranted)
            //StartTrackingFace();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void StartTrackingFace()
        {
            var session = new ARSession();
            session.Delegate = this;

            var faceTrackingConfig = new ARFaceTrackingConfiguration();
            faceTrackingConfig.LightEstimationEnabled = true;

            currentSession = session;
            session.Run(faceTrackingConfig, ARSessionRunOptions.ResetTracking);

        }

        private async Task TestCapture()
        {
            StartTrackingFace();

            System.Console.WriteLine("Delay - one second");
            await Task.Delay(1000);

            System.Console.WriteLine("Taking first image");
            FirstImage.Image = DisplayImage(FirstActivityIndicator);

            System.Console.WriteLine("Delay - two seconds");
            await Task.Delay(TimeSpan.FromSeconds(2));

            System.Console.WriteLine("Taking second image");
            SecondImage.Image = DisplayImage(SecondActivityIndicator);

        }

        async partial void TestCaptureTapped(NSObject sender)
        {
            await TestCapture();
        }

        private UIImage DisplayImage(UIActivityIndicatorView indicator)
        {
            indicator.Hidden = false;

            CVPixelBuffer pixelBuffer;
            var frame = currentSession.CurrentFrame;



            try
            {
                pixelBuffer = frame.CapturedImage;

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return new UIImage();
            }

            CGImage image;

            try
            {
                VTUtilities.ToCGImage(pixelBuffer, out image);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return new UIImage();
            }

            var orient = UIApplication.SharedApplication.StatusBarOrientation;
            var viewportSize = this.View.Bounds.Size;
            var transform = frame.GetDisplayTransform(orient, viewportSize);

            CIImage newImage = new CIImage(pixelBuffer).ImageByApplyingTransform(transform);

            indicator.Hidden = true;

            return new UIImage(newImage);
        }

        private bool GetCurrentFaceDataForPoint(CGPoint point)
        {
            if (currentSession == null || currentFaceAnchor == null) return false;

            //ARFrame
            var frame = currentSession.CurrentFrame;

            //Light estimate from frame
            var lightEstimate = frame.LightEstimate;

            //Transform property
            var anchorFaceMatrixCordinates = currentFaceAnchor.Transform;

            //geometry (do we need this?)
            return true;
        }

        [Foundation.Export("session:didFailWithError:")]
        public void DidFail(ARSession session, NSError error)
        {
            System.Console.WriteLine("Failed");
        }

        [Foundation.Export("sessionWasInterrupted:")]
        public void WasInterrupted(ARSession session)
        {
            System.Console.WriteLine("Interrupted");
        }

        [Foundation.Export("session:didAddAnchors:")]
        public void DidAddAnchors(ARSession session, ARAnchor[] anchors)
        {
            if (anchors.First() is ARFaceAnchor faceAnchor && currentFaceAnchor == null)
            {
                currentSession = session;
                currentFaceAnchor = faceAnchor;

                TestCaptureButton.Hidden = false;
                TrackingMessage.Hidden = false;

                System.Console.WriteLine("Anchor added");
            }
        }

        [Foundation.Export("session:didUpdateAnchors:")]
        public void DidUpdateAnchors(ARSession session, ARAnchor[] anchors)
        {
            if (anchors.First() is ARFaceAnchor faceAnchor)
            {
                //currentFaceAnchor = faceAnchor;

                //System.Console.WriteLine("");
                //System.Console.WriteLine("New matrix");
                //System.Console.WriteLine("{0}.{0}.{0}.{0}", faceAnchor.Transform.M11,
                //                         faceAnchor.Transform.M12,
                //                         faceAnchor.Transform.M13,
                //                         faceAnchor.Transform.M14);

                //System.Console.WriteLine("{0}.{0}.{0}.{0}", faceAnchor.Transform.M21,
                //         faceAnchor.Transform.M22,
                //         faceAnchor.Transform.M23,
                //         faceAnchor.Transform.M24);
                //System.Console.WriteLine("{0}.{0}.{0}.{0}", faceAnchor.Transform.M31,
                //     faceAnchor.Transform.M32,
                //     faceAnchor.Transform.M33,
                //     faceAnchor.Transform.M34);
                //System.Console.WriteLine("{0}.{0}.{0}.{0}", faceAnchor.Transform.M41,
                //faceAnchor.Transform.M42,
                //faceAnchor.Transform.M43,
                //faceAnchor.Transform.M44);
                System.Console.WriteLine("Updated anchor");
            }

            //System.Console.WriteLine("Updated Anchors");
        }

        [Foundation.Export("session:didUpdateFrame:")]
        public void DidUpdateFrame(ARSession session, ARFrame frame)
        {
            System.Console.WriteLine("Updated frame");
        }

        [Foundation.Export("session:didRemoveAnchors:")]
        public void DidRemoveAnchors(ARSession session, ARAnchor[] anchors)
        {
            if (anchors.First() is ARFaceAnchor faceAnchor)
            {
                currentFaceAnchor = null;
                System.Console.WriteLine("Anchor removed");
            }
        }

        //#region ASCNView delegate

        //[Export("renderer:didAddNode:forAnchor:")]
        //public void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        //{
        //    System.Console.WriteLine("Adding Node");
        //}

        //[Export("renderer:didRemoveNode:forAnchor:")]
        //public void DidRemoveNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        //{
        //    System.Console.WriteLine("Removing Node");
        //}

        //[Export("renderer:didUpdateNode:forAnchor:")]
        //public void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        //{
        //    System.Console.WriteLine("Updated Node");
        //}

        //[Export("renderer:willUpdateNode:forAnchor:")]
        //public void WillUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        //{
        //    System.Console.WriteLine("About to update Node");
        //}
        //#endregion


    }

    //public static class ARSessionObserverExtensions
    //{
    //    #region ARSessionObserver methods
    //    [Export("session:cameraDidChangeTrackingState:")]
    //    public static void CameraDidChangeTrackingState(this IARSessionObserver This, ARSession session, ARCamera camera)
    //    {
    //        System.Console.WriteLine("camera changed tracking status");
    //    }

    //    [Export("session:didOutputAudioSampleBuffer:")]
    //    public static void DidOutputAudioSampleBuffer(this IARSessionObserver This, ARSession session, CMSampleBuffer audioSampleBuffer)
    //    {

    //    }

    //    [Export("sessionInterruptionEnded:")]
    //    public static void InterruptionEnded(this IARSessionObserver This, ARSession session)
    //    {
    //        System.Console.WriteLine("interruption ended");
    //    }

    //    [Export("sessionWasInterrupted:")]
    //    public static void WasInterrupted(this IARSessionObserver This, ARSession session)
    //    {
    //        System.Console.WriteLine("interruption started");
    //    }
    //    #endregion
    //}
}
