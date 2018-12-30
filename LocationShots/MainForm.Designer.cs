namespace LocationShots
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcSites = new System.Windows.Forms.TabControl();
            this.tpCityPlan = new System.Windows.Forms.TabPage();
            this.cityPlan1 = new LocationShots.Sites.CityPlan();
            this.tpRedland = new System.Windows.Forms.TabPage();
            this.redland1 = new LocationShots.Sites.Redland();
            this.rbFirefox = new System.Windows.Forms.RadioButton();
            this.rbChrome = new System.Windows.Forms.RadioButton();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.bgwProcess = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.loadingCircle1 = new GetBusiness.LoadingCircle();
            this.rbCityPlan = new System.Windows.Forms.RadioButton();
            this.rbRedland = new System.Windows.Forms.RadioButton();
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
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(639, 208);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inputs";
            // 
            // tcSites
            // 
            this.tcSites.Controls.Add(this.tpCityPlan);
            this.tcSites.Controls.Add(this.tpRedland);
            this.tcSites.Location = new System.Drawing.Point(15, 57);
            this.tcSites.Name = "tcSites";
            this.tcSites.SelectedIndex = 0;
            this.tcSites.Size = new System.Drawing.Size(599, 145);
            this.tcSites.TabIndex = 17;
            // 
            // tpCityPlan
            // 
            this.tpCityPlan.Controls.Add(this.cityPlan1);
            this.tpCityPlan.Location = new System.Drawing.Point(4, 22);
            this.tpCityPlan.Name = "tpCityPlan";
            this.tpCityPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tpCityPlan.Size = new System.Drawing.Size(591, 119);
            this.tpCityPlan.TabIndex = 0;
            this.tpCityPlan.Text = "CityPlan";
            this.tpCityPlan.UseVisualStyleBackColor = true;
            // 
            // cityPlan1
            // 
            this.cityPlan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cityPlan1.Location = new System.Drawing.Point(3, 3);
            this.cityPlan1.Name = "cityPlan1";
            this.cityPlan1.Size = new System.Drawing.Size(585, 113);
            this.cityPlan1.Street = "Osterley Road";
            this.cityPlan1.StreetNo = "45";
            this.cityPlan1.Suburb = "YERONGA";
            this.cityPlan1.TabIndex = 0;
            // 
            // tpRedland
            // 
            this.tpRedland.Controls.Add(this.redland1);
            this.tpRedland.Location = new System.Drawing.Point(4, 22);
            this.tpRedland.Name = "tpRedland";
            this.tpRedland.Padding = new System.Windows.Forms.Padding(3);
            this.tpRedland.Size = new System.Drawing.Size(591, 119);
            this.tpRedland.TabIndex = 1;
            this.tpRedland.Text = "Redland";
            // 
            // redland1
            // 
            this.redland1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redland1.HouseNo = "";
            this.redland1.Location = new System.Drawing.Point(3, 3);
            this.redland1.Name = "redland1";
            this.redland1.Size = new System.Drawing.Size(585, 113);
            this.redland1.StreetName = "";
            this.redland1.TabIndex = 0;
            // 
            // rbFirefox
            // 
            this.rbFirefox.AutoSize = true;
            this.rbFirefox.Location = new System.Drawing.Point(555, 10);
            this.rbFirefox.Margin = new System.Windows.Forms.Padding(2);
            this.rbFirefox.Name = "rbFirefox";
            this.rbFirefox.Size = new System.Drawing.Size(56, 17);
            this.rbFirefox.TabIndex = 10;
            this.rbFirefox.Text = "Firefox";
            this.rbFirefox.UseVisualStyleBackColor = true;
            this.rbFirefox.CheckedChanged += new System.EventHandler(this.rbBrowser_CheckedChanged);
            // 
            // rbChrome
            // 
            this.rbChrome.AutoSize = true;
            this.rbChrome.Checked = true;
            this.rbChrome.Location = new System.Drawing.Point(484, 10);
            this.rbChrome.Margin = new System.Windows.Forms.Padding(2);
            this.rbChrome.Name = "rbChrome";
            this.rbChrome.Size = new System.Drawing.Size(61, 17);
            this.rbChrome.TabIndex = 9;
            this.rbChrome.TabStop = true;
            this.rbChrome.Text = "Chrome";
            this.rbChrome.UseVisualStyleBackColor = true;
            this.rbChrome.CheckedChanged += new System.EventHandler(this.rbBrowser_CheckedChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(83, 32);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(528, 20);
            this.txtUrl.TabIndex = 8;
            this.txtUrl.Text = "http://cityplan2014maps.brisbane.qld.gov.au/CityPlan/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Url:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOutputFolder);
            this.groupBox2.Controls.Add(this.txtOutputFolder);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(16, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(639, 62);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // btnOutputFolder
            // 
            this.btnOutputFolder.Location = new System.Drawing.Point(558, 28);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(53, 20);
            this.btnOutputFolder.TabIndex = 12;
            this.btnOutputFolder.Text = "Browse";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(85, 28);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(458, 20);
            this.txtOutputFolder.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output Folder:";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(499, 295);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(156, 28);
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
            this.groupBox3.Location = new System.Drawing.Point(17, 330);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(638, 79);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Results";
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(3, 16);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(632, 60);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(461, 295);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(31, 28);
            this.loadingCircle1.SpokeCount = 12;
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.TabIndex = 11;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // rbCityPlan
            // 
            this.rbCityPlan.AutoSize = true;
            this.rbCityPlan.Location = new System.Drawing.Point(114, 301);
            this.rbCityPlan.Name = "rbCityPlan";
            this.rbCityPlan.Size = new System.Drawing.Size(63, 17);
            this.rbCityPlan.TabIndex = 24;
            this.rbCityPlan.Text = "CityPlan";
            this.rbCityPlan.UseVisualStyleBackColor = true;
            // 
            // rbRedland
            // 
            this.rbRedland.AutoSize = true;
            this.rbRedland.Checked = true;
            this.rbRedland.Location = new System.Drawing.Point(31, 301);
            this.rbRedland.Name = "rbRedland";
            this.rbRedland.Size = new System.Drawing.Size(65, 17);
            this.rbRedland.TabIndex = 25;
            this.rbRedland.TabStop = true;
            this.rbRedland.Text = "Redland";
            this.rbRedland.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 415);
            this.Controls.Add(this.rbRedland);
            this.Controls.Add(this.rbCityPlan);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LocationShots";
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
        private System.Windows.Forms.RadioButton rbCityPlan;
        private System.Windows.Forms.RadioButton rbRedland;
        private System.Windows.Forms.TabControl tcSites;
        private System.Windows.Forms.TabPage tpCityPlan;
        private System.Windows.Forms.TabPage tpRedland;
        private Sites.CityPlan cityPlan1;
        private Sites.Redland redland1;
    }
}