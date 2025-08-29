using System;

namespace GolddiggerGmbh.Model
{ 
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

   
        public User() { }

        
      
        public User(string userName, string password)
        {
            try
            {
                if (!ValidationHelper.IsValidUserName(userName))
                    throw new ArgumentException("Invaliud Username");

                if (!ValidationHelper.IsValidPassword(password))
                    throw new ArgumentException("Passwort is not Correct!");

                UserName = userName;
                Password = password;

                Logger.LogInfo($"User created: {userName}");
            }
            catch (Exception ex)
            {
                //ExceptionHandler.Handle(ex);
                Logger.LogException(ex);
                throw;
            }
        }
    }
}
