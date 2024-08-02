using System;
using Common;
using SlimDX;
using SlimDX.Direct3D9;

namespace Trancity
{
    public class Ground : MeshObject, MeshObject.IFromFile, IMatrixObject
    {
    	public static int grid_step = 21;
    	public static int grid_size = 300;
    	
//    	private GroundPart[] parts;
    	
        public Matrix GetMatrix(int index)
        {
            var point = MyDirect3D.Camera_Position.xz_point;
            var point2 = new DoublePoint((Math.Floor(point.x / 500.0) * 500.0) + ((index % 2) * 500.0), (Math.Floor(point.y / 500.0) * 500.0) + ((index / 2) * 500.0));
            return (Matrix.Scaling(0.5f, 1f, 0.5f) * Matrix.Translation((float) point2.x, -0.1f, (float) point2.y));
        }

        public string Filename
        {
            get
            {
                return "ground.x";
            }
        }

        public int MatricesCount
        {
            get
            {
                return !MyDirect3D.карта ? 4 : 0;
            }
        }/**/
        
        public double GetHeight(DoublePoint pos)
        {
        	//TODO: нахождение высоты
        	/*var points = MyFeatures.GetPos(ref pos);
        	if (points[0] == 0)
        	{
        		if (points[1] == 0)
	        	{
        			return parts[4].GetHeight(pos);
	        	}
	        	else if (points[1] == -1)
	        	{
	        		return parts[7].GetHeight(pos);
	        	}
	        	else if (points[1] == 1)
	        	{
	        		return parts[1].GetHeight(pos);
	        	}
	        	return -0.1;
        	}
        	else if (points[0] == -1)
        	{
        		if (points[1] == 0)
	        	{
	        		return parts[5].GetHeight(pos);
	        	}
	        	else if (points[1] == -1)
	        	{
	        		return parts[8].GetHeight(pos);
	        	}
	        	else if (points[1] == 1)
	        	{
	        		return parts[2].GetHeight(pos);
	        	}
	        	return -0.1;
        	}
        	else if (points[0] == 1)
        	{
        		if (points[1] == 0)
	        	{
	        		return parts[3].GetHeight(pos);
	        	}
	        	else if (points[1] == -1)
	        	{
	        		return parts[6].GetHeight(pos);
	        	}
	        	else if (points[1] == 1)
	        	{
	        		return parts[0].GetHeight(pos);
	        	}
	        	return -0.1;
        	}/**/
        	return -0.1;
        }
        
        /*public void CreateMesh()
        {
        	parts = new GroundPart[9];
        	parts[0] = new GroundPart(1, 1);
        	parts[1] = new GroundPart(0, 1);
        	parts[2] = new GroundPart(-1, 1);
        	parts[3] = new GroundPart(1, 0);
        	parts[4] = new GroundPart(0, 0);
        	parts[5] = new GroundPart(-1, 0);
        	parts[6] = new GroundPart(1, -1);
        	parts[7] = new GroundPart(0, -1);
        	parts[8] = new GroundPart(-1, -1);
        	parts[0].CreateMesh();
        	parts[1].CreateMesh();
        	parts[2].CreateMesh();
        	parts[3].CreateMesh();
        	parts[4].CreateMesh();
        	parts[5].CreateMesh();
        	parts[6].CreateMesh();
        	parts[7].CreateMesh();
        	parts[8].CreateMesh();
        }
        
        public void Render()
        {
        	parts[0].Render();
        	parts[1].Render();
        	parts[2].Render();
        	parts[3].Render();
        	parts[4].Render();
        	parts[5].Render();
        	parts[6].Render();
        	parts[7].Render();
        	parts[8].Render();
        }/**/
    }
}