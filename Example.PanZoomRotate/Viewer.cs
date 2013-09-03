using System;
using Kean.Core.Extension;
using Forms = System.Windows.Forms;
using Settings = Kean.Platform.Settings;
using Vidhance = Imint.Vidhance;
using Example.PanZoomRotate.Extension;

namespace Example.PanZoomRotate
{
	public class Viewer : 
		Forms.UserControl
	{
		Imint.Vidview.Viewer vidview;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;

		private System.Windows.Forms.Button panCenter;
		private System.Windows.Forms.Button panLeft;
		private System.Windows.Forms.Button panUp;
		private System.Windows.Forms.Button panDown;
		private System.Windows.Forms.Button panRight;

		private System.Windows.Forms.Button zoomReset;
		private System.Windows.Forms.Button zoomFit;
		private System.Windows.Forms.Button zoomOut;
		private System.Windows.Forms.TrackBar zoomBar;
		private int zoomBarStartValue;
		private System.Windows.Forms.Button zoomIn; 

		private System.Windows.Forms.Button rotateReset;
		private System.Windows.Forms.Button rotateCounterClockwise;
		private System.Windows.Forms.TrackBar rotateBar;
		private System.Windows.Forms.Button rotateClockwise;

		public Viewer()
		{
			this.InitializeComponent();
			this.vidview.Started += () =>
			{
				this.panCenter.Click += (object sender, EventArgs e) =>
				{
					Vidhance.IStabilize stabilizer = this.vidview.Vidhance.Stabilize;
					if (stabilizer.NotNull())
						stabilizer.ResetPan();
					vidview.Viewer.Translation = new Kean.Math.Geometry2D.Single.Size();
				};
				this.panLeft.Click += (object sender, EventArgs e) => this.vidview.Viewer.Translation += new Kean.Math.Geometry2D.Single.Size(-50, 0);
				this.panUp.Click += (object sender, EventArgs e) => this.vidview.Viewer.Translation += new Kean.Math.Geometry2D.Single.Size(0, -50);
				this.panDown.Click += (object sender, EventArgs e) => this.vidview.Viewer.Translation += new Kean.Math.Geometry2D.Single.Size(0, 50);
				this.panRight.Click += (object sender, EventArgs e) => this.vidview.Viewer.Translation += new Kean.Math.Geometry2D.Single.Size(50, 0);
				
				this.zoomReset.Click += (object sender, EventArgs e) =>
				{
					Vidhance.IStabilize stabilizer = this.vidview.Vidhance.Stabilize;
					if (stabilizer.NotNull())
						stabilizer.ResetScale();
					this.vidview.Viewer.Scaling = 1;
				};
				this.zoomFit.Click += (object sender, EventArgs e) => this.vidview.Viewer.Fit();
				this.zoomOut.Click += (object sender, EventArgs e) => this.vidview.Viewer.Scaling *= 0.9091f;
				this.zoomBar.Scroll += (object sender, EventArgs e) => this.vidview.Viewer.Scaling = this.zoomBar.Value < this.zoomBarStartValue ? 1f / ((this.zoomBarStartValue - this.zoomBar.Value) / (float)this.zoomBarStartValue * 4f + 1f) : (this.zoomBar.Value - (float)zoomBarStartValue) / (float)zoomBarStartValue * 4 + 1f;
				Action<float> updateScaling = scaling => this.zoomBar.Invoke(() => this.zoomBar.Value = Kean.Math.Integer.Clamp(Kean.Math.Integer.Round((scaling < 1 ? (1 / scaling - 1) / 4 * -this.zoomBarStartValue : (scaling - 1) / 4 * this.zoomBarStartValue) + this.zoomBarStartValue), this.zoomBar.Minimum, this.zoomBar.Maximum));
				updateScaling(this.vidview.Viewer.Scaling);
				this.vidview.Viewer.ScalingChanged += updateScaling;
				this.zoomIn.Click += (object sender, EventArgs e) => this.vidview.Viewer.Scaling *= 1.1f;

				this.rotateReset.Click += (object sender, EventArgs e) =>
				{
					Vidhance.IStabilize stabilizer = this.vidview.Vidhance.Stabilize;
					if (stabilizer.NotNull())
						stabilizer.ResetRotation();
					this.vidview.Viewer.Rotation = 0;
				};
				this.rotateCounterClockwise.Click += (object sender, EventArgs e) => this.vidview.Viewer.Rotation -= 0.3925f; // -22.5 deg in rad
				this.rotateBar.Scroll += (object sender, EventArgs e) => this.vidview.Viewer.Rotation = this.rotateBar.Value * 2 * Kean.Math.Single.PI / this.rotateBar.Maximum - Kean.Math.Single.PI;
				Action<float> updateRotation = rotation => this.rotateBar.Invoke(() =>
				{
					float value = Kean.Math.Single.MinusPiToPi(rotation) * this.rotateBar.Maximum / 2 / Kean.Math.Single.PI + this.rotateBar.Maximum / 2;
					int v = Kean.Math.Integer.Clamp(Kean.Math.Integer.Convert(value), 0, this.rotateBar.Maximum);
					if (v == 0 || v == this.rotateBar.Maximum)
						this.rotateBar.Value = this.rotateBar.Value > this.rotateBar.Maximum / 2 ? this.rotateBar.Maximum : 0;
					else
						this.rotateBar.Value = v;
				});
				updateRotation(this.vidview.Viewer.Rotation);
				this.vidview.Viewer.RotationChanged += updateRotation;
				this.rotateClockwise.Click += (object sender, EventArgs e) => this.vidview.Viewer.Rotation += 0.3925f; // 22.5 deg in rad

				// When the Vidview viewer is closed force shutdown of the full application.
				this.vidview.Closed += System.Windows.Forms.Application.Exit;
				// When the Vidview viewer is fully initialized open test://photo. In case of errors shut down the viewer and the application.
				if (!(this.vidview.Media != null && this.vidview.Media.Open("test://photo")))
					this.vidview.Close();
			};
		}

		void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
			this.vidview = new Imint.Vidview.Viewer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panCenter = new System.Windows.Forms.Button();
			this.panLeft = new System.Windows.Forms.Button();
			this.panUp = new System.Windows.Forms.Button();
			this.panDown = new System.Windows.Forms.Button();
			this.panRight = new System.Windows.Forms.Button();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.zoomReset = new System.Windows.Forms.Button();
			this.zoomFit = new System.Windows.Forms.Button();
			this.zoomIn = new System.Windows.Forms.Button();
			this.zoomBar = new System.Windows.Forms.TrackBar();
			this.zoomOut = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.rotateReset = new System.Windows.Forms.Button();
			this.rotateCounterClockwise = new System.Windows.Forms.Button();
			this.rotateBar = new System.Windows.Forms.TrackBar();
			this.rotateClockwise = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomBar)).BeginInit();
			this.flowLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rotateBar)).BeginInit();
			this.SuspendLayout();
			// 
			// vidview
			// 
			this.vidview.Asynchronous = Kean.Platform.Settings.Asynchronous.None;
			this.vidview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.vidview.Location = new System.Drawing.Point(3, 47);
			this.vidview.Name = "vidview";
			this.vidview.Size = new System.Drawing.Size(100, 56);
			this.vidview.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.Controls.Add(this.vidview, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 150);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.panCenter);
			this.flowLayoutPanel1.Controls.Add(this.panLeft);
			this.flowLayoutPanel1.Controls.Add(this.panUp);
			this.flowLayoutPanel1.Controls.Add(this.panDown);
			this.flowLayoutPanel1.Controls.Add(this.panRight);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(106, 44);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// PanCenter
			// 
			this.panCenter.Image = ((System.Drawing.Image)(resources.GetObject("PanCenter.Image")));
			this.panCenter.Location = new System.Drawing.Point(3, 3);
			this.panCenter.Name = "PanCenter";
			this.panCenter.Size = new System.Drawing.Size(38, 38);
			this.panCenter.TabIndex = 0;
			this.panCenter.UseVisualStyleBackColor = true;
			// 
			// PanLeft
			// 
			this.panLeft.Image = ((System.Drawing.Image)(resources.GetObject("PanLeft.Image")));
			this.panLeft.Location = new System.Drawing.Point(47, 3);
			this.panLeft.Name = "PanLeft";
			this.panLeft.Size = new System.Drawing.Size(38, 38);
			this.panLeft.TabIndex = 0;
			this.panLeft.UseVisualStyleBackColor = true;
			// 
			// PanUp
			// 
			this.panUp.Image = ((System.Drawing.Image)(resources.GetObject("PanUp.Image")));
			this.panUp.Location = new System.Drawing.Point(3, 47);
			this.panUp.Name = "PanUp";
			this.panUp.Size = new System.Drawing.Size(38, 38);
			this.panUp.TabIndex = 0;
			this.panUp.UseVisualStyleBackColor = true;
			// 
			// PanDown
			// 
			this.panDown.Image = ((System.Drawing.Image)(resources.GetObject("PanDown.Image")));
			this.panDown.Location = new System.Drawing.Point(47, 47);
			this.panDown.Name = "PanDown";
			this.panDown.Size = new System.Drawing.Size(38, 38);
			this.panDown.TabIndex = 0;
			this.panDown.UseVisualStyleBackColor = true;
			// 
			// PanRight
			// 
			this.panRight.Image = ((System.Drawing.Image)(resources.GetObject("PanRight.Image")));
			this.panRight.Location = new System.Drawing.Point(3, 91);
			this.panRight.Name = "PanRight";
			this.panRight.Size = new System.Drawing.Size(38, 38);
			this.panRight.TabIndex = 0;
			this.panRight.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.zoomReset);
			this.flowLayoutPanel2.Controls.Add(this.zoomFit);
			this.flowLayoutPanel2.Controls.Add(this.zoomIn);
			this.flowLayoutPanel2.Controls.Add(this.zoomBar);
			this.flowLayoutPanel2.Controls.Add(this.zoomOut);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(106, 44);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(44, 62);
			this.flowLayoutPanel2.TabIndex = 1;
			// 
			// ZoomReset
			// 
			this.zoomReset.Image = ((System.Drawing.Image)(resources.GetObject("ZoomReset.Image")));
			this.zoomReset.Location = new System.Drawing.Point(3, 3);
			this.zoomReset.Name = "ZoomReset";
			this.zoomReset.Size = new System.Drawing.Size(38, 38);
			this.zoomReset.TabIndex = 0;
			// 
			// ZoomFit
			// 
			this.zoomFit.Image = ((System.Drawing.Image)(resources.GetObject("ZoomFit.Image")));
			this.zoomFit.Location = new System.Drawing.Point(47, 3);
			this.zoomFit.Name = "ZoomFit";
			this.zoomFit.Size = new System.Drawing.Size(38, 38);
			this.zoomFit.TabIndex = 1;
			// 
			// ZoomOut
			// 
			this.zoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut.Image")));
			this.zoomOut.Location = new System.Drawing.Point(186, 3);
			this.zoomOut.Name = "ZoomOut";
			this.zoomOut.Size = new System.Drawing.Size(38, 38);
			this.zoomOut.TabIndex = 3;
			// 
			// ZoomBar
			// 
			this.zoomBar.Location = new System.Drawing.Point(135, 3);
			this.zoomBar.Maximum = 320;
			this.zoomBar.Minimum = 0;
			this.zoomBar.LargeChange = 25;
			this.zoomBar.TickFrequency = 10;
			this.zoomBar.Value = this.zoomBarStartValue = 160;
			this.zoomBar.Name = "ZoomBar";
			this.zoomBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.zoomBar.Size = new System.Drawing.Size(38, 320);
			this.zoomBar.TabIndex = 2;
			// 
			// ZoomIn
			// 
			this.zoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn.Image")));
			this.zoomIn.Location = new System.Drawing.Point(91, 3);
			this.zoomIn.Name = "ZoomIn";
			this.zoomIn.Size = new System.Drawing.Size(38, 38);
			this.zoomIn.TabIndex = 1;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.rotateReset);
			this.flowLayoutPanel3.Controls.Add(this.rotateCounterClockwise);
			this.flowLayoutPanel3.Controls.Add(this.rotateBar);
			this.flowLayoutPanel3.Controls.Add(this.rotateClockwise);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 106);
			this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(106, 44);
			this.flowLayoutPanel3.TabIndex = 1;
			// 
			// RotateReset
			// 
			this.rotateReset.Image = ((System.Drawing.Image)(resources.GetObject("RotateReset.Image")));
			this.rotateReset.Location = new System.Drawing.Point(3, 3);
			this.rotateReset.Name = "RotateReset";
			this.rotateReset.Size = new System.Drawing.Size(38, 38);
			this.rotateReset.TabIndex = 0;
			// 
			// RotateCounterClockwise
			// 
			this.rotateCounterClockwise.Image = ((System.Drawing.Image)(resources.GetObject("RotateCounterClockwise.Image")));
			this.rotateCounterClockwise.Location = new System.Drawing.Point(47, 3);
			this.rotateCounterClockwise.Name = "RotateCounterClockwise";
			this.rotateCounterClockwise.Size = new System.Drawing.Size(38, 38);
			this.rotateCounterClockwise.TabIndex = 1;
			// 
			// RotateBar
			// 
			this.rotateBar.Location = new System.Drawing.Point(3, 47);
			this.rotateBar.Maximum = 160;
			this.rotateBar.Minimum = 0;
			this.rotateBar.LargeChange = 25;
			this.rotateBar.TickFrequency = 10;
			this.rotateBar.Value = 80;
			this.rotateBar.Name = "RotateBar";
			this.rotateBar.Size = new System.Drawing.Size(320, 38);
			this.rotateBar.TabIndex = 2;
			// 
			// RotateClockwise
			// 
			this.rotateClockwise.Image = ((System.Drawing.Image)(resources.GetObject("RotateClockwise.Image")));
			this.rotateClockwise.Location = new System.Drawing.Point(3, 98);
			this.rotateClockwise.Name = "RotateClockwise";
			this.rotateClockwise.Size = new System.Drawing.Size(38, 38);
			this.rotateClockwise.TabIndex = 3;
			// 
			// Viewer
			// 
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "Viewer";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomBar)).EndInit();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rotateBar)).EndInit();
			this.ResumeLayout(false);

		}

	}
}
