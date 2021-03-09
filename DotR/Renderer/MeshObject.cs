#if false
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using SlimDX;
using SlimDX.Direct3D9;

namespace Engine
{
    public class MeshObject
    {
        public string[] extraMeshDirs = new string[0];
        private Mesh _mesh;
        public string meshDir = "";
        private string filename = "";
        protected Material[] _meshMaterials;
        protected string[] _meshTextureFilenames;
        protected Texture[] _meshTextures;
        protected TextureFileClass[] _meshTextures2;
        private static ArrayList _meshFileStructs = new ArrayList();
        private static List<RenderStruct> _renderList = new List<RenderStruct>();
//        private static ArrayList _textureFileStructs = new ArrayList();
        protected static List<TextureFileClass> _textureFileClasses = new List<TextureFileClass>();
        public Matrix last_matrix;// = MyMatrix.Zero;//.Zero;
        private static List<RenderStruct> _renderListA = new List<RenderStruct>();

        public virtual void CreateMesh()
        {
            if (this is IFromFile)
            {
                filename = ((IFromFile)this).Filename;
                filename = meshDir + filename;
                var flag = false;
                foreach (MeshFileStruct struct2 in _meshFileStructs)
                {
                    if (struct2.filename != filename) continue;
                    _mesh = struct2.mesh;
                    _meshMaterials = (Material[])struct2.materials.Clone();
//                    _meshTextures = (Texture[])struct2.textures.Clone();
                    _meshTextures2 = (TextureFileClass[])struct2.textures2.Clone();
                    _meshTextureFilenames = (string[])struct2.textureFilenames.Clone();
                    flag = true;
                }
                if (!flag)
                {
                    try
                    {
//                        _mesh = Mesh.FromFile(filename, MeshFlags.SystemMemory, MyDirect3D.device, out materialArray);
//                        _mesh = Mesh.FromFile(MyDirect3D.device, filename, MeshFlags.SystemMemory);
                        ExtendedMaterial[] materialArray = _mesh.GetMaterials();
                        _meshMaterials = new Material[materialArray.Length];
//                        _meshTextures = new Texture[materialArray.Length];
                        _meshTextures2 = new TextureFileClass[materialArray.Length];
                        _meshTextureFilenames = new string[materialArray.Length];
                        for (var i = 0; i < materialArray.Length; i++)
                        {
                        	_meshMaterials[i] = materialArray[i].MaterialD3D;
                        	if (string.IsNullOrEmpty(materialArray[i].TextureFileName))
                        	{
                        		_meshTextures2[i] = new TextureFileClass { filename = "", memory = null, texture = null } ;
                        		continue;
                        	}
                            _meshTextureFilenames[i] = meshDir + materialArray[i].TextureFileName;
                            LoadTexture(i, _meshTextureFilenames[i]);
                        }
#region trash
                        /*var buffer = _mesh.LockVertexBuffer(LockFlags.ReadOnly);
                        var x = buffer.Read<float>();
                        var y = buffer.Read<float>();
                        var z = buffer.Read<float>();
                        var x1 = buffer.Read<float>();
                        var y1 = buffer.Read<float>();
                        var z1 = buffer.Read<float>();
                        var u = buffer.Read<float>();
                        var v = buffer.Read<float>();*/
                        
                        /*var buffer = _mesh.LockIndexBuffer(LockFlags.ReadOnly);
                        var index1 = buffer.Read<ushort>();
                        var index2 = buffer.Read<ushort>();
                        var index3 = buffer.Read<ushort>();
                        var index4 = buffer.Read<ushort>();
                        var index5 = buffer.Read<ushort>();
                        var index6 = buffer.Read<ushort>();*/
                        
                        /*var buffer = _mesh.LockAttributeBuffer(LockFlags.ReadOnly);
                        var mat1 = buffer.Read<int>();
                        var mat2 = buffer.Read<int>();
                        var mat3 = buffer.Read<int>();
                        var mat4 = buffer.Read<int>();*/
#endregion
                        _mesh.OptimizeInPlace(MeshOptimizeFlags.AttributeSort | MeshOptimizeFlags.VertexCache | MeshOptimizeFlags.Compact);
                        var struct3 = new MeshFileStruct
                        {
                            filename = filename,
                            mesh = _mesh,
                            materials = (Material[])_meshMaterials.Clone(),
//                            textures = (Texture[])_meshTextures.Clone(),
                            textures2 = (TextureFileClass[])_meshTextures2.Clone(),
                            textureFilenames = (string[])_meshTextureFilenames.Clone(),
                        };
                        _meshFileStructs.Add(struct3);
                    }
                    catch (SlimDXException e)
                    {
                        Logger.LogException(e, "Can not load model: " + filename);
                    }
                }
            }
            else
            {
                if (!(this is ICustomCreation))
                {
                    throw new Exception("Internal Error. The mesh object does not have information about building it's mesh.");
                }
                ((ICustomCreation)this).CreateCustomMesh();
            }
        }

