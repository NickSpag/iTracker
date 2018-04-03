using System;
using CoreML;
using Foundation;
using ARKit;
using OpenTK;
using CoreMotion;

namespace iTracker
{
    public class GazePredictionInput : NSObject, IMLFeatureProvider, IGazeSnapshot
    {
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

        #region IPhoneAttitude Quanterion Attributes
        public double qW { get; set; }
        public double qX { get; set; }
        public double qY { get; set; }
        public double qZ { get; set; }
        #endregion

        public GazePredictionInput()
        {

        }

        public static GazePredictionInput FromAnchor(ARFaceAnchor faceAnchor, CMAttitude attitude)
        {
            var snapshot = new GazePredictionInput();
            snapshot.SetAttitude(attitude);
            snapshot.SetTransform(faceAnchor.Transform);
            snapshot.SetBlendingShapes(faceAnchor.BlendShapes);

            return snapshot;
        }

        #region IMLFeatureProvider
        public NSSet<NSString> FeatureNames => _featureNames;


        public static NSSet<NSString> _featureNames =
                new NSSet<NSString>(
                new NSString("BrowDownLeft"),
                new NSString("BrowDownRight"),
                new NSString("BrowInnerUp"),
                new NSString("BrowOuterUpLeft"),
                new NSString("BrowOuterUpRight"),
                new NSString("CheekPuff"),
                new NSString("CheekSquintLeft"),
                new NSString("CheekSquintRight"),
                new NSString("EyeBlinkLeft"),
                new NSString("EyeBlinkRight"),
                new NSString("EyeLookDownLeft"),
                new NSString("EyeLookDownRight"),
                new NSString("EyeLookInLeft"),
                new NSString("EyeLookInRight"),
                new NSString("EyeLookOutLeft"),
                new NSString("EyeLookOutRight"),
                new NSString("EyeLookUpLeft"),
                new NSString("EyeLookUpRight"),
                new NSString("EyeSquintLeft"),
                new NSString("EyeSquintRight"),
                new NSString("EyeWideLeft"),
                new NSString("EyeWideRight"),
                new NSString("JawForward"),
                new NSString("JawLeft"),
                new NSString("JawOpen"),
                new NSString("JawRight"),
                new NSString("MouthClose"),
                new NSString("MouthDimpleLeft"),
                new NSString("MouthDimpleRight"),
                new NSString("MouthFrownLeft"),
                new NSString("MouthFrownRight"),
                new NSString("MouthFunnel"),
                new NSString("MouthLeft"),
                new NSString("MouthLowerDownLeft"),
                new NSString("MouthLowerDownRight"),
                new NSString("MouthPressLeft"),
                new NSString("MouthPressRight"),
                new NSString("MouthPucker"),
                new NSString("MouthRight"),
                new NSString("MouthRollLower"),
                new NSString("MouthRollUpper"),
                new NSString("MouthShrugLower"),
                new NSString("MouthShrugUpper"),
                new NSString("MouthSmileLeft"),
                new NSString("MouthSmileRight"),
                new NSString("MouthStretchLeft"),
                new NSString("MouthStretchRight"),
                new NSString("MouthUpperUpLeft"),
                new NSString("MouthUpperUpRight"),
                new NSString("NoseSneerLeft"),
                new NSString("NoseSneerRight"),
                new NSString("M11"),
                new NSString("M12"),
                new NSString("M13"),
                new NSString("M14"),
                new NSString("M21"),
                new NSString("M22"),
                new NSString("M23"),
                new NSString("M24"),
                new NSString("M31"),
                new NSString("M32"),
                new NSString("M33"),
                new NSString("M34"),
                new NSString("qW"),
                new NSString("qX"),
                new NSString("qY"),
                new NSString("qZ"));

