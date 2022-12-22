// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TransparencyEffect"
{
	Properties
	{
		_FresnelPower("FresnelPower", Float) = 1
		_FresnelScale("FresnelScale", Float) = 1
		[HDR]_FresnelColor("FresnelColor", Color) = (0.4784314,0.7764706,1.968627,1)
		_FresnelBias("FresnelBias", Range( 0 , 1)) = 0
		_OuterFresnel("OuterFresnel", Range( 0 , 1)) = 1
		_InnerFresnel("InnerFresnel", Range( 0 , 1)) = 1
		_GlobalOpacity("GlobalOpacity", Float) = 1
		_FlickerSpeed("FlickerSpeed", Float) = 3
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float _FlickerSpeed;
		uniform float _OuterFresnel;
		uniform float _FresnelBias;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform float _InnerFresnel;
		uniform float4 _FresnelColor;
		uniform float _GlobalOpacity;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime37 = _Time.y * _FlickerSpeed;
			float simplePerlin2D43 = snoise( float2( 0.6,0 )*mulTime37 );
			simplePerlin2D43 = simplePerlin2D43*0.5 + 0.5;
			float Flicker48 = (0.5 + (simplePerlin2D43 - 0.0) * (1.0 - 0.5) / (1.0 - 0.0));
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV1 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode1 = ( _FresnelBias + _FresnelScale * pow( 1.0 - fresnelNdotV1, _FresnelPower ) );
			float clampResult15 = clamp( ( ( _OuterFresnel * fresnelNode1 ) + ( ( 1.0 - fresnelNode1 ) * _InnerFresnel ) ) , 0.0 , 1.0 );
			float FresnelMask6 = clampResult15;
			float4 Fresnel31 = ( FresnelMask6 * _FresnelColor );
			o.Emission = ( Flicker48 * Fresnel31 ).rgb;
			o.Alpha = ( _GlobalOpacity * FresnelMask6 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				float3 worldNormal : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
0;23;1680;1006;-1227.093;382.9232;1.090001;True;True
Node;AmplifyShaderEditor.CommentaryNode;25;-894.1648,-51.25328;Inherit;False;2022.823;554.3338;Fresnel;16;7;31;29;6;28;15;13;12;16;8;11;10;9;1;4;3;;0.3545746,0.7216536,0.9056604,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-746.8495,267.2142;Inherit;False;Property;_FresnelPower;FresnelPower;0;0;Create;True;0;0;False;0;False;1;0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-853.7702,105.839;Inherit;False;Property;_FresnelBias;FresnelBias;3;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-744.1917,184.807;Inherit;False;Property;_FresnelScale;FresnelScale;1;0;Create;True;0;0;False;0;False;1;4.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;1;-499.4447,122.9377;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-478.7745,32.6182;Inherit;False;Property;_OuterFresnel;OuterFresnel;4;0;Create;True;0;0;False;0;False;1;0.542;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-477.7279,343.8622;Inherit;False;Property;_InnerFresnel;InnerFresnel;5;0;Create;True;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-128.3563,42.09023;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;11;-134.7551,249.6972;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;16;155.9451,86.45643;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;51;-221.8052,570.4824;Inherit;False;1344.687;288.7435;Flicker;5;45;48;44;43;37;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;29.53009,325.707;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-162.1866,681.7503;Inherit;False;Property;_FlickerSpeed;FlickerSpeed;7;0;Create;True;0;0;False;0;False;3;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;206.5113,130.3828;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;37;57.92609,686.3542;Inherit;False;1;0;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;15;387.5295,128.9039;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;43;341.1173,658.6628;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0.6,0;False;1;FLOAT;51.04;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;28;574.5678,299.0117;Inherit;False;Property;_FresnelColor;FresnelColor;2;1;[HDR];Create;True;0;0;False;0;False;0.4784314,0.7764706,1.968627,1;0.6509804,0.9490196,2,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;6;568.2646,125.202;Inherit;False;FresnelMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;44;626.8326,663.4523;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;780.1584,202.046;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;31;926.2588,195.8047;Inherit;True;Fresnel;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;48;893.6827,658.9494;Inherit;False;Flicker;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;1667.44,103.7978;Inherit;False;31;Fresnel;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;32;1409.306,275.5676;Inherit;False;6;FresnelMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;1412.075,197.5184;Inherit;False;Property;_GlobalOpacity;GlobalOpacity;6;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;49;1665.455,14.32322;Inherit;False;48;Flicker;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;1868.049,287.8065;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;2012.81,100.4575;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;85;2262.324,-190.6866;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;77;1442.807,-246.34;Inherit;False;Constant;_Vector0;Vector 0;8;0;Create;True;0;0;False;0;False;20,20;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.NoiseGeneratorNode;76;1949.568,-195.0579;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.06;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;78;1709.614,-180.1852;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;29.81,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;1480.101,-126.0943;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;1592.569,-55.47655;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2526.476,4.246702;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Telekinetic;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;1;7;0
WireConnection;1;2;3;0
WireConnection;1;3;4;0
WireConnection;10;0;9;0
WireConnection;10;1;1;0
WireConnection;11;0;1;0
WireConnection;16;0;10;0
WireConnection;12;0;11;0
WireConnection;12;1;8;0
WireConnection;13;0;16;0
WireConnection;13;1;12;0
WireConnection;37;0;45;0
WireConnection;15;0;13;0
WireConnection;43;1;37;0
WireConnection;6;0;15;0
WireConnection;44;0;43;0
WireConnection;29;0;6;0
WireConnection;29;1;28;0
WireConnection;31;0;29;0
WireConnection;48;0;44;0
WireConnection;35;0;34;0
WireConnection;35;1;32;0
WireConnection;83;0;49;0
WireConnection;83;1;33;0
WireConnection;85;0;76;0
WireConnection;76;0;78;0
WireConnection;78;0;77;0
WireConnection;78;1;81;0
WireConnection;81;0;37;0
WireConnection;0;2;83;0
WireConnection;0;9;35;0
ASEEND*/
//CHKSM=BAFC330B3B3A19A0299FBC9A3B004997F01B7DDC