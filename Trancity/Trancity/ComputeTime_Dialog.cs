using Engine;
using System;
using System.Windows.Forms;


namespace Trancity
{
    public class ComputeTimeDialog : Form
    {
        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.ProgressBar _progressBar;
        private bool _refreshing;
        private string _text;
        private System.Windows.Forms.Timer _timer;
        private Игрок[] _игроки;
        private Игрок _игрок;
        private World _мир;
        private Trip _рейс;
        private Transport _транспорт;
        private bool b1 = false;

        public ComputeTimeDialog(World мир, int typeOfTransport, Trip рейс, Игрок игрок_камера)
        {
            InitializeComponent();
            Localization.ApplyLocalization(this);
            _мир = мир;
            _рейс = рейс;
            _игроки = new Игрок[0];
            _игрок = игрок_камера;
            var парк = new Парк("");
            var route = new Route(typeOfTransport, "");
            route.orders = new[] { new Order(парк, route, "", "") };
            route.orders[0].рейсы = new[] { рейс };
            рейс.InitTripStopList(route);
            if (((typeOfTransport == TypeOfTransport.Bus) && (Модели.Автобусы.Count == 0)) || ((typeOfTransport == TypeOfTransport.Trolleybus) && (Модели.Троллейбусы.Count == 0)) || ((typeOfTransport == TypeOfTransport.Tramway) && (Модели.Трамваи.Count == 0)))
            {
                MessageBox.Show(this, string.Format("Тип модели {0} не найден!", typeOfTransport.ToString()), "Редактор", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            switch (typeOfTransport)
            {
                case TypeOfTransport.Tramway:
                    _транспорт = new Трамвай.ОбычныйТрамвай(Модели.Трамваи[Cheats._random.Next(0, Модели.Трамваи.Count)], (Рельс)рейс.дорога_отправления, 0.0, Управление.Автоматическое, парк, route, null);
                    break;
                case TypeOfTransport.Trolleybus:
                    {
                        var point = new Double3DPoint
                        {
                            XZPoint = рейс.дорога_отправления.концы[0],
                            y = рейс.дорога_отправления.высота[0]
                        };
                        _транспорт = new Троллейбус.ОбычныйТроллейбус(Модели.Троллейбусы[Cheats._random.Next(0, Модели.Троллейбусы.Count)], point, new DoublePoint(рейс.дорога_отправления.направления[0], 0.0), Управление.Автоматическое, парк, route, null);
                        _транспорт.SetPosition(рейс.дорога_отправления, 0.0, 0.0, point, new DoublePoint(рейс.дорога_отправления.направления[0], 0.0), this._мир);
                    }
                    break;
                case TypeOfTransport.Bus:
                    {
                        var point2 = new Double3DPoint
                        {
                            XZPoint = рейс.дорога_отправления.концы[0],
                            y = рейс.дорога_отправления.высота[0]
                        };
                        _транспорт = new Троллейбус.ОбычныйТроллейбус(Модели.Автобусы[Cheats._random.Next(0, Модели.Автобусы.Count)], point2, new DoublePoint(рейс.дорога_отправления.направления[0], 0.0), Управление.Автоматическое, парк, route, null);
                    }
                    break;
            }
            _транспорт.наряд = route.orders[0];
            _транспорт.рейс = рейс;
            мир.транспорты.Add(_транспорт);
            _progressBar.Maximum = (int)Math.Ceiling(рейс.длина_пути - рейс.дорога_прибытия.Длина);
            _timer.Enabled = true;
        }

        private void ComputeTime_Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Enabled = false;
            _мир.транспорты.Remove(_транспорт);
            //Троллейбус троллейбус;
            foreach (var положение in _транспорт.найденные_положения)
            {
                if (положение.Дорога != null)
                {
                    положение.Дорога.занятыеПоложения.Remove(положение);
                }
            }
            if (_транспорт is Трамвай)
            {
                foreach (Трамвай.Ось ось in ((Трамвай)_транспорт).все_оси)
                {
                    ось.текущий_рельс.objects.Remove(ось);
                }
                var pant = ((Трамвай)_транспорт).токоприёмник;
                if (pant.Провод != null)
                {
                    pant.Провод.objects.Remove(pant);
                }
                return;
            }
            if (!(_транспорт is Троллейбус)) return;
            foreach (var штанга in ((Троллейбус)_транспорт).штанги)
            {
                if (штанга.Провод != null)
                {
                    штанга.Провод.objects.Remove(штанга);
                    //                    троллейбус.ах.включён = !троллейбус.ах.включён;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(12, 32);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(745, 23);
            this.Cancel_Button.TabIndex = 0;
            this.Cancel_Button.Text = "Отмена";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            // 
            // _progressBar
            // 
            this._progressBar.Location = new System.Drawing.Point(12, 12);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(745, 14);
            this._progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this._progressBar.TabIndex = 1;
            // 
            // _timer
            // 
            this._timer.Interval = 1;
            this._timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // ComputeTimeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_Button;
            this.ClientSize = new System.Drawing.Size(769, 67);
            this.ControlBox = false;
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this.Cancel_Button);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComputeTimeDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Вычисление времени рейса";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComputeTime_Dialog_FormClosing);
            this.Load += new System.EventHandler(this.ComputeTimeDialog_Load);
            this.ResumeLayout(false);

        }
        private System.ComponentModel.IContainer components;

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!this._refreshing)
            {
                this._refreshing = true;
                if (this._транспорт.рейс_index == (this._рейс.pathes.Length - 1))
                {
                    this._timer.Enabled = false;
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
                else
                {
                    this._транспорт.АвтоматическиУправлять(this._мир);
                    if (((this._транспорт.осталось_стоять > 0.0) && (this._транспорт.скорость == 0.0)) && (this._транспорт.ускорение == 0.0))
                    {
                        this._мир.системноеВремя -= this._транспорт.осталось_стоять;
                    }
                    bool flag = false;
                    if (this._транспорт is Трамвай)
                    {
                        Рельс рельс = ((Трамвай)this._транспорт).передняя_ось.текущий_рельс;
                        if ((рельс.следующие_рельсы.Length > 1) && (this._рейс.pathes[this._транспорт.рейс_index] == рельс))
                        {
                            if (this._рейс.pathes[this._транспорт.рейс_index + 1] == рельс.следующие_рельсы[0])
                            {
                                рельс.следующий_рельс = 0;
                            }
                            else if (this._рейс.pathes[this._транспорт.рейс_index + 1] == рельс.следующие_рельсы[1])
                            {
                                рельс.следующий_рельс = 1;
                            }
                        }
                    }
                    else if (this._транспорт is Троллейбус)
                    {
                        foreach (Троллейбус.Штанга штанга in ((Троллейбус)this._транспорт).штанги)
                        {
                            if (!штанга.Поднята)
                            {
                                штанга.НайтиПровод(this._мир.контактныеПровода);
                                штанга.поднимается = true;
                                штанга.угол = штанга.уголNormal;
                                if (!штанга.Поднята)
                                {
                                    flag = true;
                                }
                            }
                            else
                                b1 = true;
                        }
                    }
                    this._мир.системноеВремя -= 0.275;
                    this._мир.Обновить(this._игроки);
                    string str = string.Format(". {0}: {1} {2}", Localization.current_.speed, ((this._транспорт.скорость * 3.6)).ToString("0.00"), Localization.current_.speed_km);
                    if (flag)
                    {
                        str = str + " (" + Localization.current_.shtangi_loosed + ")";
                        if (b1)
                        {
                            _игрок.cameraPosition.XZPoint = _транспорт.position;
                            b1 = false;
                        }
                    }
                    if (this._text == null)
                    {
                        this._text = this.Text;
                    }
                    this.Text = this._text + str;
                    double a = 0.0;
                    for (int i = 0; i < this._транспорт.рейс_index; i++)
                    {
                        a += this._рейс.pathes[i].Длина;
                    }
                    if ((this._транспорт.рейс_index > 0) || (this._транспорт.текущее_положение.Дорога == this._рейс.дорога_отправления))
                    {
                        a += this._транспорт.текущее_положение.расстояние;
                    }
                    this._progressBar.Value = Math.Min((int)Math.Round(a), this._progressBar.Maximum);
                    this._refreshing = false;
                }
            }
        }
        void ComputeTimeDialog_Load(object sender, EventArgs e)
        {
            // TODO: Implement ComputeTimeDialog_Load
        }
    }
}

