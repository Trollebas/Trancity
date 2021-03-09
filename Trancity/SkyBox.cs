/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 01.01.2013
 * Time: 1:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Common;
using SlimDX;
using SlimDX.Direct3D9;
using System.Windows.Forms;

namespace Trancity
{
    public class SkyBox : MeshObject, MeshObject.ICustomCreation
    {
        private MeshVertex[] vertexes = null;
        private Effect effect;
        private bool created = false;
        public static bool draw = false;

        public override void CreateMesh()
        {
            base.meshDir = Application.StartupPath + @"\Data\Skybox\";
            /*try
			{*/
            effect = Effect.FromFile(MyDirect3D.device, meshDir + "shader_test.fx", ShaderFlags.SkipValidation);
            effect.Technique = "simple_skybox";
            /*}
			catch
			{
				
			}*/
            base._meshMaterials = new Material[1];
            _meshMaterials[0] = new Material();
            _meshMaterials[0].Diffuse = new Color4(1.0f, 1.0f, 1.0f, 1.0f);//0.949f
            _meshMaterials[0].Specular = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            _meshMaterials[0].Ambient = new Color4(1.0f, 0.0f, 0.0f, 0.0f);
            _meshMaterials[0].Emissive = new Color4(1.0f, 0.0f, 0.0f, 0.0f);
            _meshMaterials[0].Power = 0;
            base._meshTextures = new Texture[1];
            //			_addTexList = new MeshObject.AdditionalTexStruct[1];
            LoadTexture(0, meshDir + "Above_The_Sea.jpg");//"SkyBox_Collapsed.png"
            CreateCustomMesh();
            created = true;
        }

        public void CreateCustomMesh()
        {
            vertexes = new MeshVertex[36];

            //front side:
            vertexes[0].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertexes[0].texcoord = new Vector2(0.5f, 0.66566f);

            vertexes[1].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertexes[1].texcoord = new Vector2(0.5f, 0.33433f);

            vertexes[2].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertexes[2].texcoord = new Vector2(0.75f, 0.66566f);

            vertexes[3] = vertexes[2];

            vertexes[4] = vertexes[1];

            vertexes[5].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertexes[5].texcoord = new Vector2(0.75f, 0.33433f);

            //right side:
            vertexes[6] = vertexes[2];

            vertexes[7] = vertexes[5];

            vertexes[8].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertexes[8].texcoord = new Vector2(1.0f, 0.66566f);

            vertexes[9] = vertexes[5];

            vertexes[10].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertexes[10].texcoord = new Vector2(1.0f, 0.33433f);

            vertexes[11] = vertexes[8];

            //back side:
            vertexes[12].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertexes[12].texcoord = new Vector2(0.0f, 0.66566f);

            vertexes[13].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertexes[13].texcoord = new Vector2(0.0f, 0.33433f);

            vertexes[14].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertexes[14].texcoord = new Vector2(0.25f, 0.66666f);

            vertexes[16] = vertexes[13];

            vertexes[15] = vertexes[14];

            vertexes[17].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertexes[17].texcoord = new Vector2(0.25f, 0.33433f);

            //left side:
            vertexes[18] = vertexes[14];

            vertexes[19] = vertexes[17];

            vertexes[20] = vertexes[0];

            vertexes[21] = vertexes[17];

            vertexes[22] = vertexes[1];

            vertexes[23] = vertexes[0];

            //bottom side:
            /*vertexes[24] = vertexes[12];
			
			vertexes[25] = vertexes[14];
			
			vertexes[26].Position = new Vector3(1.0f, -1.0f, -1.0f);
			vertexes[26].texcoord = new Vector2(0.0f, 1.0f);
			
			vertexes[27] = vertexes[26];
			
			vertexes[28] = vertexes[14];			
			
			vertexes[29].Position = new Vector3(1.0f, -1.0f, 1.0f);
			vertexes[29].texcoord = new Vector2(0.25f, 1.0f);*/

            vertexes[24].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            vertexes[24].texcoord = new Vector2(0.25021f, 0.66666f);

            vertexes[25].Position = new Vector3(1.0f, -1.0f, 1.0f);
            vertexes[25].texcoord = new Vector2(0.499f, 0.66566f);

            vertexes[26].Position = new Vector3(1.0f, -1.0f, -1.0f);
            vertexes[26].texcoord = new Vector2(0.499f, 0.999f);

            vertexes[28] = vertexes[26];

            vertexes[27] = vertexes[24];

            vertexes[29].Position = new Vector3(-1.0f, -1.0f, -1.0f);
            vertexes[29].texcoord = new Vector2(0.2502f, 0.999f);

            //top side:
            /*vertexes[31] = vertexes[5];
			
			vertexes[30] = vertexes[10];
			
			vertexes[32].Position = new Vector3(1.0f, 1.0f, 1.0f);
			vertexes[32].texcoord = new Vector2(0.75f, 0.0f);
			
			vertexes[33] = vertexes[32];
			
			vertexes[34].Position = new Vector3(-1.0f, 1.0f, 1.0f);
			vertexes[34].texcoord = new Vector2(1.0f, 0.0f);
			
			vertexes[35] = vertexes[10];*/

            vertexes[30].Position = new Vector3(1.0f, 1.0f, 1.0f);
            vertexes[30].texcoord = new Vector2(0.4989f, 0.33433f);

            vertexes[31].Position = new Vector3(-1.0f, 1.0f, 1.0f);
            vertexes[31].texcoord = new Vector2(0.2502f, 0.33433f);

            vertexes[32].Position = new Vector3(1.0f, 1.0f, -1.0f);
            vertexes[32].texcoord = new Vector2(0.4989f, 0.0004f);

            vertexes[33] = vertexes[31];

            vertexes[34].Position = new Vector3(-1.0f, 1.0f, -1.0f);
            vertexes[34].texcoord = new Vector2(0.2502f, 0.0004f);

            vertexes[35] = vertexes[32];
        }

        public virtual Matrix GetMatrix(int index)
        {
            return Matrix.Translation((float)MyDirect3D.Camera_Position.x, (float)MyDirect3D.Camera_Position.y, (float)MyDirect3D.Camera_Position.z);
        }

        public virtual int MatricesCount
        {
            get
            {
                return 1;
            }
        }

        public virtual void CustomRender()
        {
            if ((MyDirect3D.Alpha) || (!created)) return;
            using (VertexDeclaration decl = new VertexDeclaration(MyDirect3D.device, MeshVertex.Format))
            {
                /*MyDirect3D.device.SetTransform(TransformState.World, ((IMatrixObject)this).GetMatrix(0));
				MyDirect3D.device.Material = _meshMaterials[0];
				MyDirect3D.device.SetTexture(0, _meshTextures[0]);*/
                effect.SetTexture("texture0", _meshTextures[0]);
                effect.SetValue("hasTexture", true);
                effect.SetValue("intencity", MyDirect3D.light_intency);
                effect.SetValue("worldViewProjection", ((IMatrixObject)this).GetMatrix(0) * MyDirect3D.device.GetTransform(TransformState.Projection));
                effect.Begin(0);
                effect.BeginPass(0);
                MyDirect3D.device.DrawUserPrimitives<MeshVertex>(PrimitiveType.TriangleList, vertexes.Length / 3, vertexes);
                effect.EndPass();
                effect.End();
            }
        }
    }
}