using Patterns.Ex00.ExternalLibs;
namespace Patterns.Ex00
{
    public class LogImporterClient
    {
        /// <summary>
        /// TODO: Изменения нужно вносить в этом методе
        /// </summary>
        public void DoMethod()
        {
            LogImporter importer = new LogImporter(new FileLogReader());
            importer.ImportLogs("ftp://log.txt");
            LogImporter ftpImporter = new LogImporter(new FtpReader("login", "pass"));
            ftpImporter.ImportLogs("ftp://log.txt");
        }        
    }

    public class FtpReader : ILogReader
    {
        string _login, _pass;
        public FtpReader(string login, string pass)
        {
            _login = login;
            _pass = pass;
        }

        public string ReadLogFile(string identificator)
        {
            return new FtpClient().ReadFile(_login, _pass, identificator);
        }
    }
}