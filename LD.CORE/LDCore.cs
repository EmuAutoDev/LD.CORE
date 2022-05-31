using LD.CORE.Model;
using LD.CORE.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LD.CORE.Utils.Win32;

namespace LD.CORE
{

        public delegate void ShowTipInfoEventHandler(int showType, int emuNo, string message);

        public delegate void VerSerDateTimeEventHandler(DateTime server);
        internal class LDCore : ILD
        {
            public string KEY_CODE = "";
            public int GroupCount = 0;
            CmdUtils cmdUtils;
          /// <summary>
          /// 模拟器重启秒数
          /// </summary>
            public static int RebootEmuDelay = 90000;
        /// <summary>
        
        /// UI Dump 缓存时间 
        /// </summary>
            public static int UIDumpDelay = 2000;
            public static int ListEmuDelay = 3000;

            /// <summary>
            /// 雷电模拟器安装位置
            /// </summary>
            public static string LDDir = "";

        

            public event ShowTipInfoEventHandler showTipInfoEvent;

            public event VerSerDateTimeEventHandler VerSerDateTimeEvent;

            protected void ShowInfo(int showType, int emuNo, string message)
            {
                showTipInfoEvent?.Invoke(showType, emuNo, message);
            }

            public void VerServerDate(DateTime date)
            {
                VerSerDateTimeEvent?.Invoke(date);
            }


            public LDCore(string lddir)
            {
                LDDir = lddir;
                cmdUtils = new CmdUtils();



            }
            public List<LDM> lDMs;


            public async Task<List<LDM>> GetLDListAsync()
            {
               
                CmdUtils.RunExe("ldconsole.exe", "list2", LDDir, out string output, out string error);
                await Task.Delay(ListEmuDelay);
                lDMs = new List<LDM>();
                var listDivices = output.Split('\n');
                foreach (var item in listDivices)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        var ldata = item.Split(',');
                        lDMs.Add(new LDM(ldata));
                    }

                }


