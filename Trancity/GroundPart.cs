/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 15.10.2013
 * Time: 21:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Common;
using Engine;
using SlimDX;
using SlimDX.Direct3D9;
using System;

namespace Trancity
{
    /// <summary>
    /// Кусок террейна
    /// </summary>
    public class GroundPart : MeshObject, IMatrixObject, MeshObject.ICustomCreation
    {
        private MeshVertex[] vertexes = null;
        private int[] indexes = null;
        private int poly_count;
        private int row;
        private int col;

        public GroundPart(int x, int y)
        {
            row = x;
            col = y;
        }

        public void CreateCustomMesh()
        {
            //TODO: создание террейна
            //схема в тетради по п.м.
            //Система координат:
            // ^ rows (1, 2, ...)
            // | * - *
            // | | \ | <- схема индексирования полигонов
            // | * - *
            // + - > colls (0, rows_count, rows_count * 2, ...)
            poly_count = 2 * (Ground.grid_step - 1) * (Ground.grid_step - 1);//Плевать, что кривое возведение в квадрат - зато типы не преобразовываю//(int)(Math.Pow(2.0, Ground.grid_step) + 0.1);
            indexes = new int[poly_count * 3];
            vertexes = new MeshVertex[Ground.grid_step * Ground.grid_step];
            for (int i = 0; i < Ground.grid_step; i++)
            {
                for (int j = 0; j < Ground.grid_step; j++)
                {
                    vertexes[i * Ground.grid_step + j].Position = new Vector3((float)((-Ground.grid_size / 2) + i * (Ground.grid_size / (double)(Ground.grid_step - 1))), (float)Cheats._random.NextDouble() * 2.0f, (float)((-Ground.grid_size / 2) + j * (Ground.grid_size / (double)(Ground.grid_step - 1))));
                    vertexes[i * Ground.grid_step + j].Normal = new Vector3(0.0f, 1.0f, 0.0f);
                    vertexes[i * Ground.grid_step + j].texcoord = new Vector2((float)i, (float)(Ground.grid_step - j - 1));
                }
            }
            for (int i = 0; i < Ground.grid_step - 1; i++)
            {
                for (int j = 0; j < Ground.grid_step - 1; j++)
                {
                    indexes[(j + (Ground.grid_step - 1) * i) * 6] = i * Ground.grid_step + j;
                    indexes[(j + (Ground.grid_step - 1) * i) * 6 + 1] = indexes[(j + (Ground.grid_step - 1) * i) * 6] + 1;
                    indexes[(j + (Ground.grid_step - 1) * i) * 6 + 2] = indexes[(j + (Ground.grid_step - 1) * i) * 6] + Ground.grid_step;
                    indexes[(j + (Ground.grid_step - 1) * i) * 6 + 3] = indexes[(j + (Ground.grid_step - 1) * i) * 6 + 1];
                    indexes[(j + (Ground.grid_step - 1) * i) * 6 + 4] = indexes[(j + (Ground.grid_step - 1) * i) * 6 + 1] + Ground.grid_step;
                    indexes[(j + (Ground.grid_step - 1) * i) * 6 + 5] = indexes[(j + (Ground.grid_step - 1) * i) * 6 + 2];
                }
            }
            /**/
            /**
        	indexes = new int[32 * 3];
        	vertexes = new MeshVertex[25];
        	vertexes[0].Position = new Vector3(-20.0f, (float)Cheats._random.NextDouble() * 2.0f, -20.0f);
			vertexes[0].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[0].texcoord = new Vector2(0.0f, 4.0f);
			vertexes[1].Position = new Vector3(-20.0f, (float)Cheats._random.NextDouble() * 2.0f, -10.0f);
			vertexes[1].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[1].texcoord = new Vector2(0.0f, 3.0f);
			vertexes[2].Position = new Vector3(-20.0f, (float)Cheats._random.NextDouble() * 2.0f, 0.0f);
			vertexes[2].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[2].texcoord = new Vector2(0.0f, 2.0f);
			vertexes[3].Position = new Vector3(-20.0f, (float)Cheats._random.NextDouble() * 2.0f, 10.0f);
			vertexes[3].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[3].texcoord = new Vector2(0.0f, 1.0f);
			vertexes[4].Position = new Vector3(-20.0f, (float)Cheats._random.NextDouble() * 2.0f, 20.0f);
			vertexes[4].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[4].texcoord = new Vector2(0.0f, 0.0f);
			//
			vertexes[5].Position = new Vector3(-10.0f, (float)Cheats._random.NextDouble() * 2.0f, -20.0f);
			vertexes[5].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[5].texcoord = new Vector2(1.0f, 4.0f);
			vertexes[6].Position = new Vector3(-10.0f, (float)Cheats._random.NextDouble() * 2.0f, -10.0f);
			vertexes[6].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[6].texcoord = new Vector2(1.0f, 3.0f);
			vertexes[7].Position = new Vector3(-10.0f, (float)Cheats._random.NextDouble() * 2.0f, 0.0f);
			vertexes[7].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[7].texcoord = new Vector2(1.0f, 2.0f);
			vertexes[8].Position = new Vector3(-10.0f, (float)Cheats._random.NextDouble() * 2.0f, 10.0f);
			vertexes[8].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[8].texcoord = new Vector2(1.0f, 1.0f);
			vertexes[9].Position = new Vector3(-10.0f, (float)Cheats._random.NextDouble() * 2.0f, 20.0f);
			vertexes[9].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[9].texcoord = new Vector2(1.0f, 0.0f);
			/**
			vertexes[10].Position = new Vector3(0.0f, (float)Cheats._random.NextDouble() * 2.0f, -20.0f);
			vertexes[10].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[10].texcoord = new Vector2(2.0f, 4.0f);
			vertexes[11].Position = new Vector3(0.0f, (float)Cheats._random.NextDouble() * 2.0f, -10.0f);
			vertexes[11].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[11].texcoord = new Vector2(2.0f, 3.0f);
			vertexes[12].Position = new Vector3(0.0f, (float)Cheats._random.NextDouble() * 2.0f, 0.0f);
			vertexes[12].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[12].texcoord = new Vector2(2.0f, 2.0f);
			vertexes[13].Position = new Vector3(0.0f, (float)Cheats._random.NextDouble() * 2.0f, 10.0f);
			vertexes[13].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[13].texcoord = new Vector2(2.0f, 1.0f);
			vertexes[14].Position = new Vector3(0.0f, (float)Cheats._random.NextDouble() * 2.0f, 20.0f);
			vertexes[14].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[14].texcoord = new Vector2(2.0f, 0.0f);
			//
			vertexes[15].Position = new Vector3(10.0f, (float)Cheats._random.NextDouble() * 2.0f, -20.0f);
			vertexes[15].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[15].texcoord = new Vector2(3.0f, 4.0f);
			vertexes[16].Position = new Vector3(10.0f, (float)Cheats._random.NextDouble() * 2.0f, -10.0f);
			vertexes[16].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[16].texcoord = new Vector2(3.0f, 3.0f);
			vertexes[17].Position = new Vector3(10.0f, (float)Cheats._random.NextDouble() * 2.0f, 0.0f);
			vertexes[17].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[17].texcoord = new Vector2(3.0f, 2.0f);
			vertexes[18].Position = new Vector3(10.0f, (float)Cheats._random.NextDouble() * 2.0f, 10.0f);
			vertexes[18].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[18].texcoord = new Vector2(3.0f, 1.0f);
			vertexes[19].Position = new Vector3(10.0f, (float)Cheats._random.NextDouble() * 2.0f, 20.0f);
			vertexes[19].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[19].texcoord = new Vector2(3.0f, 0.0f);
			//
			vertexes[20].Position = new Vector3(20.0f, (float)Cheats._random.NextDouble() * 2.0f, -20.0f);
			vertexes[20].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[20].texcoord = new Vector2(4.0f, 4.0f);
			vertexes[21].Position = new Vector3(20.0f, (float)Cheats._random.NextDouble() * 2.0f, -10.0f);
			vertexes[21].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[21].texcoord = new Vector2(4.0f, 3.0f);
			vertexes[22].Position = new Vector3(20.0f, (float)Cheats._random.NextDouble() * 2.0f, 0.0f);
			vertexes[22].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[22].texcoord = new Vector2(4.0f, 2.0f);
			vertexes[23].Position = new Vector3(20.0f, (float)Cheats._random.NextDouble() * 2.0f, 10.0f);
			vertexes[23].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[23].texcoord = new Vector2(4.0f, 1.0f);
			vertexes[24].Position = new Vector3(20.0f, (float)Cheats._random.NextDouble() * 2.0f, 20.0f);
			vertexes[24].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[24].texcoord = new Vector2(4.0f, 0.0f);
			//*
        	indexes[0] = 0;
        	indexes[1] = 1;
        	indexes[2] = 5;
        	indexes[3] = 1;
        	indexes[4] = 6;
        	indexes[5] = 5;
        	//
        	indexes[6] = 1;
        	indexes[7] = 2;
        	indexes[8] = 6;
        	indexes[9] = 2;
        	indexes[10] = 7;
        	indexes[11] = 6;
        	//
        	indexes[12] = 2;
        	indexes[13] = 3;
        	indexes[14] = 7;
        	indexes[15] = 3;
        	indexes[16] = 8;
        	indexes[17] = 7;
        	//
        	indexes[18] = 3;
        	indexes[19] = 4;
        	indexes[20] = 8;
        	indexes[21] = 4;
        	indexes[22] = 9;
        	indexes[23] = 8;
        	//---/
        	indexes[24] = 5;
        	indexes[25] = 6;
        	indexes[26] = 10;
        	indexes[27] = 6;
        	indexes[28] = 11;
        	indexes[29] = 10;
        	//
        	indexes[30] = 6;
        	indexes[31] = 7;
        	indexes[32] = 11;
        	indexes[33] = 7;
        	indexes[34] = 12;
        	indexes[35] = 11;
        	//
        	indexes[36] = 7;
        	indexes[37] = 8;
        	indexes[38] = 12;
        	indexes[39] = 8;
        	indexes[40] = 13;
        	indexes[41] = 12;
        	//
        	indexes[42] = 8;
        	indexes[43] = 9;
        	indexes[44] = 13;
        	indexes[45] = 9;
        	indexes[46] = 14;
        	indexes[47] = 13;
        	//---
        	indexes[48] = 10;
        	indexes[49] = 11;
        	indexes[50] = 15;
        	indexes[51] = 11;
        	indexes[52] = 16;
        	indexes[53] = 15;
        	//
        	indexes[54] = 11;
        	indexes[55] = 12;
        	indexes[56] = 16;
        	indexes[57] = 12;
        	indexes[58] = 17;
        	indexes[59] = 16;
        	//
        	indexes[60] = 12;
        	indexes[61] = 13;
        	indexes[62] = 17;
        	indexes[63] = 13;
        	indexes[64] = 18;
        	indexes[65] = 17;
        	//
        	indexes[66] = 13;
        	indexes[67] = 14;
        	indexes[68] = 18;
        	indexes[69] = 14;
        	indexes[70] = 19;
        	indexes[71] = 18;
        	//---
        	indexes[72] = 15;
        	indexes[73] = 16;
        	indexes[74] = 20;
        	indexes[75] = 16;
        	indexes[76] = 21;
        	indexes[77] = 20;
        	//
        	indexes[78] = 16;
        	indexes[79] = 17;
        	indexes[80] = 21;
        	indexes[81] = 17;
        	indexes[82] = 22;
        	indexes[83] = 21;
        	//
        	indexes[84] = 17;
        	indexes[85] = 18;
        	indexes[86] = 22;
        	indexes[87] = 18;
        	indexes[88] = 23;
        	indexes[89] = 22;
        	//
        	indexes[90] = 18;
        	indexes[91] = 19;
        	indexes[92] = 23;
        	indexes[93] = 19;
        	indexes[94] = 24;
        	indexes[95] = 23;
        	//--
        	/**//*
        	indexes = new int[6];
        	vertexes = new MeshVertex[4];
        	indexes[0] = 0;
        	indexes[1] = 1;
        	indexes[2] = 2;
        	indexes[3] = 3;
        	indexes[4] = 2;
        	indexes[5] = 1;
			vertexes[0].Position = new Vector3(-500.0f, 0.0f, -500.0f);
			vertexes[0].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[0].texcoord = new Vector2(-1.0f, 0.0f);
			vertexes[1].Position = new Vector3(-500.0f, 0.0f, 500.0f);
			vertexes[1].texcoord = new Vector2(-1.0f, -1.0f);
			vertexes[1].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[3].Position = new Vector3(500.0f, 0.0f, 500.0f);
			vertexes[3].texcoord = new Vector2(4.749745e-08f, -1.0f);
			vertexes[3].Normal = new Vector3(0.0f, 1.0f, 0.0f);
			vertexes[2].Position = new Vector3(500.0f, 0.0f, -500.0f);
			vertexes[2].texcoord = new Vector2(4.749745e-08f, 0.0f);
			vertexes[2].Normal = new Vector3(0.0f, 1.0f, 0.0f);*/
            _meshMaterials = new Material[1];
            _meshMaterials[0].Diffuse = new Color4(1.0f, 1.0f, 1.0f, 1.0f);//0.949f
            _meshMaterials[0].Specular = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            _meshMaterials[0].Ambient = new Color4(1.0f, 0.0f, 0.0f, 0.0f);
            _meshMaterials[0].Emissive = new Color4(1.0f, 0.0f, 0.0f, 0.0f);
            _meshMaterials[0].Power = 0;
            _meshTextures = new Texture[1];
            //			_addTexList = new MeshObject.AdditionalTexStruct[1];
            LoadTexture(0, "Ground_test.png");
            _meshTextures[0].LevelOfDetail = 0;

        }

