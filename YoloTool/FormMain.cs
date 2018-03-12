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

namespace YoloTool
{
    public partial class FormMain : Form
    {
        string dataPath = "work\\data\\";
        public FormMain()
        {
            InitializeComponent();
        }


        private void buttonLoad_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists("obj.names"))
            {
                File.Create("obj.names").Close();
            }
            foreach (var item in File.ReadAllLines("obj.names"))
            {
                listBoxLableIndex.Items.Add(item);

            }
            if (listBoxLableIndex.Items.Count > 0)
            {
                listBoxLableIndex.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("obj.names not exist");
            }

            listBoxFiles.Items.Clear();
            var dir = new DirectoryInfo(dataPath);
            foreach (var file in dir.GetFiles())
            {
                if (file.Extension == ".jpg" || file.Extension == ".JPG")
                    listBoxFiles.Items.Add(file.FullName);
            }


        }



        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                //Image img= Image.FromFile(listBoxFiles.SelectedItem.ToString());

                //pictureBox1.Image = ZoomPicture(img, 400, 400);
                var jpgPath = listBoxFiles.SelectedItem.ToString();
                pictureBox1.ImageLocation = jpgPath;
                var txt = jpgPath.Replace(".jpg", ".txt");
                listBoxLable.Items.Clear();
                if (File.Exists(txt))
                {
                    foreach (var item in File.ReadAllLines(txt))
                    {
                        listBoxLable.Items.Add(item);
                    }

                }
            }
        }
        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        // Start Rectangle
        //
        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Determine the initial rectangle coordinates...
            RectStartPoint = e.Location;
            Invalidate();
        }

        // Draw Rectangle
        //
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

        // Draw Area
        //
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Draw the rectangle...
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
            //from https://github.com/Guanghan/darknet/blob/master/scripts/convert.py
            if (e.Button == MouseButtons.Left)
            {
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
            }
            if (e.Button == MouseButtons.Right)
            {
                //if (Rect.Contains(e.Location))
                //{
                //    Debug.WriteLine("Right click");
                //}

                var txt = GetLableFilePath(listBoxFiles.SelectedItem.ToString());
                File.Delete(txt);
                foreach (var item in listBoxLable.Items)
                {
                    File.AppendAllText(txt, item + Environment.NewLine);
                }
                if (listBoxFiles.Items.Count - 1 > listBoxFiles.SelectedIndex)
                {
                    var index = listBoxFiles.SelectedIndex;
                    listBoxFiles.ClearSelected();
                    listBoxFiles.SelectedIndex = index+1;
                }
                listBoxLable.Items.Clear();
                Rect.Size = new Size(0, 0);

            }
        }

        private void listBoxLable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBoxLable.SelectedItem != null)
                {
                    listBoxLable.Items.RemoveAt(listBoxLable.SelectedIndex);
                }
            }
        }



        string GetLableFilePath(string jpgFilePath)
        {
            return String.Format("{0}\\{1}.txt", Path.GetDirectoryName(jpgFilePath), Path.GetFileNameWithoutExtension(jpgFilePath));
        }


        void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string slash = radioButtonLinux.Checked ? "/" : "\\";

            var root = Application.StartupPath;
            var workDirection = root + "\\work\\";
            var workDataDirection = root + "\\work\\data\\";
            var workModelBackupDirectin = root + "\\work\\backup\\";
            var workCfgDirection = workDirection + "cfg\\";


            var desWorkDirection = textBoxRoot.Text + "work" + slash;
            var desWorkDataDirection = desWorkDirection + "data" + slash;
            var desModelBackupDirectin = desWorkDirection + "backup" + slash;
            var desCfgDirection = desWorkDirection + "cfg" + slash;

            var trainTxtFile = workDirection + "train.txt";
            var testTxtFile = workDirection + "test.txt";
            var objNameFile = workCfgDirection + "obj.names";
            var objDataFile = workCfgDirection + "obj.data";
            var objCfgFile = workCfgDirection + "YOLO-obj.cfg";


            Directory.CreateDirectory(workCfgDirection);

            Directory.CreateDirectory(workDirection);
            File.Delete(trainTxtFile);
            File.Delete(testTxtFile);

            List<int> testIndex = new List<int>();
            var count = listBoxFiles.Items.Count;
            int testCount = (int)(double.Parse(textBoxPercent.Text) / 100 * count);
            while (testIndex.Count < testCount)
            {
                Random ro = new Random();
                var rand = ro.Next(count);
                if (!testIndex.Contains(rand))
                {
                    testIndex.Add(rand);
                }
            }
            string testContent = "";
            string trainContent = "";

            for (int i = 0; i < listBoxFiles.Items.Count; i++)
            {
                var fileName = Path.GetFileName(listBoxFiles.Items[i].ToString());

                if (testIndex.Contains(i))
                {
                    testContent += desWorkDataDirection + fileName + "\n";
                }
                else
                {
                    trainContent += desWorkDataDirection + fileName + "\n";
                }
            }

            File.WriteAllText(testTxtFile, testContent.Trim());
            File.WriteAllText(trainTxtFile, trainContent.Trim());


            string objNamesContent = "";
            foreach (var item in listBoxLableIndex.Items)
            {
                objNamesContent += item.ToString() + Environment.NewLine;
            }
            File.WriteAllText(objNameFile, objNamesContent.Trim());
            string objDataFileContent = String.Format(File.ReadAllText("obj.data"), listBoxLableIndex.Items.Count, desWorkDirection, desCfgDirection, desModelBackupDirectin);

            File.WriteAllText(objDataFile, objDataFileContent.Trim());


            string cfgContent = File.ReadAllText("yolo-voc.cfg");
            cfgContent = cfgContent.Replace("classes=20", "classes=" + listBoxLableIndex.Items.Count).Replace("filters=125", "filters=30");
            File.WriteAllText(objCfgFile, cfgContent.Trim());


            Directory.CreateDirectory(workModelBackupDirectin);
            if (!File.Exists("work\\darknet19_448.conv.23"))
            {
                if (File.Exists("darknet19_448.conv.23"))
                {
                    File.Copy("darknet19_448.conv.23", "work\\darknet19_448.conv.23", false);
                }
            }
            if (radioButtonLinux.Checked)
            {
                textBoxTrain.Text = "./darknet detector train work/cfg/obj.data work/cfg/YOLO-obj.cfg work/darknet19_448.conv.23";
            }
            else
            {
                textBoxTrain.Text = "darknet.exe detector train work\\cfg\\obj.data work\\cfg\\YOLO-obj.cfg work\\darknet19_448.conv.23";
            }

            MessageBox.Show("生成完成，将整个work目录拷贝到darknet目录下，并执行右下角命令进行训练");

        }

        private void listBoxFiles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBoxFiles.SelectedItems != null)
                {
                    if (listBoxFiles.SelectedItems.Count == 1)
                    {
                        File.Delete(listBoxFiles.SelectedItem.ToString());
                        File.Delete(listBoxFiles.SelectedItem.ToString().Replace(".jpg", ".txt"));
                        int i = listBoxFiles.SelectedIndex;
                        listBoxFiles.Items.RemoveAt(listBoxFiles.SelectedIndex);
                        if (i < listBoxFiles.Items.Count)
                        {
                            listBoxFiles.SelectedIndex = i;
                        }
                    }
                    else
                    {

                        foreach (var item in listBoxFiles.SelectedItems)
                        {
                            File.Delete(item.ToString());
                            File.Delete(item.ToString().Replace(".jpg", ".txt"));
                        }
                        listBoxFiles.Items.Clear();
                        var dir = new DirectoryInfo(dataPath);
                        foreach (var file in dir.GetFiles())
                        {
                            if (file.Extension == ".jpg")
                            {
                                listBoxFiles.Items.Add(file.FullName);
                            }
                        }
                    }

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
        }

        private void buttonAddObj_Click(object sender, EventArgs e)
        {
            var obj = textBoxAddObj.Text.Trim();
            if (obj != "")
            {
                listBoxLableIndex.Items.Add(obj);
                File.Delete("obj.names");
                foreach (var item in listBoxLableIndex.Items)
                {
                    File.WriteAllText("obj.names", item + "\\n");
                }
            }
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
                    File.Delete("obj.names");
                    foreach (var item in listBoxLableIndex.Items)
                    {
                        File.WriteAllText("obj.names", item + "\n");
                    }
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {

            var importDir = new DirectoryInfo(textBoxImport.Text);
            foreach (var file in importDir.GetFiles())
            {
                if (file.Extension == ".jpg")
                {
                    File.Copy(file.FullName, dataPath + file.Name, true);
                }
            }


            listBoxFiles.Items.Clear();
            var dir = new DirectoryInfo(dataPath);
            foreach (var file in dir.GetFiles())
            {
                if (file.Extension == ".jpg")
                {
                    listBoxFiles.Items.Add(file.FullName);
                }
            }
        }


    }
}
