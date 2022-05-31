using LD.CORE.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.CORE
{
    
        /// <summary>
        /// 雷电操作接口
        /// </summary>
        internal interface ILD
        {
            /// <summary>
            /// 获取雷电模拟器列表
            /// </summary>
            /// <param name="lddir">雷电模拟器安装位置</param>
            /// <returns></returns>
            Task<List<LDM>> GetLDListAsync();

            /// <summary>
            /// 重置雷电模拟器网络
            /// </summary>
            /// <param name="dM">雷电模拟器</param>
            /// <param name="dmNo">雷电模拟器编号</param>
            /// <param name="dmName">雷电模拟器名称</param>
            /// <returns></returns>
            Task<int> ReConfNetWorkAsync(LDM? dM = null, int dmNo = -1, string dmName = "");

            /// <summary>
            /// 模拟输入文字
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="text">输入内容</param>
            /// <returns></returns>
            Task<int> InputTextAsync(int dmNo, string text);

            /// <summary>
            /// 点击屏幕
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="point">坐标位置</param>
            /// <param name="remain">点击毫秒数</param>
            /// <returns></returns>
            Task<int> TapAsync(int dmNo, Point? point, int remain = 200);


            /// <summary>
            /// 滑动模拟器
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="pointStart">开始位置</param>
            /// <param name="pointEnd">结束位置</param>
            /// <param name="speed">滑动速度</param>
            /// <returns></returns>
            Task<int> SwipeAsync(int dmNo, Point pointStart, Point pointEnd, int speed = 50);

            /// <summary>
            /// 启动APP
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="packagename">APP包名</param>
            /// <returns></returns>
            Task<int> StartAppAsync(int dmNo, string packagename);

            /// <summary>
            /// 杀死APP进程
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="packagename">APP包名</param>
            /// <param name="pid">APP进程</param>
            /// <returns></returns>
            Task<int> KillAppProcessAsync(int dmNo, string packagename, int pid = -1);

            /// <summary>
            /// 模拟按键
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="key">键值名称</param>
            /// <returns></returns>
            Task<int> KeyPressAsync(int dmNo, string key);




            Task<int> KeyEventAsync(int dmNo, int keycode);


            /// <summary>
            /// 断开模拟器网络
            /// </summary>
            /// <param name="dmNo"></param>
            /// <returns></returns>
            Task<int> DisconnectNetwork(int dmNo);
            /// <summary>
            /// 链接模拟器网络
            /// </summary>
            /// <param name="dmNo"></param>
            /// <returns></returns>
            Task<int> ConnectNetwork(int dmNo);

            /// <summary>
            /// 拉去或推送文件
            /// </summary>
            /// <param name="dmNo">模拟器编号</param>
            /// <param name="optype">操作类型，0 pull,1 push</param>
            /// <param name="sourcefile">源文件位置</param>
            /// <param name="targetfile">目标位置</param>
            /// <returns></returns>
            Task<int> FileOpAsync(int dmNo, int optype, string sourcefile, string targetfile);
            /// <summary>
            /// Dump UI XML
            /// </summary>
            /// <param name="dmNo"></param>
            /// <returns></returns>
            Task UIDumpAsync(int dmNo, string local);

            /// <summary>
            /// 重启
            /// </summary>
            /// <param name="dmNo"></param>
            void RebootAsync(int dmNo);

            /// <summary>
            /// 全局优化
            /// </summary>
            void GlobalAsync();

            void LaunchEmu(int dmNo);





        
    }
}
