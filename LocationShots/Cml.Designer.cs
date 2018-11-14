﻿namespace LocationShots
{
    partial class Cml
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
            LocationShots.BLL.Config.ConfInputs.CityPlan cityPlan3 = new LocationShots.BLL.Config.ConfInputs.CityPlan();
            LocationShots.BLL.Config.ConfInputs.Redland redland2 = new LocationShots.BLL.Config.ConfInputs.Redland();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcSites = new System.Windows.Forms.TabControl();
            this.tpCityPlan = new System.Windows.Forms.TabPage();
            this.tpRedland = new System.Windows.Forms.TabPage();
            this.rbFirefox = new System.Windows.Forms.RadioButton();
            this.rbChrome = new System.Windows.Forms.RadioButton();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOutputSheetPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.bgwProcess = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.loadingCircle1 = new GetBusiness.LoadingCircle();
            this.rbTakeScreenshot = new System.Windows.Forms.RadioButton();
            this.rbCityPlan = new System.Windows.Forms.RadioButton();
            this.rbRedland = new System.Windows.Forms.RadioButton();
            this.cityPlan1 = new LocationShots.Sites.CityPlan();
            this.redland1 = new LocationShots.Sites.Redland();
            this.groupBox1.SuspendLayout();
            this.tcSites.SuspendLayout();
            this.tpCityPlan.SuspendLayout();
            this.tpRedland.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tcSites);
            this.groupBox1.Controls.Add(this.rbFirefox);
            this.groupBox1.Controls.Add(this.rbChrome);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(20, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(958, 320);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inputs";
            // 
            // tcSites
            // 
            this.tcSites.Controls.Add(this.tpCityPlan);
            this.tcSites.Controls.Add(this.tpRedland);
            this.tcSites.Location = new System.Drawing.Point(22, 88);
            this.tcSites.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tcSites.Name = "tcSites";
            this.tcSites.SelectedIndex = 0;
            this.tcSites.Size = new System.Drawing.Size(898, 223);
            this.tcSites.TabIndex = 17;
            // 
            // tpCityPlan
            // 
            this.tpCityPlan.Controls.Add(this.cityPlan1);
            this.tpCityPlan.Location = new System.Drawing.Point(4, 29);
            this.tpCityPlan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpCityPlan.Name = "tpCityPlan";
            this.tpCityPlan.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpCityPlan.Size = new System.Drawing.Size(890, 190);
            this.tpCityPlan.TabIndex = 0;
            this.tpCityPlan.Text = "CityPlan";
            this.tpCityPlan.UseVisualStyleBackColor = true;
            // 
            // tpRedland
            // 
            this.tpRedland.Controls.Add(this.redland1);
            this.tpRedland.Location = new System.Drawing.Point(4, 29);
            this.tpRedland.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpRedland.Name = "tpRedland";
            this.tpRedland.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpRedland.Size = new System.Drawing.Size(890, 190);
            this.tpRedland.TabIndex = 1;
            this.tpRedland.Text = "Redland";
            // 
            // rbFirefox
            // 
            this.rbFirefox.AutoSize = true;
            this.rbFirefox.Location = new System.Drawing.Point(832, 15);
            this.rbFirefox.Name = "rbFirefox";
            this.rbFirefox.Size = new System.Drawing.Size(82, 24);
            this.rbFirefox.TabIndex = 10;
            this.rbFirefox.Text = "Firefox";
            this.rbFirefox.UseVisualStyleBackColor = true;
            this.rbFirefox.CheckedChanged += new System.EventHandler(this.rbBrowser_CheckedChanged);
            // 
            // rbChrome
            // 
            this.rbChrome.AutoSize = true;
            this.rbChrome.Checked = true;
            this.rbChrome.Location = new System.Drawing.Point(726, 15);
            this.rbChrome.Name = "rbChrome";
            this.rbChrome.Size = new System.Drawing.Size(90, 24);
            this.rbChrome.TabIndex = 9;
            this.rbChrome.TabStop = true;
            this.rbChrome.Text = "Chrome";
            this.rbChrome.UseVisualStyleBackColor = true;
            this.rbChrome.CheckedChanged += new System.EventHandler(this.rbBrowser_CheckedChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(124, 49);
            this.txtUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(790, 26);
            this.txtUrl.TabIndex = 8;
            this.txtUrl.Text = "http://cityplan2014maps.brisbane.qld.gov.au/CityPlan/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Url:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOutputFolder);
            this.groupBox2.Controls.Add(this.txtOutputFolder);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtOutputSheetPath);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(24, 348);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(958, 143);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            this.groupBox2.Visible = false;
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(837, 43);
            this.btnOutputFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(80, 31);
            this.btnOutputFolder.TabIndex = 12;
            this.btnOutputFolder.Text = "Browse";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(128, 43);
            this.txtOutputFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(685, 26);
            this.txtOutputFolder.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output Folder:";
            // 
            // txtOutputSheetPath
            // 
            this.txtOutputSheetPath.Location = new System.Drawing.Point(128, 89);
            this.txtOutputSheetPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOutputSheetPath.Name = "txtOutputSheetPath";
            this.txtOutputSheetPath.ReadOnly = true;
            this.txtOutputSheetPath.Size = new System.Drawing.Size(787, 26);
            this.txtOutputSheetPath.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sheet Path:";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(744, 518);
            this.btnStartStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(234, 43);
            this.btnStartStop.TabIndex = 10;
            this.btnStartStop.Text = "START";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // bgwProcess
            // 
            this.bgwProcess.WorkerReportsProgress = true;
            this.bgwProcess.WorkerSupportsCancellation = true;
            this.bgwProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcess_DoWork);
            this.bgwProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProcess_RunWorkerCompleted);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtResult);
            this.groupBox3.Location = new System.Drawing.Point(20, 588);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(957, 122);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Results";
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(4, 24);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(949, 93);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(688, 518);
            this.loadingCircle1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(46, 43);
            this.loadingCircle1.SpokeCount = 12;
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.TabIndex = 11;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // rbTakeScreenshot
            // 
            this.rbTakeScreenshot.AutoSize = true;
            this.rbTakeScreenshot.Checked = true;
            this.rbTakeScreenshot.Location = new System.Drawing.Point(420, 552);
            this.rbTakeScreenshot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTakeScreenshot.Name = "rbTakeScreenshot";
            this.rbTakeScreenshot.Size = new System.Drawing.Size(155, 24);
            this.rbTakeScreenshot.TabIndex = 23;
            this.rbTakeScreenshot.TabStop = true;
            this.rbTakeScreenshot.Text = "Take Screenshot";
            this.rbTakeScreenshot.UseVisualStyleBackColor = true;
            // 
            // rbCityPlan
            // 
            this.rbCityPlan.AutoSize = true;
            this.rbCityPlan.Location = new System.Drawing.Point(420, 517);
            this.rbCityPlan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbCityPlan.Name = "rbCityPlan";
            this.rbCityPlan.Size = new System.Drawing.Size(91, 24);
            this.rbCityPlan.TabIndex = 24;
            this.rbCityPlan.Text = "CityPlan";
            this.rbCityPlan.UseVisualStyleBackColor = true;
            // 
            // rbRedland
            // 
            this.rbRedland.AutoSize = true;
            this.rbRedland.Location = new System.Drawing.Point(284, 517);
            this.rbRedland.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbRedland.Name = "rbRedland";
            this.rbRedland.Size = new System.Drawing.Size(94, 24);
            this.rbRedland.TabIndex = 25;
            this.rbRedland.Text = "Redland";
            this.rbRedland.UseVisualStyleBackColor = true;
            // 
            // cityPlan1
            // 
            this.cityPlan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cityPlan1.Location = new System.Drawing.Point(4, 5);
            this.cityPlan1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cityPlan1.Name = "cityPlan1";
            this.cityPlan1.Size = new System.Drawing.Size(882, 180);
            this.cityPlan1.Street = "Osterley Road";
            this.cityPlan1.StreetNo = "45";
            this.cityPlan1.Suburb = "YERONGA";
            this.cityPlan1.TabIndex = 0;
            // 
            // redland1
            // 
            this.redland1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redland1.HouseNo = "";
            redland2.HouseNo = "";
            redland2.StreetName = "";
            redland2.UnitNo = null;
            this.redland1.Location = new System.Drawing.Point(4, 5);
            this.redland1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.redland1.Name = "redland1";
            this.redland1.Size = new System.Drawing.Size(882, 180);
            this.redland1.StreetName = "";
            this.redland1.TabIndex = 0;
            // 
            // Cml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 738);
            this.Controls.Add(this.rbRedland);
            this.Controls.Add(this.rbCityPlan);
            this.Controls.Add(this.rbTakeScreenshot);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Cml";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cml";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Cml_FormClosing);
            this.Shown += new System.EventHandler(this.Cml_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcSites.ResumeLayout(false);
            this.tpCityPlan.ResumeLayout(false);
            this.tpRedland.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtOutputSheetPath;
        private System.Windows.Forms.Label label4;
        private GetBusiness.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Button btnStartStop;
        private System.ComponentModel.BackgroundWorker bgwProcess;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.RadioButton rbFirefox;
        private System.Windows.Forms.RadioButton rbChrome;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.RadioButton rbTakeScreenshot;
        private System.Windows.Forms.RadioButton rbCityPlan;
        private System.Windows.Forms.RadioButton rbRedland;
        private System.Windows.Forms.TabControl tcSites;
        private System.Windows.Forms.TabPage tpCityPlan;
        private System.Windows.Forms.TabPage tpRedland;
        private Sites.CityPlan cityPlan1;
        private Sites.Redland redland1;
    }
}