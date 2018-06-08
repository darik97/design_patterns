using System;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Ex01
{
    public class SubscriberViewer
    {
        Dictionary<SocialNetwork, IStrategy> _strategies;

        public SubscriberViewer(Dictionary<SocialNetwork, IStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(k => k.Key, k => k.Value);
        }

        /// <summary>
        /// Возвращает список подписчиков пользователя из соц.сети.
        /// TODO: необходимо изменить этот метод по условиям задачи
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="networkType"></param>
        /// <returns></returns>
        public SocialNetworkUser[] GetSubscribers(String userName, SocialNetwork networkType)
        {
            return _strategies[networkType].GetSubscribers(userName);
        }

        public void FillStrategies()
        {
            _strategies.Add(SocialNetwork.Instagram, new InstagramStrategy());
            _strategies.Add(SocialNetwork.Twitter, new TwitterStrategy());
        }
    }

    public interface IStrategy
    {
        SocialNetworkUser[] GetSubscribers(string name);
    }

    class InstagramStrategy : IStrategy
    {
        public SocialNetworkUser[] GetSubscribers(string name)
        {
            var list = new List<SocialNetworkUser>();
            var instClient = new InstagramClient();
            var usersInst = instClient.GetSubscribers(name);
            usersInst.ToList().ForEach(u => list.Add(
                new SocialNetworkUser
                {
                    UserName = u.UserName
                }));
            return list.ToArray();
        }
    }

    class TwitterStrategy : IStrategy
    {
        public SocialNetworkUser[] GetSubscribers(string name)
        {
            var list = new List<SocialNetworkUser>();
            var twitterClient = new TwitterClient();
            var usersTwit = twitterClient.GetSubscribers(twitterClient.GetUserIdByName(name));

            usersTwit.ToList().ForEach(u => list.Add(
                new SocialNetworkUser
                {
                    UserName = twitterClient.GetUserNameById(u.UserId)
                }));
            return list.ToArray();
        }
    }
}