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
        private readonly string _SendDirectoryId;

        public DriveSender(string sendDirectoryId)
        {
            _SendDirectoryId = sendDirectoryId;
        }

        public ProcessResult TrySend(string path)
        {
            _Logger.Info("トークン取得");
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

            _Logger.Info("アップロード");
            var result = Upload(path, service);
            var id = result.Id;

            _Logger.Info("権限変更");
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

        private File Upload(string filePath, DriveService service)
        {
            var fileName = System.IO.Path.GetFileName(filePath);

            var listRequest = service.Files.List();
            listRequest.Q = $"name = '{fileName}' and '{_SendDirectoryId}' in parents and trashed=false";
            listRequest.Fields = "nextPageToken, files(id, name, webViewLink) ";

            // アップロード済みのファイルを更新するにはAPIを切り替える必要がある

            var file = listRequest.Execute().Files.FirstOrDefault();
            if(file != null)
            {
                // 更新
                using var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                var request = service.Files.Update(file, file.Id, stream, GetMimeType(filePath));
                request.Fields = "id, name, webViewLink";
                request.KeepRevisionForever = false;

                request.Upload();

                request.Body.WebViewLink = file.WebViewLink;

                return request.Body;
            }
            else
            {
                // 新規追加
                var meta = new File()
                {
                    Name = System.IO.Path.GetFileName(filePath),
                    MimeType = GetMimeType(filePath),
                    Parents = new List<string> { _SendDirectoryId },
                };

                using var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                var request = service.Files.Create(meta, stream, GetMimeType(filePath));
                request.Fields = "id, name, webViewLink";
                request.KeepRevisionForever = false;

                request.Upload();

                return request.ResponseBody;
            }
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
