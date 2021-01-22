using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using SlimDX;
using SlimDX.Direct3D9;
using Trancity;
using Engine;

namespace Common
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
#if TEXCL
        protected TextureFileClass[] _meshTextures2;
#endif
        private static ArrayList _meshFileStructs = new ArrayList();
        private static List<RenderStruct> _renderList = new List<RenderStruct>();
        protected static List<TextureFileStruct> _textureFileStructs = new List<TextureFileStruct>();
#if TEXCL
        protected static List<TextureFileClass> _textureFileClasses = new List<TextureFileClass>();
#endif
        public Engine.AABB bounding_box = null;
        public Engine.Sphere bounding_sphere = null;
        public Matrix last_matrix = MyMatrix.Zero;//.Zero;
        private static List<RenderStruct> _renderListA = new List<RenderStruct>();
        
        private bool _isNear = MainForm.in_editor || !MainForm.thread_test;//true;

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
                    flag = true;
                }
                if (!flag)
                {
                    try
                    {
                        _mesh = Mesh.FromFile(MyDirect3D.device, filename, MeshFlags.SystemMemory);
                        ExtendedMaterial[] materialArray = _mesh.GetMaterials();
                        _meshMaterials = new Material[materialArray.Length];
                        _meshTextures = new Texture[materialArray.Length];
                        _meshTextureFilenames = new string[materialArray.Length];
                        for (var i = 0; i < materialArray.Length; i++)
                        {
                        	_meshMaterials[i] = materialArray[i].MaterialD3D;
                        	if (string.IsNullOrEmpty(materialArray[i].TextureFileName))
                        	{
                        		continue;
                        	}
                            _meshTextureFilenames[i] = meshDir + materialArray[i].TextureFileName;
                            LoadTexture(i, _meshTextureFilenames[i]);
                        }
                        _mesh.OptimizeInPlace(MeshOptimizeFlags.AttributeSort | MeshOptimizeFlags.VertexCache | MeshOptimizeFlags.Compact);
                        var struct3 = new MeshFileStruct
                        {
                            filename = filename,
                            mesh = _mesh,
                            materials = (Material[])_meshMaterials.Clone(),
                            textures = (Texture[])_meshTextures.Clone(),
                            textureFilenames = (string[])_meshTextureFilenames.Clone(),
                        };
                        _meshFileStructs.Add(struct3);
                    }
                    catch (SlimDXException e)
                    {
                        Logger.LogException(e, "Couldn't load model: " + filename);
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
                Logger.LogException(new FileNotFoundException("Texture file not found!", filename));
                return;
            }
            for (int i = 0; i < _textureFileStructs.Count; i++)
            {
				if (!string.Equals(filename, _textureFileStructs[i].filename))
					continue;
            	_meshTextures[index] = _textureFileStructs[i].texture;
            	return;
            }
            _meshTextures[index] = Texture.FromFile(MyDirect3D.device, filename, Usage.None, Pool.Default);
            _meshTextures[index].GenerateMipSublevels();
            var struct3 = new TextureFileStruct { filename = filename, texture = _meshTextures[index] };
            _textureFileStructs.Add(struct3);
        }
        
#if TEXCL
        protected void LoadTexture2(int index, string filename)
        {
        	foreach (var str in extraMeshDirs)
            {
                if (!File.Exists(str + filename)) continue;
                filename = str + filename;
                break;
            }
            if (!File.Exists(filename))
            {
                Logger.LogException(new FileNotFoundException("Texture file not found!", filename));
                // TODO: перелопатить
                _meshTextures2[index] = new TextureFileClass { filename = filename, memory = null, texture = null };
                return;
            }
            for (int i = 0; i < _textureFileClasses.Count; i++)
            {
            	if (!string.Equals(_textureFileClasses[i].filename, filename)) continue;
            	_meshTextures2[index] = _textureFileClasses[i];
            	if ((_isNear) && (_meshTextures2[index].disposed)) _meshTextures2[index].StartAsyncLoad();
            	return;
            }
        	_meshTextures2[index] = new TextureFileClass { filename = filename, memory = null, texture = null };
        	if (_isNear) _meshTextures2[index].StartAsyncLoad();
        	_textureFileClasses.Add(_meshTextures2[index]);
        }
        
        /// <summary>
        /// Не будет поддерживаться
        /// </summary>
        /// <param name="index">Индекс существующей(!) текстуры в меше</param>
        /// <param name="stream">Поток</param>
        protected void LoadTextureFromStream(int index, Stream stream)
        {
        	var _texture = Texture.FromStream(MyDirect3D.device, stream);
        	_texture.GenerateMipSublevels();
        	_meshTextures2[index] = new TextureFileClass { filename = _meshTextures2[index].filename, memory = _meshTextures2[index].memory, texture = _texture };
        	_textureFileClasses.Add(_meshTextures2[index]);
        }
