xof 0302txt 0032
Header {
 1;
 0;
 1;
}
template Header {
 <3D82AB43-62DA-11cf-AB39-0020AF71E433>
 WORD major;
 WORD minor;
 DWORD flags;
}

template Vector {
 <3D82AB5E-62DA-11cf-AB39-0020AF71E433>
 FLOAT x;
 FLOAT y;
 FLOAT z;
}

template Coords2d {
 <F6F23F44-7686-11cf-8F52-0040333594A3>
 FLOAT u;
 FLOAT v;
}

template Matrix4x4 {
 <F6F23F45-7686-11cf-8F52-0040333594A3>
 array FLOAT matrix[16];
}

template ColorRGBA {
 <35FF44E0-6C7C-11cf-8F52-0040333594A3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
 FLOAT alpha;
}

template ColorRGB {
 <D3E16E81-7835-11cf-8F52-0040333594A3>
 FLOAT red;
 FLOAT green;
 FLOAT blue;
}

template TextureFilename {
 <A42790E1-7810-11cf-8F52-0040333594A3>
 STRING filename;
}

template Material {
 <3D82AB4D-62DA-11cf-AB39-0020AF71E433>
 ColorRGBA faceColor;
 FLOAT power;
 ColorRGB specularColor;
 ColorRGB emissiveColor;
 [...]
}

template MeshFace {
 <3D82AB5F-62DA-11cf-AB39-0020AF71E433>
 DWORD nFaceVertexIndices;
 array DWORD faceVertexIndices[nFaceVertexIndices];
}

template MeshTextureCoords {
 <F6F23F40-7686-11cf-8F52-0040333594A3>
 DWORD nTextureCoords;
 array Coords2d textureCoords[nTextureCoords];
}

template MeshMaterialList {
 <F6F23F42-7686-11cf-8F52-0040333594A3>
 DWORD nMaterials;
 DWORD nFaceIndexes;
 array DWORD faceIndexes[nFaceIndexes];
 [Material]
}

template MeshNormals {
 <F6F23F43-7686-11cf-8F52-0040333594A3>
 DWORD nNormals;
 array Vector normals[nNormals];
 DWORD nFaceNormals;
 array MeshFace faceNormals[nFaceNormals];
}

template Mesh {
 <3D82AB44-62DA-11cf-AB39-0020AF71E433>
 DWORD nVertices;
 array Vector vertices[nVertices];
 DWORD nFaces;
 array MeshFace faces[nFaces];
 [...]
}

template FrameTransformMatrix {
 <F6F23F41-7686-11cf-8F52-0040333594A3>
 Matrix4x4 frameMatrix;
}

template Frame {
 <3D82AB46-62DA-11cf-AB39-0020AF71E433>
 [...]
}
template FloatKeys {
 <10DD46A9-775B-11cf-8F52-0040333594A3>
 DWORD nValues;
 array FLOAT values[nValues];
}

template TimedFloatKeys {
 <F406B180-7B3B-11cf-8F52-0040333594A3>
 DWORD time;
 FloatKeys tfkeys;
}

template AnimationKey {
 <10DD46A8-775B-11cf-8F52-0040333594A3>
 DWORD keyType;
 DWORD nKeys;
 array TimedFloatKeys keys[nKeys];
}

template AnimationOptions {
 <E2BF56C0-840F-11cf-8F52-0040333594A3>
 DWORD openclosed;
 DWORD positionquality;
}

template Animation {
 <3D82AB4F-62DA-11cf-AB39-0020AF71E433>
 [...]
}

