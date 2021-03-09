namespace Trancity
{
    using SlimDX;

    public class Светофор : BaseSignal
    {
        public int/*Стрелки*/ жёлтая_стрелка;
        public int/*Стрелки*/ зелёная_стрелка;
        public int/*Стрелки*/ красная_стрелка;
        //        public bool стрелка;
        private int green_mtrl;
        private int red_mtrl;
        private int yellow_mtrl;
        public int tex_count;

        public Светофор(string name) : base(name, 2)
        {
            green_mtrl = model.FindNumericArg("green_mtrl", -1);
            red_mtrl = model.FindNumericArg("red_mtrl", -1);
            yellow_mtrl = model.FindNumericArg("yellow_mtrl", -1);
            tex_count = model.FindNumericArg("custom_tex_count", 0);
        }

        public override void CreateMesh()
        {
            if (model == null) return;
            base.meshDir = model.dir;
            base.CreateMesh();
            if (green_mtrl >= base._meshMaterials.Length) green_mtrl = -1;
            if (red_mtrl >= base._meshMaterials.Length) red_mtrl = -1;
            if (yellow_mtrl >= base._meshMaterials.Length) yellow_mtrl = -1;
            if ((red_mtrl >= 0) && (this.красная_стрелка != 0/*Стрелки.нет*/))
            {
                var str = model.FindStringArg("tex" + (this.красная_стрелка - 1), string.Empty);
                if (str != string.Empty)
                    base.LoadTexture(red_mtrl, base.meshDir + str);
            }
            if ((yellow_mtrl >= 0) && (this.жёлтая_стрелка != 0/*Стрелки.нет*/))
            {
                var str = model.FindStringArg("tex" + (this.жёлтая_стрелка - 1), string.Empty);
                if (str != string.Empty)
                    base.LoadTexture(yellow_mtrl, base.meshDir + str);
            }
            if ((green_mtrl >= 0) && (this.зелёная_стрелка != 0/*Стрелки.нет*/))
            {
                var str = model.FindStringArg("tex" + (this.зелёная_стрелка - 1), string.Empty);
                if (str != string.Empty)
                    base.LoadTexture(green_mtrl, base.meshDir + str);
            }
        }

        public void Custom_render(bool нужен_зелёный, bool нужен_жёлтый, bool нужен_красный)
        {
            if ((MatricesCount == 0) && (base._meshMaterials == null)) return;
            if (red_mtrl >= 0)
            {
                Color4 value3 = base._meshMaterials[red_mtrl].Emissive;
                if (нужен_красный && (value3.Red < 1f))
                {
                    value3.Red += 0.2f;
                }
                if (!нужен_красный && (value3.Red > 0.0001f))
                {
                    value3.Red -= 0.2f;
                }
                base._meshMaterials[red_mtrl].Emissive = value3;
            }
            if (yellow_mtrl >= 0)
            {
                Color4 value4 = base._meshMaterials[yellow_mtrl].Emissive;
                if (нужен_жёлтый && (value4.Green < 1f))
                {
                    value4.Red += 0.2f;
                    value4.Green += 0.2f;
                }
                if (!нужен_жёлтый && (value4.Green > 0.0001f))
                {
                    value4.Red -= 0.2f;
                    value4.Green -= 0.2f;
                }
                base._meshMaterials[yellow_mtrl].Emissive = value4;
            }
            if (green_mtrl >= 0)
            {
                Color4 value5 = base._meshMaterials[green_mtrl].Emissive;
                if (нужен_зелёный && (value5.Green < 1f))
                {
                    value5.Green += 0.2f;
                }
                if (!нужен_зелёный && (value5.Green > 0.0001f))
                {
                    value5.Green -= 0.2f;
                }
                base._meshMaterials[green_mtrl].Emissive = value5;
            }
            base.Render();
        }

        /*public enum Стрелки
        {
            нет,
            вправо,
            прямо,
            влево,
            белая_вправо,
            белая_прямо,
            белая_влево,
            белая_прямо_вправо,
            белая_прямо_влево,
            Count
        }*/
    }
}

