using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using TFLabelTool;

namespace YoloLabelTool
{
    public partial class FormMain : Form
    {
        string outputPath = "";
        string cfgPath = "";
        string imagePath = "";
        string objNamesPath = "";
        string trainTxtFilePath = "";
        string testTxtFilePath = "";
        string objDataFilePath = "";
        string objCfgFilePath = "";
        string preWeightPath = "";
        string backupFolder = "";
        string darknetFolder = "";

        const int MaxPageIndex = 1000;
        string imageDownloadPath = "";
        int imageDownloadCount = 10;
        string imageDownloadKey = "";
        string imageDownloadPre = "";
        int zoomWidth = 0;
        int zoomHeight = 0;

        string currentJPGName = "";
        string currentObjName = "";
        int currentObjIndex = -1;

        BackgroundWorker downloadImageThread = new BackgroundWorker();
        bool isDownloadImageThreadRun = false;


        BackgroundWorker importFilesThread = new BackgroundWorker();
        bool isImportFilesThreadRun = false;

        BackgroundWorker trainThread = new BackgroundWorker();
        bool isSrainThreadThreadRun = false;

        BackgroundWorker darknetThread = new BackgroundWorker();

        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        string[] userAgent = { "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-us) AppleWebKit/534.50 (KHTML, like Gecko) Version/5.1 Safari/534.50", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729; InfoPath.3; rv:11.0) like Gecko", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_7_0) AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.56 Safari/535.11", 
                                     "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; SE 2.X MetaSr 1.0; SE 2.X MetaSr 1.0; .NET CLR 2.0.50727; SE 2.X MetaSr 1.0)", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36"};

        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            outputPath = String.Format("{0}\\{1}", Application.StartupPath, "output");
            imagePath = String.Format("{0}\\{1}\\", outputPath, "image");
            preWeightPath = String.Format("{0}\\{1}\\", Application.StartupPath, "weight");
            cfgPath = String.Format("{0}\\{1}\\", outputPath, "cfg");
            objNamesPath = String.Format("{0}\\{1}", cfgPath, "obj.names");
            objDataFilePath = String.Format("{0}\\{1}", cfgPath, "obj.data");
            objCfgFilePath = String.Format("{0}\\{1}", cfgPath, "obj.cfg");
            trainTxtFilePath = String.Format("{0}\\{1}", outputPath, "train.txt");
            testTxtFilePath = String.Format("{0}\\{1}", outputPath, "test.txt");
            backupFolder = String.Format("{0}\\{1}\\", outputPath, "backup");
            imageDownloadPath = String.Format("{0}\\{1}", Application.StartupPath, "download");
            darknetFolder = String.Format("{0}\\{1}", Application.StartupPath, "darknet");

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            if (!Directory.Exists(imageDownloadPath))
            {
                Directory.CreateDirectory(imageDownloadPath);
            }
            if (!Directory.Exists(cfgPath))
            {
                Directory.CreateDirectory(cfgPath);
            }
            if (!Directory.Exists(preWeightPath))
            {
                Directory.CreateDirectory(preWeightPath);
            }

            LoadFiles();

            textBoxImport.Text = Properties.Settings.Default.imagePath;
            LoadObjectFromConfig();
            CreateObjNames();
            downloadImageThread.DoWork += new DoWorkEventHandler(downloadImageThread_DoWork);
            downloadImageThread.ProgressChanged += new ProgressChangedEventHandler(downloadImageThread_ProgressChanged);
            downloadImageThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(downloadImageThread_RunWorkerCompleted);
            downloadImageThread.WorkerReportsProgress = true;


            importFilesThread.DoWork += ImportFilesThread_DoWork;
            importFilesThread.ProgressChanged += ImportFilesThread_ProgressChanged;
            importFilesThread.RunWorkerCompleted += ImportFilesThread_RunWorkerCompleted;
            importFilesThread.WorkerReportsProgress = true;



            trainThread.DoWork += new DoWorkEventHandler(trainThread_DoWork);
            trainThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(trainThread_RunWorkerCompleted);

        }

