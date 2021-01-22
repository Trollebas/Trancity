/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 22.09.2013
 * Time: 12:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Engine;

namespace Common
{
	/// <summary>
	/// Попытка придумать свой контрол для ввода чисел
	/// </summary>
	public class NumericBox : TextBox
	{
		public event EventHandler EnterPressed;
		
		public NumericBox() : base()
		{
			this.KeyPress += new KeyPressEventHandler(NumericBox_KeyPress);
		}
	
		private void NumericBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '.') && !(sender as TextBox).Text.Contains("."))
                e.KeyChar = '.';
            else if (!(Char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
            if ((e.KeyChar == (char)13) && (EnterPressed != null))
            	EnterPressed(sender, null);
		}
		
		public double Value
		{
			get
			{
				try
				{
					return double.Parse(base.Text, Xml.DoubleFormat);
				}
				catch
				{
					Logger.Log("NumericBox", "Convert string -> double failed!");
					base.Text = "0";
					return 0.0;
				}
			}
			set
			{
				base.Text = value.ToString("#0.#");
			}
		}
	}
}
