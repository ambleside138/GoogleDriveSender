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
            _Logger.Info("�A�v���P�[�V�����̊J�n");
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
                _Logger.Error(ex, "�s���ȃG���[�B�A�v���P�[�V�������I�����܂��B");
            }

            _Logger.Info("�A�v���P�[�V�����̏I��");

        }

        private static void InitializeLogger()
        {
            var conf = new LoggingConfiguration();
            // �t�@�C���o�͒�`
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

            // �ݒ�𔽉f����
            LogManager.Configuration = conf;
        }



    }
}
