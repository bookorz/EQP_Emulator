using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQP_Emulator
{

    static class Command
    {
        public static BindingList<CmdScript> oCmdScript = new BindingList<CmdScript>();
        public static IEnumerable<CmdScript> getCmdList()
        {
            return (IEnumerable<CmdScript>) Command.oCmdScript.ToList();
        }
        /// <summary>
        ///     Add put wafer command to the script。
        /// </summary>
        /// <param name="point">放片目的地點位。</param>
        /// <param name="arm">放片使用手臂。ARM1~ARM3</param>
        /// <returns>指令字串</returns>
        public static void Unload(string point, string arm)
        {
            //"MOV:UNLOAD/ALIGN1/ARM1;"
            StringBuilder cmd = new StringBuilder();
            cmd.Append("MOV:UNLOAD/");
            cmd.Append(point);
            cmd.Append("/");
            cmd.Append(arm);
            cmd.Append(";");
            addScriptCmd(cmd.ToString());
        }
        /// <summary>
        ///     Add get wafer command to the script。
        /// </summary>
        /// <param name="point">取片來源點位。</param>
        /// <param name="arm">>取片使用手臂。ARM1~ARM3 </param>
        /// <returns></returns>
        public static void Load(string point, string arm)
        {
            //"MOV:UNLOAD/ALIGN1/ARM1;"
            StringBuilder cmd = new StringBuilder();
            cmd.Append("MOV:LOAD/");
            cmd.Append(point);
            cmd.Append("/");
            cmd.Append(arm);
            cmd.Append(";");
            addScriptCmd(cmd.ToString());
        }

        public static void addScriptCmd(string cmd)
        {
            int seq = Command.oCmdScript.Count + 1;
            Command.oCmdScript.Add(new CmdScript { Seq = seq, Command = cmd });
        }
    }
}
