#define LOCAL
#define FIREBASE // Eğer MySQL kullanacaksanız FIREBASE yerine MySQL kullanının

using System;
using Rehber_EL;


using System.Collections.Generic;


#if MSSQL
using BsmSqlConnection = System.Data.SqlClient.SqlConnection;
#elif MySQL
using MySql.Data.MySqlClient;
using BsmSqlConnection = MySql.Data.MySqlClient.MySqlConnection;
#endif

#if FIREBASE
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
#endif

namespace Rehber_DL
{


    public static class DL
    {

#if FIREBASE
        static FirebaseClient client = new FirebaseClient("https://rehber13-14cbd-default-rtdb.firebaseio.com");

        public static bool AddContact(Kisi k, ref string message)
        {
            client.Child($"kisiler/{k.ID}").PutAsync(k);
            return true;
        }

        public static bool EditContact(Kisi k, ref string message)
        {
            client.Child($"kisiler/{k.ID}").PutAsync(k);
            return true;
        }

        public static bool DeleteContact(Kisi k, ref string message)
        {
            client.Child($"kisiler/{k.ID}").DeleteAsync();
            return true;
        }

        public static ObservableCollection<Kisi> GetContacts(ref string message)
        {
            ObservableCollection<Kisi> kisilist = new ObservableCollection<Kisi>();

            var kisiler = client.Child("kisiler").OnceAsync<Kisi>();
            foreach (var kisi in kisiler.Result)
            {
                kisilist.Add(kisi.Object);
            }
            return kisilist;
        }


#elif MySQL
        
        static MySqlConnection con = new MySqlConnection(new MySqlConnectionStringBuilder()
        {
#if (LOCAL)
            // *** DİKKAT server IP,
            Server = "localhost", //"10.86.12.152", 
            // localhost ise local ip'niz;
            // windows machine için localhost yazmanız yeterli

            Port = 3306,
            UserID = "user1", // kendi kullanıcı adınız
            Password="WfnXy123", // kendi şifreniz
            Database = "rehber", // kendi db'niz


#else
            Server = "https://www.bayramakgul.com",
            Port = 3306,
            UserID = "bayramakgul_user2",
            Password="Qaz+Wsx-123+",
            Database = "bayramakgul_rehber"
            SslMode =  MySqlSslMode.Disabled,
            AllowPublicKeyRetrieval = true,

#endif

        }.ConnectionString);


        /*
            kid varchar(32) PK 
            name varchar(45) 
            surname varchar(45) 
            phone varchar(45) 
            mail varchar(45) 
            address varchar(245) 
            photo varchar(245)
         */

        public static  bool AddContact(Kisi k, ref string message)
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                string insert = "INSERT INTO contacts " +
                    "VALUES(@kid, @name, @sname, @phone, @mail, @addr, @photo)";

                MySqlCommand command = new MySqlCommand(insert, con);
                command.Parameters.AddWithValue("@kid", k.ID);
                command.Parameters.AddWithValue("@name", k.Ad);
                command.Parameters.AddWithValue("@sname", k.Soyad);
                command.Parameters.AddWithValue("@phone", k.Telefon);
                command.Parameters.AddWithValue("@mail", k.Mail);
                command.Parameters.AddWithValue("@addr", k.Adres);
                command.Parameters.AddWithValue("@photo", k.ResimData);

                command.ExecuteNonQuery();

                message = "";
            return true;
            }
            catch (Exception ex)
            { 
                message = ex.Message;
                return false;

            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                    con.Close();
            }
        }


        public static bool EditContact(Kisi k, ref string message)
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                string update = "UPDATE contacts " +
                    " SET  `name`=@name, `surname`=@sname, `phone`=@phone, `mail`=@mail, `address`=@addr, `photo`=@photo " +
                    " WHERE `kid`=@kid";

                MySqlCommand command = new MySqlCommand(update, con);
                command.Parameters.AddWithValue("@name", k.Ad);
                command.Parameters.AddWithValue("@sname", k.Soyad);
                command.Parameters.AddWithValue("@phone", k.Telefon);
                command.Parameters.AddWithValue("@mail", k.Mail);
                command.Parameters.AddWithValue("@addr", k.Adres);
                command.Parameters.AddWithValue("@photo", k.ResimData);
                command.Parameters.AddWithValue("@kid", k.ID);

                command.ExecuteNonQuery();

                message = "";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;

            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                    con.Close();
            }
        }

        public static bool DeleteContact(Kisi k, ref string message)
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                string delete = "DELETE FROM contacts WHERE kid=@kid";

                MySqlCommand command = new MySqlCommand(delete, con);
                command.Parameters.AddWithValue("@kid", k.ID);

                command.ExecuteNonQuery();
                message = "";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;

            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                    con.Close();
            }
        }


        public static List<Kisi> GetContacts(ref string message)
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                string select = "SELECT * FROM contacts ";

                MySqlCommand command = new MySqlCommand(select, con);
                var dr = command.ExecuteReader();
                var kisiler = new List<Kisi>();
                while (dr.Read())
                {
                    kisiler.Add(new Kisi()
                    {
                        ID      = dr["kid"].ToString(),
                        Ad      = dr["name"].ToString(),
                        Soyad   = dr["surname"].ToString(),
                        Telefon = dr["phone"].ToString(),
                        Mail    = dr["mail"].ToString(),
                        Adres   = dr["address"].ToString(),
                        Resim   = dr["photo"].ToString(),
                    });
                }

                message = "";
                return kisiler;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;

            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                    con.Close();
            }
        }
#endif

    }
}
