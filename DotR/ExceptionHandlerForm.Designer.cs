/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 12.04.2015
 * Time: 17:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ODE_Test
{
	partial class ExceptionHandlerForm
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
			this.Continue_Button = new System.Windows.Forms.Button();
			this.Terminate_Button = new System.Windows.Forms.Button();
			this.Exc_Label = new System.Windows.Forms.Label();
			this.Message_TextBox = new System.Windows.Forms.TextBox();
			this.StackTrace_Label = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.ExcName_Label = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// Continue_Button
			// 
			this.Continue_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Continue_Button.DialogResult = System.Windows.Forms.DialogResult.Retry;
			this.Continue_Button.Location = new System.Drawing.Point(12, 307);
			this.Continue_Button.Name = "Continue_Button";
			this.Continue_Button.Size = new System.Drawing.Size(108, 23);
			this.Continue_Button.TabIndex = 0;
			this.Continue_Button.Text = "Продолжить";
			this.Continue_Button.UseVisualStyleBackColor = true;
			// 
			// Terminate_Button
			// 
			this.Terminate_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Terminate_Button.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.Terminate_Button.Location = new System.Drawing.Point(524, 307);
			this.Terminate_Button.Name = "Terminate_Button";
			this.Terminate_Button.Size = new System.Drawing.Size(108, 23);
			this.Terminate_Button.TabIndex = 1;
			this.Terminate_Button.Text = "Выход";
			this.Terminate_Button.UseVisualStyleBackColor = true;
			// 
			// Exc_Label
			// 
			this.Exc_Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.Exc_Label.Location = new System.Drawing.Point(12, 9);
			this.Exc_Label.Name = "Exc_Label";
			this.Exc_Label.Size = new System.Drawing.Size(552, 26);
			this.Exc_Label.TabIndex = 2;
			this.Exc_Label.Text = "Необработанное исключение типа {0} было вызвано {1}!";
			// 
			// Message_TextBox
			// 
			this.Message_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.Message_TextBox.Location = new System.Drawing.Point(12, 113);
			this.Message_TextBox.MaxLength = 327670;
			this.Message_TextBox.Multiline = true;
			this.Message_TextBox.Name = "Message_TextBox";
			this.Message_TextBox.ReadOnly = true;
			this.Message_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.Message_TextBox.Size = new System.Drawing.Size(620, 188);
			this.Message_TextBox.TabIndex = 3;
			// 
			// StackTrace_Label
			// 
			this.StackTrace_Label.AutoSize = true;
			this.StackTrace_Label.Location = new System.Drawing.Point(12, 97);
			this.StackTrace_Label.Name = "StackTrace_Label";
			this.StackTrace_Label.Size = new System.Drawing.Size(60, 13);
			this.StackTrace_Label.TabIndex = 2;
			this.StackTrace_Label.Text = "Подробно:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(570, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(62, 62);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// ExcName_Label
			// 
			this.ExcName_Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ExcName_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ExcName_Label.Location = new System.Drawing.Point(12, 45);
			this.ExcName_Label.Name = "ExcName_Label";
			this.ExcName_Label.Size = new System.Drawing.Size(552, 52);
			this.ExcName_Label.TabIndex = 2;
			this.ExcName_Label.Text = "Необработанное исключение типа {0} было вызвано\n{1}!";
			// 
			// ExceptionHandlerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 364);
			this.ControlBox = false;
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.Message_TextBox);
			this.Controls.Add(this.StackTrace_Label);
			this.Controls.Add(this.ExcName_Label);
			this.Controls.Add(this.Exc_Label);
			this.Controls.Add(this.Terminate_Button);
			this.Controls.Add(this.Continue_Button);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(660, 380);
			this.Name = "ExceptionHandlerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ого, ошибка!";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.Label ExcName_Label;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label StackTrace_Label;
		private System.Windows.Forms.TextBox Message_TextBox;
		private System.Windows.Forms.Label Exc_Label;
		private System.Windows.Forms.Button Terminate_Button;
		private System.Windows.Forms.Button Continue_Button;
	}
}
