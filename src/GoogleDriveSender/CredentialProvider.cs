using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleDriveSender
{
    // ここからAPIを有効化し、credentialsファイルを取得する
    // https://console.developers.google.com/apis

    class CredentialProvider
    {
        private static readonly NLog.Logger _Logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string[] _Scopes = { DriveService.Scope.DriveFile };

        private static readonly string _TokenDirectory = "token.json";
        private static readonly string _CredentialFileName = "credentials.json";

        public static async Task<UserCredential> GetUserCredentialAsync()
        {
            var currentPath = Directory.GetParent(Process.GetCurrentProcess().MainModule.FileName);
            var credentialFullPath = Path.Combine(currentPath.FullName, _CredentialFileName);

            _Logger.Info($"認証ファイル取得 path=[{credentialFullPath}]");

            if (File.Exists(credentialFullPath) == false)
            {
                _Logger.Warn("認証ファイルが見つかりませんでした。");
                return null;
            }

            using (var stream = new FileStream(credentialFullPath, FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.

                var tokenPath = Path.Combine(currentPath.FullName, _TokenDirectory);
                Console.WriteLine("Credential file saved to: " + tokenPath);
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(tokenPath, true));
            }
        }
    }
}
