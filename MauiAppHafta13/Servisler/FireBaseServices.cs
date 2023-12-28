using Firebase.Auth;
using Firebase.Auth.Providers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppHafta13.Servisler
{
    public static class FireBaseServices
    {
        static string projectid = "rehber13-14cbd";
        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyClOEkO5k_bFuWb9VJCY2BCwOzDvLVRkiw",
            AuthDomain = $"{projectid}.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]{ new EmailProvider()},
        };

        public static async Task<bool> Login(string email, string pass)
        {
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = await client.SignInWithEmailAndPasswordAsync(email, pass);
                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> Register(string dispname, string email, string pass)
        {
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = await client.CreateUserWithEmailAndPasswordAsync(email, pass, dispname);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
