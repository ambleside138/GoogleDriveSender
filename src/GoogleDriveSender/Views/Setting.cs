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
        public SenderConfiguration Result => new() { Domain = tbDomain.Text,
            DriveDirectoryId = tbDirectoryId.Text };

        public Setting(SenderConfiguration configuration)
        {
            InitializeComponent();

            tbDomain.Text = configuration.Domain;
            tbDirectoryId.Text = configuration.DriveDirectoryId;
            
        }
    }
}
