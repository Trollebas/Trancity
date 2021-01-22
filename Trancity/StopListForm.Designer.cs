namespace Trancity
{
    partial class StopListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.checkedListBox = new System.Windows.Forms.CheckedListBox();
        	this.Update_Button = new System.Windows.Forms.Button();
        	this.Clear_Button = new System.Windows.Forms.Button();
        	this.SuspendLayout();
        	// 
        	// checkedListBox
        	// 
        	this.checkedListBox.FormattingEnabled = true;
        	this.checkedListBox.Location = new System.Drawing.Point(11, 11);
        	this.checkedListBox.Name = "checkedListBox";
        	this.checkedListBox.Size = new System.Drawing.Size(272, 379);
        	this.checkedListBox.TabIndex = 0;
        	// 
        	// Update_Button
        	// 
        	this.Update_Button.Location = new System.Drawing.Point(12, 397);
        	this.Update_Button.Name = "Update_Button";
        	this.Update_Button.Size = new System.Drawing.Size(136, 23);
        	this.Update_Button.TabIndex = 1;
        	this.Update_Button.Text = "Обновить";
        	this.Update_Button.UseVisualStyleBackColor = true;
        	this.Update_Button.Click += new System.EventHandler(this.UpdateClick);
        	// 
        	// Clear_Button
        	// 
        	this.Clear_Button.Location = new System.Drawing.Point(147, 397);
        	this.Clear_Button.Name = "Clear_Button";
        	this.Clear_Button.Size = new System.Drawing.Size(136, 23);
        	this.Clear_Button.TabIndex = 1;
        	this.Clear_Button.Text = "Очистить";
        	this.Clear_Button.UseVisualStyleBackColor = true;
        	this.Clear_Button.Click += new System.EventHandler(this.ClearClick);
        	// 
        	// StopListForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(294, 425);
        	this.Controls.Add(this.Clear_Button);
        	this.Controls.Add(this.Update_Button);
        	this.Controls.Add(this.checkedListBox);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "StopListForm";
        	this.Padding = new System.Windows.Forms.Padding(8);
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "Остановки";
        	this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StopListForm_FormClosed);
        	this.Load += new System.EventHandler(this.StopListForm_Load);
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.Button Update_Button;
        private System.Windows.Forms.Button Clear_Button;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        
        #endregion

    }
}