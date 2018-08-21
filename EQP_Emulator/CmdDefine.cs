using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQP_Emulator
{
    public class CmdDefine
    {
        //command type
        public string[] cmdType = new string[] { "MOV", "GET", "SET", "ACK" };
        //command list of each type
        string[] ackCmds = new string[] { "READY"};
        string[] movCmds = new string[] { "INIT","ORGSH","LOCK","UNLOCK","DOCK","UNDOCK","OPEN","CLOSE",
                                                 "WAFSH","GOTO","LOAD","UNLOAD","TRANS","CHANGE","ALIGN","HOME",
                                                 "HOLD","RESTR","ABORT","EMS","ADPLOCK","ADPUNLOCK" };
        string[] getCmds = new string[] { "MAPDT", "ERROR", "CLAMP", "STATE", "MODE", "TRANSREQ", "SIGSTAT",
                                                 "EVENT", "CSTID", "SIZE" };
        string[] setCmds = new string[] { "ALIGN", "ERROR", "CLAMP", "SIGOUT", "EVENT", "SIZE" };

        // parameter list 
        public static string[] devices = new string[] { "ALL", "P1", "P2", "P3", "P4", "ROB", "ALIGN1", "ALIGN2" };
        public static string[] devices_single = new string[] { "P1", "P2", "P3", "P4", "ROB", "ALIGN1", "ALIGN2" };
        public static string[] ports = new string[] { "P1", "P2", "P3", "P4" };
        public static string[] points = new string[] { "P1", "P2", "P3", "P4", "LL1", "LL2", "ALIGN1", "ALIGN2" };
        public static string[] points_inside = new string[] { "ALIGN1", "ALIGN2", "LL1", "LL101", "LL2", "LL201" };
        public static string[] points_slot = new string[] {
            "P101","P102","P103","P104","P105","P106","P107","P108","P109","P110",
            "P111","P112","P113","P114","P115","P116","P117","P118","P119","P120",
            "P121","P122","P123","P124","P125",
            "P201","P202","P203","P204","P205","P206","P207","P208","P209","P210",
            "P211","P212","P213","P214","P215","P216","P217","P218","P219","P220",
            "P221","P222","P223","P224","P225",
            "P301","P302","P303","P304","P305","P306","P307","P308","P309","P310",
            "P311","P312","P313","P314","P315","P316","P317","P318","P319","P320",
            "P321","P322","P323","P324","P325",
            "P401","P402","P403","P404","P405","P406","P407","P408","P409","P410",
            "P411","P412","P413","P414","P415","P416","P417","P418","P419","P420",
            "P421","P422","P423","P424","P425",
            "ALIGN1", "ALIGN2", 
            "LL1", "LL101", "LL2", "LL201" };
        public static string[] p_position = points_slot.Concat(new string[] { "ARM1", "ARM2" }).ToArray();
        public static string[] arms = new string[] { "ARM1", "ARM2", "ARM3"};
        public static string[] arms_single = new string[] { "ARM1", "ARM2" };
        public static string[] angles = new string[] { "P1", "P2", "P3", "P4", "LL1", "LL2", "Dxxxx" };
        public static string[] aligners = new string[] { "ALIGN1", "ALIGN2"};
        public static string[] devices_clamp = new string[] { "ALIGN1", "ALIGN2", "ARM1", "ARM2" };
        public static string[] p_states = new string[] { "VER", "TRACK", "PRS1", "FFU1" };
        public static string[] p_events = new string[] { "ALL", "MAPDT", "TRANSREQ", "SYSTEM", "PORT", "PRS", "FFU" };

        //command list of  auto reply ack
        public string[] autoAckCmd = new string[] { "INIT","ORGSH","LOCK","UNLOCK","DOCK","UNDOCK","OPEN","CLOSE",
                                                    "WAFSH","MAPDT","GOTO","LOAD","UNLOAD","TRANS","CHANGE","ALIGN","HOME",
                                                    "HOLD","RESTR","ABORT","EMS","ERROR","CLAMP","STATE","SIGSTAT",
                                                    "CSTID","SIZE","ADPLOCK","ADPUNLOCK"};

        public Dictionary<string, string[]> cmdList = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams1 = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams2 = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams3 = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams4 = new Dictionary<string, string[]>();

        public void initVar()
        {
            //Command List
            cmdList.Add("MOV", movCmds);
            cmdList.Add("GET", getCmds);
            cmdList.Add("SET", setCmds);
            cmdList.Add("ACK", ackCmds);

            //ACK
            cmdParams1.Add("ACK:READY", new string[] { "COMM" });
           
            /****************** Port 相關 **************************/
            cmdParams1.Add("MOV:INIT", devices);
            cmdParams1.Add("MOV:ORGSH", devices);
            cmdParams1.Add("MOV:LOCK", ports);//LOCK PORT
            cmdParams1.Add("MOV:UNLOCK", ports);//UNLOCK:
            cmdParams1.Add("MOV:DOCK", ports);//DOCK: 
            cmdParams1.Add("MOV:OPEN", ports);//OPEN: 開 PORT
            cmdParams1.Add("MOV:CLOSE", ports);//CLOSE: 關PORT
            cmdParams1.Add("MOV:WAFSH", ports);//WAFSH: mapping
            cmdParams1.Add("MOV:UNDOCK", ports);//UNDOCK
            cmdParams1.Add("MOV:HOME", devices_single);//HOME

            /****************** 傳送相關 **************************/
            //GOTO: 到目的地前方
            cmdParams1.Add("MOV:GOTO", points_slot);//Slot 位置
            cmdParams2.Add("MOV:GOTO", arms);//取片手臂: 上, 下, 雙臂
            cmdParams3.Add("MOV:GOTO", new string[] { "UP", "DOWN" });// 相對位置
            //LOAD: 取片
            cmdParams1.Add("MOV:LOAD", points_slot); //來源 Slot 位置
            cmdParams2.Add("MOV:LOAD", arms); //取片手臂: 上, 下, 雙臂
            //UNLOAD: 放片
            cmdParams1.Add("MOV:UNLOAD", points_slot); //目標 Slot 位置
            cmdParams2.Add("MOV:UNLOAD", arms); //放片手臂: 上, 下, 雙臂
            //TRANS:  取片 + 放片
            cmdParams1.Add("MOV:TRANS", points_slot); //來源 Slot 位置
            cmdParams2.Add("MOV:TRANS", arms); //取片手臂: 上, 下, 雙臂
            cmdParams3.Add("MOV:TRANS", arms); //放片手臂: 上, 下, 雙臂
            cmdParams4.Add("MOV:TRANS", points_slot); //目標 Slot 位置 
            //CHANGE: 交換片
            cmdParams1.Add("MOV:CHANGE", points_inside); //CHANGE: 交換位置
            cmdParams2.Add("MOV:CHANGE", arms_single); //取片手臂
            cmdParams3.Add("MOV:CHANGE", arms_single); //放片手臂

            /****************** Align 相關 **************************/
            //SET:ALIGN 設定 Align 角度
            cmdParams1.Add("SET:ALIGN", aligners);//指定 Aligner
            cmdParams2.Add("SET:ALIGN", angles);//ALIGN: 設定 Align 角度
            //MOV:ALIGN 執行 Align
            cmdParams1.Add("MOV:ALIGN", aligners);//指定 Aligner

            /****************** 整機動作 **************************/
            // 不須參數

            /****************** 抓取動作 **************************/
            //GET
            cmdParams1.Add("GET:CLAMP", devices_clamp);//查詢抓取狀態的設備
            //SET
            cmdParams1.Add("SET:CLAMP", devices_clamp);//指定抓取動作的設備
            cmdParams2.Add("SET:CLAMP", new string[] { "ON", "OFF" });//指定抓取動作

            /****************** 資訊相關 **************************/
            //GET
            cmdParams1.Add("GET:MAPDT", ports);//MAPDT: 取得 MAPING 資料
            cmdParams1.Add("GET:STATE", p_states);//MAPDT: 取得 MAPING 資料
            cmdParams1.Add("GET:CSTID", ports);//GET:CSTID 取得 Port 上的 Carrier ID 資料

            //SET
            cmdParams1.Add("SET:ERROR", new string[] { "CLEAR" });//RESET ERROR

            /****************** E84相關 **************************/
            //MODE
            //TRANSREQ

            /****************** 燈號控制相關 **************************/
            cmdParams1.Add("SET:SIGOUT", new string[] { "STOWER","P1","P2","P3","P4" });//燈號設備
            cmdParams2.Add("SET:SIGOUT", new string[] { "RED","YELLOW","GREEN","BLUE", "BUZZER1", "BUZZER2","LOAD","UNLOAD","OP ACCESS" });//燈號指定
            cmdParams3.Add("SET:SIGOUT", new string[] { "ON", "OFF", "BLINK" });//閃燈類型

            /****************** 訊號狀態 **************************/
            cmdParams1.Add("GET:SIGSTAT", new string[] { "SYSTEM", "P1", "P2", "P3", "P4" });

            /****************** 事件 **************************/
            cmdParams1.Add("SET:EVENT", p_events);//事件類型
            cmdParams2.Add("SET:EVENT", new string[] { "ON", "OFF" });//事件開關

            /***************** Wafer Size *******************/
            cmdParams1.Add("SET:SIZE", p_position);//位置
            cmdParams2.Add("SET:SIZE", new string[] { "????", "NONE", "200", "300" });//wafer size
            cmdParams1.Add("GET:SIZE", p_position);//位置

            /***************** FOUP Adapter *****************/
            cmdParams1.Add("MOVE:ADPLOCK", ports);//位置
            cmdParams1.Add("MOVE:ADPUNLOCK", ports);//位置
        }

        public CmdDefine()
        {
            initVar();
        }
    }
}
