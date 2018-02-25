using System;
using Microsoft.WindowsAzure.Storage.Table;
using CoreGraphics;
using ARKit;
using OpenTK;
namespace iTracker
{
    public class GazeSnapshot : TableEntity
    {
        public GazeSnapshot()
        {
        }

        private double xPoint;
        private double yPoint;

        #region Blend Shapes
        public float? BrowDownLeft { get; set; }
        public float? BrowDownRight { get; set; }
        public float? BrowInnerUp { get; set; }
        public float? BrowOuterUpLeft { get; set; }
        public float? BrowOuterUpRight { get; set; }

        public float? CheekPuff { get; set; }
        public float? CheekSquintLeft { get; set; }
        public float? CheekSquintRight { get; set; }

        public float? EyeBlinkLeft { get; set; }
        public float? EyeBlinkRight { get; set; }
        public float? EyeLookDownLeft { get; set; }
        public float? EyeLookDownRight { get; set; }
        public float? EyeLookInLeft { get; set; }
        public float? EyeLookInRight { get; set; }
        public float? EyeLookOutLeft { get; set; }
        public float? EyeLookOutRight { get; set; }
        public float? EyeLookUpLeft { get; set; }
        public float? EyeLookUpRight { get; set; }
        public float? EyeSquintLeft { get; set; }
        public float? EyeSquintRight { get; set; }
        public float? EyeWideLeft { get; set; }
        public float? EyeWideRight { get; set; }

        public float? JawForward { get; set; }
        public float? JawLeft { get; set; }
        public float? JawOpen { get; set; }
        public float? JawRight { get; set; }

        public float? MouthClose { get; set; }
        public float? MouthDimpleLeft { get; set; }
        public float? MouthDimpleRight { get; set; }
        public float? MouthFrownLeft { get; set; }
        public float? MouthFrownRight { get; set; }
        public float? MouthFunnel { get; set; }
        public float? MouthLeft { get; set; }
        public float? MouthLowerDownLeft { get; set; }
        public float? MouthLowerDownRight { get; set; }
        public float? MouthPressLeft { get; set; }
        public float? MouthPressRight { get; set; }
        public float? MouthPucker { get; set; }
        public float? MouthRight { get; set; }
        public float? MouthRollLower { get; set; }
        public float? MouthRollUpper { get; set; }
        public float? MouthShrugLower { get; set; }
        public float? MouthShrugUpper { get; set; }
        public float? MouthSmileLeft { get; set; }
        public float? MouthSmileRight { get; set; }
        public float? MouthStretchLeft { get; set; }
        public float? MouthStretchRight { get; set; }
        public float? MouthUpperUpLeft { get; set; }
        public float? MouthUpperUpRight { get; set; }

        public float? NoseSneerLeft { get; set; }
        public float? NoseSneerRight { get; set; }
        #endregion

        #region Transform Matrix
        public float M11 { get; set; }
        public float M12 { get; set; }
        public float M13 { get; set; }
        public float M14 { get; set; }

        public float M21 { get; set; }
        public float M22 { get; set; }
        public float M23 { get; set; }
        public float M24 { get; set; }

        public float M31 { get; set; }
        public float M32 { get; set; }
        public float M33 { get; set; }
        public float M34 { get; set; }

        public float M41 { get; set; }
        public float M42 { get; set; }
        public float M43 { get; set; }
        public float M44 { get; set; }
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

        public void SetBlendingShapes(ARFaceAnchor anchor)
        {
            var shapes = anchor.BlendShapes;

            BrowDownLeft = shapes.BrowDownLeft;
            BrowDownRight = shapes.BrowDownRight;
            BrowInnerUp = shapes.BrowInnerUp;
            BrowOuterUpLeft = shapes.BrowOuterUpLeft;
            BrowOuterUpRight = shapes.BrowOuterUpRight;

            CheekPuff = shapes.CheekPuff;
            CheekSquintLeft = shapes.CheekSquintLeft;
            CheekSquintRight = shapes.CheekSquintRight;

            EyeBlinkLeft = shapes.EyeBlinkLeft;
            EyeBlinkRight = shapes.EyeBlinkRight;
            EyeLookDownLeft = shapes.EyeLookDownLeft;
            EyeLookDownRight = shapes.EyeLookDownRight;
            EyeLookInLeft = shapes.EyeLookInLeft;
            EyeLookInRight = shapes.EyeLookInRight;
            EyeLookOutLeft = shapes.EyeLookOutLeft;
            EyeLookOutRight = shapes.EyeLookOutRight;
            EyeLookUpLeft = shapes.EyeLookUpLeft;
            EyeLookUpRight = shapes.EyeLookUpRight;
            EyeSquintLeft = shapes.EyeSquintLeft;
            EyeSquintRight = shapes.EyeSquintRight;
            EyeWideLeft = shapes.EyeWideLeft;
            EyeWideRight = shapes.EyeWideRight;

            JawForward = shapes.JawForward;
            JawLeft = shapes.JawLeft;
            JawOpen = shapes.JawOpen;
            JawRight = shapes.JawRight;

            MouthClose = shapes.MouthClose;
            MouthDimpleLeft = shapes.MouthDimpleLeft;
            MouthDimpleRight = shapes.MouthDimpleRight;
            MouthFrownLeft = shapes.MouthFrownLeft;
            MouthFrownRight = shapes.MouthFrownRight;
            MouthFunnel = shapes.MouthFunnel;
            MouthLeft = shapes.MouthLeft;
            MouthLowerDownLeft = shapes.MouthLowerDownLeft;
            MouthLowerDownRight = shapes.MouthLowerDownRight;
            MouthPressLeft = shapes.MouthPressLeft;
            MouthPressRight = shapes.MouthPressRight;
            MouthPucker = shapes.MouthPucker;
            MouthRight = shapes.MouthRight;
            MouthRollLower = shapes.MouthRollLower;
            MouthRollUpper = shapes.MouthRollUpper;
            MouthShrugLower = shapes.MouthShrugLower;
            MouthShrugUpper = shapes.MouthShrugUpper;
            MouthSmileLeft = shapes.MouthSmileLeft;
            MouthSmileRight = shapes.MouthSmileRight;
            MouthStretchLeft = shapes.MouthStretchLeft;
            MouthStretchRight = shapes.MouthStretchRight;
            MouthUpperUpLeft = shapes.MouthUpperUpLeft;
            MouthUpperUpRight = shapes.MouthUpperUpRight;

            NoseSneerLeft = shapes.NoseSneerLeft;
            NoseSneerRight = shapes.NoseSneerRight;
        }
    }
}
