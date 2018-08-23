using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CTCT;
using CTCT.Services;

namespace vidly.Services
{
    public class MyContactService
    {
        private static string _apiKey = "hqucfzj5r5gty3trxzn36836";
        private static string _accessToken = "07fc523b-544f-4506-9e87-c6b0e0969512";
        private static IUserServiceContext userServiceContext;
        private static ContactService contactService;

        public  MyContactService() 
        {
            if (userServiceContext == null)
            {
                userServiceContext = new UserServiceContext(_accessToken, _apiKey);
            }
            if (contactService == null) 
            {
                contactService = new ContactService(userServiceContext);
            }
        }

        public ContactService getContactService() 
        {
            return contactService;
        }
        
    }
}