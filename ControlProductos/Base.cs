using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace ControlProductos
{
    public class Base
    {
        private string cotizaService = ConfigurationManager.AppSettings["cotizaService"].ToString();

        protected DataTable ADataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        protected List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (pro.PropertyType == typeof(Int32))
                        {
                            pro.SetValue(obj, Convert.ToInt32(dr[column.ColumnName]), null);
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        protected string methodGet(string url)
        {
            url = url.Replace("/////", "/_all_/_all_/_all_/_all_/");
            url = url.Replace("////", "/_all_/_all_/_all_/");
            url = url.Replace("///", "/_all_/_all_/");
            url = url.Replace("//", "/_all_/");


            if(url.Substring(url.Length-1) == "/")
            {
                url = url + "_all_";
            }

            string strResult;
            WebRequest wrGETURL;
            try
            {
                wrGETURL = WebRequest.Create(cotizaService + url);
                wrGETURL.ContentType = "application/json; charset=utf-8";
                HttpWebResponse webresponse = wrGETURL.GetResponse() as HttpWebResponse;
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                // read response stream from response object
                StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                // read string from stream data
                strResult = loResponseStream.ReadToEnd();
                // close the stream object
                loResponseStream.Close();
                // close the response object
                webresponse.Close();
                return strResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            
        }

        protected string methodPost(string url, string json)
        {
            url = url.Replace("/////", "/_all_/_all_/_all_/_all_/");
            url = url.Replace("////", "/_all_/_all_/_all_/");
            url = url.Replace("///", "/_all_/_all_/");
            url = url.Replace("//", "/_all_/");


            if (url.Substring(url.Length - 1) == "/")
            {
                url = url + "_all_";
            }

            string strResult = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(cotizaService + url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }


                HttpWebResponse webresponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                strResult = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                webresponse.Close();

                return strResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected string methodPost(string url)
        {
            url = url.Replace("/////", "/_all_/_all_/_all_/_all_/");
            url = url.Replace("////", "/_all_/_all_/_all_/");
            url = url.Replace("///", "/_all_/_all_/");
            url = url.Replace("//", "/_all_/");


            if (url.Substring(url.Length - 1) == "/")
            {
                url = url + "_all_";
            }

            string strResult = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(cotizaService + url);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "application/json; charset=utf-8";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write("");
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse webresponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                strResult = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                webresponse.Close();

                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}