using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSender.Entity
{
    public class SenderConfiguration
    {
        public bool NeedZip { get; set; }

        public string DriveDirectoryId { get; set; }
    }
}
