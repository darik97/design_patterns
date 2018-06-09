using Patterns.Ex05.ExternalLibs;
using System.Collections.Generic;

namespace Patterns.Ex05.SubEx_01
{
    public class DatabaseSaverClient
    {
        public void Main(bool b)
        {
            var databaseSaver = new DatabaseObservable();
            databaseSaver.Observable.Add(new MailSenderObserver());
            databaseSaver.Observable.Add(new CacheUpdaterObserver());

            DoSmth(databaseSaver);

        }

        private void DoSmth(IDatabaseSaver saver)
        {
            saver.SaveData(null);
        }
    }

    class DatabaseObservable : IDatabaseSaver
    {
        public Observable Observable = new Observable();
        
        public void SaveData(object data)
        {
            Observable.Notify();
        }
    }

    class Observable
    {
        List<IObserver> observers = new List<IObserver>();

        public void Add(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Delete(IObserver observer)
        {
            observers.RemoveAt(observers.IndexOf(observer));
        }

        public void Notify()
        {
            observers.ForEach(o => o.Update());
        }
    }

    public interface IObserver
    {
        void Update();
    }

    class MailSenderObserver : IObserver
    {
        MailSender sender;
        string email;

        public void Update()
        {
            sender.Send(email);
        }
    }

    class CacheUpdaterObserver : IObserver
    {
        CacheUpdater updater;

        public void Update()
        {
            updater.UpdateCache();
        }
    }
}