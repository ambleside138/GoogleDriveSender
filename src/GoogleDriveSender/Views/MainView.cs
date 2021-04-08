using GoogleDriveSender.Entity;
using GoogleDriveSender.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleDriveSender
{
    public partial class MainView : Form
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        private BackgroundWorker _Worker = new() { WorkerReportsProgress = true };

        private string _ConfigurationFileName = "configuration.json";

        public MainView()
        {
            InitializeComponent();
            SetEvents();
        }

        private void SetEvents()
        {
            _Worker.DoWork += _Worker_DoWork;
            _Worker.ProgressChanged += _Worker_ProgressChanged;
            _Worker.RunWorkerCompleted += _Worker_RunWorkerCompleted;

            btnCopy.Click += (_, __) =>
            {
                if (string.IsNullOrEmpty(tbPath.Text) == false)
                    Clipboard.SetText(tbPath.Text);
            };

            btnClose.Click += (_, __) => Close();
            btnSetting.Click += BtnSetting_Click;
            btnSend.Click += (_, __) => Upload();
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            using(var dig = new Setting(GetConfiguration()))
            {
                if (dig.ShowDialog(this) == DialogResult.OK)
                {
                    JsonFileIO.Serialize(dig.Result, _ConfigurationFileName);
                }
            }
        }

        private void _Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbStatus.Text = e.UserState?.ToString();
        }

        private void _Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlArgs.Enabled = true;
            progress.Style = ProgressBarStyle.Continuous;

            var result = (ProcessResult)e.Result;
            if(result.HasError)
            {
                _Logger.Info("▲ アップロード完了（エラーあり）" + result.Message);
                
                lbStatus.Text = result.Message;
                lbStatus.ForeColor = ColorTranslator.FromHtml("#DD2C00");

                progress.Value = 0;
                return;
            }
            else
            {
                _Logger.Info("▲ アップロード完了");

            }

            progress.Value = 100;

            tbPath.Text = result.SharePath;
            lbStatus.Text = "送信完了.";

            Clipboard.SetText(result.SharePath);
        }

        private void _Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var path = e.Argument?.ToString();
            if(string.IsNullOrEmpty(path))
            {
                e.Result = ProcessResult.Error($"パスが未指定です [{path}]");
                return;
            }

            var isFile = File.Exists(path);
            _Logger.Info("path=" + path);
            if (Directory.Exists(path) == false
                && isFile == false)
            {
                e.Result = ProcessResult.Error($"パスが不正です [{path}]");
                return;
            }

            var config = GetConfiguration();
            if (string.IsNullOrEmpty(config.DriveDirectoryId))
            {
                e.Result = ProcessResult.Error("送信先が指定されていません");
                return;
            }

            // 1. 圧縮
            _Worker.ReportProgress(0, "ZIP圧縮中...");
            if(isFile == false)
            {
                path = ZipCompressor.Run(path);
            }

            // 2. 送信＆共有
            _Worker.ReportProgress(0, "GoogleDrive送信中...");
            var result = new DriveSender(config).TrySend(path);

            e.Result = result;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            lbStatus.Text = "";

            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
            {
                return;
            }

            tbResourcePath.Text = args[1];

            Upload();
        }


        private void Upload()
        {
            _Logger.Info("▼ アップロード開始");

            pnlArgs.Enabled = false;
            progress.Style = ProgressBarStyle.Marquee;
            lbStatus.ForeColor = ColorTranslator.FromHtml("#212121");
            tbPath.Text = "";
            Clipboard.Clear();

            _Worker.RunWorkerAsync(tbResourcePath.Text);
        }

        private SenderConfiguration GetConfiguration()
        {
            var config = JsonFileIO.Deserialize<SenderConfiguration>(_ConfigurationFileName);
            return config ?? new SenderConfiguration();
        }
    }
}
