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
    class Program
    {


      

        // start main

        static void Main(string[] args)
            {

              String ftp_user = @"rehman.rasheed";
         String ftp_password = @"password_1";
         String ftp_url = @"ftp://192.168.42.186/";

        String FIle_Path = @"D:\Encrypted-CardProduction.txtCPE00000057CARDEXPORT20190829184508-AL_Fardan_Debit";
            String fuleName = @"Encrypted-CardProduction.txtCPE00000057CARDEXPORT20190829184508-AL_Fardan_Debit";
            //   Download_Files();
            FTPConnection FTPObj = new FTPConnection(ftp_url, ftp_user, ftp_password);


            FTPObj.UploadFileToFTP(FIle_Path  , fuleName);

            Console.WriteLine("Uploading is done!");
            Console.WriteLine("Uploading is done!");

        }
        }
    }

