using CheckRanking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WelfareSearchingTool
{
    public partial class MainForm : Form
    {
        private string m_Key;

        private string m_RandCodePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rand.jpg");
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_Key = GetHardwareId() + "00000000";
            if (m_Key.Length > 8)
            {
                m_Key = m_Key.Substring(0, 8);
            }
            btnCheck.Enabled = false;
            this.Text = "查询公租房排名信息 - 加载中";
            RestoreUser();
            LoadRanking();
            Task.Factory.StartNew(() =>
            {
                CallResource();
            }).ContinueWith((t) =>
            {
                this.Text = "查询公租房排名信息";
                btnCheck.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void CallResource()
        {
            if (File.Exists(m_RandCodePath))
            {
                File.Delete(m_RandCodePath);
            }
            if (File.Exists(m_ResultPath))
            {
                File.Delete(m_ResultPath);
            }
            ProcessStartInfo info = new ProcessStartInfo("CheckRanking.exe", "randCode");
            info.WindowStyle = ProcessWindowStyle.Hidden;
            var process = Process.Start(info);
            process.WaitForExit();
            if (!File.Exists(m_RandCodePath))
            {
                MessageBox.Show("没有读取到验证码");
            }
            else {
                ;
                using (var ms = new MemoryStream(File.ReadAllBytes(m_RandCodePath)))
                {
                    pbRandCode.Image = new Bitmap(ms);
                }

            }
        }

        private readonly string m_ResultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "message.json");

        private void CallResourceForChecking()
        {
            ResultCodeType resultCode = ResultCodeType.Success;

            Task.Factory.StartNew(() =>
            {
                ProcessStartInfo info = new ProcessStartInfo("CheckRanking.exe", string.Format("check {0} {1} {2}", tbUserName.Text, tbPassword.Text, tbRandCode.Text));
                info.WindowStyle = ProcessWindowStyle.Hidden;
                var process = Process.Start(info);
                process.WaitForExit();
                resultCode = (ResultCodeType)process.ExitCode;
            }).ContinueWith((t) =>
            {
                btnCheck.Text = "查询排名";
                if (File.Exists(m_ResultPath))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(File.ReadAllText(m_ResultPath), new
                    {
                        Name = string.Empty,
                        RankingNum = 0,
                        Id = string.Empty,
                        RecvNum = string.Empty,
                        RendNum = 0,
                        Area = string.Empty
                    });
                    tbResult_Name.Text = result.Name;
                    tbResult_RankingNum.Text = result.RankingNum.ToString();
                    tbResult_Id.Text = result.Id;
                    tbResult_Area.Text = result.Area;
                    tbResult_recvNum.Text = result.RecvNum;
                    SaveRanking(result.RankingNum);
                    if (cbIsSave.Checked)
                    {
                        SaveUser();
                    }
                }
                else
                {
                    switch (resultCode)
                    {
                        case ResultCodeType.UserNameOrPasswordFailed:
                            MessageBox.Show("用户名或密码有误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ResultCodeType.PageLoadFailed:
                            MessageBox.Show("页面无法加载成功，请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ResultCodeType.CannotCatchingMessage:
                            MessageBox.Show("无法读取到排名信息，可能系统改版，请跟作者反馈问题", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case ResultCodeType.ValidateCodeFailed:
                            MessageBox.Show("验证码错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("读取数据失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                    
                }
                CallResource();
                btnCheck.Enabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void pbRandCode_Click(object sender, EventArgs e)
        {
            this.Text = "查询公租房排名信息 - 加载中";
            CallResource();
            this.Text = "查询公租房排名信息";
        }

        private static string _Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user");

        private void SaveUser()
        {
            string userName = tbUserName.Text;
            string password = tbPassword.Text;

            string desUserName = EncryptString(userName, m_Key);
            string desPassword = EncryptString(password, m_Key);


            string content = desUserName + "$" + desPassword;
            File.WriteAllBytes(_Path, Encoding.UTF8.GetBytes(content));
        }



        private void btnCheck_Click(object sender, EventArgs e)
        {
            
            btnCheck.Text = "查询中";
            btnCheck.Enabled = false;
            CallResourceForChecking();
        }

        private void RestoreUser()
        {
            if (!File.Exists(_Path))
                return;
            try
            {
                var b = File.ReadAllBytes(_Path);
                string msg = Encoding.UTF8.GetString(b);
                string[] arr = msg.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 2)
                {
                    tbUserName.Text = DecryptString(arr[0], m_Key);
                    tbPassword.Text = DecryptString(arr[1], m_Key);
                }
            }
            catch { }
        }

        private string EncryptString(string sinputString, string key)
        {
            byte[] data = Encoding.UTF8.GetBytes(sinputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);
            ICryptoTransform desEncrypt = DES.CreateEncryptor();
            byte[] result = desEncrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }

        private string DecryptString(string sinputString, string key)
        {
            string[] sinput = sinputString.Split("-".ToCharArray());
            byte[] data = new byte[sinput.Length];
            for (int i = 0; i < sinput.Length; i++)
            {
                data[i] = byte.Parse(sinput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }

        private void 软件说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        private static string _RFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ranking");
        private void SaveRanking(int num)
        {
            File.WriteAllText(_RFilePath, num.ToString());
        }
        private void LoadRanking()
        {
            if (!File.Exists(_RFilePath))
                return;
            int n = int.Parse(File.ReadAllText(_RFilePath));
            lbLastRankingNum.Text = "上次查询的排名:" + n;
            lbLastRankingNum.ForeColor = Color.Green;
        }

        private string GetHardwareId()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            foreach (ManagementObject mo in searcher.Get())
            {
                return mo["SerialNumber"].ToString().Trim();

            }
            return string.Empty;
        }
    }
}
