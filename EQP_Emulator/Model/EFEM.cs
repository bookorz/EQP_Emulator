using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQP_Emulator
{
    class EFEM
    {
        public static string VER { get; set; } = "";
        public static string[] TRACK { get; set; } = new string[] { "????", "????", "????" };
        public static string[] PRSn { get; set; } = new string[8];
        public static string[] FFUn { get; set; } = new string[20];

        // 1:Error Message 2:Errors occurred position
        public static string[] Error { get; set; } = new string[2];

        //Robot enable setting
        public static Boolean isROB1Enable { get; set; } = false;
        //public static Boolean isROB2Enable { get; set; } = false;
        //R1 Arm enable setting
        public static Boolean isR1Arm1Enable { get; set; } = false;
        public static Boolean isR1Arm2Enable { get; set; } = false;
        public static Boolean isR1Arm3Enable { get; set; } = false;
        //R2 Arm enable setting
        //public static Boolean isR2Arm1Enable { get; set; } = false;
        //public static Boolean isR2Arm2Enable { get; set; } = false;
        //public static Boolean isR2Arm3Enable { get; set; } = false;
        //Load lock enable setting
        public static Boolean isLLAEnable { get; set; } = false;
        public static Boolean isLLBEnable { get; set; } = false;
        public static Boolean isLLCEnable { get; set; } = false;
        public static Boolean isLLDEnable { get; set; } = false;
        //Load pord enable setting
        public static Boolean isP1Enable { get; set; } = false;
        public static Boolean isP2Enable { get; set; } = false;
        public static Boolean isP3Enable { get; set; } = false;
        public static Boolean isP4Enable { get; set; } = false;
        //Aligner enable setting
        public static Boolean isAlign1Enable { get; set; } = false;
        public static Boolean isAlign2Enable { get; set; } = false;

        //EFEM Unit avable status
        //Robot enable setting
        public static Boolean isR1Arm1Empty { get; set; } = true;
        public static Boolean isR1Arm2Empty { get; set; } = true;
        public static Boolean isLLAEmpty { get; set; } = true;
        public static Boolean isLLBEmpty { get; set; } = true;
        public static Boolean isLLCEmpty { get; set; } = true;
        public static Boolean isLLDEmpty { get; set; } = true;
        public static Boolean isAlign1Empty { get; set; } = true;
        public static Boolean isAlign2Empty { get; set; } = true;

        //signal
        public static string sysData1 { get; set; } = "00000000000000000000000000000000";
        public static string sysData2 { get; set; } = "00000000000000000000000000000000";
        public static string port1Data1 { get; set; } = "00000000000000000000000000000000";
        public static string port2Data1 { get; set; } = "00000000000000000000000000000000";
        public static string port3Data1 { get; set; } = "00000000000000000000000000000000";
        public static string port4Data1 { get; set; } = "00000000000000000000000000000000";
        public static string port1Data2 { get; set; } = "00000000000000000000000000000000";
        public static string port2Data2 { get; set; } = "00000000000000000000000000000000";
        public static string port3Data2 { get; set; } = "00000000000000000000000000000000";
        public static string port4Data2 { get; set; } = "00000000000000000000000000000000";

        //Record the wafer information at the point
        public static Dictionary<string, string> points = new Dictionary<string, string>()
        {
         {"R1A1", ""},{"R1A2", ""},
         {"R2A1", ""},{"R2A2", ""},
         {"LLA01", ""},{"LLB01", ""},{"LLC01", ""},{"LLD01", ""},
         {"ALIGN1", ""},{"ALIGN2", ""},
         {"P101", ""},{"P102", ""},{"P103", ""},{"P104", ""},{"P105", ""},
         {"P106", ""},{"P107", ""},{"P108", ""},{"P109", ""},{"P110", ""},
         {"P111", ""},{"P112", ""},{"P113", ""},{"P114", ""},{"P115", ""},
         {"P116", ""},{"P117", ""},{"P118", ""},{"P119", ""},{"P120", ""},
         {"P121", ""},{"P122", ""},{"P123", ""},{"P124", ""},{"P125", ""},
         {"P201", ""},{"P202", ""},{"P203", ""},{"P204", ""},{"P205", ""},
         {"P206", ""},{"P207", ""},{"P208", ""},{"P209", ""},{"P210", ""},
         {"P211", ""},{"P212", ""},{"P213", ""},{"P214", ""},{"P215", ""},
         {"P216", ""},{"P217", ""},{"P218", ""},{"P219", ""},{"P220", ""},
         {"P221", ""},{"P222", ""},{"P223", ""},{"P224", ""},{"P225", ""},
         {"P301", ""},{"P302", ""},{"P303", ""},{"P304", ""},{"P305", ""},
         {"P306", ""},{"P307", ""},{"P308", ""},{"P309", ""},{"P310", ""},
         {"P311", ""},{"P312", ""},{"P313", ""},{"P314", ""},{"P315", ""},
         {"P316", ""},{"P317", ""},{"P318", ""},{"P319", ""},{"P320", ""},
         {"P321", ""},{"P322", ""},{"P323", ""},{"P324", ""},{"P325", ""},
         {"P401", ""},{"P402", ""},{"P403", ""},{"P404", ""},{"P405", ""},
         {"P406", ""},{"P407", ""},{"P408", ""},{"P409", ""},{"P410", ""},
         {"P411", ""},{"P412", ""},{"P413", ""},{"P414", ""},{"P415", ""},
         {"P416", ""},{"P417", ""},{"P418", ""},{"P419", ""},{"P420", ""},
         {"P421", ""},{"P422", ""},{"P423", ""},{"P424", ""},{"P425", ""}
        };
        //Record the point information of the wafer
        public static Dictionary<string, string> wafers = new Dictionary<string, string>()
        {
         {"P1_W01", ""},{"P1_W02", ""},{"P1_W03", ""},{"P1_W04", ""},{"P1_W05", ""},
         {"P1_W06", ""},{"P1_W07", ""},{"P1_W08", ""},{"P1_W09", ""},{"P1_W10", ""},
         {"P1_W11", ""},{"P1_W12", ""},{"P1_W13", ""},{"P1_W14", ""},{"P1_W15", ""},
         {"P1_W16", ""},{"P1_W17", ""},{"P1_W18", ""},{"P1_W19", ""},{"P1_W20", ""},
         {"P1_W21", ""},{"P1_W22", ""},{"P1_W23", ""},{"P1_W24", ""},{"P1_W25", ""},
         {"P2_W01", ""},{"P2_W02", ""},{"P2_W03", ""},{"P2_W04", ""},{"P2_W05", ""},
         {"P2_W06", ""},{"P2_W07", ""},{"P2_W08", ""},{"P2_W09", ""},{"P2_W10", ""},
         {"P2_W11", ""},{"P2_W12", ""},{"P2_W13", ""},{"P2_W14", ""},{"P2_W15", ""},
         {"P2_W16", ""},{"P2_W17", ""},{"P2_W18", ""},{"P2_W19", ""},{"P2_W20", ""},
         {"P2_W21", ""},{"P2_W22", ""},{"P2_W23", ""},{"P2_W24", ""},{"P2_W25", ""},
         {"P3_W01", ""},{"P3_W02", ""},{"P3_W03", ""},{"P3_W04", ""},{"P3_W05", ""},
         {"P3_W06", ""},{"P3_W07", ""},{"P3_W08", ""},{"P3_W09", ""},{"P3_W10", ""},
         {"P3_W11", ""},{"P3_W12", ""},{"P3_W13", ""},{"P3_W14", ""},{"P3_W15", ""},
         {"P3_W16", ""},{"P3_W17", ""},{"P3_W18", ""},{"P3_W19", ""},{"P3_W20", ""},
         {"P3_W21", ""},{"P3_W22", ""},{"P3_W23", ""},{"P3_W24", ""},{"P3_W25", ""},
         {"P4_W01", ""},{"P4_W02", ""},{"P4_W03", ""},{"P4_W04", ""},{"P4_W05", ""},
         {"P4_W06", ""},{"P4_W07", ""},{"P4_W08", ""},{"P4_W09", ""},{"P4_W10", ""},
         {"P4_W11", ""},{"P4_W12", ""},{"P4_W13", ""},{"P4_W14", ""},{"P4_W15", ""},
         {"P4_W16", ""},{"P4_W17", ""},{"P4_W18", ""},{"P4_W19", ""},{"P4_W20", ""},
         {"P4_W21", ""},{"P4_W22", ""},{"P4_W23", ""},{"P4_W24", ""},{"P4_W25", ""}
        };

        public static void UpdateWaferInfo(string move_to, string wafer_id)
        {
            // Remove wafer information from source location
            if (!wafers[wafer_id].Equals(""))
            {
                EFEM.setPointEmpty(wafers[wafer_id], true);
                points[wafers[wafer_id]] = "";
            }


            points[move_to] = wafer_id;// Record the wafer information at the point
            wafers[wafer_id] = move_to;// Record the point information of the wafer
            EFEM.setPointEmpty(move_to, false);
        }

        public static void setPointEmpty(string point, Boolean isEmpty)
        {
            switch (point)
            {
                case "ALIGN1":
                    isAlign1Empty = isEmpty;
                    break;
                case "ALIGN2":
                    isAlign2Empty = isEmpty;
                    break;
                case "LLA01":
                    isLLAEmpty = isEmpty;
                    break;
                case "LLB01":
                    isLLBEmpty = isEmpty;
                    break;
                case "LLC01":
                    isLLCEmpty = isEmpty;
                    break;
                case "LLD01":
                    isLLDEmpty = isEmpty;
                    break;
                case "R1A1":
                    isR1Arm1Empty = isEmpty;
                    break;
                case "R1A2":
                    isR1Arm2Empty = isEmpty;
                    break;
            }
        }
        public static void parsePRSn(string data)
        {
            string[] temp = new string[8];
            foreach ( string item in data.Split(','))
            {
                if (!item.Contains("|"))
                    continue;
                try
                {
                    string key = item.Substring(0, item.IndexOf("|"));
                    int idx = Int32.Parse(key.Replace("SNO", "")) - 1;
                    string value = item.Substring(item.IndexOf("|") + 1);
                    temp[idx] = value;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            PRSn = temp;
        }
        public static void parseFFUn(string data)
        {
            string[] temp = new string[20];
            foreach (string item in data.Split(','))
            {
                if (!item.Contains("|"))
                    continue;
                try
                {
                    string key = item.Substring(0, item.IndexOf("|"));
                    int idx = Int32.Parse(key.Replace("FNO", "")) - 1;
                    string value = item.Substring(item.IndexOf("|") + 1);
                    temp[idx] = value;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            FFUn = temp;
        }
    }
}
