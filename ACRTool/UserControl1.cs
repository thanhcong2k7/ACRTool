/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 12:35 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACRTool
{
	/// <summary>
	/// Description of UserControl1.
	/// </summary>
	public partial class UserControl1 : UserControl
	{
		public event EventHandler Click;
		public string res = "{}"; //default json string
		private string orgF = "";
		public async Task triggerScan(){
			string scanResult = await Task.Run(() => new AcrCloudIdentify(orgF).Scan());
			res = scanResult;
            panel1.BackColor = Color.Green;
		}
		public UserControl1(string title, string directory)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			filename.Text = title;
			fileDir.Text = Path.GetDirectoryName(directory);
			orgF = directory;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		protected override void OnClick(EventArgs e)
	    {
	        base.OnClick(e); // Call the base class's OnClick
	        // Check for subscribers and invoke the event
	        Click.Invoke(this, e);
	    }
		public string result{
			get{
				return res;
			}
			set{
				res = value;
			}
		}
		void FilenameClick(object sender, EventArgs e)
		{
			Click.Invoke(this, e);
		}
		void FileDirClick(object sender, EventArgs e)
		{
			Click.Invoke(this, e);
		}
	}
}
