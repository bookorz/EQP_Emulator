using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQP_Emulator
{
    public class ErrMsg
    {
        public string msg { get; set; } = "";
        public string desc { get; set; } = "";
        public int recover_method { get; set; } = 0;
        public Boolean isNeedMaintain { get; set; } = false;
    }

    public class ErrPosition
    {
        public string position { get; set; }
        public string desc { get; set; }
    }

    public class CmdDefine
    {
        //command type
        public string[] cmdType = new string[] { "MOV", "GET", "SET", "ACK" };
        //command list of each type
        string[] ackCmds = new string[] { "READY"};
        string[] movCmds = new string[] { "INIT","ORGSH","LOCK","UNLOCK","DOCK","UNDOCK","OPEN","CLOSE",
                                                 "WAFSH","GOTO","LOAD","UNLOAD","TRANS","CHANGE","ALIGN","HOME",
                                                 "HOLD","RESTR","ABORT","EMS","ADPLOCK","ADPUNLOCK", "TRANSREQ" };
        string[] getCmds = new string[] { "MAPDT", "ERROR", "CLAMP", "STATE", "MODE", "TRANSREQ", "SIGSTAT",
                                                 "EVENT", "CSTID", "SIZE" };
        string[] setCmds = new string[] { "ALIGN", "ERROR", "CLAMP", "MODE", "SIGOUT", "EVENT", "SIZE" };

        // parameter list 
        public static string[] devices = new string[] { "ALL", "P1", "P2", "P3", "P4", "ROB", "ROB1", "ROB2", "ALIGN", "ALIGN1", "ALIGN2" };
        public static string[] devices_single = new string[] { "P1", "P2", "P3", "P4", "ROB", "ROB1", "ROB2", "ALIGN", "ALIGN1", "ALIGN2" };
        public static string[] ports = new string[] { "P1", "P2", "P3", "P4" };
        public static string[] points = new string[] { "P1", "P2", "P3", "P4", "LLA", "LLB", "LLC", "LLD", "ALIGN", "ALIGN1", "ALIGN2" };
        public static string[] points_inside = new string[] { "ALIGN", "ALIGN1", "ALIGN2", "LLA", "LLA01", "LLB", "LLB01", "LLC", "LLC01", "LLD", "LLD01" };
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
            "ALIGN", "ALIGN1", "ALIGN2", 
            "LLA", "LLA01", "LLB", "LLB01", "LLC", "LLC01", "LLD", "LLD01" };
        //public static string[] p_position = points_slot.Concat(new string[] { "ARM1", "ARM2" }).ToArray();
        public static string[] p_size_pos = new string[] { "P1", "P2", "P3", "P4", "ALIGN", "ALIGN1", "ALIGN2", "ARM1", "ARM2",
                                                           "LLA", "LLA01", "LLB", "LLB01", "LLC", "LLC01", "LLD", "LLD01" };
        public static string[] arms = new string[] { "ARM1", "ARM2", "ARM3"};
        public static string[] arms_single = new string[] { "ARM1", "ARM2" };
        public static string[] angles = new string[] { "P1", "P2", "P3", "P4", "LLA", "LLB", "LLC", "LLD", "D******" };
        public static string[] aligners = new string[] { "ALIGN", "ALIGN1", "ALIGN2"};
        public static string[] devices_clamp = new string[] { "ALIGN", "ALIGN1", "ALIGN2", "ARM1", "ARM2" };
        public static string[] p_states = new string[] { "VER", "TRACK", "PRS1", "FFU1" };
        public static string[] p_events_set = new string[] { "ALL", "MAPDT", "TRANSREQ", "SYSTEM", "PORT", "PRS", "FFU" };
        public static string[] p_events_get = new string[] { "MAPDT", "TRANSREQ", "SYSTEM", "PORT", "PRS", "FFU" };
        public static string[] p_e84_device = new string[] { "ALL", "P1", "P2", "P3", "P4"};
        public static string[] p_e84_port = new string[] { "P1", "P2", "P3", "P4" };
        public static string[] p_e84_request = new string[] { "LOAD", "UNLOAD", "STOP"};

        //command list of  auto reply ack
        //public string[] autoAckCmd = new string[] { "INIT","ORGSH","LOCK","UNLOCK","DOCK","UNDOCK","OPEN","CLOSE",
        //                                            "WAFSH","MAPDT","GOTO","LOAD","UNLOAD","TRANS","CHANGE","ALIGN","HOME",
        //                                            "HOLD","RESTR","ABORT","EMS","ERROR","CLAMP","STATE","SIGSTAT",
        //                                            "CSTID","SIZE","ADPLOCK","ADPUNLOCK"};

        public Dictionary<string, string[]> cmdList = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams1 = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams2 = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams3 = new Dictionary<string, string[]>();
        public Dictionary<string, string[]> cmdParams4 = new Dictionary<string, string[]>();

        public DataTable dtErrMsg = new DataTable("Error Message");
        public DataTable dtErrPos = new DataTable("Error Position");

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
            cmdParams1.Add("GET:MODE", p_e84_port);//E84 port
            cmdParams1.Add("SET:MODE", p_e84_device);//E84 設備
            cmdParams2.Add("SET:MODE", new string[] { "AUTO", "MANUAL" });//E84 模式
            //TRANSREQ
            cmdParams1.Add("MOV:TRANSREQ", p_e84_port);//E84 Port
            cmdParams2.Add("MOV:TRANSREQ", p_e84_request);//E84 request type
            cmdParams1.Add("GET:TRANSREQ", p_e84_port);//E84 Port

            /****************** 燈號控制相關 **************************/
            cmdParams1.Add("SET:SIGOUT", new string[] { "STOWER","P1","P2","P3","P4" });//燈號設備
            cmdParams2.Add("SET:SIGOUT", new string[] { "RED","YELLOW","GREEN","BLUE", "BUZZER1", "BUZZER2","LOAD","UNLOAD","OP ACCESS" });//燈號指定
            cmdParams3.Add("SET:SIGOUT", new string[] { "ON", "OFF", "BLINK" });//閃燈類型

            /****************** 訊號狀態 **************************/
            cmdParams1.Add("GET:SIGSTAT", new string[] { "SYSTEM", "P1", "P2", "P3", "P4" });

            /****************** 事件 **************************/
            cmdParams1.Add("SET:EVENT", p_events_set);//事件類型
            cmdParams2.Add("SET:EVENT", new string[] { "ON", "OFF" });//事件開關
            cmdParams1.Add("GET:EVENT", p_events_get);//事件類型

            /***************** Wafer Size *******************/
            cmdParams1.Add("SET:SIZE", p_size_pos);//位置
            cmdParams2.Add("SET:SIZE", new string[] { "????", "NONE", "200", "300" });//wafer size
            cmdParams1.Add("GET:SIZE", p_size_pos);//位置

            /***************** FOUP Adapter *****************/
            cmdParams1.Add("MOV:ADPLOCK", ports);//位置
            cmdParams1.Add("MOV:ADPUNLOCK", ports);//位置

            //Data table init
            dtErrMsg.Columns.Add("msg", typeof(String));
            dtErrMsg.Columns.Add("desc", typeof(String));
            dtErrMsg.Columns.Add("recover", typeof(Int32));
            dtErrMsg.Columns.Add("maintain", typeof(Boolean));
            dtErrMsg.Rows.Add("NOTHING", "No error", 0, false);
            dtErrMsg.Rows.Add("VAC", "Insufficient vacuum source pressure", 2, false);
            dtErrMsg.Rows.Add("AIR", "Insufficient compressed air source pressure", 2, false);
            dtErrMsg.Rows.Add("STALL", "Motor stall", 2, true);
            dtErrMsg.Rows.Add("TIMEOUT", "Motor stall", 2, true);
            dtErrMsg.Rows.Add("LIMIT", "Action over the limit", 2, true);
            dtErrMsg.Rows.Add("SENSOR", "Sensor abnormal", 3, true);
            dtErrMsg.Rows.Add("POSITION", "Position error", 3, true);
            dtErrMsg.Rows.Add("EMS", "Emergency stop", 2, true);
            dtErrMsg.Rows.Add("COMM", "Communication error between controllers", 3, true);
            dtErrMsg.Rows.Add("COMM2", "Communication error between modules", 3, true);
            dtErrMsg.Rows.Add("VACON", "Vacuum on error", 2, false);
            dtErrMsg.Rows.Add("VACOF", "Vacuum off error", 2, true);
            dtErrMsg.Rows.Add("CLAMPON", "Clamp on error", 1, false);
            dtErrMsg.Rows.Add("CLAMPOF", "Clamp off error", 2, true);
            dtErrMsg.Rows.Add("PRTWAF", "Wafer protrusion", 2, false);
            dtErrMsg.Rows.Add("CRSWAF", "Cross wafer detection", 1, false);
            dtErrMsg.Rows.Add("THICKWAF", "Thickness abnormal wafer detection (thick)", 1, false);
            dtErrMsg.Rows.Add("THINWAF", "Thickness abnormal wafer detection (thin)", 1, false);
            dtErrMsg.Rows.Add("DBLWAF", "Dual wafer detection", 1, false);
            dtErrMsg.Rows.Add("BOWWAF", "Front bow wafer detection", 1, false);
            dtErrMsg.Rows.Add("COMMAND", "Command error", 2, false);
            dtErrMsg.Rows.Add("PODNG", "FOUP abnormal loading error", 2, false);
            dtErrMsg.Rows.Add("PODMISMATCH", "FOUP type mismatch", 2, false);
            dtErrMsg.Rows.Add("VAC_S", "Vacuum sensor abnormal", 3, true);
            dtErrMsg.Rows.Add("CLAMP_S", "Clamp sensor abnormal", 3, true);
            dtErrMsg.Rows.Add("SAFTY", "Hand and End-EF pinch sensor reaction", 2, false);
            dtErrMsg.Rows.Add("LOCKNG", "FOUP lock disabled", 2, true);
            dtErrMsg.Rows.Add("UNLOCKNG", "FOUP unlock disabled ", 2, true);
            dtErrMsg.Rows.Add("L_KEY_LK", "Latch key lock failure", 2, true);
            dtErrMsg.Rows.Add("L_KEY_UL", "Latch key unlock failure", 2, true);
            dtErrMsg.Rows.Add("MAP_S", "Mapping sensor abnormal", 2, true);
            dtErrMsg.Rows.Add("MAP_S1", "Mapping sensor ready disabled", 2, true);
            dtErrMsg.Rows.Add("MAP_S2", "Mapping sensor containing disabled", 2, true);
            dtErrMsg.Rows.Add("WAFLOST", "Wafer lost", 2, false);
            dtErrMsg.Rows.Add("ALIGNNG", "Alignment failure", 2, false);
            dtErrMsg.Rows.Add("DRIVER", "Driver abnormal", 3, true);
            dtErrMsg.Rows.Add("DRPOWERDOWN", "Drive power turned off", 3, false);
            dtErrMsg.Rows.Add("HARDWARE", "Hardware malfunction", 3, true);
            dtErrMsg.Rows.Add("INTERNAL", "Internal error", 3, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT1", "E84 TR_REQ timeout(TP1)", 1, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT2", "E84 BUSY ON timeout(TP2)", 1, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT3", "E84 carrier carrying-in timeout(TP3)", 1, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT4", "E84 carrier carrying-out timeout(TP3)", 1, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT5", "E84 BUSY OFF timeout(TP4)", 1, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT7", "E84 VALID OFF timeout (TP5)", 1, true);
            dtErrMsg.Rows.Add("E84_TIMEOUT8", "E84 CONTINUE timeout", 1, true);
            dtErrMsg.Rows.Add("E84_CS_VALID", "CS_0,CS_1,VALID error", 1, true);
            dtErrMsg.Rows.Add("READFAIL", "Reading error", 1, true);
            dtErrMsg.Rows.Add("UNDEFINITION", "Undefined error", 3, true);
            
            dtErrPos.Columns.Add("position", typeof(String));
            dtErrPos.Columns.Add("desc", typeof(String));
            dtErrPos.Rows.Add("ROBOT1", "Robot 1");
            dtErrPos.Rows.Add("ROBOT2", "Robot 2");
            dtErrPos.Rows.Add("ROBOT1_Z", "Robot 1 Z-axis");
            dtErrPos.Rows.Add("ROBOT2_Z", "Robot 2 Z-axis");
            dtErrPos.Rows.Add("ROBOT1_S", "Robot 1 θ-axis");
            dtErrPos.Rows.Add("ROBOT2_S", "Robot 2 θ-axis");
            dtErrPos.Rows.Add("ROBOT1_ARM1", "ROBOT 1 Upper ARM");
            dtErrPos.Rows.Add("ROBOT1_ARM2", "ROBOT 1 Lower ARM");
            dtErrPos.Rows.Add("ROBOT2_ARM1", "ROBOT 2 Upper ARM");
            dtErrPos.Rows.Add("ROBOT2_ARM2", "ROBOT 2 Lower ARM");
            dtErrPos.Rows.Add("P1", "Load port 1");
            dtErrPos.Rows.Add("P2", "Load port 2");
            dtErrPos.Rows.Add("P3", "Load port 3");
            dtErrPos.Rows.Add("P4", "Load port 4");
            dtErrPos.Rows.Add("P1_Y", "Load port 1 Y-axis");
            dtErrPos.Rows.Add("P2_Y", "Load port 2 Y-axis");
            dtErrPos.Rows.Add("P3_Y", "Load port 3 Y-axis");
            dtErrPos.Rows.Add("P4_Y", "Load port 4 Y-axis");
            dtErrPos.Rows.Add("P1_Z", "Load port 1 Z-axis");
            dtErrPos.Rows.Add("P2_Z", "Load port 2 Z-axis");
            dtErrPos.Rows.Add("P2_Z", "Load port 3 Z-axis");
            dtErrPos.Rows.Add("P2_Z", "Load port 4 Z-axis");
            dtErrPos.Rows.Add("P1_MAP", "Load port 1 Mapper");
            dtErrPos.Rows.Add("P2_MAP", "Load port 2 Mapper");
            dtErrPos.Rows.Add("P3_MAP", "Load port 3 Mapper");
            dtErrPos.Rows.Add("P4_MAP", "Load port 4 Mapper");
            dtErrPos.Rows.Add("P1_IOxx", "Load port 1 IO");
            dtErrPos.Rows.Add("P2_IOxx", "Load port 2 IO");
            dtErrPos.Rows.Add("P3_IOxx", "Load port 3 IO");
            dtErrPos.Rows.Add("P4_IOxx", "Load port 4 IO");
            dtErrPos.Rows.Add("ALIGN1", "Aligner 1");
            dtErrPos.Rows.Add("ALIGN2", "Aligner 2");
            dtErrPos.Rows.Add("ALIGN1_X", "Aligner 1 X-axis");
            dtErrPos.Rows.Add("ALIGN2_X", "Aligner 2 X-axis");
            dtErrPos.Rows.Add("ALIGN1_Y", "Aligner 1 Y-axis");
            dtErrPos.Rows.Add("ALIGN2_Y", "Aligner 2 Y-axis");
            dtErrPos.Rows.Add("ALIGN1_R", "Aligner 1 θ-axis");
            dtErrPos.Rows.Add("ALIGN2_R", "Aligner 2 θ-axis");
            dtErrPos.Rows.Add("DIO1", "DIO 1");
            dtErrPos.Rows.Add("DIO2", "DIO 2");
            dtErrPos.Rows.Add("OCR1", "OCR 1");
            dtErrPos.Rows.Add("OCR2", "OCR 2");
            dtErrPos.Rows.Add("UNDEFINITION", "Undefined error");
        }

        public CmdDefine()
        {
            initVar();
        }

        public string[] getErrMsgList()
        {
            string[] listAry;
            var query = (from t in dtErrMsg.AsEnumerable()
                         where 1 == 1
                         select t).ToList();
            listAry = new string[query.Count];
            if (query.Count > 0)
            {
                DataTable dtTemp = query.CopyToDataTable();
                DataView dvTemp = dtTemp.DefaultView;

                for (int i = 0; i < dvTemp.Table.Rows.Count; i++)
                {
                    listAry[i] = (string)dvTemp.Table.Rows[i]["msg"];
                }
            }
            return listAry;
        }

        public ErrMsg getErrMsgInfo(string msg)
        {
            ErrMsg result = new ErrMsg();
            var query = (from t in dtErrMsg.AsEnumerable()
                         where t.Field<string>("msg") == msg
                         select t).ToList();
            if (query.Count > 0)
            {
                DataTable dtTemp = query.CopyToDataTable();
                DataView dvTemp = dtTemp.DefaultView;

                for (int i = 0; i < dvTemp.Table.Rows.Count; i++)
                {
                    result.msg = (string)dvTemp.Table.Rows[i]["msg"];
                    result.desc = (string)dvTemp.Table.Rows[i]["desc"];
                    result.recover_method = (Int32)dvTemp.Table.Rows[i]["recover"];
                    result.isNeedMaintain = (Boolean)dvTemp.Table.Rows[i]["maintain"];
                }
            }
            return result;
        }

        public ErrPosition getErrPosInfo(string position)
        {
            ErrPosition result = new ErrPosition();
            var query = (from t in dtErrPos.AsEnumerable()
                         where t.Field<string>("position") == position
                         select t).ToList();
            if (query.Count > 0)
            {
                DataTable dtTemp = query.CopyToDataTable();
                DataView dvTemp = dtTemp.DefaultView;

                for (int i = 0; i < dvTemp.Table.Rows.Count; i++)
                {
                    result.position = (string)dvTemp.Table.Rows[i]["position"];
                    result.desc = (string)dvTemp.Table.Rows[i]["desc"];
                }
            }
            return result;
        }

    }
}
