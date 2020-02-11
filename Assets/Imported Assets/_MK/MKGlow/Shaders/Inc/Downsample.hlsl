//////////////////////////////////////////////////////
// MK Glow Downsample		 						//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2019 All rights reserved.            //
//////////////////////////////////////////////////////
#ifndef MK_GLOW_DOWNSAMPLE
	#define MK_GLOW_DOWNSAMPLE

	#include "../Inc/Common.hlsl"
	
	#ifdef MK_BLOOM
		UNIFORM_SAMPLER_AND_TEXTURE_2D(_BloomTex)
		#ifdef COMPUTE_SHADER
			UNIFORM_RWTEXTURE_2D(_BloomTargetTex)
		#else
			uniform float2 _BloomTex_TexelSize;
		#endif
	#endif

	#ifdef MK_LENS_FLARE
		UNIFORM_SAMPLER_AND_TEXTURE_2D(_LensFlareTex)
		#ifdef COMPUTE_SHADER
			UNIFORM_RWTEXTURE_2D(_LensFlareTargetTex)
		#else
			uniform float2 _LensFlareTex_TexelSize;
		#endif
	#endif

	#ifdef MK_GLARE
		UNIFORM_SAMPLER_AND_TEXTURE_2D(_Glare0Tex)
		#ifdef COMPUTE_SHADER
			UNIFORM_RWTEXTURE_2D(_Glare0TargetTex)
		#else
			uniform float2 _Glare0Tex_TexelSize;
		#endif
	#endif

	#ifdef COMPUTE_SHADER
		#define HEADER [numthreads(8,8,1)] void Downsample (uint2 id : SV_DispatchThreadID)
	#else
		#define HEADER FragmentOutputAuto frag (VertGeoOutputSimple o)
	#endif

	HEADER
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(o);
		#ifndef COMPUTE_SHADER
			FragmentOutputAuto fO;
			UNITY_INITIALIZE_OUTPUT(FragmentOutputAuto, fO);
		#endif

		#ifdef MK_BLOOM
			BLOOM_RENDER_TARGET = DownsampleHQ(PASS_TEXTURE_2D(_BloomTex, sampler_BloomTex), BLOOM_UV, BLOOM_TEXEL_SIZE);
		#endif

		#ifdef MK_LENS_FLARE
			LENS_FLARE_RENDER_TARGET = DownsampleHQ(PASS_TEXTURE_2D(_LensFlareTex, sampler_LensFlareTex), LENS_FLARE_UV, LENS_FLARE_TEXEL_SIZE);
		#endif

		#ifdef MK_GLARE
			GLARE0_RENDER_TARGET = DownsampleHQ(PASS_TEXTURE_2D(_Glare0Tex, sampler_Glare0Tex), GLARE_UV, GLARE0_TEXEL_SIZE);
		#endif

		#ifndef COMPUTE_SHADER
			return fO;
		#endif
	}
#endif