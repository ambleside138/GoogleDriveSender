using GoogleDriveSender.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            btnCopy.Click += (_, __) => Clipboard.SetText(tbPath.Text);
            btnClose.Click += (_, __) => Close();
        }

        private void _Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lbStatus.Text = e.UserState?.ToString();
        }

        private void _Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (ProcessResult)e.Result;
            if(result.HasError)
            {
                lbStatus.Text = result.Message;
                lbStatus.ForeColor = ColorTranslator.FromHtml("#DD2C00");

                progress.Style = ProgressBarStyle.Continuous;
                progress.Value = 0;
                return;
            }

            progress.Style = ProgressBarStyle.Continuous;
            progress.Value = 100;

            tbPath.Text = result.SharePath;
            lbStatus.Text = "送信完了.";

            Clipboard.SetText(result.SharePath);
        }

        private void _Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Upload(e);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            _Worker.RunWorkerAsync();
        }


        private void Upload(DoWorkEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
            {
                e.Result = ProcessResult.Error("引数不正");
                return;
            }

            var path = args[1];
            _Logger.Info("path=" + path);
            if(Directory.Exists(path) == false
                && File.Exists(path) == false)
            {
                e.Result = ProcessResult.Error($"パスが不正です [{path}]");
                return;
            }

            // 1. 圧縮
            _Worker.ReportProgress(0, "ZIP圧縮中...");
            var zipPath = ZipCompressor.Run(path);

            // 2. 送信＆共有
            _Worker.ReportProgress(0, "GoogleDrive送信中...");
            var result = new DriveSender().TrySend(zipPath);

            e.Result = result;
        }
    }
}