                return lDMs;
            

          
            }



            public async Task<int> InputTextAsync(int dmNo, string text)
            {
            

                CmdUtils.ExecuteCommand("ldconsole.exe", $"--index  {dmNo} --key call.input --value {text}", LDDir, out string output, out string error);
              
              
                 return CODE_SUCCESS;
            }

            public async Task<int> KeyPressAsync(int dmNo, string key)
            {
          
                //dnconsole.exe action --name *** --key call.input --value ***            

                CmdUtils.ExecuteCommand("ldconsole.exe", $" --index {dmNo} --key call.keyboard --value {key}", LDDir, out string output, out string error);

            
                    return CODE_SUCCESS;
             
                
            }

       

        public async Task<int> KillAppProcessAsync(int dmNo, string packagename, int pid = -1)
            {
               




                CmdUtils.ExecuteCommand("ldconsole.exe", $" killapp --index {dmNo} --packagename {packagename}", LDDir, out string output, out string error);

             
                    return CODE_SUCCESS;
               
            }

            public async Task<int> ReConfNetWorkAsync(LDM? dM = null, int dmNo = -1, string dmName = "")
            {

                await DisconnectNetwork(dmNo);
                await ConnectNetwork(dmNo);
                return CODE_SUCCESS;
           
            }

            public async Task<int> StartAppAsync(int dmNo, string packagename)
            {
             
                //dnconsole.exe action --name *** --key call.input --value ***

                CmdUtils.ExecuteCommand("ldconsole.exe", $" runapp --index {dmNo} --packagename {packagename}", LDDir, out string output, out string error);

                return CODE_SUCCESS;
            }

            public async Task<int> TapAsync(int dmNo, Point? point, int remain = CODE_SUCCESS)
            {
                if (point == null)
                    return 100;
          
                //dnconsole.exe action --name *** --key call.input --value ***          
                CmdUtils.ExecuteCommand("ld.exe", $" -s {dmNo} input tap {point?.X} {point?.Y}", LDDir, out string output, out string error);
                return CODE_SUCCESS;

            }

            public async Task<int> SwipeAsync(int dmNo, Point pointStart, Point pointEnd, int speed = 50)
            {
              
                //dnconsole.exe action --name *** --key call.input --value ***

                CmdUtils.ExecuteCommand("ld.exe", $" -s {dmNo} input swipe {pointStart.X} {pointStart.Y} {pointEnd.X} {pointEnd.Y}", LDDir, out string output, out string error);
                return CODE_SUCCESS;

            }
            public async Task<int> ScollAsync(int dmNo, int dx, int dy, int speed = 50)
            {
              
                //dnconsole.exe action --name *** --key call.input --value ***
                //
                CmdUtils.ExecuteCommand("ld.exe", $" -s {dmNo} input swipe {dx} {dy}", LDDir, out string output, out string error);
                return CODE_SUCCESS;

            }



            public async Task<int> KeyEventAsync(int dmNo, int keycode)
            {
                CmdUtils.ExecuteCommand("ld.exe", $" -s {dmNo} input keyevent {keycode}", LDDir, out string output, out string error);
                return CODE_SUCCESS;
            }

            public const int CODE_SUCCESS = 200;
            public const int CODE_FAIL = 0;

            /// <summary>
            /// 发送消息方式滚动模拟器屏幕
            /// </summary>
            /// <param name="dM"></param>
            /// <param name="scollrange"></param>
            /// <param name="upDown"></param>
            /// <param name="xPos"></param>
            /// <param name="yPos"></param>
            /// <returns></returns>
            public async Task<int> ForceMsgScollAsync(LDM dM, int scolltimes = 3, float scollrange = 3, int upDown = -1, int xPos = 238, int yPos = 391)
            {

                if (dM.Pid != 0 && dM.TopWinHwnd != 0)
                {

                    if (FindWindow((IntPtr)(dM.TopWinHwnd), "RenderWindow"))
                    {
                        IntPtr wParam = MAKEWPARAM(upDown, 1f, WinMsgMouseKey.MK_LBUTTON);
                        // Scrolls [Handle] down by 1/2 wheel rotation with Left Button pressed
                        IntPtr lParam = MAKELPARAM(xPos, yPos);
                        for (int i = 0; i < scollrange; i++)
                        {
                            for (int j = 0; j < scollrange; j++)
                            {
                                PostMessage((IntPtr)TargetHwnd, WM_MOUSEWHEEL, wParam, lParam);
                            }

                            await Task.Delay(1500);
                        }

                        return CODE_SUCCESS;
                    }
                }
                return CODE_FAIL;



            }



            // ctor does the work--just instantiate and go
            public bool FindWindow(IntPtr hwndParent, string classname)
            {
                TargetHwnd = IntPtr.Zero;
                m_classname = classname;

                // IntPtr childHwnd = FindWindowEx(hwndParent, IntPtr.Zero, m_classname, null);   //获得按钮的句柄
                if (!FindChildClassHwnd(hwndParent, IntPtr.Zero))
                {
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Find the child window, if found m_classname will be assigned 
            /// </summary>
            /// <param name="hwndParent">parent's handle</param>
            /// <param name="lParam">the application value, nonuse</param>
            /// <returns>found or not found</returns>
            //The C++ code is that  lParam is the instance of FindWindow class , if found assign the instance's m_hWnd
            private bool FindChildClassHwnd(IntPtr hwndParent, IntPtr lParam)
            {
                EnumWindowProc childProc = new EnumWindowProc(FindChildClassHwnd);
                IntPtr hwnd = FindWindowEx(hwndParent, IntPtr.Zero, m_classname, null);
                if (hwnd != IntPtr.Zero)
                {
                    this.TargetHwnd = hwnd; // found: save it
                    return false; // stop enumerating
                }
                EnumChildWindows(hwndParent, childProc, IntPtr.Zero); // recurse  redo FindChildClassHwnd
                return true;// keep looking
            }


            private IntPtr TargetHwnd = IntPtr.Zero;
            public string m_classname = "RenderWindow"; // class name to look for


            public async Task<int> ConnectNetwork(int dmNo)
            {
              
                //dnconsole.exe action --name *** --key call.input --value ***          
                CmdUtils.ExecuteCommand("ldconsole.exe", $" action --index  {dmNo} --key call.network --value connect", LDDir, out string output, out string error);

                await Task.Delay(1000);
                return CODE_SUCCESS;

            }
            public async Task<int> DisconnectNetwork(int dmNo)
            {
              
                //dnconsole.exe action --name *** --key call.input --value ***

                CmdUtils.ExecuteCommand("ldconsole.exe", $" action --index  {dmNo} --key call.network --value offline", LDDir, out string output, out string error);
                return CODE_SUCCESS;

            }

            public async Task<int> FileOpAsync(int dmNo, int optype, string remote, string local)
            {
                if (optype == 0)
                    CmdUtils.ExecuteCommand("ldconsole.exe", $"pull --index {dmNo} --remote {remote} --local {local}", LDDir, out string output, out string error);
                else
                    CmdUtils.ExecuteCommand("ldconsole.exe", $"pull --index {dmNo} --remote {remote} --local {local}", LDDir, out string output, out string error);
                return CODE_SUCCESS;
            }

            public async Task UIDumpAsync(int dmNo, string local)
            {
                CmdUtils.ExecuteCommand("ld.exe", $"-s {dmNo} uiautomator dump", LDDir, out string output, out string error);
                await Task.Delay(UIDumpDelay);
                await FileOpAsync(dmNo, 0, "/sdcard/window_dump.xml", local);
                await Task.Delay(UIDumpDelay);
            }


            public async void RebootAsync(int dmNo)
            {
                CmdUtils.ExecuteCommand("ldconsole.exe", $"reboot --index {dmNo}  ", LDDir, out string output, out string error);
                await Task.Delay(RebootEmuDelay);

            }

            public void SortWnd()
            {
                CmdUtils.ExecuteCommand("ldconsole.exe", $"sortWnd", LDDir, out string output, out string error);
            }

            public void LaunchEmu(int dmNo)
            {
                CmdUtils.ExecuteCommand("ldconsole.exe", $"launch --index {dmNo}  ", LDDir, out string output, out string error);

            }

            public void GlobalAsync()
            {
                //dnconsole.exe globalsetting --fps 10 --audio 0 --fastplay 1 --cleanmode 1
                CmdUtils.ExecuteCommand("dnconsole.exe", "globalsetting --fps 10 --audio 0 --fastplay 1 --cleanmode 1", LDDir, out string output, out string error);
            }

            public async Task<int> ModifyLocation(int dmNo, string address)
            {

                var local = await LRDataHelpers.GeocLocalInfoAsync(address);



                if (local != null)
                {
                    var x = local.detail.pointx;
                    var y = local.detail.pointy;
                    if (LocalRandom)
                    {
                        x = (double.Parse(x) + (GetRandomRange(LocalRandomRangeX)) * 0.001).ToString();
                        y = (double.Parse(y) + (GetRandomRange(LocalRandomRangeY)) * 0.001).ToString();
                    }
                    VerServerDate(LRDataHelpers.serVerTime);
                    CmdUtils.ExecuteCommand("ldconsole.exe", $"action --index {dmNo} --key call.locate --value {x},{y} ", LDDir, out string output, out string error);
                    return CODE_SUCCESS;
                }
                else
                {
                    showTipInfoEvent?.Invoke(0, dmNo, "地址转化失败");
                    return CODE_FAIL;
                }
            }

            public static bool LocalRandom = false;
            public static string LocalRandomRangeY = "-1000,1000";
            public static string LocalRandomRangeX = "-3000,3000";
            public int GetRandomRange(string range)
            {
                var dc = range.Split(',');
                var r = new Random();
                return r.Next(int.Parse(dc[0]), int.Parse(dc[1]));
            }


            #region WINAPI


            internal const uint WM_MOUSEWHEEL = 0x020A;

            [Flags]
            public enum WinMsgMouseKey : int
            {
                MK_NONE = 0x0000,
                MK_LBUTTON = 0x0001,
                MK_RBUTTON = 0x0002,
                MK_SHIFT = 0x0004,
                MK_CONTROL = 0x0008,
                MK_MBUTTON = 0x0010,
                MK_XBUTTON1 = 0x0020,
                MK_XBUTTON2 = 0x0040
            }




            [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

            internal static IntPtr MAKEWPARAM(int direction, float multiplier, WinMsgMouseKey button)
            {
                int delta = (int)(SystemInformation.MouseWheelScrollDelta * multiplier);
                return (IntPtr)(((delta << 16) * Math.Sign(direction) | (ushort)button));
            }

            internal static IntPtr MAKELPARAM(int low, int high)
            {
                return (IntPtr)((high << 16) | (low & 0xFFFF));
            }
            #endregion
        }
    
}
