using Rehber_DL;
using Rehber_EL;

using System;
using System.Collections.ObjectModel;

namespace Rehber_BL
{
    public class BL
    {
        public static ObservableCollection<Kisi> Kisiler = new ObservableCollection<Kisi> ();

        public static bool KisileriYukle(ref string message)
        {
            var list = DL.GetContacts(ref message);
            if(list!=null)
            {
                foreach (var k in list)
                    Kisiler.Add(k);

                return true;
            }
            return false;
        }

        public static bool  KisiEkle(Kisi kisi, ref string message)
        {

            if (kisi!=null && DL.AddContact(kisi, ref message ))
            {
                Kisiler.Add(kisi);
                return true;
            }

            return false;
        }
        public static bool KisiDuzenle(Kisi kisi, ref string message)
        {
            if (kisi != null && DL.EditContact(kisi, ref message))
            {
                return true;
            }

            return false;
        }
        public static bool KisiSil(Kisi kisi, ref string message)
        {
            if (kisi != null && DL.DeleteContact(kisi, ref message))
            {
                Kisiler.Remove(kisi);
                return true;
            }

            return false;
        }


    }
}
