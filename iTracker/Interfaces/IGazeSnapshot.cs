using System;
namespace iTracker
{
    public interface IGazeSnapshot : IGazeFaceData, IGazeTransformMatrix, IPhoneAttitude
    {

    }

    public interface IGazeTrainingSnapshot : IGazeSnapshot, IGazePoint
    {

    }

    public interface IGazePoint
    {
        double xPoint { get; set; }
        double yPoint { get; set; }
    }

    /// <summary>
    /// Phone attitude attributes via CMAttitude's Quanterion.
    /// </summary>
    public interface IPhoneAttitude
    {
        double qW { get; set; }
        double qX { get; set; }
        double qY { get; set; }
        double qZ { get; set; }
    }

    public interface IGazeTransformMatrix
    {
        double M11 { get; set; }
        double M12 { get; set; }
        double M13 { get; set; }
        double M14 { get; set; }

        double M21 { get; set; }
        double M22 { get; set; }
        double M23 { get; set; }
        double M24 { get; set; }

        double M31 { get; set; }
        double M32 { get; set; }
        double M33 { get; set; }
        double M34 { get; set; }
    }

    public interface IGazeFaceData
    {
        double BrowDownLeft { get; set; }
        double BrowDownRight { get; set; }
        double BrowInnerUp { get; set; }
        double BrowOuterUpLeft { get; set; }
        double BrowOuterUpRight { get; set; }

        double CheekPuff { get; set; }
        double CheekSquintLeft { get; set; }
        double CheekSquintRight { get; set; }

        double EyeBlinkLeft { get; set; }
        double EyeBlinkRight { get; set; }
        double EyeLookDownLeft { get; set; }
        double EyeLookDownRight { get; set; }
        double EyeLookInLeft { get; set; }
        double EyeLookInRight { get; set; }
        double EyeLookOutLeft { get; set; }
        double EyeLookOutRight { get; set; }
        double EyeLookUpLeft { get; set; }
        double EyeLookUpRight { get; set; }
        double EyeSquintLeft { get; set; }
        double EyeSquintRight { get; set; }
        double EyeWideLeft { get; set; }
        double EyeWideRight { get; set; }

        double JawForward { get; set; }
        double JawLeft { get; set; }
        double JawOpen { get; set; }
        double JawRight { get; set; }

        double MouthClose { get; set; }
        double MouthDimpleLeft { get; set; }
        double MouthDimpleRight { get; set; }
        double MouthFrownLeft { get; set; }
        double MouthFrownRight { get; set; }
        double MouthFunnel { get; set; }
        double MouthLeft { get; set; }
        double MouthLowerDownLeft { get; set; }
        double MouthLowerDownRight { get; set; }
        double MouthPressLeft { get; set; }
        double MouthPressRight { get; set; }
        double MouthPucker { get; set; }
        double MouthRight { get; set; }
        double MouthRollLower { get; set; }
        double MouthRollUpper { get; set; }
        double MouthShrugLower { get; set; }
        double MouthShrugUpper { get; set; }
        double MouthSmileLeft { get; set; }
        double MouthSmileRight { get; set; }
        double MouthStretchLeft { get; set; }
        double MouthStretchRight { get; set; }
        double MouthUpperUpLeft { get; set; }
        double MouthUpperUpRight { get; set; }

        double NoseSneerLeft { get; set; }
        double NoseSneerRight { get; set; }
    }
}
