using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleDriveSender
{
    class DriveSender
    {

        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string _ApplicationName = "Sender";

        public ProcessResult TrySend(string path)
        {
            // アクセストークン取得
            var credential = CredentialProvider.GetUserCredential();
            if (credential == null)
                return ProcessResult.Error("認証トークン取得に失敗");

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _ApplicationName,
            });

            var result = Upload(path, service);
            var id = result.Id;
            var webViewLink = result.WebViewLink;

            //var id = p.Id;

            service.Permissions.Create(new Permission
            {
                Role = "reader",
                Type = "domain",
            }, id);

            return new ProcessResult 
            { 
                HasError = false, 
                SharePath = result.WebViewLink
            };

        }

        //private async Task<File> Upload(string filePath, DriveService service)
        //{
        //    var meta = new File()
        //    {
        //        Name = System.IO.Path.GetFileName(filePath),
        //        MimeType = GetMimeType(filePath)
        //    };

        //    using var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
        //    // 新規追加
        //    var request = service.Files.Create(meta, stream, GetMimeType(filePath));
        //    request.Fields = "id, name";


        //    var result = await request.UploadAsync();
        //    if (result.Status == Google.Apis.Upload.UploadStatus.Failed)
        //    {
        //        throw result.Exception;
        //    }

        //    return request.Body;
        //}

        private File Upload(string filePath, DriveService service)
        {
            var meta = new File()
            {
                Name = System.IO.Path.GetFileName(filePath),
                MimeType = GetMimeType(filePath)
            };

            using var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            // 新規追加
            var request = service.Files.Create(meta, stream, GetMimeType(filePath));
            request.Fields = "id, name, webViewLink";
            request.KeepRevisionForever = false;

            // 
            request.Upload();

            return request.ResponseBody;
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

    }
}
