using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.CORE.Utils
{
    class CmdUtils
    {
        public static void RunExe(string command, string args, string workdir, out string output, out string error)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = Path.Combine(workdir, command);
                process.StartInfo.Arguments = args;
                // 必须禁用操作系统外壳程序  
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();

                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();

                process.WaitForExit();
                process.Close();
            }
        }
        /// <summary>
        /// 执行一条command命令
        /// </summary>
        /// <param name="command">需要执行的Command</param>
        /// <param name="output">输出</param>
        /// <param name="error">错误</param>
        public static void ExecuteCommand(string command, string args, string workdir, out string output, out string error)
        {
            try
            {
                //创建一个进程
                Process process = new Process();
                process.StartInfo.FileName = Path.Combine(workdir, command);
                process.StartInfo.Arguments = args;

                // 必须禁用操作系统外壳程序
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WorkingDirectory = workdir;


                //启动进程
                process.Start();

                //准备读出输出流和错误流
                string outputData = string.Empty;
                string errorData = string.Empty;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.OutputDataReceived += (sender, e) =>
                {
                    outputData += (e.Data + "\n");
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    errorData += (e.Data + "\n");
                };

                //等待退出
                process.WaitForExit();

                //关闭进程
                process.Close();

                //返回流结果
                output = outputData;
                error = errorData;
            }
            catch (Exception)
            {
                output = null;
                error = null;
            }
        }
    }
}
