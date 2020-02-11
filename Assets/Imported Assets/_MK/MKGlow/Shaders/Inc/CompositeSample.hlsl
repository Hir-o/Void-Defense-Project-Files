//////////////////////////////////////////////////////
// MK Glow Composite Sample							//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2019 All rights reserved.            //
//////////////////////////////////////////////////////
#ifndef MK_GLOW_COMPOSITE_SAMPLE
	#define MK_GLOW_COMPOSITE_SAMPLE
	
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(o);
	
	#ifndef COMPUTE_SHADER
		FragmentOutputAuto fO;
		UNITY_INITIALIZE_OUTPUT(FragmentOutputAuto, fO);
	#endif
	/*
	#ifdef MK_RENDER_PRIORITY_QUALITY
		half4 g = UpsampleHQ(PASS_TEXTURE_2D(_BloomTex, sampler_BloomTex), UV_0, BLOOM_COMPOSITE_SPREAD);
	#else
		half4 g = SampleTex2D(PASS_TEXTURE_2D(_BloomTex, sampler_BloomTex), UV_0);
	#endif
	*/
	half4 g = SampleTex2D(PASS_TEXTURE_2D(_BloomTex, sampler_BloomTex), UV_0);
	
	#if defined(MK_LENS_SURFACE) && defined(MK_NATURAL)
		half4 gs = g;
	#endif

	#ifdef COLORSPACE_GAMMA
		half3 source = GammaToLinearSpace(SAMPLE_SOURCE.rgb).rgb;
	#else
		half3 source = SAMPLE_SOURCE.rgb;
	#endif

	#ifdef MK_GLOW_DEBUG
		source = 0;
	#endif

	#ifdef MK_NATURAL
		g.rgb = lerp(source, g, BLOOM_INTENSITY);
	#else
		g.rgb *= BLOOM_INTENSITY;
		#if defined(MK_RENDER_PRIORITY_BALANCED) || defined(MK_RENDER_PRIORITY_QUALITY)
			g.rgb = Blooming(g.rgb, BLOOMING);
		#endif
	#endif

	#ifdef MK_GLARE
		half4 glare = 0;
		#ifdef MK_GLARE_1
			/*
			#ifdef MK_RENDER_PRIORITY_QUALITY
				glare = GaussianBlur1D(PASS_TEXTURE_2D(_Glare0Tex, sampler_Glare0Tex), UV_0, GLARE0_TEXEL_SIZE * RESOLUTION_SCALE, GLARE0_SCATTERING, GLARE0_DIRECTION, GLARE0_OFFSET) * GLARE0_INTENSITY;
			#else
				glare = SampleTex2D(PASS_TEXTURE_2D(_Glare0Tex, sampler_Glare0Tex), UV_0);
			#endif
			*/
			glare = SampleTex2D(PASS_TEXTURE_2D(_Glare0Tex, sampler_Glare0Tex), UV_0);
		#endif
		#ifdef MK_GLARE_2
			/*
			#ifdef MK_RENDER_PRIORITY_QUALITY
				glare += GaussianBlur1D(PASS_TEXTURE_2D(_Glare1Tex, sampler_Glare1Tex), UV_0, GLARE0_TEXEL_SIZE * RESOLUTION_SCALE, GLARE1_SCATTERING, GLARE1_DIRECTION, GLARE1_OFFSET) * GLARE1_INTENSITY;
			#else
				glare += SampleTex2D(PASS_TEXTURE_2D(_Glare1Tex, sampler_Glare1Tex), UV_0);
			#endif
			*/
			glare += SampleTex2D(PASS_TEXTURE_2D(_Glare1Tex, sampler_Glare1Tex), UV_0);
		#endif
		#ifdef MK_GLARE_3
			/*
			#ifdef MK_RENDER_PRIORITY_QUALITY
				glare += GaussianBlur1D(PASS_TEXTURE_2D(_Glare2Tex, sampler_Glare2Tex), UV_0, GLARE0_TEXEL_SIZE * RESOLUTION_SCALE, GLARE2_SCATTERING, GLARE2_DIRECTION, GLARE2_OFFSET) * GLARE2_INTENSITY;
			#else
				glare += SampleTex2D(PASS_TEXTURE_2D(_Glare2Tex, sampler_Glare2Tex), UV_0);
			#endif
			*/
			glare += SampleTex2D(PASS_TEXTURE_2D(_Glare2Tex, sampler_Glare2Tex), UV_0);
		#endif
		#ifdef MK_GLARE_4
			/*
			#ifdef MK_RENDER_PRIORITY_QUALITY
				glare += GaussianBlur1D(PASS_TEXTURE_2D(_Glare3Tex, sampler_Glare3Tex), UV_0, GLARE0_TEXEL_SIZE * RESOLUTION_SCALE, GLARE3_SCATTERING, GLARE3_DIRECTION, GLARE3_OFFSET) * GLARE3_INTENSITY;
			#else
				glare += SampleTex2D(PASS_TEXTURE_2D(_Glare3Tex, sampler_Glare3Tex), UV_0);
			#endif
			*/
			glare += SampleTex2D(PASS_TEXTURE_2D(_Glare3Tex, sampler_Glare3Tex), UV_0);
		#endif
		#ifdef MK_NATURAL
			glare.rgb = max(0, lerp(source.rgb, glare.rgb * 0.25, GLARE_GLOBAL_INTENSITY));
		#else
			glare *= GLARE_GLOBAL_INTENSITY;
		#endif

		g.rgb = max(0, lerp(g.rgb, glare.rgb, GLARE_BLEND));
	#endif

	#ifdef MK_LENS_FLARE
		g.rgb += SampleTex2DCircularChromaticAberration(PASS_TEXTURE_2D(_LensFlareTex, sampler_LensFlareTex), UV_0, LENS_FLARE_CHROMATIC_ABERRATION).rgb;
	#endif

	#ifdef MK_LENS_SURFACE
		half3 dirt = SampleTex2DNoScale(PASS_TEXTURE_2D(_LensSurfaceDirtTex, sampler_LensSurfaceDirtTex), LENS_SURFACE_DIRT_UV).rgb;
		half3 diffraction = SampleTex2DNoScale(PASS_TEXTURE_2D(_LensSurfaceDiffractionTex, sampler_LensSurfaceDiffractionTex), LENS_DIFFRACTION_UV).rgb;

		#ifdef COLORSPACE_GAMMA
			dirt = GammaToLinearSpace(dirt);
			diffraction = GammaToLinearSpace(diffraction);
		#endif

		#ifdef MK_NATURAL
			g.rgb = lerp(g.rgb, g.rgb + gs.rgb * LENS_SURFACE_DIRT_INTENSITY, dirt);
			g.rgb = lerp(g.rgb, g.rgb + gs.rgb * LENS_SURFACE_DIFFRACTION_INTENSITY, diffraction);
		#else
			dirt *= LENS_SURFACE_DIRT_INTENSITY;
			diffraction *= LENS_SURFACE_DIFFRACTION_INTENSITY;
			g.rgb = lerp(g.rgb * 3, g.rgb + g.rgb * dirt + g.rgb * diffraction, 0.5) * 0.3333h;
		#endif
	#endif
	
	//When using gamma space at least try to get a nice looking result by adding the glow in the linear space of the source even if the base color space is gamma
	#ifdef MK_GLOW_COMPOSITE
		#ifdef COLORSPACE_GAMMA
			#ifndef MK_NATURAL
				g.rgb += source.rgb;
			#endif
			RETURN_TARGET_TEX ConvertToColorSpace(g);
		#else
			#ifndef MK_NATURAL
				g.rgb += source.rgb;
			#endif
			RETURN_TARGET_TEX g;
		#endif
	#else
		RETURN_TARGET_TEX ConvertToColorSpace(g);
	#endif
#endif