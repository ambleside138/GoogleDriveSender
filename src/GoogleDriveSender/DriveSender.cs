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

        public async Task<bool> TrySend(string path)
        {
            // アクセストークン取得
            var credential = await CredentialProvider.GetUserCredentialAsync();
            if (credential == null)
                return false;

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _ApplicationName,
            });



            var p = Upload(path, service).Result;


            var id = p.Id;

            service.Permissions.Create(new Permission
            {

            }, id);

            //// Define parameters of request.
            //FilesResource.ListRequest listRequest = service.Files.List();
            //listRequest.PageSize = 10;
            //listRequest.Fields = "nextPageToken, files(id, name)";

            //// List files.
            //IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
            //    .Files;
            //Console.WriteLine("Files:");
            //if (files != null && files.Count > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        Console.WriteLine("{0} ({1})", file.Name, file.Id);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No files found.");
            //}
            //Console.Read();


            return true;
        }

        private async Task<File> Upload(string filePath, DriveService service)
        {
            var meta = new File()
            {
                Name = System.IO.Path.GetFileName(filePath),
                MimeType = GetMimeType(filePath)
            };

            using var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            // 新規追加
            var request = service.Files.Create(meta, stream, GetMimeType(filePath));
            request.Fields = "id, name";


            var result = await request.UploadAsync();
            if (result.Status == Google.Apis.Upload.UploadStatus.Failed)
            {
                throw result.Exception;
            }

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
