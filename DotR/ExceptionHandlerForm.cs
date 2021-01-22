/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 12.04.2015
 * Time: 17:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ODE_Test
{
	/// <summary>
	/// ExceptionHandlerForm - show exception info to user.
	/// </summary>
	public partial class ExceptionHandlerForm : Form
	{
		// HACK: слишком хреновый конструктор
		public ExceptionHandlerForm(Exception exc, string source, string comment, bool aborted, bool terminated)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Continue_Button.Enabled = !aborted;
			Terminate_Button.Enabled = !terminated;
			Exc_Label.Text = string.Format(Exc_Label.Text, exc.GetType(), source);
			ExcName_Label.Text = exc.Message;
			if (!string.IsNullOrEmpty(comment))
			{
				ExcName_Label.Text += Environment.NewLine + comment;
			}
			Message_TextBox.Text = exc.ToString();
			this.DialogResult = DialogResult.Abort;
		}
		
		[Obsolete("Может быть изменен!")]
		public static DialogResult ShowHandlerDialog(Exception exc, string source, bool aborted)
		{
			var handler_form = new ExceptionHandlerForm(exc, source, string.Empty, aborted, false);
			return handler_form.ShowDialog();
		}
		
		[Obsolete("Может быть изменен!")]
		public static DialogResult ShowException(Exception exc, string comment)
		{
			var handler_form = new ExceptionHandlerForm(exc, "некоторый пользовательский код", comment, false, true);
			return handler_form.ShowDialog();
		}
	}
}
