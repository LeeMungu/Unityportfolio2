Shader "Unlit/CatonShader"
{
    Properties
    {
        _Os("OutlineBold",Range(0,1)) = 0.0
        //_OutlineBold("OutlineBold",Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque"}
        Pass
        {
            CGPROGRAM
            #pragma vertex _VertexFuc
            #pragma fragment _FragmentFuc
            #include "UnityCG.cginc"
            struct ST_VertexInput//버텍스 INPUT
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct ST_VertexOutput//버텍스 OUTPUT
            {
                float4 vertex : SY_POSTION;
            };

            ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
                {
                    ST_VertexOutput stOutput;
                    float3 fNormalized_Normal = normalize(stInput.normal);
                    float3 fOutline_Position = stInput.vertex + fNormalized_Normal;// +(_OutlineBold * 0.1f);
                    stOutput.vertex = UnityObjectToClipPos(fOutline_Position);
                    return stOutput;
                }
            
                float4 _FragmentFuc(ST_VertexOutput i) : SV_Target
                {
                    return 0.0f;
                }
            ENDCG
        }
    }
}
