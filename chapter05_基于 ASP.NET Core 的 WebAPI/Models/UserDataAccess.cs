using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCoreAPI.Models
{
    public class UserDataAccess
    {
        private readonly UserDbContext _context;

        //在构造函数中显示注入
        public UserDataAccess(UserDbContext context)
        {
            _context = context;
        }

        //保存用户
        public User SaveUser(User user)
        {
            _context.Users.Add(user);//添加一个user
            _context.SaveChanges();//保存（调用SaveChanges才真正将数据写入了数据库）
            return user;
        }

        //查询用户
        public User SelectUser(User user)
        {
            var users = (from m in _context.Users
                         where m.Name == user.Name && m.Password == user.Password
                         select m);
            return users.FirstOrDefault();
        }
    }
}
