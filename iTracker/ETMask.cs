using System;
using SceneKit;
using ARKit;
using UIKit;
using ModelIO;
namespace iTracker
{
    public class ETMask : SCNNode
    {
        public ETMask()
        {
        }

        public ETMask(ARSCNFaceGeometry geometry)
        {
            var material = geometry.FirstMaterial;

            //79CCB5
            material.Diffuse.Contents = UIColor.FromRGB(2, 195, 154);
            material.LightingModelName = SCNLightingModel.PhysicallyBased;

            this.Geometry = geometry;
        }

        public void UpdateFromAnchor(ARFaceAnchor anchor)
        {
            if (this.Geometry is ARSCNFaceGeometry faceGeometry)
            {
                faceGeometry.Update(anchor.Geometry);
            }
        }

    }
}
