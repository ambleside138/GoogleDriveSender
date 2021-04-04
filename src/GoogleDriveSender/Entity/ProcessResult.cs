using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSender
{
    class ProcessResult
    {
        public bool HasError { get; set; }

        public string Message { get; set; }

        public string SharePath { get; set; }

        public static ProcessResult Error(string mes)
        {
            return new ProcessResult
            {
                HasError = true,
                Message = mes,
            };
        }
    }
}
