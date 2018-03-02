using Microsoft.WindowsAzure.Storage.Table;
using CoreGraphics;
using ARKit;
using OpenTK;
using System;

namespace iTracker
{
    public class GazeSnapshot : TableEntity
    {
        public GazeSnapshot()
        {
            this.Timestamp = DateTimeOffset.UtcNow;
        }

        public GazeSnapshot(ARFaceAnchor anchor, CGPoint gazePoint)
        {
            SetGazeTarget(gazePoint);

            SetTransform(anchor.Transform);
            SetBlendingShapes(anchor.BlendShapes);
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

        public double M41 { get; set; }
        public double M42 { get; set; }
        public double M43 { get; set; }
        public double M44 { get; set; }
        #endregion

        public void SetGazeTarget(CGPoint point)
        {
            xPoint = point.X;
            yPoint = point.Y;
        }

        public void SetTransform(NMatrix4 transformMatrix)
        {
            M11 = transformMatrix.M11;
            M12 = transformMatrix.M12;
            M13 = transformMatrix.M13;
            M14 = transformMatrix.M14;

            M21 = transformMatrix.M21;
            M22 = transformMatrix.M22;
            M23 = transformMatrix.M23;
            M24 = transformMatrix.M24;

            M31 = transformMatrix.M31;
            M32 = transformMatrix.M32;
            M33 = transformMatrix.M33;
            M34 = transformMatrix.M34;

            M41 = transformMatrix.M41;
            M42 = transformMatrix.M42;
            M43 = transformMatrix.M43;
            M44 = transformMatrix.M44;
        }

        public void SetBlendingShapes(ARBlendShapeLocationOptions shapes)
        {
            BrowDownLeft = (double)shapes.BrowDownLeft;
            BrowDownRight = (double)shapes.BrowDownRight;
            BrowInnerUp = (double)shapes.BrowInnerUp;
            BrowOuterUpLeft = (double)shapes.BrowOuterUpLeft;
            BrowOuterUpRight = (double)shapes.BrowOuterUpRight;

            CheekPuff = (double)shapes.CheekPuff;
            CheekSquintLeft = (double)shapes.CheekSquintLeft;
            CheekSquintRight = (double)shapes.CheekSquintRight;

            EyeBlinkLeft = (double)shapes.EyeBlinkLeft;
            EyeBlinkRight = (double)shapes.EyeBlinkRight;
            EyeLookDownLeft = (double)shapes.EyeLookDownLeft;
            EyeLookDownRight = (double)shapes.EyeLookDownRight;
            EyeLookInLeft = (double)shapes.EyeLookInLeft;
            EyeLookInRight = (double)shapes.EyeLookInRight;
            EyeLookOutLeft = (double)shapes.EyeLookOutLeft;
            EyeLookOutRight = (double)shapes.EyeLookOutRight;
            EyeLookUpLeft = (double)shapes.EyeLookUpLeft;
            EyeLookUpRight = (double)shapes.EyeLookUpRight;
            EyeSquintLeft = (double)shapes.EyeSquintLeft;
            EyeSquintRight = (double)shapes.EyeSquintRight;
            EyeWideLeft = (double)shapes.EyeWideLeft;
            EyeWideRight = (double)shapes.EyeWideRight;

            JawForward = (double)shapes.JawForward;
            JawLeft = (double)shapes.JawLeft;
            JawOpen = (double)shapes.JawOpen;
            JawRight = (double)shapes.JawRight;

            MouthClose = (double)shapes.MouthClose;
            MouthDimpleLeft = (double)shapes.MouthDimpleLeft;
            MouthDimpleRight = (double)shapes.MouthDimpleRight;
            MouthFrownLeft = (double)shapes.MouthFrownLeft;
            MouthFrownRight = (double)shapes.MouthFrownRight;
            MouthFunnel = (double)shapes.MouthFunnel;
            MouthLeft = (double)shapes.MouthLeft;
            MouthLowerDownLeft = (double)shapes.MouthLowerDownLeft;
            MouthLowerDownRight = (double)shapes.MouthLowerDownRight;
            MouthPressLeft = (double)shapes.MouthPressLeft;
            MouthPressRight = (double)shapes.MouthPressRight;
            MouthPucker = (double)shapes.MouthPucker;
            MouthRight = (double)shapes.MouthRight;
            MouthRollLower = (double)shapes.MouthRollLower;
            MouthRollUpper = (double)shapes.MouthRollUpper;
            MouthShrugLower = (double)shapes.MouthShrugLower;
            MouthShrugUpper = (double)shapes.MouthShrugUpper;
            MouthSmileLeft = (double)shapes.MouthSmileLeft;
            MouthSmileRight = (double)shapes.MouthSmileRight;
            MouthStretchLeft = (double)shapes.MouthStretchLeft;
            MouthStretchRight = (double)shapes.MouthStretchRight;
            MouthUpperUpLeft = (double)shapes.MouthUpperUpLeft;
            MouthUpperUpRight = (double)shapes.MouthUpperUpRight;

            NoseSneerLeft = (double)shapes.NoseSneerLeft;
            NoseSneerRight = (double)shapes.NoseSneerRight;
        }
    }
}
