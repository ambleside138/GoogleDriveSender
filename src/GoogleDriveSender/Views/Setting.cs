using GoogleDriveSender.Entity;
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
    public partial class Setting : Form
    {
        public SenderConfiguration Result => new() { DriveDirectoryId = tbDirectoryId.Text, NeedZip = chkNeedZip.Checked };

        public Setting(SenderConfiguration configuration)
        {
            InitializeComponent();

            tbDirectoryId.Text = configuration.DriveDirectoryId;
            chkNeedZip.Checked = configuration.NeedZip;
        }
    }
}
