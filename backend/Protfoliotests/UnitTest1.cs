using protfolio.Data;
using System;
using Xunit;

namespace Protfoliotests
{
    public class UserRepoTests
    {
        [Fact]
        public void AddUserTest()
        {
            var context = new ApplicationContext();
            var user = new User();
            user.FirstName = "someName";
            user.SecondName = "secondName";
            user.Email = "some";

            user.Password = new byte[64];
            user.Salt = new byte[64];
            context.Users.Add(user);
            context.SaveChanges();
            Assert.True(true);

        }

        [Fact]
        public void UpdateserTest()
        {

        }


    }
}