        public void CustomRender()
        {
            if (MyDirect3D.Alpha) return;
            //			MyDirect3D.test_effect.SetTexture("texture0", _meshTextures[0]);
            //			MyDirect3D.test_effect.SetValue("hasTexture", true);
            //			MyDirect3D.test_effect.SetValue("intencity", MyDirect3D.light_intency);
            //			MyDirect3D.test_effect.SetValue("worldViewProjection", ((IMatrixObject)this).GetMatrix(0) * MyDirect3D.device.GetTransform(TransformState.Projection));
            //			MyDirect3D.test_effect.Begin(0);
            //			MyDirect3D.test_effect.BeginPass(0);
            MyDirect3D.device.Material = _meshMaterials[0];
            MyDirect3D.device.SetTexture(0, _meshTextures[0]);
            MyDirect3D.device.SetTransform(TransformState.World, ((IMatrixObject)this).GetMatrix(0));
            MyDirect3D.device.DrawIndexedUserPrimitives<int, MeshVertex>(PrimitiveType.TriangleList, 0, 0, 0, vertexes.Length, poly_count, indexes, Format.Index32, vertexes, 32);
            //            MyDirect3D.test_effect.EndPass();
            //            MyDirect3D.test_effect.End();
        }

        public Matrix GetMatrix(int index)
        {
            return Matrix.Translation((float)(Ground.grid_size * row), 0.0f, (float)(Ground.grid_size * col));
        }