template AnimationSet {
 <3D82AB50-62DA-11cf-AB39-0020AF71E433>
 [Animation]
}
Frame sw_CO_1 {
   FrameTransformMatrix {
0.000000,0.000000,-1.000000,0.000000,
1.000000,0.000000,0.000000,0.000000,
0.000000,1.000000,0.000000,0.000000,
0.000000,0.120000,0.000000,1.000000;;
 }
Mesh sw_CO_11 {
 40;
1.122028;-2.703927;0.951109;,
1.009427;-2.703928;0.870287;,
1.009427;-2.703927;0.951109;,
1.122028;-2.703928;0.870287;,
1.009427;-2.703927;0.951109;,
1.009427;-2.703928;0.870287;,
1.004845;-2.682616;0.958143;,
1.003851;-2.683759;0.863791;,
1.009427;-2.703928;0.870287;,
1.122028;-2.703928;0.870287;,
1.003851;-2.683759;0.863791;,
1.127107;-2.683002;0.864270;,
1.122028;-2.703928;0.870287;,
1.122028;-2.703927;0.951109;,
1.127107;-2.683002;0.864270;,
1.127605;-2.683000;0.957760;,
1.122028;-2.703927;0.951109;,
1.009427;-2.703927;0.951109;,
1.127605;-2.683000;0.957760;,
1.004845;-2.682616;0.958143;,
-1.007950;-2.703927;0.951109;,
-1.007950;-2.703928;0.870287;,
-1.120551;-2.703927;0.951109;,
-1.120551;-2.703928;0.870287;,
-1.003368;-2.682616;0.958143;,
-1.007950;-2.703928;0.870287;,
-1.007950;-2.703927;0.951109;,
-1.002374;-2.683759;0.863791;,
-1.002374;-2.683759;0.863791;,
-1.120551;-2.703928;0.870287;,
-1.007950;-2.703928;0.870287;,
-1.125630;-2.683002;0.864270;,
-1.125630;-2.683002;0.864270;,
-1.120551;-2.703927;0.951109;,
-1.120551;-2.703928;0.870287;,
-1.126127;-2.683000;0.957760;,
-1.126127;-2.683000;0.957760;,
-1.007950;-2.703927;0.951109;,
-1.120551;-2.703927;0.951109;,
-1.003368;-2.682616;0.958143;;

 20;
3;0,1,2;,
3;0,3,1;,
3;4,5,6;,
3;5,7,6;,
3;8,9,10;,
3;9,11,10;,
3;12,13,14;,
3;13,15,14;,
3;16,17,18;,
3;17,19,18;,
3;20,21,22;,
3;21,23,22;,
3;24,25,26;,
3;24,27,25;,
3;28,29,30;,
3;28,31,29;,
3;32,33,34;,
3;32,35,33;,
3;36,37,38;,
3;36,39,37;;
MeshMaterialList {
 1;
 20;
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0,
  0;;
Material {
 1.000000;1.000000;1.000000;1.000000;;
2048.000000;
 1.000000;1.000000;1.000000;;
 0.200000;0.200000;0.200000;;
TextureFilename {
"129.bmp";
}
 }
}

 MeshNormals {
 40;
0.000000;1.000000;-0.000009;,
0.000000;1.000000;-0.000009;,
0.000000;1.000000;-0.000009;,
0.000000;1.000000;-0.000009;,
0.977665;0.210171;-0.000002;,
0.971635;0.236390;-0.006676;,
0.971635;0.236390;-0.006676;,
0.964859;0.262429;-0.013346;,
0.000000;0.306564;0.951850;,
-0.002721;0.292084;0.956389;,
-0.002721;0.292084;0.956389;,
-0.005441;0.277534;0.960700;,
-0.971788;0.235855;-0.000002;,
-0.969285;0.245926;0.002571;,
-0.969285;0.245926;0.002571;,
-0.966671;0.255969;0.005144;,
0.000000;0.302900;-0.953022;,
-0.000993;0.307988;-0.951390;,
-0.000993;0.307988;-0.951390;,
-0.001986;0.313066;-0.949729;,
0.000000;1.000000;-0.000009;,
0.000000;1.000000;-0.000009;,
0.000000;1.000000;-0.000009;,
0.000000;1.000000;-0.000009;,
-0.971635;0.236391;-0.006675;,
-0.971635;0.236391;-0.006675;,
-0.977664;0.210176;-0.000002;,
-0.964860;0.262424;-0.013344;,
0.002721;0.292084;0.956389;,
0.002721;0.292084;0.956389;,
0.000000;0.306564;0.951850;,
0.005441;0.277534;0.960700;,
0.969285;0.245927;0.002567;,
0.969285;0.245927;0.002567;,
0.971784;0.235871;-0.000002;,
0.966675;0.255956;0.005137;,
0.000993;0.307988;-0.951390;,
0.000993;0.307988;-0.951390;,
0.000000;0.302900;-0.953022;,
0.001986;0.313066;-0.949729;;

 20;
3;0,1,2;,
3;0,3,1;,
3;4,5,6;,
3;5,7,6;,
3;8,9,10;,
3;9,11,10;,
3;12,13,14;,
3;13,15,14;,
3;16,17,18;,
3;17,19,18;,
3;20,21,22;,
3;21,23,22;,
3;24,25,26;,
3;24,27,25;,
3;28,29,30;,
3;28,31,29;,
3;32,33,34;,
3;32,35,33;,
3;36,37,38;,
3;36,39,37;;
 }
MeshTextureCoords {
 40;
0.969586;0.440799;,
0.991481;0.462137;,
0.991481;0.440799;,
0.969586;0.462137;,
0.991481;0.440799;,
0.991481;0.462137;,
0.992371;0.438939;,
0.992563;0.463854;,
0.991481;0.462137;,
0.969586;0.462137;,
0.992563;0.463854;,
0.968600;0.463724;,
0.969586;0.462137;,
0.969586;0.440799;,
0.968600;0.463724;,
0.968506;0.439042;,
0.969586;0.440799;,
0.991481;0.440799;,
0.968506;0.439042;,
0.992371;0.438939;,
0.969407;0.440799;,
0.969407;0.462137;,
0.991299;0.440799;,
0.991299;0.462137;,
0.968515;0.438939;,
0.969407;0.462137;,
0.969407;0.440799;,
0.968321;0.463854;,
0.968321;0.463854;,
0.991299;0.462137;,
0.969407;0.462137;,
0.992287;0.463724;,
0.992287;0.463724;,
0.991299;0.440799;,
0.991299;0.462137;,
0.992387;0.439042;,
0.992387;0.439042;,
0.969407;0.440799;,
0.991299;0.440799;,
0.968515;0.438939;;
}
}
 }
