namespace Example.PanZoomRotate
{
	partial class Window
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
			this.viewer = new Example.PanZoomRotate.Viewer();
			this.SuspendLayout();
			// 
			// viewer
			// 
			this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewer.Location = new System.Drawing.Point(0, 0);
			this.viewer.Name = "viewer";
			this.viewer.Size = new System.Drawing.Size(784, 562);
			this.viewer.TabIndex = 0;
			// 
			// Window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.viewer);
			this.Name = "Window";
			this.Text = "Vidview Example PanZoomRotate";
			this.ResumeLayout(false);

		}

		#endregion

		private Viewer viewer;
	}
}

