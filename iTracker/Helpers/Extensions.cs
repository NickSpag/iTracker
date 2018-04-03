using System;
using ARKit;
using CoreGraphics;
using CoreMotion;
using OpenTK;

namespace iTracker
{
    public static class Extensions
    {
        public static void SetGazeTarget(this IGazeTrainingSnapshot trainingSnapshot, CGPoint point)
        {
            trainingSnapshot.xPoint = point.X;
            trainingSnapshot.yPoint = point.Y;
        }
        public static void SetAttitude(this IGazeSnapshot snapshot, CMAttitude attitude)
        {
            snapshot.qW = attitude.Quaternion.w;
            snapshot.qX = attitude.Quaternion.x;
            snapshot.qY = attitude.Quaternion.y;
            snapshot.qZ = attitude.Quaternion.z;
        }
        public static void SetTransform(this IGazeSnapshot snapshot, NMatrix4 transformMatrix)
        {
            snapshot.M11 = transformMatrix.M11;
            snapshot.M12 = transformMatrix.M12;
            snapshot.M13 = transformMatrix.M13;
            snapshot.M14 = transformMatrix.M14;

            snapshot.M21 = transformMatrix.M21;
            snapshot.M22 = transformMatrix.M22;
            snapshot.M23 = transformMatrix.M23;
            snapshot.M24 = transformMatrix.M24;

            snapshot.M31 = transformMatrix.M31;
            snapshot.M32 = transformMatrix.M32;
            snapshot.M33 = transformMatrix.M33;
            snapshot.M34 = transformMatrix.M34;

            //snapshot.M41 = transformMatrix.M41;
            //snapshot.M42 = transformMatrix.M42;
            //snapshot.M43 = transformMatrix.M43;
            //snapshot.M44 = transformMatrix.M44;
        }

        public static void SetBlendingShapes(this IGazeSnapshot snapshot, ARBlendShapeLocationOptions shapes)
        {
            snapshot.BrowDownLeft = (double)shapes.BrowDownLeft;
            snapshot.BrowDownRight = (double)shapes.BrowDownRight;
            snapshot.BrowInnerUp = (double)shapes.BrowInnerUp;
            snapshot.BrowOuterUpLeft = (double)shapes.BrowOuterUpLeft;
            snapshot.BrowOuterUpRight = (double)shapes.BrowOuterUpRight;

            snapshot.CheekPuff = (double)shapes.CheekPuff;
            snapshot.CheekSquintLeft = (double)shapes.CheekSquintLeft;
            snapshot.CheekSquintRight = (double)shapes.CheekSquintRight;

            snapshot.EyeBlinkLeft = (double)shapes.EyeBlinkLeft;
            snapshot.EyeBlinkRight = (double)shapes.EyeBlinkRight;
            snapshot.EyeLookDownLeft = (double)shapes.EyeLookDownLeft;
            snapshot.EyeLookDownRight = (double)shapes.EyeLookDownRight;
            snapshot.EyeLookInLeft = (double)shapes.EyeLookInLeft;
            snapshot.EyeLookInRight = (double)shapes.EyeLookInRight;
            snapshot.EyeLookOutLeft = (double)shapes.EyeLookOutLeft;
            snapshot.EyeLookOutRight = (double)shapes.EyeLookOutRight;
            snapshot.EyeLookUpLeft = (double)shapes.EyeLookUpLeft;
            snapshot.EyeLookUpRight = (double)shapes.EyeLookUpRight;
            snapshot.EyeSquintLeft = (double)shapes.EyeSquintLeft;
            snapshot.EyeSquintRight = (double)shapes.EyeSquintRight;
            snapshot.EyeWideLeft = (double)shapes.EyeWideLeft;
            snapshot.EyeWideRight = (double)shapes.EyeWideRight;

            snapshot.JawForward = (double)shapes.JawForward;
            snapshot.JawLeft = (double)shapes.JawLeft;
            snapshot.JawOpen = (double)shapes.JawOpen;
            snapshot.JawRight = (double)shapes.JawRight;

            snapshot.MouthClose = (double)shapes.MouthClose;
            snapshot.MouthDimpleLeft = (double)shapes.MouthDimpleLeft;
            snapshot.MouthDimpleRight = (double)shapes.MouthDimpleRight;
            snapshot.MouthFrownLeft = (double)shapes.MouthFrownLeft;
            snapshot.MouthFrownRight = (double)shapes.MouthFrownRight;
            snapshot.MouthFunnel = (double)shapes.MouthFunnel;
            snapshot.MouthLeft = (double)shapes.MouthLeft;
            snapshot.MouthLowerDownLeft = (double)shapes.MouthLowerDownLeft;
            snapshot.MouthLowerDownRight = (double)shapes.MouthLowerDownRight;
            snapshot.MouthPressLeft = (double)shapes.MouthPressLeft;
            snapshot.MouthPressRight = (double)shapes.MouthPressRight;
            snapshot.MouthPucker = (double)shapes.MouthPucker;
            snapshot.MouthRight = (double)shapes.MouthRight;
            snapshot.MouthRollLower = (double)shapes.MouthRollLower;
            snapshot.MouthRollUpper = (double)shapes.MouthRollUpper;
            snapshot.MouthShrugLower = (double)shapes.MouthShrugLower;
            snapshot.MouthShrugUpper = (double)shapes.MouthShrugUpper;
            snapshot.MouthSmileLeft = (double)shapes.MouthSmileLeft;
            snapshot.MouthSmileRight = (double)shapes.MouthSmileRight;
            snapshot.MouthStretchLeft = (double)shapes.MouthStretchLeft;
            snapshot.MouthStretchRight = (double)shapes.MouthStretchRight;
            snapshot.MouthUpperUpLeft = (double)shapes.MouthUpperUpLeft;
            snapshot.MouthUpperUpRight = (double)shapes.MouthUpperUpRight;

            snapshot.NoseSneerLeft = (double)shapes.NoseSneerLeft;
            snapshot.NoseSneerRight = (double)shapes.NoseSneerRight;
        }

    }
}
