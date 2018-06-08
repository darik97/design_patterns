namespace Patterns.Ex02
{
    public abstract class UserService<T>
    {
        public UserInfo GetUserInfo(string pageUrl)
        {
            var userId = Parse(pageUrl);
            T[] users = GetFriendsById(userId);
            UserInfo[] friends = ConvertToUserInfo(users);

            UserInfo result = new UserInfo
            {
                Name = GetName(userId),
                UserId = userId,
                Friends = friends
            };

            return result;
        }

        protected abstract string Parse(string pageUrl);
        protected abstract T[] GetFriendsById(string userId);
        protected abstract UserInfo[] ConvertToUserInfo(T[] users);
        protected abstract string GetName(string userId);
    }
}
