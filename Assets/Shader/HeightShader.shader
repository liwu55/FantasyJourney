Shader "MyShader/HeightShader"
{
    Properties
    {
        _height("可渲染最大高度",Range(0,4))  = 0
        _mainTex("主纹理",2D) = ""{}
    }
    
    SubShader
    {
    
        Pass{
            CGPROGRAM
            #pragma vertex vertexStart
            #pragma fragment fragStart
            #include "UnityCG.cginc"
            
            struct v2f
            {
                half4 pos:SV_POSITION;
                float2 texcoor:TEXCOORD0;
                half4 worldPosition:TEXCOORD1;
            };
            
            sampler2D _mainTex;
            float _height;
            
            v2f vertexStart(appdata_base input)
            {
                v2f output;
                output.pos = UnityObjectToClipPos(input.vertex);
                output.texcoor=input.texcoord.xy;
                output.worldPosition = mul(unity_ObjectToWorld,input.vertex);
                return output;
            }
            
            fixed4 fragStart(v2f v2f):SV_TARGET
            {
                if(v2f.worldPosition.y>_height){
                    discard;
                }
                return fixed4(tex2D(_mainTex,v2f.texcoor).xyz,1);
            }
            
            ENDCG
        }
    }
}