        private void ImportFilesThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarImport.Value = 0;
        }

        private void ImportFilesThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarImport.Value = e.ProgressPercentage;
        }

        private void ImportFilesThread_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportFiles((string)e.Argument);
            
        }



        void trainThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("ok");
        }

        void trainThread_DoWork(object sender, DoWorkEventArgs e)
        {
            Process proc = null;
            try
            {
                var bat = String.Format("cd \"{0}\"\n", Application.StartupPath) + String.Format("\"{0}/darknet/darknet_no_gpu.exe\" detector train \"{0}/output/cfg/obj.data\" \"{0}/output/cfg/obj.cfg\" \"{0}/darknet19_448.conv.23\" -dont_show \n pause", Application.StartupPath);
                File.WriteAllText("train.bat", bat);

                proc = new Process();
                proc.StartInfo.FileName = @"train.bat";
                proc.StartInfo.Arguments = string.Format("10");//this is argument
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }


        #region 逻辑
        void CreateObjNames()
        {
            var labelMapPathContent = "";
            foreach (var item in listBoxLableIndex.Items)
            {
                labelMapPathContent += item.ToString() + "\n";
            }
            File.WriteAllText(objNamesPath, labelMapPathContent.Trim());

        }

        void SaveObjectToConfig()
        {
            string lines = "";
            foreach (var item in listBoxLableIndex.Items)
            {
                lines += item + Environment.NewLine;
            }
            Properties.Settings.Default.objects = lines;
            Properties.Settings.Default.Save();
        }

        void LoadObjectFromConfig()
        {
            foreach (var item in Properties.Settings.Default.objects.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                listBoxLableIndex.Items.Add(item);
            }
            if (listBoxLableIndex.Items.Count > 0)
            {
                listBoxLableIndex.SelectedIndex = 0;
            }

        }

        void LoadFiles()
        {
            listBoxFiles.Items.Clear();
            var dir = new DirectoryInfo(imagePath);
            foreach (var file in dir.GetFiles())
            {
                if (file.Extension == ".jpg")
                    listBoxFiles.Items.Add(file.FullName);
            }
        }

        void AppendLabelFile()
        {
            var txt = GetLableFilePath(listBoxFiles.SelectedItem.ToString());
            File.Delete(txt);
            var content = "";
            foreach (var item in listBoxLable.Items)
            {
                content += item + "\n";
            }
            File.AppendAllText(txt, content.Trim());
        }

        void CreateObjNamesFile()
        {
            string content = "";
            foreach (var item in listBoxLableIndex.Items)
            {
                content += item + "\n";
            }
            File.WriteAllText(objNamesPath, content.Trim());
        }

        void CreateCfgFile()
        {
            string cfgContent = File.ReadAllText("yolo-voc.cfg");
            cfgContent = cfgContent.Replace("classes=20", "classes=" + listBoxLableIndex.Items.Count).Replace("filters=125", "filters=30");
            var dirName = Path.GetDirectoryName(objCfgFilePath);
            if (Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            File.WriteAllText(objCfgFilePath, cfgContent.Trim());
        }

        void CrateObjdataFile()
        {
            string objDataFileContent = String.Format(File.ReadAllText("obj.data"), listBoxLableIndex.Items.Count, Application.StartupPath);
            File.WriteAllText(objDataFilePath, objDataFileContent.Trim());
        }

        void CreateTestTrainTxt()
        {
            List<string> filePaths = new List<string>();
            foreach (var item in listBoxFiles.Items)
            {
                filePaths.Add(item.ToString());
            }
            //filePaths.Sort(delegate(string a, string b) { return (new Random()).Next(-1, 1); });
            filePaths=filePaths.Select(a => new { a, newID = Guid.NewGuid() }).OrderBy(b => b.newID).Select(c => c.a).ToList();
            var testCount = (int)((double)filePaths.Count() * (int)numericUpDownPercent.Value / 100);
            var testTxtFileContent = "";
            var trainTxtFileContent = "";
            for (int i = 0; i < filePaths.Count(); i++)
            {
                if (i < testCount)
                {
                    testTxtFileContent += filePaths[i] + "\n";
                }
                else
                {
                    trainTxtFileContent += filePaths[i] + "\n";
                }
            }
            File.WriteAllText(testTxtFilePath, testTxtFileContent.Trim());
            File.WriteAllText(trainTxtFilePath, trainTxtFileContent.Trim());
        }


        void downloadImageThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarDownloadImage.Value = 0;
            buttonDownlaodImage.Text = "启动";
        }

        void downloadImageThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelCount.Text = ((int)e.ProgressPercentage).ToString();
            progressBarDownloadImage.Value = (int)((float)e.ProgressPercentage * 100 / (int)numericUpDownCount.Value);
        }

        void ClearSelect()
        {
            Rect.Size = new Size(0, 0);
            pictureBox1.Refresh();
        }

        string GetLableFilePath(string jpgFilePath)
        {
            return String.Format("{0}\\{1}.txt", Path.GetDirectoryName(jpgFilePath), Path.GetFileNameWithoutExtension(jpgFilePath));
        }

        void ImportFiles(string path)
        {
            
            if (Directory.Exists(path))
            {
                var importDir = new DirectoryInfo(path);
                var files = importDir.GetFiles();
                var count = files.Count();
                int index = 0;
                foreach (var file in files)
                {
                    index++;
                    if (file.Extension == ".jpg")
                    {
                        string desFilePath = imagePath + file.Name;
                        if (!File.Exists(desFilePath))
                        {
                            //File.Copy(file.FullName, imagePath + file.Name, true);
                            var image = Bitmap.FromFile(file.FullName);
                            var f = ZoomImage(new Bitmap(image), (int)numericUpDownImportHeight.Value, (int)numericUpDownImportHeight.Value);
                            f.Save(imagePath + file.Name, System.Drawing.Imaging.ImageFormat.Jpeg);
                            f.Dispose();
                            image.Dispose();
                            importFilesThread.ReportProgress((int)(((float)index/(float)count)*100));
                            FileInfo fi = new FileInfo(imagePath + file.Name);
                            if (fi.IsReadOnly)
                            {
                                fi.IsReadOnly = false;
                            }
                            BeginInvoke(new Action(()=> { listBoxFiles.Items.Add(imagePath + file.Name); }));
                        }
                    }
                }
            }
        }

        #endregion


        #region 事件
        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "";
            if (listBoxFiles.SelectedItem != null)
            {
                currentJPGName = Path.GetFileName(listBoxFiles.SelectedItem.ToString());
                var jpgPath = listBoxFiles.SelectedItem.ToString();
                pictureBox1.ImageLocation = jpgPath;
                var txt = jpgPath.Replace(".jpg", ".txt");
                listBoxLable.Items.Clear();
                if (File.Exists(txt))
                {
                    foreach (var item in File.ReadAllLines(txt.Trim()))
                    {
                        listBoxLable.Items.Add(item);
                    }
                }
                ClearSelect();
            }
        }



        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RectStartPoint = e.Location;
            Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            Debug.WriteLine(e.Location.ToString());
            Point tempEndPoint = e.Location;
            Rect.Location = new Point(
                Math.Min(RectStartPoint.X, tempEndPoint.X),
                Math.Min(RectStartPoint.Y, tempEndPoint.Y));
            Rect.Size = new Size(
                Math.Abs(RectStartPoint.X - tempEndPoint.X),
                Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                {
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listBoxLableIndex.Items.Count == 0)
                {
                    MessageBox.Show("请先在界面右上角处添加对象，比如car");
                    ClearSelect();
                    return;
                }
                if (listBoxLableIndex.SelectedIndex == -1)
                {
                    MessageBox.Show("请先在界面右上角对象列表里选择一个对象");
                    ClearSelect();
                    return;
                }
                if (RectStartPoint == e.Location)
                {
                    return;
                }

                if (RectStartPoint.X > e.Location.X || RectStartPoint.Y > e.Location.Y)
                {
                    MessageBox.Show("亲,只能从左上向右下选");
                    ClearSelect();
                    return;
                }

                var dw = (double)1 / (double)pictureBox1.Width;
                var dh = (double)1 / (double)pictureBox1.Height;
                var x = ((double)RectStartPoint.X + (double)e.Location.X) / 2;
                var y = ((double)RectStartPoint.Y + (double)e.Location.Y) / 2;
                var w = (double)e.Location.X - (double)RectStartPoint.X;
                var h = (double)e.Location.Y - (double)RectStartPoint.Y;
                x = x * dw;
                w = w * dw;
                y = y * dh;
                h = h * dh;
                listBoxLable.Items.Add(String.Format("{0} {1} {2} {3} {4}", listBoxLableIndex.SelectedIndex, x, y, w, h));
                AppendLabelFile();
            }
            if (e.Button == MouseButtons.Right)
            {

                AppendLabelFile();
                if (listBoxFiles.Items.Count - 1 > listBoxFiles.SelectedIndex)
                {
                    var index = listBoxFiles.SelectedIndex;
                    listBoxFiles.ClearSelected();
                    listBoxFiles.SelectedIndex = index + 1;
                }
                listBoxLable.Items.Clear();
                ClearSelect();

            }
        }

        private void listBoxLable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBoxLable.SelectedItem != null)
                {
                    listBoxLable.Items.RemoveAt(listBoxLable.SelectedIndex);
                    if (listBoxLable.Items.Count > 0)
                    {
                        AppendLabelFile();
                    }
                    else
                    {
                        File.Delete(GetLableFilePath(listBoxFiles.SelectedItem.ToString()));
                    }
                }
            }
        }
        private void listBoxFiles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBoxFiles.SelectedItems != null)
                {
                    if (listBoxFiles.SelectedItems.Count == 1)
                    {
                        FileInfo fi = new FileInfo(listBoxFiles.SelectedItem.ToString());
                        if (fi.IsReadOnly)
                        {
                            fi.IsReadOnly = false; 

                        }
                        File.Delete(listBoxFiles.SelectedItem.ToString());
                        File.Delete(listBoxFiles.SelectedItem.ToString().Replace(".jpg", ".txt"));
                        listBoxLable.Items.Clear();
                        int i = listBoxFiles.SelectedIndex;
                        listBoxFiles.Items.RemoveAt(listBoxFiles.SelectedIndex);
                        if (i < listBoxFiles.Items.Count)
                        {
                            listBoxFiles.SelectedIndex = i;
                        }
                    }
                    else
                    if (listBoxFiles.SelectedItems.Count == 0)
                    {
                        return;
                    }
                    else
                    {
                        foreach (var item in listBoxFiles.SelectedItems)
                        {
                            FileInfo fi = new FileInfo(item.ToString());
                            if (fi.IsReadOnly)
                            {
                                fi.IsReadOnly = false;
                            }
                            File.Delete(item.ToString());
                            File.Delete(item.ToString().Replace(".jpg", ".txt"));
                        }

                        listBoxLable.Items.Clear();
                        listBoxFiles.Items.Clear();
                        var dir = new DirectoryInfo(imagePath);
                        foreach (var file in dir.GetFiles())
                        {
                            if (file.Extension == ".jpg")
                            {
                                listBoxFiles.Items.Add(file.FullName);
                            }
                        }
                    }
                    CreateTestTrainTxt();
                }
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                for (int i = 0; i < listBoxFiles.Items.Count; i++)
                {
                    listBoxFiles.SelectedIndex = i;
                }
             }


        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (var item in listBoxFiles.Items)
            {
                var txtFile = item.ToString().Replace(".jpg", ".txt").Replace(".JPG", ".txt");
                if (item.ToString().Contains("6733"))
                {
                    int i = 111;
                    i++;
                }
                if (!File.Exists(txtFile))
                {
                    File.Delete(item.ToString());
                }
                else
                {
                    if (item.ToString().EndsWith(".JPG"))
                    {
                        File.Move(item.ToString(), item.ToString().Replace(".JPG", ".jpg"));
                    }

                }
            }
            LoadFiles();
        }

        private void buttonAddObj_Click(object sender, EventArgs e)
        {
            var obj = textBoxAddObj.Text.Trim();
            if (obj.Contains(' '))
            {
                MessageBox.Show("对象不允许带空格");
            }
            if (obj != "")
            {
                listBoxLableIndex.Items.Add(obj);
                listBoxLableIndex.SelectedIndex = listBoxLableIndex.Items.Count - 1;
                SaveObjectToConfig();
            }
            CreateObjNames();
        }

        private void listBoxLableIndex_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBoxLableIndex.SelectedItem != null)
                {
                    int i = listBoxLableIndex.SelectedIndex;
                    listBoxLableIndex.Items.RemoveAt(listBoxLableIndex.SelectedIndex);
                    if (i < listBoxLableIndex.Items.Count)
                    {
                        listBoxLableIndex.SelectedIndex = i;
                    }
                    SaveObjectToConfig();
                    CreateObjNames();
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            ImportFiles(textBoxImport.Text);
            Properties.Settings.Default.imagePath = textBoxImport.Text;
            Properties.Settings.Default.Save();
            CreateTestTrainTxt();
        }




        private void listBoxLableIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentObjIndex = listBoxLableIndex.SelectedIndex;
            currentObjName = listBoxLableIndex.Text;
        }

        private void listBoxLable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxLable.SelectedIndex != -1)
            {
                var line = listBoxLable.SelectedItem.ToString();
                if (line != "")
                {
                    var items = line.Split(' ');
                    MessageBox.Show(String.Format("objIndex={0}\nx={1}\nwidth={2}\ny={3}\nheight={4}",
                        items[0], items[1], items[2], items[3], items[4]), "样本标注信息");
                }
            }
        }



        private void buttonOpenDownloadPath_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", imageDownloadPath);
        }

        private void buttonClearDownloadFolder_Click(object sender, EventArgs e)
        {
            foreach (var item in new DirectoryInfo(imageDownloadPath).GetFiles())
            {
                File.Delete(item.FullName);
            }
        }

        private void buttonImportDownload_Click(object sender, EventArgs e)
        {
            ImportFiles(imageDownloadPath);
            tabControl1.SelectedIndex = 0;
        }

        private void buttonOpenImageFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", imagePath);
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            CreateCfgFile();
            CrateObjdataFile();
            CreateObjNamesFile();
            CreateTestTrainTxt();
            System.Diagnostics.Process.Start("explorer.exe", outputPath);
        }
        #endregion


        #region 图片下载

        private void buttonDownlaodImage_Click(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Trim() == "")
            {
                MessageBox.Show("请填搜索关键字");
                return;
            }
            imageDownloadCount = (int)numericUpDownCount.Value;
            imageDownloadKey = textBoxKey.Text;
            imageDownloadPre = textBoxPreName.Text;
            zoomHeight = (int)numericUpDownZoomHeight.Value;
            zoomWidth = (int)numericUpDownZoomWidth.Value;
            if (buttonDownlaodImage.Text == "启动")
            {
                if (downloadImageThread.IsBusy)
                {
                    MessageBox.Show("线程还没停止，请稍候再试");
                }
                else
                {
                    isDownloadImageThreadRun = true;
                    downloadImageThread.RunWorkerAsync();
                    buttonDownlaodImage.Text = "停止";

                }
            }
            else
            {
                isDownloadImageThreadRun = false;
            }

        }


        void downloadImageThread_DoWork(object sender, DoWorkEventArgs e)
        {
            int imageCount = 0;
            for (int i = 0; i < MaxPageIndex; i++)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://image.baidu.com/search/avatarjson?tn=resultjsonavatarnew&ie=utf-8&word="
                    + Uri.EscapeUriString(imageDownloadKey) + "&cg=girl&pn=" + (i + 1) * 60 + "&rn=60&itg=0&z=0&fr=&width=&height=&lm=-1&ic=0&s=0&st=-1&gsm=360600003c");
        
    
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = res.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string json = reader.ReadToEnd();
                                JObject objRoot = (JObject)JsonConvert.DeserializeObject(json);
                                JArray imgs = (JArray)objRoot["imgs"];
                                for (int j = 0; j < imgs.Count; j++)
                                {
                                    if (!isDownloadImageThreadRun)
                                    {
                                        return;
                                    }
                                    JObject img = (JObject)imgs[j];
                                    string objUrl = (string)img["objURL"];
                                    try
                                    {
                                        string path = String.Format("{0}/{1}.jpg", imageDownloadPath, imageDownloadPre + imageCount.ToString());
                                        HttpWebRequest reqImage = (HttpWebRequest)WebRequest.Create(objUrl);
                                        reqImage.Referer = "http://image.baidu.com/";
                                        reqImage.UserAgent = userAgent[new Random(DateTime.Now.Millisecond).Next(userAgent.Length)];
                                        using (HttpWebResponse resImage = (HttpWebResponse)reqImage.GetResponse())
                                        {
                                            if (resImage.StatusCode == HttpStatusCode.OK)
                                            {
                                                using (Stream streamImage = resImage.GetResponseStream())
                                                {
                                                    Bitmap b = new Bitmap(streamImage);
                                                    b = ZoomImage(b, zoomHeight, zoomWidth);
                                                    b.Save("temp.jpg");
                                                    var gram = GetHisogram(b);
                                                    if (ExistSimlator(gram))
                                                    {
                                                        continue;
                                                    }

                                                    b.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                    picPaths.Add(path);
                                                    imageCount++;
                                                    downloadImageThread.ReportProgress(imageCount);
                                                    if (imageCount >= imageDownloadCount)
                                                    {
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        BeginInvoke(new Action(() =>
                                        {
                                            richTextBoxInfo.AppendText(ex.Message + Environment.NewLine);
                                        }));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }

                }
            }
        }
        private Bitmap ZoomImage(Bitmap bitmap, int destHeight, int destWidth)
        {
            try
            {
                System.Drawing.Image sourImage = bitmap;
                int width = 0, height = 0;
                //按比例缩放             
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                Bitmap destBitmap = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);
                //设置画布的描绘质量           
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.DrawImage(sourImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                //设置压缩质量       
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                bitmap.Dispose();
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }

        List<int[]> grams = new List<int[]>();
        List<string> picPaths = new List<string>();
        bool ExistSimlator(int[] newPic)
        {
            int i = 0;
            foreach (var item in grams)
            {
                var diff = GetResult(item, newPic);
                Console.WriteLine(diff);
                if (diff > 0.8)
                {
                    return true;
                }
                i++;
            }
            grams.Add(newPic);

            return false;
        }
        public int[] GetHisogram(Bitmap img)
        {

            BitmapData data = img.LockBits(new System.Drawing.Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int[] histogram = new int[256];
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 3;
                for (int i = 0; i < histogram.Length; i++)
                    histogram[i] = 0;
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        int mean = ptr[0] + ptr[1] + ptr[2];
                        mean /= 3;
                        histogram[mean]++;
                        ptr += 3;
                    }
                    ptr += remain;
                }
            }
            img.UnlockBits(data);
            return histogram;

        }


        private float GetAbs(int firstNum, int secondNum)
        {
            float abs = Math.Abs((float)firstNum - (float)secondNum);
            float result = Math.Max(firstNum, secondNum);
            if (result == 0)
                result = 1;
            return abs / result;

        }

        public float GetResult(int[] firstNum, int[] scondNum)
        {
            if (firstNum.Length != scondNum.Length)
            {
                return 0;
            }
            else
            {
                float result = 0;
                int j = firstNum.Length;
                for (int i = 0; i < j; i++)
                {
                    result += 1 - GetAbs(firstNum[i], scondNum[i]);
                }
                return result / j;
            }
        }


        #endregion


        private void linkLabelVideo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://edu.csdn.net/course/detail/7620");
        }

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sanfooh/quick_yolo2_label_tool");
        }

        private void buttonTrain_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(darknetFolder))
            {
                trainThread.RunWorkerAsync();
            }
            

        }

        private void linkLabelYolo2Weight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label1Yolo2Weight.Text=="未下载")
            {
                DownloadHelper  helper = new DownloadHelper();
                helper.Start("https://pjreddie.com/media/files/darknet19_448.conv.23", preWeightPath + "\\darknet19_448.conv.23", progressBarYolo2Weight, label1Yolo2Weight);
            }
           
        }

        private void linkLabelYolo3Weight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label1Yolo3Weight.Text == "未下载")
            {
                DownloadHelper helper = new DownloadHelper();
                helper.Start("https://pjreddie.com/media/files/darknet53.conv.74", preWeightPath + "\\darknet53.conv.74", progressBarYolo3Weight, label1Yolo3Weight);
            }
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                if (File.Exists(preWeightPath + "\\darknet19_448.conv.23"))
                {
                    label1Yolo2Weight.Text = "已下载";
                }
                if (File.Exists(preWeightPath + "\\darknet53.conv.74"))
                {
                    label1Yolo3Weight.Text = "已下载";
                }
            }
        }



        
       
    }
}
