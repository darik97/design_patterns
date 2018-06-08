using System;
using System.Linq;
using System.Text.RegularExpressions;
using Patterns.Ex01.ExternalLibs.Twitter;

namespace Patterns.Ex02
{
    public class TwitterUserService : UserService<TwitterUser>
    {
        readonly TwitterClient _client = new TwitterClient();
        string _userName;
        
        override protected UserInfo[] ConvertToUserInfo(TwitterUser[] users)
        {
            return users.Select(c =>
                {
                    UserInfo userInfo = new UserInfo
                    {
                        UserId = c.UserId.ToString(),
                        Name = _client.GetUserNameById(c.UserId)
                    };
                    return userInfo;
                })
                .ToArray();
        }

        override protected TwitterUser[] GetFriendsById(string userId)
        {
            return _client.GetSubscribers(Convert.ToInt64(userId));
        }

        /// <summary>
        /// Нет необходимости менять этот метод, достаточно просто переиспользовать
        /// Реализация его не важна, стоит полагаться только на его внешний интерфейс
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        long GetUserId(String userName)
        {
            //TODO: Return userId from userName
            return 0;
        }

        override protected string GetName(string userId)
        {
            return _userName;
        }

        override protected string Parse(string pageUrl)
        {
            var regex = new Regex("twitter.com/(.*)");
            _userName = regex.Match(pageUrl).Groups[0].Value;
            return GetUserId(_userName).ToString();
        }
    }
}