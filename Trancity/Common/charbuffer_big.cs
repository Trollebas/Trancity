/*namespace Common
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), DebuggerNonUserCode]
    public struct charbuffer_big
    {
        public const int size = 0x10000;
        public charbuffer buff000;
        public charbuffer buff001;
        public charbuffer buff002;
        public charbuffer buff003;
        public charbuffer buff004;
        public charbuffer buff005;
        public charbuffer buff006;
        public charbuffer buff007;
        public charbuffer buff008;
        public charbuffer buff009;
        public charbuffer buff010;
        public charbuffer buff011;
        public charbuffer buff012;
        public charbuffer buff013;
        public charbuffer buff014;
        public charbuffer buff015;
        public charbuffer buff016;
        public charbuffer buff017;
        public charbuffer buff018;
        public charbuffer buff019;
        public charbuffer buff020;
        public charbuffer buff021;
        public charbuffer buff022;
        public charbuffer buff023;
        public charbuffer buff024;
        public charbuffer buff025;
        public charbuffer buff026;
        public charbuffer buff027;
        public charbuffer buff028;
        public charbuffer buff029;
        public charbuffer buff030;
        public charbuffer buff031;
        public charbuffer buff032;
        public charbuffer buff033;
        public charbuffer buff034;
        public charbuffer buff035;
        public charbuffer buff036;
        public charbuffer buff037;
        public charbuffer buff038;
        public charbuffer buff039;
        public charbuffer buff040;
        public charbuffer buff041;
        public charbuffer buff042;
        public charbuffer buff043;
        public charbuffer buff044;
        public charbuffer buff045;
        public charbuffer buff046;
        public charbuffer buff047;
        public charbuffer buff048;
        public charbuffer buff049;
        public charbuffer buff050;
        public charbuffer buff051;
        public charbuffer buff052;
        public charbuffer buff053;
        public charbuffer buff054;
        public charbuffer buff055;
        public charbuffer buff056;
        public charbuffer buff057;
        public charbuffer buff058;
        public charbuffer buff059;
        public charbuffer buff060;
        public charbuffer buff061;
        public charbuffer buff062;
        public charbuffer buff063;
        public charbuffer buff064;
        public charbuffer buff065;
        public charbuffer buff066;
        public charbuffer buff067;
        public charbuffer buff068;
        public charbuffer buff069;
        public charbuffer buff070;
        public charbuffer buff071;
        public charbuffer buff072;
        public charbuffer buff073;
        public charbuffer buff074;
        public charbuffer buff075;
        public charbuffer buff076;
        public charbuffer buff077;
        public charbuffer buff078;
        public charbuffer buff079;
        public charbuffer buff080;
        public charbuffer buff081;
        public charbuffer buff082;
        public charbuffer buff083;
        public charbuffer buff084;
        public charbuffer buff085;
        public charbuffer buff086;
        public charbuffer buff087;
        public charbuffer buff088;
        public charbuffer buff089;
        public charbuffer buff090;
        public charbuffer buff091;
        public charbuffer buff092;
        public charbuffer buff093;
        public charbuffer buff094;
        public charbuffer buff095;
        public charbuffer buff096;
        public charbuffer buff097;
        public charbuffer buff098;
        public charbuffer buff099;
        public charbuffer buff100;
        public charbuffer buff101;
        public charbuffer buff102;
        public charbuffer buff103;
        public charbuffer buff104;
        public charbuffer buff105;
        public charbuffer buff106;
        public charbuffer buff107;
        public charbuffer buff108;
        public charbuffer buff109;
        public charbuffer buff110;
        public charbuffer buff111;
        public charbuffer buff112;
        public charbuffer buff113;
        public charbuffer buff114;
        public charbuffer buff115;
        public charbuffer buff116;
        public charbuffer buff117;
        public charbuffer buff118;
        public charbuffer buff119;
        public charbuffer buff120;
        public charbuffer buff121;
        public charbuffer buff122;
        public charbuffer buff123;
        public charbuffer buff124;
        public charbuffer buff125;
        public charbuffer buff126;
        public charbuffer buff127;
        public charbuffer buff128;
        public charbuffer buff129;
        public charbuffer buff130;
        public charbuffer buff131;
        public charbuffer buff132;
        public charbuffer buff133;
        public charbuffer buff134;
        public charbuffer buff135;
        public charbuffer buff136;
        public charbuffer buff137;
        public charbuffer buff138;
        public charbuffer buff139;
        public charbuffer buff140;
        public charbuffer buff141;
        public charbuffer buff142;
        public charbuffer buff143;
        public charbuffer buff144;
        public charbuffer buff145;
        public charbuffer buff146;
        public charbuffer buff147;
        public charbuffer buff148;
        public charbuffer buff149;
        public charbuffer buff150;
        public charbuffer buff151;
        public charbuffer buff152;
        public charbuffer buff153;
        public charbuffer buff154;
        public charbuffer buff155;
        public charbuffer buff156;
        public charbuffer buff157;
        public charbuffer buff158;
        public charbuffer buff159;
        public charbuffer buff160;
        public charbuffer buff161;
        public charbuffer buff162;
        public charbuffer buff163;
        public charbuffer buff164;
        public charbuffer buff165;
        public charbuffer buff166;
        public charbuffer buff167;
        public charbuffer buff168;
        public charbuffer buff169;
        public charbuffer buff170;
        public charbuffer buff171;
        public charbuffer buff172;
        public charbuffer buff173;
        public charbuffer buff174;
        public charbuffer buff175;
        public charbuffer buff176;
        public charbuffer buff177;
        public charbuffer buff178;
        public charbuffer buff179;
        public charbuffer buff180;
        public charbuffer buff181;
        public charbuffer buff182;
        public charbuffer buff183;
        public charbuffer buff184;
        public charbuffer buff185;
        public charbuffer buff186;
        public charbuffer buff187;
        public charbuffer buff188;
        public charbuffer buff189;
        public charbuffer buff190;
        public charbuffer buff191;
        public charbuffer buff192;
        public charbuffer buff193;
        public charbuffer buff194;
        public charbuffer buff195;
        public charbuffer buff196;
        public charbuffer buff197;
        public charbuffer buff198;
        public charbuffer buff199;
        public charbuffer buff200;
        public charbuffer buff201;
        public charbuffer buff202;
        public charbuffer buff203;
        public charbuffer buff204;
        public charbuffer buff205;
        public charbuffer buff206;
        public charbuffer buff207;
        public charbuffer buff208;
        public charbuffer buff209;
        public charbuffer buff210;
        public charbuffer buff211;
        public charbuffer buff212;
        public charbuffer buff213;
        public charbuffer buff214;
        public charbuffer buff215;
        public charbuffer buff216;
        public charbuffer buff217;
        public charbuffer buff218;
        public charbuffer buff219;
        public charbuffer buff220;
        public charbuffer buff221;
        public charbuffer buff222;
        public charbuffer buff223;
        public charbuffer buff224;
        public charbuffer buff225;
        public charbuffer buff226;
        public charbuffer buff227;
        public charbuffer buff228;
        public charbuffer buff229;
        public charbuffer buff230;
        public charbuffer buff231;
        public charbuffer buff232;
        public charbuffer buff233;
        public charbuffer buff234;
        public charbuffer buff235;
        public charbuffer buff236;
        public charbuffer buff237;
        public charbuffer buff238;
        public charbuffer buff239;
        public charbuffer buff240;
        public charbuffer buff241;
        public charbuffer buff242;
        public charbuffer buff243;
        public charbuffer buff244;
        public charbuffer buff245;
        public charbuffer buff246;
        public charbuffer buff247;
        public charbuffer buff248;
        public charbuffer buff249;
        public charbuffer buff250;
        public charbuffer buff251;
        public charbuffer buff252;
        public charbuffer buff253;
        public charbuffer buff254;
        public charbuffer buff255;
        public charbuffer get_buff(int index)
        {
            switch (index)
            {
                case 0:
                    return this.buff000;

                case 1:
                    return this.buff001;

                case 2:
                    return this.buff002;

                case 3:
                    return this.buff003;

                case 4:
                    return this.buff004;

                case 5:
                    return this.buff005;

                case 6:
                    return this.buff006;

                case 7:
                    return this.buff007;

                case 8:
                    return this.buff008;

                case 9:
                    return this.buff009;

                case 10:
                    return this.buff010;

                case 11:
                    return this.buff011;

                case 12:
                    return this.buff012;

                case 13:
                    return this.buff013;

                case 14:
                    return this.buff014;

                case 15:
                    return this.buff015;

                case 0x10:
                    return this.buff016;

                case 0x11:
                    return this.buff017;

                case 0x12:
                    return this.buff018;

                case 0x13:
                    return this.buff019;

                case 20:
                    return this.buff020;

                case 0x15:
                    return this.buff021;

                case 0x16:
                    return this.buff022;

                case 0x17:
                    return this.buff023;

                case 0x18:
                    return this.buff024;

                case 0x19:
                    return this.buff025;

                case 0x1a:
                    return this.buff026;

                case 0x1b:
                    return this.buff027;

                case 0x1c:
                    return this.buff028;

                case 0x1d:
                    return this.buff029;

                case 30:
                    return this.buff030;

                case 0x1f:
                    return this.buff031;

                case 0x20:
                    return this.buff032;

                case 0x21:
                    return this.buff033;

                case 0x22:
                    return this.buff034;

                case 0x23:
                    return this.buff035;

                case 0x24:
                    return this.buff036;

                case 0x25:
                    return this.buff037;

                case 0x26:
                    return this.buff038;

                case 0x27:
                    return this.buff039;

                case 40:
                    return this.buff040;

                case 0x29:
                    return this.buff041;

                case 0x2a:
                    return this.buff042;

                case 0x2b:
                    return this.buff043;

                case 0x2c:
                    return this.buff044;

                case 0x2d:
                    return this.buff045;

                case 0x2e:
                    return this.buff046;

                case 0x2f:
                    return this.buff047;

                case 0x30:
                    return this.buff048;

                case 0x31:
                    return this.buff049;

                case 50:
                    return this.buff050;

                case 0x33:
                    return this.buff051;

                case 0x34:
                    return this.buff052;

                case 0x35:
                    return this.buff053;

                case 0x36:
                    return this.buff054;

                case 0x37:
                    return this.buff055;

                case 0x38:
                    return this.buff056;

                case 0x39:
                    return this.buff057;

                case 0x3a:
                    return this.buff058;

                case 0x3b:
                    return this.buff059;

                case 60:
                    return this.buff060;

                case 0x3d:
                    return this.buff061;

                case 0x3e:
                    return this.buff062;

                case 0x3f:
                    return this.buff063;

                case 0x40:
                    return this.buff064;

                case 0x41:
                    return this.buff065;

                case 0x42:
                    return this.buff066;

                case 0x43:
                    return this.buff067;

                case 0x44:
                    return this.buff068;

                case 0x45:
                    return this.buff069;

                case 70:
                    return this.buff070;

                case 0x47:
                    return this.buff071;

                case 0x48:
                    return this.buff072;

                case 0x49:
                    return this.buff073;

                case 0x4a:
                    return this.buff074;

                case 0x4b:
                    return this.buff075;

                case 0x4c:
                    return this.buff076;

                case 0x4d:
                    return this.buff077;

                case 0x4e:
                    return this.buff078;

                case 0x4f:
                    return this.buff079;

                case 80:
                    return this.buff080;

                case 0x51:
                    return this.buff081;

                case 0x52:
                    return this.buff082;

                case 0x53:
                    return this.buff083;

                case 0x54:
                    return this.buff084;

                case 0x55:
                    return this.buff085;

                case 0x56:
                    return this.buff086;

                case 0x57:
                    return this.buff087;

                case 0x58:
                    return this.buff088;

                case 0x59:
                    return this.buff089;

                case 90:
                    return this.buff090;

                case 0x5b:
                    return this.buff091;

                case 0x5c:
                    return this.buff092;

                case 0x5d:
                    return this.buff093;

                case 0x5e:
                    return this.buff094;

                case 0x5f:
                    return this.buff095;

                case 0x60:
                    return this.buff096;

                case 0x61:
                    return this.buff097;

                case 0x62:
                    return this.buff098;

                case 0x63:
                    return this.buff099;

                case 100:
                    return this.buff100;

                case 0x65:
                    return this.buff101;

                case 0x66:
                    return this.buff102;

                case 0x67:
                    return this.buff103;

                case 0x68:
                    return this.buff104;

                case 0x69:
                    return this.buff105;

                case 0x6a:
                    return this.buff106;

                case 0x6b:
                    return this.buff107;

                case 0x6c:
                    return this.buff108;

                case 0x6d:
                    return this.buff109;

                case 110:
                    return this.buff110;

                case 0x6f:
                    return this.buff111;

                case 0x70:
                    return this.buff112;

                case 0x71:
                    return this.buff113;

                case 0x72:
                    return this.buff114;

                case 0x73:
                    return this.buff115;

                case 0x74:
                    return this.buff116;

                case 0x75:
                    return this.buff117;

                case 0x76:
                    return this.buff118;

                case 0x77:
                    return this.buff119;

                case 120:
                    return this.buff120;

                case 0x79:
                    return this.buff121;

                case 0x7a:
                    return this.buff122;

                case 0x7b:
                    return this.buff123;

                case 0x7c:
                    return this.buff124;

                case 0x7d:
                    return this.buff125;

                case 0x7e:
                    return this.buff126;

                case 0x7f:
                    return this.buff127;

                case 0x80:
                    return this.buff128;

                case 0x81:
                    return this.buff129;

                case 130:
                    return this.buff130;

                case 0x83:
                    return this.buff131;

                case 0x84:
                    return this.buff132;

                case 0x85:
                    return this.buff133;

                case 0x86:
                    return this.buff134;

                case 0x87:
                    return this.buff135;

                case 0x88:
                    return this.buff136;

                case 0x89:
                    return this.buff137;

                case 0x8a:
                    return this.buff138;

                case 0x8b:
                    return this.buff139;

                case 140:
                    return this.buff140;

                case 0x8d:
                    return this.buff141;

                case 0x8e:
                    return this.buff142;

                case 0x8f:
                    return this.buff143;

                case 0x90:
                    return this.buff144;

                case 0x91:
                    return this.buff145;

                case 0x92:
                    return this.buff146;

                case 0x93:
                    return this.buff147;

                case 0x94:
                    return this.buff148;

                case 0x95:
                    return this.buff149;

                case 150:
                    return this.buff150;

                case 0x97:
                    return this.buff151;

                case 0x98:
                    return this.buff152;

                case 0x99:
                    return this.buff153;

                case 0x9a:
                    return this.buff154;

                case 0x9b:
                    return this.buff155;

                case 0x9c:
                    return this.buff156;

                case 0x9d:
                    return this.buff157;

                case 0x9e:
                    return this.buff158;

                case 0x9f:
                    return this.buff159;

                case 160:
                    return this.buff160;

                case 0xa1:
                    return this.buff161;

                case 0xa2:
                    return this.buff162;

                case 0xa3:
                    return this.buff163;

                case 0xa4:
                    return this.buff164;

                case 0xa5:
                    return this.buff165;

                case 0xa6:
                    return this.buff166;

                case 0xa7:
                    return this.buff167;

                case 0xa8:
                    return this.buff168;

                case 0xa9:
                    return this.buff169;

                case 170:
                    return this.buff170;

                case 0xab:
                    return this.buff171;

                case 0xac:
                    return this.buff172;

                case 0xad:
                    return this.buff173;

                case 0xae:
                    return this.buff174;

                case 0xaf:
                    return this.buff175;

                case 0xb0:
                    return this.buff176;

                case 0xb1:
                    return this.buff177;

                case 0xb2:
                    return this.buff178;

                case 0xb3:
                    return this.buff179;

                case 180:
                    return this.buff180;

                case 0xb5:
                    return this.buff181;

                case 0xb6:
                    return this.buff182;

                case 0xb7:
                    return this.buff183;

                case 0xb8:
                    return this.buff184;

                case 0xb9:
                    return this.buff185;

                case 0xba:
                    return this.buff186;

                case 0xbb:
                    return this.buff187;

                case 0xbc:
                    return this.buff188;

                case 0xbd:
                    return this.buff189;

                case 190:
                    return this.buff190;

                case 0xbf:
                    return this.buff191;

                case 0xc0:
                    return this.buff192;

                case 0xc1:
                    return this.buff193;

                case 0xc2:
                    return this.buff194;

                case 0xc3:
                    return this.buff195;

                case 0xc4:
                    return this.buff196;

                case 0xc5:
                    return this.buff197;

                case 0xc6:
                    return this.buff198;

                case 0xc7:
                    return this.buff199;

                case 200:
                    return this.buff200;

                case 0xc9:
                    return this.buff201;

                case 0xca:
                    return this.buff202;

                case 0xcb:
                    return this.buff203;

                case 0xcc:
                    return this.buff204;

                case 0xcd:
                    return this.buff205;

                case 0xce:
                    return this.buff206;

                case 0xcf:
                    return this.buff207;

                case 0xd0:
                    return this.buff208;

                case 0xd1:
                    return this.buff209;

                case 210:
                    return this.buff210;

                case 0xd3:
                    return this.buff211;

                case 0xd4:
                    return this.buff212;

                case 0xd5:
                    return this.buff213;

                case 0xd6:
                    return this.buff214;

                case 0xd7:
                    return this.buff215;

                case 0xd8:
                    return this.buff216;

                case 0xd9:
                    return this.buff217;

                case 0xda:
                    return this.buff218;

                case 0xdb:
                    return this.buff219;

                case 220:
                    return this.buff220;

                case 0xdd:
                    return this.buff221;

                case 0xde:
                    return this.buff222;

                case 0xdf:
                    return this.buff223;

                case 0xe0:
                    return this.buff224;

                case 0xe1:
                    return this.buff225;

                case 0xe2:
                    return this.buff226;

                case 0xe3:
                    return this.buff227;

                case 0xe4:
                    return this.buff228;

                case 0xe5:
                    return this.buff229;

                case 230:
                    return this.buff230;

                case 0xe7:
                    return this.buff231;

                case 0xe8:
                    return this.buff232;

                case 0xe9:
                    return this.buff233;

                case 0xea:
                    return this.buff234;

                case 0xeb:
                    return this.buff235;

                case 0xec:
                    return this.buff236;

                case 0xed:
                    return this.buff237;

                case 0xee:
                    return this.buff238;

                case 0xef:
                    return this.buff239;

                case 240:
                    return this.buff240;

                case 0xf1:
                    return this.buff241;

                case 0xf2:
                    return this.buff242;

                case 0xf3:
                    return this.buff243;

                case 0xf4:
                    return this.buff244;

                case 0xf5:
                    return this.buff245;

                case 0xf6:
                    return this.buff246;

                case 0xf7:
                    return this.buff247;

                case 0xf8:
                    return this.buff248;

                case 0xf9:
                    return this.buff249;

                case 250:
                    return this.buff250;

                case 0xfb:
                    return this.buff251;

                case 0xfc:
                    return this.buff252;

                case 0xfd:
                    return this.buff253;

                case 0xfe:
                    return this.buff254;

                case 0xff:
                    return this.buff255;
            }
            return new charbuffer();
        }

        public void set_buff(int index, charbuffer value)
        {
            switch (index)
            {
                case 0:
                    this.buff000 = value;
                    return;

                case 1:
                    this.buff001 = value;
                    return;

                case 2:
                    this.buff002 = value;
                    return;

                case 3:
                    this.buff003 = value;
                    return;

                case 4:
                    this.buff004 = value;
                    return;

                case 5:
                    this.buff005 = value;
                    return;

                case 6:
                    this.buff006 = value;
                    return;

                case 7:
                    this.buff007 = value;
                    return;

                case 8:
                    this.buff008 = value;
                    return;

                case 9:
                    this.buff009 = value;
                    return;

                case 10:
                    this.buff010 = value;
                    return;

                case 11:
                    this.buff011 = value;
                    return;

                case 12:
                    this.buff012 = value;
                    return;

                case 13:
                    this.buff013 = value;
                    return;

                case 14:
                    this.buff014 = value;
                    return;

                case 15:
                    this.buff015 = value;
                    return;

                case 0x10:
                    this.buff016 = value;
                    return;

                case 0x11:
                    this.buff017 = value;
                    return;

                case 0x12:
                    this.buff018 = value;
                    return;

                case 0x13:
                    this.buff019 = value;
                    return;

                case 20:
                    this.buff020 = value;
                    return;

                case 0x15:
                    this.buff021 = value;
                    return;

                case 0x16:
                    this.buff022 = value;
                    return;

                case 0x17:
                    this.buff023 = value;
                    return;

                case 0x18:
                    this.buff024 = value;
                    return;

                case 0x19:
                    this.buff025 = value;
                    return;

                case 0x1a:
                    this.buff026 = value;
                    return;

                case 0x1b:
                    this.buff027 = value;
                    return;

                case 0x1c:
                    this.buff028 = value;
                    return;

                case 0x1d:
                    this.buff029 = value;
                    return;

                case 30:
                    this.buff030 = value;
                    return;

                case 0x1f:
                    this.buff031 = value;
                    return;

                case 0x20:
                    this.buff032 = value;
                    return;

                case 0x21:
                    this.buff033 = value;
                    return;

                case 0x22:
                    this.buff034 = value;
                    return;

                case 0x23:
                    this.buff035 = value;
                    return;

                case 0x24:
                    this.buff036 = value;
                    return;

                case 0x25:
                    this.buff037 = value;
                    return;

                case 0x26:
                    this.buff038 = value;
                    return;

                case 0x27:
                    this.buff039 = value;
                    return;

                case 40:
                    this.buff040 = value;
                    return;

                case 0x29:
                    this.buff041 = value;
                    return;

                case 0x2a:
                    this.buff042 = value;
                    return;

                case 0x2b:
                    this.buff043 = value;
                    return;

                case 0x2c:
                    this.buff044 = value;
                    return;

                case 0x2d:
                    this.buff045 = value;
                    return;

                case 0x2e:
                    this.buff046 = value;
                    return;

                case 0x2f:
                    this.buff047 = value;
                    return;

                case 0x30:
                    this.buff048 = value;
                    return;

                case 0x31:
                    this.buff049 = value;
                    return;

                case 50:
                    this.buff050 = value;
                    return;

                case 0x33:
                    this.buff051 = value;
                    return;

                case 0x34:
                    this.buff052 = value;
                    return;

                case 0x35:
                    this.buff053 = value;
                    return;

                case 0x36:
                    this.buff054 = value;
                    return;

                case 0x37:
                    this.buff055 = value;
                    return;

                case 0x38:
                    this.buff056 = value;
                    return;

                case 0x39:
                    this.buff057 = value;
                    return;

                case 0x3a:
                    this.buff058 = value;
                    return;

                case 0x3b:
                    this.buff059 = value;
                    return;

                case 60:
                    this.buff060 = value;
                    return;

                case 0x3d:
                    this.buff061 = value;
                    return;

                case 0x3e:
                    this.buff062 = value;
                    return;

                case 0x3f:
                    this.buff063 = value;
                    return;

                case 0x40:
                    this.buff064 = value;
                    return;

                case 0x41:
                    this.buff065 = value;
                    return;

                case 0x42:
                    this.buff066 = value;
                    return;

                case 0x43:
                    this.buff067 = value;
                    return;

                case 0x44:
                    this.buff068 = value;
                    return;

                case 0x45:
                    this.buff069 = value;
                    return;

                case 70:
                    this.buff070 = value;
                    return;

                case 0x47:
                    this.buff071 = value;
                    return;

                case 0x48:
                    this.buff072 = value;
                    return;

                case 0x49:
                    this.buff073 = value;
                    return;

                case 0x4a:
                    this.buff074 = value;
                    return;

                case 0x4b:
                    this.buff075 = value;
                    return;

                case 0x4c:
                    this.buff076 = value;
                    return;

                case 0x4d:
                    this.buff077 = value;
                    return;

                case 0x4e:
                    this.buff078 = value;
                    return;

                case 0x4f:
                    this.buff079 = value;
                    return;

                case 80:
                    this.buff080 = value;
                    return;

                case 0x51:
                    this.buff081 = value;
                    return;

                case 0x52:
                    this.buff082 = value;
                    return;

                case 0x53:
                    this.buff083 = value;
                    return;

                case 0x54:
                    this.buff084 = value;
                    return;

                case 0x55:
                    this.buff085 = value;
                    return;

                case 0x56:
                    this.buff086 = value;
                    return;

                case 0x57:
                    this.buff087 = value;
                    return;

                case 0x58:
                    this.buff088 = value;
                    return;

                case 0x59:
                    this.buff089 = value;
                    return;

                case 90:
                    this.buff090 = value;
                    return;

                case 0x5b:
                    this.buff091 = value;
                    return;

                case 0x5c:
                    this.buff092 = value;
                    return;

                case 0x5d:
                    this.buff093 = value;
                    return;

                case 0x5e:
                    this.buff094 = value;
                    return;

                case 0x5f:
                    this.buff095 = value;
                    return;

                case 0x60:
                    this.buff096 = value;
                    return;

                case 0x61:
                    this.buff097 = value;
                    return;

                case 0x62:
                    this.buff098 = value;
                    return;

                case 0x63:
                    this.buff099 = value;
                    return;

                case 100:
                    this.buff100 = value;
                    return;

                case 0x65:
                    this.buff101 = value;
                    return;

                case 0x66:
                    this.buff102 = value;
                    return;

                case 0x67:
                    this.buff103 = value;
                    return;

                case 0x68:
                    this.buff104 = value;
                    return;

                case 0x69:
                    this.buff105 = value;
                    return;

                case 0x6a:
                    this.buff106 = value;
                    return;

                case 0x6b:
                    this.buff107 = value;
                    return;

                case 0x6c:
                    this.buff108 = value;
                    return;

                case 0x6d:
                    this.buff109 = value;
                    return;

                case 110:
                    this.buff110 = value;
                    return;

                case 0x6f:
                    this.buff111 = value;
                    return;

                case 0x70:
                    this.buff112 = value;
                    return;

                case 0x71:
                    this.buff113 = value;
                    return;

                case 0x72:
                    this.buff114 = value;
                    return;

                case 0x73:
                    this.buff115 = value;
                    return;

                case 0x74:
                    this.buff116 = value;
                    return;

                case 0x75:
                    this.buff117 = value;
                    return;

                case 0x76:
                    this.buff118 = value;
                    return;

                case 0x77:
                    this.buff119 = value;
                    return;

                case 120:
                    this.buff120 = value;
                    return;

                case 0x79:
                    this.buff121 = value;
                    return;

                case 0x7a:
                    this.buff122 = value;
                    return;

                case 0x7b:
                    this.buff123 = value;
                    return;

                case 0x7c:
                    this.buff124 = value;
                    return;

                case 0x7d:
                    this.buff125 = value;
                    return;

                case 0x7e:
                    this.buff126 = value;
                    return;

                case 0x7f:
                    this.buff127 = value;
                    return;

                case 0x80:
                    this.buff128 = value;
                    return;

                case 0x81:
                    this.buff129 = value;
                    return;

                case 130:
                    this.buff130 = value;
                    return;

                case 0x83:
                    this.buff131 = value;
                    return;

                case 0x84:
                    this.buff132 = value;
                    return;

                case 0x85:
                    this.buff133 = value;
                    return;

                case 0x86:
                    this.buff134 = value;
                    return;

                case 0x87:
                    this.buff135 = value;
                    return;

                case 0x88:
                    this.buff136 = value;
                    return;

                case 0x89:
                    this.buff137 = value;
                    return;

                case 0x8a:
                    this.buff138 = value;
                    return;

                case 0x8b:
                    this.buff139 = value;
                    return;

                case 140:
                    this.buff140 = value;
                    return;

                case 0x8d:
                    this.buff141 = value;
                    return;

                case 0x8e:
                    this.buff142 = value;
                    return;

                case 0x8f:
                    this.buff143 = value;
                    return;

                case 0x90:
                    this.buff144 = value;
                    return;

                case 0x91:
                    this.buff145 = value;
                    return;

                case 0x92:
                    this.buff146 = value;
                    return;

                case 0x93:
                    this.buff147 = value;
                    return;

                case 0x94:
                    this.buff148 = value;
                    return;

                case 0x95:
                    this.buff149 = value;
                    return;

                case 150:
                    this.buff150 = value;
                    return;

                case 0x97:
                    this.buff151 = value;
                    return;

                case 0x98:
                    this.buff152 = value;
                    return;

                case 0x99:
                    this.buff153 = value;
                    return;

                case 0x9a:
                    this.buff154 = value;
                    return;

                case 0x9b:
                    this.buff155 = value;
                    return;

                case 0x9c:
                    this.buff156 = value;
                    return;

                case 0x9d:
                    this.buff157 = value;
                    return;

                case 0x9e:
                    this.buff158 = value;
                    return;

                case 0x9f:
                    this.buff159 = value;
                    return;

                case 160:
                    this.buff160 = value;
                    return;

                case 0xa1:
                    this.buff161 = value;
                    return;

                case 0xa2:
                    this.buff162 = value;
                    return;

                case 0xa3:
                    this.buff163 = value;
                    return;

                case 0xa4:
                    this.buff164 = value;
                    return;

                case 0xa5:
                    this.buff165 = value;
                    return;

                case 0xa6:
                    this.buff166 = value;
                    return;

                case 0xa7:
                    this.buff167 = value;
                    return;

                case 0xa8:
                    this.buff168 = value;
                    return;

                case 0xa9:
                    this.buff169 = value;
                    return;

                case 170:
                    this.buff170 = value;
                    return;

                case 0xab:
                    this.buff171 = value;
                    return;

                case 0xac:
                    this.buff172 = value;
                    return;

                case 0xad:
                    this.buff173 = value;
                    return;

                case 0xae:
                    this.buff174 = value;
                    return;

                case 0xaf:
                    this.buff175 = value;
                    return;

                case 0xb0:
                    this.buff176 = value;
                    return;

                case 0xb1:
                    this.buff177 = value;
                    return;

                case 0xb2:
                    this.buff178 = value;
                    return;

                case 0xb3:
                    this.buff179 = value;
                    return;

                case 180:
                    this.buff180 = value;
                    return;

                case 0xb5:
                    this.buff181 = value;
                    return;

                case 0xb6:
                    this.buff182 = value;
                    return;

                case 0xb7:
                    this.buff183 = value;
                    return;

                case 0xb8:
                    this.buff184 = value;
                    return;

                case 0xb9:
                    this.buff185 = value;
                    return;

                case 0xba:
                    this.buff186 = value;
                    return;

                case 0xbb:
                    this.buff187 = value;
                    return;

                case 0xbc:
                    this.buff188 = value;
                    return;

                case 0xbd:
                    this.buff189 = value;
                    return;

                case 190:
                    this.buff190 = value;
                    return;

                case 0xbf:
                    this.buff191 = value;
                    return;

                case 0xc0:
                    this.buff192 = value;
                    return;

                case 0xc1:
                    this.buff193 = value;
                    return;

                case 0xc2:
                    this.buff194 = value;
                    return;

                case 0xc3:
                    this.buff195 = value;
                    return;

                case 0xc4:
                    this.buff196 = value;
                    return;

                case 0xc5:
                    this.buff197 = value;
                    return;

                case 0xc6:
                    this.buff198 = value;
                    return;

                case 0xc7:
                    this.buff199 = value;
                    return;

                case 200:
                    this.buff200 = value;
                    return;

                case 0xc9:
                    this.buff201 = value;
                    return;

                case 0xca:
                    this.buff202 = value;
                    return;

                case 0xcb:
                    this.buff203 = value;
                    return;

                case 0xcc:
                    this.buff204 = value;
                    return;

                case 0xcd:
                    this.buff205 = value;
                    return;

                case 0xce:
                    this.buff206 = value;
                    return;

                case 0xcf:
                    this.buff207 = value;
                    return;

                case 0xd0:
                    this.buff208 = value;
                    return;

                case 0xd1:
                    this.buff209 = value;
                    return;

                case 210:
                    this.buff210 = value;
                    return;

                case 0xd3:
                    this.buff211 = value;
                    return;

                case 0xd4:
                    this.buff212 = value;
                    return;

                case 0xd5:
                    this.buff213 = value;
                    return;

                case 0xd6:
                    this.buff214 = value;
                    return;

                case 0xd7:
                    this.buff215 = value;
                    return;

                case 0xd8:
                    this.buff216 = value;
                    return;

                case 0xd9:
                    this.buff217 = value;
                    return;

                case 0xda:
                    this.buff218 = value;
                    return;

                case 0xdb:
                    this.buff219 = value;
                    return;

                case 220:
                    this.buff220 = value;
                    return;

                case 0xdd:
                    this.buff221 = value;
                    return;

                case 0xde:
                    this.buff222 = value;
                    return;

                case 0xdf:
                    this.buff223 = value;
                    return;

                case 0xe0:
                    this.buff224 = value;
                    return;

                case 0xe1:
                    this.buff225 = value;
                    return;

                case 0xe2:
                    this.buff226 = value;
                    return;

                case 0xe3:
                    this.buff227 = value;
                    return;

                case 0xe4:
                    this.buff228 = value;
                    return;

                case 0xe5:
                    this.buff229 = value;
                    return;

                case 230:
                    this.buff230 = value;
                    return;

                case 0xe7:
                    this.buff231 = value;
                    return;

                case 0xe8:
                    this.buff232 = value;
                    return;

                case 0xe9:
                    this.buff233 = value;
                    return;

                case 0xea:
                    this.buff234 = value;
                    return;

                case 0xeb:
                    this.buff235 = value;
                    return;

                case 0xec:
                    this.buff236 = value;
                    return;

                case 0xed:
                    this.buff237 = value;
                    return;

                case 0xee:
                    this.buff238 = value;
                    return;

                case 0xef:
                    this.buff239 = value;
                    return;

                case 240:
                    this.buff240 = value;
                    return;

                case 0xf1:
                    this.buff241 = value;
                    return;

                case 0xf2:
                    this.buff242 = value;
                    return;

                case 0xf3:
                    this.buff243 = value;
                    return;

                case 0xf4:
                    this.buff244 = value;
                    return;

                case 0xf5:
                    this.buff245 = value;
                    return;

                case 0xf6:
                    this.buff246 = value;
                    return;

                case 0xf7:
                    this.buff247 = value;
                    return;

                case 0xf8:
                    this.buff248 = value;
                    return;

                case 0xf9:
                    this.buff249 = value;
                    return;

                case 250:
                    this.buff250 = value;
                    return;

                case 0xfb:
                    this.buff251 = value;
                    return;

                case 0xfc:
                    this.buff252 = value;
                    return;

                case 0xfd:
                    this.buff253 = value;
                    return;

                case 0xfe:
                    this.buff254 = value;
                    return;

                case 0xff:
                    this.buff255 = value;
                    return;
            }
        }

        public char this[int index]
        {
            get
            {
                return this.get_buff(index / 0x100)[index % 0x100];
            }
            set
            {
                charbuffer charbuffer = this.get_buff(index / 0x100);
                charbuffer[index % 0x100] = value;
                this.set_buff(index / 0x100, charbuffer);
            }
        }
        public charbuffer_big(string str)
        {
            this = new charbuffer_big();
            this.str_val = str;
        }

        public charbuffer_big(string[] arr)
        {
            this = new charbuffer_big();
            this.str_arr = arr;
        }

        public override string ToString()
        {
            string str = "";
            string[] strArray = this.str_arr;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (i > 0)
                {
                    str = str + "; ";
                }
                str = str + strArray[i];
            }
            return str;
        }

        public string str_val
        {
            get
            {
                string str = "";
                for (int i = 0; i < 0x10000; i++)
                {
                    if (this[i] == '\0')
                    {
                        return str;
                    }
                    str = str + this[i];
                }
                return str;
            }
            set
            {
                for (int i = 0; i < 0x10000; i++)
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
        public string[] str_arr
        {
            get
            {
                ArrayList list = new ArrayList();
                string str = "";
                int count = 0;
                for (int i = 0; i < 0x10000; i++)
                {
                    if (this[i] != '\0')
                    {
                        str = str + this[i];
                        count = list.Count + 1;
                    }
                    else
                    {
                        list.Add(str);
                        str = "";
                    }
                }
                list.Add(str);
                string[] array = new string[count];
                list.CopyTo(0, array, 0, count);
                return array;
            }
            set
            {
                string str = "";
                for (int i = 0; i < value.Length; i++)
                {
                    if (i > 0)
                    {
                        str = str + '\0';
                    }
                    str = str + value[i];
                }
                for (int j = 0; j < 0x10000; j++)
                {
                    if (j < str.Length)
                    {
                        this[j] = str[j];
                    }
                    else
                    {
                        this[j] = '\0';
                    }
                }
            }
        }
        public static implicit operator string(charbuffer_big c)
        {
            return c.str_val;
        }

        public static implicit operator charbuffer_big(string s)
        {
            charbuffer_big _big = new charbuffer_big();
            _big.str_val = s;
            return _big;
        }

        public static implicit operator string[](charbuffer_big c)
        {
            return c.str_arr;
        }

        public static implicit operator charbuffer_big(string[] s)
        {
            charbuffer_big _big = new charbuffer_big();
            _big.str_arr = s;
            return _big;
        }
    }
}

*/