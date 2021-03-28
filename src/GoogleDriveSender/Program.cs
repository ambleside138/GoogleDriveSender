using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Windows.Forms;

namespace GoogleDriveSender
{
    static class Program
    {

        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeLogger();
            _Logger.Info("アプリケーションの開始");
            _Logger.Info("arguments: " + Environment.CommandLine);

            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainView());
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, "不明なエラー。アプリケーションを終了します。");
            }

            _Logger.Info("アプリケーションの終了");

        }

        private static void InitializeLogger()
        {
            var conf = new LoggingConfiguration();
            // ファイル出力定義
            var file = new FileTarget("logfile")
            {
                Encoding = System.Text.Encoding.UTF8,
                Layout = "${longdate} [${threadid:padding=2}] [${uppercase:${level:padding=-5}}] ${callsite}() - ${message}${exception:format=ToString}",
                FileName = "${basedir}/logs/TimeRecorder_${date:format=yyyyMMdd}.log",
                ArchiveNumbering = ArchiveNumberingMode.Date,
                ArchiveFileName = "${basedir}/logs/GoogleDriveSender.log.{#}",
                ArchiveEvery = FileArchivePeriod.None,
                MaxArchiveFiles = 10
            };
            conf.AddTarget(file);

            conf.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, file));

            // 設定を反映する
            LogManager.Configuration = conf;
        }



    }
}
