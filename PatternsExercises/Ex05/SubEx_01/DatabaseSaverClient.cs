using Patterns.Ex05.ExternalLibs;
using System;
using System.Collections.Generic;

namespace Patterns.Ex05.SubEx_01
{
    public class DatabaseSaverClient
    {
        public void Main(bool b)
        {
            var databaseSaver = new DatabaseObservable();
            DoSmth(databaseSaver);

        }

        private void DoSmth(IDatabaseSaver saver)
        {
            saver.SaveData(null);
        }
    }


    class DatabaseObservable : IDatabaseSaver
    {
        DatabaseSaver dbSaver;
        CacheUpdater cacheUpdater;
        MailSender mailSender;

        List<Action<>> actions;
        public DatabaseObservable()
        {
            dbSaver = new DatabaseSaver();
            cacheUpdater = new CacheUpdater();
            mailSender = new MailSender();
        }
        public void SaveData(object data)
        {
            dbSaver.SaveData(data);
            mailSender.Send("email");
            cacheUpdater.UpdateCache();
        }

        public void AddAction(Action<> action)
        {

        }
    }
}