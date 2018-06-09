using Patterns.Ex05.ExternalLibs;

namespace Patterns.Ex05.SubEx_02
{
    public class DatabaseSaverClient
    {
        public void Main(bool b)
        {
            IDatabaseSaver dbSaver = new DatabaseSaver();
            dbSaver = new MailSenderDecorator(dbSaver, new MailSender(), "");
            dbSaver = new CacheUpdaterDecorator(dbSaver, new CacheUpdater());

            DoSmth(dbSaver);
        }

        private void DoSmth(IDatabaseSaver saver)
        {
            saver.SaveData(null);
        }
    }

    public class DatabaseSaverDecorator : IDatabaseSaver
    {
        IDatabaseSaver _saver;

        public DatabaseSaverDecorator(IDatabaseSaver saver)
        {
            _saver = saver;
        }

        public void SaveData(object data)
        {
            _saver.SaveData(data);
        }
    }

    public class MailSenderDecorator : DatabaseSaverDecorator
    {
        MailSender _mailSender;
        string _email;

        public MailSenderDecorator(IDatabaseSaver saver, MailSender mailSender, string email) : base(saver)
        {
            _mailSender = mailSender;
            _email = email;
        }

        public new void SaveData(object data)
        {
            base.SaveData(data);
            _mailSender.Send(_email);
        }
    }

    public class CacheUpdaterDecorator : DatabaseSaverDecorator
    {
        CacheUpdater _cacheUpdater;

        public CacheUpdaterDecorator(IDatabaseSaver saver, CacheUpdater cacheUpdater) : base(saver)
        {
            _cacheUpdater = cacheUpdater;
        }

        public new void SaveData(object data)
        {
            base.SaveData(data);
            _cacheUpdater.UpdateCache();
        }
    }
}