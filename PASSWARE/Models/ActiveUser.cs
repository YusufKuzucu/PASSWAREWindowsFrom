using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASSWARE.Models
{
    public class ActiveUser
    {
        static int _id;
        static string _password, _firstName, _lastName, _email,_token;
        static bool _status;
        public static int Id
        {
            get { return ActiveUser._id; }
            set { ActiveUser._id = value; }
        }
        public static string FirstName
        {
            get { return ActiveUser._firstName; }
            set { ActiveUser._firstName = value; }
        }
        public static string LastName
        {
            get { return ActiveUser._lastName; }
            set { ActiveUser._lastName = value; }
        }
        public static string Email
        {

            get { return ActiveUser._email; }
            set { ActiveUser._email = value; }
        }
        public static bool Status
        {
            get { return ActiveUser._status; }
            set { ActiveUser._status = value; }
        }
        public static string Token
        {
            get { return ActiveUser._token; }
            set { ActiveUser._token = value; }
        }
        public static void SetActiveUser(User user)
        {
            ActiveUser.Id = _id;
            ActiveUser.Email = user.Email;
            ActiveUser.FirstName = user.FirstName;
            ActiveUser._lastName = user.LastName;
            ActiveUser.Status = user.Status;
        }
    }
}
