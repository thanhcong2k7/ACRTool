/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 5:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ACRTool
{
	/// <summary>
	/// Description of Configuration.
	/// </summary>
	public partial class Configuration : Form
	{
		public Configuration()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			textBox1.Text = GlobalVariables.host;
			textBox2.Text = GlobalVariables.accesskey;
			textBox3.Text = GlobalVariables.accesssecret;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			GlobalVariables.host = textBox1.Text;
			GlobalVariables.accesskey = textBox2.Text;
			GlobalVariables.accesssecret = textBox3.Text;
			GlobalVariables.ini.Write("ACR","host",GlobalVariables.host);
			GlobalVariables.ini.Write("ACR","key",GlobalVariables.accesskey);
			GlobalVariables.ini.Write("ACR","secret",GlobalVariables.accesssecret);
		}
	}
}
