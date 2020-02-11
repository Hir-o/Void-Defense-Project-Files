//////////////////////////////////////////////////////
// MK Glow Compatibility	    	    	       	//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2019 All rights reserved.            //
//////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MK.Glow
{
    #if UNITY_2018_3_OR_NEWER
    using XRSettings = UnityEngine.XR.XRSettings;
    #endif
	public static class Compatibility
    {
        private static readonly bool _defaultHDRFormatSupported = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.DefaultHDR);
        private static readonly bool _11R11G10BFormatSupported = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGB111110Float);
        private static readonly bool _2A10R10G10BFormatSupported = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB2101010);
        //RenderToTexture and a hdr color format required
        public static readonly bool IsSupported = _11R11G10BFormatSupported ? true : _2A10R10G10BFormatSupported ? true : _defaultHDRFormatSupported ? true : false;
        
        /// <summary>
        /// Returns true if the device and used API supports geometry shaders
        /// </summary>
        public static bool CheckGeometryShaderSupport()
        {
            bool supportedOnPlattform = false;
            switch(SystemInfo.graphicsDeviceType)
            {
                case GraphicsDeviceType.Vulkan:
                case GraphicsDeviceType.Direct3D11:
                case GraphicsDeviceType.Direct3D12:
                case GraphicsDeviceType.PlayStation4:
                case GraphicsDeviceType.OpenGLCore:
                #if UNITY_2017_3_OR_NEWER
                case GraphicsDeviceType.XboxOneD3D12:
                #endif
                case GraphicsDeviceType.XboxOne:
                    supportedOnPlattform = true;
                break;
                default:
                supportedOnPlattform = false;
                break;
            }

            switch(Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.tvOS:
                    supportedOnPlattform = false;
                break;
                default:
                    supportedOnPlattform = true;
                break;
            }

            return SystemInfo.graphicsShaderLevel >= 40 && supportedOnPlattform;
        }

        /// <summary>
        /// Returns true if the device and used API supports direct compute
        /// </summary>
        public static bool CheckComputeShaderSupport()
        {
            #if UNITY_2017_1_OR_NEWER
                bool supportedOnPlattform = false;
                switch(SystemInfo.graphicsDeviceType)
                {
                    case GraphicsDeviceType.Vulkan:
                    case GraphicsDeviceType.Direct3D11:
                    case GraphicsDeviceType.Direct3D12:
                    case GraphicsDeviceType.PlayStation4:
                    //Gles3 random writes is throwing the following issue on some devices
                    //RenderTexture.Create failed: format unsupported for random writes - RGBA4 UNorm (9).
                    //The issue should be officially fixed in late 2019
                    //case GraphicsDeviceType.OpenGLES3:
                    case GraphicsDeviceType.OpenGLCore:
                    #if UNITY_2017_4_OR_NEWER
                    case GraphicsDeviceType.Switch:
                    #endif
                    #if UNITY_2017_3_OR_NEWER
                    case GraphicsDeviceType.XboxOneD3D12:
                    #endif
                    case GraphicsDeviceType.XboxOne:
                        supportedOnPlattform = true;
                    break;
                    default:
                    supportedOnPlattform = false;
                    break;
                }
                return SystemInfo.supportsComputeShaders && supportedOnPlattform && _defaultHDRFormatSupported;
            #else
                //On lower unity versions its impossible to get a temporary RT with randomwrites enabled, so dont allow direct compute
                return false;
            #endif
        }

        /// <summary>
        /// Returns true if the device and used API supports lens flare
        /// </summary>
        /// <returns></returns>
        public static bool CheckLensFlareFeatureSupport()
        {
            bool supportedOnPlattform;
            switch(SystemInfo.graphicsDeviceType)
            {
                case GraphicsDeviceType.OpenGLCore:
                case GraphicsDeviceType.Vulkan:
                case GraphicsDeviceType.Direct3D11:
                case GraphicsDeviceType.Direct3D12:
                case GraphicsDeviceType.PlayStation4:
                case GraphicsDeviceType.OpenGLES3:
                case GraphicsDeviceType.Metal:
                #if UNITY_2017_4_OR_NEWER
                case GraphicsDeviceType.Switch:
                #endif
                #if UNITY_2017_3_OR_NEWER
                case GraphicsDeviceType.XboxOneD3D12:
                #endif
                case GraphicsDeviceType.XboxOne:
                    supportedOnPlattform = true;
                break;
                default:
                supportedOnPlattform = false;
                break;
            }

            return SystemInfo.graphicsShaderLevel >= 30 && SystemInfo.supportedRenderTargetCount >= 2 && supportedOnPlattform && !PipelineProperties.singlePassStereoInstancedEnabled;
        }

        /// <summary>
        /// Returns true if the device and used API support glare
        /// </summary>
        /// <returns></returns>
        public static bool CheckGlareFeatureSupport()
        {
            bool supportedOnPlattform;
            switch(SystemInfo.graphicsDeviceType)
            {
                case GraphicsDeviceType.OpenGLCore:
                case GraphicsDeviceType.Vulkan:
                case GraphicsDeviceType.Direct3D11:
                case GraphicsDeviceType.Direct3D12:
                case GraphicsDeviceType.PlayStation4:
                case GraphicsDeviceType.Metal:
                #if UNITY_2017_4_OR_NEWER
                case GraphicsDeviceType.Switch:
                #endif
                #if UNITY_2017_3_OR_NEWER
                case GraphicsDeviceType.XboxOneD3D12:
                #endif
                case GraphicsDeviceType.XboxOne:
                    supportedOnPlattform = true;
                break;
                default:
                supportedOnPlattform = false;
                break;
            }

            return SystemInfo.graphicsShaderLevel >= 40 && SystemInfo.supportedRenderTargetCount >= 6 && supportedOnPlattform && !PipelineProperties.singlePassStereoInstancedEnabled;
        }

        /// <summary>
        /// Returns the supported rendertexture format used for rendering
        /// </summary>
        /// <returns></returns>
        internal static RenderTextureFormat CheckSupportedRenderTextureFormat()
        {
            //return _defaultHDRFormatSupported ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
            return _11R11G10BFormatSupported ? RenderTextureFormat.RGB111110Float : _2A10R10G10BFormatSupported ? RenderTextureFormat.ARGB2101010 : _defaultHDRFormatSupported ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
        }
    }
}