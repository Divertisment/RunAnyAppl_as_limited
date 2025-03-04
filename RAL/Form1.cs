﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Ookii.Dialogs.WinForms;

namespace RAL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (
                Properties.Settings.Default.user != "" && Properties.Settings.Default.password != ""
            )
                try
                {
                    textBox1.Text = Properties.Settings.Default.user;
                    textBox2.Text = Properties.Settings.Default.password;
                    pictureBox1.BackgroundImage = Properties.Resources.ok48;
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    button1.Enabled = false;
                }
                catch { }
            else
                try
                {
                    pictureBox1.BackgroundImage = Properties.Resources.no48;
                    textBox1.ReadOnly = false;
                    textBox2.ReadOnly = false;
                    button1.Enabled = true;
                }
                catch { }
            ;
        }
        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = CS_DropShadow;
                return cp;
            }
        }
        private void button1_Click(object sender, EventArgs e) //create
        {
            if (textBox1.Text != "" && textBox2.Text != "")
                try
                {
                    var proc1 = new ProcessStartInfo();
                    string Command;
                    proc1.UseShellExecute = true;
                    Command = @"net user " + textBox1.Text + " " + textBox2.Text + " /add";
                    proc1.WorkingDirectory = @"C:\Windows\System32";
                    proc1.FileName = @"C:\Windows\System32\cmd.exe";
                    proc1.Verb = "runas";
                    proc1.Arguments = "/c " + Command;
                    proc1.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(proc1);
                    Properties.Settings.Default.user = textBox1.Text;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.password = textBox2.Text;
                    Properties.Settings.Default.Save();
                    pictureBox1.BackgroundImage = Properties.Resources.ok48;
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    button1.Enabled = false;
                }
                catch { }
        }

        private void button5_Click(object sender, EventArgs e) //remove (user)
        {
            var proc1 = new ProcessStartInfo();
            string Command;
            proc1.UseShellExecute = true;
            Command = @"net user " + textBox4.Text + " /delete";
            proc1.WorkingDirectory = @"C:\Windows\System32";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/c " + Command;
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc1);
        }

        List<Process> processlist = new List<Process>();
        private void button3_Click(object sender, EventArgs e) // exit
        {
            foreach (var process in Process.GetProcessesByName("cmd"))
            {
                process.Kill();
            }
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e) // get user info
        {
            var proc1 = new ProcessStartInfo();
            string Command;
            proc1.UseShellExecute = true;
            Command = @"net user " + textBox3.Text + " && pause";
            proc1.WorkingDirectory = @"C:\Windows\System32";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/c " + Command;
            proc1.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(proc1);
        }

        private void button6_Click(object sender, EventArgs e) // get list of all users
        {
            var proc1 = new ProcessStartInfo();
            string Command;
            proc1.UseShellExecute = true;
            Command = @"net user && pause";
            proc1.WorkingDirectory = @"C:\Windows\System32";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/c " + Command;
            proc1.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(proc1);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button9_Click(object sender, EventArgs e) //reset
        {
            var proc1 = new ProcessStartInfo();
            string Command;
            proc1.UseShellExecute = true;
            Command = @"net user " + Properties.Settings.Default.user + " /delete";
            proc1.WorkingDirectory = @"C:\Windows\System32";
            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/c " + Command;
            proc1.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(proc1);
            textBox1.Text = "";
            textBox2.Text = "";
            Properties.Settings.Default.user = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.password = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.fifle = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.exename = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.fifleN = "";
            Properties.Settings.Default.Save();
            Properties.Settings.Default.exenameV2 = "";
            Properties.Settings.Default.Save();
            pictureBox1.BackgroundImage = Properties.Resources.no48;
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e) //v1 run
        {
            if (Properties.Settings.Default.fifleN == "")
                try
                {
                    VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();

                    dlg.ShowNewFolderButton = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string fifleN = dlg.SelectedPath.Replace("\u005C", "\u002F");
                        Properties.Settings.Default.fifleN = fifleN;
                        Properties.Settings.Default.Save();
                    }
                }
                catch { }
            ;
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var ressss = string.Empty;
            if (Properties.Settings.Default.exename == "")
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Exe file (*.exe)|*.exe";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            filePath = openFileDialog.FileName;
                            ressss = Path.GetFileName(filePath);
                            string applicationPath = Path.GetFullPath(
                                Properties.Settings.Default.fifleN
                            );
                            string saveFilePath = Path.Combine(applicationPath, ressss);
                            Properties.Settings.Default.exename = filePath;
                            Properties.Settings.Default.Save();
                        }
                    }
                }
                catch { }
            ;
            if (
                Properties.Settings.Default.user == "" && Properties.Settings.Default.password == ""
            )
            {
                MessageBox.Show("Create USER !!!");
            }

            if (
                Properties.Settings.Default.user != ""
                && Properties.Settings.Default.fifleN != ""
                && Properties.Settings.Default.exename != ""
            )
                try
                {
                    string filfe = Properties.Settings.Default.fifleN;
                    string exename = Properties.Settings.Default.exename;
                    string userStr = Properties.Settings.Default.user;
                    string userPwd = Properties.Settings.Default.password;
                    string path = System.IO.Directory.GetCurrentDirectory();
                    string filename = userStr + "-V1.bat";
                    using (StreamWriter writer = File.CreateText(filename))
                    {
                        writer.WriteLine(
                            "C:\\Windows\\System32\\runas.exe /user:"
                                + userStr
                                + " /savecred "
                                + '\u0022'
                                + "cmd /C cd \u005C\u0022"
                                + filfe
                                + "\u002F"
                                + "\u005C"
                                + "\" && "
                                + exename
                                + "\u0022"
                        );
                    }
                    Process ps = new Process();
                    ps.StartInfo.FileName = filename;
                    // add process to list
                    processlist.Add(ps);
                    ps.Start();
                }
                catch { }
            ;
        }

        private void button7_Click(object sender, EventArgs e) //v2 run
        {
            if (Properties.Settings.Default.fifleN == "")
                try
                {
                    VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();

                    dlg.ShowNewFolderButton = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string fifleN = dlg.SelectedPath.Replace("\u005C", "\u002F");
                        Properties.Settings.Default.fifleN = fifleN;
                        Properties.Settings.Default.Save();
                    }
                }
                catch { }
            ;
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var ressss = string.Empty;
            if (Properties.Settings.Default.exenameV2 == "")
                try
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Exe file (*.exe)|*.exe";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Get the path of specified file
                            filePath = openFileDialog.FileName;
                            ressss = Path.GetFileName(filePath);
                            Properties.Settings.Default.exenameV2 = ressss;
                            Properties.Settings.Default.Save();
                        }
                    }
                }
                catch { }
            ;
            if (
                Properties.Settings.Default.user == "" && Properties.Settings.Default.password == ""
            )
            {
                MessageBox.Show("Create USER !!!");
            }

            if (
                Properties.Settings.Default.user != ""
                && Properties.Settings.Default.fifleN != ""
                && Properties.Settings.Default.exename != ""
            )
                try
                {
                    string filfe = Properties.Settings.Default.fifleN;
                    string exenameV2 = Properties.Settings.Default.exenameV2;
                    string userStr = Properties.Settings.Default.user;
                    string userPwd = Properties.Settings.Default.password;
                    string path = System.IO.Directory.GetCurrentDirectory();
                    string filename = userStr + "-V2.bat";
                    using (StreamWriter writer = File.CreateText(filename))
                    {
                        writer.WriteLine(
                            "C:\\Windows\\System32\\runas.exe /user:"
                                + userStr
                                + " /savecred "
                                + '\u0022'
                                + "cmd /C cd \u005C\u0022"
                                + filfe
                                + "\u002F"
                                + "\u005C"
                                + "\" && "
                                + exenameV2
                                + "\u0022"
                        );
                    }
                    Process ps = new Process();
                    ps.StartInfo.FileName = filename;
                    // add process to list
                    processlist.Add(ps);
                    ps.Start();
                }
                catch { }
            ;
        }
    }
    public static class OpenFileDialogExtensions
    {
        public static string ShowDialogAndReturnFileName(this OpenFileDialog dialog)
        {
            dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
