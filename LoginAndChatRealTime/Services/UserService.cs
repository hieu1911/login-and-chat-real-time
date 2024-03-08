using LoginAndChatRealTime.Entities;
using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Interfaces;
using LoginAndChatRealTime.Models;

namespace LoginAndChatRealTime.Services
{
    public class UserService : IUserSerivce
    {
        public User? GetUser(string userName, string password)
        {
            using (var db = new MyDbContext())
            {
                var users = db.Users.Where<User>(u => (u.Name == userName) &&
                    (u.Password == password)).Take(1).ToList();

                if (!users.Any())
                {
                    return null;
                }

                return users[0];
            }
        }

        public List<User> GetUsersExceptId(int id)
        {
            using (var db = new MyDbContext())
            {
                var users = db.Users.Where(u => u.Id != id).ToList();

                return users;
            }
        }
    }
}
