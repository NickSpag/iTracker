using System;
using System.Linq;
using ARKit;

namespace iTracker
{
    public class ARKitSessionDelegate : ARSessionDelegate
    {
        private ARFaceAnchor _faceAnchor;
        public ARFaceAnchor FaceAnchor
        {
            get => _faceAnchor;
            private set
            {
                if (_faceAnchor != null)
                {
                    _faceAnchor.Dispose();
                }

                _faceAnchor = value;
            }
        }

        #region Constructors
        public ARKitSessionDelegate()
        {
        }
        #endregion

        private bool isBroadcasting = false;
        public delegate void BroadcastAnchor(ARFaceAnchor anchor);
        private BroadcastAnchor del;


        public void StartBroadcastingAnchor(BroadcastAnchor broadcast)
        {
            isBroadcasting = true;
            del = broadcast;
        }

        public void StopBroadcastingAnchor()
        {
            isBroadcasting = false;
        }

        #region Override Methods
        public override void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
        {
            var state = "";
            var reason = "";

            switch (camera.TrackingState)
            {
                case ARTrackingState.NotAvailable:
                    state = "Tracking Not Available";
                    break;
                case ARTrackingState.Normal:
                    state = "Tracking Normal";
                    break;
                case ARTrackingState.Limited:
                    state = "Tracking Limited";
                    switch (camera.TrackingStateReason)
                    {
                        case ARTrackingStateReason.ExcessiveMotion:
                            reason = "because of excessive motion";
                            break;
                        case ARTrackingStateReason.Initializing:
                            reason = "because tracking is initializing";
                            break;
                        case ARTrackingStateReason.InsufficientFeatures:
                            reason = "because of insufficient features in the environment";
                            break;
                        case ARTrackingStateReason.None:
                            reason = "because of an unknown reason";
                            break;
                    }
                    break;
            }

            // Inform user
            Console.WriteLine("{0} {1}", state, reason);
        }

        public override void DidFail(ARSession session, Foundation.NSError error)
        {
            System.Console.WriteLine("Failed");
        }

        public override void DidRemoveAnchors(ARSession session, ARAnchor[] anchors)
        {
            System.Console.WriteLine("Removing anchors");
        }

        public override void DidAddAnchors(ARSession session, ARAnchor[] anchors)
        {
            System.Console.WriteLine("Adding anchors");
        }

        public override void DidUpdateAnchors(ARSession session, ARAnchor[] anchors)
        {
            if (isBroadcasting &&
                anchors.First() is ARFaceAnchor faceAnchor)
            {
                del?.Invoke(faceAnchor);
                //System.Console.WriteLine("Updated face anchor");
            }
            else
            {
                System.Console.WriteLine("Unrecognized anchor found during broadcast");
            }
        }

        public override void WasInterrupted(ARSession session)
        {
            System.Console.WriteLine("Interrupted");
        }

        public override void InterruptionEnded(ARSession session)
        {
            System.Console.WriteLine("Interruption ended");
        }

        public override void DidOutputAudioSampleBuffer(ARSession session, CoreMedia.CMSampleBuffer audioSampleBuffer)
        {
        }

        ////WARN: If you override this method, you must dispose of the frame when you're finished processing.
        ////Apple optimizations to speed these methods up will not create a new frame while one is referenced. In a 
        ////GC world like .NET this means no new frames will be updated/registered until the previous one is GC'd, 
        //// so you should do it manually once finished instead of waiting. otherwise they build up 
        ////and the session is effectively frozen without warning
        //public override void DidUpdateFrame(ARSession session, ARFrame frame)
        //{
        //    //Do frame processing
        //
        //    frame.Dispose();
        //}

        #endregion
    }
}
