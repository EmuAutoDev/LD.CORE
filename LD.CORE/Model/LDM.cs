using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD.CORE.Model
{
    public class LDM
    {
        public LDM(string[] ldata)
        {
            index = int.Parse(ldata[0]);
            Title = ldata[1];
            TopWinHwnd = int.Parse(ldata[2]);
            EnterAndroid = int.Parse(ldata[3]);
            Pid = int.Parse(ldata[4]);
            VmBoxId = int.Parse(ldata[5]);
        }

        public int index { set; get; }
        public string? Title { set; get; }
        public int TopWinHwnd { set; get; }
        public int EnterAndroid { set; get; }
        public int Pid { set; get; }
        public int VmBoxId { set; get; }

        public bool Nomal { set; get; } = true;

    }
}
