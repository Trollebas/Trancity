/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 23.08.2013
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Trancity
{
	partial class UserControlForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.Save_Button = new System.Windows.Forms.Button();
			this.Close_Button = new System.Windows.Forms.Button();
			this.KeysListBox = new System.Windows.Forms.ListBox();
			this.Reset_Button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Save_Button
			// 
			this.Save_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Save_Button.Location = new System.Drawing.Point(12, 380);
			this.Save_Button.Name = "Save_Button";
			this.Save_Button.Size = new System.Drawing.Size(115, 35);
			this.Save_Button.TabIndex = 0;
			this.Save_Button.Text = "Сохранить";
			this.Save_Button.UseVisualStyleBackColor = true;
			this.Save_Button.Click += new System.EventHandler(this.Save_ButtonClick);
			// 
			// Close_Button
			// 
			this.Close_Button.Location = new System.Drawing.Point(342, 380);
			this.Close_Button.Name = "Close_Button";
			this.Close_Button.Size = new System.Drawing.Size(115, 35);
			this.Close_Button.TabIndex = 0;
			this.Close_Button.Text = "Закрыть";
			this.Close_Button.UseVisualStyleBackColor = true;
			this.Close_Button.Click += new System.EventHandler(this.Close_ButtonClick);
			// 
			// KeysListBox
			// 
			this.KeysListBox.DisplayMember = "1";
			this.KeysListBox.FormattingEnabled = true;
			this.KeysListBox.Items.AddRange(new object[] {
			"Пауза",
			"Режим карты",
			"Автоматическое управление",
			"Ручное управление",
			"Запустить/заглушить двигатель",
			"Дверь водителя",
			"Все двери",
			"Первая дверь",
			"Вторая дверь",
			"Третия дверь",
			"Четвёртая дверь",
			"Пятая дверь",
			"Шестая дверь",
			"Седьмая дверь",
			"Восьмая дверь",
			"Девятая дверь",
			"Фары",
			"Камера 1",
			"Камера 2",
			"Камера 3",
			"Камера 4",
			"Токоприёмник",
			"Реверс",
			"Ход",
			"Тормоз",
			"Налево",
			"Направо",
			"Сброс хода",
			"Трамваная аварийка",
			"Трамвайный поворотник налево",
			"Трамвайный поворотник направо",
			"Аварийка",
			"Поворотник налево",
			"Поворотник направо",
			"Передача вперёд",
			"Передача назад",
			"Сделать скриншот",
			"Отладочная информация"});
			this.KeysListBox.Location = new System.Drawing.Point(12, 19);
			this.KeysListBox.Name = "KeysListBox";
			this.KeysListBox.Size = new System.Drawing.Size(445, 355);
			this.KeysListBox.TabIndex = 1;
			this.KeysListBox.Tag = "UpdateListBox";
			this.KeysListBox.ValueMember = "1";
			// 
			// Reset_Button
			// 
			this.Reset_Button.Location = new System.Drawing.Point(133, 391);
			this.Reset_Button.Name = "Reset_Button";
			this.Reset_Button.Size = new System.Drawing.Size(88, 24);
			this.Reset_Button.TabIndex = 0;
			this.Reset_Button.Text = "Восстановить";
			this.Reset_Button.UseVisualStyleBackColor = true;
			this.Reset_Button.Click += new System.EventHandler(this.Reset_ButtonClick);
			// 
			// UserControlForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(469, 427);
			this.Controls.Add(this.KeysListBox);
			this.Controls.Add(this.Reset_Button);
			this.Controls.Add(this.Close_Button);
			this.Controls.Add(this.Save_Button);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UserControlForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Управление";
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button Reset_Button;
		private System.Windows.Forms.ListBox KeysListBox;
		private System.Windows.Forms.Button Close_Button;
		private System.Windows.Forms.Button Save_Button;
	}
}
