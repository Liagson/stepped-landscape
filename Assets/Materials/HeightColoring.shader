Shader "Custom/heightColoring"
{
    Properties
    {
        _Color0("DeepOcean", Color) = (1,1,1,1)
        _Color1("Ocean", Color) = (1,1,1,1)
        _Color2("Shallow water", Color) = (1,1,1,1)
        _Color3("Transition water", Color) = (1,1,1,1)
        _Color4("Sand", Color) = (1,1,1,1)
        _Color5("Low farmlands", Color) = (1,1,1,1)
        _Color6("Farmlands", Color) = (1,1,1,1)
        _Color7("High farmlands", Color) = (1,1,1,1)
        _Color8("Grass", Color) = (1,1,1,1)
        _Color9("Low Forest", Color) = (1,1,1,1)
        _Color10("Forest", Color) = (1,1,1,1)
        _Color11("High forest", Color) = (1,1,1,1)
        _Color12("Hill", Color) = (1,1,1,1)
        _Color13("Low mountain", Color) = (1,1,1,1)
        _Color14("Mountain", Color) = (1,1,1,1)
        _Color15("Snow", Color) = (1,1,1,1)

        _StepSize("Step height", Float) = 20
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #pragma enable_d3d11_debug_symbols

        sampler2D _MainTex;

        struct Input
        {
            float3 worldPos;
        };


        fixed4 _Color0;
        fixed4 _Color1;
        fixed4 _Color2;
        fixed4 _Color3;
        fixed4 _Color4;
        fixed4 _Color5;
        fixed4 _Color6;
        fixed4 _Color7;
        fixed4 _Color8;
        fixed4 _Color9;
        fixed4 _Color10;
        fixed4 _Color11;
        fixed4 _Color12;
        fixed4 _Color13;
        fixed4 _Color14;
        fixed4 _Color15;

        static const int numberOfColours = 16;
        static fixed4 _colorArray[numberOfColours] = {_Color0, _Color1, _Color2, _Color3, _Color4, _Color5, _Color6, _Color7, _Color8,
            _Color9, _Color10, _Color11, _Color12, _Color13, _Color14, _Color15};

        half _Glossiness;
        half _Metallic;
        float _StepSize;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Albedo = _colorArray[numberOfColours - 1];

            for (int layer = 0; layer < numberOfColours; layer++) {
                if (IN.worldPos.y < _StepSize * (1 + layer)) {
                    o.Albedo = _colorArray[layer];
                    break;
                }
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
