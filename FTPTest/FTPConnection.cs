using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
//using System.Net;
using System.Text;

using System.Threading.Tasks;

namespace FTPTest
{
    class FTPConnection
    {
        private  String ftp_user = "";
        private String ftp_password = "";
        private String ftp_url = "";

        public FTPConnection(String FTP_URL , String USER_NAME , String  PASSWORD ) {
            this.ftp_url = FTP_URL;
            this.ftp_user = USER_NAME;
            this.ftp_password = PASSWORD;
        }




        public  void Download_Files()
        {
            String txtResults = "";
            List<String> files = Get_File_List_From_FTP();

            foreach (String file in files)
            {
                String file_data = Get_File_Data_And_Download_File(file);

                /*  txtResults.con(file + "\t\r");
                  txtResults.AppendText(file_data + "\t\r");
                  txtResults.AppendText("\t\r");*/

                Console.WriteLine("Hello World!" + file + " file \t\r " + "file_data" + file_data + "\t\r");


            }

        }



        private  List<String> Get_File_List_From_FTP()
        {
            //result data from file
            List<String> file_list = new List<String>();

            //do ftpwebrequest
            //initialize FtpWebRequest with your FTP Url
            //your FTP url should start with ftp://wwww.youftpsite.com//
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp_url);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            //set up credentials
            request.Credentials = new NetworkCredential(ftp_user, ftp_password);

            //initialize ftp response
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //open readers
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);


            String line = String.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(".txt"))
                    file_list.Add(line);
            }


            //closing
            reader.Close();
            response.Close();

            return file_list;

        }


        private  String Get_File_Data_And_Download_File(String file_name)
        {
            //result data from file
            String result = String.Empty;



            //do ftpwebrequest
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp_url + file_name);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //set up credentials
            request.Credentials = new NetworkCredential(ftp_user, ftp_password);

            //initialize ftp response
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //open readers
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            //data from file.
            result = reader.ReadToEnd();

            //set to save file locally. 
            using (StreamWriter file = File.CreateText(file_name))
            {
                file.WriteLine(result);
                file.Close();

            }

            //closing
            reader.Close();
            response.Close();

            return result;



        }

        public  void UploadFileToFTP(String source, String nFileName)
        {
            //  FtpWebRequest ftpRequest = null;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp_url + "/" + nFileName);

            request.Credentials = new NetworkCredential(ftp_user, ftp_password);

            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;




            Stream reqStream = request.GetRequestStream();
            try
            {

                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();


                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
