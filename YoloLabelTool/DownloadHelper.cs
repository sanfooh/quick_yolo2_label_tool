using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;

namespace TFLabelTool
{
    class DownloadHelper
    {
        ProgressBar proBarDownLoad;
        Label lblMessage;

        public void Start(string url, string filePath, ProgressBar proBarDownLoad, Label lblMessage)
        {
            this.proBarDownLoad = proBarDownLoad;
            this.lblMessage = lblMessage;
            if (File.Exists(filePath))
            {
                return;
            }
            string fileDirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(fileDirPath))
            {
                Directory.CreateDirectory(fileDirPath);
            }
            try
            {
                WebClient client = new WebClient();
                client.DownloadFileCompleted += client_DownloadFileCompleted;
                client.DownloadProgressChanged += client_DownloadProgressChanged;
                client.DownloadFileAsync(new Uri(url), filePath);
            }
            catch(Exception ex)
            {

            }


        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
            {
                if (lblMessage!=null)
                {
                    lblMessage.Text =  "已下载";
                }
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            proBarDownLoad.Minimum = 0;
            proBarDownLoad.Maximum = (int)e.TotalBytesToReceive;
            proBarDownLoad.Value = (int)e.BytesReceived;
            //this.lblPercent.Text = e.ProgressPercentage + "%";
        }
    }
}
