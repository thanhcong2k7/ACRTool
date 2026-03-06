/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 05-Mar-26
 * Time: 8:39 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

namespace ACRTool
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public Queue<UserControl1> waiting = new Queue<UserControl1>();
		public UserControl1[] arrQueue = new UserControl1[]{};
		//public List<UserControl1> arrQueue = new List<UserControl1>();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			GlobalVariables.host = GlobalVariables.ini.Read("ACR","host");
			GlobalVariables.accesskey = GlobalVariables.ini.Read("ACR","key");
			GlobalVariables.accesssecret = GlobalVariables.ini.Read("ACR","secret");
			button5.Click += async (sender, e) => {
				try{
					label2.Text = "Processing...";
					while(waiting.Count != 0){
						await waiting.Dequeue().triggerScan();
					}
					label2.Text = "Finished!";
				} catch (Exception er){
					label2.Text = "Error.";
					Console.WriteLine(er.ToString());
				}
			};
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void ColorizeJson(string json)
		{
			var rtb = resBox;
		    rtb.Text = json; // Set the formatted text
		    rtb.SuspendLayout();
		
		    // Regex patterns for JSON syntax
		    var keyRegex = new Regex(@"""\w+""\s*:", RegexOptions.Compiled);
		    var stringRegex = new Regex(@"(?<=:\s*)"".*?""", RegexOptions.Compiled); // Simplified
		    var numBoolNullRegex = new Regex(@"\b(true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)\b", RegexOptions.Compiled);
		
		    // Apply colors to matches
		    ColorSyntax(rtb, keyRegex, Color.Red);
		    ColorSyntax(rtb, stringRegex, Color.Green);
		    ColorSyntax(rtb, numBoolNullRegex, Color.Blue);
		
		    rtb.ResumeLayout();
		}
		private void ColorSyntax(RichTextBox rtb, Regex regex, Color color)
		{
		    foreach (Match match in regex.Matches(rtb.Text))
		    {
		        rtb.Select(match.Index, match.Length);
		        rtb.SelectionColor = color;
		    }
		}
		void Button2Click(object sender, EventArgs e)
		{
			OpenFileDialog f = new OpenFileDialog();
			f.Title = "Choose Audio File";
			f.Multiselect = true;
			f.RestoreDirectory = true;
			f.Filter = "Audio File|*.mp3;*.wav;*.flac|MP3 Files|*.mp3|WAV Files|*.wav|FLAC Files|*.flac|All Files|*.*";
			if (f.ShowDialog() == DialogResult.OK){
				for (int i=0; i<f.FileNames.Length; i++){
					var t = new UserControl1(f.SafeFileNames[i].ToString(), f.FileNames[i]);
					t.Click += (se, ev)=>{
						//resBox.Text = JsonHelper.Beautify(t.result);
						ColorizeJson(JsonHelper.Beautify(t.result));
					};
					waiting.Enqueue(t);
					flowLayoutPanel1.Controls.Add(t);
				}
			}
		}
		void Button1Click(object sender, EventArgs e)
		{
			List<Control> controlsToDispose = new List<Control>();
			foreach (Control control in flowLayoutPanel1.Controls)
			{
			    controlsToDispose.Add(control);
			}
			flowLayoutPanel1.Controls.Clear();
			waiting.Clear();
			foreach (Control control in controlsToDispose)
			{
			    control.Dispose();
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			(new About()).ShowDialog();
		}
		void Button4Click(object sender, EventArgs e)
		{
			(new Configuration()).ShowDialog();
		}
		void Button6Click(object sender, EventArgs e)
		{
			SaveFileDialog f = new SaveFileDialog();
			f.Title = "Save result as ...";
			f.FileName = "result.json";
			f.Filter = "JSON file|*.json|Text file|*.txt|All Files|*.*";
			if (f.ShowDialog() == DialogResult.OK){
				try{
					System.IO.File.WriteAllText(f.FileName, resBox.Text);
				} catch (Exception er){
					MessageBox.Show(er.ToString(), "Unable to save file!");
				}
			}
		}
	}
}
