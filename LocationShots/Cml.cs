using LocationShots.BLL;
using Ookii.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationShots
{
    public partial class Cml : Form
    {
        delegate void SetTextCallback(string text);
        delegate void SetCompletedCallback(string finalMessage);

        private UI myUI = new UI();
        class UI
        {
            public Cml o;
            public string Url { get { return o.txtUrl.Text; } set { o.txtUrl.Text = value; } }

            public string Suburb { get { return o.tpCityPlan.txtSuburb.Text; } set { o.txtSuburb.Text = value; } }
            public string Street { get { return o.txtStreet.Text; } set { o.txtStreet.Text = value; } }
            public string StreetNo { get { return o.txtStreetNo.Text; } set { o.txtStreetNo.Text = value; } }
            public Lookups.Browser Browser { get { return o.rbChrome.Checked ? Lookups.Browser.Chrome : Lookups.Browser.Firefox; } }

            public string OutputFolder { get { return o.txtOutputFolder.Text; } set { o.txtOutputFolder.Text = value; } }
            public string OutputSheetPath { get { return o.txtOutputSheetPath.Text; } set { o.txtOutputSheetPath.Text = value; } }

            public string Result { get { return o.txtResult.Text; } set { o.txtResult.Text = value; } }

            public void SignalError(string msg)
            {
                o.txtResult.ForeColor = Color.Red;
                o.txtResult.Text = msg;
            }
            public void SignalWarning(string msg)
            {
                o.txtResult.ForeColor = Color.Yellow;
                o.txtResult.Text = msg;
            }
            public void SignalSuccess(string msg)
            {
                o.txtResult.ForeColor = Color.Green;
                o.txtResult.Text = msg;
            }
            public void ClearSignals()
            {
                o.txtResult.ForeColor = Color.Black;
                o.txtResult.Text = null;
            }

            public Config BuildConfig()
            {
                Config conf = new Config();
                conf.Inputs = new Config.ConfInputs()
                {
                    Url = Url,
                    Username = Username,
                    Password = Password,
                    Suburb = Suburb,
                    Street = Street,
                    StreetNo = StreetNo,
                    Browser = Browser,
                };
                conf.Outputs = new Config.ConfOutputs()
                {
                    OutputFolder = OutputFolder
                };
                return conf;
            }
        }

        public Cml()
        {
            InitializeComponent();

            //Get screen resolution
            Rectangle res = Screen.PrimaryScreen.Bounds;

            // Calculate location (etc. 1366 Width - form size...)
            this.Location = new Point(res.Width - Size.Width);


            myUI.o = this;
            this.TopMost = true;
        }

        private bool LoadSettings(out string exeVersion)
        {
            exeVersion = "";
            try
            {
                List<String> missingKeys = new List<string>();
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                exeVersion = ConfigurationManager.AppSettings[ConfigKeys.Config.ExeVersion];

                //------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------
                if (config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Inputs.Url))
                    myUI.Url = ConfigurationManager.AppSettings[ConfigKeys.Inputs.Url];
                else
                    missingKeys.Add(ConfigKeys.Inputs.Url);
                //------------------------------------------------------------------------------------------
                if (config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Inputs.Username))
                    myUI.Username = ConfigurationManager.AppSettings[ConfigKeys.Inputs.Username];
                else
                    missingKeys.Add(ConfigKeys.Inputs.Username);
                //------------------------------------------------------------------------------------------
                if (config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Inputs.Password))
                    myUI.Password = ConfigurationManager.AppSettings[ConfigKeys.Inputs.Password];
                else
                    missingKeys.Add(ConfigKeys.Inputs.Password);
                //------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------
                if (config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Outputs.OutputFolder))
                    myUI.OutputFolder = ConfigurationManager.AppSettings[ConfigKeys.Outputs.OutputFolder];
                else
                    missingKeys.Add(ConfigKeys.Outputs.OutputFolder);
                //------------------------------------------------------------------------------------------

                if (missingKeys.Count > 0)
                    throw new ApplicationException(String.Concat(MSG.MissingConfigKeys, String.Join(", ", missingKeys)));
                return true;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                return false;
            }
        }
        private void SaveSettings()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                if (!config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Inputs.Url))
                    config.AppSettings.Settings.Add(ConfigKeys.Inputs.Url, myUI.Url);
                else
                    config.AppSettings.Settings[ConfigKeys.Inputs.Url].Value = myUI.Url;
                //------------------------------------------------------------------------------------------
                if (!config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Inputs.Username))
                    config.AppSettings.Settings.Add(ConfigKeys.Inputs.Username, myUI.Username);
                else
                    config.AppSettings.Settings[ConfigKeys.Inputs.Username].Value = myUI.Username;
                //------------------------------------------------------------------------------------------
                if (!config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Inputs.Password))
                    config.AppSettings.Settings.Add(ConfigKeys.Inputs.Password, myUI.Password);
                else
                    config.AppSettings.Settings[ConfigKeys.Inputs.Password].Value = myUI.Password;
                //------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------
                if (!config.AppSettings.Settings.AllKeys.Contains(ConfigKeys.Outputs.OutputFolder))
                    config.AppSettings.Settings.Add(ConfigKeys.Outputs.OutputFolder, myUI.OutputFolder);
                else
                    config.AppSettings.Settings[ConfigKeys.Outputs.OutputFolder].Value = myUI.OutputFolder;
                //------------------------------------------------------------------------------------------

                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                MessageBox.Show(MSG.UnableToSaveConfig);
            }
        }

        private void DetachEvents()
        {
            Engine.UpdateStatusEvent -= UpdateProgress;
            Engine.MarkCompletedEvent -= MarkCompleted;
        }
        private void AttachEvents()
        {
            Engine.UpdateStatusEvent += UpdateProgress;
            Engine.MarkCompletedEvent += MarkCompleted;
        }

        private void UpdateProgress(String Message)
        {
            if (this.txtResult.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateProgress);
                this.Invoke(d, new object[] { Message });
            }
            else
            {
                this.txtResult.Text = Message;
                XLogger.Info(Message);
            }
        }
        private void MarkCompleted(string finalMessage)
        {
            //String finalMessage = "Completed the Process";

            if (this.txtResult.InvokeRequired)
            {
                SetCompletedCallback d = new SetCompletedCallback(MarkCompleted);
                this.Invoke(d, new Object[] { finalMessage });
            }
            else
            {
                this.txtResult.Text = finalMessage;
            }

            loadingCircle1.Active = false;
            btnStartStop.Text = "START";
            btnStartStop.Enabled = true;
        }
        private void UpdateOutputSheetPath(String thePath)
        {
            if (this.txtOutputSheetPath.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(UpdateOutputSheetPath);
                this.Invoke(d, new object[] { thePath });
            }
            else
            {
                myUI.OutputSheetPath = thePath;
            }
        }

        /// <summary>
        /// <see cref="https://www.codeproject.com/Articles/20627/BackgroundWorker-Threads-and-Supporting-Cancel"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            string err = "";
            if (!ValidateInputs(out err))
            {
                myUI.SignalError(err);
                return;
            }

            if (bgwProcess.IsBusy)
            {
                btnStartStop.Enabled = false;
                btnStartStop.Text = "Cancelling...";

                bgwProcess.CancelAsync();
                Engine.Variables.CancellationPending = true;
            }
            else
            {
                btnStartStop.Text = "STOP";
                loadingCircle1.Active = true;
                Reset();
                bgwProcess.RunWorkerAsync();
            }
        }

        private bool ValidateInputs(out string err)
        {
            err = "";

            if (String.IsNullOrEmpty(err) &&
                    (String.IsNullOrWhiteSpace(myUI.Url) || String.IsNullOrWhiteSpace(myUI.Username) || String.IsNullOrWhiteSpace(myUI.Password) || String.IsNullOrWhiteSpace(myUI.OutputFolder)))
                err = "Please fill all the inputs";

            if (String.IsNullOrEmpty(err) && !HtmlAgility.UrlIsValid(myUI.Url))
                err = "The Url is invalid or down, please enter a valid Url";


            return String.IsNullOrEmpty(err);
        }
        private void Reset()
        {
            myUI.ClearSignals();
            Engine.Reset();
        }
        private async Task DoProcess()
        {
            try
            {
                Engine.Variables.ExecutionTime.Start();
                Stopwatch timer = Stopwatch.StartNew();
                XLogger.Info("BEGIN:\t Task Execution");

                Engine.Variables.OutputSheetPath = Path.Combine(Engine.Config.Outputs.OutputFolder, Engine.Variables.OutputSheetPath);
                UpdateOutputSheetPath(Engine.Variables.OutputSheetPath);
                Engine.Config = myUI.BuildConfig();

                if (rbCityPlan.Checked)
                {
                    if (!Engine.DoTaskCml())
                        throw new ApplicationException("Some error occurred");
                }
                else if (rbRedland.Checked)
                {
                    if (!Engine.DoTaskRedland())
                        throw new ApplicationException("Some error occurred");
                }
                else if (rbTakeScreenshot.Checked)
                {
                    if (!Engine.DoTaskScreenshot())
                        throw new ApplicationException("Some error occurred");
                }

                //MarkCompleted("done.please check results");
                var elapsed = timer.Elapsed.ToStandardElapsedFormat();
                XLogger.Info($"END:{elapsed}\t Task Execution");

            }
            catch (Exception x)
            {
                if (x is ApplicationException)
                    MarkCompleted(x.Message);
                else
                    MarkCompleted(MSG.OperationFailed);

                XLogger.Error(x);
            }
        }

        private async void bgwProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    Engine.Variables.CancellationPending = true;
                }
                else
                {
                    AttachEvents();
                    await DoProcess();
                    //DetachEvents();
                }
            }
            catch (Exception x)
            {
                XLogger.Error(x);
                e.Cancel = true;
                MarkCompleted(MSG.OperationFailed);
            }
        }
        private void bgwProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        /*
        private void btnOuputSheetPath_Click(object sender, EventArgs e)
        {
            try
            {
                var dlgSaveResults = new SaveFileDialog()
                //var dlgSaveResults = new Ookii.Dialogs.VistaSaveFileDialog()
                {
                    Filter = "|*.xlsx",
                    InitialDirectory = Environment.SpecialFolder.MyComputer.ToString(),
                    Title = "Please input the path for the results workbook",
                    AddExtension = true,
                    DefaultExt = ".xlsx",
                    ValidateNames = true,
                    CheckPathExists = true,
                };

                if (dlgSaveResults.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    myUI.OuputSheetPath = dlgSaveResults.FileName;
            }
            catch (Exception x)
            {
                XLogger.Error(x);
            }

        }
        */
        private void Cml_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
        private void Cml_Shown(object sender, EventArgs e)
        {
            string exeVersion = "";
            if (!LoadSettings(out exeVersion))
                MessageBox.Show(@"Unable to read app settings. Please check the log file");

            Engine.Config = myUI.BuildConfig();
            Engine.Config.ExeVersion = exeVersion;
            Engine.Variables = new ExecutionVariables();
            Engine.Variables.BeginTimestamp = DateTime.Now;
            Engine.Variables.ActiveTest = "DefaultTest";
            Engine.Variables.OutputSheetPath =
                $"tm_automation_output_{Engine.Variables.ActiveTest}_{Engine.Variables.FilenameTimestamp}.xlsx";

            XLogger.Application = String.Format("LocationShots||{0}||{1}", Engine.Config.ExeVersion, Engine.Variables.ExecutionTimestamp);
            this.Text = "LocationShots||" + Engine.Config.ExeVersion;
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var dlgOpenFolder = new VistaFolderBrowserDialog()
                {
                    RootFolder = Environment.SpecialFolder.MyComputer,
                    Description = "Please input the folder where the results workbook will be placed",
                    UseDescriptionForTitle = true,
                };

                if (dlgOpenFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!Directory.Exists(dlgOpenFolder.SelectedPath))
                        MessageBox.Show(MSG.InvalidFolderPath);
                    else
                        myUI.OutputFolder = dlgOpenFolder.SelectedPath;
                }
            }
            catch (Exception x)
            {
                XLogger.Error(x);
            }
        }
        private void rbBrowser_CheckedChanged(object sender, EventArgs e)
        {
            Engine.Config.Inputs.Browser = myUI.Browser;
        }

        private void btnTakeScreenshot_Click(object sender, EventArgs e)
        {

        }
    }
}
