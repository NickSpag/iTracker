using Microsoft.WindowsAzure.Storage.Table;
using CoreGraphics;
using ARKit;
using OpenTK;
using System;
using CoreML;
using Foundation;
using System.Security.Cryptography.X509Certificates;
using CoreMotion;

namespace iTracker
{
    public class GazeTrainingSnapshot : TableEntity, IGazeTrainingSnapshot
    {
        public GazeTrainingSnapshot()
        {
            this.Timestamp = DateTimeOffset.UtcNow;
        }

        public GazeTrainingSnapshot(ARFaceAnchor anchor, CGPoint gazePoint, CMAttitude attitude)
        {
            this.SetGazeTarget(gazePoint);
            this.SetTransform(anchor.Transform);
            this.SetBlendingShapes(anchor.BlendShapes);
            this.SetAttitude(attitude);
        }

        public double xPoint { get; set; }
        public double yPoint { get; set; }

        #region Blend Shapes
        public double BrowDownLeft { get; set; }
        public double BrowDownRight { get; set; }
        public double BrowInnerUp { get; set; }
        public double BrowOuterUpLeft { get; set; }
        public double BrowOuterUpRight { get; set; }

        public double CheekPuff { get; set; }
        public double CheekSquintLeft { get; set; }
        public double CheekSquintRight { get; set; }

        public double EyeBlinkLeft { get; set; }
        public double EyeBlinkRight { get; set; }
        public double EyeLookDownLeft { get; set; }
        public double EyeLookDownRight { get; set; }
        public double EyeLookInLeft { get; set; }
        public double EyeLookInRight { get; set; }
        public double EyeLookOutLeft { get; set; }
        public double EyeLookOutRight { get; set; }
        public double EyeLookUpLeft { get; set; }
        public double EyeLookUpRight { get; set; }
        public double EyeSquintLeft { get; set; }
        public double EyeSquintRight { get; set; }
        public double EyeWideLeft { get; set; }
        public double EyeWideRight { get; set; }

        public double JawForward { get; set; }
        public double JawLeft { get; set; }
        public double JawOpen { get; set; }
        public double JawRight { get; set; }

        public double MouthClose { get; set; }
        public double MouthDimpleLeft { get; set; }
        public double MouthDimpleRight { get; set; }
        public double MouthFrownLeft { get; set; }
        public double MouthFrownRight { get; set; }
        public double MouthFunnel { get; set; }
        public double MouthLeft { get; set; }
        public double MouthLowerDownLeft { get; set; }
        public double MouthLowerDownRight { get; set; }
        public double MouthPressLeft { get; set; }
        public double MouthPressRight { get; set; }
        public double MouthPucker { get; set; }
        public double MouthRight { get; set; }
        public double MouthRollLower { get; set; }
        public double MouthRollUpper { get; set; }
        public double MouthShrugLower { get; set; }
        public double MouthShrugUpper { get; set; }
        public double MouthSmileLeft { get; set; }
        public double MouthSmileRight { get; set; }
        public double MouthStretchLeft { get; set; }
        public double MouthStretchRight { get; set; }
        public double MouthUpperUpLeft { get; set; }
        public double MouthUpperUpRight { get; set; }

        public double NoseSneerLeft { get; set; }
        public double NoseSneerRight { get; set; }
        #endregion

        #region Transform Matrix
        public double M11 { get; set; }
        public double M12 { get; set; }
        public double M13 { get; set; }
        public double M14 { get; set; }

        public double M21 { get; set; }
        public double M22 { get; set; }
        public double M23 { get; set; }
        public double M24 { get; set; }

        public double M31 { get; set; }
        public double M32 { get; set; }
        public double M33 { get; set; }
        public double M34 { get; set; }
        #endregion

        #region Attitude 
        public double qW { get; set; }
        public double qX { get; set; }
        public double qY { get; set; }
        public double qZ { get; set; }
        #endregion
    }
}
