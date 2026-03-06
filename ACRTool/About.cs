/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 5:21 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ACRTool
{
	/// <summary>
	/// Description of About.
	/// </summary>
	public partial class About : Form
	{
		public About()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			linkLabel1.Links.Add(0, "https://auroramusicvietnam.net".Length, "https://auroramusicvietnam.net");
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = e.Link.LinkData.ToString(), UseShellExecute = true });
		}
	}
}
