using Application;
using Persistence.Repositories;

namespace TaskForge.Test
{
    public class PasswordHasherTests
    {
        private IPasswordHasher _passwordHasher;
        [SetUp]
        public void Setup()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Test]
        public void PasswordHashing_Test1()
        {
            //Arrange
            var password = "P@dasdkasjdlkajgfasd412u34912jKLDSJSDFA___";

            //Act
            var hashedPassword = _passwordHasher.HashPassword(password);

            //Assert
            Assert.IsTrue(_passwordHasher.VerifyPassword(hashedPassword, password));
        }

        [Test]
        public void PasswordHashing_Test2()
        {
            //Arrange
            var password = "2134h23ujibjktnjas!@#!@+_+u32y48912037!$+_+#@$@4fhjksdalhf78f3208923hfnaskdjfhbn283723";

            //Act
            var hashedPassword = _passwordHasher.HashPassword(password);

            //Assert
            Assert.IsTrue(_passwordHasher.VerifyPassword(hashedPassword, password));
        }

        [Test]
        public void PasswordHashing_VerifyFailed_Test()
        {
            //Arrange
            // not equal passwords
            var password1 = "2134h23ujibjktnjas!@#!@+_+u32y48912037!$+_+#@$@4fhjksdalhf78f3208923hfnaskdjfhbn283723";
            var password2 = "2137h23ujibjktnjafd@#!@+_+u32y48912037!$+_+#@$@4fhjksdalhf78f3208923hfnaskdjfhbn283723"; 

            //Act
            var hashedPassword = _passwordHasher.HashPassword(password1);

            //Assert
            Assert.IsFalse(_passwordHasher.VerifyPassword(hashedPassword, password2));
        }
    }
}