        protected void LoadTexture(int index, string filename)
        {
        	
        }
        
        public void Render()
        {
        	if ((this is ICustomCreation) && (_mesh == null))
        	{
        		((ICustomCreation)this).CustomRender();
        		return;
        	}
        	if (this._meshMaterials == null) return;
            try
            {
                var num2 = ((IMatrixObject)this).MatricesCount;
                Matrix matrix;
                for (var j = 0; j < num2; j++)
                {
                    matrix = ((IMatrixObject)this).GetMatrix(j);
//                    if (matrix == MyMatrix.Zero) continue;
                    for (var k = 0; k < _meshMaterials.Length; k++)
                    {
                        if (_meshMaterials[k].Diffuse.Alpha < 1.0f)
                        	_renderListA.Add(new RenderStruct(_meshMaterials[k], _meshTextures2[k].texture, _mesh, k, matrix));
                        else
                        	_renderList.Add(new RenderStruct(_meshMaterials[k], _meshTextures2[k].texture, _mesh, k, matrix));
                    }
                }
            }
            catch (Exception e)
            {
            	Logger.LogException(e, "Render meshes");
            }
        }

        public static void RenderList()
        {
            try
            {
                _renderList.Sort();
            }
            catch (Exception e)
            {
            	Logger.LogException(e, "RenderList");
            }
            foreach (var structArray in _renderList)
            {
                /*MyDirect3D.device.Material = structArray.material;
                MyDirect3D.device.SetTexture(0, structArray.texture);
                MyDirect3D.device.SetTransform(TransformState.World, structArray.matrix);
                structArray.mesh.DrawSubset(structArray.subset);
				/*MyDirect3D.effect.SetValue("texture0", structArray.texture);
				MyDirect3D.effect.SetValue("hasTexture", structArray.texture != null ? true : false);
				MyDirect3D.effect.SetValue("worldViewProjection", structArray.matrix * MyDirect3D.device.Transform.Projection);
				MyDirect3D.effect.Begin(0);
				MyDirect3D.effect.BeginPass(0);
                structArray.mesh.DrawSubset(structArray.subset);
                MyDirect3D.effect.EndPass();
                MyDirect3D.effect.End();*/
            }
            _renderList.Clear();
        }
        
        public static void RenderListA()
        {
            try
            {
                _renderListA.Sort();
            }
            catch (Exception e)
            {
            	Logger.LogException(e, "RenderListA");
            }
            /*foreach (var structArray in _renderListA)
            {
                MyDirect3D.device.Material = structArray.material;
                MyDirect3D.device.SetTexture(0, structArray.texture);
                MyDirect3D.device.SetTransform(TransformState.World, structArray.matrix);
                structArray.mesh.DrawSubset(structArray.subset);
            }*/
            _renderListA.Clear();
        }
                
#region Trash
        /*public void TestToOde(Double3DPoint pos, double ang)
        {
        	int indexcount = _mesh.NumberFaces*3;
        	int vertexcount = _mesh.NumberVertices;
        	d.Vector3[] vertices = new d.Vector3[vertexcount-1];
        	int[] indices = new int[indexcount-1];
        	
        	GetMeshVertexes(_mesh);
        	GetMeshIndexes(_mesh);
        	FixCenter(_mesh);
        	
        	for (int i = 0; i < vertexcount - 1; i++)
        	{
        		vertices[i].X = vertexes[i].Pos.X;
        		vertices[i].Y = vertexes[i].Pos.Y;
        		vertices[i].Z = vertexes[i].Pos.Z;
        	}
        	for (int a = 0; a < indexcount - 1; a++)
        	{
        		indices[a] = indexes[a];
        	}
        	
        	var c = new Double3DPoint(center.X, center.Y, center.Z);
        	
//        	Physics.TestAttach(vertices, indices, vertexcount, indexcount, pos, ang, c);
        }
        
        private void GetMeshVertexes(Mesh mesh, ref MeshVertex[] vertexes)
        {
        	vertexes = (MeshVertex[])mesh.LockVertexBuffer(typeof(MeshVertex),LockFlags.Discard, mesh.NumberVertices);
        	mesh.UnlockVertexBuffer();
        }
        
        private void GetMeshIndexes(Mesh mesh, ref short[] indexes)
        {
        	indexes = (short[])mesh.LockIndexBuffer(typeof(short), LockFlags.None, mesh.NumberFaces*3);
        	mesh.UnlockIndexBuffer();
        }
        
        private void FixCenter(Mesh mesh)
        {
        	using (GraphicsStream data = mesh.LockVertexBuffer(LockFlags.None))
			{
//				Vector3 center;
				Geometry.ComputeBoundingSphere(data, mesh.NumberVertices, mesh.VertexFormat, out center);

				worldFix = Matrix.Translation(-center);

				mesh.UnlockVertexBuffer();
			}
        }*/
#endregion
		
