using ProjectBank.Accounts;
using ProjectBank.Bank;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProjectBank.FileMeneger
{
    internal static class XmlMethods
    {
        public static void Serialize<T>(this T data, string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<T>));
            List<T> list = new List<T>();
            if (File.Exists(path))
            {
                try
                {
                    foreach (T item in Deserialize<T>(path))
                    {
                        list.Add(item);
                    }
                    list.Add(data);
                    using (StreamWriter asd = new StreamWriter(path))
                    {
                        xml.Serialize(asd, list);
                    }
                }
                catch (Exception)
                {
                    list.Add(data);
                    using (StreamWriter asd = new StreamWriter(path))
                    {
                        xml.Serialize(asd, list);
                    }
                }
            }
            else
            {
                list.Add(data);
                using (FileStream fs = new FileStream(path, FileMode.Create)) { }
                using (StreamWriter sw = new StreamWriter(path))
                {
                    xml.Serialize(sw, list);
                }
            }
        }

        public static void Serialize<T>(this List<T> data, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            if (File.Exists(path))
            {
                try
                {
                    List<T> values = Deserialize<T>(path);
                    foreach (T item in data)
                    {
                        values.Add(item);
                    }
                    using (StreamWriter xd = new StreamWriter(path))
                    {
                        xmlSerializer.Serialize(xd, values);
                    }
                }
                catch (Exception)
                {
                    using (StreamWriter xd = new StreamWriter(path))
                    {
                        xmlSerializer.Serialize(xd, data);
                    }
                }
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Create)) { }
                using (StreamWriter sw = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(sw, data);
                }
            }
        }

        public static List<T> Deserialize<T>(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    List<T> item = (List<T>)xmlSerializer.Deserialize(sr);
                    return item;
                }
            }
            return null;
        }

        public static void RemoveFromXML<T>(this T obj, string path)
        {
            List<T> result = new List<T>();
            try
            {
                List<T> values = Deserialize<T>(path);
                if (values.Count == 1)
                {
                    File.Delete(path);
                }
                else
                {
                    foreach (var i in values)
                    {
                        if (!i.Equals(obj))
                        {
                            result.Add(i);
                        }
                    }
                    File.Delete(path);
                    using (FileStream fs = new FileStream(path, FileMode.Create)) { }
                    result.Serialize(path);
                }
            }

            catch (Exception)
            {

            }
        }
        public static Account FindInXML(string name, string path)
        {
            try
            {
                List<Account> accounts = Deserialize<Account>(path);
                foreach (var account in accounts)
                {
                    if (account.Name == name)
                    {
                        return account;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Loan FindLoanInXML(string name, string path)
        {
            try
            {
                List<Loan> loans = Deserialize<Loan>(path);
                foreach (var i in loans)
                {
                    if (i.Name == name)
                    {
                        return i;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int Count<T>(string path)
        {
            try
            {
                List<T> values = Deserialize<T>(path);
                return values.Count;
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public static bool Contains<T>(this T data, string path)
        {
            try
            {
                List<T> values = Deserialize<T>(path);
                return values.Contains(data);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
