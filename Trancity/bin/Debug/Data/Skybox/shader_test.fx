//-----------------------------------------------------------------------------
// Title : Simple Diffuse
// Author: Pieter Germishuys
//-----------------------------------------------------------------------------

//--------------------------------------------------------------------------------------
// Global variables
//--------------------------------------------------------------------------------------
float4x4 worldViewProjection;
bool hasTexture;

float intencity;
float4 ambient_color;

//--------------------------------------------------------------------------------------
// Textures and Samplers
//--------------------------------------------------------------------------------------
texture texture0;
sampler2D texSampler0 : TEXUNIT0 = sampler_state
{
	Texture	  = (texture0);
    MIPFILTER = LINEAR;
    MAGFILTER = LINEAR;
    MINFILTER = LINEAR;
};
 
//--------------------------------------------------------------------------------------
// Structures
//--------------------------------------------------------------------------------------
//Application to Vertex Shader
struct a2v
{ 
    float4 position   : POSITION0;
    float2 texCoord	  : TEXCOORD0;
};
 
//--------------------------------------------------------------------------------------
// Vertex Shader to Pixel Shader
//--------------------------------------------------------------------------------------
struct v2p
{
    float4 position	  : POSITION0;
    float2 texCoord	  : TEXCOORD0;
};

//--------------------------------------------------------------------------------------
// Pixel Shader to Screen
//--------------------------------------------------------------------------------------
struct p2f
{
    float4 color	  : COLOR0;
};
 
//--------------------------------------------------------------------------------------
// Vertex Shader
//--------------------------------------------------------------------------------------
void VShader( in a2v IN, out v2p OUT ) 
{
    //getting to position to object space
    OUT.position = mul(IN.position, worldViewProjection);
    OUT.texCoord = IN.texCoord;
}

void PShader( in v2p IN, out p2f OUT)
{
	if(hasTexture)
		OUT.color = tex2D(texSampler0, IN.texCoord);
	else
		OUT.color = ambient_color;//float4(0.0f, 0.0f, 0.0f, 1.0f);
}

//For ambient lighting
void APShader( in v2p IN, out p2f OUT)
{
	if(hasTexture)
	{
		float4 color = tex2D(texSampler0, IN.texCoord);
		OUT.color = float4(color[0] * intencity, color[1] * intencity, color[2] * intencity, color[3]);
	}
	else
		OUT.color = float4(ambient_color[0] * intencity, ambient_color[1] * intencity, ambient_color[2] * intencity, ambient_color[3]);
}

//--------------------------------------------------------------------------------------
// Techniques
//--------------------------------------------------------------------------------------
technique diffuse
{
    pass p0
    {
        vertexshader = compile vs_2_0 VShader();
        pixelshader = compile ps_2_0 PShader();
    }
}

//Lighting
technique _ambient
{
    pass p0
    {
        vertexshader = compile vs_2_0 VShader();
        pixelshader = compile ps_2_0 APShader();
    }
}

//Skybox (grabbed from internet)
technique simple_skybox
{
    pass p0
    {
	  LIGHTING = FALSE;
        ZENABLE = FALSE;
        ZWRITEENABLE = FALSE;
        ALPHATESTENABLE = FALSE;
        ALPHABLENDENABLE = FALSE;

        CULLMODE = CCW;

        vertexshader = compile vs_2_0 VShader();
        pixelshader = compile ps_2_0 APShader();
    }
}