#endif
        
        protected void LoadTextureFromStream(int index, Stream stream)
        {
        	Texture _texture = null;
        	try
        	{
        		_texture = Texture.FromStream(MyDirect3D.device, stream);
        	}
        	catch (Exception exc)
        	{
        		Logger.LogException(exc, "LoadTextureFromStream");
        		return;
        	}
        	_texture.GenerateMipSublevels();
        	_meshTextures[index] = _texture;
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
                    if (matrix == MyMatrix.Zero)
                    	continue;
                    for (var k = 0; k < _meshMaterials.Length; k++)
                    {
                        if (_meshMaterials[k].Diffuse.Alpha < 1.0f)
                        	_renderListA.Add(new RenderStruct(_meshMaterials[k], _meshTextures[k], _mesh, k, matrix));
                        else
                        	_renderList.Add(new RenderStruct(_meshMaterials[k], _meshTextures[k], _mesh, k, matrix));
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
                MyDirect3D.device.Material = structArray.material;
                MyDirect3D.device.SetTexture(0, structArray.texture);
                MyDirect3D.device.SetTransform(TransformState.World, structArray.matrix);
                structArray.mesh.DrawSubset(structArray.subset);
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
            foreach (var structArray in _renderListA)
            {
                MyDirect3D.device.Material = structArray.material;
                MyDirect3D.device.SetTexture(0, structArray.texture);
                MyDirect3D.device.SetTransform(TransformState.World, structArray.matrix);
                structArray.mesh.DrawSubset(structArray.subset);
            }
            _renderListA.Clear();
        }
        
        public bool IsNear
        {
        	get
        	{
        		return _isNear;
        	}
        	set
        	{
        		_isNear = value;
        	}
        }
		
        public interface ICustomCreation : IMatrixObject
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
                return 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        protected struct TextureFileStruct
        {
            public string filename;
            public Texture texture;
        }
        
#if TEXCL
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
            
            public void StartAsyncLoad()
			{
            	if (onLoad)
            		return;
				try 
				{
					FileStream stream = File.OpenRead(filename);
					Byte[] myByteArray = new Byte[1024];
					stream.BeginRead(myByteArray, 0, myByteArray.Length, 
					ReadAsyncCallback, new MyAsyncInfo(myByteArray, stream));
					onLoad = true;
				}
				catch (Exception exc)
			  	{
			  		Logger.LogException(exc);
			  	}
			}
			
			private void ReadAsyncCallback(IAsyncResult ar)
			{
				MyAsyncInfo info = (MyAsyncInfo)ar.AsyncState;
				try
				{
					int amountRead = info.MyStream.EndRead(ar);
					//переделать
				  	for (int i = 0; i < amountRead; i++)
				  	{
				  		info.temp_bytes.Add(info.ByteArray[i]);
				  	}
				  	if (info.MyStream.Position < info.MyStream.Length)
				  	{
				  		info.MyStream.BeginRead(info.ByteArray, 0,
				    	info.ByteArray.Length, ReadAsyncCallback, info);
				  	}
				  	else
				  	{
				  		//apply changes
//				  		memory = info.temp_bytes.ToArray();
				  		texture = Texture.FromMemory(MyDirect3D.device, info.temp_bytes.ToArray()/*memory*/, Usage.None, Pool.Default);
				  		onLoad = false;
				  		disposed = false;
				  		needed = true;
				    	info.MyStream.Close();
				  	}
				}
			  	catch (Exception exc)
			  	{
			  		Logger.LogException(exc);
			  		info.MyStream.Close();
			  		return;
			  	}
			}
			
			private class MyAsyncInfo
			{
				public Byte[] ByteArray;
				public Stream MyStream;
				public List<byte> temp_bytes = new List<byte>();
				
				public MyAsyncInfo(Byte[] array, Stream stream)
				{
					ByteArray = array;
					MyStream = stream;
				}
			}
        }
#endif
        
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

