using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppIntegrador.Models;

namespace AppIntegrador.Utilities
{
    public static class UsersManager
    {
        public static string GetCurrentUserName(HttpSessionStateBase Session)
        {
            if (Session != null)
                return ((LoggedInUserData)Session["UserData"]).Username;
            else
                return "";
            
        }

        public static string GetCurrentUserProfile(HttpSessionStateBase Session)
        {
            if (Session != null)
                return ((LoggedInUserData)Session["UserData"]).Profile;
            else
                return "";
        }

        public static string GetCurrentUserMajorId(HttpSessionStateBase Session)
        {
            if (Session != null)
                return ((LoggedInUserData)Session["UserData"]).MajorId;
            else
                return "";
        }

        public static string GetCurrentUserEmphasisId(HttpSessionStateBase Session)
        {
            if (Session != null)
                return ((LoggedInUserData)Session["UserData"]).EmphasisId;
            else
                return "";

        }
    }
}