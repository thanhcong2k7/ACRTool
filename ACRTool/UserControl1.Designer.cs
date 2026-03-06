/*
 * Created by SharpDevelop.
 * User: ADMINN
 * Date: 06-Mar-26
 * Time: 12:35 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ACRTool
{
	partial class UserControl1
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label filename;
		private System.Windows.Forms.Label fileDir;
		private System.Windows.Forms.Panel panel1;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.filename = new System.Windows.Forms.Label();
			this.fileDir = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// filename
			// 
			this.filename.Cursor = System.Windows.Forms.Cursors.Hand;
			this.filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.filename.Location = new System.Drawing.Point(21, 2);
			this.filename.Name = "filename";
			this.filename.Size = new System.Drawing.Size(262, 20);
			this.filename.TabIndex = 0;
			this.filename.Text = "Default File Name.wav";
			this.filename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.filename.Click += new System.EventHandler(this.FilenameClick);
			// 
			// fileDir
			// 
			this.fileDir.Cursor = System.Windows.Forms.Cursors.Hand;
			this.fileDir.Location = new System.Drawing.Point(21, 22);
			this.fileDir.Name = "fileDir";
			this.fileDir.Size = new System.Drawing.Size(262, 22);
			this.fileDir.TabIndex = 1;
			this.fileDir.Text = "Default Dir/Dir";
			this.fileDir.Click += new System.EventHandler(this.FileDirClick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Red;
			this.panel1.Location = new System.Drawing.Point(11, 20);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(4, 4);
			this.panel1.TabIndex = 2;
			// 
			// UserControl1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.fileDir);
			this.Controls.Add(this.filename);
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(286, 44);
			this.ResumeLayout(false);

		}
	}
}
