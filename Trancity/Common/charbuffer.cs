/*namespace Common
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), DebuggerNonUserCode]
    public struct charbuffer
    {
        public const int size = 0x100;
        public char char000;
        public char char001;
        public char char002;
        public char char003;
        public char char004;
        public char char005;
        public char char006;
        public char char007;
        public char char008;
        public char char009;
        public char char010;
        public char char011;
        public char char012;
        public char char013;
        public char char014;
        public char char015;
        public char char016;
        public char char017;
        public char char018;
        public char char019;
        public char char020;
        public char char021;
        public char char022;
        public char char023;
        public char char024;
        public char char025;
        public char char026;
        public char char027;
        public char char028;
        public char char029;
        public char char030;
        public char char031;
        public char char032;
        public char char033;
        public char char034;
        public char char035;
        public char char036;
        public char char037;
        public char char038;
        public char char039;
        public char char040;
        public char char041;
        public char char042;
        public char char043;
        public char char044;
        public char char045;
        public char char046;
        public char char047;
        public char char048;
        public char char049;
        public char char050;
        public char char051;
        public char char052;
        public char char053;
        public char char054;
        public char char055;
        public char char056;
        public char char057;
        public char char058;
        public char char059;
        public char char060;
        public char char061;
        public char char062;
        public char char063;
        public char char064;
        public char char065;
        public char char066;
        public char char067;
        public char char068;
        public char char069;
        public char char070;
        public char char071;
        public char char072;
        public char char073;
        public char char074;
        public char char075;
        public char char076;
        public char char077;
        public char char078;
        public char char079;
        public char char080;
        public char char081;
        public char char082;
        public char char083;
        public char char084;
        public char char085;
        public char char086;
        public char char087;
        public char char088;
        public char char089;
        public char char090;
        public char char091;
        public char char092;
        public char char093;
        public char char094;
        public char char095;
        public char char096;
        public char char097;
        public char char098;
        public char char099;
        public char char100;
        public char char101;
        public char char102;
        public char char103;
        public char char104;
        public char char105;
        public char char106;
        public char char107;
        public char char108;
        public char char109;
        public char char110;
        public char char111;
        public char char112;
        public char char113;
        public char char114;
        public char char115;
        public char char116;
        public char char117;
        public char char118;
        public char char119;
        public char char120;
        public char char121;
        public char char122;
        public char char123;
        public char char124;
        public char char125;
        public char char126;
        public char char127;
        public char char128;
        public char char129;
        public char char130;
        public char char131;
        public char char132;
        public char char133;
        public char char134;
        public char char135;
        public char char136;
        public char char137;
        public char char138;
        public char char139;
        public char char140;
        public char char141;
        public char char142;
        public char char143;
        public char char144;
        public char char145;
        public char char146;
        public char char147;
        public char char148;
        public char char149;
        public char char150;
        public char char151;
        public char char152;
        public char char153;
        public char char154;
        public char char155;
        public char char156;
        public char char157;
        public char char158;
        public char char159;
        public char char160;
        public char char161;
        public char char162;
        public char char163;
        public char char164;
        public char char165;
        public char char166;
        public char char167;
        public char char168;
        public char char169;
        public char char170;
        public char char171;
        public char char172;
        public char char173;
        public char char174;
        public char char175;
        public char char176;
        public char char177;
        public char char178;
        public char char179;
        public char char180;
        public char char181;
        public char char182;
        public char char183;
        public char char184;
        public char char185;
        public char char186;
        public char char187;
        public char char188;
        public char char189;
        public char char190;
        public char char191;
        public char char192;
        public char char193;
        public char char194;
        public char char195;
        public char char196;
        public char char197;
        public char char198;
        public char char199;
        public char char200;
        public char char201;
        public char char202;
        public char char203;
        public char char204;
        public char char205;
        public char char206;
        public char char207;
        public char char208;
        public char char209;
        public char char210;
        public char char211;
        public char char212;
        public char char213;
        public char char214;
        public char char215;
        public char char216;
        public char char217;
        public char char218;
        public char char219;
        public char char220;
        public char char221;
        public char char222;
        public char char223;
        public char char224;
        public char char225;
        public char char226;
        public char char227;
        public char char228;
        public char char229;
        public char char230;
        public char char231;
        public char char232;
        public char char233;
        public char char234;
        public char char235;
        public char char236;
        public char char237;
        public char char238;
        public char char239;
        public char char240;
        public char char241;
        public char char242;
        public char char243;
        public char char244;
        public char char245;
        public char char246;
        public char char247;
        public char char248;
        public char char249;
        public char char250;
        public char char251;
        public char char252;
        public char char253;
        public char char254;
        public char char255;
        public char this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.char000;

                    case 1:
                        return this.char001;

                    case 2:
                        return this.char002;

                    case 3:
                        return this.char003;

                    case 4:
                        return this.char004;

                    case 5:
                        return this.char005;

                    case 6:
                        return this.char006;

                    case 7:
                        return this.char007;

                    case 8:
                        return this.char008;

                    case 9:
                        return this.char009;

                    case 10:
                        return this.char010;

                    case 11:
                        return this.char011;

                    case 12:
                        return this.char012;

                    case 13:
                        return this.char013;

                    case 14:
                        return this.char014;

                    case 15:
                        return this.char015;

                    case 0x10:
                        return this.char016;

                    case 0x11:
                        return this.char017;

                    case 0x12:
                        return this.char018;

                    case 0x13:
                        return this.char019;

                    case 20:
                        return this.char020;

                    case 0x15:
                        return this.char021;

                    case 0x16:
                        return this.char022;

                    case 0x17:
                        return this.char023;

                    case 0x18:
                        return this.char024;

                    case 0x19:
                        return this.char025;

                    case 0x1a:
                        return this.char026;

                    case 0x1b:
                        return this.char027;

                    case 0x1c:
                        return this.char028;

                    case 0x1d:
                        return this.char029;

                    case 30:
                        return this.char030;

                    case 0x1f:
                        return this.char031;

                    case 0x20:
                        return this.char032;

                    case 0x21:
                        return this.char033;

                    case 0x22:
                        return this.char034;

                    case 0x23:
                        return this.char035;

                    case 0x24:
                        return this.char036;

                    case 0x25:
                        return this.char037;

                    case 0x26:
                        return this.char038;

                    case 0x27:
                        return this.char039;

                    case 40:
                        return this.char040;

                    case 0x29:
                        return this.char041;

                    case 0x2a:
                        return this.char042;

                    case 0x2b:
                        return this.char043;

                    case 0x2c:
                        return this.char044;

                    case 0x2d:
                        return this.char045;

                    case 0x2e:
                        return this.char046;

                    case 0x2f:
                        return this.char047;

                    case 0x30:
                        return this.char048;

                    case 0x31:
                        return this.char049;

                    case 50:
                        return this.char050;

                    case 0x33:
                        return this.char051;

                    case 0x34:
                        return this.char052;

                    case 0x35:
                        return this.char053;

                    case 0x36:
                        return this.char054;

                    case 0x37:
                        return this.char055;

                    case 0x38:
                        return this.char056;

                    case 0x39:
                        return this.char057;

                    case 0x3a:
                        return this.char058;

                    case 0x3b:
                        return this.char059;

                    case 60:
                        return this.char060;

                    case 0x3d:
                        return this.char061;

                    case 0x3e:
                        return this.char062;

                    case 0x3f:
                        return this.char063;

                    case 0x40:
                        return this.char064;

                    case 0x41:
                        return this.char065;

                    case 0x42:
                        return this.char066;

                    case 0x43:
                        return this.char067;

                    case 0x44:
                        return this.char068;

                    case 0x45:
                        return this.char069;

                    case 70:
                        return this.char070;

                    case 0x47:
                        return this.char071;

                    case 0x48:
                        return this.char072;

                    case 0x49:
                        return this.char073;

                    case 0x4a:
                        return this.char074;

                    case 0x4b:
                        return this.char075;

                    case 0x4c:
                        return this.char076;

                    case 0x4d:
                        return this.char077;

                    case 0x4e:
                        return this.char078;

                    case 0x4f:
                        return this.char079;

                    case 80:
                        return this.char080;

                    case 0x51:
                        return this.char081;

                    case 0x52:
                        return this.char082;

                    case 0x53:
                        return this.char083;

                    case 0x54:
                        return this.char084;

                    case 0x55:
                        return this.char085;

                    case 0x56:
                        return this.char086;

                    case 0x57:
                        return this.char087;

                    case 0x58:
                        return this.char088;

                    case 0x59:
                        return this.char089;

                    case 90:
                        return this.char090;

                    case 0x5b:
                        return this.char091;

                    case 0x5c:
                        return this.char092;

                    case 0x5d:
                        return this.char093;

                    case 0x5e:
                        return this.char094;

                    case 0x5f:
                        return this.char095;

                    case 0x60:
                        return this.char096;

                    case 0x61:
                        return this.char097;

                    case 0x62:
                        return this.char098;

                    case 0x63:
                        return this.char099;

                    case 100:
                        return this.char100;

                    case 0x65:
                        return this.char101;

                    case 0x66:
                        return this.char102;

                    case 0x67:
                        return this.char103;

                    case 0x68:
                        return this.char104;

                    case 0x69:
                        return this.char105;

                    case 0x6a:
                        return this.char106;

                    case 0x6b:
                        return this.char107;

                    case 0x6c:
                        return this.char108;

                    case 0x6d:
                        return this.char109;

                    case 110:
                        return this.char110;

                    case 0x6f:
                        return this.char111;

                    case 0x70:
                        return this.char112;

                    case 0x71:
                        return this.char113;

                    case 0x72:
                        return this.char114;

                    case 0x73:
                        return this.char115;

                    case 0x74:
                        return this.char116;

                    case 0x75:
                        return this.char117;

                    case 0x76:
                        return this.char118;

                    case 0x77:
                        return this.char119;

                    case 120:
                        return this.char120;

                    case 0x79:
                        return this.char121;

                    case 0x7a:
                        return this.char122;

                    case 0x7b:
                        return this.char123;

                    case 0x7c:
                        return this.char124;

                    case 0x7d:
                        return this.char125;

                    case 0x7e:
                        return this.char126;

                    case 0x7f:
                        return this.char127;

                    case 0x80:
                        return this.char128;

                    case 0x81:
                        return this.char129;

                    case 130:
                        return this.char130;

                    case 0x83:
                        return this.char131;

                    case 0x84:
                        return this.char132;

                    case 0x85:
                        return this.char133;

                    case 0x86:
                        return this.char134;

                    case 0x87:
                        return this.char135;

                    case 0x88:
                        return this.char136;

                    case 0x89:
                        return this.char137;

                    case 0x8a:
                        return this.char138;

                    case 0x8b:
                        return this.char139;

                    case 140:
                        return this.char140;

                    case 0x8d:
                        return this.char141;

                    case 0x8e:
                        return this.char142;

                    case 0x8f:
                        return this.char143;

                    case 0x90:
                        return this.char144;

                    case 0x91:
                        return this.char145;

                    case 0x92:
                        return this.char146;

                    case 0x93:
                        return this.char147;

                    case 0x94:
                        return this.char148;

                    case 0x95:
                        return this.char149;

                    case 150:
                        return this.char150;

                    case 0x97:
                        return this.char151;

                    case 0x98:
                        return this.char152;

                    case 0x99:
                        return this.char153;

                    case 0x9a:
                        return this.char154;

                    case 0x9b:
                        return this.char155;

                    case 0x9c:
                        return this.char156;

                    case 0x9d:
                        return this.char157;

                    case 0x9e:
                        return this.char158;

                    case 0x9f:
                        return this.char159;

                    case 160:
                        return this.char160;

                    case 0xa1:
                        return this.char161;

                    case 0xa2:
                        return this.char162;

                    case 0xa3:
                        return this.char163;

                    case 0xa4:
                        return this.char164;

                    case 0xa5:
                        return this.char165;

                    case 0xa6:
                        return this.char166;

                    case 0xa7:
                        return this.char167;

                    case 0xa8:
                        return this.char168;

                    case 0xa9:
                        return this.char169;

                    case 170:
                        return this.char170;

                    case 0xab:
                        return this.char171;

                    case 0xac:
                        return this.char172;

                    case 0xad:
                        return this.char173;

                    case 0xae:
                        return this.char174;

                    case 0xaf:
                        return this.char175;

                    case 0xb0:
                        return this.char176;

                    case 0xb1:
                        return this.char177;

                    case 0xb2:
                        return this.char178;

                    case 0xb3:
                        return this.char179;

                    case 180:
                        return this.char180;

                    case 0xb5:
                        return this.char181;

                    case 0xb6:
                        return this.char182;

                    case 0xb7:
                        return this.char183;

                    case 0xb8:
                        return this.char184;

                    case 0xb9:
                        return this.char185;

                    case 0xba:
                        return this.char186;

                    case 0xbb:
                        return this.char187;

                    case 0xbc:
                        return this.char188;

                    case 0xbd:
                        return this.char189;

                    case 190:
                        return this.char190;

                    case 0xbf:
                        return this.char191;

                    case 0xc0:
                        return this.char192;

                    case 0xc1:
                        return this.char193;

                    case 0xc2:
                        return this.char194;

                    case 0xc3:
                        return this.char195;

                    case 0xc4:
                        return this.char196;

                    case 0xc5:
                        return this.char197;

                    case 0xc6:
                        return this.char198;

                    case 0xc7:
                        return this.char199;

                    case 200:
                        return this.char200;

                    case 0xc9:
                        return this.char201;

                    case 0xca:
                        return this.char202;

                    case 0xcb:
                        return this.char203;

                    case 0xcc:
                        return this.char204;

                    case 0xcd:
                        return this.char205;

                    case 0xce:
                        return this.char206;

                    case 0xcf:
                        return this.char207;

                    case 0xd0:
                        return this.char208;

                    case 0xd1:
                        return this.char209;

                    case 210:
                        return this.char210;

                    case 0xd3:
                        return this.char211;

                    case 0xd4:
                        return this.char212;

                    case 0xd5:
                        return this.char213;

                    case 0xd6:
                        return this.char214;

                    case 0xd7:
                        return this.char215;

                    case 0xd8:
                        return this.char216;

                    case 0xd9:
                        return this.char217;

                    case 0xda:
                        return this.char218;

                    case 0xdb:
                        return this.char219;

                    case 220:
                        return this.char220;

                    case 0xdd:
                        return this.char221;

                    case 0xde:
                        return this.char222;

                    case 0xdf:
                        return this.char223;

                    case 0xe0:
                        return this.char224;

                    case 0xe1:
                        return this.char225;

                    case 0xe2:
                        return this.char226;

                    case 0xe3:
                        return this.char227;

                    case 0xe4:
                        return this.char228;

                    case 0xe5:
                        return this.char229;

                    case 230:
                        return this.char230;

                    case 0xe7:
                        return this.char231;

                    case 0xe8:
                        return this.char232;

                    case 0xe9:
                        return this.char233;

                    case 0xea:
                        return this.char234;

                    case 0xeb:
                        return this.char235;

                    case 0xec:
                        return this.char236;

                    case 0xed:
                        return this.char237;

                    case 0xee:
                        return this.char238;

                    case 0xef:
                        return this.char239;

                    case 240:
                        return this.char240;

                    case 0xf1:
                        return this.char241;

                    case 0xf2:
                        return this.char242;

                    case 0xf3:
                        return this.char243;

                    case 0xf4:
                        return this.char244;

                    case 0xf5:
                        return this.char245;

                    case 0xf6:
                        return this.char246;

                    case 0xf7:
                        return this.char247;

                    case 0xf8:
                        return this.char248;

                    case 0xf9:
                        return this.char249;

                    case 250:
                        return this.char250;

                    case 0xfb:
                        return this.char251;

                    case 0xfc:
                        return this.char252;

                    case 0xfd:
                        return this.char253;

                    case 0xfe:
                        return this.char254;

                    case 0xff:
                        return this.char255;
                }
                return '\0';
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.char000 = value;
                        return;

                    case 1:
                        this.char001 = value;
                        return;

                    case 2:
                        this.char002 = value;
                        return;

                    case 3:
                        this.char003 = value;
                        return;

                    case 4:
                        this.char004 = value;
                        return;

                    case 5:
                        this.char005 = value;
                        return;

                    case 6:
                        this.char006 = value;
                        return;

                    case 7:
                        this.char007 = value;
                        return;

                    case 8:
                        this.char008 = value;
                        return;

                    case 9:
                        this.char009 = value;
                        return;

                    case 10:
                        this.char010 = value;
                        return;

                    case 11:
                        this.char011 = value;
                        return;

                    case 12:
                        this.char012 = value;
                        return;

                    case 13:
                        this.char013 = value;
                        return;

                    case 14:
                        this.char014 = value;
                        return;

                    case 15:
                        this.char015 = value;
                        return;

                    case 0x10:
                        this.char016 = value;
                        return;

                    case 0x11:
                        this.char017 = value;
                        return;

                    case 0x12:
                        this.char018 = value;
                        return;

                    case 0x13:
                        this.char019 = value;
                        return;

                    case 20:
                        this.char020 = value;
                        return;

                    case 0x15:
                        this.char021 = value;
                        return;

                    case 0x16:
                        this.char022 = value;
                        return;

                    case 0x17:
                        this.char023 = value;
                        return;

                    case 0x18:
                        this.char024 = value;
                        return;

                    case 0x19:
                        this.char025 = value;
                        return;

                    case 0x1a:
                        this.char026 = value;
                        return;

                    case 0x1b:
                        this.char027 = value;
                        return;

                    case 0x1c:
                        this.char028 = value;
                        return;

                    case 0x1d:
                        this.char029 = value;
                        return;

                    case 30:
                        this.char030 = value;
                        return;

                    case 0x1f:
                        this.char031 = value;
                        return;

                    case 0x20:
                        this.char032 = value;
                        return;

                    case 0x21:
                        this.char033 = value;
                        return;

                    case 0x22:
                        this.char034 = value;
                        return;

                    case 0x23:
                        this.char035 = value;
                        return;

                    case 0x24:
                        this.char036 = value;
                        return;

                    case 0x25:
                        this.char037 = value;
                        return;

                    case 0x26:
                        this.char038 = value;
                        return;

                    case 0x27:
                        this.char039 = value;
                        return;

                    case 40:
                        this.char040 = value;
                        return;

                    case 0x29:
                        this.char041 = value;
                        return;

                    case 0x2a:
                        this.char042 = value;
                        return;

                    case 0x2b:
                        this.char043 = value;
                        return;

                    case 0x2c:
                        this.char044 = value;
                        return;

                    case 0x2d:
                        this.char045 = value;
                        return;

                    case 0x2e:
                        this.char046 = value;
                        return;

                    case 0x2f:
                        this.char047 = value;
                        return;

                    case 0x30:
                        this.char048 = value;
                        return;

                    case 0x31:
                        this.char049 = value;
                        return;

                    case 50:
                        this.char050 = value;
                        return;

                    case 0x33:
                        this.char051 = value;
                        return;

                    case 0x34:
                        this.char052 = value;
                        return;

                    case 0x35:
                        this.char053 = value;
                        return;

                    case 0x36:
                        this.char054 = value;
                        return;

                    case 0x37:
                        this.char055 = value;
                        return;

                    case 0x38:
                        this.char056 = value;
                        return;

                    case 0x39:
                        this.char057 = value;
                        return;

                    case 0x3a:
                        this.char058 = value;
                        return;

                    case 0x3b:
                        this.char059 = value;
                        return;

                    case 60:
                        this.char060 = value;
                        return;

                    case 0x3d:
                        this.char061 = value;
                        return;

                    case 0x3e:
                        this.char062 = value;
                        return;

                    case 0x3f:
                        this.char063 = value;
                        return;

                    case 0x40:
                        this.char064 = value;
                        return;

                    case 0x41:
                        this.char065 = value;
                        return;

                    case 0x42:
                        this.char066 = value;
                        return;

                    case 0x43:
                        this.char067 = value;
                        return;

                    case 0x44:
                        this.char068 = value;
                        return;

                    case 0x45:
                        this.char069 = value;
                        return;

                    case 70:
                        this.char070 = value;
                        return;

                    case 0x47:
                        this.char071 = value;
                        return;

                    case 0x48:
                        this.char072 = value;
                        return;

                    case 0x49:
                        this.char073 = value;
                        return;

                    case 0x4a:
                        this.char074 = value;
                        return;

                    case 0x4b:
                        this.char075 = value;
                        return;

                    case 0x4c:
                        this.char076 = value;
                        return;

                    case 0x4d:
                        this.char077 = value;
                        return;

                    case 0x4e:
                        this.char078 = value;
                        return;

                    case 0x4f:
                        this.char079 = value;
                        return;

                    case 80:
                        this.char080 = value;
                        return;

                    case 0x51:
                        this.char081 = value;
                        return;

                    case 0x52:
                        this.char082 = value;
                        return;

                    case 0x53:
                        this.char083 = value;
                        return;

                    case 0x54:
                        this.char084 = value;
                        return;

                    case 0x55:
                        this.char085 = value;
                        return;

                    case 0x56:
                        this.char086 = value;
                        return;

                    case 0x57:
                        this.char087 = value;
                        return;

                    case 0x58:
                        this.char088 = value;
                        return;

                    case 0x59:
                        this.char089 = value;
                        return;

                    case 90:
                        this.char090 = value;
                        return;

                    case 0x5b:
                        this.char091 = value;
                        return;

                    case 0x5c:
                        this.char092 = value;
                        return;

                    case 0x5d:
                        this.char093 = value;
                        return;

                    case 0x5e:
                        this.char094 = value;
                        return;

                    case 0x5f:
                        this.char095 = value;
                        return;

                    case 0x60:
                        this.char096 = value;
                        return;

                    case 0x61:
                        this.char097 = value;
                        return;

                    case 0x62:
                        this.char098 = value;
                        return;

                    case 0x63:
                        this.char099 = value;
                        return;

                    case 100:
                        this.char100 = value;
                        return;

                    case 0x65:
                        this.char101 = value;
                        return;

                    case 0x66:
                        this.char102 = value;
                        return;

                    case 0x67:
                        this.char103 = value;
                        return;

                    case 0x68:
                        this.char104 = value;
                        return;

                    case 0x69:
                        this.char105 = value;
                        return;

                    case 0x6a:
                        this.char106 = value;
                        return;

                    case 0x6b:
                        this.char107 = value;
                        return;

                    case 0x6c:
                        this.char108 = value;
                        return;

                    case 0x6d:
                        this.char109 = value;
                        return;

                    case 110:
                        this.char110 = value;
                        return;

                    case 0x6f:
                        this.char111 = value;
                        return;

                    case 0x70:
                        this.char112 = value;
                        return;

                    case 0x71:
                        this.char113 = value;
                        return;

                    case 0x72:
                        this.char114 = value;
                        return;

                    case 0x73:
                        this.char115 = value;
                        return;

                    case 0x74:
                        this.char116 = value;
                        return;

                    case 0x75:
                        this.char117 = value;
                        return;

                    case 0x76:
                        this.char118 = value;
                        return;

                    case 0x77:
                        this.char119 = value;
                        return;

                    case 120:
                        this.char120 = value;
                        return;

                    case 0x79:
                        this.char121 = value;
                        return;

                    case 0x7a:
                        this.char122 = value;
                        return;

                    case 0x7b:
                        this.char123 = value;
                        return;

                    case 0x7c:
                        this.char124 = value;
                        return;

                    case 0x7d:
                        this.char125 = value;
                        return;

                    case 0x7e:
                        this.char126 = value;
                        return;

                    case 0x7f:
                        this.char127 = value;
                        return;

                    case 0x80:
                        this.char128 = value;
                        return;

                    case 0x81:
                        this.char129 = value;
                        return;

                    case 130:
                        this.char130 = value;
                        return;

                    case 0x83:
                        this.char131 = value;
                        return;

                    case 0x84:
                        this.char132 = value;
                        return;

                    case 0x85:
                        this.char133 = value;
                        return;

                    case 0x86:
                        this.char134 = value;
                        return;

                    case 0x87:
                        this.char135 = value;
                        return;

                    case 0x88:
                        this.char136 = value;
                        return;

                    case 0x89:
                        this.char137 = value;
                        return;

                    case 0x8a:
                        this.char138 = value;
                        return;

                    case 0x8b:
                        this.char139 = value;
                        return;

                    case 140:
                        this.char140 = value;
                        return;

                    case 0x8d:
                        this.char141 = value;
                        return;

                    case 0x8e:
                        this.char142 = value;
                        return;

                    case 0x8f:
                        this.char143 = value;
                        return;

                    case 0x90:
                        this.char144 = value;
                        return;

                    case 0x91:
                        this.char145 = value;
                        return;

                    case 0x92:
                        this.char146 = value;
                        return;

                    case 0x93:
                        this.char147 = value;
                        return;

                    case 0x94:
                        this.char148 = value;
                        return;

                    case 0x95:
                        this.char149 = value;
                        return;

                    case 150:
                        this.char150 = value;
                        return;

                    case 0x97:
                        this.char151 = value;
                        return;

                    case 0x98:
                        this.char152 = value;
                        return;

                    case 0x99:
                        this.char153 = value;
                        return;

                    case 0x9a:
                        this.char154 = value;
                        return;

                    case 0x9b:
                        this.char155 = value;
                        return;

                    case 0x9c:
                        this.char156 = value;
                        return;

                    case 0x9d:
                        this.char157 = value;
                        return;

                    case 0x9e:
                        this.char158 = value;
                        return;

                    case 0x9f:
                        this.char159 = value;
                        return;

                    case 160:
                        this.char160 = value;
                        return;

                    case 0xa1:
                        this.char161 = value;
                        return;

                    case 0xa2:
                        this.char162 = value;
                        return;

                    case 0xa3:
                        this.char163 = value;
                        return;

                    case 0xa4:
                        this.char164 = value;
                        return;

                    case 0xa5:
                        this.char165 = value;
                        return;

                    case 0xa6:
                        this.char166 = value;
                        return;

                    case 0xa7:
                        this.char167 = value;
                        return;

                    case 0xa8:
                        this.char168 = value;
                        return;

                    case 0xa9:
                        this.char169 = value;
                        return;

                    case 170:
                        this.char170 = value;
                        return;

                    case 0xab:
                        this.char171 = value;
                        return;

                    case 0xac:
                        this.char172 = value;
                        return;

                    case 0xad:
                        this.char173 = value;
                        return;

                    case 0xae:
                        this.char174 = value;
                        return;

                    case 0xaf:
                        this.char175 = value;
                        return;

                    case 0xb0:
                        this.char176 = value;
                        return;

                    case 0xb1:
                        this.char177 = value;
                        return;

                    case 0xb2:
                        this.char178 = value;
                        return;

                    case 0xb3:
                        this.char179 = value;
                        return;

                    case 180:
                        this.char180 = value;
                        return;

                    case 0xb5:
                        this.char181 = value;
                        return;

                    case 0xb6:
                        this.char182 = value;
                        return;

                    case 0xb7:
                        this.char183 = value;
                        return;

                    case 0xb8:
                        this.char184 = value;
                        return;

                    case 0xb9:
                        this.char185 = value;
                        return;

                    case 0xba:
                        this.char186 = value;
                        return;

                    case 0xbb:
                        this.char187 = value;
                        return;

                    case 0xbc:
                        this.char188 = value;
                        return;

                    case 0xbd:
                        this.char189 = value;
                        return;

                    case 190:
                        this.char190 = value;
                        return;

                    case 0xbf:
                        this.char191 = value;
                        return;

                    case 0xc0:
                        this.char192 = value;
                        return;

                    case 0xc1:
                        this.char193 = value;
                        return;

                    case 0xc2:
                        this.char194 = value;
                        return;

                    case 0xc3:
                        this.char195 = value;
                        return;

                    case 0xc4:
                        this.char196 = value;
                        return;

                    case 0xc5:
                        this.char197 = value;
                        return;

                    case 0xc6:
                        this.char198 = value;
                        return;

                    case 0xc7:
                        this.char199 = value;
                        return;

                    case 200:
                        this.char200 = value;
                        return;

                    case 0xc9:
                        this.char201 = value;
                        return;

                    case 0xca:
                        this.char202 = value;
                        return;

                    case 0xcb:
                        this.char203 = value;
                        return;

                    case 0xcc:
                        this.char204 = value;
                        return;

                    case 0xcd:
                        this.char205 = value;
                        return;

                    case 0xce:
                        this.char206 = value;
                        return;

                    case 0xcf:
                        this.char207 = value;
                        return;

                    case 0xd0:
                        this.char208 = value;
                        return;

                    case 0xd1:
                        this.char209 = value;
                        return;

                    case 210:
                        this.char210 = value;
                        return;

                    case 0xd3:
                        this.char211 = value;
                        return;

                    case 0xd4:
                        this.char212 = value;
                        return;

                    case 0xd5:
                        this.char213 = value;
                        return;

                    case 0xd6:
                        this.char214 = value;
                        return;

                    case 0xd7:
                        this.char215 = value;
                        return;

                    case 0xd8:
                        this.char216 = value;
                        return;

                    case 0xd9:
                        this.char217 = value;
                        return;

                    case 0xda:
                        this.char218 = value;
                        return;

                    case 0xdb:
                        this.char219 = value;
                        return;

                    case 220:
                        this.char220 = value;
                        return;

                    case 0xdd:
                        this.char221 = value;
                        return;

                    case 0xde:
                        this.char222 = value;
                        return;

                    case 0xdf:
                        this.char223 = value;
                        return;

                    case 0xe0:
                        this.char224 = value;
                        return;

                    case 0xe1:
                        this.char225 = value;
                        return;

                    case 0xe2:
                        this.char226 = value;
                        return;

                    case 0xe3:
                        this.char227 = value;
                        return;

                    case 0xe4:
                        this.char228 = value;
                        return;

                    case 0xe5:
                        this.char229 = value;
                        return;

                    case 230:
                        this.char230 = value;
                        return;

                    case 0xe7:
                        this.char231 = value;
                        return;

                    case 0xe8:
                        this.char232 = value;
                        return;

                    case 0xe9:
                        this.char233 = value;
                        return;

                    case 0xea:
                        this.char234 = value;
                        return;

                    case 0xeb:
                        this.char235 = value;
                        return;

                    case 0xec:
                        this.char236 = value;
                        return;

                    case 0xed:
                        this.char237 = value;
                        return;

                    case 0xee:
                        this.char238 = value;
                        return;

                    case 0xef:
                        this.char239 = value;
                        return;

                    case 240:
                        this.char240 = value;
                        return;

                    case 0xf1:
                        this.char241 = value;
                        return;

                    case 0xf2:
                        this.char242 = value;
                        return;

                    case 0xf3:
                        this.char243 = value;
                        return;

                    case 0xf4:
                        this.char244 = value;
                        return;

                    case 0xf5:
                        this.char245 = value;
                        return;

                    case 0xf6:
                        this.char246 = value;
                        return;

                    case 0xf7:
                        this.char247 = value;
                        return;

                    case 0xf8:
                        this.char248 = value;
                        return;

                    case 0xf9:
                        this.char249 = value;
                        return;

                    case 250:
                        this.char250 = value;
                        return;

                    case 0xfb:
                        this.char251 = value;
                        return;

                    case 0xfc:
                        this.char252 = value;
                        return;

                    case 0xfd:
                        this.char253 = value;
                        return;

                    case 0xfe:
                        this.char254 = value;
                        return;

                    case 0xff:
                        this.char255 = value;
                        return;
                }
            }
        }
        public charbuffer(string str)
        {
            this = new charbuffer();
            this.str_val = str;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < 0x100; i++)
            {
                if (this[i] == '\0')
                {
                    return str;
                }
                str = str + this[i];
            }
            return str;
        }

        public string str_val
        {
            get
            {
                return this.ToString();
            }
            set
            {
                for (int i = 0; i < 0x100; i++)
                {
                    if (i < value.Length)
                    {
                        this[i] = value[i];
                    }
                    else
                    {
                        this[i] = '\0';
                    }
                }
            }
        }
        public static implicit operator string(charbuffer c)
        {
            return c.ToString();
        }

        public static implicit operator charbuffer(string s)
        {
            charbuffer charbuffer = new charbuffer();
            charbuffer.str_val = s;
            return charbuffer;
        }
    }
}
*/
