using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleDriveSender
{
    public partial class MainView : Form
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Upload();
        }


        private async void Upload()
        {
            var args = Environment.GetCommandLineArgs();
            if(args.Length < 2)
            {
                return;
            }

            var path = args[1];
            _Logger.Info("path=" + path);
            await new DriveSender().TrySend(path);
        }
    }
}
