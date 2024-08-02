namespace Common
{
//    using Microsoft.DirectX;
//    using Microsoft.DirectX.Direct3D;
    using SlimDX;
    using SlimDX.Direct3D9;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Threading;
    using Trancity;

    public class MeshObject
    {
        public string[] extraMeshDirs = new string[0];
        private Mesh _mesh;
        public string meshDir = "";
        private string filename = "";
        protected Material[] _meshMaterials;
        protected string[] _meshTextureFilenames;
        protected Texture[] _meshTextures;
        private static ArrayList _meshFileStructs = new ArrayList();
        private static List<RenderStruct> _renderList = new List<RenderStruct>();
//        private static ArrayList _textureFileStructs = new ArrayList();
        protected static List<TextureFileStruct> _textureFileStructs = new List<TextureFileStruct>();
        public Trancity.AABB bounding_box = null;
        public Trancity.Sphere bounding_sphere = null;
        public Matrix last_matrix = MyMatrix.Zero;//.Zero;
        /*private VertexBuffer vb = null;
        private IndexBuffer ib = null;
        public MeshVertex[] vertexes;
        public short[] indexes;
        public Matrix worldFix;
        public Vector3 center;
        public int vertexcount;*/
        /*private static List<byte[]> _texBytes = new List<byte[]>();
        private static List<Texture> _allTextures = new List<Texture>();*/
//        private static List<int> _texOwners = new List<int>();
        private static StreamWork stream = new StreamWork();
        public static object renderobj = new object();
        public static ThreadTest manager;
        private static List<RenderStruct> _renderList2 = new List<RenderStruct>();
        private static List<RenderStruct> _renderListA = new List<RenderStruct>();
        protected AdditionalTexStruct[] _addTexList;// = new List<AdditionalTexStruct>();
        public static SimpleTimer timer = new SimpleTimer(1.0);

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
                    _meshTextures = (Texture[])struct2.textures.Clone();
                    _meshTextureFilenames = (string[])struct2.textureFilenames.Clone();
                    _addTexList = (AdditionalTexStruct[])struct2._textStr.Clone();
                    flag = true;
                }
                if (!flag)
                {
                    ExtendedMaterial[] materialArray;
                    //System.Windows.Forms.MessageBox.Show(Directory.GetCurrentDirectory() + filename);
                    try
                    {
//                        _mesh = Mesh.FromFile(filename, MeshFlags.SystemMemory, MyDirect3D.device, out materialArray);
                        _mesh = Mesh.FromFile(MyDirect3D.device, filename, MeshFlags.SystemMemory);
                        materialArray = _mesh.GetMaterials();
                        _meshMaterials = new Material[materialArray.Length];
                        _meshTextures = new Texture[materialArray.Length];
                        _meshTextureFilenames = new string[materialArray.Length];
//                        _addTexList = new List<AdditionalTexStruct>();
                        _addTexList = new AdditionalTexStruct[materialArray.Length];
                        for (var i = 0; i < materialArray.Length; i++)
                        {
                        	_meshMaterials[i] = materialArray[i].MaterialD3D;
//                        	_addTexList.Add(new AdditionalTexStruct() { loaded = false, arpos = -1 } );
                            if (string.IsNullOrEmpty(materialArray[i].TextureFileName)) continue;
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
                        
                        _mesh = _mesh.Optimize(MeshOptimizeFlags.AttributeSort | MeshOptimizeFlags.VertexCache | MeshOptimizeFlags.Compact);
                        var struct3 = new MeshFileStruct
                        {
                            filename = filename,
                            mesh = _mesh,
                            materials = (Material[])_meshMaterials.Clone(),
                            textures = (Texture[])_meshTextures.Clone(),
                            textureFilenames = (string[])_meshTextureFilenames.Clone(),
                            _textStr = (AdditionalTexStruct[])_addTexList.Clone()
                        };
                        _meshFileStructs.Add(struct3);
                    }
                    catch (SlimDXException e)//(Exception e)
                    {
//                        System.Windows.Forms.MessageBox.Show("Couldn't load model:\n" + filename);//("File not found!\n" + filename);//(e.Source);
//                        throw new SlimDXException(e.Message + "\nCouldn't load model: " + filename);
                        Logging.WriteExc(e, "Couldn't load model: " + filename);
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
            foreach (var str in extraMeshDirs)
            {
                if (!File.Exists(str + filename)) continue;
                filename = str + filename;
                break;
            }
            if (!File.Exists(filename))
            {
//                throw new FileNotFoundException("Texture file not found!", filename);
                Logging.WriteExc(new FileNotFoundException("Texture file not found!", filename));
                return;
            }
            byte[] _bytes;
            using (var strw = new StreamWork())
	        {
	        	_bytes = strw.LoadData(filename);
	        }
            /*for (int i = 0; i < _texBytes.Count; i++)
            {
            	if (!MyFeatures.ByteEquals(_bytes, _texBytes[i])) continue;
            	_meshTextures[index] = _allTextures[i];
            	return;
            }*/
            /*AdditionalTexStruct _str = new MeshObject.AdditionalTexStruct();
            if (_addTexList.Count != 0)
            {
            	_str = _addTexList[index];
            	_str.loaded = true;
            }*/
            var flag = ((_addTexList != null) && (index < _addTexList.Length));
            if (flag) _addTexList[index].loaded = true;
//            foreach (TextureFileStruct _struct in _textureFileStructs)
            for (int i = 0; i < _textureFileStructs.Count; i++)
            {
            	if (!MyFeatures.ByteEquals(_textureFileStructs[i].memory, _bytes)) continue;
            	_meshTextures[index] = _textureFileStructs[i].texture;
            	if (flag) _addTexList[index].arpos = i;
            	return;
            }
            _meshTextures[index] = Texture.FromMemory(MyDirect3D.device, _bytes);
            _meshTextures[index].GenerateMipSublevels();
            if (flag) _addTexList[index].arpos = _textureFileStructs.Count;
            var struct3 = new TextureFileStruct { filename = filename, memory = _bytes, texture = _meshTextures[index], needed = false };
            _textureFileStructs.Add(struct3);
            /*_allTextures.Add(_meshTextures[index]);
            _texBytes.Add(_bytes);*/
            /*var flag = false;
            /*foreach (TextureFileStruct struct2 in _textureFileStructs)
            {
            	if (struct2.filename != filename) continue;
                _meshTextures[index] = struct2.texture;
                flag = true;
            }
            if (flag) return;
            _meshTextures[index] = Texture.FromFile(MyDirect3D.device, filename);
			var struct3 = new TextureFileStruct { filename = filename, texture = _meshTextures[index] };
            _textureFileStructs.Add(struct3);*/
        }
        
        /*public static void AfterCreating()
        {
        	_texBytes.Clear();
        	_texBytes = null;
        }*/
        
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
                var flag = false;
                int p = 0;
                for (var i = 0; i < _meshMaterials.Length; i++)
                {//0xff
                    if ((_meshMaterials[i].Diffuse.Alpha < 1.0f) != MyDirect3D.Alpha) continue;
                    flag = true; p = i;
                    break;
                }
                if (flag)
                {
                    var num2 = ((IMatrixObject)this).MatricesCount;
                    for (var j = 0; j < num2; j++)
                    {
                        var matrix = ((IMatrixObject)this).GetMatrix(j);
                        if (matrix == MyMatrix.Zero) continue;//Matrix.Identity
                        for (var k = p; k < _meshMaterials.Length; k++)
                        {//0xff
                            if ((_meshMaterials[k].Diffuse.Alpha < 1.0f) != MyDirect3D.Alpha) _renderListA.Add(new RenderStruct(_meshMaterials[k], _meshTextures[k], _mesh, k, matrix));//continue;
                            else _renderList.Add(new RenderStruct(_meshMaterials[k], _meshTextures[k], _mesh, k, matrix));
                            /*if ((_addTexList[k].loaded) && ((_meshTextures[k] == null) || (_meshTextures[k].Disposed)))
                            {
                            	var str = _textureFileStructs[_addTexList[k].arpos];
                            	if ((str.texture == null) || (str.texture.Disposed)) str.needed = true;
                            	else _meshTextures[k] = str.texture;
                            	_textureFileStructs[_addTexList[k].arpos]= str;
                            }/**/
                        }
                    }
                }
            }
            catch (Exception e)
            {
            	Logging.WriteExc(e, "Render meshes");
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
            	Logging.WriteExc(e, "RenderList");
//                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            /*var structArray = _renderList.ToArray();
            for (var i = 0; i < structArray.Length; i++)
            {
                MyDirect3D.device.Material = structArray[i].material;
                MyDirect3D.device.SetTexture(0, structArray[i].texture);
                MyDirect3D.device.Transform.World = structArray[i].matrix;
                structArray[i].mesh.DrawSubset(structArray[i].subset);
            }*/
            foreach (var structArray in _renderList)
            {
                MyDirect3D.device.Material = structArray.material;
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
//            if (manager != null) _renderList2.AddRange(_renderList);
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
            	Logging.WriteExc(e, "RenderListA");
            }
            foreach (var structArray in _renderListA)
            {
                MyDirect3D.device.Material = structArray.material;
                MyDirect3D.device.SetTexture(0, structArray.texture);
                MyDirect3D.device.SetTransform(TransformState.World, structArray.matrix);
                structArray.mesh.DrawSubset(structArray.subset);
            }
//            if (manager != null) _renderList2.AddRange(_renderListA);
            _renderListA.Clear();
        }
        
        public static void BeginManager()
        {
        	if ((MainForm.no_thread) || (MainForm.in_editor)) return;
        	manager = new ThreadTest(WorkThread);
        }
        
        private static void WorkThread()
        {
        	do
        	{
        		if (timer.flag)//lock (timer/*renderobj*/)
        		{
        			try {
        			if (_renderList2.Count != 0)
        			{
        				bool flag;
        				for (int i = 0; i < _textureFileStructs.Count; i++)
	        			{
        					if (_textureFileStructs[i].texture == null) continue;
        					if (!_textureFileStructs[i].texture.Disposed)
        					{
        						flag = false;
		        				foreach (var renderstruct in _renderList2)
		        				{
		        					if (renderstruct.texture == _textureFileStructs[i].texture)
		        					{
		        						flag = true;
		        						break;
		        					}
		        				}
		        				if (!flag)
		        				{
		        					_textureFileStructs[i].texture.Dispose();
		        					var str = _textureFileStructs[i];
	        						str.texture = null;
	        						str.memory = null;
	        						_textureFileStructs[i] = str;
		        				}
        					}
	        				/*else
	        				{
	        					var str = _textureFileStructs[i];
	        					str.texture = null;
	        					Logging.Write("Зашли в null");
	        					_textureFileStructs[i] = str;
	        				}*/
	        			}
        			}
        			_renderList2.Clear(); int z = 0;
					for (int i = 0; i < _textureFileStructs.Count; i++)
	        		{
						if ((_textureFileStructs[i].needed) && ((_textureFileStructs[i].texture == null) || (_textureFileStructs[i].texture.Disposed)))
        				{
							var str = _textureFileStructs[i];
							str.texture = null;
							byte[] bytes;
        					if (str.memory == null) bytes = stream.StartAsyncLoad(_textureFileStructs[i].filename);
        					else bytes = str.memory;
        					if (bytes.Length != 0)
        					{
        						if (str.memory == null) str.memory = bytes;
        						if (z <= 0)
        						{
        							str.texture = Texture.FromMemory(MyDirect3D.device, bytes, Usage.None, Pool.Managed);
        							str.needed = false;
        							z++;
        						}
        					}
        					_textureFileStructs[i] = str;
        					/*if (z > 0)
        						break;*/
        				}
					}}
        			catch (Exception exc)
        			{
        				Logging.WriteExc(exc);
//        				MessageBox.Show("Error in additional render thread!");
        				_renderList2.Clear();
        			}
        		}
        		Thread.Sleep(300);
        	}
        	while (!MyDirectInput.alt_f4);
        }
        
        public void AttachToManager(Material material, Texture texture)
        {
        	if (manager != null) _renderList2.Add(new RenderStruct(material, texture, null, 0, Matrix.Identity));
        }
        
        /*private void AsyncCreating(byte[] buffer)
        {
        	if (buffer.Length == 0) return;
        	_mesh = Mesh.FromMemory(MyDirect3D.device, buffer, MeshFlags.SystemMemory);
        }*/
        
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
            public Texture[] textures;
            public string[] textureFilenames;
            public AdditionalTexStruct[] _textStr;
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

        [StructLayout(LayoutKind.Sequential)]
        protected struct TextureFileStruct
        {
            public string filename;
            public byte[] memory;
            public Texture texture;
            public bool needed;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        protected struct AdditionalTexStruct
        {
            public bool loaded;
            public int arpos;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        protected struct MeshVertex
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 texcoord;
            
			public MeshVertex(Vector3 pos, Vector3 norm, Vector2 uv)
            {
            	Position = pos;
            	Normal = norm;
            	texcoord = uv;
            }
            
            public static VertexElement[] Format
            {
            	get
            	{
            		return new VertexElement[] {
            			new VertexElement(0, 0, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Position, 0),
            			new VertexElement(0, 12, DeclarationType.Float3, DeclarationMethod.Default, DeclarationUsage.Normal, 0),
            			new VertexElement(0, 24, DeclarationType.Float2, DeclarationMethod.Default, DeclarationUsage.TextureCoordinate, 0),
            			VertexElement.VertexDeclarationEnd
            		};
            	}
            }
        }
    }
}

