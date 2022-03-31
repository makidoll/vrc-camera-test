Shader "Maki/Bypass Occluder"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background-1" }
        Cull Off

        Stencil
        {
            Ref 123
            Comp Always
            Pass Replace
        }    
        
        Blend Zero One
        ZWrite Off
        
        Pass {}
    }
}
