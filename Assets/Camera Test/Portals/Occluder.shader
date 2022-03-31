Shader "Maki/Occluder"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background" }

        Stencil
        {
            Ref 123
            Comp NotEqual
        }
        
        ColorMask 0
        ZWrite On

        Pass {}
    }
}
