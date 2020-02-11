//////////////////////////////////////////////////////
// MK Glow Change Settings Legacy        			//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2017 All rights reserved.            //
//////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK.Glow.Example
{
    public class ChangeSettingsLegacy : MonoBehaviour
    {
        private Legacy.MKGlow _mkGlow;

        private void Awake()
        {
            _mkGlow = GetComponent<Legacy.MKGlow>();
        }

        //Main
        public int debugView
        { 
            get { return (int)_mkGlow.debugView; }
            set { _mkGlow.debugView = (DebugView)value; }
        }
        public float anamorphicRatio
        { 
            get { return _mkGlow.anamorphicRatio; }
            set { _mkGlow.anamorphicRatio = value; }
        }
        public float lumaScale
        { 
            get { return _mkGlow.lumaScale; }
            set { _mkGlow.lumaScale = value; }
        }
		public float blooming
		{ 
			get { return _mkGlow.blooming; }
			set { _mkGlow.blooming = value; }
		}

        //Bloom
        public float bloomThreshold
		{ 
			get { return _mkGlow.bloomThreshold.minValue; }
			set { _mkGlow.bloomThreshold.minValue = value; }
		}
        public float bloomClamp
		{ 
			get { return _mkGlow.bloomThreshold.maxValue; }
			set { _mkGlow.bloomThreshold.maxValue = value; }
		}
        public float bloomScattering
		{ 
			get { return _mkGlow.bloomScattering; }
			set { _mkGlow.bloomScattering = value; }
		}
        public float bloomIntensity
		{ 
			get { return _mkGlow.bloomIntensity; }
			set { _mkGlow.bloomIntensity = value; }
		}

        //Lens Surface
         public bool allowLensSurface
		{ 
			get { return _mkGlow.allowLensSurface; }
			set { _mkGlow.allowLensSurface = value; }
		}
        public float lensSurfaceDirtIntensity
		{ 
			get { return _mkGlow.lensSurfaceDirtIntensity; }
			set { _mkGlow.lensSurfaceDirtIntensity = value; }
		}
        public float lensSurfaceDiffractionIntensity
		{ 
			get { return _mkGlow.lensSurfaceDiffractionIntensity; }
			set { _mkGlow.lensSurfaceDiffractionIntensity = value; }
		}

        //Lens Flare
        public bool allowLensFlare
		{ 
			get { return _mkGlow.allowLensFlare; }
			set { _mkGlow.allowLensFlare = value; }
		}
        public float lensFlareThreshold
		{ 
			get { return _mkGlow.lensFlareThreshold.minValue; }
			set { _mkGlow.lensFlareThreshold.minValue = value; }
		}
        public float lensFlareClamp
		{ 
			get { return _mkGlow.lensFlareThreshold.maxValue; }
			set { _mkGlow.lensFlareThreshold.maxValue = value; }
		}
        public float lensFlareChromaticAberration
		{ 
			get { return _mkGlow.lensFlareChromaticAberration; }
			set { _mkGlow.lensFlareChromaticAberration = value; }
		}
        public float lensFlareScattering
		{ 
			get { return _mkGlow.lensFlareScattering; }
			set { _mkGlow.lensFlareScattering = value; }
		}

        public float lensFlareGhostFade
		{ 
			get { return _mkGlow.lensFlareGhostFade; }
			set { _mkGlow.lensFlareGhostFade = value; }
		}
        public float lensFlareGhostCount
		{ 
			get { return _mkGlow.lensFlareGhostCount; }
			set { _mkGlow.lensFlareGhostCount = (int)value; }
		}
        public float lensFlareGhostDispersal
		{ 
			get { return _mkGlow.lensFlareGhostDispersal; }
			set { _mkGlow.lensFlareGhostDispersal = value; }
		}
        public float lensFlareGhostIntensity
		{ 
			get { return _mkGlow.lensFlareGhostIntensity; }
			set { _mkGlow.lensFlareGhostIntensity = value; }
		}

        public float lensFlareHaloFade
		{ 
			get { return _mkGlow.lensFlareHaloFade; }
			set { _mkGlow.lensFlareHaloFade = value; }
		}
        public float lensFlareHaloSize
		{ 
			get { return _mkGlow.lensFlareHaloSize; }
			set { _mkGlow.lensFlareHaloSize = value; }
		}
        public float lensFlareHaloIntensity
		{ 
			get { return _mkGlow.lensFlareHaloIntensity; }
			set { _mkGlow.lensFlareHaloIntensity = value; }
		}

        //Glare
         public bool allowGlare
		{ 
			get { return _mkGlow.allowGlare; }
			set { _mkGlow.allowGlare = value; }
		}
        public float glareThreshold
		{ 
			get { return _mkGlow.glareThreshold.minValue; }
			set { _mkGlow.glareThreshold.minValue = value; }
		}
        public float glareClamp
		{ 
			get { return _mkGlow.glareThreshold.maxValue; }
			set { _mkGlow.glareThreshold.maxValue = value; }
		}
        public float glareBlend
		{ 
			get { return _mkGlow.glareBlend; }
			set { _mkGlow.glareBlend = value; }
		}

        //Sample0
        public float glareSample0Scattering
		{ 
			get { return _mkGlow.glareSample0Scattering; }
			set { _mkGlow.glareSample0Scattering = value; }
		}
        public float glareSample0Angle
		{ 
			get { return _mkGlow.glareSample0Angle; }
			set { _mkGlow.glareSample0Angle = value; }
		}
        public float glareSample0Offset
		{ 
			get { return _mkGlow.glareSample0Offset; }
			set { _mkGlow.glareSample0Offset = value; }
		}
        public float glareSample0Intensity
		{ 
			get { return _mkGlow.glareSample0Intensity; }
			set { _mkGlow.glareSample0Intensity = value; }
		}

        //Sample2
        public float glareSample1Scattering
		{ 
			get { return _mkGlow.glareSample1Scattering; }
			set { _mkGlow.glareSample1Scattering = value; }
		}
        public float glareSample1Angle
		{ 
			get { return _mkGlow.glareSample1Angle; }
			set { _mkGlow.glareSample1Angle = value; }
		}
        public float glareSample1Offset
		{ 
			get { return _mkGlow.glareSample1Offset; }
			set { _mkGlow.glareSample1Offset = value; }
		}
        public float glareSample1Intensity
		{ 
			get { return _mkGlow.glareSample1Intensity; }
			set { _mkGlow.glareSample1Intensity = value; }
		}

        //Sample2
        public float glareSample2Scattering
		{ 
			get { return _mkGlow.glareSample2Scattering; }
			set { _mkGlow.glareSample2Scattering = value; }
		}
        public float glareSample2Angle
		{ 
			get { return _mkGlow.glareSample2Angle; }
			set { _mkGlow.glareSample2Angle = value; }
		}
        public float glareSample2Offset
		{ 
			get { return _mkGlow.glareSample2Offset; }
			set { _mkGlow.glareSample2Offset = value; }
		}
        public float glareSample2Intensity
		{ 
			get { return _mkGlow.glareSample2Intensity; }
			set { _mkGlow.glareSample2Intensity = value; }
		}

        //Sample3
        public float glareSample3Scattering
		{ 
			get { return _mkGlow.glareSample3Scattering; }
			set { _mkGlow.glareSample3Scattering = value; }
		}
        public float glareSample3Angle
		{ 
			get { return _mkGlow.glareSample3Angle; }
			set { _mkGlow.glareSample3Angle = value; }
		}
        public float glareSample3Offset
		{ 
			get { return _mkGlow.glareSample3Offset; }
			set { _mkGlow.glareSample3Offset = value; }
		}
        public float glareSample3Intensity
		{ 
			get { return _mkGlow.glareSample3Intensity; }
			set { _mkGlow.glareSample3Intensity = value; }
		}
    }
}