        public interface ICustomCreation : IMatrixObject//private
        {
            void CreateCustomMesh();
            void CustomRender();
        }

        public interface IFromFile : IMatrixObject
        {
            string Filename { get; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MeshFileStruct
        {
            public string filename;
            public Mesh mesh;
            public Material[] materials;
//            public Texture[] textures;
            public TextureFileClass[] textures2;
            public string[] textureFilenames;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RenderStruct : IComparable
        {
            public Material material;
            public Texture texture;
            public Mesh mesh;
            public int subset;
            public Matrix matrix;
            public RenderStruct(Material material, Texture texture, Mesh mesh, int subset, Matrix matrix)
            {
                this.material = material;
                this.texture = texture;
                this.mesh = mesh;
                this.subset = subset;
                this.matrix = matrix;
            }

            public int CompareTo(object obj)
            {
                if (!(obj is RenderStruct))
                {
                    throw new ArgumentException();
                }
                var struct2 = this;
                var struct3 = (RenderStruct)obj;
                if (struct2.texture != struct3.texture)
                {
                    var num = (struct2.texture != null) ? struct2.texture.GetHashCode() : 0;
                    var num2 = (struct3.texture != null) ? struct3.texture.GetHashCode() : 0;
                    if (num != num2)
                    {
                        return (num - num2);
                    }
                    return -1;
                }
                if (struct2.material != struct3.material)
                {
                    if (struct2.material.Ambient.ToArgb() != struct3.material.Ambient.ToArgb())
                    {
                        return (struct2.material.Ambient.ToArgb() - struct3.material.Ambient.ToArgb());
                    }
                    if (struct2.material.Diffuse.ToArgb() != struct3.material.Diffuse.ToArgb())
                    {
                        return (struct2.material.Diffuse.ToArgb() - struct3.material.Diffuse.ToArgb());
                    }
                    if (struct2.material.Emissive.ToArgb() != struct3.material.Emissive.ToArgb())
                    {
                        return (struct2.material.Emissive.ToArgb() - struct3.material.Emissive.ToArgb());
                    }
                    if (struct2.material.Specular.ToArgb() != struct3.material.Specular.ToArgb())
                    {
                        return (struct2.material.Specular.ToArgb() - struct3.material.Specular.ToArgb());
                    }
                    return Math.Sign(struct2.material.Specular.ToArgb() - struct3.material.Specular.ToArgb());
                }
                if (!(struct2.matrix != struct3.matrix))
                {
                    return 0;
                }
                /*if (struct2.matrix.Determinant() != struct3.matrix.Determinant())
                {
                	return Math.Sign(struct2.matrix.Determinant() - struct3.matrix.Determinant());
                }*/
                return 0;//-1;
            }
        }
        
        protected class TextureFileClass
        {
        	public string filename;
            public Texture texture;
            public byte[] memory;
            //public ImageInformation image_info;
            public bool needed = true;
			public bool disposed = true;
            
            //try to load this damned shit
            private bool onLoad = false;
            
            
        }
    }
}
#endif
