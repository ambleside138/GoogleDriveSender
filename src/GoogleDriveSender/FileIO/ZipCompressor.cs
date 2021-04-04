using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleDriveSender.FileIO
{
    public static class ZipCompressor
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        private static string _TempFolderName = "WorkspaceForZip";

        public static string Run(string targetDirPath)
        {
            var workPath = CreateWorkspacePath();
            var dirName = Path.GetFileName(targetDirPath);
            var zipFullPath = Path.Combine(workPath, $"{dirName}.zip");

            //ZIP書庫を作成
            System.IO.Compression.ZipFile.CreateFromDirectory(
                targetDirPath,
                zipFullPath,
                System.IO.Compression.CompressionLevel.Optimal,
                false,
                System.Text.Encoding.UTF8);

            return zipFullPath;
        }

        private static string CreateWorkspacePath()
        {
            var currentPath = Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName);
            var targetPath = Path.Combine(currentPath.FullName, _TempFolderName);

            if(Directory.Exists(targetPath))
            {
                _Logger.Info($"zip圧縮作業ディレクトリがすでに存在するため再作成します");
                Directory.Delete(targetPath, true);
            }

            Directory.CreateDirectory(targetPath);
            _Logger.Info($"zip圧縮作業ディレクトリ 作成完了 [{targetPath}]");

            return targetPath;
        }
    }
}