        public int MatricesCount
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Based on netlib.narod.ru/library/book0032/ch13_05.htm
        /// </summary>
        /// <param name="pos">XZ-point</param>
        /// <returns>height</returns>
        public double GetHeight(DoublePoint pos)
        {
            //TODO: нахождение высоты
            pos.x += Ground.grid_size / 2.0;//20.0;
            pos.y += Ground.grid_size / 2.0;//20.0;
            pos.x /= Ground.grid_size / (Ground.grid_step - 1);//10.0;
            pos.y /= Ground.grid_size / (Ground.grid_step - 1);//10.0;
            int coll = (int)Math.Floor(pos.x);
            int row = (int)Math.Floor(pos.y);
            //^c d
            //|a b
            // - >
            var A = GetVertexHeight(coll, row);
            var B = GetVertexHeight(coll + 1, row);
            var C = GetVertexHeight(coll, row + 1);
            var D = GetVertexHeight(coll + 1, row + 1);
            var dx = pos.x - coll;
            var dy = pos.y - row;
            double height = 0.0;
            if (dy < 1.0 - dx)
            {
                double uy = B - A;
                double vy = C - A;

                height = A + MyFeatures.Lerp(0.0, uy, dx) +
                             MyFeatures.Lerp(0.0, vy, dy);
            }
            else
            {
                double uy = C - D;
                double vy = B - D;

                height = D + MyFeatures.Lerp(0.0, uy, 1.0 - dx) +
                             MyFeatures.Lerp(0.0, vy, 1.0 - dy);
            }
            return height;
        }

        private double GetVertexHeight(int coll, int row)
        {
            //			try {
            return vertexes[row + Ground.grid_step * coll].Position.Y;
            //			} catch { return -0.1; };
        }
    }
}