        public MLFeatureValue GetFeatureValue(string featureName)
        {
            switch (featureName)
            {
                case "BrowDownLeft": return MLFeatureValue.Create(BrowDownLeft);
                case "BrowDownRight": return MLFeatureValue.Create(BrowDownRight);
                case "BrowInnerUp": return MLFeatureValue.Create(BrowInnerUp);
                case "BrowOuterUpLeft": return MLFeatureValue.Create(BrowOuterUpLeft);
                case "BrowOuterUpRight": return MLFeatureValue.Create(BrowOuterUpRight);
                case "CheekPuff": return MLFeatureValue.Create(CheekPuff);
                case "CheekSquintLeft": return MLFeatureValue.Create(CheekSquintLeft);
                case "CheekSquintRight": return MLFeatureValue.Create(CheekSquintRight);
                case "EyeBlinkLeft": return MLFeatureValue.Create(EyeBlinkLeft);
                case "EyeBlinkRight": return MLFeatureValue.Create(EyeBlinkRight);
                case "EyeLookDownLeft": return MLFeatureValue.Create(EyeLookDownLeft);
                case "EyeLookDownRight": return MLFeatureValue.Create(EyeLookDownRight);
                case "EyeLookInLeft": return MLFeatureValue.Create(EyeLookInLeft);
                case "EyeLookInRight": return MLFeatureValue.Create(EyeLookInRight);
                case "EyeLookOutLeft": return MLFeatureValue.Create(EyeLookOutRight);
                case "EyeLookOutRight": return MLFeatureValue.Create(EyeLookOutRight);
                case "EyeLookUpLeft": return MLFeatureValue.Create(EyeLookUpLeft);
                case "EyeLookUpRight": return MLFeatureValue.Create(EyeLookUpRight);
                case "EyeSquintLeft": return MLFeatureValue.Create(EyeSquintLeft);
                case "EyeSquintRight": return MLFeatureValue.Create(EyeSquintRight);
                case "EyeWideLeft": return MLFeatureValue.Create(EyeWideLeft);
                case "EyeWideRight": return MLFeatureValue.Create(EyeWideRight);
                case "JawForward": return MLFeatureValue.Create(JawForward);
                case "JawLeft": return MLFeatureValue.Create(JawLeft);
                case "JawOpen": return MLFeatureValue.Create(JawOpen);
                case "JawRight": return MLFeatureValue.Create(JawRight);
                case "MouthClose": return MLFeatureValue.Create(MouthClose);
                case "MouthDimpleLeft": return MLFeatureValue.Create(MouthDimpleLeft);
                case "MouthDimpleRight": return MLFeatureValue.Create(MouthDimpleRight);
                case "MouthFrownLeft": return MLFeatureValue.Create(MouthFrownLeft);
                case "MouthFrownRight": return MLFeatureValue.Create(MouthFrownRight);
                case "MouthFunnel": return MLFeatureValue.Create(MouthFunnel);
                case "MouthLeft": return MLFeatureValue.Create(MouthLeft);
                case "MouthLowerDownLeft": return MLFeatureValue.Create(MouthLowerDownLeft);
                case "MouthLowerDownRight": return MLFeatureValue.Create(MouthLowerDownRight);
                case "MouthPressLeft": return MLFeatureValue.Create(MouthPressLeft);
                case "MouthPressRight": return MLFeatureValue.Create(MouthPressRight);
                case "MouthPucker": return MLFeatureValue.Create(MouthPucker);
                case "MouthRight": return MLFeatureValue.Create(MouthRight);
                case "MouthRollLower": return MLFeatureValue.Create(MouthRollLower);
                case "MouthRollUpper": return MLFeatureValue.Create(MouthRollUpper);
                case "MouthShrugLower": return MLFeatureValue.Create(MouthShrugLower);
                case "MouthShrugUpper": return MLFeatureValue.Create(MouthShrugUpper);
                case "MouthSmileLeft": return MLFeatureValue.Create(MouthSmileLeft);
                case "MouthSmileRight": return MLFeatureValue.Create(MouthSmileRight);
                case "MouthStretchLeft": return MLFeatureValue.Create(MouthStretchLeft);
                case "MouthStretchRight": return MLFeatureValue.Create(MouthStretchRight);
                case "MouthUpperUpLeft": return MLFeatureValue.Create(MouthUpperUpLeft);
                case "MouthUpperUpRight": return MLFeatureValue.Create(MouthUpperUpRight);
                case "NoseSneerLeft": return MLFeatureValue.Create(NoseSneerLeft);
                case "NoseSneerRight": return MLFeatureValue.Create(NoseSneerRight);
                case "M11": return MLFeatureValue.Create(M11);
                case "M12": return MLFeatureValue.Create(M12);
                case "M13": return MLFeatureValue.Create(M13);
                case "M14": return MLFeatureValue.Create(M14);
                case "M21": return MLFeatureValue.Create(M21);
                case "M22": return MLFeatureValue.Create(M22);
                case "M23": return MLFeatureValue.Create(M23);
                case "M24": return MLFeatureValue.Create(M24);
                case "M31": return MLFeatureValue.Create(M31);
                case "M32": return MLFeatureValue.Create(M32);
                case "M33": return MLFeatureValue.Create(M33);
                case "M34": return MLFeatureValue.Create(M34);
                case "qW": return MLFeatureValue.Create(qW);
                case "qX": return MLFeatureValue.Create(qX);
                case "qY": return MLFeatureValue.Create(qY);
                case "qZ": return MLFeatureValue.Create(qZ);
                default:
                    return MLFeatureValue.Create("");
            }
        }
        #endregion
    }
}
