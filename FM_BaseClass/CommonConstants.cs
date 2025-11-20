using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ClassList
{
    public class CommonConstants
    {
        public const int AccesslevelMaxUser = 255;

        public const int LOGPIXELSY = 90;

        public const int SUCCESS = 0;

        public const int FAILURE = -1;

        public const int ONLYBOOTBLOCK = 2;

        public const byte FourGrayColorIndex_White = 3;

        public const byte FourGrayColorIndex_Black = 0;

        public const string Ladder = "Ladder";

        public const int START_LADDER_SCREEN = 50000;

        public const int END_LADDER_SCREENS = 60000;

        public const ushort MAX_LADDER_SCREENS = 10000;

        public const int START_WEB_SCREEN = 64000;

        public const int END_WEB_SCREENS = 64900;

        public const string LoggedDatapath = "";

        public const int PRIZM_MIN_BYTES_EV3 = 80;

        public const int PRIZM_HW_BYTES = 64;

        public const int VERSION_BYTE = 49;

        public const int PRIZM3_MIN_BYTES = 50;

        public const int PRIZM_EV3_READ_BYTES = 30;

        public const int PRODUCT_PRIZM10 = 501;

        public const int PRODUCT_PRIZM12 = 502;

        public const int PRODUCT_PRIZM15 = 15;

        public const int PRODUCT_PRIZM18 = 503;

        public const int PRODUCT_PRIZM22 = 504;

        public const int PRODUCT_PRIZM50 = 505;

        public const int PRODUCT_PRIZM120 = 507;

        public const int PRODUCT_PRIZM20 = 20;

        public const int PRODUCCT_PRIZM40 = 30;

        public const int PRODUCT_PRIZM80 = 41;

        public const int PRODUCT_PRIZM140 = 500;

        public const int PRODUCT_PRIZM10_EV2 = 501;

        public const int PRODUCT_PRIZM15_EV2 = 502;

        public const int PRODUCT_PRIZM20_EV2 = 503;

        public const int PRODUCT_PRIZM40_EV2 = 504;

        public const int PRODUCT_PRIZM50_EV2 = 505;

        public const int PRODUCT_PRIZM80_EV2 = 506;

        public const int PRODUCT_PRIZM90_EV2 = 507;

        public const int PRODUCT_HIO_05 = 508;

        public const int PRODUCT_PRIZM140_EV3 = 509;

        public const int PRODUCT_PRIZM280 = 510;

        public const int PRODUCT_PRIZM210 = 511;

        public const int PRODUCT_PRIZM230 = 512;

        public const int PRODUCT_PLC_CARD = 514;

        public const int PRODUCT_PRIZM540 = 520;

        public const int PRODUCT_PRIZM285 = 513;

        public const int PRODUCT_PRIZM290N = 646;

        public const int PRODUCT_PRIZM290E = 647;

        public const int PRODUCT_PRIZM720 = 721;

        public const int PRODUCT_PRIZM720N = 688;

        public const int PRODUCT_PRIZM545 = 521;

        public const int PRODUCT_PRIZM550N = 686;

        public const int PRODUCT_PRIZM550E = 687;

        public const int PRODUCT_PRIZM760n = 522;

        public const int PRODUCT_PRIZM760 = 523;

        public const int PRODUCT_PRIZM760E = 525;

        public const int PRODUCT_PRIZM760nk = 526;

        public const int PRODUCT_PRIZMCE545 = 1001;

        public const int PRODUCT_PRIZMCE760 = 1002;

        public const int PRODUCT_PZM4_0216 = 2161;

        public const int PRODUCT_PZM4_1300 = 13001;

        public const int PRODUCT_PZM4_1600 = 5701;

        public const int PRODUCT_PZM4_1615 = 5702;

        public const int PRODUCT_PZM4_2600 = 5703;

        public const int PRODUCT_PZM4_2615 = 5704;

        public const int PRODUCT_NQ3_TQ000_B = 3503;

        public const int PRODUCT_NQ3_TQ010_B = 3504;

        public const int PRODUCT_NQ3_MQ000_B = 3801;

        public const int PRODUCT_NQ5_MQ000_B = 5706;

        public const int PRODUCT_NQ5_MQ010_B = 5707;

        public const int PRODUCT_NQ5_SQ000_B = 5708;

        public const int PRODUCT_NQ5_SQ010_B = 5709;

        public const int PRODUCT_NQ5_MQ001_B = 5710;

        public const int PRODUCT_NQ5_MQ011_B = 5711;

        public const int PRODUCT_NQ5_SQ001_B = 5712;

        public const int PRODUCT_NQ5_SQ011_B = 5713;

        public const int ianalogInputVal = 0;

        public const int ianalogOutputVal = 0;

        public const int PRODUCT_HIO_05_1 = 802;

        public const int PRODUCT_HIO_05_2 = 803;

        public const int PRODUCT_HIO_05_3 = 804;

        public const int PRODUCT_HIO_05_4 = 805;

        public const int PRODUCT_HIO_05_5 = 806;

        public const int PRODUCT_HIO_10_1 = 821;

        public const int PRODUCT_HIO_10_2 = 822;

        public const int PRODUCT_HIO_10_3 = 823;

        public const int PRODUCT_HIO_10_4 = 824;

        public const int PRODUCT_HIO_10_5 = 825;

        public const int PRODUCT_HIO_12_1 = 841;

        public const int PRODUCT_HIO_12_2 = 842;

        public const int PRODUCT_HIO_12_3 = 843;

        public const int PRODUCT_HIO_12_4 = 844;

        public const int PRODUCT_HIO_18_1 = 861;

        public const int PRODUCT_HIO_18_2 = 862;

        public const int PRODUCT_HIO_18_3 = 863;

        public const int PRODUCT_HIO_18_4 = 864;

        public const int PRODUCT_HIO_18_5 = 845;

        public const int PRODUCT_HIO_50_1 = 881;

        public const int PRODUCT_HIO_50_2 = 882;

        public const int PRODUCT_HIO_50_3 = 883;

        public const int PRODUCT_HIO_50_4 = 884;

        public const int PRODUCT_HIO_50_5 = 885;

        public const int PRODUCT_HIO_50_6 = 886;

        public const int PRODUCT_HIO_50_7 = 887;

        public const int PRODUCT_HIO_50_8 = 888;

        public const int PRODUCT_HIO_140_1 = 901;

        public const int PRODUCT_HIO_140_2 = 902;

        public const int PRODUCT_HIO_140_3 = 903;

        public const int PRODUCT_HIO_140_4 = 904;

        public const int PRODUCT_HIO_140_5 = 905;

        public const int PRODUCT_HIO_140_6 = 906;

        public const int PRODUCT_HIO_140_7 = 907;

        public const int PRODUCT_HIO_140_8 = 908;

        public const int PRODUCT_HIO_230_1 = 601;

        public const int PRODUCT_HIO_230_2 = 603;

        public const int PRODUCT_HIO_230_3 = 604;

        public const int PRODUCT_HIO_230_4 = 605;

        public const int PRODUCT_HIO_230_5 = 602;

        public const int PRODUCT_HIO_285_1 = 641;

        public const int PRODUCT_HIO_285_2 = 642;

        public const int PRODUCT_HIO_285_3 = 643;

        public const int PRODUCT_HIO_285_4 = 644;

        public const int PRODUCT_HIO_545_1 = 681;

        public const int PRODUCT_HIO_545_2 = 682;

        public const int PRODUCT_HIO_545_3 = 683;

        public const int PRODUCT_HIO_545_4 = 684;

        public const int PRODUCT_PRIZM_760_2 = 524;

        public const int PRODUCT_RDIO_1612_A = 921;

        public const int PRODUCT_RDIO_1612_B = 922;

        public const int PRODUCT_RDIO_1612_C = 923;

        public const int PRODUCT_RDIO_1612_D = 924;

        public const int PRODUCT_RDIO_0808_A = 931;

        public const int PRODUCT_RDIO_0808_B = 932;

        public const int PRODUCT_RDIO_0808_C = 933;

        public const int PRODUCT_FIOA_0402_A = 701;

        public const int PRODUCT_PZ4030M_E = 1102;

        public const int PRODUCT_PZ4030MN_E = 1103;

        public const int PRODUCT_PZ4035TN_E = 1105;

        public const int PRODUCT_PZ4057M_E = 1106;

        public const int PRODUCT_PZ4057TN_E = 1108;

        public const int PRODUCT_PZ4084TN_E = 1109;

        public const int PRODUCT_PZ4121TN_E = 1110;

        public const int PRODUCT_GSM900 = 2001;

        public const int PRODUCT_GSM901 = 2002;

        public const int PRODUCT_GSM910 = 2003;

        public const int PRODUCT_GWY00 = 2021;

        public const int PRODUCT_FL010 = 913;

        public const int PRODUCT_FL050 = 914;

        public const int PRODUCT_FL050_V2 = 920;

        public const int PRODUCT_FL011_S1 = 912;

        public const int PRODUCT_FL011 = 915;

        public const int PRODUCT_FL011_S3 = 917;

        public const int PRODUCT_GPU288_3S = 941;

        public const int PRODUCT_GPU200_3S = 942;

        public const int PRODUCT_GPU232_3S = 943;

        public const int PRODUCT_GPU230_3S = 944;

        public const int PRODUCT_GPU110_3S = 945;

        public const int PRODUCT_GPU105_3S = 946;

        public const int PRODUCT_GPU120_3S = 947;

        public const int PRODUCT_GPU122_3S = 948;

        public const int PRODUCT_FL100 = 918;

        public const int PRODUCT_FL100_S0 = 919;

        public const int PRODUCT_FL005_0808RP = 927;

        public const int PRODUCT_FL005_0808RP0201L = 928;

        public const int PRODUCT_FL005_0604P = 951;

        public const int PRODUCT_FL005_0808P = 952;

        public const int PRODUCT_FL005_0808P0201L = 953;

        public const int PRODUCT_FL005_0604N = 954;

        public const int PRODUCT_FL005_0808N = 955;

        public const int PRODUCT_FL005_0808N0201L = 956;

        public const int PRODUCT_FL005_1616P0201L_S1 = 963;

        public const int PRODUCT_FL055 = 970;

        public const int PRODUCT_FL005_0808RP0402U = 957;

        public const int PRODUCT_FL005_1616RP0201L = 958;

        public const int PRODUCT_FL005_1616P0201L = 959;

        public const int PRODUCT_FL005_1616N0201L = 960;

        public const int PRODUCT_FL005_1616RP = 961;

        public const int PRODUCT_FL005_1616P = 962;

        public const int PRODUCT_FP2020_L0808RP_A0401L = 1171;

        public const int PRODUCT_FP2020_L0808P_A0401L = 1172;

        public const int PRODUCT_FP2020_L0604P_A0401L = 1173;

        public const int PRODUCT_FP4020MR = 1150;

        public const int PRODUCT_FP4020MR_L0808P = 1151;

        public const int PRODUCT_FP4020MR_L0808N = 1152;

        public const int PRODUCT_FP4020MR_L0808R = 1153;

        public const int PRODUCT_FP4020MR_L0808R_S3 = 1154;

        public const int PRODUCT_FP3020MR_L1608RP = 1155;

        public const int PRODUCT_FP4030MR = 1200;

        public const int PRODUCT_FP4030MR_E = 1209;

        public const int PRODUCT_FP4030MR_L1208R = 1210;

        public const int PRODUCT_FP4030MR_0808R_A0400_S0 = 1228;

        public const int PRODUCT_FP4030MR_L1210RP_A0402U = 1229;

        public const int PRODUCT_FP4030MR_L1210P_A0402U = 1261;

        public const int PRODUCT_FP4030MR_L1210RP = 1262;

        public const int PRODUCT_FP4030MR_L1210P = 1263;

        public const int PRODUCT_FP4030MR_L0808R_A0400U = 1264;

        public const int PRODUCT_FP4030MT_HORIZONTAL = 1211;

        public const int PRODUCT_FP4030MT_VERTICAL = 1212;

        public const int PRODUCT_FP4030MT_S1_HORIZONTAL = 1213;

        public const int PRODUCT_FP4030MT_S1_VERTICAL = 1214;

        public const int PRODUCT_FP4030MT_L0808RN_A0201 = 1215;

        public const int PRODUCT_FP4030MT_L0808RP_A0201 = 1216;

        public const int PRODUCT_FP4030MT_REV1 = 1218;

        public const int PRODUCT_FP4030MT_L0808RN = 1219;

        public const int PRODUCT_FP4030MT_L0808RP = 1220;

        public const int PRODUCT_FP4030MT_L0808RP_A0201L = 1226;

        public const int PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL = 1227;

        public const int PRODUCT_FP4030MT_L0808RP_A0201_S0 = 1217;

        public const int PRODUCT_FP4030MT_L0808P = 1301;

        public const int PRODUCT_FP4030MT_L0808P_A0201U = 1302;

        public const int PRODUCT_FP4030MT_REV1_VERTICAL = 1221;

        public const int PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL = 1222;

        public const int PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL = 1223;

        public const int PRODUCT_FP4030MT_L0808RN_VERTICAL = 1224;

        public const int PRODUCT_FP4030MT_L0808RP_VERTICAL = 1225;

        public const int PRODUCT_FP4030MT_L0808P_A0402L = 1303;

        public const int PRODUCT_FP4035T = 1231;

        public const int PRODUCT_FP4035T_E = 1230;

        public const int PRODUCT_FP4035TN = 1232;

        public const int PRODUCT_FP4035TN_E = 1233;

        public const int PRODUCT_PRIZM_710_S0 = 1234;

        public const int PRODUCT_FP4057T = 1251;

        public const int PRODUCT_FP4057T_E = 1250;

        public const int PRODUCT_FP4057T_S2 = 1252;

        public const int PRODUCT_FP4057TN = 1253;

        public const int PRODUCT_FP4057TN_E = 1254;

        public const int PRODUCT_FP4057T_E_S1 = 1255;

        public const int PRODUCT_FP4057T_E_VERTICAL = 1256;

        public const int PRODUCT_MICRO_PLC = 909;

        public const int PRODUCT_MICRO_PLC_ETHERNET = 910;

        public const int PRODUCT_TRPMIU0300L = 1330;

        public const int PRODUCT_TRPMIU0300A = 1331;

        public const int PRODUCT_TRPMIU0500L = 1350;

        public const int PRODUCT_TRPMIU0500A = 1351;

        public const int PRODUCT_TRPMIU0300E = 1333;

        public const int PRODUCT_TRPMIU0500E = 1354;

        public const int PRODUCT_TRPMIU0400E = 1343;

        public const int PRODUCT_TRPMIU0700E = 1373;

        public const int PRODUCT_FP4020M_L0808P_A = 1203;

        public const int PRODUCT_FP4020M_L0808P_A0400R = 1204;

        public const int PRODUCT_FP4020M_L0808N_A = 1205;

        public const int PRODUCT_FP4020M_L0808N_AR = 1206;

        public const int PRODUCT_FP4020M_L0808R_A = 1207;

        public const int PRODUCT_FH9020MR = 1400;

        public const int PRODUCT_FH9020MR_L0808P = 1401;

        public const int PRODUCT_FH9020MR_L0808N = 1402;

        public const int PRODUCT_FH9020MR_L0808R = 1403;

        public const int PRODUCT_FH9030MR = 1411;

        public const int PRODUCT_FH9030MR_E = 1412;

        public const int PRODUCT_FH9030MR_L1208R = 1413;

        public const int PRODUCT_FH9035T_E = 1421;

        public const int PRODUCT_FH9035T = 1422;

        public const int PRODUCT_FH9057T_E = 1431;

        public const int PRODUCT_FH9057T = 1432;

        public const int PRODUCT_PLC7008A_ML = 931;

        public const int PRODUCT_PLC7008A_ME = 932;

        public const int PRODUCT_HMC7030A_M = 1531;

        public const int PRODUCT_HMC7030A_L = 1532;

        public const int PRODUCT_HMC7035A_M = 1551;

        public const int PRODUCT_HMC7057A_M = 1571;

        public const int PRODUCT_HMC7043A_M = 1543;

        public const int PRODUCT_HMC7070A_M = 1573;

        public const int PRODUCT_FP3043T = 1701;

        public const int PRODUCT_FP3043TN = 1703;

        public const int PRODUCT_FP3070T = 1711;

        public const int PRODUCT_FP3070TN = 1713;

        public const int PRODUCT_FP3102T = 1721;

        public const int PRODUCT_FP3102TN = 1723;

        public const int PRODUCT_FP3070T_E = 1712;

        public const int PRODUCT_FP3070TN_E = 1715;

        public const int PRODUCT_FP3102T_E = 1722;

        public const int PRODUCT_FP3102TN_E = 1725;

        public const int PRODUCT_OIS43E_Plus = 1803;

        public const int PRODUCT_OIS72E_Plus = 1813;

        public const int PRODUCT_OIS100E_Plus = 1823;

        public const int PRODUCT_FP3043T_E = 1702;

        public const int PRODUCT_FP3043TN_E = 1704;

        public const int PRODUCT_GTXL07N = 1714;

        public const int PRODUCT_GTXL10N = 1724;

        public const int PRODUCT_FP5043T = 1241;

        public const int PRODUCT_FP5043TN = 1242;

        public const int PRODUCT_FP5043T_E = 1240;

        public const int PRODUCT_FP5043TN_E = 1243;

        public const int PRODUCT_FP5070T = 1271;

        public const int PRODUCT_FP5070TN = 1272;

        public const int PRODUCT_FP5070T_E = 1270;

        public const int PRODUCT_FP5070TN_E = 1273;

        public const int PRODUCT_FP5070T_E_S2 = 1274;

        public const int PRODUCT_FP5121T = 1280;

        public const int PRODUCT_FP5121TN = 1281;

        public const int PRODUCT_FP5121TN_S0 = 1282;

        public const int PRODUCT_OIS12 = 1600;

        public const int PRODUCT_OIS10_Plus = 1603;

        public const int PRODUCT_OIS22_Plus = 1612;

        public const int PRODUCT_OIS20_Plus = 1613;

        public const int PRODUCT_OIS55_Plus = 1630;

        public const int PRODUCT_OIS60_Plus = 1650;

        public const int PRODUCT_OIS40_Plus_HORIZONTAL = 1621;

        public const int PRODUCT_OIS40_Plus_VERTICAL = 1622;

        public const int PRODUCT_OIS42_Plus = 1623;

        public const int PRODUCT_OIS42L_Plus = 1624;

        public const int PRODUCT_OIS45_Plus = 1642;

        public const int PRODUCT_OIS45E_Plus = 1643;

        public const int PRODUCT_OIS70_Plus = 1672;

        public const int PRODUCT_OIS70E_Plus = 1673;

        public const int PRODUCT_OIS120A = 1681;

        public const int PRODUCT_FP3035T_24 = 1130;

        public const int PRODUCT_FP3035T_5 = 1132;

        public const int PRODUCT_OIS24 = 1634;

        public const int PRODUCT_OIS25 = 1635;

        public const int PRODUCT_CPU_300 = 980;

        public const int PRODUCT_CPU_111_RP = 981;

        public const int PRODUCT_CPU_120_ARP = 982;

        public const int PRODUCT_CPU_100_P = 983;

        public const int PRODUCT_CPU_110_P = 984;

        public const int PRODUCT_CPU_120_AP = 985;

        public const int PRODUCT_CPU_100_N = 986;

        public const int PRODUCT_CPU_110_N = 987;

        public const int PRODUCT_CPU_120_AN = 988;

        public const int PRODUCT_HH5L_B0604D_P = 971;

        public const int PRODUCT_HH5L_B0808D_P = 972;

        public const int PRODUCT_HH5L_B1616D_P = 973;

        public const int PRODUCT_HH5L_B1616D_RP = 974;

        public const int PRODUCT_HH5L_B0201A0808D_P = 975;

        public const int PRODUCT_HH5L_B0201A1616D_RP = 976;

        public const int PRODUCT_HH5L_B0402AU0808D_RP = 977;

        public const int PRODUCT_HH1L_000 = 978;

        public const int PRODUCT_HH5P_H43_NS = 1901;

        public const int PRODUCT_HH5P_H43_S = 1902;

        public const int PRODUCT_HH5P_H70_NS = 1903;

        public const int PRODUCT_HH5P_H70_S = 1904;

        public const int PRODUCT_HH5P_H100_NS = 1905;

        public const int PRODUCT_HH5P_H100_S = 1906;

        public const int PRODUCT_HH5P_HP200808D_P = 1907;

        public const int PRODUCT_HH5P_HP301208D_R = 1908;

        public const int PRODUCT_HH5P_HP300201U0808_RP = 1909;

        public const int PRODUCT_HH5P_HP300201L0808_RP = 1910;

        public const int PRODUCT_HH5P_HP43_NS = 1911;

        public const int PRODUCT_HH5P_HP70_NS = 1912;

        public const int RECTANGLE = 0;

        public const int CIRCLE = 1;

        public const int ROUNDRECTANGLE = 2;

        public const int ELLIPSE = 3;

        public const int RECTANGLE_OBJECTTYPE = 10;

        public const int ELLIPSE_OBJECTTYPE = 11;

        public const int BITBUTTON_OBJECTTYPE = 61;

        public const int WORDBUTTON_OBJECTTYE = 63;

        public const int WORDLAMP_OBJECTTYPE = 64;

        public const int TEXTWIZARD_OBJECTTYPE = 41;

        public const int TEXTOBJECT_OBJECTTYPE = 1;

        public const int BITLAMP_OBJECTTYPE = 62;

        public const int BARGRAPH_OBJECTTYPE = 66;

        public const int LINE_OBJECTTYPE = 13;

        public const int ROUNDRECT_OBJECTTYPE = 12;

        public const int BITMAP_OBJECTTYPE = 0;

        public const int SINGLEBARGRAPH_OBJECTTYPE = 21;

        public const int TIME_OBJECTTYPE = 7;

        public const int DATE_OBJECTTYPE = 8;

        public const int ANALOGMETER_OBJECTTYPE = 65;

        public const int DATAENTRYREGISTER_OBJECTTYPE = 3;

        public const int DATAENTRYCOIL_OBJECTTYPE = 2;

        public const int DISPLAYDATACOIL_OBJECTTYPE = 4;

        public const int DISPLAYDATAREGISTER_OBJECTTYPE = 5;

        public const int DISPLAYDATAREGISTERTEXT_OBJECTTYPE = 6;

        public const int KEYPAD_OBJECTTYPE = 67;

        public const int KEYPADPASSWORD_OBJECTTYPE = 42;

        public const int ASCIIKEYPAD_OBJECTTYPE = 46;

        public const int EDITPASSWORD_OBJECTTYPE = 45;

        public const int TREND_OBJECTTYPE = 68;

        public const int ALARM_OBJECTTYPE = 16;

        public const int HISTORICALTREND_OBJECTTYPE = 69;

        public const int CUSTOMKEYPAD_OBJECTTYPE = 95;

        public const int KEYPADWITHTAG_OBJECTTYPE = 98;

        public const int SPEEDOMETER_OBJECTTYPE = 99;

        public const int POLYLINE_OBJECTTYPE = 90;

        public const int POLYGON_OBJECTTYPE = 91;

        public const string Rectangular = "Generic Square";

        public const string Circular = "Circle";

        public const string RoundRectangle = "Rounded Rectangle";

        public const string Invisible = "Invisible";

        public const string ChangeColor = "Change_Color";

        public const string Bitmap = "Bitmap";

        public const string Task = "Task";

        public const string ButtonStyle = "ButtonStyle";

        public const string LampStyle = "LampStyle";

        public const string Style = "Style";

        public const string ScreenColor = "ScreenColor";

        public const string ScreenNumber = "ScreenNumber";

        public const string Number = "Number";

        public const string ScreenName = "ScreenName";

        public const string PopUpScreenNumber = "PopUpScreenNumber";

        public const string PopUpScreenName = "PopUpScreenName";

        public const string Text = "Text";

        public const string SampleDefaultText = "Sample Text";

        public const string Pattern = "Pattern";

        public const string PatternColor = "PatternColor";

        public const string BackgroundColor = "BackgroundColor";

        public const string TextColor = "TextColor";

        public const string Font = "Font";

        public const string Description = "Description";

        public const string Category = "Category";

        public const string On = "On";

        public const string Off = "Off";

        public const string Simulation = "Simulation";

        public const string Label = "Label";

        public const string LabelX = "X-Label";

        public const string LabelY = "Y-Label";

        public const string RLabel = "RLabel";

        public const string Top = "Top";

        public const string Bottom = "Bottom";

        public const string OnText = "OnText";

        public const string OnTextPattern = "OnTextPattern";

        public const string OnTextPatternColor = "OnTextPatternColor";

        public const string OnTextBackgroundColor = "OnTextBackgroundColor";

        public const string OnTextColor = "OnTextColor";

        public const string OnTextFont = "OnTextFont";

        public const string OffText = "OffText";

        public const string OffTextPattern = "OffTextPattern";

        public const string OffTextPatternColor = "OffTextPatternColor";

        public const string OffTextBackgroundColor = "OffTextBackgroundColor";

        public const string OffTextColor = "OffTextColor";

        public const string OffTextFont = "OffTextFont";

        public const string LabelText = "LabelText";

        public const string LabelPattern = "LabelPattern";

        public const string LabelPatternColor = "LabelPatternColor";

        public const string LabelBackgroundColor = "LabelBackgroundColor";

        public const string LabelTextColor = "LabelTextColor";

        public const string LabelPosition = "LabelPosition";

        public const string LabelTextFont = "LabelTextFont";

        public const string FeedbackTag = "FeedbackTag";

        public const string Border = "Border";

        public const string Previous = "Previous";

        public const string Next = "Next";

        public const string GoTo = "GoTo";

        public const string GoToScreen = "GoTo Screen";

        public const string GoToNextScreen = "Goto Next Screen";

        public const string GoToPreviousScreen = "Goto Previous Screen";

        public const string GoToPopUpScreen = "Popup";

        public const string HidePopUpScreen = "X";

        public const string Set = "Set";

        public const string Reset = "Reset";

        public const string Momentary = "Momentary";

        public const string Toggle = "Toggle";

        public const string HoldOn = "Hold On";

        public const string HoldOff = "Hold Off";

        public const string TurnBiton = "Turn Bit On";

        public const string TurnBitoff = "Turn Bit Off";

        public const string ToggleBit = "Toggle Bit";

        public const string MomentaryBit = "Momentary Bit";

        public const string FileName = "FileName";

        public const string FileNameOff = "FileNameOff";

        public const string FromPictureLibraryON = "FromPictureLibraryON";

        public const string FromPictureLibraryOFF = "FromPictureLibraryOFF";

        public const string CannotOpenBitmap = "Cannot open Bitmap";

        public const string BinaryFile = "Logger.BIN";

        public const string CSVFile = "Logg.csv";

        public const string CSVFile_New = "Logger.csv";

        public const string CustomKeypad = "Custom Keypad";

        public const string HistAlarmBinaryFile = "HistAlarmData.BIN";

        public const string HistAlarmCSVFile = "HistAlarm.csv";

        public const string Error = "Error";

        public const string iNumber = "iNumber";

        public const string uiNumber = "uiNumber";

        public const string fNumber = "fNumber";

        public const string DfNumber = "DfNumber";

        public const string hNumber = "hNumber";

        public const string bNumber = "bNumber";

        public const string BCDNumber = "BCDNumber";

        public const string FbTagAddress = "FBTagAddress";

        public const string FbTagName = "FBTagName";

        public const string Delay = "Delay";

        public const string Wait = "Wait";

        public const string ScreenType = "ScreenType";

        public const string TopLeft = "TopLeft";

        public const string BottomRight = "BottomRight";

        public const string Size = "Size";

        public const string Password = "Password";

        public const string ScreenProperties = "ScreenProperties";

        public const string TemplateProperties = "TemplateProperties";

        public const string ScreenPrintColumns = "ScreenPrintColumns";

        public const string CharactersToPrint = "CharactersToPrint";

        public const string AssociatedScreen = "AssociatedScreen";

        public const string NoofTemplates = "NoofTemplates";

        public const string UseTemplate = "UseTemplate";

        public const string Template1 = "Template1";

        public const string Template2 = "Template2";

        public const string Template3 = "Template3";

        public const string Template4 = "Template4";

        public const string Template5 = "Template5";

        public const string Template6 = "Template6";

        public const string Template7 = "Template7";

        public const string Template8 = "Template8";

        public const string Template9 = "Template9";

        public const string Template10 = "Template10";

        public const string Base = "Base";

        public const string GSM = "GSM";

        public const string PopUp = "Popup";

        public const string Template = "Template";

        public const string Bookmark = "Bookmark";

        public const string ScreenStatus = "ScreenMemoryStatus";

        public const string NumericKeypad = "Numeric Keypad";

        public const string HexKeypad = "Hex Keypad";

        public const string BitKeypad = "Bit Keypad";

        public const string AsciiKeypad = "Ascii Keypad";

        public const string Name = "Name";

        public const string ShowHideSelect = "ShowHideSelect";

        public const string Direction = "Direction";

        public const string FillPattern = "FillPattern";

        public const string EnableTaskBit = "EnableTaskBit";

        public const string EBTagAddress = "EBTagAddress";

        public const string EBTagName = "EBTagName";

        public const string FlashSelect = "FlashSelect";

        public const string ColorSelect = "ColorSelect";

        public const string IECColorSelect = "IECColorSelect";

        public const string ShowHide = "ShowHide";

        public const string Flash = "Flash";

        public const string ColorAnimation = "ColorAnimation";

        public const string LineThickness = "LineThickness";

        public const string TagList = "TagList";

        public const string LowRange = "LowRange";

        public const string HighRange = "HighRange";

        public const string BitShowWhen = "BitShowWhen";

        public const string RegShowWhen = "RegShowWhen";

        public const string KeypadLabel = "Key Pad";

        public const string MeterLabel = "Not enough size to Draw";

        public const string Bar = "Bar";

        public const string Axis = "Axis";

        public const string AxisColor = "AxisColor";

        public const string AxisLabel = "AxisLabel";

        public const string AxisLabelText = "AxisLabelText";

        public const string AxisLabelBackgroundColor = "AxisLabelBackgroundColor";

        public const string AxisLabelTextColor = "AxisLabelTextColor";

        public const string FillColor = "FillColor";

        public const string LineColor = "LineColor";

        public const string FillPatternColor = "FillPatternColor";

        public const string FontColor = "FontColor";

        public const string BackColor = "BackColor";

        public const string ActiveAndAcknowledgeAlarmColor = "ActiveAndAcknowledgeAlarmColor";

        public const string ActiveAndUnacknowledgeAlarmColor = "ActiveAndUnacknowledgeAlarmColor";

        public const string InActiveAndUnacknowledgeAlarmColor = "InActiveAndUnacknowledgeAlarmColor";

        public const string ScrollbarStyle = "ScrollbarStyle";

        public const string AlarmWindowInterColoumnDistance = "AlarmWindowInterColoumnDistance";

        public const string TextBorder = "TextBorder";

        public const string DisplayRange = "DisplayRange";

        public const string DisplayRangeProperties = "DisplayRangeProperties";

        public const string DisplayDivisions = "DisplayDivisions";

        public const string DivisionsProperties = "DivisionsProperties";

        public const string ShowLabel = "ShowLabel";

        public const string BargraphLabelFont = "BargraphLabelFont";

        public const string LabelAtBottom = "LabelAtBottom";

        public const string LabelColor = "LabelColor";

        public const string LabelFontColor = "LabelFontColor";

        public const string ColorPatchPropertiesBrowser = "ColorPatchPropertiesBrowser";

        public const string BorderColor = "BorderColor";

        public const string Language = "Language";

        public const string StateText = "StateText";

        public const string AlarmOrder = "AlarmOrder";

        public const string NewSize = "NewSize";

        public const string Accesslevelpropscr = "AccessLevelToShow";

        public const int NoFlash = 0;

        public const int SlowFlash = 128;

        public const int MediumFlash = 129;

        public const int FastFlash = 130;

        public const ushort MAX_TOUCH_SCREENS = 64990;

        public const ushort MAX_KEYPAD_SCREENS = 65534;

        public const ushort TOTAL_POPUP_SCREENS = 534;

        public const ushort START_POPUP_SCREEN = 65001;

        public const ushort END_TEMPLATE_SCREENS = 65000;

        public const ushort START_TEMPLATE_SCREEN = 64991;

        public const ushort MAX_TEMPLATE_SCREENS = 10;

        public const int ADVANCEDPICTURE_OBJECTTYPE = 96;

        public const int XYPlot_OBJECTTYPE = 97;

        public const int ERROR_FILE_NOT_FOUND = 2;

        public const int ERROR_ACCESS_DENIED = 5;

        public const string XML_NAME_TAG = "Name";

        public const string XML_PATH_TAG = "Path";

        public const string XML_MODEL_TAG = "model";

        public const string XML_BMP_NAME_TAG = "BMPFile";

        public const string XML_PROJCET_TAG = "Project";

        public const string XML_INITDATA_TAG = "initdata";

        public const string XML_PROJCET_DATA_TAG = "ProjectData";

        public const string XML_SPLASHSCREEN_TAG = "SplashScreen";

        public const string XML_PLC_MODEL_DATA_TAG = "ModelData";

        public const string XML_PRODUCT_TAG = "Product";

        public const int MAX_NO_RECENT_PROJ = 4;

        public const string FirstOperation = "FirstOperation";

        public const string SecondOperation = "SecondOperation";

        public const string DataType = "DataType";

        public const string FirstOperand = "FirstOperand";

        public const string SecondOperand = "SecondOperand";

        public const string Format = "Format";

        public const string Length = "Length";

        public const string LeadingZerosBlank = "LeadingZerosBlank";

        public const string FlashAnimation = "FlashAnimation";

        public const string MeterBackground = "MeterBackground";

        public const string MeterColor = "MeterColor";

        public const string MeterBGColor = "MeterBGColor";

        public const string ColorRange = "ColorRange";

        public const string ColorPatches = "ColorPatches";

        public const string FirstColorPatchProperties = "FirstColorPatchProperties";

        public const string SecondColorPatchProperties = "SecondColorPatchProperties";

        public const string ThirdColorPatchProperties = "ThirdColorPatchProperties";

        public const string FourthColorPatchProperties = "FourthColorPatchProperties";

        public const string FifthColorPatchProperties = "FifthColorPatchProperties";

        public const string StartAngle = "StartAngle";

        public const string EndAngle = "EndAngle";

        public const string StartValue = "StartValue";

        public const string EndValue = "EndValue";

        public const string iStartValue = "iStartValue";

        public const string iEndValue = "iEndValue";

        public const string hStartValue = "hStartValue";

        public const string hEndValue = "hEndValue";

        public const string uiStartValue = "uiStartValue";

        public const string uiEndValue = "uiEndValue";

        public const string bStartValue = "bStartValue";

        public const string bEndValue = "bEndValue";

        public const string fMaximumDisplayRange = "fMaximumDisplayRange";

        public const string fMinimumDisplayRange = "fMinimumDisplayRange";

        public const string iMaximumDisplayRange = "iMaximumDisplayRange";

        public const string iMinimumDisplayRange = "iMinimumDisplayRange";

        public const string uiMaximumDisplayRange = "uiMaximumDisplayRange";

        public const string uiMinimumDisplayRange = "uiMinimumDisplayRange";

        public const string hMaximumDisplayRange = "hMaximumDisplayRange";

        public const string hMinimumDisplayRange = "hMinimumDisplayRange";

        public const string bMaximumDisplayRange = "bMaximumDisplayRange";

        public const string bMinimumDisplayRange = "bMinimumDisplayRange";

        public const string MeterStyle = "MeterStyle";

        public const string PatchHighLimit = "PatchHighLimit";

        public const string PatchLowLimit = "PatchLowLimit";

        public const string NumericKey0 = "0";

        public const string NumericKey1 = "1";

        public const string NumericKey2 = "2";

        public const string NumericKey3 = "3";

        public const string NumericKey4 = "4";

        public const string NumericKey5 = "5";

        public const string NumericKey6 = "6";

        public const string NumericKey7 = "7";

        public const string NumericKey8 = "8";

        public const string NumericKey9 = "9";

        public const string NumericKeyA = "A";

        public const string NumericKeyB = "B";

        public const string NumericKeyC = "C";

        public const string NumericKeyD = "D";

        public const string NumericKeyE = "E";

        public const string NumericKeyF = "F";

        public const string SignKey = "+/-";

        public const string ClearDataEntry = "CLR";

        public const string AcceptDataEntry = "ENT";

        public const string CancelDataEntry = "ESC";

        public const string IncreaseDigitBy1 = "^";

        public const string DecreaseDigitBy1 = "v";

        public const string MoveCursorLeft = "<-";

        public const string MoveCursorRight = "->";

        public const string IncreaseValueBy1 = "INC";

        public const string DecreaseValueBy1 = "DCR";

        public const string TurnBitOn = "ON";

        public const string TurnBitOff = "OFF";

        public const string SetRTC = "Set RTC";

        public const string PrintData = "Print Data";

        public const string KeysSpecificTask = "Key's Specific Task";

        public const string GotoPopUpScreen = "Goto Popup Screen";

        public const string TagAddress = "TagAddress";

        public const string TagName = "TagName";

        public const string TagA = "TagA";

        public const string TagB = "TagB";

        public const string TagAadderess = "TagAadderess";

        public const string TagBadderess = "TagBadderess";

        public const string OnTextBorderStyle = "OnTextBorderStyle";

        public const string OffTextBorderStyle = "OffTextBorderStyle";

        public const string LabelBorderStyle = "LabelBorderStyle";

        public const string BorderStyle = "BorderStyle";

        public const string Keypad = "Keypad";

        public const string FrameAlignment = "FrameAlignment";

        public const string TopLeftColor = "TopLeftColor";

        public const string BottomRightColor = "BottomRightColor";

        public const string TopRightColor = "TopRightColor";

        public const string BottomLeftColor = "BottomLeftColor";

        public const string FrameWidth = "FrameWidth";

        public const string OnTextTopLeftColor = "OnTextTopLeftColor";

        public const string OnTextBottomRightColor = "OnTextBottomRightColor";

        public const string OnTextTopRightColor = "OnTextTopRightColor";

        public const string OnTextBottomLeftColor = "OnTextBottomLeftColor";

        public const string OnTextFrameWidth = "OnTextFrameWidth";

        public const string OnTextFrameAlignment = "OnTextFrameAlignment";

        public const string OffTextTopLeftColor = "OffTextTopLeftColor";

        public const string OffTextBottomRightColor = "OffTextBottomRightColor";

        public const string OffTextTopRightColor = "OffTextTopRightColor";

        public const string OffTextBottomLeftColor = "OffTextBottomLeftColor";

        public const string OffTextFrameWidth = "OffTextFrameWidth";

        public const string OffTextFrameAlignment = "OffTextFrameAlignment";

        public const string strAddValueToTag = "Add Value To Tag";

        public const string strSubtractValueFromTag = "Sub Value From Tag";

        public const string strAddTagBToTagA = "Add TagB To TagA";

        public const string strSubTagBFromTagA = "Subtract TagB From TagA";

        public const string Id = "Id";

        public const string Ethernet_UploadEthernetSettingBinaryFile = "EthUpld.bin";

        public const string strSubtractContsValueFromTag = "Subtract Constant Value From Tag";

        public const string ShowEthernetConfigurationScreen = "Show Ethernet Configuration Screen";

        public const string ConfirmEthernetConfigurationScreenSetting = "Confirm Ethernet Setting";

        public const string CancelEthernetConfigurationScreenSetting = "Cancel Ethernet Setting";

        public const string ShowLoginScreen = "Show Login Screen";

        public const string Logout = "Log Out";

        public const string ChangeLoginScreenPassword = "Change Password";

        public const string CopyScreenToSDCard = "Copy Screen data to SD Card";

        public const string TextVal = "TextVal";

        public const string TextBackgroundColor = "TextBackgroundColor";

        public const string TasksList = "TasksList";

        public const string Picture = "User defined Images";

        public const string FromPictureLibrary = "FromPictureLibrary";

        public const string FromPictLibrary = "From Picture Library";

        public const string DummyOnTextBackGroundColor = "DummyOnTextBackGroundColor";

        public const string DummyOnTextTextColor = "DummyOnTextTextColor";

        public const string DummyOnTextPattern = "DummyOnTextPattern";

        public const string DummyOnTextPatternColor = "DummyOnTextPatternColor";

        public const string DummyOnTextBorderStyle = "DummyOnTextBorderStyle";

        public const string DummyOnTextTopLeftcolor = "DummyOnTextTopLeftcolor";

        public const string DummyOnTextBottomRightcolor = "DummyOnTextBottomRightcolor";

        public const string DummyOnTextTopRightcolor = "DummyOnTextTopRightcolor";

        public const string DummyOnTextBottomLeftcolor = "DummyOnTextBottomLeftcolor";

        public const string DummyOffTextBackGroundColor = "DummyOffTextBackGroundColor";

        public const string DummyOffTextTextColor = "DummyOffTextTextColor";

        public const string DummyOffTextPattern = "DummyOffTextPattern";

        public const string DummyOffTextPatternColor = "DummyOffTextPatternColor";

        public const string DummyOffTextBorderStyle = "DummyOffTextBorderStyle";

        public const string DummyOffTextTopLeftcolor = "DummyOffTextTopLeftcolor";

        public const string DummyOffTextBottomRightcolor = "DummyOffTextBottomRightcolor";

        public const string DummyOffTextTopRightcolor = "DummyOffTextTopRightcolor";

        public const string DummyOffTextBottomLeftcolor = "DummyOffTextBottomLeftcolor";

        public const string Collection = "( Collection )";

        public const string WordLampText = "Word Lamp";

        public const string WordButtonText = "Word Button";

        public const string LowLimit = "LowLimit";

        public const string HighLimit = "HighLimit";

        public const string WordButtonStatePropertiesBrowser = "WordButtonStatePropertiesBrowser";

        public const string WordLampStatePropertiesBrowser = "WordLampStatePropertiesBrowser";

        public const string TitleText = "TitleText";

        public const string DisplayAreaCheck = "DisplayAreaCheck";

        public const string KeypadDispAreaBackColor = "KeypadDispAreaBackColor";

        public const string KeypadDispAreaTextColor = "KeypadDispAreaTextColor";

        public const string KeySelect = "KeySelect";

        public const string KeyBackColor = "KeyBackColor";

        public const string KeyTextColor = "KeyTextColor";

        public const string KeysGapHeight = "KeysGapHeight";

        public const string KeysGapWidth = "KeysGapWidth";

        public const string LabelBackColor = "LabelBackColor";

        public const string LabelCheck = "LabelCheck";

        public const int MaxNoofLanguages = 9;

        public const float FontSize5X7 = 7.2f;

        public const float FontSize7x14 = 9.7f;

        public const float FontSize10x14 = 14.3f;

        public const float FontSize20x24 = 27f;

        public const string strHTTPStatuscode = "HTTP/1.1 200 OK\r\n";

        public const string strHTTPStatuscodeBadRequest = "HTTP/1.1 400 OK\r\n";

        public const string strHTTPResponseHeader = "Cache-Control: no-cache, must-revalidate\r\n";

        public const string strHTTPExpiresHeader = "Expires: Sat, 26 Jul 1997 05:00:00 GMT\r\n";

        public const string strHTTPStatuscodeUnauthorizedUser = "HTTP/1.1 401 OK\r\n";

        public const string strHTTPStatuscodePageNotFound = "HTTP/1.1 404 OK\r\n";

        public const string strHTTPStatuscodeForbidden = "HTTP/1.1 403 OK\r\n";

        public const string strHTTPStatuscodeNoContent = "HTTP/1.1 204 OK\r\n";

        public const string strHTMLContentType = "Content-type: text/html\r\n\r\n";

        public const string strJSContentType = "Content-type: text/javascript\r\n\r\n";

        public const string strXMLContentType = "Content-type: text/xml\r\n\r\n";

        public const string strBMPContentType = "Content-type: image/bmp\r\n\r\n";

        public const string strJPGContentType = "Content-type: image/gpeg\r\n\r\n";

        public const string strPNGContentType = "Content-type: image/png\r\n\r\n";

        public const string strGIFContentType = "Content-type: image/gif\r\n\r\n";

        public const string strHeaderPageName = "h9.html";

        public const string strLinksPageName = "h10.html";

        public const string AlarmType = "AlarmType";

        public const ushort START_COMM_FACTORY_SCREEN = 64901;

        public const ushort END_COMM_FACTORY_SCREEN = 64910;

        public const ushort MAX_COMM_FACTORY_SCREEN = 10;

        public const ushort START_SYSTEM_FACTORY_SCREEN = 64911;

        public const ushort END_SYSTEM_FACTORY_SCREEN = 64950;

        public const ushort MAX_SYSTEM_FACTORY_SCREEN = 40;

        public const ushort START_FHWT_FACTORY_SCREEN = 64951;

        public const ushort END_FHWT_FACTORY_SCREEN = 64980;

        public const ushort MAX_FHWT_FACTORY_SCREEN = 30;

        public const string ModeSelectionMenu = "Mode Selection Menu ";

        public const string SystemSetupMenu1 = "SystemSetupMenu-1";

        public const string SystemSetupMenu2 = "SystemSetupMenu-2";

        public const string ScreenSaverTime = "Screen Saver Time";

        public const string BrightnessControl = "Brightness Control";

        public const string ContrastControl = "Contrast Control";

        public const string Serial1CommnPara = "Serial Port 1";

        public const string Serial2Commnpara = "Serial Port 2";

        public const string DateTimeSet = "Set RTC";

        public const string SystemInfo = "System Information";

        public const string ApplnErase = "Appllication Erase";

        public const string FirmwareErase = "Firmware Erase";

        public const string RetentiveMemoryErase = "Retentive Erase";

        public const string Beeper = "Beeper Settings";

        public const string Battery = "Battery Status";

        public const string FHWT232Info = "FHWT_232_Info";

        public const string FHWT485Info = "FHWT_485_Info";

        public const string FHWT232Com1Info = "FHWT_232_Com1_Info";

        public const string FHWT485Com1Info = "FHWT_485_Com1_Info";

        public const string FHWT232Com2Info = "FHWT_232_Com2_Info";

        public const string FHWT485Com2Info = "FHWT_485_Com2_Info";

        public const string FHWT1 = "FHWT-1";

        public const string FHWT2 = "FHWT-2";

        public const string USB = "USB_Test";

        public const string Result1 = "Display Result1";

        public const string Result2 = "Display Result2";

        public const string Result = "Display Result";

        public const string NQ3T1 = "NQ3-T_FHWT_1";

        public const string NQ3T2 = "NQ3-T_FHWT_2";

        public const string NQ3M1 = "NQ3-M_FHWT_1";

        public const string NQ3M2 = "NQ3-M_FHWT_2";

        public const string NQ5S1 = "NQ5-S_FHWT_1";

        public const string NQ5S2 = "NQ5-S_FHWT_2";

        public const string NQ5M1 = "NQ5-M_FHWT_1";

        public const string NQ5M2 = "NQ5-M_FHWT_2";

        public const string LCD_Keypad = "LCD-Keypad Test";

        public const string RTC_EEPROM = "RTC-EEPROM Test";

        public const string Key_Power_LED = "Key-Power LED Test";

        public const string PowerDown = "PowerDown_Test";

        public const string EthernetSetting = "Ethernet Settings";

        public const string Ethernet = "Ethernet_Test";

        public const string SystemInfo2 = "System Information 2";

        public const string Temperature = "Temp_Test";

        public const string DataLogErase = "Data Log Erase";

        public const string HistAlarmErase = "Hist Alarm Erase";

        public const string Information = "Information";

        public const char chrImportSepCharacter = ',';

        public const int ARC_OBJECTTYPE = 92;

        public const int PIE_OBJECTTYPE = 93;

        public const int CHORD_OBJECTTYPE = 94;

        public static int ScrollPosLeft;

        public static int ScrollPosTop;

        public static string projectFolderPathWithoutExtension;

        public static string _bitmapFileFolderPathOldProj;

        public static string _bitmapFileFolderPathNewProj;

        public static bool _IsEthernetSelected;

        public static bool _IsImageEdited;

        public static bool _IsXYPlotText;

        public static bool _XYSTYLE2;

        public static int _XYPlotStyle2NumberofBytes;

        public static int _AccLvlHomScrNum;

        public static int _AccLvlGoToPwrONScrNum;

        public static bool _AccLvlGoToPwrONTsk;

        public static bool _AccLvlAutoLogOff;

        public static short _AccLvlAutoLogOffTime;

        public static bool _isProductVertical;

        public static char InvertedComma;

        public static string TABstring;

        public static int ReadingPrizm4File;

        public static int communicationType;

        public static int MaxNoOfObjectOnScreen;

        public static bool _isCalledForTagUsage;

        public static bool _isScreenTaskUpdatedFromTagUsage;

        public static bool _isScreenTaskIDsTOUpdateOnTagUsage;

        public static bool _DefaultTagMemory;

        public static bool _ExpansionTagExceed;

        public static string strtext;

        public static string strstring;

        public static string strError;

        public static string imagechar;

        public static bool isMoveOutSide;

        public static bool isGroupCreated;

        public static bool isProjectSaveAs;

        public static bool isDownloading;

        public static string TagSelDefaultTagName;

        public static bool DownloadTagNames;

        public static string DefaultRegTagAddr;

        public static string DefaultBitTagAddr;

        public static string LanguageTagAddr;

        public static int DefaultRegTagId;

        public static int DefaultBitTagId;

        public static bool blIsFactoryScreen;

        public static string DefaultRegTagName;

        public static string DefaultBitTagName;

        public static string PassThruXMLFilename;

        public static bool IsHobvisionClientFL100S0;

        public static int ImportTempDefaultRegTagId;

        public static int ImportTempDefaultBitTagId;

        public static string ImportTempDefaultRegTagName;

        public static string ImportTempDefaultBitTagName;

        public static string BitInstructionName;

        //public static InstructionType INST_TYPE;

        public static System.Drawing.Rectangle LadderAreaRect;

        //public static RungManager objRungManager;

        public static bool bLadderScreenRightClick;

        public static string CounterName;

        public static string CounterInputName;

        public static string CounterOutputName;

        public static string CounterResetName;

        public static string UpDownCounterSelectionInput;

        public static int LadderSaveFileDataVersion;

        public static string BlockStartHeader;

        public static string InstructionStartHeader;

        public static string DataMonitorStartHeader;

        //public static LadderEditorMode ActiveLadderMode;

        public static bool bPlcCommunicationStatus;

        public static byte enableCalibration;

        public static string LadderSaveFileStartHeader;

        public static string UnicodeTagNamesStartHeader;

        public static bool bObjectMoved;

        public static bool bNewObjectAdded;

        public static bool bLadderEditing;

        public static bool bImportBlock;

        public static bool bUploadFromGUI;

        public static bool bIsShiftKeyPressed;

        //public static CommonConstants.LadderOperandInfo objLadderOperandInfo;

        public static int ProductIdentifier;

        public static byte UnicodeTagNameRevision;

        public static string strDefaultTagAddress;

        public static Color ColorContactOnState;

        public static Color ColorContactOffState;

        public static Color ColorFunctionBlock;

        public static Color ColorOperandVale;

        public static Color ColorForceVar;

        public static Color ColorLadderBackGroundArea;

        public static Color ColorActiveRung;

        public static Color ColorLeftMarginRung1;

        public static Color ColorLeftMarginRung2;

        public static bool ShowRegisterEntryMessage;

        public static bool AutoAddTagForExpansion;

        public static bool g_ShowNewInst_DefaultTagSel;

        public static bool ShowOverlappingErrorMessage;

        public static string FileNameLadderSettings;

        public static string SystemTag_MainLoopScanTime;

        public static string SystemTag_PLCMode;

        public static string SystemTag_LadderScanTime;

        public static string SystemTag_ErrorHandle1;

        public static string SystemTag_ErrorHandle2;

        public static int EventHistoryClearAddr;

        public static int IOExpInfoAddr;

        public static char Retentive_Prefix;

        public static string Retentive_MemoryLifeCycle;

        public static string Retentive_MemoryLifeCycle_2;

        public static string Retentive_MemoryLifeCycle_Caption;

        public static int SleepCount;

        public static int SleepMultiplier;

        public static byte[] FontDataBuff;

        public static float ZoomFactorLadder;

        public static float ZoomFactorScreen;

        public static int OriginalWidth;

        public static int OriginalHeight;

        public static string SystemTag_DebugMode;

        public static string SystemTag_BkPoint;

        public static string SystemTag_StepAddress;

        public static string SystemTag_ActiveStep;

        public static long g_CurrentStepAddress;

        public static int g_debugMode;

        public static int g_BkPointRegValue;

        public static string strNode;

        public static string strNode0;

        public static string strCom;

        public static string strRenuDrv;

        public static string strDeviceNodeName;

        public static string strExpansionIPTagAddress;

        public static string strSerialIOIPTagAddress;

        public static string strExpansionIPTagName;

        public static string strExpansionIPRegType;

        public static string strExpansionOPTagAddress;

        public static string strSerialIOOPTagAddress;

        public static string strExpansionOPTagName;

        public static string strExpansionOPRegType;

        public static string strExpansionIPTagBitAddress;

        public static string strSerialIOIPTagBitAddress;

        public static string strExpansionIPTagBitName;

        public static string strExpansionIPBitRegType;

        public static string strExpansionOPTagBitAddress;

        public static string strSerialIOOPTagBitAddress;

        public static string strExpansionOPTagBitName;

        public static string strExpansionOPBitRegType;

        public static string strExpansionIOConfigTagAddress;

        public static string strSerialIOIOConfigTagAddress;

        public static string strExpansionIOConfigTagName;

        public static string strExpansionIOConfigRegType;

        public static string strExpansionIOConfigCoilType;

        public static int TagNameLen_Display;

        public static int Comm_Mode;

        public static bool b_chkhaltmode_dnld;

        public static bool b_chkrunmode_dnld;

        public static bool b_chkcleanmemory_dnld;

        public static bool b_chkPLCMemory_dnld;

        public static bool b_chkApplication_dnld;

        public static bool b_chkLadder_dnld;

        public static bool b_chkData_dnld;

        public static bool StatusErrorDlg;

        public static int PortIndex;

        public static bool SimulationFlag_Native;

        public static bool _flagClose;

        public static bool _simulationErrorFlag;

        public static int FixedGridWidth;

        public static int FixedGridHeight;

        public static ArrayList StructureInfo;

        public static ArrayList loggeddata_Filelist;

        public static ArrayList loggeddata_StartDatelist;

        public static ArrayList loggeddata_Enddatelist;

        public static ArrayList loggeddata_StarTimelist;

        public static ArrayList loggeddata_EndTimelist;

        public static string Path;

        public static int projectID;

        public string _singlefile = "";

        public static ArrayList objListDataMonitorData;

        //public static CommonConstants.PlcModuleHeaderInfo commonHeaderInfo;

        public static ArrayList commonListModuleInfo;

        public static int ProjectReadVersion;

        public static int ImportProjectReadVersion;

        public static bool ImportScreenFlag;

        public static bool ImportScreenXmlRoutine;

        public static bool ImportScreenIsProductVertical;

        public static byte ProjectCurrentVersion;

        public static bool IsDownloadMessageChecked;

        public static bool bScreenSnapToGrid;

        public static bool LADDER_PRESENT;

        public static bool EndorRetInstError;

        public static string ProjectName;

        public static bool IsHeizomatClient;

        public static bool IsAllBodySoltnClient;

        public static bool HidePowerOnMsgForFP3557;

        public static bool FPSCDriver;

        public static bool DigiHomeDriver;

        public static bool Tox_Changes;

        public static bool FP4020MR_L0808R_S3;

        public static bool FP4030MT_L0808RP_A0201_S0;

        public static bool FP4030MR_0808R_A0400_S0;

        public static bool ShowAllSpecialProducts;

        public static bool Show_5121N_S0;

        public static bool Show_5070T_E_S2;

        public static bool Show_PRIZM_710_S0;

        //public static CommonConstants.ProductData _commconstantDestProductData;

        //public static CommonConstants.ProductData _commconstantSourceProductData;

        //public static CommonConstants.ModelDataInfo _commconstantDestModelData;

        //public static CommonConstants.ResolutionValues _commonConstantsResValues;

        //public static CommonConstants.PortValues _commonConstantsPortValues;

        //public static CommonConstants.KeyValues _commonConstantsKeyValues;

        //public static CommonConstants.ColorValues _commonConstantsColorValues;

        public static ArrayList _commonConstantsCom1DestinationModels;

        public static ArrayList _commonConstantsCom2DestinationModels;

        public static string _commonConstantsSrcProjName;

        public static string _commonConstantsDstProjName;

        public static bool _commonConstantblIsColorConvert;

        public static bool _commonConstantblIsProductConvert;

        public static bool _commonConstantblIsFHWTConvert;

        public static string _commonConstantsApplicationDirPath;

        public static string _commonConstantstrSaveAsFileName;

        public static bool ProjectConversion;

        public static string ProjectConversionName;

        //public static CommonConstants.ProductData OldProductDataInfo;

        public static bool g_Build_IEC_Ladder;

        public static bool g_Support_IEC_Ladder;

        public static bool g_IEC_Simulation;

        public static bool g_IEC_OnLine;

        public static bool g_SelectVar;

        public static uint g_hDBClient;

        public static uint g_hDBProject;

        public static uint g_hMWClient;

        public static uint g_hMWProject;

        public static string LadderProjectFolder;

        public static ArrayList objListSlotAddressXW;

        public static ArrayList objListSlotAddressYW;

        public static string SerialPortName;

        public static bool g_AfteSelectFinished;

        public static bool ModelConversion;

        public static bool g_InsertMode;

        public static int MonitoringPort;

        public static string strIPAddress;

        public static int _ethernetMonitoringPort;

        public static string ComNoOrIpAddressDownload;

        public static int ethernetPortNumber;

        public static int responseTimeOutDownload;

        public static int template1Series;

        public static int template2Product;

        public static int template3Mode;

        public static int template4Language;

        public static int template5Orientation;

        public static uint g_hobjData;

        public static uint g_hobjData2;

        public static uint g_hobjData3;

        public static uint g_hobjMode;

        public static uint g_hobjModeW;

        public static bool g_Status1;

        public static bool g_Status2;

        public static bool g_DM_Online;

        public static bool g_LadderModified;

        public static bool g_UploadForOnLine;

        public static bool g_DownloadForOnLine;

        public static uint g_hMWClientDW;

        public static uint g_hMWProjectDW;

        public static bool g_Logix;

        public static string g_StrValue;

        public static string g_StrName;

        public static string g_MWStatus;

        public static bool g_Save_Project;

        public static bool g_MDIClose;

        public static int g_Color_BOOL;

        public static int g_Color_BYTE;

        public static int g_Color_WORD;

        public static int g_Color_DWORD;

        public static int g_Color_INT;

        public static int g_Color_SINT;

        public static int g_Color_DINT;

        public static int g_Color_USINT;

        public static int g_Color_UDINT;

        public static int g_Color_UINT;

        public static int g_Color_Time;

        public static int g_Color_REAL;

        public static bool g_OffDM_Simulation;

        public static bool g_ForceIO;

        public static ArrayList objListForceIO;

        public static int g_Print_CellW_LD;

        public static int g_Print_CellW_FBD;

        public static int g_Print_CellW_SFC;

        public static string g_FindString;

        public static double g_ScanTime;

        public static string g_ProjectPath;

        public static bool DemoVersion;

        public static string RegistryEntryPath;

        public static bool FirmChkCheck;

        public static string ETH_settingFileName;

        public static string XMLTemplateFolder;

        public static string ProjectTemplateFolder;

        public static string ProjectTemplateIECFolder;

        public static string ProjectTemplateNativeFolder;

        public static string ProjectBackupFolder;

        public static bool ProjectTemplateFlag;

        public static bool ProjectTemplateCreateNewFlag;

        public static string ProjectTemplateTempFolder;

        public static string WriteExcepFile;

        public static string ReadExcepFile;

        public static string DwnlExcepFile;

        public static string UpldExcepFile;

        public static string OperExcepFile;

        public static bool IsXMLFileSaveRoutine;

        public static float DpiX;

        public static float DpiY;

        public static string HeaderXMLFile;

        public static string NonDownloadbleXMLFile;

        public static string TaskXMLFile;

        public static string KeysXMLFile;

        public static string screenHeader;

        public static string objectHeader;

        public static string XProjectData;

        public static string DataLoggerXMLFile;

        public static string ObjectAnimationXMLFile;

        public static string NodeDBXMLFile;

        public static string TagDBXMLFile;

        public static string objectTaskFile;

        public static string objectXMLFileExtension;

        public static string AlarmInformationXML;

        public static string LangDBXMLFile;

        public static string USDBXMLFile;

        public static string ModbusMapXML;

        public static string ExpansionXML;

        public static string UseAsDefaultBIN;

        public static string WebScreenXML;

        public static string AccessLvluserXML;

        public static string DMonitoringXML;

        public static string ConversionLogXML;

        public static string EmailInformationXML;

        public static string EmailContactGroupInformationXML;

        public static string TagGroupXML;

        public static string DefaultTagGroup;

        public static string FTPXMLFile;

        public static string GWYBlockXML;

        public static bool IsProjectClosing;

        public static bool TagDBDirtyFlag;

        public static int SelectedProjectID;

        public static int g_Conversion_Def_RegTag_Id;

        public static string g_Conversion_Def_RegTag_Addr;

        public static string g_Conversion_Def_RegTag_Name;

        public static int g_Conversion_Def_CoilTag_Id;

        public static string g_Conversion_Def_CoilTag_Addr;

        public static string g_Conversion_Def_CoilTag_Name;

        public static ArrayList List_DelTagInfo;

        public static bool g_Project_Conversion;

        public static List<int> lstCopyTaskIDs;

        public static string IECBackUpFolder;

        public static ArrayList List_DefBlockNames;

        public static int g_Grid_Max_Rows;

        public static string ProjectExtension;

        public static string ProjectExtensionFilter;

        public static string ProjectConversionPath;

        public static string _stDatatype;

        public static string ProjectIconFile;

        public static string IOExpansionFile;

        public static string ProjectFontBinFile;

        public static string ProjectUnitXmlFile;

        public static string ProjectUnitLPCFile;

        public static string ProjectSaveFolder;

        public static string DEFAULT_NODETAG_FILE;

        public static byte SERIAL_LADDER_DNLD_FILEID;

        public static byte SERIAL_FONT_DNLD_FILEID;

        public static byte SERIAL_FIRMWARE_DNLD_FILEID;

        public static byte SERIAL_APPLICATION_DNLD_FILEID;

        public static byte SERIAL_EXP_DNLD_FILEID;

        public static byte SERIAL_ANALOGEXP_DNLD_FILEID;

        public static byte ETHER_APP_LADD_DNLD_FILEID;

        public static byte ETHER_LADD_FIRM_DNLD_FILEID;

        public static byte ETHER_LADDER_DNLD_FILEID;

        public static byte ETHER_FONT_DNLD_FILEID;

        public static byte ETHER_APPLICATION_DNLD_FILEID;

        public static byte ETHER_FIRMWARE_LADD_DNLD_FILEID;

        public static byte ETHER_LOGG_UPLD_FILEID;

        public static byte ETHER_ETHERNET_SETTINGS_DNLD_FILEID;

        public static byte ETHER_APP_UPLD_FILEID;

        public static byte PLC_STATUS_FILEID;

        public static byte ETHER_LOGG_UPLD_FLASH_FILEID;

        public static byte ETHER_HISTORICAL_ALARM_UPLD_FILEID;

        public static byte LADDER_UPLD_FILEID;

        public static int ETHERNET_PORT_NUMBER;

        public static int SHAPE_TRACKER_SIZE;

        public static byte SERIAL_APPLICATION_UPLD_FILEID;

        public static byte SERIAL_LOGGED_UPLD_FILEID;

        public static byte SERIAL_HISTALARM_UPLD_FILEID;

        public static string TEMP_DOWNLOAD_FILENAME;

        public static string TEMP_UPLOAD_FILENAME;

        public static string UPLOAD_LADDER_FILENAME;

        public static int BAUDRATE;

        public static byte PARITY;

        public static byte BITESIZE;

        public static byte STOPBIT;

        public static int ON_LINE_COMMUNICATION;

        public static int SERIAL_PORT;

        public static ushort DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT230;

        public static ushort DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT230;

        public static ushort DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT230;

        public static ushort DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT230;

        public static ushort DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT230;

        public static ushort DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT230;

        public static ushort DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT_4030MT;

        public static ushort DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT_4030MT;

        public static ushort DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT_4030MT;

        public static ushort DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT_4030MT;

        public static DataSet dsRecentProjectList;

        public static DataSet dsRecentProjectsData;

        public static string _lastCloseProjectPath;

        //public static CommonConstants.TagSelectionFilters stTagSelFilters;

        public static string CURRENTCULTURE;

        public static byte[] DEFAULTPOWERONTASKBYTES;

        //public static DrawingOperations DRAWOPERATION;

        //public static DrawingObjects DRAWOBJECT;

        public static int DRAWx1;

        public static int DRAWx2;

        public static int DRAWy1;

        public static int DRAWy2;

        public static int _chBoxCheck;

        public static bool _ethernetCheck;

        public static bool _isSoftwareOILDS;

        public static bool _checkPass;

        public static bool _pictureShowHide;

        public static bool stGroupType_editPara;

        public static ArrayList varList1;

        public static bool _asciiDT;

        public static bool _isSoftwareFlexiSoft;

        public static bool _isSoftwareMaple;

        public static bool _checkProtocol;

        public static ArrayList tagHistroy;

        public static string _softwareName;

        public static bool _isSoftwareGTXLSoft;

        public static bool _isSoftwareHitachi;

        public static ArrayList _isTagsSelected;

        public static bool _totalNoNodes;

        public static bool _importInternelTagsCom1Com2;

        public static bool _importModbusSlaveTagsCom1Com2;

        public static int _totalNoNodesInCSV;

        public static int _noOfLoopCount;

        public static bool _isNextClick;

        public static bool _isOKClick;

        public static bool _scrCom1;

        public static bool _scrCom2;

        public static bool _isMasterNodeAdded;

        public static bool _isModbusSlave_Ev3defined;

        public static bool _isCancelClick;

        public static bool _isSlaveSelected;

        public static bool _TagSelectionGUIFromDataLogger;

        public static bool _isNodeModbusMasterEdited;

        public static bool _tagUsage;

        public static bool _ShowImportTags;

        public static bool _ShowImportTagsOK;

        public static int _totalNoTagsInCSV;

        public static int _totalNoTagsCount;

        public static ArrayList _totalTagList;

        public static ArrayList _importTagList;

        public static string CONFIGFILENAME;

        public static int MINIMUMSCREENBYTES;

        public static string strProtocolFileName;

        public static string strPortInfoFileName;

        public static int UndoActionCount;

        public static volatile int SelectedObjectCount;

        //private static CommonConstants.ProductData _commconstantProductData;

        public static int HorizontalGrid;

        public static int VerticalGrid;

        public static int GridStyle;

        public static int iActiveScreenNumber;

        public static int iActiveDragDropScreenNumber;

        private static float _commconstiZoomFactor;

        public static bool ShowHideShowValueOn;

        public static bool ShowHideWithinRange;

        public static int ShowHideFromRange;

        public static int ShowHideToRange;

        public static byte TotalNoOfObject;

        public static byte DefaultObjectBackColor;

        public static byte DefaultObjectThickness;

        public static byte btPrizm4Version;

        public static Point DefaultObjectTopLeft;

        public static Point DefaultObjectBottomRight;

        public static byte DefaultObjectNumber;

        public static byte DefaultObjectType;

        public static byte DefaultObjectZLevel;

        public static byte DefaultObjectBorder;

        public static ushort DefaultEv3ProjectHSCounter;

        public static ushort DefaultEv3ProjectHSTimmer;

        public static ushort DefaultEv3ProjectPID;

        public static ushort DefaultEv3ProjectChannel;

        public static ushort DefaultEv3ProjectASCIIComm;

        public static short DefaultEv3ProjectPOnTaskListSize;

        public static byte ColorIndex;

        public static byte PatternIndex;

        public static bool IsOverlappingChecked;

        public static string FeedBackTagValue;

        public static string strImportAlarmLogErrFile;

        public static string strImportAlarmSeverityErr;

        public static string strImportAlarmIDErr;

        public static string strImportAlarmErrorsFound;

        public static string strImportAlarmTextErr;

        public static bool blFileErrMsgFlag;

        public static bool blwaitdlgFlag;

        public static ArrayList BitMapAddressDataTT;

        public static ArrayList BitMapObjectAddressTT;

        public static ArrayList BitMapAddressDataCoil;

        public static ArrayList BitMapObjectAddressCoil;

        public static int[] WindowsUnsignedFontsUsedInApplication;

        public static int[] WindowsHexFontsUsedInApplication;

        public static int[] WindowsASCIIFontsUsedInApplication;

        public static int CurrenmtScreenStartPosition;

        public static string strFontName;

        public static int fontIndex;

        public static Hashtable rdeHtBitMapAddressDataTT;

        public static Hashtable rdeASCIIHtBitMapAddressDataTT;

        public static Hashtable rdeHEXHtBitMapAddressDataTT;

        public static bool blFiveBySeven;

        public static bool blSevenByFourteen;

        public static bool blTenByFourteen;

        public static string NextAlarm;

        public static string PrevAlarm;

        public static string AckAlarm;

        public static string AckAllAlarms;

        public static string AcknowledgeAlarm;

        public static string AcknowledgeAllAlarms;

        public static string WriteValueToTag;

        public static string AddAConstantValueToATag;

        public static string SubtractAConstantValueFromATag;

        public static string AddTagBToTagA;

        public static string SubtractTagBFromTagA;

        public static string CopyTagBToTagA;

        public static string SwapTagAAndTagB;

        public static string CopyTagToSTR;

        public static string CopyTagToLED;

        public static string CopyPrizmToPLC;

        public static string CopyPLCToPrizm;

        public static string CopyRTCToPLC;

        public static string ExecutePLCLogicBlock;

        public static string USBDataLogUpload;

        public static string USBHostUpload;

        public static string SDCardUpload;

        public static string StrAlarm;

        public static string StrArc;

        public static string StrBitbutton;

        public static string StrBitlamp;

        public static string StrBitmap;

        public static string StrDataEntryCoil;

        public static string StrDataEnteryRegister;

        public static string StrDate;

        public static string StrDisplayDataCoil;

        public static string StrDisplayDataRegister;

        public static string StrDisplayDataText;

        public static string StrEllipse;

        public static string StrGoto;

        public static string StrGroup;

        public static string StrHistoricalTrends;

        public static string StrHoldon;

        public static string StrHoldoff;

        public static string StrLine;

        public static string StrMultibargraph;

        public static string StrNext;

        public static string StrNumericalKeypad;

        public static string StrPie;

        public static string StrPolygon;

        public static string StrPolyLine;

        public static string StrPrev;

        public static string StrPopup;

        public static string StrRectangle;

        public static string StrRoundRectangle;

        public static string StrReset;

        public static string StrSet;

        public static string StrMomentary;

        public static string StrSingleBargraph;

        public static string StrTextobject;

        public static string StrTime;

        public static string StrTextwizard;

        public static string StrToggle;

        public static string StrTrend;

        public static string StrWordbutton;

        public static string StrWordlamp;

        public static string StrAnalogmeter;

        public static string StrWriteValueToTag;

        public static string StrAddValueToTag;

        public static string StrSubTractValueToTag;

        public static string StrAddTags;

        public static string StrSubTags;

        public static string StrPrintData;

        public static string StrNumkeypad;

        public static string StrAsciikeypad;

        public static string StrCustomkeypad;

        public static string StrShape;

        public static string StrXYPlot;

        public static short DEFAULT_POPUP_SCREEN_TOPLEFT_X;

        public static short DEFAULT_POPUP_SCREEN_TOPLEFT_Y;

        public static ushort DEFAULT_POPUP_SCREEN_HEIGHT;

        public static ushort DEFAULT_POPUP_SCREEN_WIDTH;

        public static ushort DEFAULT_SCREEN_SCRATCHPAD_AREA;

        public static short MaximumFormSize;

        public static ushort DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT290;

        public static ushort DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT290;

        public static ushort DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT290;

        public static ushort DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT290;

        public static ushort DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT290;

        public static ushort DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT290;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_PRODUCT290;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENWIDTH_PRODUCT290;

        public static ushort DEFAULT_NUMKEYPAD_SCREENHEIGHT_ALLPRODUCTS;

        public static ushort DEFAULT_NUMKEYPAD_SCREENWIDTH_ALLPRODUCTS;

        public static ushort DEFAULT_HEXKEYPAD_SCREENHEIGHT_ALLPRODUCTS;

        public static ushort DEFAULT_HEXKEYPAD_SCREENWIDTH_ALLPRODUCTS;

        public static ushort DEFAULT_BITKEYPAD_SCREENHEIGHT_ALLPRODUCTS;

        public static ushort DEFAULT_BITKEYPAD_SCREENWIDTH_ALLPRODUCTS;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_ALLPRODUCTS;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENWIDTH_ALLPRODUCTS;

        public static ushort DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT5XXX;

        public static ushort DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT5XXX;

        public static ushort DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT5XXX;

        public static ushort DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT5XXX;

        public static ushort DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT5XXX;

        public static ushort DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT5XXX;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_PRODUCT5XXX;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENWIDTH_PRODUCT5XXX;

        public static ushort DEFAULT_POPUP_SCREEN_HEIGHT_PRODUCT5XXX;

        public static ushort DEFAULT_POPUP_SCREEN_WIDTH_PRODUCT5XXX;

        public static ushort DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT5X43;

        public static ushort DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT5X43;

        public static ushort DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT5X43;

        public static ushort DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT5X43;

        public static ushort DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT5X43;

        public static ushort DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT5X43;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_PRODUCT5X43;

        public static ushort DEFAULT_ASCIIKEYPAD_SCREENWIDTH_PRODUCT5X43;

        public static ushort DEFAULT_POPUP_SCREEN_HEIGHT_PRODUCT5X43;

        public static ushort DEFAULT_POPUP_SCREEN_WIDTH_PRODUCT5X43;

        public static int m_gBlockType;

        public static int selIECBlockType;

        public static string DOWNLOAD_FONT_FILENAME;

        public static string DOWNLOAD_LADDER_FILENAME;

        public static string DOWNLOAD_FIRMWARE_FILENAME;

        public static string DOWNLOAD_FHWT_FILENAME;

        public static string DefaultBlockName;

        public static ArrayList BitMapAddressData;

        public static ArrayList BitMapObjectAddress;

        public static int MaximumIntensityOfRed;

        public static int MaximumIntensityOfGreen;

        public static int MaximumIntensityOfBlue;

        public static int DEFAULT_COLOR_SUPPORT;

        public static int communicationStatus;

        public static bool downloadSucess;

        public static ushort DefaultEv3ProjectLanguageID;

        public static int[,] NumberOfKeysInRowAndColumn;

        public static string[,] KeysTitle;

        public static string[,] AsciiKeysTitle;

        //public static KeyStyle[,] KeysDefaultStyles;

        public static string[] ListofLogMod;

        public static string[] ListofLogModFL100;

        public static string[] ListofLogModDataType;

        public static string[] ListofLogModDataType_New;

        private static DataSet dsPLCInformation;

        private static DataSet dsReadPLCSupportedModelList_Native;

        private static DataSet dsReadPLCSupportedModelList_IEC;

        public static byte WORDWIZARD_MAX_STATES;

        public static byte WORDWIZARD_MIN_STATES;

        public static int HorizontalScreenScrollBarValue;

        public static int VerticalScreenScrollBarValue;

        public static ushort usColorAnimationMinValue;

        public static ushort usColorAnimationMaxValue;

        public static string[] AlarmTimeFormats;

        public static string[] AlarmYNFormat;

        public static string AlarmDateFormat;

        public static string AlarmTextStringDisplay;

        public static string AlarmAlarmNumberDisplay;

        public static string AlarmOtherDisplay;

        public static bool PasswordScreenDisplay;

        //public static List<CommonConstants.LanguageInformation> LanguageIdList;

        public static ushort MaxLimitOfColorAnimation;

        public static string IBMCommunication;

        public static int iKeyPadTouchGridWidth;

        public static int iKeyPadTouchGridHeight;

        public static bool blSimulation;

        public static bool blSaveForSimulation;

        public static bool blLanguageChanged;

        public static string TTFontNameUsedForPrizmFont;

        public static string strImportTagStartCharacter;

        public static string strImportNodeStartCharacter;

        public static string strImportTagsFileStartCharacter;

        public static string strImportTagsCSV;

        public static string strImportTagsCSVSeperator;

        public static string strImportTagsVersion;

        public static string strImportTagsDate;

        public static string strImportTagsTotalTags;

        public static string strNewLineCharacter;

        public static string strImportNodeNamePresent;

        public static string strImportNodeAddAutoGenerated;

        public static string strImportNodeInvalidProtocol;

        public static string strImportNodeInvalidModel;

        public static string strImportNodeProtocolDefinedOnPort;

        public static string IBMComm;

        public static string G9SP_ProtocolName;

        public static string PrizmUnit;

        public static string Com1Com2;

        public static string strImportNodePrizmUnitDefaultNode;

        public static string strImportTagErrWrongTagHeader;

        public static string strImportTagErrInvalidTagColumnCount;

        public static string strImportTagErrInvalidTagInformation;

        public static string strImportTagErrDuplicateTagAddress;

        public static string strImportTagErrDuplicateTagName;

        public static string strImportTagErrWrongTagAddress;

        public static string strImportTagErrWrongTagName;

        public static string strImportTagErrWrongTagType;

        public static string strImportTagErrWrongNoofBytes;

        public static string strImportTagErrWrongPrefix;

        public static string strImportTagErrWrongNodeName;

        public static string strImportTagErrWrongPortName;

        public static string strImportTagErrTagLimitReached;

        public static string strImportTagErrTagAddedWithAutoGeneratedTagName;

        public static string strImportTagErrTagCannotReplace;

        public static string strImportTagErrTagReplaced;

        public static string strExportLogFileName;

        public static string strStringNotSupport;

        public static string strTagMappingError;

        public static string strProjectType;

        public static List<string> lstNodeColInformation;

        public static List<string> lstTagColInformation;

        public static List<string> lstTagColInformation_Native;

        public static string strGroupName_Global;

        public static string strGroupName_Retain;

        public static string strImportTagErrDupNativeTagAddress;

        public static string strImportTagErrDuplicateNativeAddress;

        public static string strStatustagname;

        public static string ApplicationPath;

        public static string strUserName;

        public static string strPassword;

        public static string strConfirmPW;

        public static string strHeaderText;

        public static bool bEnableWebserver;

        public static bool bEnableWebserverHeader;

        public static bool bEnableWebserverNavigation;

        public static bool bEnableWebserverBorder;

        public static int WEB_SCREEN_WIDTH;

        public static int WEB_SCREEN_HEIGHT;

        public static ushort DefaultWebscreenNumber;

        public static Hashtable _sequencialScreenMap;

        public static Hashtable _noOfTagsPerScreen;

        public static Hashtable _noOfTagsPerXML;

        public static int _totalImageCnt;

        public static int _totalXMLCnt;

        public static int _htmlWebscreenCnt;

        public static int _selectedTab;

        public static string strDataRegister;

        public static string strRetentiveRegister;

        public static string strSystemRegister;

        public static string strInternalRegister;

        public static string strInputRegister;

        public static string strOutputRegister;

        public static string strTimmerRegister;

        public static string strCounterRegister;

        //public static ClassList.Debug debugObject;

        public static string strLangSequenceErrMsg;

        public static string strIECTaskName;

        public static string strIECAlarmDataLogger;

        public static string[] strRenuUDFB;

        public static string[] arrDigits;

        public static string[] arrSpecialCharacters;

        public static string[] arrStrings;

        public static string strAlarmParamText;

        public static int iAlarmRectHeight;

        public static System.Drawing.Font fAlarmParam;

        public static StringFormat objAlarmStringFormat;

        public static byte bSelShapeFont;

        public static bool ScreenNumberMsg;

        public static bool FlashAnimLowRangeMsg;

        public static bool FlashAnimHighRangeMsg;

        public static bool ColorAnimLinColorMsg;

        public static bool ColorAnimFillColorMsg;

        public static bool ShowHideAnimLowRangeMsg;

        public static bool ShowHideAnimHighRangeMsg;

        public static bool SingleBarGraphMaximumRangeMsg;

        public static bool SingleBarGraphMinimumRangeMsg;

        public static bool MultiBarGraphMsg;

        public static bool MultiBarGraphMinimumRangeMsg;

        public static bool RepeatMsg;

        public static string NoImage;

        public static string NoImageDotBmp;

        public static string OnlyDotBmp;

        public static string OnlyDotBMP;

        public static string OnlyDotbmp;

        public static string OnlyDotBmP;

        public static string OnlyDotBMp;

        public static string OnlyDotbMP;

        public static string OnlyDotbmP;

        public static string OnlyDotbMp;

        public static string UnderscoreTBmp;

        public static string UnderscoreT;

        public static string UnderscoreTPzp;

        public static string OnlyDotPzp;

        public static string UnderscoreTNoImage;

        public static string PictureFolder;

        public static string Slash;

        public static string strAbbrevations;

        public static string strAlarmType;

        public static string strAlmType1;

        public static string strAlmType2;

        public static string strAlmType3;

        public static string strAlmType4;

        public static string strAlmType5;

        public static string strAlmType6;

        public static string strAlmType7;

        public static string strAlmType8;

        public static string strAlmType9;

        public static string strAlmType10;

        public static string strAlmMsg;

        public static string strAlmMsg1;

        public static string strAlmMsg2;

        public static string strAlmMsg3;

        public static string strHist;

        public static string strHist1;

        public static string strHist2;

        public static string strHist3;

        public static string strIsAlmAssign;

        public static string strAlmType;

        public static string strAlm;

        public static string strErr;

        public static string strWarning;

        public static string strFileExtErr;

        public static string strFileOpenErr;

        public static string strAlmTxtLen;

        public static string strTagBitPresent;

        public static string strBitNum;

        public static string strTagGrp;

        public static string strIsAlmAssignMsg;

        public static string strLogMsg;

        public static string strAlmSeverityMsg1;

        public static string strAlmSeverityMsg2;

        public static string strAlmPrintMsg;

        public static string strAlmColBlankHeadingMsg;

        public static string strAlmCondOperatorMsg;

        public static string strAlmIDPresent;

        public static string strAlmTypeMsg;

        public static string strAlmTypeMsg1;

        public static string strAlmAckMsg;

        public static string strAlmActionMsg;

        public static string strAlmColHeadingMsg;

        public static string strLangErrMsg;

        public static string strAlmTypeTxt;

        public static string strTagPresent;

        public static string strTagMsg;

        public static string strExpFileMsg;

        public static string strFileErrFormat;

        public static string strFileErr;

        public static string strNull;

        public static string strTitle;

        public static string strGlInfo;

        public static string strAlmScanTime;

        public static string strAlmActionTxt;

        public static string strAlmErrMsg;

        public static string strAutoAck;

        public static string strAlmProp;

        public static string SequenceIDs;

        public static bool RunTime_LineProperty;

        public static PropertySort SortType;

        public static char dblCote;

        public static int AlarmFontIndex;

        public static string ScrNumber;

        public static string Obj_ImgText;

        public static string Co_ordinate;

        public static string Task_Name;

        public static int actualDeletedScrNo;

        private static int _commConstantPrizmVersion;

        private static ArrayList _commconstantarrFormObject;

        private static ArrayList _constarrPrizmPatternValues;

        public static ArrayList _constarrBaudRates;

        private static bool _commConstantblRecalBlockSize;

        private static byte _commConstantModbusSlavePlcCode;

        private static byte _commConstantPrizmPlcCode;

        public static string _CommConstantsProjHMILadderType;

        public static string strImportLogFileName;

        private bool is64BitProcess = IntPtr.Size == 8;

        public static string _NewProjectFolderName;

        public static int _NewProjectnumber;

        public static string _ProjectFolderName;

        public static string _PreviousFolderName;

        public static string _TEMPPreviousFolderName;

        public static string _IECFoldername;

        public static byte[,] Col16Supported;

        public static byte[,] Col256Supported;

        public static byte[,] Col2Supported;

        //	public static string ApplicationDirectoryPath
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsApplicationDirPath;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsApplicationDirPath = value;
        //		}
        //	}

        //	public static CommonConstants.ColorValues ColorValuesObject
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsColorValues;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsColorValues = value;
        //		}
        //	}

        //	public static string DestinationProjectName
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsDstProjName;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsDstProjName = value;
        //		}
        //	}

        //public static CommonConstants.ModelDataInfo DestModelDataInfo
        //{
        //    get
        //    {
        //        return CommonConstants._commconstantDestModelData;
        //    }
        //    set
        //    {
        //        CommonConstants._commconstantDestModelData = value;
        //    }
        //}

        //public static CommonConstants.ProductData DestProductDataInfo
        //{
        //    get
        //    {
        //        return CommonConstants._commconstantDestProductData;
        //    }
        //    set
        //    {
        //        CommonConstants._commconstantDestProductData = value;
        //    }
        //}

        //	public static bool IsApplicationConversion
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantblIsProductConvert;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantblIsProductConvert = value;
        //		}
        //	}

        //	public static bool IsColorConversion
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantblIsColorConvert;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantblIsColorConvert = value;
        //		}
        //	}

        //	public static bool IsFHWTForConversion
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantblIsFHWTConvert;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantblIsFHWTConvert = value;
        //		}
        //	}

        //	public static CommonConstants.KeyValues KeyValuesObject
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsKeyValues;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsKeyValues = value;
        //		}
        //	}

        //	public static byte ModbusSlavePlcCode
        //	{
        //		get
        //		{
        //			return CommonConstants._commConstantModbusSlavePlcCode;
        //		}
        //	}

        //	public static string ModelSaveAsFileName
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantstrSaveAsFileName;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantstrSaveAsFileName = value;
        //		}
        //	}

        //	public static DataSet PLCInformation
        //	{
        //		get
        //		{
        //			return CommonConstants.dsPLCInformation;
        //		}
        //		set
        //		{
        //			CommonConstants.dsPLCInformation = value;
        //		}
        //	}

        //	public static DataSet PLCInformationFromXMLIEC
        //	{
        //		get
        //		{
        //			return CommonConstants.dsReadPLCSupportedModelList_IEC;
        //		}
        //		set
        //		{
        //			CommonConstants.dsReadPLCSupportedModelList_IEC = value;
        //		}
        //	}

        //	public static DataSet PLCInformationFromXMLNative
        //	{
        //		get
        //		{
        //			return CommonConstants.dsReadPLCSupportedModelList_Native;
        //		}
        //		set
        //		{
        //			CommonConstants.dsReadPLCSupportedModelList_Native = value;
        //		}
        //	}

        //	public static CommonConstants.PortValues PortValuesObject
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsPortValues;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsPortValues = value;
        //		}
        //	}

        //	public static byte PrizmPlcCode
        //	{
        //		get
        //		{
        //			return CommonConstants._commConstantPrizmPlcCode;
        //		}
        //	}

        //	public static CommonConstants.ProductData ProductDataInfo
        //	{
        //		get
        //		{
        //			return CommonConstants._commconstantProductData;
        //		}
        //		set
        //		{
        //			CommonConstants._commconstantProductData = value;
        //		}
        //	}

        //	public static string ProjectHMILadderType
        //	{
        //		get
        //		{
        //			return CommonConstants._CommConstantsProjHMILadderType;
        //		}
        //		set
        //		{
        //			CommonConstants._CommConstantsProjHMILadderType = value;
        //		}
        //	}

        //	public static int ProjectPrizmVersion
        //	{
        //		get
        //		{
        //			return CommonConstants._commConstantPrizmVersion;
        //		}
        //		set
        //		{
        //			CommonConstants._commConstantPrizmVersion = value;
        //		}
        //	}

        //	public static bool RecalculateBlockSize
        //	{
        //		get
        //		{
        //			return CommonConstants._commConstantblRecalBlockSize;
        //		}
        //		set
        //		{
        //			CommonConstants._commConstantblRecalBlockSize = value;
        //		}
        //	}

        //	public static CommonConstants.ResolutionValues ResolutionValuesObject
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsResValues;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsResValues = value;
        //		}
        //	}

        //	public static CommonConstants.ProductData SourceProductDataInfo
        //	{
        //		get
        //		{
        //			return CommonConstants._commconstantSourceProductData;
        //		}
        //		set
        //		{
        //			CommonConstants._commconstantSourceProductData = value;
        //		}
        //	}

        //	public static string SourceProjectName
        //	{
        //		get
        //		{
        //			return CommonConstants._commonConstantsSrcProjName;
        //		}
        //		set
        //		{
        //			CommonConstants._commonConstantsSrcProjName = value;
        //		}
        //	}

        //	public static float ZoomFactor
        //	{
        //		get
        //		{
        //			return CommonConstants._commconstiZoomFactor / 100f;
        //		}
        //		set
        //		{
        //			CommonConstants._commconstiZoomFactor = value;
        //		}
        //	}

        static CommonConstants()
        {
            //		CommonConstants.ScrollPosLeft = 0;
            //		CommonConstants.ScrollPosTop = 0;
            //		CommonConstants.projectFolderPathWithoutExtension = "";
            //		CommonConstants._bitmapFileFolderPathOldProj = "";
            //		CommonConstants._bitmapFileFolderPathNewProj = "";
            //		CommonConstants._IsEthernetSelected = false;
            //		CommonConstants._IsImageEdited = false;
            //		CommonConstants._IsXYPlotText = false;
            //		CommonConstants._XYSTYLE2 = false;
            //		CommonConstants._XYPlotStyle2NumberofBytes = 0;
            //		CommonConstants._AccLvlHomScrNum = 1;
            //		CommonConstants._AccLvlGoToPwrONScrNum = 1;
            //		CommonConstants._AccLvlGoToPwrONTsk = false;
            //		CommonConstants._AccLvlAutoLogOff = false;
            //		CommonConstants._AccLvlAutoLogOffTime = 600;
            //		CommonConstants._isProductVertical = false;
            //		CommonConstants.InvertedComma = '\"';
            //		CommonConstants.TABstring = "\t";
            //		CommonConstants.ReadingPrizm4File = -1;
            //		CommonConstants.communicationType = 0;
            //		CommonConstants.MaxNoOfObjectOnScreen = 256;
            //		CommonConstants._isCalledForTagUsage = false;
            //		CommonConstants._isScreenTaskUpdatedFromTagUsage = false;
            //		CommonConstants._isScreenTaskIDsTOUpdateOnTagUsage = false;
            //		CommonConstants._DefaultTagMemory = false;
            //		CommonConstants._ExpansionTagExceed = false;
            //		CommonConstants.strtext = "";
            //		CommonConstants.strstring = "";
            //		CommonConstants.strError = "";
            //		CommonConstants.imagechar = "";
            //		CommonConstants.isMoveOutSide = false;
            //		CommonConstants.isGroupCreated = false;
            //		CommonConstants.isProjectSaveAs = false;
            //		CommonConstants.isDownloading = false;
            //		CommonConstants.TagSelDefaultTagName = "";
            //		CommonConstants.DownloadTagNames = true;
            //		CommonConstants.DefaultRegTagAddr = "SW0000";
            //		CommonConstants.DefaultBitTagAddr = "S00011";
            //		CommonConstants.LanguageTagAddr = "SW0001";
            //		CommonConstants.DefaultRegTagId = -1;
            //		CommonConstants.DefaultBitTagId = -1;
            //		CommonConstants.blIsFactoryScreen = false;
            //		CommonConstants.DefaultRegTagName = "DefaultReg";
            //		CommonConstants.DefaultBitTagName = "DefaultCoil";
            //		CommonConstants.PassThruXMLFilename = "PassThroughPort.xml";
            //		CommonConstants.IsHobvisionClientFL100S0 = false;
            //		CommonConstants.ImportTempDefaultRegTagId = -1;
            //		CommonConstants.ImportTempDefaultBitTagId = -1;
            //		CommonConstants.ImportTempDefaultRegTagName = "DefaultReg";
            //		CommonConstants.ImportTempDefaultBitTagName = "DefaultCoil";
            //		CommonConstants.INST_TYPE = InstructionType.NO;
            //		CommonConstants.bLadderScreenRightClick = false;
            //		CommonConstants.LadderSaveFileDataVersion = 7;
            //		CommonConstants.BlockStartHeader = "Start of Block";
            //		CommonConstants.InstructionStartHeader = "Start of Instruction";
            //		CommonConstants.DataMonitorStartHeader = "Start of Data Monitor";
            //		CommonConstants.ActiveLadderMode = LadderEditorMode.MODE_OFFLINE;
            //		CommonConstants.bPlcCommunicationStatus = false;
            //		CommonConstants.enableCalibration = 0;
            //		CommonConstants.LadderSaveFileStartHeader = "$$#*#Start of Ladder4 Data#*#$$";
            //		CommonConstants.UnicodeTagNamesStartHeader = "$$#*#Start of Tag Names Data#*#$$";
            //		CommonConstants.bObjectMoved = false;
            //		CommonConstants.bNewObjectAdded = false;
            //		CommonConstants.bLadderEditing = true;
            //		CommonConstants.bImportBlock = false;
            //		CommonConstants.bUploadFromGUI = false;
            //		CommonConstants.bIsShiftKeyPressed = false;
            //		CommonConstants.objLadderOperandInfo = new CommonConstants.LadderOperandInfo();
            //		CommonConstants.ProductIdentifier = 0;
            //		CommonConstants.UnicodeTagNameRevision = 1;
            //		CommonConstants.strDefaultTagAddress = "XXXX";
            //		CommonConstants.ColorContactOnState = Color.LightGreen;
            //		CommonConstants.ColorContactOffState = Color.Red;
            //		CommonConstants.ColorFunctionBlock = Color.Black;
            //		CommonConstants.ColorOperandVale = Color.Black;
            //		CommonConstants.ColorForceVar = Color.Red;
            //		CommonConstants.ColorLadderBackGroundArea = Color.White;
            //		CommonConstants.ColorActiveRung = Color.FromArgb(0, 255, 255);
            //		CommonConstants.ColorLeftMarginRung1 = Color.Turquoise;
            //		CommonConstants.ColorLeftMarginRung2 = Color.MistyRose;
            //		CommonConstants.ShowRegisterEntryMessage = true;
            //		CommonConstants.AutoAddTagForExpansion = true;
            //		CommonConstants.g_ShowNewInst_DefaultTagSel = false;
            //		CommonConstants.ShowOverlappingErrorMessage = true;
            //		CommonConstants.FileNameLadderSettings = "LadderSettingsV6.dat";
            //		CommonConstants.SystemTag_MainLoopScanTime = "SW0017";
            //		CommonConstants.SystemTag_PLCMode = "MW0000";
            //		CommonConstants.SystemTag_LadderScanTime = "SW0046";
            //		CommonConstants.SystemTag_ErrorHandle1 = "MW0001";
            //		CommonConstants.SystemTag_ErrorHandle2 = "MW0002";
            //		CommonConstants.EventHistoryClearAddr = 15617;
            //		CommonConstants.IOExpInfoAddr = 16128;
            //		CommonConstants.Retentive_Prefix = 'R';
            //		CommonConstants.Retentive_MemoryLifeCycle = "Retentive memory write operation is limited to maximum of 10,00,000 write cycles \nExceeding this limit will cause damage to Retentive memory";
            //		CommonConstants.Retentive_MemoryLifeCycle_2 = "Retentive memory write operation is limited to maximum of 30,000 write cycles \nExceeding this limit will cause damage to Retentive memory";
            //		CommonConstants.Retentive_MemoryLifeCycle_Caption = "Warning";
            //		CommonConstants.SleepCount = 100;
            //		CommonConstants.SleepMultiplier = 50;
            //		CommonConstants.ZoomFactorLadder = 100f;
            //		CommonConstants.ZoomFactorScreen = 200f;
            //		CommonConstants.OriginalWidth = 40;
            //		CommonConstants.OriginalHeight = 40;
            //		CommonConstants.SystemTag_DebugMode = "SW0150";
            //		CommonConstants.SystemTag_BkPoint = "SW0151";
            //		CommonConstants.SystemTag_StepAddress = "SW0152";
            //		CommonConstants.SystemTag_ActiveStep = "SW0170";
            //		CommonConstants.g_CurrentStepAddress = (long)0;
            //		CommonConstants.g_debugMode = 0;
            //		CommonConstants.g_BkPointRegValue = 0;
            //		CommonConstants.strNode = "Node ";
            //		CommonConstants.strNode0 = "Node 0";
            //		CommonConstants.strCom = "Com";
            //		CommonConstants.strRenuDrv = "Renu Electronics";
            //		CommonConstants.strDeviceNodeName = "Operator Panel";
            //		CommonConstants.strExpansionIPTagAddress = "XW0";
            //		CommonConstants.strSerialIOIPTagAddress = "XW";
            //		CommonConstants.strExpansionIPTagName = "InputReg";
            //		CommonConstants.strExpansionIPRegType = "Input Registers";
            //		CommonConstants.strExpansionOPTagAddress = "YW0";
            //		CommonConstants.strSerialIOOPTagAddress = "YW";
            //		CommonConstants.strExpansionOPTagName = "OutputReg";
            //		CommonConstants.strExpansionOPRegType = "Output Registers";
            //		CommonConstants.strExpansionIPTagBitAddress = "X0";
            //		CommonConstants.strSerialIOIPTagBitAddress = "X";
            //		CommonConstants.strExpansionIPTagBitName = "InputCoil";
            //		CommonConstants.strExpansionIPBitRegType = "Input Coils";
            //		CommonConstants.strExpansionOPTagBitAddress = "Y0";
            //		CommonConstants.strSerialIOOPTagBitAddress = "Y";
            //		CommonConstants.strExpansionOPTagBitName = "OutputCoil";
            //		CommonConstants.strExpansionOPBitRegType = "Output Coils";
            //		CommonConstants.strExpansionIOConfigTagAddress = "MW0";
            //		CommonConstants.strSerialIOIOConfigTagAddress = "MW";
            //		CommonConstants.strExpansionIOConfigTagName = "IOConfigReg";
            //		CommonConstants.strExpansionIOConfigRegType = "IO Configuration Registers";
            //		CommonConstants.strExpansionIOConfigCoilType = "IO Configuration Coils";
            //		CommonConstants.TagNameLen_Display = 6;
            //		CommonConstants.Comm_Mode = 2;
            //		CommonConstants.b_chkhaltmode_dnld = true;
            //		CommonConstants.b_chkrunmode_dnld = true;
            //		CommonConstants.b_chkcleanmemory_dnld = false;
            //		CommonConstants.b_chkPLCMemory_dnld = true;
            //		CommonConstants.b_chkApplication_dnld = true;
            //		CommonConstants.b_chkLadder_dnld = true;
            //		CommonConstants.b_chkData_dnld = false;
            //		CommonConstants.StatusErrorDlg = false;
            //		CommonConstants.PortIndex = 0;
            //		CommonConstants.SimulationFlag_Native = false;
            //		CommonConstants._flagClose = false;
            //		CommonConstants._simulationErrorFlag = false;
            //		CommonConstants.FixedGridWidth = 12;
            //		CommonConstants.FixedGridHeight = 16;
            //		CommonConstants.StructureInfo = new ArrayList();
            //		CommonConstants.loggeddata_Filelist = new ArrayList();
            //		CommonConstants.loggeddata_StartDatelist = new ArrayList();
            //		CommonConstants.loggeddata_Enddatelist = new ArrayList();
            //		CommonConstants.loggeddata_StarTimelist = new ArrayList();
            //		CommonConstants.loggeddata_EndTimelist = new ArrayList();
            //		CommonConstants.Path = string.Empty;
            //		CommonConstants.objListDataMonitorData = new ArrayList();
            //		CommonConstants.commonHeaderInfo = new CommonConstants.PlcModuleHeaderInfo();
            //		CommonConstants.commonListModuleInfo = new ArrayList();
            //		CommonConstants.ProjectReadVersion = 0;
            //		CommonConstants.ImportProjectReadVersion = 0;
            //		CommonConstants.ImportScreenFlag = false;
            //		CommonConstants.ImportScreenXmlRoutine = false;
            //		CommonConstants.ImportScreenIsProductVertical = false;
            //		CommonConstants.ProjectCurrentVersion = 107;
            //		CommonConstants.IsDownloadMessageChecked = false;
            //		CommonConstants.bScreenSnapToGrid = false;
            //		CommonConstants.LADDER_PRESENT = false;
            //		CommonConstants.EndorRetInstError = false;
            //		CommonConstants.ProjectName = "";
            //		CommonConstants.IsHeizomatClient = false;
            //		CommonConstants.IsAllBodySoltnClient = false;
            //		CommonConstants.HidePowerOnMsgForFP3557 = false;
            //		CommonConstants.FPSCDriver = false;
            //		CommonConstants.DigiHomeDriver = false;
            //		CommonConstants.Tox_Changes = false;
            //		CommonConstants.FP4020MR_L0808R_S3 = false;
            //		CommonConstants.FP4030MT_L0808RP_A0201_S0 = false;
            //		CommonConstants.FP4030MR_0808R_A0400_S0 = false;
            //		CommonConstants.ShowAllSpecialProducts = false;
            //		CommonConstants.Show_5121N_S0 = false;
            //		CommonConstants.Show_5070T_E_S2 = false;
            //		CommonConstants.Show_PRIZM_710_S0 = false;
            //		CommonConstants._commconstantDestProductData = new CommonConstants.ProductData();
            //		CommonConstants._commconstantSourceProductData = new CommonConstants.ProductData();
            //		CommonConstants._commconstantDestModelData = new CommonConstants.ModelDataInfo();
            //		CommonConstants._commonConstantsResValues = new CommonConstants.ResolutionValues();
            //		CommonConstants._commonConstantsPortValues = new CommonConstants.PortValues();
            //		CommonConstants._commonConstantsKeyValues = new CommonConstants.KeyValues();
            //		CommonConstants._commonConstantsColorValues = new CommonConstants.ColorValues();
            //		CommonConstants._commonConstantsCom1DestinationModels = new ArrayList();
            //		CommonConstants._commonConstantsCom2DestinationModels = new ArrayList();
            //		CommonConstants._commonConstantsSrcProjName = "";
            //		CommonConstants._commonConstantsDstProjName = "";
            //		CommonConstants._commonConstantblIsColorConvert = false;
            //		CommonConstants._commonConstantblIsProductConvert = false;
            //		CommonConstants._commonConstantblIsFHWTConvert = false;
            //		CommonConstants._commonConstantsApplicationDirPath = "";
            //		CommonConstants._commonConstantstrSaveAsFileName = "";
            //		CommonConstants.ProjectConversion = false;
            //		CommonConstants.ProjectConversionName = "";
            //		CommonConstants.g_Build_IEC_Ladder = false;
            //		CommonConstants.g_Support_IEC_Ladder = false;
            //		CommonConstants.g_IEC_Simulation = false;
            //		CommonConstants.g_IEC_OnLine = false;
            //		CommonConstants.g_SelectVar = false;
            //		CommonConstants.g_hDBClient = 0;
            //		CommonConstants.g_hDBProject = 0;
            //		CommonConstants.g_hMWClient = 0;
            //		CommonConstants.g_hMWProject = 0;
            //		CommonConstants.LadderProjectFolder = "LDProject";
            //		CommonConstants.objListSlotAddressXW = new ArrayList();
            //		CommonConstants.objListSlotAddressYW = new ArrayList();
            //		CommonConstants.SerialPortName = "Com1";
            //		CommonConstants.g_AfteSelectFinished = false;
            //		CommonConstants.ModelConversion = false;
            //		CommonConstants.g_InsertMode = true;
            //		CommonConstants.MonitoringPort = 1100;
            //		CommonConstants.strIPAddress = "192.168.0.254";
            //		CommonConstants._ethernetMonitoringPort = 1100;
            //		CommonConstants.ComNoOrIpAddressDownload = "192.168.0.254";
            //		CommonConstants.ethernetPortNumber = 5000;
            //		CommonConstants.responseTimeOutDownload = 40;
            //		CommonConstants.template1Series = 0;
            //		CommonConstants.template2Product = 0;
            //		CommonConstants.template3Mode = 0;
            //		CommonConstants.template4Language = 0;
            //		CommonConstants.template5Orientation = 0;
            //		CommonConstants.g_hobjData = 0;
            //		CommonConstants.g_hobjData2 = 0;
            //		CommonConstants.g_hobjData3 = 0;
            //		CommonConstants.g_hobjMode = 0;
            //		CommonConstants.g_hobjModeW = 0;
            //		CommonConstants.g_Status1 = false;
            //		CommonConstants.g_Status2 = false;
            //		CommonConstants.g_DM_Online = false;
            //		CommonConstants.g_LadderModified = false;
            //		CommonConstants.g_UploadForOnLine = false;
            //		CommonConstants.g_DownloadForOnLine = false;
            //		CommonConstants.g_hMWClientDW = 0;
            //		CommonConstants.g_hMWProjectDW = 0;
            //		CommonConstants.g_Logix = false;
            //		CommonConstants.g_StrValue = "";
            //		CommonConstants.g_StrName = "";
            //		CommonConstants.g_MWStatus = "";
            //		CommonConstants.g_Save_Project = false;
            //		CommonConstants.g_MDIClose = false;
            //		CommonConstants.g_Color_BOOL = Color.Magenta.ToArgb();
            //		CommonConstants.g_Color_BYTE = Color.Red.ToArgb();
            //		CommonConstants.g_Color_WORD = Color.Blue.ToArgb();
            //		CommonConstants.g_Color_DWORD = Color.Violet.ToArgb();
            //		CommonConstants.g_Color_INT = Color.LawnGreen.ToArgb();
            //		CommonConstants.g_Color_SINT = Color.Orange.ToArgb();
            //		CommonConstants.g_Color_DINT = Color.Red.ToArgb();
            //		CommonConstants.g_Color_USINT = Color.DodgerBlue.ToArgb();
            //		CommonConstants.g_Color_UDINT = Color.Black.ToArgb();
            //		CommonConstants.g_Color_UINT = Color.BlueViolet.ToArgb();
            //		CommonConstants.g_Color_Time = Color.DarkGreen.ToArgb();
            //		CommonConstants.g_Color_REAL = Color.Black.ToArgb();
            //		CommonConstants.g_OffDM_Simulation = false;
            //		CommonConstants.g_ForceIO = false;
            //		CommonConstants.objListForceIO = new ArrayList();
            //		CommonConstants.g_Print_CellW_LD = 50;
            //		CommonConstants.g_Print_CellW_FBD = 50;
            //		CommonConstants.g_Print_CellW_SFC = 150;
            //		CommonConstants.g_FindString = "";
            //		CommonConstants.g_ScanTime = 0;
            //		CommonConstants.g_ProjectPath = "";
            //		CommonConstants.DemoVersion = false;
            //		CommonConstants.FirmChkCheck = true;
            //		CommonConstants.ETH_settingFileName = "ETH_SETTING.BIN";
            //		CommonConstants.XMLTemplateFolder = "Template";
            //		CommonConstants.ProjectTemplateFolder = "ProjectTemplates";
            //		CommonConstants.ProjectTemplateIECFolder = "IECApps";
            //		CommonConstants.ProjectTemplateNativeFolder = "NativeApps";
            //		CommonConstants.ProjectBackupFolder = "AppBkp";
            //		CommonConstants.ProjectTemplateFlag = false;
            //		CommonConstants.ProjectTemplateCreateNewFlag = false;
            //		CommonConstants.ProjectTemplateTempFolder = "TempProjectTemplates";
            //		CommonConstants.WriteExcepFile = "WExcp.fpe";
            //		CommonConstants.ReadExcepFile = "RExcp.fpe";
            //		CommonConstants.DwnlExcepFile = "DExcp.fpe";
            //		CommonConstants.UpldExcepFile = "UExcp.fpe";
            //		CommonConstants.OperExcepFile = "OperExcp.fpe";
            //		CommonConstants.IsXMLFileSaveRoutine = false;
            //		CommonConstants.DpiX = 0f;
            //		CommonConstants.DpiY = 0f;
            //		CommonConstants.HeaderXMLFile = "PRJHDRIN.fpx";
            //		CommonConstants.NonDownloadbleXMLFile = "NDWIN.fpx";
            //		CommonConstants.TaskXMLFile = "TSKDB.fpx";
            //		CommonConstants.KeysXMLFile = "KYDB.fpx";
            //		CommonConstants.screenHeader = "SCRHDRIN.fpx";
            //		CommonConstants.objectHeader = "OBHIN.fpx";
            //		CommonConstants.XProjectData = "XProjectData";
            //		CommonConstants.DataLoggerXMLFile = "DLDB.fpx";
            //		CommonConstants.ObjectAnimationXMLFile = "OBAnimIN.fpx";
            //		CommonConstants.NodeDBXMLFile = "NDDB.fpx";
            //		CommonConstants.TagDBXMLFile = "NTDB.fpx";
            //		CommonConstants.objectTaskFile = "OBTSKIN.fpx";
            //		CommonConstants.objectXMLFileExtension = ".obx";
            //		CommonConstants.AlarmInformationXML = "ALDB.fpx";
            //		CommonConstants.LangDBXMLFile = "LGDB.fpx";
            //		CommonConstants.USDBXMLFile = "UNSDIN.fpx";
            //		CommonConstants.ModbusMapXML = "MSMIN.fpx";
            //		CommonConstants.ExpansionXML = "EXDB.fpx";
            //		CommonConstants.UseAsDefaultBIN = "UADef.bin";
            //		CommonConstants.WebScreenXML = "WSIN.fpx";
            //		CommonConstants.AccessLvluserXML = "ACCLU.fpx";
            //		CommonConstants.DMonitoringXML = "DMNTR.fpx";
            //		CommonConstants.ConversionLogXML = "CNLG.FPX";
            //		CommonConstants.EmailInformationXML = "EMDB.fpx";
            //		CommonConstants.EmailContactGroupInformationXML = "EMCG.fpx";
            //		CommonConstants.TagGroupXML = "GTIN.fpx";
            //		CommonConstants.DefaultTagGroup = "None";
            //		CommonConstants.FTPXMLFile = "TFIN.fpx";
            //		CommonConstants.GWYBlockXML = "GWYBLK.FPX";
            //		CommonConstants.IsProjectClosing = false;
            //		CommonConstants.TagDBDirtyFlag = false;
            //		CommonConstants.SelectedProjectID = 0;
            //		CommonConstants.g_Conversion_Def_RegTag_Id = 0;
            //		CommonConstants.g_Conversion_Def_RegTag_Addr = "";
            //		CommonConstants.g_Conversion_Def_RegTag_Name = "";
            //		CommonConstants.g_Conversion_Def_CoilTag_Id = 0;
            //		CommonConstants.g_Conversion_Def_CoilTag_Addr = "";
            //		CommonConstants.g_Conversion_Def_CoilTag_Name = "";
            //		CommonConstants.List_DelTagInfo = new ArrayList();
            //		CommonConstants.g_Project_Conversion = false;
            //		CommonConstants.lstCopyTaskIDs = new List<int>();
            //		CommonConstants.IECBackUpFolder = "BkpPrg";
            //		CommonConstants.List_DefBlockNames = new ArrayList();
            //		CommonConstants.g_Grid_Max_Rows = 256;
            //		CommonConstants.ProjectConversionPath = "";
            //		CommonConstants._stDatatype = string.Empty;
            //		CommonConstants.DEFAULT_NODETAG_FILE = "DefaultNodeTag.xml";
            //		CommonConstants.SERIAL_LADDER_DNLD_FILEID = 119;
            //		CommonConstants.SERIAL_FONT_DNLD_FILEID = 136;
            //		CommonConstants.SERIAL_FIRMWARE_DNLD_FILEID = 153;
            //		CommonConstants.SERIAL_APPLICATION_DNLD_FILEID = 238;
            //		CommonConstants.SERIAL_EXP_DNLD_FILEID = 225;
            //		CommonConstants.SERIAL_ANALOGEXP_DNLD_FILEID = 209;
            //		CommonConstants.ETHER_APP_LADD_DNLD_FILEID = 6;
            //		CommonConstants.ETHER_LADD_FIRM_DNLD_FILEID = 5;
            //		CommonConstants.ETHER_LADDER_DNLD_FILEID = 4;
            //		CommonConstants.ETHER_FONT_DNLD_FILEID = 3;
            //		CommonConstants.ETHER_APPLICATION_DNLD_FILEID = 2;
            //		CommonConstants.ETHER_FIRMWARE_LADD_DNLD_FILEID = 1;
            //		CommonConstants.ETHER_LOGG_UPLD_FILEID = 7;
            //		CommonConstants.ETHER_ETHERNET_SETTINGS_DNLD_FILEID = 11;
            //		CommonConstants.ETHER_APP_UPLD_FILEID = 6;
            //		CommonConstants.PLC_STATUS_FILEID = 112;
            //		CommonConstants.ETHER_LOGG_UPLD_FLASH_FILEID = 8;
            //		CommonConstants.ETHER_HISTORICAL_ALARM_UPLD_FILEID = 13;
            //		CommonConstants.LADDER_UPLD_FILEID = 64;
            //		CommonConstants.ETHERNET_PORT_NUMBER = 5000;
            //		CommonConstants.SHAPE_TRACKER_SIZE = 4;
            //		CommonConstants.SERIAL_APPLICATION_UPLD_FILEID = 187;
            //		CommonConstants.SERIAL_LOGGED_UPLD_FILEID = 170;
            //		CommonConstants.SERIAL_HISTALARM_UPLD_FILEID = 34;
            //		CommonConstants.TEMP_UPLOAD_FILENAME = "c:\\upLoad";
            //		CommonConstants.UPLOAD_LADDER_FILENAME = "UpldLadder.bin";
            //		CommonConstants.BAUDRATE = 115200;
            //		CommonConstants.PARITY = 0;
            //		CommonConstants.BITESIZE = 8;
            //		CommonConstants.STOPBIT = 1;
            //		CommonConstants.ON_LINE_COMMUNICATION = 0;
            //		CommonConstants.SERIAL_PORT = 0;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT230 = 62;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT230 = 127;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT230 = 62;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT230 = 127;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT230 = 44;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT230 = 100;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT_4030MT = 126;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT_4030MT = 62;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT_4030MT = 126;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT_4030MT = 62;
            //		CommonConstants.dsRecentProjectList = new DataSet();
            //		CommonConstants.dsRecentProjectsData = new DataSet();
            //		CommonConstants._lastCloseProjectPath = "";
            //		CommonConstants.CURRENTCULTURE = "";
            //		CommonConstants.DEFAULTPOWERONTASKBYTES = new byte[] { 4, 0, 1, 0, 1, 0 };
            //		CommonConstants.DRAWOPERATION = DrawingOperations.NONE;
            //		CommonConstants.DRAWOBJECT = DrawingObjects.NOOBJECT;
            //		CommonConstants.DRAWx1 = 0;
            //		CommonConstants.DRAWx2 = 0;
            //		CommonConstants.DRAWy1 = 0;
            //		CommonConstants.DRAWy2 = 0;
            //		CommonConstants._chBoxCheck = 0;
            //		CommonConstants._ethernetCheck = false;
            //		CommonConstants._isSoftwareOILDS = false;
            //		CommonConstants._checkPass = false;
            //		CommonConstants._pictureShowHide = false;
            //		CommonConstants.stGroupType_editPara = false;
            //		CommonConstants.varList1 = new ArrayList();
            //		CommonConstants._asciiDT = false;
            //		CommonConstants._isSoftwareFlexiSoft = false;
            //		CommonConstants._isSoftwareMaple = false;
            //		CommonConstants._checkProtocol = false;
            //		CommonConstants.tagHistroy = new ArrayList();
            //		CommonConstants._softwareName = "";
            //		CommonConstants._isSoftwareGTXLSoft = false;
            //		CommonConstants._isSoftwareHitachi = false;
            //		CommonConstants._isTagsSelected = new ArrayList();
            //		CommonConstants._totalNoNodes = false;
            //		CommonConstants._importInternelTagsCom1Com2 = false;
            //		CommonConstants._importModbusSlaveTagsCom1Com2 = false;
            //		CommonConstants._totalNoNodesInCSV = 0;
            //		CommonConstants._noOfLoopCount = 0;
            //		CommonConstants._isNextClick = false;
            //		CommonConstants._isOKClick = false;
            //		CommonConstants._scrCom1 = false;
            //		CommonConstants._scrCom2 = false;
            //		CommonConstants._isMasterNodeAdded = false;
            //		CommonConstants._isModbusSlave_Ev3defined = false;
            //		CommonConstants._isCancelClick = false;
            //		CommonConstants._isSlaveSelected = false;
            //		CommonConstants._TagSelectionGUIFromDataLogger = false;
            //		CommonConstants._isNodeModbusMasterEdited = false;
            //		CommonConstants._tagUsage = false;
            //		CommonConstants._ShowImportTags = false;
            //		CommonConstants._ShowImportTagsOK = false;
            //		CommonConstants._totalNoTagsInCSV = 0;
            //		CommonConstants._totalNoTagsCount = 0;
            //		CommonConstants._totalTagList = new ArrayList();
            //		CommonConstants._importTagList = new ArrayList();
            //		CommonConstants.CONFIGFILENAME = "Prizm4conf.xml";
            //		CommonConstants.MINIMUMSCREENBYTES = 68;
            //		CommonConstants.strProtocolFileName = "ModelInformation.xml";
            //		CommonConstants.strPortInfoFileName = "PortInformation.xml";
            //		CommonConstants.UndoActionCount = 20;
            //		CommonConstants.SelectedObjectCount = null;
            //		CommonConstants._commconstantProductData = new CommonConstants.ProductData();
            //		CommonConstants.HorizontalGrid = 5;
            //		CommonConstants.VerticalGrid = 5;
            //		CommonConstants.GridStyle = 0;
            //		CommonConstants.iActiveScreenNumber = -1;
            //		CommonConstants.iActiveDragDropScreenNumber = -1;
            //		CommonConstants._commconstiZoomFactor = 100f;
            //		CommonConstants.ShowHideShowValueOn = false;
            //		CommonConstants.ShowHideWithinRange = false;
            //		CommonConstants.ShowHideFromRange = 0;
            //		CommonConstants.ShowHideToRange = 0;
            //		CommonConstants.TotalNoOfObject = 1;
            //		CommonConstants.DefaultObjectBackColor = 26;
            //		CommonConstants.DefaultObjectThickness = 1;
            //		CommonConstants.btPrizm4Version = 40;
            //		CommonConstants.DefaultObjectTopLeft = new Point(10, 10);
            //		CommonConstants.DefaultObjectBottomRight = new Point(100, 100);
            //		CommonConstants.DefaultObjectNumber = 1;
            //		CommonConstants.DefaultObjectType = 10;
            //		CommonConstants.DefaultObjectZLevel = 0;
            //		CommonConstants.DefaultObjectBorder = 1;
            //		CommonConstants.DefaultEv3ProjectHSCounter = 1766;
            //		CommonConstants.DefaultEv3ProjectHSTimmer = 80;
            //		CommonConstants.DefaultEv3ProjectPID = 0;
            //		CommonConstants.DefaultEv3ProjectChannel = 0;
            //		CommonConstants.DefaultEv3ProjectASCIIComm = 0;
            //		CommonConstants.DefaultEv3ProjectPOnTaskListSize = 6;
            //		CommonConstants.ColorIndex = 0;
            //		CommonConstants.PatternIndex = 0;
            //		CommonConstants.IsOverlappingChecked = false;
            //		CommonConstants.FeedBackTagValue = "";
            //		CommonConstants.strImportAlarmLogErrFile = "";
            //		CommonConstants.strImportAlarmSeverityErr = "";
            //		CommonConstants.strImportAlarmIDErr = "";
            //		CommonConstants.strImportAlarmErrorsFound = "";
            //		CommonConstants.strImportAlarmTextErr = "";
            //		CommonConstants.blFileErrMsgFlag = false;
            //		CommonConstants.blwaitdlgFlag = false;
            //		CommonConstants.BitMapAddressDataTT = new ArrayList();
            //		CommonConstants.BitMapObjectAddressTT = new ArrayList();
            //		CommonConstants.BitMapAddressDataCoil = new ArrayList();
            //		CommonConstants.BitMapObjectAddressCoil = new ArrayList();
            //		CommonConstants.WindowsUnsignedFontsUsedInApplication = new int[10];
            //		CommonConstants.WindowsHexFontsUsedInApplication = new int[10];
            //		CommonConstants.WindowsASCIIFontsUsedInApplication = new int[10];
            //		CommonConstants.CurrenmtScreenStartPosition = 0;
            //		CommonConstants.strFontName = "Arial";
            //		CommonConstants.fontIndex = 0;
            //		CommonConstants.rdeHtBitMapAddressDataTT = new Hashtable();
            //		CommonConstants.rdeASCIIHtBitMapAddressDataTT = new Hashtable();
            //		CommonConstants.rdeHEXHtBitMapAddressDataTT = new Hashtable();
            //		CommonConstants.blFiveBySeven = false;
            //		CommonConstants.blSevenByFourteen = false;
            //		CommonConstants.blTenByFourteen = false;
            //		CommonConstants.NextAlarm = "Next Alarm";
            //		CommonConstants.PrevAlarm = "Previous Alarm";
            //		CommonConstants.AckAlarm = "Ack Alarm";
            //		CommonConstants.AckAllAlarms = "Ack All Alarms";
            //		CommonConstants.AcknowledgeAlarm = "Acknowledge Alarm";
            //		CommonConstants.AcknowledgeAllAlarms = "Acknowledge All Alarms";
            //		CommonConstants.WriteValueToTag = "";
            //		CommonConstants.AddAConstantValueToATag = "";
            //		CommonConstants.SubtractAConstantValueFromATag = "";
            //		CommonConstants.AddTagBToTagA = "";
            //		CommonConstants.SubtractTagBFromTagA = "";
            //		CommonConstants.CopyTagBToTagA = "";
            //		CommonConstants.SwapTagAAndTagB = "";
            //		CommonConstants.CopyTagToSTR = "";
            //		CommonConstants.CopyTagToLED = "";
            //		CommonConstants.CopyPrizmToPLC = "";
            //		CommonConstants.CopyPLCToPrizm = "";
            //		CommonConstants.CopyRTCToPLC = "";
            //		CommonConstants.ExecutePLCLogicBlock = "";
            //		CommonConstants.USBDataLogUpload = "";
            //		CommonConstants.USBHostUpload = "";
            //		CommonConstants.SDCardUpload = "";
            //		CommonConstants.DEFAULT_POPUP_SCREEN_TOPLEFT_X = 0;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_TOPLEFT_Y = 0;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_HEIGHT = 180;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_WIDTH = 237;
            //		CommonConstants.DEFAULT_SCREEN_SCRATCHPAD_AREA = 768;
            //		CommonConstants.MaximumFormSize = 1088;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT290 = 135;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT290 = 130;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT290 = 155;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT290 = 160;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT290 = 75;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT290 = 120;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_PRODUCT290 = 200;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENWIDTH_PRODUCT290 = 212;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENHEIGHT_ALLPRODUCTS = 162;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENWIDTH_ALLPRODUCTS = 157;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENHEIGHT_ALLPRODUCTS = 189;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENWIDTH_ALLPRODUCTS = 194;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENHEIGHT_ALLPRODUCTS = 81;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENWIDTH_ALLPRODUCTS = 120;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_ALLPRODUCTS = 218;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENWIDTH_ALLPRODUCTS = 292;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT5XXX = 375;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT5XXX = 375;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT5XXX = 375;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT5XXX = 375;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT5XXX = 150;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT5XXX = 300;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_PRODUCT5XXX = 360;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENWIDTH_PRODUCT5XXX = 400;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_HEIGHT_PRODUCT5XXX = 375;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_WIDTH_PRODUCT5XXX = 375;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENHEIGHT_PRODUCT5X43 = 225;
            //		CommonConstants.DEFAULT_NUMKEYPAD_SCREENWIDTH_PRODUCT5X43 = 225;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENHEIGHT_PRODUCT5X43 = 225;
            //		CommonConstants.DEFAULT_HEXKEYPAD_SCREENWIDTH_PRODUCT5X43 = 225;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENHEIGHT_PRODUCT5X43 = 100;
            //		CommonConstants.DEFAULT_BITKEYPAD_SCREENWIDTH_PRODUCT5X43 = 200;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENHEIGHT_PRODUCT5X43 = 210;
            //		CommonConstants.DEFAULT_ASCIIKEYPAD_SCREENWIDTH_PRODUCT5X43 = 305;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_HEIGHT_PRODUCT5X43 = 225;
            //		CommonConstants.DEFAULT_POPUP_SCREEN_WIDTH_PRODUCT5X43 = 225;
            //		CommonConstants.m_gBlockType = 0;
            //		CommonConstants.selIECBlockType = 0;
            //		CommonConstants.DOWNLOAD_FONT_FILENAME = "prizmFont.bin";
            //		CommonConstants.DOWNLOAD_LADDER_FILENAME = "ladder.bin";
            //		CommonConstants.DOWNLOAD_FIRMWARE_FILENAME = "firmware.abs";
            //		CommonConstants.DOWNLOAD_FHWT_FILENAME = "fhwt.bin";
            //		CommonConstants.DefaultBlockName = "Block";
            //		CommonConstants.BitMapAddressData = new ArrayList();
            //		CommonConstants.BitMapObjectAddress = new ArrayList();
            //		CommonConstants.MaximumIntensityOfRed = 240;
            //		CommonConstants.MaximumIntensityOfGreen = 240;
            //		CommonConstants.MaximumIntensityOfBlue = 240;
            //		CommonConstants.DEFAULT_COLOR_SUPPORT = 262144;
            //		CommonConstants.communicationStatus = 0;
            //		CommonConstants.downloadSucess = false;
            //		CommonConstants.DefaultEv3ProjectLanguageID = 50;
            //		CommonConstants.NumberOfKeysInRowAndColumn = new int[,] { { 4, 4 }, { 3, 4 }, { 5, 4 }, { 5, 5 }, { 5, 3 }, { 4, 2 }, { 5, 1 }, { 3, 1 }, { 4, 4 } };
            //		string[,] strArrays = new string[10, 25];
            //		strArrays[0, 0] = "1";
            //		strArrays[0, 1] = "2";
            //		strArrays[0, 2] = "3";
            //		strArrays[0, 3] = "+/-";
            //		strArrays[0, 4] = "4";
            //		strArrays[0, 5] = "5";
            //		strArrays[0, 6] = "6";
            //		strArrays[0, 7] = "CLR";
            //		strArrays[0, 8] = "7";
            //		strArrays[0, 9] = "8";
            //		strArrays[0, 10] = "9";
            //		strArrays[0, 11] = "";
            //		strArrays[0, 12] = "0";
            //		strArrays[0, 13] = "0";
            //		strArrays[0, 14] = "";
            //		strArrays[0, 15] = "ENT";
            //		strArrays[0, 16] = "";
            //		strArrays[0, 17] = "";
            //		strArrays[0, 18] = "";
            //		strArrays[0, 19] = "";
            //		strArrays[0, 20] = "";
            //		strArrays[0, 21] = "";
            //		strArrays[0, 22] = "";
            //		strArrays[0, 23] = "";
            //		strArrays[0, 24] = "";
            //		strArrays[1, 0] = "1";
            //		strArrays[1, 1] = "2";
            //		strArrays[1, 2] = "3";
            //		strArrays[1, 3] = "4";
            //		strArrays[1, 4] = "5";
            //		strArrays[1, 5] = "6";
            //		strArrays[1, 6] = "7";
            //		strArrays[1, 7] = "8";
            //		strArrays[1, 8] = "9";
            //		strArrays[1, 9] = "CLR";
            //		strArrays[1, 10] = "0";
            //		strArrays[1, 11] = "ENT";
            //		strArrays[1, 12] = "";
            //		strArrays[1, 13] = "";
            //		strArrays[1, 14] = "";
            //		strArrays[1, 15] = "";
            //		strArrays[1, 16] = "";
            //		strArrays[1, 17] = "";
            //		strArrays[1, 18] = "";
            //		strArrays[1, 19] = "";
            //		strArrays[1, 20] = "";
            //		strArrays[1, 21] = "";
            //		strArrays[1, 22] = "";
            //		strArrays[1, 23] = "";
            //		strArrays[1, 24] = "";
            //		strArrays[2, 0] = "1";
            //		strArrays[2, 1] = "2";
            //		strArrays[2, 2] = "3";
            //		strArrays[2, 3] = "A";
            //		strArrays[2, 4] = "B";
            //		strArrays[2, 5] = "4";
            //		strArrays[2, 6] = "5";
            //		strArrays[2, 7] = "6";
            //		strArrays[2, 8] = "C";
            //		strArrays[2, 9] = "D";
            //		strArrays[2, 10] = "7";
            //		strArrays[2, 11] = "8";
            //		strArrays[2, 12] = "9";
            //		strArrays[2, 13] = "E";
            //		strArrays[2, 14] = "F";
            //		strArrays[2, 15] = "+/-";
            //		strArrays[2, 16] = "0";
            //		strArrays[2, 17] = "ESC";
            //		strArrays[2, 18] = "CLR";
            //		strArrays[2, 19] = "ENT";
            //		strArrays[2, 20] = "";
            //		strArrays[2, 21] = "";
            //		strArrays[2, 22] = "";
            //		strArrays[2, 23] = "";
            //		strArrays[2, 24] = "";
            //		strArrays[3, 0] = "1";
            //		strArrays[3, 1] = "2";
            //		strArrays[3, 2] = "3";
            //		strArrays[3, 3] = "A";
            //		strArrays[3, 4] = "B";
            //		strArrays[3, 5] = "4";
            //		strArrays[3, 6] = "5";
            //		strArrays[3, 7] = "6";
            //		strArrays[3, 8] = "C";
            //		strArrays[3, 9] = "D";
            //		strArrays[3, 10] = "7";
            //		strArrays[3, 11] = "8";
            //		strArrays[3, 12] = "9";
            //		strArrays[3, 13] = "E";
            //		strArrays[3, 14] = "F";
            //		strArrays[3, 15] = "+/-";
            //		strArrays[3, 16] = "0";
            //		strArrays[3, 17] = "^";
            //		strArrays[3, 18] = "CLR";
            //		strArrays[3, 19] = "ESC";
            //		strArrays[3, 20] = "<-";
            //		strArrays[3, 21] = "->";
            //		strArrays[3, 22] = "v";
            //		strArrays[3, 23] = "ENT";
            //		strArrays[3, 24] = "ENT";
            //		strArrays[4, 0] = "<-";
            //		strArrays[4, 1] = "->";
            //		strArrays[4, 2] = "INC";
            //		strArrays[4, 3] = "CLR";
            //		strArrays[4, 4] = "CLR";
            //		strArrays[4, 5] = "^";
            //		strArrays[4, 6] = "v";
            //		strArrays[4, 7] = "DCR";
            //		strArrays[4, 8] = "";
            //		strArrays[4, 9] = "";
            //		strArrays[4, 10] = "0";
            //		strArrays[4, 11] = "1";
            //		strArrays[4, 12] = "ESC";
            //		strArrays[4, 13] = "0";
            //		strArrays[4, 14] = "ENT";
            //		strArrays[4, 15] = "";
            //		strArrays[4, 16] = "";
            //		strArrays[4, 17] = "";
            //		strArrays[4, 18] = "";
            //		strArrays[4, 19] = "";
            //		strArrays[4, 20] = "";
            //		strArrays[4, 21] = "";
            //		strArrays[4, 22] = "";
            //		strArrays[4, 23] = "";
            //		strArrays[4, 24] = "";
            //		strArrays[5, 0] = "<-";
            //		strArrays[5, 1] = "->";
            //		strArrays[5, 2] = "CLR";
            //		strArrays[5, 3] = "CLR";
            //		strArrays[5, 4] = "^";
            //		strArrays[5, 5] = "v";
            //		strArrays[5, 6] = "ENT";
            //		strArrays[5, 7] = "ENT";
            //		strArrays[5, 8] = "";
            //		strArrays[5, 9] = "";
            //		strArrays[5, 10] = "";
            //		strArrays[5, 11] = "";
            //		strArrays[5, 12] = "";
            //		strArrays[5, 13] = "";
            //		strArrays[5, 14] = "";
            //		strArrays[5, 15] = "";
            //		strArrays[5, 16] = "";
            //		strArrays[5, 17] = "";
            //		strArrays[5, 18] = "";
            //		strArrays[5, 19] = "";
            //		strArrays[5, 20] = "";
            //		strArrays[5, 21] = "";
            //		strArrays[5, 22] = "";
            //		strArrays[5, 23] = "";
            //		strArrays[5, 24] = "";
            //		strArrays[6, 0] = "<-";
            //		strArrays[6, 1] = "^";
            //		strArrays[6, 2] = "v";
            //		strArrays[6, 3] = "->";
            //		strArrays[6, 4] = "ENT";
            //		strArrays[6, 5] = "";
            //		strArrays[6, 6] = "";
            //		strArrays[6, 7] = "";
            //		strArrays[6, 8] = "";
            //		strArrays[6, 9] = "";
            //		strArrays[6, 10] = "";
            //		strArrays[6, 11] = "";
            //		strArrays[6, 12] = "";
            //		strArrays[6, 13] = "";
            //		strArrays[6, 14] = "";
            //		strArrays[6, 15] = "";
            //		strArrays[6, 16] = "";
            //		strArrays[6, 17] = "";
            //		strArrays[6, 18] = "";
            //		strArrays[6, 19] = "";
            //		strArrays[6, 20] = "";
            //		strArrays[6, 21] = "";
            //		strArrays[6, 22] = "";
            //		strArrays[6, 23] = "";
            //		strArrays[6, 24] = "";
            //		strArrays[7, 0] = "ON";
            //		strArrays[7, 1] = "OFF";
            //		strArrays[7, 2] = "ENT";
            //		strArrays[7, 3] = "";
            //		strArrays[7, 4] = "";
            //		strArrays[7, 5] = "";
            //		strArrays[7, 6] = "";
            //		strArrays[7, 7] = "";
            //		strArrays[7, 8] = "";
            //		strArrays[7, 9] = "";
            //		strArrays[7, 10] = "";
            //		strArrays[7, 11] = "";
            //		strArrays[7, 12] = "";
            //		strArrays[7, 13] = "";
            //		strArrays[7, 14] = "";
            //		strArrays[7, 15] = "";
            //		strArrays[7, 16] = "";
            //		strArrays[7, 17] = "";
            //		strArrays[7, 18] = "";
            //		strArrays[7, 19] = "";
            //		strArrays[7, 20] = "";
            //		strArrays[7, 21] = "";
            //		strArrays[7, 22] = "";
            //		strArrays[7, 23] = "";
            //		strArrays[7, 24] = "";
            //		strArrays[8, 0] = "0";
            //		strArrays[8, 1] = "1";
            //		strArrays[8, 2] = "2";
            //		strArrays[8, 3] = "3";
            //		strArrays[8, 4] = "4";
            //		strArrays[8, 5] = "5";
            //		strArrays[8, 6] = "6";
            //		strArrays[8, 7] = "7";
            //		strArrays[8, 8] = "8";
            //		strArrays[8, 9] = "9";
            //		strArrays[8, 10] = "CLR";
            //		strArrays[8, 11] = "ENT";
            //		strArrays[8, 12] = "ABORT";
            //		strArrays[8, 13] = "";
            //		strArrays[8, 14] = "";
            //		strArrays[8, 15] = "";
            //		strArrays[8, 16] = "";
            //		strArrays[8, 17] = "";
            //		strArrays[8, 18] = "";
            //		strArrays[8, 19] = "";
            //		strArrays[8, 20] = "";
            //		strArrays[8, 21] = "";
            //		strArrays[8, 22] = "";
            //		strArrays[8, 23] = "";
            //		strArrays[8, 24] = "";
            //		strArrays[9, 0] = "";
            //		strArrays[9, 1] = "";
            //		strArrays[9, 2] = "";
            //		strArrays[9, 3] = "";
            //		strArrays[9, 4] = "";
            //		strArrays[9, 5] = "";
            //		strArrays[9, 6] = "";
            //		strArrays[9, 7] = "";
            //		strArrays[9, 8] = "";
            //		strArrays[9, 9] = "";
            //		strArrays[9, 10] = "";
            //		strArrays[9, 11] = "";
            //		strArrays[9, 12] = "";
            //		strArrays[9, 13] = "";
            //		strArrays[9, 14] = "";
            //		strArrays[9, 15] = "";
            //		strArrays[9, 16] = "";
            //		strArrays[9, 17] = "";
            //		strArrays[9, 18] = "";
            //		strArrays[9, 19] = "";
            //		strArrays[9, 20] = "";
            //		strArrays[9, 21] = "";
            //		strArrays[9, 22] = "";
            //		strArrays[9, 23] = "";
            //		strArrays[9, 24] = "";
            //		CommonConstants.KeysTitle = strArrays;
            //		strArrays = new string[3, 56];
            //		strArrays[0, 0] = "~";
            //		strArrays[0, 1] = "!";
            //		strArrays[0, 2] = "@";
            //		strArrays[0, 3] = "#";
            //		strArrays[0, 4] = "$";
            //		strArrays[0, 5] = "%";
            //		strArrays[0, 6] = "^";
            //		strArrays[0, 7] = "&";
            //		strArrays[0, 8] = "*";
            //		strArrays[0, 9] = "(";
            //		strArrays[0, 10] = ")";
            //		strArrays[0, 11] = "_";
            //		strArrays[0, 12] = "+";
            //		strArrays[0, 13] = "<";
            //		strArrays[0, 14] = ">";
            //		strArrays[0, 15] = ":";
            //		strArrays[0, 16] = "A";
            //		strArrays[0, 17] = "B";
            //		strArrays[0, 18] = "C";
            //		strArrays[0, 19] = "D";
            //		strArrays[0, 20] = "E";
            //		strArrays[0, 21] = "F";
            //		strArrays[0, 22] = "G";
            //		strArrays[0, 23] = "H";
            //		strArrays[0, 24] = "I";
            //		strArrays[0, 25] = "J";
            //		strArrays[0, 26] = "K";
            //		strArrays[0, 27] = "L";
            //		strArrays[0, 28] = "M";
            //		strArrays[0, 29] = "N";
            //		strArrays[0, 30] = "O";
            //		strArrays[0, 31] = "P";
            //		strArrays[0, 32] = "Q";
            //		strArrays[0, 33] = "R";
            //		strArrays[0, 34] = "S";
            //		strArrays[0, 35] = "T";
            //		strArrays[0, 36] = "U";
            //		strArrays[0, 37] = "V";
            //		strArrays[0, 38] = "W";
            //		strArrays[0, 39] = "X";
            //		strArrays[0, 40] = "Y";
            //		strArrays[0, 41] = "Z";
            //		strArrays[0, 42] = "{";
            //		strArrays[0, 43] = "}";
            //		strArrays[0, 44] = "|";
            //		strArrays[0, 45] = "?";
            //		strArrays[0, 46] = "\"";
            //		strArrays[0, 47] = "";
            //		strArrays[0, 48] = "";
            //		strArrays[0, 49] = "";
            //		strArrays[0, 50] = "";
            //		strArrays[0, 51] = "";
            //		strArrays[0, 52] = "";
            //		strArrays[0, 53] = "";
            //		strArrays[0, 54] = "";
            //		strArrays[0, 55] = "";
            //		strArrays[1, 0] = "`";
            //		strArrays[1, 1] = "1";
            //		strArrays[1, 2] = "2";
            //		strArrays[1, 3] = "3";
            //		strArrays[1, 4] = "4";
            //		strArrays[1, 5] = "5";
            //		strArrays[1, 6] = "6";
            //		strArrays[1, 7] = "7";
            //		strArrays[1, 8] = "8";
            //		strArrays[1, 9] = "9";
            //		strArrays[1, 10] = "0";
            //		strArrays[1, 11] = "-";
            //		strArrays[1, 12] = "=";
            //		strArrays[1, 13] = ",";
            //		strArrays[1, 14] = ".";
            //		strArrays[1, 15] = ";";
            //		strArrays[1, 16] = "a";
            //		strArrays[1, 17] = "b";
            //		strArrays[1, 18] = "c";
            //		strArrays[1, 19] = "d";
            //		strArrays[1, 20] = "e";
            //		strArrays[1, 21] = "f";
            //		strArrays[1, 22] = "g";
            //		strArrays[1, 23] = "h";
            //		strArrays[1, 24] = "i";
            //		strArrays[1, 25] = "j";
            //		strArrays[1, 26] = "k";
            //		strArrays[1, 27] = "l";
            //		strArrays[1, 28] = "m";
            //		strArrays[1, 29] = "n";
            //		strArrays[1, 30] = "o";
            //		strArrays[1, 31] = "p";
            //		strArrays[1, 32] = "q";
            //		strArrays[1, 33] = "r";
            //		strArrays[1, 34] = "s";
            //		strArrays[1, 35] = "t";
            //		strArrays[1, 36] = "u";
            //		strArrays[1, 37] = "v";
            //		strArrays[1, 38] = "w";
            //		strArrays[1, 39] = "x";
            //		strArrays[1, 40] = "y";
            //		strArrays[1, 41] = "z";
            //		strArrays[1, 42] = "[";
            //		strArrays[1, 43] = "]";
            //		strArrays[1, 44] = "\\";
            //		strArrays[1, 45] = "/";
            //		strArrays[1, 46] = "'";
            //		strArrays[1, 47] = "CLR";
            //		strArrays[1, 48] = "Home";
            //		strArrays[1, 49] = "End";
            //		strArrays[1, 50] = "SP";
            //		strArrays[1, 51] = "BS";
            //		strArrays[1, 52] = "<<";
            //		strArrays[1, 53] = ">>";
            //		strArrays[1, 54] = "Shift";
            //		strArrays[1, 55] = "ENT";
            //		strArrays[2, 0] = "0";
            //		strArrays[2, 1] = "1";
            //		strArrays[2, 2] = "2";
            //		strArrays[2, 3] = "3";
            //		strArrays[2, 4] = "4";
            //		strArrays[2, 5] = "5";
            //		strArrays[2, 6] = "6";
            //		strArrays[2, 7] = "7";
            //		strArrays[2, 8] = "8";
            //		strArrays[2, 9] = "9";
            //		strArrays[2, 10] = ".";
            //		strArrays[2, 11] = "ENT";
            //		strArrays[2, 12] = "";
            //		strArrays[2, 13] = "";
            //		strArrays[2, 14] = "";
            //		strArrays[2, 15] = "";
            //		strArrays[2, 16] = "";
            //		strArrays[2, 17] = "";
            //		strArrays[2, 18] = "";
            //		strArrays[2, 19] = "";
            //		strArrays[2, 20] = "";
            //		strArrays[2, 21] = "";
            //		strArrays[2, 22] = "";
            //		strArrays[2, 23] = "";
            //		strArrays[2, 24] = "";
            //		strArrays[2, 25] = "";
            //		strArrays[2, 26] = "";
            //		strArrays[2, 27] = "";
            //		strArrays[2, 28] = "";
            //		strArrays[2, 29] = "";
            //		strArrays[2, 30] = "";
            //		strArrays[2, 31] = "";
            //		strArrays[2, 32] = "";
            //		strArrays[2, 33] = "";
            //		strArrays[2, 34] = "";
            //		strArrays[2, 35] = "";
            //		strArrays[2, 36] = "";
            //		strArrays[2, 37] = "";
            //		strArrays[2, 38] = "";
            //		strArrays[2, 39] = "";
            //		strArrays[2, 40] = "";
            //		strArrays[2, 41] = "";
            //		strArrays[2, 42] = "";
            //		strArrays[2, 43] = "";
            //		strArrays[2, 44] = "";
            //		strArrays[2, 45] = "";
            //		strArrays[2, 46] = "";
            //		strArrays[2, 47] = "";
            //		strArrays[2, 48] = "";
            //		strArrays[2, 49] = "";
            //		strArrays[2, 50] = "";
            //		strArrays[2, 51] = "";
            //		strArrays[2, 52] = "";
            //		strArrays[2, 53] = "";
            //		strArrays[2, 54] = "";
            //		strArrays[2, 55] = "";
            //		CommonConstants.AsciiKeysTitle = strArrays;
            //		KeyStyle[,] keyStyleArray = new KeyStyle[9, 25];
            //		keyStyleArray[0, 0] = KeyStyle.ONE;
            //		keyStyleArray[0, 1] = KeyStyle.ONE;
            //		keyStyleArray[0, 2] = KeyStyle.ONE;
            //		keyStyleArray[0, 3] = KeyStyle.ONE;
            //		keyStyleArray[0, 4] = KeyStyle.ONE;
            //		keyStyleArray[0, 5] = KeyStyle.ONE;
            //		keyStyleArray[0, 6] = KeyStyle.ONE;
            //		keyStyleArray[0, 7] = KeyStyle.ONE;
            //		keyStyleArray[0, 8] = KeyStyle.ONE;
            //		keyStyleArray[0, 9] = KeyStyle.ONE;
            //		keyStyleArray[0, 10] = KeyStyle.ONE;
            //		keyStyleArray[0, 11] = KeyStyle.THREE_IN_ONE;
            //		keyStyleArray[0, 12] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[0, 13] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[0, 14] = KeyStyle.THREE_IN_ONE;
            //		keyStyleArray[0, 15] = KeyStyle.THREE_IN_ONE;
            //		keyStyleArray[0, 16] = KeyStyle.ONE;
            //		keyStyleArray[0, 17] = KeyStyle.ONE;
            //		keyStyleArray[0, 18] = KeyStyle.ONE;
            //		keyStyleArray[0, 19] = KeyStyle.ONE;
            //		keyStyleArray[0, 20] = KeyStyle.ONE;
            //		keyStyleArray[0, 21] = KeyStyle.ONE;
            //		keyStyleArray[0, 22] = KeyStyle.ONE;
            //		keyStyleArray[0, 23] = KeyStyle.ONE;
            //		keyStyleArray[0, 24] = KeyStyle.ONE;
            //		keyStyleArray[1, 0] = KeyStyle.ONE;
            //		keyStyleArray[1, 1] = KeyStyle.ONE;
            //		keyStyleArray[1, 2] = KeyStyle.ONE;
            //		keyStyleArray[1, 3] = KeyStyle.ONE;
            //		keyStyleArray[1, 4] = KeyStyle.ONE;
            //		keyStyleArray[1, 5] = KeyStyle.ONE;
            //		keyStyleArray[1, 6] = KeyStyle.ONE;
            //		keyStyleArray[1, 7] = KeyStyle.ONE;
            //		keyStyleArray[1, 8] = KeyStyle.ONE;
            //		keyStyleArray[1, 9] = KeyStyle.ONE;
            //		keyStyleArray[1, 10] = KeyStyle.ONE;
            //		keyStyleArray[1, 11] = KeyStyle.ONE;
            //		keyStyleArray[1, 12] = KeyStyle.ONE;
            //		keyStyleArray[1, 13] = KeyStyle.ONE;
            //		keyStyleArray[1, 14] = KeyStyle.ONE;
            //		keyStyleArray[1, 15] = KeyStyle.ONE;
            //		keyStyleArray[1, 16] = KeyStyle.ONE;
            //		keyStyleArray[1, 17] = KeyStyle.ONE;
            //		keyStyleArray[1, 18] = KeyStyle.ONE;
            //		keyStyleArray[1, 19] = KeyStyle.ONE;
            //		keyStyleArray[1, 20] = KeyStyle.ONE;
            //		keyStyleArray[1, 21] = KeyStyle.ONE;
            //		keyStyleArray[1, 22] = KeyStyle.ONE;
            //		keyStyleArray[1, 23] = KeyStyle.ONE;
            //		keyStyleArray[1, 24] = KeyStyle.ONE;
            //		keyStyleArray[2, 0] = KeyStyle.ONE;
            //		keyStyleArray[2, 1] = KeyStyle.ONE;
            //		keyStyleArray[2, 2] = KeyStyle.ONE;
            //		keyStyleArray[2, 3] = KeyStyle.ONE;
            //		keyStyleArray[2, 4] = KeyStyle.ONE;
            //		keyStyleArray[2, 5] = KeyStyle.ONE;
            //		keyStyleArray[2, 6] = KeyStyle.ONE;
            //		keyStyleArray[2, 7] = KeyStyle.ONE;
            //		keyStyleArray[2, 8] = KeyStyle.ONE;
            //		keyStyleArray[2, 9] = KeyStyle.ONE;
            //		keyStyleArray[2, 10] = KeyStyle.ONE;
            //		keyStyleArray[2, 11] = KeyStyle.ONE;
            //		keyStyleArray[2, 12] = KeyStyle.ONE;
            //		keyStyleArray[2, 13] = KeyStyle.ONE;
            //		keyStyleArray[2, 14] = KeyStyle.ONE;
            //		keyStyleArray[2, 15] = KeyStyle.ONE;
            //		keyStyleArray[2, 16] = KeyStyle.ONE;
            //		keyStyleArray[2, 17] = KeyStyle.ONE;
            //		keyStyleArray[2, 18] = KeyStyle.ONE;
            //		keyStyleArray[2, 19] = KeyStyle.ONE;
            //		keyStyleArray[2, 20] = KeyStyle.ONE;
            //		keyStyleArray[2, 21] = KeyStyle.ONE;
            //		keyStyleArray[2, 22] = KeyStyle.ONE;
            //		keyStyleArray[2, 23] = KeyStyle.ONE;
            //		keyStyleArray[2, 24] = KeyStyle.ONE;
            //		keyStyleArray[3, 0] = KeyStyle.ONE;
            //		keyStyleArray[3, 1] = KeyStyle.ONE;
            //		keyStyleArray[3, 2] = KeyStyle.ONE;
            //		keyStyleArray[3, 3] = KeyStyle.ONE;
            //		keyStyleArray[3, 4] = KeyStyle.ONE;
            //		keyStyleArray[3, 5] = KeyStyle.ONE;
            //		keyStyleArray[3, 6] = KeyStyle.ONE;
            //		keyStyleArray[3, 7] = KeyStyle.ONE;
            //		keyStyleArray[3, 8] = KeyStyle.ONE;
            //		keyStyleArray[3, 9] = KeyStyle.ONE;
            //		keyStyleArray[3, 10] = KeyStyle.ONE;
            //		keyStyleArray[3, 11] = KeyStyle.ONE;
            //		keyStyleArray[3, 12] = KeyStyle.ONE;
            //		keyStyleArray[3, 13] = KeyStyle.ONE;
            //		keyStyleArray[3, 14] = KeyStyle.ONE;
            //		keyStyleArray[3, 15] = KeyStyle.ONE;
            //		keyStyleArray[3, 16] = KeyStyle.ONE;
            //		keyStyleArray[3, 17] = KeyStyle.ONE;
            //		keyStyleArray[3, 18] = KeyStyle.ONE;
            //		keyStyleArray[3, 19] = KeyStyle.ONE;
            //		keyStyleArray[3, 20] = KeyStyle.ONE;
            //		keyStyleArray[3, 21] = KeyStyle.ONE;
            //		keyStyleArray[3, 22] = KeyStyle.ONE;
            //		keyStyleArray[3, 23] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[3, 24] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[4, 0] = KeyStyle.ONE;
            //		keyStyleArray[4, 1] = KeyStyle.ONE;
            //		keyStyleArray[4, 2] = KeyStyle.ONE;
            //		keyStyleArray[4, 3] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[4, 4] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[4, 5] = KeyStyle.ONE;
            //		keyStyleArray[4, 6] = KeyStyle.ONE;
            //		keyStyleArray[4, 7] = KeyStyle.ONE;
            //		keyStyleArray[4, 8] = KeyStyle.FOUR_IN_ONE;
            //		keyStyleArray[4, 9] = KeyStyle.FOUR_IN_ONE;
            //		keyStyleArray[4, 10] = KeyStyle.ONE;
            //		keyStyleArray[4, 11] = KeyStyle.ONE;
            //		keyStyleArray[4, 12] = KeyStyle.ONE;
            //		keyStyleArray[4, 13] = KeyStyle.FOUR_IN_ONE;
            //		keyStyleArray[4, 14] = KeyStyle.FOUR_IN_ONE;
            //		keyStyleArray[4, 15] = KeyStyle.ONE;
            //		keyStyleArray[4, 16] = KeyStyle.ONE;
            //		keyStyleArray[4, 17] = KeyStyle.ONE;
            //		keyStyleArray[4, 18] = KeyStyle.ONE;
            //		keyStyleArray[4, 19] = KeyStyle.ONE;
            //		keyStyleArray[4, 20] = KeyStyle.ONE;
            //		keyStyleArray[4, 21] = KeyStyle.ONE;
            //		keyStyleArray[4, 22] = KeyStyle.ONE;
            //		keyStyleArray[4, 23] = KeyStyle.ONE;
            //		keyStyleArray[4, 24] = KeyStyle.ONE;
            //		keyStyleArray[5, 0] = KeyStyle.ONE;
            //		keyStyleArray[5, 1] = KeyStyle.ONE;
            //		keyStyleArray[5, 2] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[5, 3] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[5, 4] = KeyStyle.ONE;
            //		keyStyleArray[5, 5] = KeyStyle.ONE;
            //		keyStyleArray[5, 6] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[5, 7] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[5, 8] = KeyStyle.ONE;
            //		keyStyleArray[5, 9] = KeyStyle.ONE;
            //		keyStyleArray[5, 10] = KeyStyle.ONE;
            //		keyStyleArray[5, 11] = KeyStyle.ONE;
            //		keyStyleArray[5, 12] = KeyStyle.ONE;
            //		keyStyleArray[5, 13] = KeyStyle.ONE;
            //		keyStyleArray[5, 14] = KeyStyle.FOUR_IN_ONE;
            //		keyStyleArray[5, 15] = KeyStyle.ONE;
            //		keyStyleArray[5, 16] = KeyStyle.ONE;
            //		keyStyleArray[5, 17] = KeyStyle.ONE;
            //		keyStyleArray[5, 18] = KeyStyle.ONE;
            //		keyStyleArray[5, 19] = KeyStyle.ONE;
            //		keyStyleArray[5, 20] = KeyStyle.ONE;
            //		keyStyleArray[5, 21] = KeyStyle.ONE;
            //		keyStyleArray[5, 22] = KeyStyle.ONE;
            //		keyStyleArray[5, 23] = KeyStyle.ONE;
            //		keyStyleArray[5, 24] = KeyStyle.ONE;
            //		keyStyleArray[6, 0] = KeyStyle.ONE;
            //		keyStyleArray[6, 1] = KeyStyle.ONE;
            //		keyStyleArray[6, 2] = KeyStyle.ONE;
            //		keyStyleArray[6, 3] = KeyStyle.ONE;
            //		keyStyleArray[6, 4] = KeyStyle.ONE;
            //		keyStyleArray[6, 5] = KeyStyle.ONE;
            //		keyStyleArray[6, 6] = KeyStyle.ONE;
            //		keyStyleArray[6, 7] = KeyStyle.ONE;
            //		keyStyleArray[6, 8] = KeyStyle.ONE;
            //		keyStyleArray[6, 9] = KeyStyle.ONE;
            //		keyStyleArray[6, 10] = KeyStyle.ONE;
            //		keyStyleArray[6, 11] = KeyStyle.ONE;
            //		keyStyleArray[6, 12] = KeyStyle.ONE;
            //		keyStyleArray[6, 13] = KeyStyle.ONE;
            //		keyStyleArray[6, 14] = KeyStyle.ONE;
            //		keyStyleArray[6, 15] = KeyStyle.ONE;
            //		keyStyleArray[6, 16] = KeyStyle.ONE;
            //		keyStyleArray[6, 17] = KeyStyle.ONE;
            //		keyStyleArray[6, 18] = KeyStyle.ONE;
            //		keyStyleArray[6, 19] = KeyStyle.ONE;
            //		keyStyleArray[6, 20] = KeyStyle.ONE;
            //		keyStyleArray[6, 21] = KeyStyle.ONE;
            //		keyStyleArray[6, 22] = KeyStyle.ONE;
            //		keyStyleArray[6, 23] = KeyStyle.ONE;
            //		keyStyleArray[6, 24] = KeyStyle.ONE;
            //		keyStyleArray[7, 0] = KeyStyle.ONE;
            //		keyStyleArray[7, 1] = KeyStyle.ONE;
            //		keyStyleArray[7, 2] = KeyStyle.ONE;
            //		keyStyleArray[7, 3] = KeyStyle.ONE;
            //		keyStyleArray[7, 4] = KeyStyle.ONE;
            //		keyStyleArray[7, 5] = KeyStyle.ONE;
            //		keyStyleArray[7, 6] = KeyStyle.ONE;
            //		keyStyleArray[7, 7] = KeyStyle.ONE;
            //		keyStyleArray[7, 8] = KeyStyle.ONE;
            //		keyStyleArray[7, 9] = KeyStyle.ONE;
            //		keyStyleArray[7, 10] = KeyStyle.ONE;
            //		keyStyleArray[7, 11] = KeyStyle.ONE;
            //		keyStyleArray[7, 12] = KeyStyle.ONE;
            //		keyStyleArray[7, 13] = KeyStyle.ONE;
            //		keyStyleArray[7, 14] = KeyStyle.ONE;
            //		keyStyleArray[7, 15] = KeyStyle.ONE;
            //		keyStyleArray[7, 16] = KeyStyle.ONE;
            //		keyStyleArray[7, 17] = KeyStyle.ONE;
            //		keyStyleArray[7, 18] = KeyStyle.ONE;
            //		keyStyleArray[7, 19] = KeyStyle.ONE;
            //		keyStyleArray[7, 20] = KeyStyle.ONE;
            //		keyStyleArray[7, 21] = KeyStyle.ONE;
            //		keyStyleArray[7, 22] = KeyStyle.ONE;
            //		keyStyleArray[7, 23] = KeyStyle.ONE;
            //		keyStyleArray[7, 24] = KeyStyle.ONE;
            //		keyStyleArray[8, 0] = KeyStyle.ONE;
            //		keyStyleArray[8, 1] = KeyStyle.ONE;
            //		keyStyleArray[8, 2] = KeyStyle.ONE;
            //		keyStyleArray[8, 3] = KeyStyle.ONE;
            //		keyStyleArray[8, 4] = KeyStyle.ONE;
            //		keyStyleArray[8, 5] = KeyStyle.ONE;
            //		keyStyleArray[8, 6] = KeyStyle.ONE;
            //		keyStyleArray[8, 7] = KeyStyle.ONE;
            //		keyStyleArray[8, 8] = KeyStyle.ONE;
            //		keyStyleArray[8, 9] = KeyStyle.ONE;
            //		keyStyleArray[8, 10] = KeyStyle.ONE;
            //		keyStyleArray[8, 11] = KeyStyle.ONE;
            //		keyStyleArray[8, 12] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[8, 13] = KeyStyle.TWO_IN_ONE;
            //		keyStyleArray[8, 14] = KeyStyle.ONE;
            //		keyStyleArray[8, 15] = KeyStyle.ONE;
            //		keyStyleArray[8, 16] = KeyStyle.ONE;
            //		keyStyleArray[8, 17] = KeyStyle.ONE;
            //		keyStyleArray[8, 18] = KeyStyle.ONE;
            //		keyStyleArray[8, 19] = KeyStyle.ONE;
            //		keyStyleArray[8, 20] = KeyStyle.ONE;
            //		keyStyleArray[8, 21] = KeyStyle.ONE;
            //		keyStyleArray[8, 22] = KeyStyle.ONE;
            //		keyStyleArray[8, 23] = KeyStyle.ONE;
            //		keyStyleArray[8, 24] = KeyStyle.ONE;
            //		CommonConstants.KeysDefaultStyles = keyStyleArray;
            //		string[] strArrays1 = new string[] { "Power up", "Start/Stop Time", "Key Task", "Logging with run time Frequency", "Bit task", "Event based" };
            //		CommonConstants.ListofLogMod = strArrays1;
            //		strArrays1 = new string[] { "Power up", "Start/Stop Time", "Bit task", "Event based" };
            //		CommonConstants.ListofLogModFL100 = strArrays1;
            //		strArrays1 = new string[] { "2 Byte (Int)", "4 Byte (Int)", "4 Byte (Float)", "2 Byte (SInt)", "4 Byte (SInt)" };
            //		CommonConstants.ListofLogModDataType = strArrays1;
            //		strArrays1 = new string[] { "2 Byte (Int)", "4 Byte (Int)", "2 Byte (SInt)", "4 Byte (SInt)", "4 Byte (Float)" };
            //		CommonConstants.ListofLogModDataType_New = strArrays1;
            //		CommonConstants.dsPLCInformation = new DataSet();
            //		CommonConstants.dsReadPLCSupportedModelList_Native = new DataSet();
            //		CommonConstants.dsReadPLCSupportedModelList_IEC = new DataSet();
            //		CommonConstants.WORDWIZARD_MAX_STATES = 32;
            //		CommonConstants.WORDWIZARD_MIN_STATES = 1;
            //		CommonConstants.HorizontalScreenScrollBarValue = 0;
            //		CommonConstants.VerticalScreenScrollBarValue = 0;
            //		CommonConstants.AlarmDateFormat = "";
            //		CommonConstants.AlarmTextStringDisplay = "";
            //		CommonConstants.AlarmAlarmNumberDisplay = "";
            //		CommonConstants.AlarmOtherDisplay = "";
            //		CommonConstants.PasswordScreenDisplay = false;
            //		CommonConstants.LanguageIdList = new List<CommonConstants.LanguageInformation>();
            //		CommonConstants.MaxLimitOfColorAnimation = 999;
            //		CommonConstants.IBMCommunication = "";
            //		CommonConstants.iKeyPadTouchGridWidth = 12;
            //		CommonConstants.iKeyPadTouchGridHeight = 16;
            //		CommonConstants.blSimulation = false;
            //		CommonConstants.blSaveForSimulation = false;
            //		CommonConstants.blLanguageChanged = false;
            //		CommonConstants.TTFontNameUsedForPrizmFont = "Courier New";
            //		CommonConstants.strImportTagStartCharacter = "$";
            //		CommonConstants.strImportNodeStartCharacter = "@";
            //		CommonConstants.strImportTagsFileStartCharacter = "#";
            //		CommonConstants.strImportTagsCSV = ",";
            //		CommonConstants.strImportTagsCSVSeperator = ",\"";
            //		CommonConstants.strImportTagsVersion = "Version";
            //		CommonConstants.strImportTagsDate = "Date";
            //		CommonConstants.strImportTagsTotalTags = "TotalTags";
            //		CommonConstants.strNewLineCharacter = "\n";
            //		CommonConstants.strImportNodeNamePresent = "";
            //		CommonConstants.strImportNodeAddAutoGenerated = "";
            //		CommonConstants.strImportNodeInvalidProtocol = "";
            //		CommonConstants.strImportNodeInvalidModel = "";
            //		CommonConstants.strImportNodeProtocolDefinedOnPort = "";
            //		CommonConstants.IBMComm = "";
            //		CommonConstants.G9SP_ProtocolName = "G9SP-N20S";
            //		CommonConstants.PrizmUnit = "";
            //		CommonConstants.Com1Com2 = "";
            //		CommonConstants.strImportNodePrizmUnitDefaultNode = "";
            //		CommonConstants.strImportTagErrWrongTagHeader = "";
            //		CommonConstants.strImportTagErrInvalidTagColumnCount = "";
            //		CommonConstants.strImportTagErrInvalidTagInformation = "";
            //		CommonConstants.strImportTagErrDuplicateTagAddress = "";
            //		CommonConstants.strImportTagErrDuplicateTagName = "";
            //		CommonConstants.strImportTagErrWrongTagAddress = "";
            //		CommonConstants.strImportTagErrWrongTagName = "";
            //		CommonConstants.strImportTagErrWrongTagType = "";
            //		CommonConstants.strImportTagErrWrongNoofBytes = "";
            //		CommonConstants.strImportTagErrWrongPrefix = "";
            //		CommonConstants.strImportTagErrWrongNodeName = "";
            //		CommonConstants.strImportTagErrWrongPortName = "";
            //		CommonConstants.strImportTagErrTagLimitReached = "";
            //		CommonConstants.strImportTagErrTagAddedWithAutoGeneratedTagName = "";
            //		CommonConstants.strImportTagErrTagCannotReplace = "";
            //		CommonConstants.strImportTagErrTagReplaced = "";
            //		CommonConstants.strExportLogFileName = "";
            //		CommonConstants.strStringNotSupport = "";
            //		CommonConstants.strTagMappingError = "";
            //		CommonConstants.strProjectType = "ProjectType";
            //		CommonConstants.lstNodeColInformation = new List<string>();
            //		CommonConstants.lstTagColInformation = new List<string>();
            //		CommonConstants.lstTagColInformation_Native = new List<string>();
            CommonConstants.strGroupName_Global = "(Global)";
            //		CommonConstants.strGroupName_Retain = "(Retain)";
            //		CommonConstants.strImportTagErrDupNativeTagAddress = "";
            //		CommonConstants.strImportTagErrDuplicateNativeAddress = "";
            //		CommonConstants.strStatustagname = "Status Tag : Com";
            //		CommonConstants.ApplicationPath = "";
            //		CommonConstants.strUserName = "";
            //		CommonConstants.strPassword = "";
            //		CommonConstants.strConfirmPW = "";
            //		CommonConstants.strHeaderText = "Web server v1.00";
            //		CommonConstants.bEnableWebserver = false;
            //		CommonConstants.bEnableWebserverHeader = false;
            //		CommonConstants.bEnableWebserverNavigation = false;
            //		CommonConstants.bEnableWebserverBorder = false;
            //		CommonConstants.WEB_SCREEN_WIDTH = 800;
            //		CommonConstants.WEB_SCREEN_HEIGHT = 600;
            //		CommonConstants.DefaultWebscreenNumber = 64000;
            //		CommonConstants._sequencialScreenMap = new Hashtable();
            //		CommonConstants._noOfTagsPerScreen = new Hashtable();
            //		CommonConstants._noOfTagsPerXML = new Hashtable();
            //		CommonConstants._totalImageCnt = 2;
            //		CommonConstants._totalXMLCnt = 3;
            //		CommonConstants._htmlWebscreenCnt = 11;
            //		CommonConstants._selectedTab = 0;
            //		CommonConstants.strDataRegister = "Data Register";
            //		CommonConstants.strRetentiveRegister = "Retentive Register";
            //		CommonConstants.strSystemRegister = "System Register";
            //		CommonConstants.strInternalRegister = "Internal Register";
            //		CommonConstants.strInputRegister = "Input Register";
            //		CommonConstants.strOutputRegister = "Output Register";
            //		CommonConstants.strTimmerRegister = "Timmer Register";
            //		CommonConstants.strCounterRegister = "Counter Register";
            //		CommonConstants.debugObject = new ClassList.Debug();
            //		CommonConstants.strLangSequenceErrMsg = "strLangSequenceErrMsg";
            //		CommonConstants.strIECTaskName = "";
            //		CommonConstants.strIECAlarmDataLogger = "";
            //		strArrays1 = new string[] { "AnalogTotalizer" };
            //		CommonConstants.strRenuUDFB = strArrays1;
            //		strArrays1 = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "?" };
            //		CommonConstants.arrDigits = strArrays1;
            //		strArrays1 = new string[] { "/", ".", ",", "-", ":" };
            //		CommonConstants.arrSpecialCharacters = strArrays1;
            //		strArrays1 = new string[] { "Y", "N", "YES", "NO" };
            //		CommonConstants.arrStrings = strArrays1;
            //		CommonConstants.strAlarmParamText = "";
            //		CommonConstants.iAlarmRectHeight = 0;
            //		CommonConstants.bSelShapeFont = 0;
            //		CommonConstants.ScreenNumberMsg = false;
            //		CommonConstants.FlashAnimLowRangeMsg = false;
            //		CommonConstants.FlashAnimHighRangeMsg = false;
            //		CommonConstants.ColorAnimLinColorMsg = false;
            //		CommonConstants.ColorAnimFillColorMsg = false;
            //		CommonConstants.ShowHideAnimLowRangeMsg = false;
            //		CommonConstants.ShowHideAnimHighRangeMsg = false;
            //		CommonConstants.SingleBarGraphMaximumRangeMsg = false;
            //		CommonConstants.SingleBarGraphMinimumRangeMsg = false;
            //		CommonConstants.MultiBarGraphMsg = false;
            //		CommonConstants.MultiBarGraphMinimumRangeMsg = false;
            //		CommonConstants.RepeatMsg = false;
            //		CommonConstants.NoImage = "NoImage";
            //		CommonConstants.NoImageDotBmp = "NoImage.bmp";
            //		CommonConstants.OnlyDotBmp = ".bmp";
            //		CommonConstants.OnlyDotBMP = ".BMP";
            //		CommonConstants.OnlyDotbmp = ".Bmp";
            //		CommonConstants.OnlyDotBmP = ".BmP";
            //		CommonConstants.OnlyDotBMp = ".BMp";
            //		CommonConstants.OnlyDotbMP = ".bMP";
            //		CommonConstants.OnlyDotbmP = ".bmP";
            //		CommonConstants.OnlyDotbMp = ".bMp";
            //		CommonConstants.UnderscoreTBmp = "_T.bmp";
            //		CommonConstants.UnderscoreT = "_T";
            //		CommonConstants.UnderscoreTPzp = "_T.pzp";
            //		CommonConstants.OnlyDotPzp = ".pzp";
            //		CommonConstants.UnderscoreTNoImage = "NoImage_T";
            //		CommonConstants.PictureFolder = "Picture";
            //		CommonConstants.Slash = "\\";
            //		CommonConstants.strAbbrevations = "Abbrevations:";
            //		CommonConstants.strAlarmType = "Alarm Type:";
            //		CommonConstants.strAlmType1 = "i) 0- 16 Consecutive Words: Each bit of each";
            //		CommonConstants.strAlmType2 = "word is";
            //		CommonConstants.strAlmType3 = "an alarm";
            //		CommonConstants.strAlmType4 = "ii)1- 16 Random Words: Each bit of each";
            //		CommonConstants.strAlmType5 = "word is";
            //		CommonConstants.strAlmType6 = "an alarm";
            //		CommonConstants.strAlmType7 = "iii)2- 256 Discrete Alarms: Each alarm is";
            //		CommonConstants.strAlmType8 = "either a bit";
            //		CommonConstants.strAlmType9 = "alarm [on/off]";
            //		CommonConstants.strAlmType10 = "or a word alarm";
            //		CommonConstants.strAlmMsg = "Alarm Action:";
            //		CommonConstants.strAlmMsg1 = "i) 0- Erase previous Alarms and starts logging from beginning";
            //		CommonConstants.strAlmMsg2 = "ii)1- Stop Logging";
            //		CommonConstants.strAlmMsg3 = "iii)2- Stop Logging and Display Error Message";
            //		CommonConstants.strHist = "History:";
            //		CommonConstants.strHist1 = "i)No- No History";
            //		CommonConstants.strHist2 = "ii)Yes_1- History With Acknowledge";
            //		CommonConstants.strHist3 = "iii)Yes_2- History Without Acknowledge";
            //		CommonConstants.strIsAlmAssign = "Alarm Assign";
            //		CommonConstants.strAlmType = "Alarm Type:";
            //		CommonConstants.strAlm = " for Alarm";
            //		CommonConstants.strErr = "Error";
            //		CommonConstants.strWarning = "Warning";
            //		CommonConstants.strFileExtErr = "File does not exist";
            //		CommonConstants.strFileOpenErr = "The file is used by another process,please close it.";
            //		CommonConstants.strAlmTxtLen = "Alarm Text is too long for Alarm No";
            //		CommonConstants.strTagBitPresent = "tag is not present in Tag Database";
            //		CommonConstants.strBitNum = "BitNumber should not be repeated";
            //		CommonConstants.strTagGrp = "GroupNumber is already present";
            //		CommonConstants.strIsAlmAssignMsg = "Alarm Assign Text should be in Yes/No formats";
            //		CommonConstants.strLogMsg = "Log Text should be in No, Yes_1 and Yes_2 formats";
            //		CommonConstants.strAlmSeverityMsg1 = "Severity should be 0 to 9 only";
            //		CommonConstants.strAlmSeverityMsg2 = "Severity should be a number";
            //		CommonConstants.strAlmPrintMsg = "Print Text should be either 0 or 1";
            //		CommonConstants.strAlmColBlankHeadingMsg = "Heading text should not be blank.";
            //		CommonConstants.strAlmCondOperatorMsg = "Conditional operator should be valid";
            //		CommonConstants.strAlmIDPresent = "Alarm ID should be Positive Integer value";
            //		CommonConstants.strAlmTypeMsg = "Current project Alarm Type, Alarm Action and Acknowledge type must be same as in import alarm database";
            //		CommonConstants.strAlmTypeMsg1 = "Current project Alarm Type is not same as in import alarm database";
            //		CommonConstants.strAlmAckMsg = "Current project Alarm Acknowledge Type is not same as in import alarm database";
            //		CommonConstants.strAlmActionMsg = "Current project Alarm Action Type is not same as in import alarm database";
            //		CommonConstants.strAlmColHeadingMsg = "Heading text should not change for";
            //		CommonConstants.strLangErrMsg = "Please make sure languages in current project are same as in import alarm database";
            //		CommonConstants.strAlmTypeTxt = "Text Alarm Type is not found";
            //		CommonConstants.strTagPresent = "Tag is not present in Tag Database";
            //		CommonConstants.strTagMsg = "Acknowledge tag can not be null";
            //		CommonConstants.strExpFileMsg = "No such record is found in";
            //		CommonConstants.strFileErrFormat = "Invalid file format...!";
            //		CommonConstants.strFileErr = "No Such File Exists";
            //		CommonConstants.strNull = "NULL";
            //		CommonConstants.strTitle = "Title:";
            //		CommonConstants.strGlInfo = "Global Information";
            //		CommonConstants.strAlmScanTime = "Alarm Scan Time[0-5000ms]:";
            //		CommonConstants.strAlmActionTxt = "Alarm Action if Memory is Full:";
            //		CommonConstants.strAlmErrMsg = "Alarm Error Message:";
            //		CommonConstants.strAutoAck = "Alarm Auto Ack:";
            //		CommonConstants.strAlmProp = "Alarm Properties";
            //		CommonConstants.SequenceIDs = "SequenceIDs";
            //		CommonConstants.RunTime_LineProperty = false;
            //		CommonConstants.SortType = PropertySort.CategorizedAlphabetical;
            //		CommonConstants.dblCote = '\"';
            //		CommonConstants.AlarmFontIndex = 0;
            //		CommonConstants.ScrNumber = "Screen Number";
            //		CommonConstants.Obj_ImgText = "Object Text/Image Text";
            //		CommonConstants.Co_ordinate = "Co-ordinate";
            //		CommonConstants.Task_Name = "Task Name";
            //		CommonConstants._commConstantPrizmVersion = 0;
            //		CommonConstants._commconstantarrFormObject = new ArrayList();
            //		CommonConstants._constarrPrizmPatternValues = new ArrayList();
            //		CommonConstants._constarrBaudRates = new ArrayList();
            //		CommonConstants._commConstantblRecalBlockSize = false;
            //		CommonConstants._commConstantModbusSlavePlcCode = 39;
            //		CommonConstants._commConstantPrizmPlcCode = 0;
            //		CommonConstants._CommConstantsProjHMILadderType = "";
            //		CommonConstants.strImportLogFileName = "";
            //		CommonConstants._NewProjectFolderName = "";
            //		CommonConstants._ProjectFolderName = "";
            //		CommonConstants._PreviousFolderName = "";
            //		CommonConstants._TEMPPreviousFolderName = "";
            //		CommonConstants._IECFoldername = "IEC";
            //		CommonConstants.Col16Supported = new byte[,] { { 0, 0, 0 }, { 16, 16, 16 }, { 32, 32, 32 }, { 48, 48, 48 }, { 64, 64, 64 }, { 80, 80, 80 }, { 96, 96, 96 }, { 112, 112, 112 }, { 128, 128, 128 }, { 144, 144, 144 }, { 160, 160, 160 }, { 176, 176, 176 }, { 192, 192, 192 }, { 208, 208, 208 }, { 224, 224, 224 }, { 240, 240, 240 } };
            //		CommonConstants.Col256Supported = new byte[,] { { 0, 0, 0 }, { 48, 0, 0 }, { 48, 48, 48 }, { 0, 16, 16 }, { 32, 32, 32 }, { 16, 16, 16 }, { 0, 48, 0 }, { 48, 48, 0 }, { 0, 48, 48 }, { 64, 64, 64 }, { 80, 80, 80 }, { 96, 96, 96 }, { 104, 104, 104 }, { 112, 112, 112 }, { 128, 128, 128 }, { 144, 144, 144 }, { 160, 160, 160 }, { 168, 168, 168 }, { 176, 176, 176 }, { 192, 192, 192 }, { 200, 200, 200 }, { 208, 208, 208 }, { 216, 216, 216 }, { 224, 224, 224 }, { 232, 232, 232 }, { 240, 240, 240 }, { 255, 255, 255 }, { 96, 48, 48 }, { 96, 48, 0 }, { 96, 0, 0 }, { 96, 0, 96 }, { 96, 48, 96 }, { 160, 0, 96 }, { 160, 48, 96 }, { 160, 0, 32 }, { 160, 48, 48 }, { 160, 48, 0 }, { 160, 0, 0 }, { 160, 96, 96 }, { 160, 96, 48 }, { 160, 96, 0 }, { 208, 0, 96 }, { 208, 48, 96 }, { 208, 96, 96 }, { 208, 96, 0 }, { 208, 96, 48 }, { 208, 48, 48 }, { 208, 48, 0 }, { 208, 0, 0 }, { 240, 96, 96 }, { 240, 0, 112 }, { 240, 48, 96 }, { 240, 0, 96 }, { 240, 96, 48 }, { 240, 96, 0 }, { 240, 16, 32 }, { 240, 80, 80 }, { 240, 0, 0 }, { 240, 128, 128 }, { 240, 160, 160 }, { 240, 160, 168 }, { 240, 160, 208 }, { 240, 96, 208 }, { 240, 96, 240 }, { 224, 16, 208 }, { 232, 16, 216 }, { 240, 0, 176 }, { 240, 0, 160 }, { 240, 48, 160 }, { 240, 96, 160 }, { 208, 0, 144 }, { 208, 0, 160 }, { 208, 48, 160 }, { 208, 96, 160 }, { 240, 0, 208 }, { 208, 96, 208 }, { 208, 0, 208 }, { 208, 24, 208 }, { 208, 48, 208 }, { 240, 0, 240 }, { 240, 48, 240 }, { 240, 160, 240 }, { 240, 208, 240 }, { 208, 160, 96 }, { 208, 160, 48 }, { 208, 160, 0 }, { 208, 160, 128 }, { 208, 160, 160 }, { 240, 208, 160 }, { 240, 208, 176 }, { 240, 208, 192 }, { 240, 208, 208 }, { 208, 160, 208 }, { 176, 160, 144 }, { 192, 184, 152 }, { 208, 208, 160 }, { 224, 224, 176 }, { 232, 232, 192 }, { 240, 240, 208 }, { 96, 96, 0 }, { 96, 96, 48 }, { 0, 96, 0 }, { 48, 96, 0 }, { 0, 96, 48 }, { 48, 96, 48 }, { 0, 128, 0 }, { 96, 160, 48 }, { 96, 160, 96 }, { 48, 160, 96 }, { 0, 160, 96 }, { 0, 160, 48 }, { 48, 160, 48 }, { 48, 160, 0 }, { 0, 160, 0 }, { 48, 208, 0 }, { 0, 208, 0 }, { 96, 208, 0 }, { 96, 208, 96 }, { 64, 208, 64 }, { 0, 208, 48 }, { 48, 208, 48 }, { 96, 208, 48 }, { 0, 208, 96 }, { 48, 208, 96 }, { 0, 240, 0 }, { 96, 240, 96 }, { 48, 240, 48 }, { 48, 240, 96 }, { 0, 240, 96 }, { 96, 240, 48 }, { 96, 240, 0 }, { 160, 240, 0 }, { 160, 240, 48 }, { 160, 240, 96 }, { 192, 224, 192 }, { 176, 224, 192 }, { 176, 232, 192 }, { 208, 240, 208 }, { 208, 240, 0 }, { 208, 240, 48 }, { 208, 240, 96 }, { 240, 208, 96 }, { 240, 208, 48 }, { 240, 208, 0 }, { 128, 128, 0 }, { 160, 160, 48 }, { 160, 160, 64 }, { 160, 160, 96 }, { 176, 192, 0 }, { 208, 208, 48 }, { 208, 208, 96 }, { 176, 208, 48 }, { 168, 192, 96 }, { 160, 208, 96 }, { 160, 208, 0 }, { 192, 176, 96 }, { 240, 160, 96 }, { 240, 160, 48 }, { 240, 160, 0 }, { 240, 192, 0 }, { 240, 240, 0 }, { 240, 240, 96 }, { 240, 240, 48 }, { 224, 224, 0 }, { 160, 240, 160 }, { 0, 240, 160 }, { 48, 240, 160 }, { 96, 240, 160 }, { 48, 224, 160 }, { 48, 192, 160 }, { 48, 208, 160 }, { 0, 208, 160 }, { 0, 216, 168 }, { 48, 216, 168 }, { 96, 208, 160 }, { 176, 176, 144 }, { 48, 96, 96 }, { 0, 96, 96 }, { 0, 128, 128 }, { 0, 160, 160 }, { 96, 160, 160 }, { 48, 240, 208 }, { 0, 240, 208 }, { 160, 240, 208 }, { 0, 208, 208 }, { 48, 208, 208 }, { 96, 208, 208 }, { 0, 192, 208 }, { 160, 240, 240 }, { 96, 240, 240 }, { 0, 240, 240 }, { 48, 240, 240 }, { 208, 240, 240 }, { 192, 240, 240 }, { 176, 240, 240 }, { 160, 208, 240 }, { 184, 208, 240 }, { 208, 208, 240 }, { 160, 160, 208 }, { 208, 0, 240 }, { 208, 160, 240 }, { 160, 48, 208 }, { 160, 0, 208 }, { 160, 96, 208 }, { 208, 96, 240 }, { 208, 48, 240 }, { 176, 80, 192 }, { 160, 96, 160 }, { 160, 0, 160 }, { 160, 48, 160 }, { 128, 0, 128 }, { 160, 48, 240 }, { 160, 0, 240 }, { 160, 96, 240 }, { 96, 0, 160 }, { 96, 48, 160 }, { 48, 0, 96 }, { 0, 0, 96 }, { 0, 48, 96 }, { 48, 48, 96 }, { 48, 48, 112 }, { 0, 0, 112 }, { 0, 0, 128 }, { 160, 160, 240 }, { 48, 48, 160 }, { 0, 0, 160 }, { 0, 48, 160 }, { 48, 0, 160 }, { 48, 96, 160 }, { 96, 96, 160 }, { 0, 96, 208 }, { 48, 96, 208 }, { 0, 0, 208 }, { 96, 48, 208 }, { 48, 48, 208 }, { 0, 48, 208 }, { 0, 0, 240 }, { 96, 0, 240 }, { 96, 96, 240 }, { 0, 96, 240 }, { 96, 48, 240 }, { 48, 48, 240 }, { 48, 96, 240 }, { 96, 96, 208 }, { 96, 160, 240 }, { 48, 160, 240 }, { 0, 160, 240 }, { 48, 160, 224 }, { 96, 160, 224 }, { 0, 160, 208 }, { 96, 160, 208 }, { 48, 160, 208 }, { 0, 192, 224 }, { 0, 208, 240 }, { 48, 208, 240 }, { 96, 208, 240 } };
            //		CommonConstants.Col2Supported = new byte[,] { { 0, 0, 0 }, { 255, 255, 255 } };
        }

        //	public CommonConstants()
        //	{
        //	}

        //	public static int BinaryToDecimal(string binary)
        //	{
        //		return (int)Convert.ToInt64(binary, 2);
        //	}

        //	public static void BREAKINT(byte[] pTemparr, int pProperty)
        //	{
        //		pTemparr[3] = (byte)(((long)pProperty & (ulong)-16777216) >> 24);
        //		pTemparr[2] = (byte)((pProperty & 16711680) >> 16);
        //		pTemparr[1] = (byte)((pProperty & 65280) >> 8);
        //		pTemparr[0] = (byte)(pProperty & 255);
        //	}

        //	public static void BREAKUINT(byte[] pTemparr, uint pProperty)
        //	{
        //		pTemparr[3] = (byte)((pProperty & -16777216) >> 24);
        //		pTemparr[2] = (byte)((pProperty & 16711680) >> 16);
        //		pTemparr[1] = (byte)((pProperty & 65280) >> 8);
        //		pTemparr[0] = (byte)(pProperty & 255);
        //	}

        //	public static void BREAKWORD(byte[] pTemparr, short pProperty)
        //	{
        //		pTemparr[0] = Convert.ToByte(pProperty & 255);
        //		int num = pProperty >> 8;
        //		pTemparr[1] = Convert.ToByte(num & 255);
        //	}

        //	public static void BREAKWORD(byte[] pTemparr, ushort pProperty)
        //	{
        //		pTemparr[0] = Convert.ToByte((uint)(pProperty & 255));
        //		uint num = pProperty >> 8;
        //		pTemparr[1] = Convert.ToByte(num & 255);
        //	}

        //	public static void BREAKWORD(byte[] pTemparr, uint pProperty)
        //	{
        //		pTemparr[0] = Convert.ToByte((int)(pProperty & 255));
        //		int num = (int)(pProperty >> 8);
        //		pTemparr[1] = Convert.ToByte(num & 255);
        //	}

        //	public static void checkVPSerialPort(ref string V_PortName1, ref string V_PortName2)
        //	{
        //		string empty = string.Empty;
        //		empty = string.Concat(Directory.GetCurrentDirectory(), "\\setupc1.txt");
        //		if (File.Exists(empty))
        //		{
        //			StreamReader streamReader = File.OpenText(empty);
        //			try
        //			{
        //				if (streamReader.ReadLine().ToString() == "1")
        //				{
        //					if (File.Exists("VPCOMPORT.txt"))
        //					{
        //						StreamReader streamReader1 = new StreamReader("VPCOMPORT.txt");
        //						V_PortName1 = streamReader1.ReadLine();
        //						V_PortName2 = streamReader1.ReadLine();
        //						streamReader1.Close();
        //					}
        //					streamReader.Close();
        //				}
        //			}
        //			finally
        //			{
        //				if (streamReader != null)
        //				{
        //					((IDisposable)streamReader).Dispose();
        //				}
        //			}
        //		}
        //	}

        //	public static string Conversion_GetTagNameFromTagID(int TagID)
        //	{
        //		string tagName = "";
        //		int num = 0;
        //		while (num < CommonConstants.List_DelTagInfo.Count)
        //		{
        //			if (((CommonConstants.DelTagInfo)CommonConstants.List_DelTagInfo[num]).TagID != TagID)
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				tagName = ((CommonConstants.DelTagInfo)CommonConstants.List_DelTagInfo[num]).TagName;
        //				break;
        //			}
        //		}
        //		return tagName;
        //	}

        //	public static void ConversionLog(string msgs)
        //	{
        //		StreamWriter streamWriter = null;
        //		try
        //		{
        //			try
        //			{
        //				streamWriter = new StreamWriter(CommonConstants.ProjectConversionPath, true);
        //				streamWriter.WriteLine(msgs);
        //			}
        //			catch (Exception exception)
        //			{
        //				ExceptionLogger.DisplayError("Error occured in logging product conversion", "Error");
        //			}
        //		}
        //		finally
        //		{
        //			streamWriter.Close();
        //			streamWriter.Dispose();
        //		}
        //	}

        //	public static string convert_from_BinToDec(string str)
        //	{
        //		return Convert.ToInt64(str, 2).ToString();
        //	}

        //	public static string convert_From_DecToHex(ushort pushort)
        //	{
        //		return Conversion.Hex(pushort);
        //	}

        //	public static string convert_From_HexToDec(string str)
        //	{
        //		return Convert.ToInt64(str, 16).ToString();
        //	}

        //	public static string Convert_To_BinFromDec(string pDecimal)
        //	{
        //		string str;
        //		int num;
        //		int num1 = Convert.ToInt32(pDecimal);
        //		string str1 = "";
        //		if (num1 != 0)
        //		{
        //			int num2 = 0;
        //			while (num1 > 0)
        //			{
        //				if ((num1 | 1) != num1)
        //				{
        //					num = 0;
        //					str1 = string.Concat(num.ToString(), str1);
        //				}
        //				else
        //				{
        //					num = 1;
        //					str1 = string.Concat(num.ToString(), str1);
        //				}
        //				num1 >>= 1;
        //				num2++;
        //			}
        //			str = str1;
        //		}
        //		else
        //		{
        //			str = "0";
        //		}
        //		return str;
        //	}

        //	public static void ConvertFontToLogFont(System.Drawing.Font pFont, CommonConstants.LOGFONT pLogFont, ref CommonConstants.FontInfo pFontInfo)
        //	{
        //		pFont.ToLogFont(pLogFont);
        //		pFontInfo._fFontHeight = pLogFont.lfHeight;
        //		pFontInfo._fFontWidth = Convert.ToInt32(pFont.Size);
        //		pFontInfo._fEscapement = pLogFont.lfEscapement;
        //		pFontInfo._fOrientation = pLogFont.lfOrientation;
        //		pFontInfo._fWeight = pLogFont.lfWeight;
        //		pFontInfo._fItalic = pLogFont.lfItalic;
        //		pFontInfo._fUnderline = pLogFont.lfUnderline;
        //		pFontInfo._fStrikeOut = pLogFont.lfStrikeOut;
        //		pFontInfo._fCharSet = pLogFont.lfCharSet;
        //		pFontInfo._fOutPrecision = pLogFont.lfOutPrecision;
        //		pFontInfo._fClipPrecision = pLogFont.lfClipPrecision;
        //		pFontInfo._fQuality = pLogFont.lfQuality;
        //		pFontInfo._fPitchFamily = pLogFont.lfPitchAndFamily;
        //		pFontInfo._fLenOfFaceName = Convert.ToByte(pLogFont.lfFaceName.Length);
        //	}

        //	public static System.Drawing.Font ConvertLogFontToFont(CommonConstants.LOGFONT pLOGFONT, ref CommonConstants.FontInfo pFontInfo, short[] pCharOfFaceName)
        //	{
        //		System.Drawing.Font font;
        //		System.Drawing.Font font1;
        //		pLOGFONT.lfHeight = pFontInfo._fFontHeight;
        //		pLOGFONT.lfWidth = pFontInfo._fFontWidth;
        //		pLOGFONT.lfEscapement = pFontInfo._fEscapement;
        //		pLOGFONT.lfOrientation = pFontInfo._fOrientation;
        //		pLOGFONT.lfWeight = pFontInfo._fWeight;
        //		pLOGFONT.lfItalic = pFontInfo._fItalic;
        //		pLOGFONT.lfUnderline = pFontInfo._fUnderline;
        //		pLOGFONT.lfStrikeOut = pFontInfo._fStrikeOut;
        //		pLOGFONT.lfCharSet = Convert.ToByte(pFontInfo._fCharSet);
        //		pLOGFONT.lfOutPrecision = pFontInfo._fOutPrecision;
        //		pLOGFONT.lfClipPrecision = pFontInfo._fClipPrecision;
        //		pLOGFONT.lfQuality = pFontInfo._fQuality;
        //		pLOGFONT.lfPitchAndFamily = pFontInfo._fPitchFamily;
        //		pLOGFONT.lfFaceName = CommonConstants.ShortArrayToString(pCharOfFaceName);
        //		if (CommonConstants.ProductDataInfo.btPrizmVersion != 10)
        //		{
        //			try
        //			{
        //				font = System.Drawing.Font.FromLogFont(pLOGFONT);
        //			}
        //			catch (Exception exception)
        //			{
        //				pLOGFONT.lfFaceName = CommonConstants.TTFontNameUsedForPrizmFont;
        //				font = System.Drawing.Font.FromLogFont(pLOGFONT);
        //			}
        //			if (pFontInfo._fFontWidth <= 0)
        //			{
        //				pFontInfo._fFontWidth = Convert.ToInt32(font.Size);
        //			}
        //			System.Drawing.Font font2 = new System.Drawing.Font(font.FontFamily, (float)pFontInfo._fFontWidth, font.Style);
        //			font = font2;
        //			font1 = font2;
        //		}
        //		else
        //		{
        //			if (pLOGFONT.lfFaceName.Contains("prizm"))
        //			{
        //				pLOGFONT.lfFaceName = "Arial";
        //			}
        //			font = System.Drawing.Font.FromHfont(CommonConstants.CreateFontIndirect(pLOGFONT));
        //			if (font.Size - font.Size / 4f > 50f)
        //			{
        //				System.Drawing.Font font3 = new System.Drawing.Font(font.FontFamily, font.Size / 10f - font.Size / 10f / 4f, font.Style);
        //				font = font3;
        //				font1 = font3;
        //			}
        //			else
        //			{
        //				System.Drawing.Font font4 = new System.Drawing.Font(font.FontFamily, font.Size - font.Size / 4f, font.Style);
        //				font = font4;
        //				font1 = font4;
        //			}
        //		}
        //		return font1;
        //	}

        //	public static System.Drawing.Font ConvertPrizmLogFontToFPFont(CommonConstants.LOGFONT pLOGFONT, ref CommonConstants.FontInfo pFontInfo, short[] pCharOfFaceName)
        //	{
        //		pLOGFONT.lfHeight = pFontInfo._fFontHeight;
        //		pLOGFONT.lfWidth = pFontInfo._fFontWidth;
        //		pLOGFONT.lfEscapement = pFontInfo._fEscapement;
        //		pLOGFONT.lfOrientation = pFontInfo._fOrientation;
        //		pLOGFONT.lfWeight = pFontInfo._fWeight;
        //		pLOGFONT.lfItalic = pFontInfo._fItalic;
        //		pLOGFONT.lfUnderline = pFontInfo._fUnderline;
        //		pLOGFONT.lfStrikeOut = pFontInfo._fStrikeOut;
        //		pLOGFONT.lfCharSet = Convert.ToByte(pFontInfo._fCharSet);
        //		pLOGFONT.lfOutPrecision = pFontInfo._fOutPrecision;
        //		pLOGFONT.lfClipPrecision = pFontInfo._fClipPrecision;
        //		pLOGFONT.lfQuality = pFontInfo._fQuality;
        //		pLOGFONT.lfPitchAndFamily = pFontInfo._fPitchFamily;
        //		pLOGFONT.lfFaceName = CommonConstants.ShortArrayToString(pCharOfFaceName);
        //		if (pLOGFONT.lfFaceName.Contains("prizm"))
        //		{
        //			pLOGFONT.lfFaceName = "Arial";
        //		}
        //		System.Drawing.Font font = System.Drawing.Font.FromHfont(CommonConstants.CreateFontIndirect(pLOGFONT));
        //		font = new System.Drawing.Font(font.FontFamily, font.SizeInPoints, font.Style);
        //		if (pFontInfo._fFontWidth <= 0)
        //		{
        //			pFontInfo._fFontWidth = Convert.ToInt32(font.Size);
        //		}
        //		font.ToLogFont(pLOGFONT);
        //		pFontInfo._fFontHeight = pLOGFONT.lfHeight;
        //		pFontInfo._fFontWidth = Convert.ToInt32(font.Size);
        //		pFontInfo._fEscapement = pLOGFONT.lfEscapement;
        //		pFontInfo._fOrientation = pLOGFONT.lfOrientation;
        //		pFontInfo._fWeight = pLOGFONT.lfWeight;
        //		pFontInfo._fItalic = pLOGFONT.lfItalic;
        //		pFontInfo._fUnderline = pLOGFONT.lfUnderline;
        //		pFontInfo._fStrikeOut = pLOGFONT.lfStrikeOut;
        //		pFontInfo._fCharSet = pLOGFONT.lfCharSet;
        //		pFontInfo._fOutPrecision = pLOGFONT.lfOutPrecision;
        //		pFontInfo._fClipPrecision = pLOGFONT.lfClipPrecision;
        //		pFontInfo._fQuality = pLOGFONT.lfQuality;
        //		pFontInfo._fPitchFamily = pLOGFONT.lfPitchAndFamily;
        //		pFontInfo._fLenOfFaceName = Convert.ToByte(pLOGFONT.lfFaceName.Length);
        //		return font;
        //	}

        //	public static float ConvertStringToFloat(string strFont)
        //	{
        //		float single = 0f;
        //		if (strFont != null)
        //		{
        //			if (strFont.Length > 0)
        //			{
        //				if (strFont.Contains(","))
        //				{
        //					strFont = strFont.Replace(",", ".");
        //				}
        //			}
        //		}
        //		single = Convert.ToSingle(strFont, CultureInfo.InvariantCulture);
        //		if ((double)single == 825)
        //		{
        //			single = 8.25f;
        //		}
        //		else if ((double)single == 975)
        //		{
        //			single = 9.75f;
        //		}
        //		else if ((double)single == 1125)
        //		{
        //			single = 11.25f;
        //		}
        //		else if ((double)single == 1425)
        //		{
        //			single = 14.25f;
        //		}
        //		else if ((double)single == 1575)
        //		{
        //			single = 15.75f;
        //		}
        //		else if ((double)single == 2025)
        //		{
        //			single = 20.25f;
        //		}
        //		else if ((double)single == 2175)
        //		{
        //			single = 21.75f;
        //		}
        //		else if ((double)single == 2625)
        //		{
        //			single = 26.25f;
        //		}
        //		else if ((double)single == 2775)
        //		{
        //			single = 27.75f;
        //		}
        //		return single;
        //	}

        //	public static uint ConvertToBCD(uint Num, uint Divider, uint Factor)
        //	{
        //		uint num = 0;
        //		uint num1 = 0;
        //		uint bCD = 0;
        //		num = Num % Divider;
        //		num1 = Num / Divider;
        //		if ((num1 != 0 ? true : num != 0))
        //		{
        //			bCD = bCD + CommonConstants.ConvertToBCD(num1, Divider, Factor) * Factor + num;
        //		}
        //		return bCD;
        //	}

        //	public static CommonConstants.G9SPNodeInfo copyComNodeStructureToFins(CommonConstants.NodeInfo objNode)
        //	{
        //		CommonConstants.G9SPNodeInfo g9SPNodeInfo = new CommonConstants.G9SPNodeInfo()
        //		{
        //			_iNodeId = objNode._iNodeId,
        //			_strName = objNode._strName,
        //			_usAddress = objNode._usAddress,
        //			_btType = objNode._btType,
        //			_btPort = 3,
        //			_strProtocol = objNode._strProtocol,
        //			_strModel = objNode._strModel,
        //			_btHasTag = objNode._btHasTag,
        //			_strPortName = Port.Ethernet.ToString(),
        //			_btPLCCode = 177,
        //			_btPLCModel = 1,
        //			_btRegLength = objNode._btRegLength,
        //			_btSpecialData1 = objNode._btSpecialData1,
        //			_btSpecialData2 = objNode._btSpecialData2,
        //			_btSpecialData3 = objNode._btSpecialData3,
        //			_usTotalBlocks = objNode._usTotalBlocks,
        //			_uiEthernetIpAddress = objNode._uiEthernetIpAddress,
        //			_usEthernetPortNumber = 9600,
        //			_usEthernetScanTime = objNode._usEthernetScanTime,
        //			_usEthernetResponseTimeOut = objNode._usEthernetResponseTimeOut,
        //			_btBaudRate = objNode._btBaudRate,
        //			_btParity = objNode._btParity,
        //			_btDataBits = objNode._btDataBits,
        //			_btStopBits = objNode._btStopBits,
        //			_btRetryCount = 3,
        //			_usInterframeDelay = objNode._usInterframeDelay,
        //			_usResponseTime = objNode._usResponseTime,
        //			_btInterByteDelay = objNode._btInterByteDelay,
        //			_btFloatFormat = objNode._btFloatFormat,
        //			_btIntFormat = objNode._btIntFormat,
        //			_btExpansionType = objNode._btExpansionType
        //		};
        //		g9SPNodeInfo._usEthernetScanTime = objNode._usInterframeDelay;
        //		g9SPNodeInfo._usEthernetResponseTimeOut = objNode._usResponseTime;
        //		g9SPNodeInfo._btG9SPSrcNetwork = 0;
        //		g9SPNodeInfo._btG9SPSrcNode = 100;
        //		g9SPNodeInfo._btG9SPSrcID = 0;
        //		g9SPNodeInfo._btG9SPDestNetwork = 0;
        //		g9SPNodeInfo._btG9SPDestNode = 0;
        //		g9SPNodeInfo._btG9SPDestID = 0;
        //		g9SPNodeInfo._btReserved = 0;
        //		return g9SPNodeInfo;
        //	}

        //	public static void CopyLoggDataAndHistoricalAlarmFilesFromUSBDrive(int pProductID, ProjectInfo pProjectInfo)
        //	{
        //		string filePath = pProjectInfo.FilePath;
        //		string str = "HISTALARM.BIN";
        //		string str1 = "LOGGER_DATA.BIN";
        //		if (File.Exists(string.Concat(filePath, "\\", str)))
        //		{
        //			File.Copy(string.Concat(filePath, "\\", str), "HistAlarmData.BIN", true);
        //		}
        //		if (File.Exists(string.Concat(filePath, "\\", str1)))
        //		{
        //			File.Copy(string.Concat(filePath, "\\", str1), "Logger.BIN", true);
        //		}
        //	}

        //	[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
        //	private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        //	[DllImport("gdi32.dll", CharSet=CharSet.Auto, ExactSpelling=false)]
        //	public static extern IntPtr CreateFontIndirect([In] CommonConstants.LOGFONT lplf);

        //	public static Graphics CreateMemoryDC(int pTLX, int pTLY, int pBRX, int pBRY)
        //	{
        //		unsafe
        //		{
        //			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(pBRX - pTLX + 2, pBRY - pTLY + 1);
        //			IntPtr intPtr = CommonConstants.CreateCompatibleDC((IntPtr)0);
        //			CommonConstants.SelectObject(intPtr, bitmap.GetHbitmap());
        //			return Graphics.FromHdc(intPtr);
        //		}
        //	}

        //	public static string DecimalToBinary(ushort pNumber)
        //	{
        //		string str = "";
        //		while (pNumber > 0)
        //		{
        //			ushort num = Convert.ToUInt16(pNumber % 2);
        //			str = string.Concat(str, Convert.ToUInt16(num));
        //			pNumber = Convert.ToUInt16(pNumber / 2);
        //		}
        //		char[] charArray = str.ToCharArray();
        //		Array.Reverse(charArray);
        //		str = new string(charArray);
        //		return str;
        //	}

        //	public static string DecimalToBinary(uint pNumber)
        //	{
        //		string str = "";
        //		while (pNumber != 0)
        //		{
        //			uint num = Convert.ToUInt32(pNumber % 2);
        //			str = string.Concat(str, Convert.ToUInt32(num));
        //			pNumber = Convert.ToUInt32(pNumber / 2);
        //		}
        //		char[] charArray = str.ToCharArray();
        //		Array.Reverse(charArray);
        //		str = new string(charArray);
        //		return str;
        //	}

        //	public static string Decrypt(string encrypData)
        //	{
        //		string str = "Pas5pr@se";
        //		string str1 = "s@1tValue";
        //		string str2 = "SHA1";
        //		int num = 2;
        //		string str3 = "Renu Electronics";
        //		int num1 = 256;
        //		string str4 = "";
        //		try
        //		{
        //			string str5 = encrypData;
        //			byte[] bytes = Encoding.ASCII.GetBytes(str3);
        //			byte[] numArray = Encoding.ASCII.GetBytes(str1);
        //			byte[] numArray1 = Convert.FromBase64String(str5);
        //			PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(str, numArray, str2, num);
        //			byte[] bytes1 = passwordDeriveByte.GetBytes(num1 / 8);
        //			RijndaelManaged rijndaelManaged = new RijndaelManaged()
        //			{
        //				Mode = CipherMode.CBC
        //			};
        //			ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(bytes1, bytes);
        //			MemoryStream memoryStream = new MemoryStream(numArray1);
        //			CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);
        //			byte[] numArray2 = new byte[(int)numArray1.Length];
        //			int num2 = cryptoStream.Read(numArray2, 0, (int)numArray2.Length);
        //			memoryStream.Close();
        //			cryptoStream.Close();
        //			str4 = Encoding.UTF8.GetString(numArray2, 0, num2);
        //		}
        //		catch (Exception exception)
        //		{
        //		}
        //		return str4;
        //	}

        //	public static string Encrypt(string decrypData)
        //	{
        //		string str = "";
        //		string str1 = "Pas5pr@se";
        //		string str2 = "s@1tValue";
        //		string str3 = "SHA1";
        //		int num = 2;
        //		string str4 = "Renu Electronics";
        //		int num1 = 256;
        //		str = decrypData;
        //		byte[] bytes = Encoding.ASCII.GetBytes(str4);
        //		byte[] numArray = Encoding.ASCII.GetBytes(str2);
        //		byte[] bytes1 = Encoding.UTF8.GetBytes(str);
        //		PasswordDeriveBytes passwordDeriveByte = new PasswordDeriveBytes(str1, numArray, str3, num);
        //		byte[] numArray1 = passwordDeriveByte.GetBytes(num1 / 8);
        //		RijndaelManaged rijndaelManaged = new RijndaelManaged()
        //		{
        //			Mode = CipherMode.CBC
        //		};
        //		ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(numArray1, bytes);
        //		MemoryStream memoryStream = new MemoryStream();
        //		CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
        //		cryptoStream.Write(bytes1, 0, (int)bytes1.Length);
        //		cryptoStream.FlushFinalBlock();
        //		byte[] array = memoryStream.ToArray();
        //		memoryStream.Close();
        //		cryptoStream.Close();
        //		return Convert.ToBase64String(array);
        //	}

        //	public static string FormatedDecimalToBinary(ushort pNumber)
        //	{
        //		string str = "";
        //		int length = 0;
        //		while (pNumber > 0)
        //		{
        //			ushort num = Convert.ToUInt16(pNumber % 2);
        //			str = string.Concat(str, Convert.ToUInt16(num));
        //			pNumber = Convert.ToUInt16(pNumber / 2);
        //		}
        //		char[] charArray = str.ToCharArray();
        //		Array.Reverse(charArray);
        //		str = new string(charArray);
        //		length = 8 - str.Length;
        //		string str1 = "";
        //		for (int i = 0; i < length; i++)
        //		{
        //			str1 = string.Concat(str1, '0');
        //		}
        //		str = string.Concat(str1, str);
        //		return str;
        //	}

        //	public static bool FP4030MT_L0808P_A0201UApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				{
        //					break;
        //				}
        //				case 1217:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808P_A0402LApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				{
        //					break;
        //				}
        //				case 1217:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808PApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				{
        //					break;
        //				}
        //				case 1217:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RN_A0201UApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RN_A0201UVerticalApplicationConversion(int ProductID)
        //	{
        //		bool flag = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 1222:
        //			case 1223:
        //			{
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				if (productID == 1227)
        //				{
        //					goto case 1223;
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RNApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				case 1219:
        //				case 1220:
        //				{
        //					break;
        //				}
        //				case 1217:
        //				case 1218:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RNVerticalApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		switch (ProductID)
        //		{
        //			case 1222:
        //			case 1223:
        //			case 1224:
        //			case 1225:
        //			case 1227:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 1226:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //	}

        //	public static bool FP4030MT_L0808RP_A0201LApplicationConversion(int ProductID)
        //	{
        //		bool flag = false;
        //		if (ProductID == 1301)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RP_A0201UApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RP_A0201UVerticalApplicationConversion(int ProductID)
        //	{
        //		bool flag = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 1222:
        //			case 1223:
        //			{
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				if (productID == 1227)
        //				{
        //					goto case 1223;
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RPApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				case 1219:
        //				case 1220:
        //				{
        //					break;
        //				}
        //				case 1217:
        //				case 1218:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_L0808RPVerticalApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		switch (ProductID)
        //		{
        //			case 1222:
        //			case 1223:
        //			case 1224:
        //			case 1225:
        //			case 1227:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 1226:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //	}

        //	public static bool FP4030MT_Rev1ApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1226)
        //		{
        //			switch (productID)
        //			{
        //				case 1301:
        //				case 1302:
        //				case 1303:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1215:
        //				case 1216:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				{
        //					break;
        //				}
        //				case 1217:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1226)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool FP4030MT_Rev1VerticalApplicationConversion(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		switch (ProductID)
        //		{
        //			case 1221:
        //			case 1222:
        //			case 1223:
        //			case 1224:
        //			case 1225:
        //			case 1227:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 1226:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //	}

        //	public static ArrayList GetActiveFormObject()
        //	{
        //		return CommonConstants._commconstantarrFormObject;
        //	}

        //	public static string GetBMPFileName(int pProductId)
        //	{
        //		string str = "";
        //		DataRow[] dataRowArray = CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Select(string.Concat("ModelNo= '", pProductId, "'"));
        //		DataRow[] dataRowArray1 = dataRowArray;
        //		for (int i = 0; i < (int)dataRowArray1.Length; i++)
        //		{
        //			str = dataRowArray1[i]["BMPFile"].ToString();
        //		}
        //		return str;
        //	}

        //	public static byte[,] GetColorArray(int pNoOfColors)
        //	{
        //		byte[,] numArray;
        //		int num = pNoOfColors;
        //		if (num == 2)
        //		{
        //			numArray = (byte[,])CommonConstants.Col2Supported.Clone();
        //		}
        //		else if (num == 16)
        //		{
        //			numArray = (byte[,])CommonConstants.Col16Supported.Clone();
        //		}
        //		else
        //		{
        //			numArray = (num == 256 ? (byte[,])CommonConstants.Col256Supported.Clone() : (byte[,])CommonConstants.Col256Supported.Clone());
        //		}
        //		return numArray;
        //	}

        //	public static byte GetColorIndex(Color pColor)
        //	{
        //		byte r = pColor.R;
        //		byte g = pColor.G;
        //		byte b = pColor.B;
        //		byte num = 0;
        //		while (num < CommonConstants._commconstantProductData.ColorArray.GetLength(0))
        //		{
        //			if (r != CommonConstants._commconstantProductData.ColorArray[num, 0] || g != CommonConstants._commconstantProductData.ColorArray[num, 1] || b != CommonConstants._commconstantProductData.ColorArray[num, 2])
        //			{
        //				num = (byte)(num + 1);
        //			}
        //			else
        //			{
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static Color GetConversionColor(Color objcolor, int Source, int Dest)
        //	{
        //		byte r = 0;
        //		Color color = new Color();
        //		if (!(Source != 256 ? true : Dest != 2))
        //		{
        //			r = (byte)((byte)((double)objcolor.R * 0.3) + (byte)((double)objcolor.G * 0.59) + (byte)((double)objcolor.B * 0.11));
        //			r = (byte)((r <= 128 ? 0 : 255));
        //			color = Color.FromArgb((int)r, (int)r, (int)r);
        //		}
        //		else if ((Dest != 256 ? false : Source == 2))
        //		{
        //			color = objcolor;
        //		}
        //		return color;
        //	}

        //	public static byte GetConversionIndex(byte index, int Source, int Dest)
        //	{
        //		byte r = 0;
        //		byte num = 0;
        //		Color color = new Color();
        //		Color color1 = new Color();
        //		if (!(Source != 256 ? true : Dest != 2))
        //		{
        //			color1 = Color.FromArgb((int)CommonConstants.OldProductDataInfo.ColorArray[index, 0], (int)CommonConstants.OldProductDataInfo.ColorArray[index, 1], (int)CommonConstants.OldProductDataInfo.ColorArray[index, 2]);
        //			r = (byte)((byte)((double)color1.R * 0.3) + (byte)((double)color1.G * 0.59) + (byte)((double)color1.B * 0.11));
        //			r = (byte)((r <= 128 ? 0 : 255));
        //			num = (byte)((r != 0 ? 1 : 0));
        //		}
        //		else if ((Dest != 256 ? false : Source == 2))
        //		{
        //			num = (byte)((index != 1 ? 0 : 26));
        //		}
        //		return num;
        //	}

        //	public static void GetCountIOValuesBase(int ProductID, ref int IP, ref int OP)
        //	{
        //		int productID = ProductID;
        //		if (productID <= 1264)
        //		{
        //			if (productID > 1210)
        //			{
        //				switch (productID)
        //				{
        //					case 1228:
        //					{
        //						break;
        //					}
        //					case 1229:
        //					{
        //						IP = 12;
        //						OP = 10;
        //						return;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1261:
        //							case 1262:
        //							case 1263:
        //							{
        //								IP = 12;
        //								OP = 10;
        //								return;
        //							}
        //							case 1264:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								return;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case 1151:
        //					case 1152:
        //					case 1153:
        //					case 1154:
        //					{
        //						break;
        //					}
        //					case 1155:
        //					{
        //						IP = 16;
        //						OP = 8;
        //						return;
        //					}
        //					default:
        //					{
        //						if (productID == 1210)
        //						{
        //							IP = 12;
        //							OP = 8;
        //							return;
        //						}
        //						return;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1603)
        //		{
        //			if (productID == 1532)
        //			{
        //				IP = 12;
        //				OP = 8;
        //				return;
        //			}
        //			if (productID == 1603)
        //			{
        //				IP = 8;
        //				OP = 8;
        //				return;
        //			}
        //			return;
        //		}
        //		else if (productID != 1613)
        //		{
        //			switch (productID)
        //			{
        //				case 1907:
        //				{
        //					break;
        //				}
        //				case 1908:
        //				{
        //					IP = 12;
        //					OP = 8;
        //					return;
        //				}
        //				default:
        //				{
        //					return;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			IP = 12;
        //			OP = 8;
        //			return;
        //		}
        //		IP = 8;
        //		OP = 8;
        //	}

        //	public static int GetCurrentPictureAddress()
        //	{
        //		return 0;
        //	}

        //	public static int GetDefaultScreenColorIndex(int iProductSeriesID)
        //	{
        //		int num;
        //		int num1 = 1;
        //		int num2 = iProductSeriesID;
        //		if (num2 <= 1532)
        //		{
        //			if (num2 > 1132)
        //			{
        //				if (num2 <= 1354)
        //				{
        //					if (num2 > 1303)
        //					{
        //						switch (num2)
        //						{
        //							case 1330:
        //							case 1331:
        //							case 1333:
        //							{
        //								break;
        //							}
        //							case 1332:
        //							{
        //								num = num1;
        //								return num;
        //							}
        //							default:
        //							{
        //								if (num2 == 1343)
        //								{
        //									break;
        //								}
        //								switch (num2)
        //								{
        //									case 1350:
        //									case 1351:
        //									case 1354:
        //									{
        //										break;
        //									}
        //									case 1352:
        //									case 1353:
        //									{
        //										num = num1;
        //										return num;
        //									}
        //									default:
        //									{
        //										num = num1;
        //										return num;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //					}
        //					else
        //					{
        //						switch (num2)
        //						{
        //							case 1150:
        //							case 1151:
        //							case 1152:
        //							case 1153:
        //							case 1154:
        //							case 1155:
        //							{
        //								num1 = 1;
        //								num = num1;
        //								return num;
        //							}
        //							default:
        //							{
        //								switch (num2)
        //								{
        //									case 1171:
        //									case 1172:
        //									case 1173:
        //									case 1200:
        //									case 1203:
        //									case 1204:
        //									case 1205:
        //									case 1206:
        //									case 1207:
        //									case 1209:
        //									case 1210:
        //									case 1211:
        //									case 1212:
        //									case 1213:
        //									case 1214:
        //									case 1215:
        //									case 1216:
        //									case 1217:
        //									case 1218:
        //									case 1219:
        //									case 1220:
        //									case 1221:
        //									case 1222:
        //									case 1223:
        //									case 1224:
        //									case 1225:
        //									case 1226:
        //									case 1227:
        //									case 1228:
        //									case 1229:
        //									case 1261:
        //									case 1262:
        //									case 1263:
        //									case 1264:
        //									{
        //										num1 = 1;
        //										num = num1;
        //										return num;
        //									}
        //									case 1174:
        //									case 1175:
        //									case 1176:
        //									case 1177:
        //									case 1178:
        //									case 1179:
        //									case 1180:
        //									case 1181:
        //									case 1182:
        //									case 1183:
        //									case 1184:
        //									case 1185:
        //									case 1186:
        //									case 1187:
        //									case 1188:
        //									case 1189:
        //									case 1190:
        //									case 1191:
        //									case 1192:
        //									case 1193:
        //									case 1194:
        //									case 1195:
        //									case 1196:
        //									case 1197:
        //									case 1198:
        //									case 1199:
        //									case 1201:
        //									case 1202:
        //									case 1208:
        //									case 1235:
        //									case 1236:
        //									case 1237:
        //									case 1238:
        //									case 1239:
        //									case 1244:
        //									case 1245:
        //									case 1246:
        //									case 1247:
        //									case 1248:
        //									case 1249:
        //									case 1257:
        //									case 1258:
        //									case 1259:
        //									case 1260:
        //									case 1265:
        //									case 1266:
        //									case 1267:
        //									case 1268:
        //									case 1269:
        //									case 1275:
        //									case 1276:
        //									case 1277:
        //									case 1278:
        //									case 1279:
        //									{
        //										num = num1;
        //										return num;
        //									}
        //									case 1230:
        //									case 1231:
        //									case 1232:
        //									case 1233:
        //									case 1234:
        //									case 1240:
        //									case 1241:
        //									case 1242:
        //									case 1243:
        //									case 1250:
        //									case 1251:
        //									case 1252:
        //									case 1253:
        //									case 1254:
        //									case 1255:
        //									case 1256:
        //									case 1270:
        //									case 1271:
        //									case 1272:
        //									case 1273:
        //									case 1274:
        //									case 1280:
        //									case 1281:
        //									case 1282:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										switch (num2)
        //										{
        //											case 1301:
        //											case 1302:
        //											case 1303:
        //											{
        //												num1 = 1;
        //												num = num1;
        //												return num;
        //											}
        //											default:
        //											{
        //												num = num1;
        //												return num;
        //											}
        //										}
        //										break;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //					}
        //				}
        //				else if (num2 > 1413)
        //				{
        //					switch (num2)
        //					{
        //						case 1421:
        //						case 1422:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (num2)
        //							{
        //								case 1431:
        //								case 1432:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									switch (num2)
        //									{
        //										case 1531:
        //										case 1532:
        //										{
        //											num1 = 1;
        //											num = num1;
        //											return num;
        //										}
        //										default:
        //										{
        //											num = num1;
        //											return num;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					if (num2 == 1373)
        //					{
        //						num1 = 26;
        //						num = num1;
        //						return num;
        //					}
        //					switch (num2)
        //					{
        //						case 1400:
        //						case 1401:
        //						case 1402:
        //						case 1403:
        //						{
        //							num1 = 1;
        //							num = num1;
        //							return num;
        //						}
        //						default:
        //						{
        //							switch (num2)
        //							{
        //								case 1411:
        //								case 1412:
        //								case 1413:
        //								{
        //									num1 = 1;
        //									num = num1;
        //									return num;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (num2 <= 920)
        //			{
        //				if (num2 > 647)
        //				{
        //					switch (num2)
        //					{
        //						case 686:
        //						case 687:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (num2 == 721)
        //							{
        //								break;
        //							}
        //							switch (num2)
        //							{
        //								case 918:
        //								case 919:
        //								case 920:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else if (num2 != 15)
        //				{
        //					switch (num2)
        //					{
        //						case 500:
        //						case 501:
        //						case 502:
        //						case 503:
        //						case 504:
        //						case 505:
        //						case 507:
        //						case 508:
        //						case 509:
        //						case 512:
        //						{
        //							num1 = 1;
        //							num = num1;
        //							return num;
        //						}
        //						case 506:
        //						case 511:
        //						case 514:
        //						case 515:
        //						case 516:
        //						case 517:
        //						case 518:
        //						case 519:
        //						case 524:
        //						{
        //							num = num1;
        //							return num;
        //						}
        //						case 510:
        //						case 513:
        //						{
        //							num1 = 15;
        //							num = num1;
        //							return num;
        //						}
        //						case 520:
        //						case 521:
        //						case 522:
        //						case 523:
        //						case 525:
        //						case 526:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (num2)
        //							{
        //								case 646:
        //								case 647:
        //								{
        //									num1 = 15;
        //									num = num1;
        //									return num;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					num1 = 1;
        //					num = num1;
        //					return num;
        //				}
        //			}
        //			else if (num2 > 980)
        //			{
        //				switch (num2)
        //				{
        //					case 1001:
        //					case 1002:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (num2)
        //						{
        //							case 1102:
        //							case 1103:
        //							{
        //								num1 = 1;
        //								num = num1;
        //								return num;
        //							}
        //							case 1104:
        //							case 1107:
        //							case 1109:
        //							{
        //								num = num1;
        //								return num;
        //							}
        //							case 1105:
        //							case 1108:
        //							case 1110:
        //							{
        //								break;
        //							}
        //							case 1106:
        //							{
        //								num1 = 15;
        //								num = num1;
        //								return num;
        //							}
        //							default:
        //							{
        //								switch (num2)
        //								{
        //									case 1130:
        //									case 1132:
        //									{
        //										break;
        //									}
        //									case 1131:
        //									{
        //										num = num1;
        //										return num;
        //									}
        //									default:
        //									{
        //										num = num1;
        //										return num;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (num2 != 944 && num2 != 970)
        //			{
        //				switch (num2)
        //				{
        //					case 978:
        //					case 980:
        //					{
        //						break;
        //					}
        //					case 979:
        //					{
        //						num = num1;
        //						return num;
        //					}
        //					default:
        //					{
        //						num = num1;
        //						return num;
        //					}
        //				}
        //			}
        //		}
        //		else if (num2 <= 1673)
        //		{
        //			if (num2 > 1613)
        //			{
        //				if (num2 > 1635)
        //				{
        //					switch (num2)
        //					{
        //						case 1642:
        //						case 1643:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (num2 == 1650)
        //							{
        //								break;
        //							}
        //							switch (num2)
        //							{
        //								case 1672:
        //								case 1673:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (num2)
        //					{
        //						case 1621:
        //						case 1622:
        //						case 1623:
        //						case 1624:
        //						{
        //							num1 = 1;
        //							num = num1;
        //							return num;
        //						}
        //						default:
        //						{
        //							if (num2 == 1630)
        //							{
        //								break;
        //							}
        //							switch (num2)
        //							{
        //								case 1634:
        //								case 1635:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (num2 <= 1573)
        //			{
        //				if (num2 != 1543 && num2 != 1551)
        //				{
        //					switch (num2)
        //					{
        //						case 1571:
        //						case 1573:
        //						{
        //							break;
        //						}
        //						case 1572:
        //						{
        //							num = num1;
        //							return num;
        //						}
        //						default:
        //						{
        //							num = num1;
        //							return num;
        //						}
        //					}
        //				}
        //			}
        //			else if (num2 != 1600 && num2 != 1603)
        //			{
        //				switch (num2)
        //				{
        //					case 1612:
        //					case 1613:
        //					{
        //						num1 = 1;
        //						num = num1;
        //						return num;
        //					}
        //					default:
        //					{
        //						num = num1;
        //						return num;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				num1 = 1;
        //				num = num1;
        //				return num;
        //			}
        //		}
        //		else if (num2 <= 1912)
        //		{
        //			if (num2 <= 1803)
        //			{
        //				if (num2 != 1681)
        //				{
        //					switch (num2)
        //					{
        //						case 1701:
        //						case 1702:
        //						case 1703:
        //						case 1704:
        //						case 1711:
        //						case 1712:
        //						case 1713:
        //						case 1714:
        //						case 1715:
        //						case 1721:
        //						case 1722:
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1705:
        //						case 1706:
        //						case 1707:
        //						case 1708:
        //						case 1709:
        //						case 1710:
        //						case 1716:
        //						case 1717:
        //						case 1718:
        //						case 1719:
        //						case 1720:
        //						{
        //							num = num1;
        //							return num;
        //						}
        //						default:
        //						{
        //							if (num2 == 1803)
        //							{
        //								break;
        //							}
        //							num = num1;
        //							return num;
        //						}
        //					}
        //				}
        //			}
        //			else if (num2 != 1813 && num2 != 1823)
        //			{
        //				switch (num2)
        //				{
        //					case 1901:
        //					case 1902:
        //					case 1903:
        //					case 1904:
        //					case 1905:
        //					case 1906:
        //					case 1911:
        //					case 1912:
        //					{
        //						break;
        //					}
        //					case 1907:
        //					case 1908:
        //					case 1909:
        //					case 1910:
        //					{
        //						num1 = 1;
        //						num = num1;
        //						return num;
        //					}
        //					default:
        //					{
        //						num = num1;
        //						return num;
        //					}
        //				}
        //			}
        //		}
        //		else if (num2 <= 2161)
        //		{
        //			switch (num2)
        //			{
        //				case 2001:
        //				case 2002:
        //				case 2003:
        //				{
        //					num1 = 1;
        //					num = num1;
        //					return num;
        //				}
        //				default:
        //				{
        //					if (num2 == 2021 || num2 == 2161)
        //					{
        //						num1 = 1;
        //						num = num1;
        //						return num;
        //					}
        //					num = num1;
        //					return num;
        //				}
        //			}
        //		}
        //		else if (num2 > 3801)
        //		{
        //			switch (num2)
        //			{
        //				case 5701:
        //				case 5702:
        //				case 5706:
        //				case 5707:
        //				case 5710:
        //				case 5711:
        //				{
        //					num1 = 15;
        //					num = num1;
        //					return num;
        //				}
        //				case 5703:
        //				case 5704:
        //				case 5708:
        //				case 5709:
        //				case 5712:
        //				case 5713:
        //				{
        //					break;
        //				}
        //				case 5705:
        //				{
        //					num = num1;
        //					return num;
        //				}
        //				default:
        //				{
        //					if (num2 == 13001)
        //					{
        //						num1 = 1;
        //						num = num1;
        //						return num;
        //					}
        //					num = num1;
        //					return num;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (num2)
        //			{
        //				case 3503:
        //				case 3504:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (num2 == 3801)
        //					{
        //						num1 = 15;
        //						num = num1;
        //						return num;
        //					}
        //					num = num1;
        //					return num;
        //				}
        //			}
        //		}
        //		num1 = 26;
        //		num = num1;
        //		return num;
        //	}

        //	public static string GetDeviceNodeNameFromXML()
        //	{
        //		string str;
        //		string str1 = "DefaultNodeTag.xml";
        //		FileStream fileStream = null;
        //		string str2 = "";
        //		DataSet dataSet = new DataSet();
        //		if (CommonConstants.g_Support_IEC_Ladder)
        //		{
        //			str1 = "DefaultNodeTag_IEC.xml";
        //		}
        //		string str3 = "";
        //		try
        //		{
        //			fileStream = new FileStream(str1, FileMode.Open);
        //		}
        //		catch (Exception exception)
        //		{
        //			str2 = string.Concat(str2, "Failed to Open File ", str1);
        //			MessageBox.Show(str2);
        //			str = str3;
        //			return str;
        //		}
        //		dataSet.ReadXml(fileStream);
        //		DataTable item = dataSet.Tables["NodeData"];
        //		str3 = item.Rows[0][1].ToString().Trim();
        //		fileStream.Close();
        //		if (str3.Length == 0)
        //		{
        //			MessageBox.Show("Failed to initialize device node name.");
        //		}
        //		str = str3;
        //		return str;
        //	}

        //	public static string GetDeviceRangesXMLFileName(int ProductID)
        //	{
        //		string projectUnitXmlFile = CommonConstants.ProjectUnitXmlFile;
        //		if (!(!CommonConstants.IsProductPLC(ProductID) ? true : CommonConstants.g_Support_IEC_Ladder))
        //		{
        //			projectUnitXmlFile = "MicropLC.xml";
        //		}
        //		else if ((CommonConstants.IsProductIsTextBased(ProductID) ? false : !CommonConstants.IsProductIsTextAndGraphicsBased(ProductID)))
        //		{
        //			projectUnitXmlFile = (!CommonConstants.IsProductSupportedFP3series(ProductID) ? CommonConstants.ProjectUnitXmlFile : "PrizmUnit3XX.xml");
        //		}
        //		else if (!(ProductID == 1228 ? false : ProductID != 1264))
        //		{
        //			projectUnitXmlFile = CommonConstants.ProjectUnitXmlFile;
        //		}
        //		else if (ProductID != 1155)
        //		{
        //			projectUnitXmlFile = ((ProductID == 1171 || ProductID == 1172 ? false : ProductID != 1173) ? CommonConstants.ProjectUnitLPCFile : "PrizmUnitLPC20XX.xml");
        //		}
        //		else
        //		{
        //			projectUnitXmlFile = CommonConstants.ProjectUnitXmlFile;
        //		}
        //		return projectUnitXmlFile;
        //	}

        //	public static int GetExpansion_Count(int ProductID)
        //	{
        //		DataSet dataSet = new DataSet();
        //		if (CommonConstants.IsProductPLC(ProductID))
        //		{
        //			if (!CommonConstants.g_Support_IEC_Ladder)
        //			{
        //				dataSet.ReadXml("LdrConfigPLC.xml");
        //			}
        //			else
        //			{
        //				dataSet.ReadXml("LdrConfigPLC_IEC.xml");
        //			}
        //		}
        //		else if ((ProductID == 1702 || ProductID == 1704 || ProductID == 1712 || ProductID == 1715 || ProductID == 1722 ? false : ProductID != 1725))
        //		{
        //			dataSet.ReadXml(CommonConstants.IOExpansionFile);
        //		}
        //		else
        //		{
        //			dataSet.ReadXml("LdrIOFlexi3XX.xml");
        //		}
        //		return dataSet.Tables["Slot_One"].Rows.Count - 1;
        //	}

        //	public static int GetExpansion_ProductID(int ModuleType, int ProductID)
        //	{
        //		int num;
        //		if (ModuleType != 0)
        //		{
        //			string empty = string.Empty;
        //			DataSet dataSet = new DataSet();
        //			if (CommonConstants.IsProductPLC(ProductID))
        //			{
        //				dataSet.ReadXml("LdrConfigPLC.xml");
        //			}
        //			else if ((ProductID == 1702 || ProductID == 1704 || ProductID == 1712 || ProductID == 1715 || ProductID == 1722 ? false : ProductID != 1725))
        //			{
        //				dataSet.ReadXml(CommonConstants.IOExpansionFile);
        //			}
        //			else
        //			{
        //				dataSet.ReadXml("LdrIOFlexi3XX.xml");
        //			}
        //			DataTable item = dataSet.Tables["Slot_One"];
        //			int num1 = 1;
        //			while (num1 < item.Rows.Count)
        //			{
        //				if (Convert.ToInt32(item.Rows[num1]["ExpProduct_ID"].ToString()) != ModuleType)
        //				{
        //					num1++;
        //				}
        //				else
        //				{
        //					num = num1;
        //					return num;
        //				}
        //			}
        //			num = 0;
        //		}
        //		else
        //		{
        //			num = 0;
        //		}
        //		return num;
        //	}

        //	public static int GetExpansionSlotCount(int pProductID)
        //	{
        //		int num;
        //		int num1 = 0;
        //		int num2 = pProductID;
        //		if (num2 <= 1412)
        //		{
        //			if (num2 > 1240)
        //			{
        //				if (num2 <= 1333)
        //				{
        //					if (num2 > 1256)
        //					{
        //						switch (num2)
        //						{
        //							case 1270:
        //							case 1273:
        //							case 1274:
        //							{
        //								num1 = 5;
        //								num = num1;
        //								return num;
        //							}
        //							case 1271:
        //							case 1272:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (num2)
        //								{
        //									case 1331:
        //									case 1333:
        //									{
        //										num1 = 3;
        //										num = num1;
        //										return num;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //					}
        //					else
        //					{
        //						if (num2 == 1243)
        //						{
        //							num1 = 3;
        //							num = num1;
        //							return num;
        //						}
        //						switch (num2)
        //						{
        //							case 1250:
        //							case 1254:
        //							case 1255:
        //							case 1256:
        //							{
        //								num1 = 5;
        //								num = num1;
        //								return num;
        //							}
        //						}
        //					}
        //				}
        //				else if (num2 > 1351)
        //				{
        //					if (num2 == 1354 || num2 == 1373)
        //					{
        //						num1 = 5;
        //						num = num1;
        //						return num;
        //					}
        //					if (num2 == 1412)
        //					{
        //						num1 = 3;
        //						num = num1;
        //						return num;
        //					}
        //				}
        //				else
        //				{
        //					if (num2 == 1343)
        //					{
        //						num1 = 3;
        //						num = num1;
        //						return num;
        //					}
        //					if (num2 == 1351)
        //					{
        //						num1 = 5;
        //						num = num1;
        //						return num;
        //					}
        //				}
        //			}
        //			else if (num2 <= 980)
        //			{
        //				if (num2 > 932)
        //				{
        //					switch (num2)
        //					{
        //						case 941:
        //						case 942:
        //						{
        //							num1 = 8;
        //							num = num1;
        //							return num;
        //						}
        //						case 943:
        //						case 945:
        //						case 946:
        //						case 949:
        //						case 950:
        //						case 951:
        //						case 952:
        //						case 953:
        //						case 954:
        //						case 955:
        //						case 956:
        //						{
        //							num = num1;
        //							return num;
        //						}
        //						case 944:
        //						{
        //							num1 = 16;
        //							num = num1;
        //							return num;
        //						}
        //						case 947:
        //						case 948:
        //						{
        //							num1 = 16;
        //							num = num1;
        //							return num;
        //						}
        //						case 957:
        //						case 958:
        //						case 959:
        //						case 960:
        //						case 961:
        //						case 962:
        //						case 963:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (num2)
        //							{
        //								case 970:
        //								case 973:
        //								case 974:
        //								case 976:
        //								case 977:
        //								case 978:
        //								{
        //									break;
        //								}
        //								case 971:
        //								case 972:
        //								case 975:
        //								case 979:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //								case 980:
        //								{
        //									num1 = 16;
        //									num = num1;
        //									return num;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (num2)
        //					{
        //						case 909:
        //						case 910:
        //						case 913:
        //						case 914:
        //						{
        //							num1 = 8;
        //							num = num1;
        //							return num;
        //						}
        //						case 911:
        //						case 912:
        //						case 915:
        //						case 916:
        //						case 919:
        //						{
        //							num = num1;
        //							return num;
        //						}
        //						case 917:
        //						{
        //							num1 = 1;
        //							num = num1;
        //							return num;
        //						}
        //						case 918:
        //						{
        //							num1 = 16;
        //							num = num1;
        //							return num;
        //						}
        //						case 920:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (num2)
        //							{
        //								case 931:
        //								case 932:
        //								{
        //									num1 = 8;
        //									num = num1;
        //									return num;
        //								}
        //								default:
        //								{
        //									num = num1;
        //									return num;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				num1 = 16;
        //			}
        //			else if (num2 <= 1230)
        //			{
        //				if (num2 == 1209 || num2 == 1230)
        //				{
        //					num1 = 3;
        //					num = num1;
        //					return num;
        //				}
        //			}
        //			else if (num2 == 1233 || num2 == 1240)
        //			{
        //				num1 = 3;
        //				num = num1;
        //				return num;
        //			}
        //		}
        //		else if (num2 <= 1630)
        //		{
        //			if (num2 <= 1543)
        //			{
        //				if (num2 > 1431)
        //				{
        //					if (num2 == 1531)
        //					{
        //						num1 = 3;
        //						num = num1;
        //						return num;
        //					}
        //					if (num2 == 1543)
        //					{
        //						num1 = 3;
        //					}
        //				}
        //				else
        //				{
        //					if (num2 == 1421)
        //					{
        //						num1 = 3;
        //						num = num1;
        //						return num;
        //					}
        //					if (num2 == 1431)
        //					{
        //						num1 = 5;
        //						num = num1;
        //						return num;
        //					}
        //				}
        //			}
        //			else if (num2 <= 1573)
        //			{
        //				if (num2 == 1551)
        //				{
        //					num1 = 3;
        //					num = num1;
        //					return num;
        //				}
        //				switch (num2)
        //				{
        //					case 1571:
        //					{
        //						num1 = 5;
        //						num = num1;
        //						return num;
        //					}
        //					case 1573:
        //					{
        //						num1 = 5;
        //						break;
        //					}
        //				}
        //			}
        //			else if (num2 == 1612 || num2 == 1630)
        //			{
        //				num1 = 3;
        //				num = num1;
        //				return num;
        //			}
        //		}
        //		else if (num2 <= 1704)
        //		{
        //			if (num2 > 1650)
        //			{
        //				if (num2 == 1673)
        //				{
        //					num1 = 5;
        //					num = num1;
        //					return num;
        //				}
        //				switch (num2)
        //				{
        //					case 1702:
        //					case 1704:
        //					{
        //						num1 = 1;
        //						num = num1;
        //						return num;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (num2 == 1643)
        //				{
        //					num1 = 3;
        //					num = num1;
        //					return num;
        //				}
        //				if (num2 == 1650)
        //				{
        //					num1 = 5;
        //					num = num1;
        //					return num;
        //				}
        //			}
        //		}
        //		else if (num2 <= 1715)
        //		{
        //			if (num2 == 1712 || num2 == 1715)
        //			{
        //				num1 = 3;
        //				num = num1;
        //				return num;
        //			}
        //		}
        //		else if (num2 != 1722 && num2 != 1725)
        //		{
        //			switch (num2)
        //			{
        //				case 1911:
        //				{
        //					num1 = 3;
        //					num = num1;
        //					return num;
        //				}
        //				case 1912:
        //				{
        //					num1 = 5;
        //					num = num1;
        //					return num;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			num1 = 5;
        //			num = num1;
        //			return num;
        //		}
        //		num = num1;
        //		return num;
        //	}

        //	public static string GetFolderName(int pProjectID)
        //	{
        //		return CommonConstants.GetProductName(pProjectID);
        //	}

        //	public static byte GetGroupNumber(byte btbitAlarmNo)
        //	{
        //		byte num;
        //		byte num1 = 17;
        //		if (!(btbitAlarmNo < 0 ? true : btbitAlarmNo > 15))
        //		{
        //			int num2 = 0;
        //			num1 = (byte)num2;
        //			num = (byte)num2;
        //		}
        //		else if (!(btbitAlarmNo < 16 ? true : btbitAlarmNo > 31))
        //		{
        //			int num3 = 1;
        //			num1 = (byte)num3;
        //			num = (byte)num3;
        //		}
        //		else if (!(btbitAlarmNo < 32 ? true : btbitAlarmNo > 47))
        //		{
        //			int num4 = 2;
        //			num1 = (byte)num4;
        //			num = (byte)num4;
        //		}
        //		else if (!(btbitAlarmNo < 48 ? true : btbitAlarmNo > 63))
        //		{
        //			int num5 = 3;
        //			num1 = (byte)num5;
        //			num = (byte)num5;
        //		}
        //		else if (!(btbitAlarmNo < 64 ? true : btbitAlarmNo > 79))
        //		{
        //			int num6 = 4;
        //			num1 = (byte)num6;
        //			num = (byte)num6;
        //		}
        //		else if (!(btbitAlarmNo < 80 ? true : btbitAlarmNo > 95))
        //		{
        //			int num7 = 5;
        //			num1 = (byte)num7;
        //			num = (byte)num7;
        //		}
        //		else if (!(btbitAlarmNo < 96 ? true : btbitAlarmNo > 111))
        //		{
        //			int num8 = 6;
        //			num1 = (byte)num8;
        //			num = (byte)num8;
        //		}
        //		else if (!(btbitAlarmNo < 112 ? true : btbitAlarmNo > 127))
        //		{
        //			int num9 = 7;
        //			num1 = (byte)num9;
        //			num = (byte)num9;
        //		}
        //		else if (!(btbitAlarmNo < 128 ? true : btbitAlarmNo > 143))
        //		{
        //			int num10 = 8;
        //			num1 = (byte)num10;
        //			num = (byte)num10;
        //		}
        //		else if (!(btbitAlarmNo < 144 ? true : btbitAlarmNo > 159))
        //		{
        //			int num11 = 9;
        //			num1 = (byte)num11;
        //			num = (byte)num11;
        //		}
        //		else if (!(btbitAlarmNo < 160 ? true : btbitAlarmNo > 175))
        //		{
        //			int num12 = 10;
        //			num1 = (byte)num12;
        //			num = (byte)num12;
        //		}
        //		else if (!(btbitAlarmNo < 176 ? true : btbitAlarmNo > 191))
        //		{
        //			int num13 = 11;
        //			num1 = (byte)num13;
        //			num = (byte)num13;
        //		}
        //		else if (!(btbitAlarmNo < 192 ? true : btbitAlarmNo > 207))
        //		{
        //			int num14 = 12;
        //			num1 = (byte)num14;
        //			num = (byte)num14;
        //		}
        //		else if (!(btbitAlarmNo < 208 ? true : btbitAlarmNo > 223))
        //		{
        //			int num15 = 13;
        //			num1 = (byte)num15;
        //			num = (byte)num15;
        //		}
        //		else if (!(btbitAlarmNo < 224 ? true : btbitAlarmNo > 239))
        //		{
        //			int num16 = 14;
        //			num1 = (byte)num16;
        //			num = (byte)num16;
        //		}
        //		else if ((btbitAlarmNo < 240 ? true : btbitAlarmNo > 255))
        //		{
        //			num = num1;
        //		}
        //		else
        //		{
        //			int num17 = 15;
        //			num1 = (byte)num17;
        //			num = (byte)num17;
        //		}
        //		return num;
        //	}

        //	public static byte[] GetHalfWord(int temp_variable)
        //	{
        //		byte[] num = new byte[2];
        //		int tempVariable = 0;
        //		num[0] = Convert.ToByte(temp_variable & 255);
        //		tempVariable = temp_variable >> 8;
        //		num[1] = Convert.ToByte(tempVariable & 255);
        //		return num;
        //	}

        //	public static int GetInitializationColorIndex(int pNoOfColorsSupported, int pColorIndexOfPrizm545)
        //	{
        //		int num = 0;
        //		if (pNoOfColorsSupported == 256)
        //		{
        //			num = pColorIndexOfPrizm545;
        //		}
        //		else if (pNoOfColorsSupported != 16)
        //		{
        //			if (pNoOfColorsSupported != 2)
        //			{
        //				if (pNoOfColorsSupported == 4)
        //				{
        //					if (!(pColorIndexOfPrizm545 == 26 ? false : pColorIndexOfPrizm545 != 141))
        //					{
        //						num = 3;
        //					}
        //					else if (!(pColorIndexOfPrizm545 == 16 ? false : pColorIndexOfPrizm545 != 10))
        //					{
        //						num = 2;
        //					}
        //					else if (!(pColorIndexOfPrizm545 == 70 || pColorIndexOfPrizm545 == 236 || pColorIndexOfPrizm545 == 160 || pColorIndexOfPrizm545 == 0 || pColorIndexOfPrizm545 == 57 || pColorIndexOfPrizm545 == 124 ? false : pColorIndexOfPrizm545 != 255))
        //					{
        //						num = 0;
        //					}
        //					else if (pColorIndexOfPrizm545 == 19)
        //					{
        //						num = 2;
        //					}
        //				}
        //			}
        //			else if (!(pColorIndexOfPrizm545 == 26 ? false : pColorIndexOfPrizm545 != 141))
        //			{
        //				num = 1;
        //			}
        //			else if ((pColorIndexOfPrizm545 == 70 || pColorIndexOfPrizm545 == 236 || pColorIndexOfPrizm545 == 160 || pColorIndexOfPrizm545 == 0 || pColorIndexOfPrizm545 == 57 || pColorIndexOfPrizm545 == 124 || pColorIndexOfPrizm545 == 16 ? true : pColorIndexOfPrizm545 == 255))
        //			{
        //				num = 0;
        //			}
        //		}
        //		else if (!(pColorIndexOfPrizm545 == 26 ? false : pColorIndexOfPrizm545 != 141))
        //		{
        //			num = 15;
        //		}
        //		else if (pColorIndexOfPrizm545 == 16)
        //		{
        //			num = 10;
        //		}
        //		else if (!(pColorIndexOfPrizm545 == 70 || pColorIndexOfPrizm545 == 236 || pColorIndexOfPrizm545 == 160 || pColorIndexOfPrizm545 == 0 || pColorIndexOfPrizm545 == 57 || pColorIndexOfPrizm545 == 124 ? false : pColorIndexOfPrizm545 != 255))
        //		{
        //			num = 0;
        //		}
        //		else if (pColorIndexOfPrizm545 == 19)
        //		{
        //			num = 12;
        //		}
        //		return num;
        //	}

        //	public static string GetKeySpecificTaskName(KeyTaskCode objTaskCode)
        //	{
        //		string str;
        //		string str1 = "";
        //		KeyTaskCode keyTaskCode = objTaskCode;
        //		if (keyTaskCode <= KeyTaskCode.RefreshTrendWindow)
        //		{
        //			switch (keyTaskCode)
        //			{
        //				case KeyTaskCode.StartLogger:
        //				{
        //					str1 = CoreConstStrings.strStartLogger;
        //					break;
        //				}
        //				case KeyTaskCode.StopLogger:
        //				{
        //					str1 = CoreConstStrings.strStopLogger;
        //					break;
        //				}
        //				case KeyTaskCode.ClearLogMemory:
        //				{
        //					str1 = CoreConstStrings.strClearLogMemory;
        //					break;
        //				}
        //				default:
        //				{
        //					if (keyTaskCode == KeyTaskCode.StartLoggerOfGroupNo)
        //					{
        //						str1 = CoreConstStrings.strStartLoggerOfGroupNo;
        //						break;
        //					}
        //					else if (keyTaskCode == KeyTaskCode.RefreshTrendWindow)
        //					{
        //						str1 = CoreConstStrings.strRefreshTrendWindow;
        //						break;
        //					}
        //					else
        //					{
        //						str = str1;
        //						return str;
        //					}
        //				}
        //			}
        //		}
        //		else if (keyTaskCode == KeyTaskCode.StopPrintingOfGroupNo)
        //		{
        //			str1 = CoreConstStrings.strStopLoggerOfGroupNo;
        //		}
        //		else if (keyTaskCode == KeyTaskCode.ShowEthernetConfigurationScreen)
        //		{
        //			str1 = CoreConstStrings.strShowEthernetConfigurationScreen;
        //		}
        //		else
        //		{
        //			switch (keyTaskCode)
        //			{
        //				case KeyTaskCode.StartExternalLogger:
        //				{
        //					str1 = CoreConstStrings.strStartExternalLogger;
        //					break;
        //				}
        //				case KeyTaskCode.StopExternalLogger:
        //				{
        //					str1 = CoreConstStrings.strStopExternalLogger;
        //					break;
        //				}
        //				case KeyTaskCode.StartExternalLoggerOfGroupNo:
        //				{
        //					str1 = CoreConstStrings.strStartExternalLoggerOfGroupNo;
        //					break;
        //				}
        //				case KeyTaskCode.StopExternalLoggerOfGroupNo:
        //				{
        //					str1 = CoreConstStrings.strStopExternalLoggerOfGroupNo;
        //					break;
        //				}
        //				default:
        //				{
        //					str = str1;
        //					return str;
        //				}
        //			}
        //		}
        //		str = str1;
        //		return str;
        //	}

        //	public static string GetModelSeriesName(int iProductId)
        //	{
        //		string str = "";
        //		foreach (DataRow row in CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Rows)
        //		{
        //			if (iProductId == Convert.ToInt32(row.ItemArray[13].ToString()))
        //			{
        //				str = row.ItemArray[2].ToString();
        //				break;
        //			}
        //		}
        //		return str;
        //	}

        //	public static int GetModelSeriesProductId(int iProductId)
        //	{
        //		DataRow row = null;
        //		string str = "";
        //		int num = 0;
        //		foreach (DataRow row in CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Rows)
        //		{
        //			if (iProductId == Convert.ToInt32(row.ItemArray[13].ToString()))
        //			{
        //				str = row.ItemArray[2].ToString();
        //				break;
        //			}
        //		}
        //		foreach (DataRow dataRow in CommonConstants.dsRecentProjectList.Tables["ModelData"].Rows)
        //		{
        //			if (str.Equals(dataRow.ItemArray[1].ToString()))
        //			{
        //				num = Convert.ToInt32(dataRow.ItemArray[6].ToString());
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static byte GetNumberOfKeysForAsciiStyle(AsciiKeypadStyles pKeypadStyle)
        //	{
        //		byte num = Convert.ToByte(NumberOfAsciiKeypadKeys.AsciiKeyPad_Keys);
        //		switch (pKeypadStyle)
        //		{
        //			case AsciiKeypadStyles.AsciiKeypad_Style:
        //			{
        //				num = Convert.ToByte(NumberOfAsciiKeypadKeys.AsciiKeyPad_Keys);
        //				break;
        //			}
        //			case AsciiKeypadStyles.AsciiNumericKeyPad_Style:
        //			{
        //				num = Convert.ToByte(NumberOfAsciiKeypadKeys.AsciiNumericKeyPadKeys);
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static byte GetNumberOfKeysForAStyle(KeypadStyles pKeypadStyle)
        //	{
        //		byte num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_16_KEYS);
        //		switch (pKeypadStyle)
        //		{
        //			case KeypadStyles.KEYPAD_16_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_16_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_12_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_12_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_20_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_20_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_25_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_25_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_15_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_15_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_8_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_8_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_5_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_5_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_3_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_3_KEYS);
        //				break;
        //			}
        //			case KeypadStyles.KEYPAD_14_KEYS_STYLE:
        //			{
        //				num = Convert.ToByte(NumberOfKeypadKeys.KEYPAD_14_KEYS);
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static void GetPatternIndex(ref object pPattern, byte pPatternIndex)
        //	{
        //		if (pPatternIndex == Convert.ToByte(PatternBrush.NOFILL))
        //		{
        //			pPattern = 0;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.ONE_BLACK_ONE_WHITE))
        //		{
        //			pPattern = 14;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.THREE_BLACK_ONE_WHITE))
        //		{
        //			pPattern = 16;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.ONE_BLACK_THREE_WHITE))
        //		{
        //			pPattern = 7;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.ONE_WHITE_ONE_BLACK))
        //		{
        //			pPattern = 12;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.HORIZONTAL))
        //		{
        //			pPattern = 25;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.VERTICAL))
        //		{
        //			pPattern = 24;
        //		}
        //		else if (pPatternIndex == Convert.ToByte(PatternBrush.CROSS))
        //		{
        //			pPattern = 48;
        //		}
        //	}

        //	public static int GetPrizmPixels(Color pColor)
        //	{
        //		int num = 0;
        //		int num1 = 0;
        //		int num2 = 0;
        //		int num3 = 16;
        //		int r = pColor.R;
        //		int g = pColor.G;
        //		int b = pColor.B;
        //		int length = CommonConstants.ProductDataInfo.ColorArray.GetLength(0);
        //		int num4 = 0;
        //		int num5 = 0;
        //		int num6 = 0;
        //		if (length != 2)
        //		{
        //			if (r > CommonConstants.MaximumIntensityOfRed)
        //			{
        //				r = CommonConstants.MaximumIntensityOfRed;
        //			}
        //			if (g > CommonConstants.MaximumIntensityOfGreen)
        //			{
        //				g = CommonConstants.MaximumIntensityOfGreen;
        //			}
        //			if (b > CommonConstants.MaximumIntensityOfBlue)
        //			{
        //				b = CommonConstants.MaximumIntensityOfBlue;
        //			}
        //		}
        //		int num7 = num3 - 1;
        //		num3 = num7;
        //		if ((r & num7) != 0)
        //		{
        //			num1 = 1;
        //		}
        //		if ((g & num3) != 0)
        //		{
        //			num1 = 1;
        //		}
        //		int num8 = num3;
        //		num3 = num8 + 1;
        //		if ((b & num8) != 0)
        //		{
        //			num1 = 1;
        //		}
        //		Color color = Color.FromArgb(r, g, b);
        //		int argb = color.ToArgb();
        //		num1 = (num1 != 0 ? -1 : CommonConstants.ReturnColorIndex(argb));
        //		if (num1 == -1)
        //		{
        //			if (length == 2)
        //			{
        //				color = Color.FromArgb(r, g, b);
        //				argb = color.ToArgb();
        //				num1 = CommonConstants.ReturnColorIndex(argb);
        //			}
        //			else
        //			{
        //				num4 = r / num3 * num3;
        //				if (r - num4 >= 8)
        //				{
        //					num4 += num3;
        //				}
        //				num6 = g / num3 * num3;
        //				if (g - num6 >= 8)
        //				{
        //					num6 += num3;
        //				}
        //				num5 = b / num3 * num3;
        //				if (b - num5 >= 8)
        //				{
        //					num5 += num3;
        //				}
        //				color = Color.FromArgb(num4, num6, num5);
        //				argb = color.ToArgb();
        //				num1 = CommonConstants.ReturnColorIndex(argb);
        //			}
        //			if (num1 == -1)
        //			{
        //				int num9 = 1;
        //				while (num9 < 16)
        //				{
        //					num2 = 0;
        //					length = CommonConstants.ProductDataInfo.ColorArray.GetLength(0);
        //					num = 0;
        //					while (num < length)
        //					{
        //						int colorArray = CommonConstants.ProductDataInfo.ColorArray[num, 0];
        //						colorArray = num4 - colorArray;
        //						if (colorArray == 0)
        //						{
        //							colorArray = num3 * num9;
        //						}
        //						if ((colorArray == -num3 * num9 ? true : colorArray == num3 * num9))
        //						{
        //							int colorArray1 = CommonConstants.ProductDataInfo.ColorArray[num, 1];
        //							colorArray1 = num6 - colorArray1;
        //							if (colorArray1 == 0)
        //							{
        //								colorArray1 = num3 * num9;
        //							}
        //							if ((colorArray1 == -num3 * num9 ? true : colorArray1 == num3 * num9))
        //							{
        //								int colorArray2 = CommonConstants.ProductDataInfo.ColorArray[num, 2];
        //								colorArray2 = num5 - colorArray2;
        //								if (colorArray2 == 0)
        //								{
        //									colorArray2 = num3 * num9;
        //								}
        //								if ((colorArray2 == -num3 * num9 ? true : colorArray2 == num3 * num9))
        //								{
        //									num2 = 1;
        //								}
        //							}
        //						}
        //						if (num2 != 1)
        //						{
        //							num++;
        //						}
        //						else
        //						{
        //							break;
        //						}
        //					}
        //					if (num2 != 1)
        //					{
        //						num9++;
        //					}
        //					else
        //					{
        //						break;
        //					}
        //				}
        //				num1 = num;
        //			}
        //		}
        //		return num1;
        //	}

        //	public static int GetProductID_FromModelName(string ModelName)
        //	{
        //		int num = 0;
        //		DataRow[] dataRowArray = CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Select(string.Concat("Model = '", ModelName, "'"));
        //		DataRow[] dataRowArray1 = dataRowArray;
        //		for (int i = 0; i < (int)dataRowArray1.Length; i++)
        //		{
        //			DataRow dataRow = dataRowArray1[i];
        //			num = Convert.ToInt32(dataRow.ItemArray[13].ToString());
        //		}
        //		return num;
        //	}

        //	public static string GetProductName(int pProjectID)
        //	{
        //		return CommonConstants.GetProductName_FromProductID(pProjectID);
        //	}

        //	public static string GetProductName_FromProductID(int ProductId)
        //	{
        //		string str = "";
        //		DataRow[] dataRowArray = CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Select(string.Concat("ModelNo = '", ProductId, "'"));
        //		DataRow[] dataRowArray1 = dataRowArray;
        //		for (int i = 0; i < (int)dataRowArray1.Length; i++)
        //		{
        //			str = dataRowArray1[i]["FolderName"].ToString();
        //		}
        //		return str;
        //	}

        //	public static string GetProductNameForUSBHost(int pProjectID)
        //	{
        //		string str;
        //		string folderName = "";
        //		ProductID productID = (ProductID)pProjectID;
        //		if (productID <= ProductID.PRODUCT_FH9057T)
        //		{
        //			if (productID <= ProductID.PRODUCT_TRPMIU0300E)
        //			{
        //				if (productID > ProductID.PRODUCT_FP4020MR_L0808R_S3)
        //				{
        //					switch (productID)
        //					{
        //						case ProductID.PRODUCT_FP4035T_E:
        //						{
        //							folderName = "FP4035TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4035T:
        //						{
        //							folderName = "FP4035T";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4035TN:
        //						{
        //							folderName = "FP4035TN";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4035TN_E:
        //						{
        //							folderName = "FP4035TNE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4035TN:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E:
        //						case ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP5043T_E:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043T_E:
        //						case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E:
        //						case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4030MN_E | ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035T | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5043TN_E:
        //						case ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MT_L0808RP_A0201:
        //						case ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0:
        //						{
        //							folderName = CommonConstants.GetFolderName(pProjectID);
        //							str = folderName;
        //							return str;
        //						}
        //						case ProductID.PRODUCT_FP5043T_E:
        //						{
        //							folderName = "FP5043TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP5043T:
        //						{
        //							folderName = "FP5043T";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP5043TN:
        //						{
        //							folderName = "FP5043TN";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP5043TN_E:
        //						{
        //							folderName = "FP5043TNE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_E:
        //						{
        //							folderName = "FP4057TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T:
        //						{
        //							folderName = "FP4057T";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_S2:
        //						{
        //							folderName = "FP4057TS2";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057TN:
        //						{
        //							folderName = "FP4057TN";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057TN_E:
        //						{
        //							folderName = "FP4057TNE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_E_S1:
        //						{
        //							folderName = "FP4057TES1";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_E_VERTICAL:
        //						{
        //							folderName = "FP4057TE";
        //							break;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case ProductID.PRODUCT_FP5070T_E:
        //								{
        //									folderName = "FP5070TE";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070T:
        //								{
        //									folderName = "FP5070T";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070TN:
        //								{
        //									folderName = "FP5070TN";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070TN_E:
        //								{
        //									folderName = "FP5070TNE";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070T_E_S2:
        //								{
        //									folderName = "FP5070TES2";
        //									break;
        //								}
        //								case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_E | ProductID.PRODUCT_FP4030MR_L1208R | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_HORIZONTAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP4057T | ProductID.PRODUCT_FP4057T_E | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5043TN_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070TN_E | ProductID.PRODUCT_FP5070T_E_S2 | ProductID.PRODUCT_FP3035T_24:
        //								case ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP3035T_5:
        //								case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_E | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MR_L1210P_A0402U | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_S1_HORIZONTAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057TN | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070TN_E | ProductID.PRODUCT_FP3035T_5:
        //								case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_L1208R | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_S1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP4057T_E | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057TN_E | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070T_E | ProductID.PRODUCT_FP5070T_E_S2 | ProductID.PRODUCT_FP3035T_24 | ProductID.PRODUCT_FP3035T_5:
        //								case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4030MN_E | ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR | ProductID.PRODUCT_FP4020MR_L0808P | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_E | ProductID.PRODUCT_FP4030MR_L1208R | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MR_L1210P_A0402U | ProductID.PRODUCT_FP4030MR_L1210RP | ProductID.PRODUCT_FP4030MR_L1210P | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_HORIZONTAL | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_S1_HORIZONTAL | ProductID.PRODUCT_FP4030MT_S1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035T | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP4057T | ProductID.PRODUCT_FP4057T_E | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057TN | ProductID.PRODUCT_FP4057TN_E | ProductID.PRODUCT_FP4057T_E_S1 | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP4020M_L0808R_A | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5043TN_E | ProductID.PRODUCT_FP5070T | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070T_E | ProductID.PRODUCT_FP5070TN_E | ProductID.PRODUCT_FP5070T_E_S2 | ProductID.PRODUCT_FP3035T_24 | ProductID.PRODUCT_FP3035T_5:
        //								{
        //									folderName = CommonConstants.GetFolderName(pProjectID);
        //									str = folderName;
        //									return str;
        //								}
        //								case ProductID.PRODUCT_FP5121T:
        //								{
        //									folderName = "FP5121T";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5121TN:
        //								{
        //									folderName = "FP5121TN";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5121TN_S0:
        //								{
        //									folderName = "FP5121TN-S0";
        //									break;
        //								}
        //								default:
        //								{
        //									switch (productID)
        //									{
        //										case ProductID.PRODUCT_TRPMIU0300A:
        //										{
        //											folderName = "TRP0300A";
        //											break;
        //										}
        //										case 1332:
        //										{
        //											folderName = CommonConstants.GetFolderName(pProjectID);
        //											str = folderName;
        //											return str;
        //										}
        //										case ProductID.PRODUCT_TRPMIU0300E:
        //										{
        //											folderName = "TRP0300E";
        //											break;
        //										}
        //										default:
        //										{
        //											folderName = CommonConstants.GetFolderName(pProjectID);
        //											str = folderName;
        //											return str;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else if (productID == ProductID.PRODUCT_FL050_V2)
        //				{
        //					folderName = "FL050V2";
        //				}
        //				else if (productID == ProductID.PRODUCT_FL055)
        //				{
        //					folderName = "FL0550404P0802U";
        //				}
        //				else
        //				{
        //					if (productID != ProductID.PRODUCT_FP4020MR_L0808R_S3)
        //					{
        //						folderName = CommonConstants.GetFolderName(pProjectID);
        //						str = folderName;
        //						return str;
        //					}
        //					folderName = "FP4020S3";
        //				}
        //			}
        //			else if (productID > ProductID.PRODUCT_TRPMIU0500E)
        //			{
        //				if (productID == ProductID.PRODUCT_TRPMIU0700E)
        //				{
        //					folderName = "TRP0700E";
        //				}
        //				else
        //				{
        //					switch (productID)
        //					{
        //						case ProductID.PRODUCT_FH9035T_E:
        //						{
        //							folderName = "FH9035TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FH9035T:
        //						{
        //							folderName = "FH9035T";
        //							break;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case ProductID.PRODUCT_FH9057T_E:
        //								{
        //									folderName = "FH9057TE";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FH9057T:
        //								{
        //									folderName = "FH9057T";
        //									break;
        //								}
        //								default:
        //								{
        //									folderName = CommonConstants.GetFolderName(pProjectID);
        //									str = folderName;
        //									return str;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (productID == ProductID.PRODUCT_TRPMIU0400E)
        //			{
        //				folderName = "TRP0400E";
        //			}
        //			else if (productID == ProductID.PRODUCT_TRPMIU0500A)
        //			{
        //				folderName = "TRP0500A";
        //			}
        //			else
        //			{
        //				if (productID != ProductID.PRODUCT_TRPMIU0500E)
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //				folderName = "TRP0500E";
        //			}
        //		}
        //		else if (productID <= ProductID.PRODUCT_OIS60_Plus)
        //		{
        //			if (productID > ProductID.PRODUCT_HMC7070A_M)
        //			{
        //				if (productID == ProductID.PRODUCT_OIS55_Plus)
        //				{
        //					folderName = "OIS55P";
        //				}
        //				else
        //				{
        //					switch (productID)
        //					{
        //						case ProductID.PRODUCT_OIS45_Plus:
        //						{
        //							folderName = "OIS45P";
        //							break;
        //						}
        //						case ProductID.PRODUCT_OIS45E_Plus:
        //						{
        //							folderName = "OIS45EP";
        //							break;
        //						}
        //						default:
        //						{
        //							if (productID == ProductID.PRODUCT_OIS60_Plus)
        //							{
        //								folderName = "OIS60P";
        //								break;
        //							}
        //							else
        //							{
        //								folderName = CommonConstants.GetFolderName(pProjectID);
        //								str = folderName;
        //								return str;
        //							}
        //						}
        //					}
        //				}
        //			}
        //			else if (productID == ProductID.PRODUCT_HMC7043A_M)
        //			{
        //				folderName = "HMC7043M";
        //			}
        //			else if (productID == ProductID.PRODUCT_HMC7035A_M)
        //			{
        //				folderName = "HMC7035M";
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case ProductID.PRODUCT_HMC7057A_M:
        //					{
        //						folderName = "HMC7057M";
        //						break;
        //					}
        //					case 1572:
        //					{
        //						folderName = CommonConstants.GetFolderName(pProjectID);
        //						str = folderName;
        //						return str;
        //					}
        //					case ProductID.PRODUCT_HMC7070A_M:
        //					{
        //						folderName = "HMC7070M";
        //						break;
        //					}
        //					default:
        //					{
        //						folderName = CommonConstants.GetFolderName(pProjectID);
        //						str = folderName;
        //						return str;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= ProductID.PRODUCT_FP3102TN_E)
        //		{
        //			switch (productID)
        //			{
        //				case ProductID.PRODUCT_OIS70_Plus:
        //				{
        //					folderName = "OIS70P";
        //					break;
        //				}
        //				case ProductID.PRODUCT_OIS70E_Plus:
        //				{
        //					folderName = "OIS70EP";
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == ProductID.PRODUCT_OIS120A)
        //					{
        //						folderName = "OIS120A";
        //						break;
        //					}
        //					else
        //					{
        //						switch (productID)
        //						{
        //							case ProductID.PRODUCT_FP3043T:
        //							{
        //								folderName = "FP3043T";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3043T_E:
        //							{
        //								folderName = "FP3043TE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3043TN:
        //							{
        //								folderName = "FP3043TN";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3043TN_E:
        //							{
        //								folderName = "FP3043TNE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM545 | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_545_1 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus | ProductID.PRODUCT_OIS70E_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM760n | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_545_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM300 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM545 | ProductID.PRODUCT_PRIZM760n | ProductID.PRODUCT_PRIZM760 | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_3 | ProductID.PRODUCT_HIO_545_1 | ProductID.PRODUCT_HIO_545_2 | ProductID.PRODUCT_HIO_545_3 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus | ProductID.PRODUCT_OIS70E_Plus | ProductID.PRODUCT_HMC7057A_M:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_HIO_545_4 | ProductID.PRODUCT_PRIZM_760_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM545 | ProductID.PRODUCT_PRIZM760E | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_HIO_545_1 | ProductID.PRODUCT_HIO_545_4 | ProductID.PRODUCT_PRIZM_760_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP3043T | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus | ProductID.PRODUCT_OIS70E_Plus | ProductID.PRODUCT_HMC7070A_M:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM290N | ProductID.PRODUCT_PRIZM550N | ProductID.PRODUCT_PRIZM760n | ProductID.PRODUCT_PRIZM760nk | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_HIO_545_2 | ProductID.PRODUCT_HIO_545_4 | ProductID.PRODUCT_PRIZM_760_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3043T_E | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP3070T_E:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP3043T | ProductID.PRODUCT_FP3070TN | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_OIS120A | ProductID.PRODUCT_HMC7070A_M:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM290N | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_FP3043T_E | ProductID.PRODUCT_GTXL07N:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM300 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM290N | ProductID.PRODUCT_PRIZM290E | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_3 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP4020M_L0808R_A | ProductID.PRODUCT_FP3043T | ProductID.PRODUCT_FP3043TN | ProductID.PRODUCT_FP3070TN | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_FP3070TN_E | ProductID.PRODUCT_FP3043T_E | ProductID.PRODUCT_OIS120A | ProductID.PRODUCT_HMC7057A_M | ProductID.PRODUCT_HMC7043A_M | ProductID.PRODUCT_HMC7070A_M | ProductID.PRODUCT_GTXL07N:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							{
        //								folderName = CommonConstants.GetFolderName(pProjectID);
        //								str = folderName;
        //								return str;
        //							}
        //							case ProductID.PRODUCT_FP3070T:
        //							{
        //								folderName = "FP3070T";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3070T_E:
        //							{
        //								folderName = "FP3070TE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3070TN:
        //							{
        //								folderName = "FP3070TN";
        //								break;
        //							}
        //							case ProductID.PRODUCT_GTXL07N:
        //							{
        //								folderName = "GTXL07N";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3070TN_E:
        //							{
        //								folderName = "FP3070TNE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102T:
        //							{
        //								folderName = "FP3102T";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102T_E:
        //							{
        //								folderName = "FP3102TE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102TN:
        //							{
        //								folderName = "FP3102TN";
        //								break;
        //							}
        //							case ProductID.PRODUCT_GTXL10N:
        //							{
        //								folderName = "GTXL10N";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102TN_E:
        //							{
        //								folderName = "FP3102TNE";
        //								break;
        //							}
        //							default:
        //							{
        //								folderName = CommonConstants.GetFolderName(pProjectID);
        //								str = folderName;
        //								return str;
        //							}
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (productID <= ProductID.PRODUCT_OIS72E_Plus)
        //		{
        //			if (productID == ProductID.PRODUCT_OIS43E_Plus)
        //			{
        //				folderName = "OIS43EP";
        //			}
        //			else
        //			{
        //				if (productID != ProductID.PRODUCT_OIS72E_Plus)
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //				folderName = "OIS72EP";
        //			}
        //		}
        //		else if (productID == ProductID.PRODUCT_OIS100E_Plus)
        //		{
        //			folderName = "OIS100EP";
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case ProductID.PRODUCT_HH5P_H43_NS:
        //				{
        //					folderName = "HH5P-H43-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H43_S:
        //				{
        //					folderName = "HH5P-H43-S";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H70_NS:
        //				{
        //					folderName = "HH5P-H70-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H70_S:
        //				{
        //					folderName = "HH5P-H70-S";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H100_NS:
        //				{
        //					folderName = "HH5P-H100-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H100_S:
        //				{
        //					folderName = "HH5P-H100-S";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_HP200808D_P:
        //				case ProductID.PRODUCT_HH5P_HP301208D_R:
        //				case ProductID.PRODUCT_HH5P_HP300201U0808_RP:
        //				case ProductID.PRODUCT_HH5P_HP300201L0808_RP:
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //				case ProductID.PRODUCT_HH5P_HP43_NS:
        //				{
        //					folderName = "HH5P-HP43-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_HP70_NS:
        //				{
        //					folderName = "HH5P-HP70-NS";
        //					break;
        //				}
        //				default:
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //			}
        //		}
        //		str = folderName;
        //		return str;
        //	}

        //	public static string GetProductNameForUSBHostFileName(int pProjectID)
        //	{
        //		string str;
        //		string folderName = "";
        //		ProductID productID = (ProductID)pProjectID;
        //		if (productID <= ProductID.PRODUCT_FH9057T)
        //		{
        //			if (productID <= ProductID.PRODUCT_TRPMIU0300E)
        //			{
        //				if (productID > ProductID.PRODUCT_FP4020MR_L0808R_S3)
        //				{
        //					switch (productID)
        //					{
        //						case ProductID.PRODUCT_FP4035T_E:
        //						{
        //							folderName = "FP4035TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4035T:
        //						{
        //							folderName = "FP4035T";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4035TN:
        //						{
        //							folderName = "FP4035TN";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4035TN_E:
        //						{
        //							folderName = "FP4035TNE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4035TN:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E:
        //						case ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0:
        //						case ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP5043T_E:
        //						case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043T_E:
        //						case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E:
        //						case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4030MN_E | ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035T | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5043TN_E:
        //						case ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MT_L0808RP_A0201:
        //						case ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0:
        //						{
        //							folderName = CommonConstants.GetFolderName(pProjectID);
        //							str = folderName;
        //							return str;
        //						}
        //						case ProductID.PRODUCT_FP5043T_E:
        //						{
        //							folderName = "FP5043TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP5043T:
        //						{
        //							folderName = "FP5043T";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP5043TN:
        //						{
        //							folderName = "FP5043TN";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP5043TN_E:
        //						{
        //							folderName = "FP5043TNE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_E:
        //						{
        //							folderName = "FP4057TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T:
        //						{
        //							folderName = "FP4057T";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_S2:
        //						{
        //							folderName = "FP4057TS2";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057TN:
        //						{
        //							folderName = "FP4057TN";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057TN_E:
        //						{
        //							folderName = "FP4057TNE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_E_S1:
        //						{
        //							folderName = "FP4057TES1";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FP4057T_E_VERTICAL:
        //						{
        //							folderName = "FP4057TE";
        //							break;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case ProductID.PRODUCT_FP5070T_E:
        //								{
        //									folderName = "FP5070TE";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070T:
        //								{
        //									folderName = "FP5070T";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070TN:
        //								{
        //									folderName = "FP5070TN";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070TN_E:
        //								{
        //									folderName = "FP5070TNE";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5070T_E_S2:
        //								{
        //									folderName = "FP5070TES2";
        //									break;
        //								}
        //								case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_E | ProductID.PRODUCT_FP4030MR_L1208R | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_HORIZONTAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP4057T | ProductID.PRODUCT_FP4057T_E | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5043TN_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070TN_E | ProductID.PRODUCT_FP5070T_E_S2 | ProductID.PRODUCT_FP3035T_24:
        //								case ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP3035T_5:
        //								case ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_E | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MR_L1210P_A0402U | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_S1_HORIZONTAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057TN | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070TN_E | ProductID.PRODUCT_FP3035T_5:
        //								case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_L1208R | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_S1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP4057T_E | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057TN_E | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070T_E | ProductID.PRODUCT_FP5070T_E_S2 | ProductID.PRODUCT_FP3035T_24 | ProductID.PRODUCT_FP3035T_5:
        //								case ProductID.PRODUCT_PZ4030M_E | ProductID.PRODUCT_PZ4030MN_E | ProductID.PRODUCT_PZ4035TN_E | ProductID.PRODUCT_PZ4057M_E | ProductID.PRODUCT_PZ4057TN_E | ProductID.PRODUCT_PZ4084TN_E | ProductID.PRODUCT_PZ4121TN_E | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR | ProductID.PRODUCT_FP4020MR_L0808P | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4030MR_E | ProductID.PRODUCT_FP4030MR_L1208R | ProductID.PRODUCT_FP4030MR_0808R_A0400_S0 | ProductID.PRODUCT_FP4030MR_L1210RP_A0402U | ProductID.PRODUCT_FP4030MR_L1210P_A0402U | ProductID.PRODUCT_FP4030MR_L1210RP | ProductID.PRODUCT_FP4030MR_L1210P | ProductID.PRODUCT_FP4030MR_L0808R_A0400U | ProductID.PRODUCT_FP4030MT_HORIZONTAL | ProductID.PRODUCT_FP4030MT_VERTICAL | ProductID.PRODUCT_FP4030MT_S1_HORIZONTAL | ProductID.PRODUCT_FP4030MT_S1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201 | ProductID.PRODUCT_FP4030MT_L0808RP_A0201 | ProductID.PRODUCT_FP4030MT_REV1 | ProductID.PRODUCT_FP4030MT_L0808RN | ProductID.PRODUCT_FP4030MT_L0808RP | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L | ProductID.PRODUCT_FP4030MT_L0808RP_A0201L_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_S0 | ProductID.PRODUCT_FP4030MT_REV1_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_A0201_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RN_VERTICAL | ProductID.PRODUCT_FP4030MT_L0808RP_VERTICAL | ProductID.PRODUCT_FP4035T | ProductID.PRODUCT_FP4035T_E | ProductID.PRODUCT_FP4035TN | ProductID.PRODUCT_FP4035TN_E | ProductID.PRODUCT_PRIZM_710_S0 | ProductID.PRODUCT_FP4057T | ProductID.PRODUCT_FP4057T_E | ProductID.PRODUCT_FP4057T_S2 | ProductID.PRODUCT_FP4057TN | ProductID.PRODUCT_FP4057TN_E | ProductID.PRODUCT_FP4057T_E_S1 | ProductID.PRODUCT_FP4057T_E_VERTICAL | ProductID.PRODUCT_FP4020M_L0808P_A | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP4020M_L0808R_A | ProductID.PRODUCT_FP5043T | ProductID.PRODUCT_FP5043TN | ProductID.PRODUCT_FP5043T_E | ProductID.PRODUCT_FP5043TN_E | ProductID.PRODUCT_FP5070T | ProductID.PRODUCT_FP5070TN | ProductID.PRODUCT_FP5070T_E | ProductID.PRODUCT_FP5070TN_E | ProductID.PRODUCT_FP5070T_E_S2 | ProductID.PRODUCT_FP3035T_24 | ProductID.PRODUCT_FP3035T_5:
        //								{
        //									folderName = CommonConstants.GetFolderName(pProjectID);
        //									str = folderName;
        //									return str;
        //								}
        //								case ProductID.PRODUCT_FP5121T:
        //								{
        //									folderName = "FP5121T";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5121TN:
        //								{
        //									folderName = "FP5121TN";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FP5121TN_S0:
        //								{
        //									folderName = "FP5121TN-S0";
        //									break;
        //								}
        //								default:
        //								{
        //									switch (productID)
        //									{
        //										case ProductID.PRODUCT_TRPMIU0300A:
        //										{
        //											folderName = "TRP0300A";
        //											break;
        //										}
        //										case 1332:
        //										{
        //											folderName = CommonConstants.GetFolderName(pProjectID);
        //											str = folderName;
        //											return str;
        //										}
        //										case ProductID.PRODUCT_TRPMIU0300E:
        //										{
        //											folderName = "TRP0300E";
        //											break;
        //										}
        //										default:
        //										{
        //											folderName = CommonConstants.GetFolderName(pProjectID);
        //											str = folderName;
        //											return str;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else if (productID == ProductID.PRODUCT_FL050_V2)
        //				{
        //					folderName = "FL050V2";
        //				}
        //				else if (productID == ProductID.PRODUCT_FL055)
        //				{
        //					folderName = "FL0550404P0802U";
        //				}
        //				else
        //				{
        //					if (productID != ProductID.PRODUCT_FP4020MR_L0808R_S3)
        //					{
        //						folderName = CommonConstants.GetFolderName(pProjectID);
        //						str = folderName;
        //						return str;
        //					}
        //					folderName = "FP4020S3";
        //				}
        //			}
        //			else if (productID > ProductID.PRODUCT_TRPMIU0500E)
        //			{
        //				if (productID == ProductID.PRODUCT_TRPMIU0700E)
        //				{
        //					folderName = "TRP0700E";
        //				}
        //				else
        //				{
        //					switch (productID)
        //					{
        //						case ProductID.PRODUCT_FH9035T_E:
        //						{
        //							folderName = "FH9035TE";
        //							break;
        //						}
        //						case ProductID.PRODUCT_FH9035T:
        //						{
        //							folderName = "FH9035T";
        //							break;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case ProductID.PRODUCT_FH9057T_E:
        //								{
        //									folderName = "FH9057TE";
        //									break;
        //								}
        //								case ProductID.PRODUCT_FH9057T:
        //								{
        //									folderName = "FH9057T";
        //									break;
        //								}
        //								default:
        //								{
        //									folderName = CommonConstants.GetFolderName(pProjectID);
        //									str = folderName;
        //									return str;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (productID == ProductID.PRODUCT_TRPMIU0400E)
        //			{
        //				folderName = "TRP0400E";
        //			}
        //			else if (productID == ProductID.PRODUCT_TRPMIU0500A)
        //			{
        //				folderName = "TRP0500A";
        //			}
        //			else
        //			{
        //				if (productID != ProductID.PRODUCT_TRPMIU0500E)
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //				folderName = "TRP0500E";
        //			}
        //		}
        //		else if (productID <= ProductID.PRODUCT_OIS60_Plus)
        //		{
        //			if (productID > ProductID.PRODUCT_HMC7070A_M)
        //			{
        //				if (productID == ProductID.PRODUCT_OIS55_Plus)
        //				{
        //					folderName = "OIS55P";
        //				}
        //				else
        //				{
        //					switch (productID)
        //					{
        //						case ProductID.PRODUCT_OIS45_Plus:
        //						{
        //							folderName = "OIS45P";
        //							break;
        //						}
        //						case ProductID.PRODUCT_OIS45E_Plus:
        //						{
        //							folderName = "OIS45EP";
        //							break;
        //						}
        //						default:
        //						{
        //							if (productID == ProductID.PRODUCT_OIS60_Plus)
        //							{
        //								folderName = "OIS60P";
        //								break;
        //							}
        //							else
        //							{
        //								folderName = CommonConstants.GetFolderName(pProjectID);
        //								str = folderName;
        //								return str;
        //							}
        //						}
        //					}
        //				}
        //			}
        //			else if (productID == ProductID.PRODUCT_HMC7043A_M)
        //			{
        //				folderName = "HMC7043M";
        //			}
        //			else if (productID == ProductID.PRODUCT_HMC7035A_M)
        //			{
        //				folderName = "HMC7035M";
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case ProductID.PRODUCT_HMC7057A_M:
        //					{
        //						folderName = "HMC7057M";
        //						break;
        //					}
        //					case 1572:
        //					{
        //						folderName = CommonConstants.GetFolderName(pProjectID);
        //						str = folderName;
        //						return str;
        //					}
        //					case ProductID.PRODUCT_HMC7070A_M:
        //					{
        //						folderName = "HMC7070M";
        //						break;
        //					}
        //					default:
        //					{
        //						folderName = CommonConstants.GetFolderName(pProjectID);
        //						str = folderName;
        //						return str;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= ProductID.PRODUCT_FP3102TN_E)
        //		{
        //			switch (productID)
        //			{
        //				case ProductID.PRODUCT_OIS70_Plus:
        //				{
        //					folderName = "OIS70P";
        //					break;
        //				}
        //				case ProductID.PRODUCT_OIS70E_Plus:
        //				{
        //					folderName = "OIS70EP";
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == ProductID.PRODUCT_OIS120A)
        //					{
        //						folderName = "OIS120A";
        //						break;
        //					}
        //					else
        //					{
        //						switch (productID)
        //						{
        //							case ProductID.PRODUCT_FP3043T:
        //							{
        //								folderName = "FP3043T";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3043T_E:
        //							{
        //								folderName = "FP3043TE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3043TN:
        //							{
        //								folderName = "FP3043TN";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3043TN_E:
        //							{
        //								folderName = "FP3043TNE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM545 | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_545_1 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus | ProductID.PRODUCT_OIS70E_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM760n | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_545_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM300 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM545 | ProductID.PRODUCT_PRIZM760n | ProductID.PRODUCT_PRIZM760 | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_3 | ProductID.PRODUCT_HIO_545_1 | ProductID.PRODUCT_HIO_545_2 | ProductID.PRODUCT_HIO_545_3 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus | ProductID.PRODUCT_OIS70E_Plus | ProductID.PRODUCT_HMC7057A_M:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_HIO_545_4 | ProductID.PRODUCT_PRIZM_760_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM545 | ProductID.PRODUCT_PRIZM760E | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_HIO_545_1 | ProductID.PRODUCT_HIO_545_4 | ProductID.PRODUCT_PRIZM_760_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP3043T | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus | ProductID.PRODUCT_OIS70E_Plus | ProductID.PRODUCT_HMC7070A_M:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM290N | ProductID.PRODUCT_PRIZM550N | ProductID.PRODUCT_PRIZM760n | ProductID.PRODUCT_PRIZM760nk | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_HIO_545_2 | ProductID.PRODUCT_HIO_545_4 | ProductID.PRODUCT_PRIZM_760_2 | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3043T_E | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP3070T_E:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP3043T | ProductID.PRODUCT_FP3070TN | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_OIS120A | ProductID.PRODUCT_HMC7070A_M:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM290N | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_FP3043T_E | ProductID.PRODUCT_GTXL07N:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PLC_CARD | ProductID.PRODUCT_PRIZM300 | ProductID.PRODUCT_PRIZM285 | ProductID.PRODUCT_PRIZM290N | ProductID.PRODUCT_PRIZM290E | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_HIO_285_1 | ProductID.PRODUCT_HIO_285_2 | ProductID.PRODUCT_HIO_285_3 | ProductID.PRODUCT_HIO_285_4 | ProductID.PRODUCT_FP2020_L0808RP_A0401L | ProductID.PRODUCT_FP2020_L0808P_A0401L | ProductID.PRODUCT_FP2020_L0604P_A0401L | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4020MR_L0808R | ProductID.PRODUCT_FP4020MR_L0808R_S3 | ProductID.PRODUCT_FP3020MR_L1608RP | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP4020M_L0808P_A | ProductID.PRODUCT_FP4020M_L0808P_A0400R | ProductID.PRODUCT_FP4020M_L0808N_A | ProductID.PRODUCT_FP4020M_L0808N_AR | ProductID.PRODUCT_FP4020M_L0808R_A | ProductID.PRODUCT_FP3043T | ProductID.PRODUCT_FP3043TN | ProductID.PRODUCT_FP3070TN | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_FP3070TN_E | ProductID.PRODUCT_FP3043T_E | ProductID.PRODUCT_OIS120A | ProductID.PRODUCT_HMC7057A_M | ProductID.PRODUCT_HMC7043A_M | ProductID.PRODUCT_HMC7070A_M | ProductID.PRODUCT_GTXL07N:
        //							case ProductID.PRODUCT_PRIZM230 | ProductID.PRODUCT_PRIZM720N | ProductID.PRODUCT_FP4020MR_L0808N | ProductID.PRODUCT_FP4030MR | ProductID.PRODUCT_FP3070T_E | ProductID.PRODUCT_FP3043TN_E | ProductID.PRODUCT_OIS70_Plus:
        //							{
        //								folderName = CommonConstants.GetFolderName(pProjectID);
        //								str = folderName;
        //								return str;
        //							}
        //							case ProductID.PRODUCT_FP3070T:
        //							{
        //								folderName = "FP3070T";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3070T_E:
        //							{
        //								folderName = "FP3070TE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3070TN:
        //							{
        //								folderName = "FP3070TN";
        //								break;
        //							}
        //							case ProductID.PRODUCT_GTXL07N:
        //							{
        //								folderName = "GTXL07N";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3070TN_E:
        //							{
        //								folderName = "FP3070TNE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102T:
        //							{
        //								folderName = "FP3102T";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102T_E:
        //							{
        //								folderName = "FP3102TE";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102TN:
        //							{
        //								folderName = "FP3102TN";
        //								break;
        //							}
        //							case ProductID.PRODUCT_GTXL10N:
        //							{
        //								folderName = "GTXL10N";
        //								break;
        //							}
        //							case ProductID.PRODUCT_FP3102TN_E:
        //							{
        //								folderName = "FP3102TNE";
        //								break;
        //							}
        //							default:
        //							{
        //								folderName = CommonConstants.GetFolderName(pProjectID);
        //								str = folderName;
        //								return str;
        //							}
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (productID <= ProductID.PRODUCT_OIS72E_Plus)
        //		{
        //			if (productID == ProductID.PRODUCT_OIS43E_Plus)
        //			{
        //				folderName = "OIS43EP";
        //			}
        //			else
        //			{
        //				if (productID != ProductID.PRODUCT_OIS72E_Plus)
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //				folderName = "OIS72EP";
        //			}
        //		}
        //		else if (productID == ProductID.PRODUCT_OIS100E_Plus)
        //		{
        //			folderName = "OIS100EP";
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case ProductID.PRODUCT_HH5P_H43_NS:
        //				{
        //					folderName = "HH5P-H43-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H43_S:
        //				{
        //					folderName = "HH5P-H43-S";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H70_NS:
        //				{
        //					folderName = "HH5P-H70-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H70_S:
        //				{
        //					folderName = "HH5P-H70-S";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H100_NS:
        //				{
        //					folderName = "HH5P-H100-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_H100_S:
        //				{
        //					folderName = "HH5P-H100-S";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_HP200808D_P:
        //				case ProductID.PRODUCT_HH5P_HP301208D_R:
        //				case ProductID.PRODUCT_HH5P_HP300201U0808_RP:
        //				case ProductID.PRODUCT_HH5P_HP300201L0808_RP:
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //				case ProductID.PRODUCT_HH5P_HP43_NS:
        //				{
        //					folderName = "HH5P-HP43-NS";
        //					break;
        //				}
        //				case ProductID.PRODUCT_HH5P_HP70_NS:
        //				{
        //					folderName = "HH5P-HP70-NS";
        //					break;
        //				}
        //				default:
        //				{
        //					folderName = CommonConstants.GetFolderName(pProjectID);
        //					str = folderName;
        //					return str;
        //				}
        //			}
        //		}
        //		str = folderName;
        //		return str;
        //	}

        //	public static int GetRowNumber(string Model, string SubModel, string TableName)
        //	{
        //		int num = 0;
        //		DataTable item = CommonConstants.dsRecentProjectList.Tables[TableName];
        //		int num1 = 0;
        //		while (num1 < item.Rows.Count)
        //		{
        //			if ((Model != item.Rows[num1]["ModelSeries"].ToString() ? true : !(SubModel == item.Rows[num1]["Model"].ToString())))
        //			{
        //				num1++;
        //			}
        //			else
        //			{
        //				num = num1;
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static int GetScreenIndex(List<CommonConstants.ScreenInfo> pScreenInfoList, ushort pScreenNumber)
        //	{
        //		int num;
        //		int num1 = 0;
        //		while (true)
        //		{
        //			if (num1 >= pScreenInfoList.Count)
        //			{
        //				num = -1;
        //				break;
        //			}
        //			else if (pScreenInfoList[num1].usScrNumber != pScreenNumber)
        //			{
        //				num1++;
        //			}
        //			else
        //			{
        //				num = num1;
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static string GetSeparateTagNameFromTagList(string pstrTag)
        //	{
        //		string str;
        //		string str1 = pstrTag;
        //		if (!CommonConstants.g_Support_IEC_Ladder)
        //		{
        //			int num = str1.IndexOf("(") + 1;
        //			string str2 = str1.Substring(num, str1.LastIndexOf(")") - num);
        //			str = str2;
        //		}
        //		else
        //		{
        //			str = str1;
        //		}
        //		return str;
        //	}

        //	public static byte GetShapeDefaultHeightSize(byte pShapeID)
        //	{
        //		byte num = 10;
        //		switch (pShapeID)
        //		{
        //			case 1:
        //			{
        //				num = (byte)((!CommonConstants.IsProductIsTextBased(CommonConstants.ProductDataInfo.iProductID) ? 8 : 16));
        //				break;
        //			}
        //			case 2:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 3:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 4:
        //			{
        //				num = 17;
        //				break;
        //			}
        //			case 5:
        //			{
        //				num = 13;
        //				break;
        //			}
        //			case 6:
        //			{
        //				num = 17;
        //				break;
        //			}
        //			case 7:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 8:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 9:
        //			{
        //				if (!CommonConstants.IsProductIsTextBased(CommonConstants.ProductDataInfo.iProductID))
        //				{
        //					num = (byte)((!CommonConstants.IsProductIsTextAndGraphicsBased(CommonConstants.ProductDataInfo.iProductID) ? 100 : 40));
        //				}
        //				else
        //				{
        //					num = 16;
        //				}
        //				break;
        //			}
        //			case 10:
        //			{
        //				if (!CommonConstants.IsProductIsTextBased(CommonConstants.ProductDataInfo.iProductID))
        //				{
        //					num = (byte)((!CommonConstants.IsProductIsTextAndGraphicsBased(CommonConstants.ProductDataInfo.iProductID) ? 100 : 28));
        //				}
        //				else
        //				{
        //					num = 16;
        //				}
        //				break;
        //			}
        //			case 11:
        //			case 27:
        //			case 32:
        //			case 39:
        //			case 51:
        //			case 54:
        //			case 59:
        //			case 62:
        //			case 63:
        //			case 64:
        //			case 65:
        //			case 66:
        //			case 67:
        //			case 68:
        //			case 69:
        //			case 70:
        //			case 71:
        //			case 72:
        //			case 73:
        //			case 74:
        //			case 76:
        //			case 77:
        //			case 78:
        //			case 80:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 12:
        //			{
        //				num = 20;
        //				break;
        //			}
        //			case 13:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 40 : 20));
        //				break;
        //			}
        //			case 14:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 40 : 20));
        //				break;
        //			}
        //			case 15:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 40 : 30));
        //				break;
        //			}
        //			case 16:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 100 : 20));
        //				break;
        //			}
        //			case 17:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 40 : 16));
        //				break;
        //			}
        //			case 18:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 40 : 16));
        //				break;
        //			}
        //			case 19:
        //			{
        //				num = 17;
        //				break;
        //			}
        //			case 20:
        //			{
        //				if (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID))
        //				{
        //					num = 78;
        //				}
        //				else
        //				{
        //					num = (byte)(((CommonConstants.IsProductSupports16GrayScale(CommonConstants.ProductDataInfo.iProductID) ? false : !CommonConstants.IsProductSupports2Color(CommonConstants.ProductDataInfo.iProductID)) ? 35 : 45));
        //				}
        //				break;
        //			}
        //			case 21:
        //			{
        //				num = (byte)((!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 115 : 55));
        //				break;
        //			}
        //			case 22:
        //			{
        //				num = 122;
        //				break;
        //			}
        //			case 23:
        //			{
        //				num = 130;
        //				break;
        //			}
        //			case 24:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 25:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 26:
        //			{
        //				num = 125;
        //				break;
        //			}
        //			case 28:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 29:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 30:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 31:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 33:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 34:
        //			{
        //				num = 130;
        //				break;
        //			}
        //			case 35:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 36:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 37:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 38:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 40:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 41:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 42:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 43:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 44:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 45:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 46:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 47:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 48:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 49:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 50:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 52:
        //			{
        //				num = 196;
        //				break;
        //			}
        //			case 53:
        //			case 81:
        //			{
        //				num = 130;
        //				break;
        //			}
        //			case 55:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 56:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 57:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 58:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 60:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 61:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			case 75:
        //			{
        //				num = 100;
        //				break;
        //			}
        //			case 79:
        //			{
        //				num = 125;
        //				break;
        //			}
        //			case 82:
        //			{
        //				num = 200;
        //				break;
        //			}
        //			case 83:
        //			{
        //				num = 40;
        //				break;
        //			}
        //			default:
        //			{
        //				goto case 80;
        //			}
        //		}
        //		return num;
        //	}

        //	public static int GetShapeDefaultWidthSize(byte pShapeID)
        //	{
        //		int num = 10;
        //		switch (pShapeID)
        //		{
        //			case 1:
        //			{
        //				num = (!CommonConstants.IsFixedGridSizeProduct(CommonConstants.ProductDataInfo.iProductID) ? 24 : 48);
        //				break;
        //			}
        //			case 2:
        //			{
        //				num = 45;
        //				break;
        //			}
        //			case 3:
        //			{
        //				num = 45;
        //				break;
        //			}
        //			case 4:
        //			{
        //				num = 30;
        //				break;
        //			}
        //			case 5:
        //			{
        //				num = 45;
        //				break;
        //			}
        //			case 6:
        //			{
        //				num = 30;
        //				break;
        //			}
        //			case 7:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 8:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 9:
        //			{
        //				if (!CommonConstants.IsProductIsTextBased(CommonConstants.ProductDataInfo.iProductID))
        //				{
        //					num = (!CommonConstants.IsProductIsTextAndGraphicsBased(CommonConstants.ProductDataInfo.iProductID) ? 40 : 20);
        //				}
        //				else
        //				{
        //					num = 48;
        //				}
        //				break;
        //			}
        //			case 10:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 100 : 70);
        //				break;
        //			}
        //			case 11:
        //			case 27:
        //			case 32:
        //			case 39:
        //			case 51:
        //			case 54:
        //			case 59:
        //			case 62:
        //			case 63:
        //			case 64:
        //			case 65:
        //			case 66:
        //			case 67:
        //			case 68:
        //			case 69:
        //			case 70:
        //			case 71:
        //			case 72:
        //			case 73:
        //			case 74:
        //			case 76:
        //			case 77:
        //			case 78:
        //			case 80:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 12:
        //			{
        //				num = 20;
        //				break;
        //			}
        //			case 13:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 40 : 20);
        //				break;
        //			}
        //			case 14:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 70 : 20);
        //				break;
        //			}
        //			case 15:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 50 : 30);
        //				break;
        //			}
        //			case 16:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 100 : 20);
        //				break;
        //			}
        //			case 17:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 45 : 30);
        //				break;
        //			}
        //			case 18:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 130 : 46);
        //				break;
        //			}
        //			case 19:
        //			{
        //				num = 82;
        //				break;
        //			}
        //			case 20:
        //			{
        //				if (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID))
        //				{
        //					num = 125;
        //				}
        //				else
        //				{
        //					num = ((CommonConstants.IsProductSupports16GrayScale(CommonConstants.ProductDataInfo.iProductID) ? false : !CommonConstants.IsProductSupports2Color(CommonConstants.ProductDataInfo.iProductID)) ? 85 : 80);
        //				}
        //				break;
        //			}
        //			case 21:
        //			{
        //				num = (!CommonConstants.IsProductCompatibleWith4030(CommonConstants.ProductDataInfo.iProductID) ? 150 : 118);
        //				break;
        //			}
        //			case 22:
        //			{
        //				num = 185;
        //				break;
        //			}
        //			case 23:
        //			{
        //				num = 150;
        //				break;
        //			}
        //			case 24:
        //			{
        //				num = 45;
        //				break;
        //			}
        //			case 25:
        //			{
        //				num = 140;
        //				break;
        //			}
        //			case 26:
        //			{
        //				num = 200;
        //				break;
        //			}
        //			case 28:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 29:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 30:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 31:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 33:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 34:
        //			{
        //				num = 150;
        //				break;
        //			}
        //			case 35:
        //			{
        //				num = 55;
        //				break;
        //			}
        //			case 36:
        //			{
        //				num = 55;
        //				break;
        //			}
        //			case 37:
        //			{
        //				num = 75;
        //				break;
        //			}
        //			case 38:
        //			{
        //				num = 60;
        //				break;
        //			}
        //			case 40:
        //			{
        //				num = 50;
        //				break;
        //			}
        //			case 41:
        //			{
        //				num = 60;
        //				break;
        //			}
        //			case 42:
        //			{
        //				num = 65;
        //				break;
        //			}
        //			case 43:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 44:
        //			{
        //				num = 10;
        //				break;
        //			}
        //			case 45:
        //			{
        //				num = 155;
        //				break;
        //			}
        //			case 46:
        //			{
        //				num = 155;
        //				break;
        //			}
        //			case 47:
        //			{
        //				num = 165;
        //				break;
        //			}
        //			case 48:
        //			{
        //				num = 160;
        //				break;
        //			}
        //			case 49:
        //			{
        //				num = 170;
        //				break;
        //			}
        //			case 50:
        //			{
        //				num = 71;
        //				break;
        //			}
        //			case 52:
        //			{
        //				num = 255;
        //				break;
        //			}
        //			case 53:
        //			case 81:
        //			{
        //				num = 150;
        //				break;
        //			}
        //			case 55:
        //			{
        //				num = 90;
        //				break;
        //			}
        //			case 56:
        //			{
        //				num = 120;
        //				break;
        //			}
        //			case 57:
        //			{
        //				num = 90;
        //				break;
        //			}
        //			case 58:
        //			{
        //				num = 110;
        //				break;
        //			}
        //			case 60:
        //			{
        //				num = 130;
        //				break;
        //			}
        //			case 61:
        //			{
        //				num = 125;
        //				break;
        //			}
        //			case 75:
        //			{
        //				num = 100;
        //				break;
        //			}
        //			case 79:
        //			{
        //				num = 200;
        //				break;
        //			}
        //			case 82:
        //			{
        //				num = 450;
        //				break;
        //			}
        //			case 83:
        //			{
        //				num = 90;
        //				break;
        //			}
        //			default:
        //			{
        //				goto case 80;
        //			}
        //		}
        //		return num;
        //	}

        //	public static int GetShapeIndex(ArrayList pShapeList, uint pObjectID)
        //	{
        //		int num;
        //		int num1 = 0;
        //		while (true)
        //		{
        //			if (num1 >= pShapeList.Count)
        //			{
        //				num = -1;
        //				break;
        //			}
        //			else if (((Shape)pShapeList[num1]).ObjectID != pObjectID)
        //			{
        //				num1++;
        //			}
        //			else
        //			{
        //				num = num1;
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static string GetShapeName(int ShapeId)
        //	{
        //		string str;
        //		string str1 = "";
        //		int shapeId = ShapeId;
        //		if (shapeId > 46)
        //		{
        //			switch (shapeId)
        //			{
        //				case 61:
        //				{
        //					str1 = "Bit Button";
        //					break;
        //				}
        //				case 62:
        //				{
        //					str1 = "Default";
        //					str = str1;
        //					return str;
        //				}
        //				case 63:
        //				{
        //					str1 = "Word Button";
        //					break;
        //				}
        //				case 64:
        //				{
        //					str1 = "Word Lamp";
        //					break;
        //				}
        //				case 65:
        //				{
        //					str1 = "Analogmeter";
        //					break;
        //				}
        //				case 66:
        //				{
        //					str1 = "Multiple Bargraph";
        //					break;
        //				}
        //				case 67:
        //				{
        //					str1 = "Keypad";
        //					break;
        //				}
        //				case 68:
        //				{
        //					str1 = "Trend";
        //					break;
        //				}
        //				case 69:
        //				{
        //					str1 = "Historical Trend";
        //					break;
        //				}
        //				default:
        //				{
        //					switch (shapeId)
        //					{
        //						case 90:
        //						{
        //							str1 = "Polyline";
        //							break;
        //						}
        //						case 91:
        //						{
        //							str1 = "Polygon";
        //							break;
        //						}
        //						case 92:
        //						{
        //							str1 = "Arc";
        //							break;
        //						}
        //						case 93:
        //						{
        //							str1 = "Pie";
        //							break;
        //						}
        //						case 94:
        //						{
        //							str1 = "Default";
        //							str = str1;
        //							return str;
        //						}
        //						case 95:
        //						{
        //							str1 = "Custom Keypad";
        //							break;
        //						}
        //						case 96:
        //						{
        //							str1 = "Picture";
        //							break;
        //						}
        //						case 97:
        //						{
        //							str1 = "XY Plot";
        //							break;
        //						}
        //						case 98:
        //						{
        //							str1 = "Advanced Custom keypad";
        //							break;
        //						}
        //						default:
        //						{
        //							str1 = "Default";
        //							str = str1;
        //							return str;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (shapeId)
        //			{
        //				case 0:
        //				{
        //					str1 = "Bitmap";
        //					break;
        //				}
        //				case 1:
        //				{
        //					str1 = "Text";
        //					break;
        //				}
        //				case 2:
        //				{
        //					str1 = "Coil Data Entry";
        //					break;
        //				}
        //				case 3:
        //				{
        //					str1 = "Register Data Entry";
        //					break;
        //				}
        //				case 4:
        //				{
        //					str1 = "Display Data Coil";
        //					break;
        //				}
        //				case 5:
        //				{
        //					str1 = "Display Data Register";
        //					break;
        //				}
        //				case 6:
        //				{
        //					str1 = "Message Display Data";
        //					break;
        //				}
        //				case 7:
        //				{
        //					str1 = "Time";
        //					break;
        //				}
        //				case 8:
        //				{
        //					str1 = "Date";
        //					break;
        //				}
        //				case 9:
        //				case 14:
        //				case 15:
        //				case 17:
        //				case 18:
        //				case 19:
        //				case 20:
        //				{
        //					str1 = "Default";
        //					str = str1;
        //					return str;
        //				}
        //				case 10:
        //				{
        //					str1 = "Rectangle";
        //					break;
        //				}
        //				case 11:
        //				{
        //					str1 = "Ellipse";
        //					break;
        //				}
        //				case 12:
        //				{
        //					str1 = "Round Rectangle";
        //					break;
        //				}
        //				case 13:
        //				{
        //					str1 = "Line";
        //					break;
        //				}
        //				case 16:
        //				{
        //					str1 = "Alarm";
        //					break;
        //				}
        //				case 21:
        //				{
        //					str1 = "Single Bargraph";
        //					break;
        //				}
        //				default:
        //				{
        //					switch (shapeId)
        //					{
        //						case 41:
        //						{
        //							str1 = "Mutilingual Text";
        //							break;
        //						}
        //						case 42:
        //						{
        //							str1 = "Keypad Password";
        //							break;
        //						}
        //						case 43:
        //						case 44:
        //						{
        //							str1 = "Default";
        //							str = str1;
        //							return str;
        //						}
        //						case 45:
        //						{
        //							str1 = "Edit Password";
        //							break;
        //						}
        //						case 46:
        //						{
        //							str1 = "Ascii Keypad";
        //							break;
        //						}
        //						default:
        //						{
        //							str1 = "Default";
        //							str = str1;
        //							return str;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		str = str1;
        //		return str;
        //	}

        //	public static int GetstratonDataTypeByte(string _StratonDataType, ref int dataTypebyte)
        //	{
        //		if (!(Convert.ToString(_StratonDataType) == "BOOL" || Convert.ToString(_StratonDataType) == "BYTE" || Convert.ToString(_StratonDataType) == "SINT" ? false : !(Convert.ToString(_StratonDataType) == "USINT")))
        //		{
        //			dataTypebyte = 1;
        //		}
        //		else if (!(Convert.ToString(_StratonDataType) == "INT" || Convert.ToString(_StratonDataType) == "UINT" ? false : !(Convert.ToString(_StratonDataType) == "WORD")))
        //		{
        //			dataTypebyte = 2;
        //		}
        //		else if (Convert.ToString(_StratonDataType) == "LREAL")
        //		{
        //			dataTypebyte = 8;
        //		}
        //		else if (!(Convert.ToString(_StratonDataType) == "DINT" || Convert.ToString(_StratonDataType) == "DWORD" || Convert.ToString(_StratonDataType) == "UDINT" || Convert.ToString(_StratonDataType) == "REAL" ? false : !(Convert.ToString(_StratonDataType) == "TIME")))
        //		{
        //			dataTypebyte = 4;
        //		}
        //		else if (Convert.ToString(_StratonDataType) == "STRING")
        //		{
        //		}
        //		return dataTypebyte;
        //	}

        //	public static void GetTagDataTypeRanges(TagType pTagType, byte pDataType, ref string pMinVal, ref string pMaxVal)
        //	{
        //		string str = "";
        //		string str1 = "";
        //		switch (pTagType)
        //		{
        //			case TagType.Byte:
        //			{
        //				switch (pDataType)
        //				{
        //					case 0:
        //					{
        //						str = "0";
        //						str1 = "255";
        //						break;
        //					}
        //					case 1:
        //					{
        //						str = "-127";
        //						str1 = "127";
        //						break;
        //					}
        //					case 2:
        //					{
        //						str = "0";
        //						str1 = "FF";
        //						break;
        //					}
        //					case 3:
        //					{
        //						str = "0";
        //						str1 = "99";
        //						break;
        //					}
        //					case 4:
        //					{
        //						str = "0";
        //						str1 = "255";
        //						break;
        //					}
        //				}
        //				break;
        //			}
        //			case TagType.Word:
        //			{
        //				switch (pDataType)
        //				{
        //					case 0:
        //					{
        //						str = "65535000";
        //						str1 = "65535000";
        //						break;
        //					}
        //					case 1:
        //					{
        //						str = "-32768000";
        //						str1 = "32767000";
        //						break;
        //					}
        //					case 2:
        //					{
        //						str = "0000000";
        //						str1 = "FFFFFFF";
        //						break;
        //					}
        //					case 3:
        //					{
        //						str = "9999000";
        //						str1 = "9999000";
        //						break;
        //					}
        //					case 4:
        //					{
        //						str = "655355";
        //						str1 = "655355";
        //						break;
        //					}
        //				}
        //				break;
        //			}
        //			case TagType.DoubleWord:
        //			{
        //				switch (pDataType)
        //				{
        //					case 0:
        //					{
        //						str = "4294967295000";
        //						str1 = "4294967295000";
        //						break;
        //					}
        //					case 1:
        //					{
        //						str = "-2147483648000";
        //						str1 = "2147483647000";
        //						break;
        //					}
        //					case 2:
        //					{
        //						str = "FFFFFFFFFFF";
        //						str1 = "FFFFFFFFFFF";
        //						break;
        //					}
        //					case 3:
        //					{
        //						str = "99999999999";
        //						str1 = "99999999999";
        //						break;
        //					}
        //					case 4:
        //					{
        //						str = "-999999999.0000";
        //						str1 = "-999999999.0000";
        //						break;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //		pMinVal = str;
        //		pMaxVal = str1;
        //	}

        //	public static double GetTagValueFromDataMonitorData(string strAddress)
        //	{
        //		double num;
        //		int count = 0;
        //		int num1 = 0;
        //		while (true)
        //		{
        //			if (num1 < CommonConstants.objListDataMonitorData.Count)
        //			{
        //				DmBlockInfo item = (DmBlockInfo)CommonConstants.objListDataMonitorData[num1];
        //				count = item.TagList.Count;
        //				int num2 = 0;
        //				while (num2 < count)
        //				{
        //					DmTagInfo dmTagInfo = (DmTagInfo)item.TagList[num2];
        //					if (!(dmTagInfo.strTagAddress == strAddress))
        //					{
        //						num2++;
        //					}
        //					else
        //					{
        //						num = dmTagInfo.doubleTagValue;
        //						return num;
        //					}
        //				}
        //				num1++;
        //			}
        //			else
        //			{
        //				num = 0;
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static string GetTaskName(TaskCode pTaskCode)
        //	{
        //		string str = CoreConstStrings.strGotoScreen;
        //		TaskCode taskCode = pTaskCode;
        //		if (taskCode <= TaskCode.CopyTagToLED)
        //		{
        //			if (taskCode <= TaskCode.ToggleBit)
        //			{
        //				if (taskCode > TaskCode.WriteValueToTag)
        //				{
        //					switch (taskCode)
        //					{
        //						case TaskCode.AddaConstValueToTag:
        //						{
        //							str = CoreConstStrings.strAddAConstantValueToATag;
        //							break;
        //						}
        //						case TaskCode.AddTagBToTagA:
        //						{
        //							str = CoreConstStrings.strAddTagBToTagA;
        //							break;
        //						}
        //						default:
        //						{
        //							switch (taskCode)
        //							{
        //								case TaskCode.SubaConstValueFromTag:
        //								{
        //									str = CoreConstStrings.strSubtractAConstantValueFromATag;
        //									break;
        //								}
        //								case TaskCode.SubTagBFromTagA:
        //								{
        //									str = CoreConstStrings.strSubtractTagBFromTagA;
        //									break;
        //								}
        //								default:
        //								{
        //									switch (taskCode)
        //									{
        //										case TaskCode.TurnBitOn:
        //										{
        //											str = CoreConstStrings.strTurnBitON;
        //											break;
        //										}
        //										case TaskCode.TurnBitOff:
        //										{
        //											str = CoreConstStrings.strTurnBitOFF;
        //											break;
        //										}
        //										case TaskCode.ToggleBit:
        //										{
        //											str = CoreConstStrings.strToggleBit;
        //											break;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (taskCode)
        //					{
        //						case TaskCode.GoToScreen:
        //						{
        //							str = CoreConstStrings.strGotoScreen;
        //							break;
        //						}
        //						case TaskCode.GoToNextScreen:
        //						{
        //							str = CoreConstStrings.strNextScreen;
        //							break;
        //						}
        //						case TaskCode.GoToPreviousScreen:
        //						{
        //							str = CoreConstStrings.strPreviousScreen;
        //							break;
        //						}
        //						default:
        //						{
        //							if (taskCode == TaskCode.WriteValueToTag)
        //							{
        //								str = CoreConstStrings.strWriteValueToTag;
        //								break;
        //							}
        //							else
        //							{
        //								break;
        //							}
        //						}
        //					}
        //				}
        //			}
        //			else if (taskCode <= TaskCode.SwapTagAandTagBBoth)
        //			{
        //				if (taskCode == TaskCode.CopyTagBToTagA)
        //				{
        //					str = CoreConstStrings.strCopyTagBToTagA;
        //				}
        //				else if (taskCode == TaskCode.SwapTagAandTagBBoth)
        //				{
        //					str = CoreConstStrings.strSwapTagAAndTagB;
        //				}
        //			}
        //			else if (taskCode == TaskCode.PrintData)
        //			{
        //				str = CoreConstStrings.strPrintData;
        //			}
        //			else if (taskCode == TaskCode.SetRTC)
        //			{
        //				str = CoreConstStrings.strSetRTC;
        //			}
        //			else
        //			{
        //				switch (taskCode)
        //				{
        //					case TaskCode.CopyTagToSTR:
        //					{
        //						str = CoreConstStrings.strCopyTagToSTR;
        //						break;
        //					}
        //					case TaskCode.CopyTagToLED:
        //					{
        //						str = CoreConstStrings.strCopyTagToLED;
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (taskCode <= TaskCode.CopyPLCBlockToRecipe)
        //		{
        //			if (taskCode <= TaskCode.Wait)
        //			{
        //				if (taskCode == TaskCode.Delay)
        //				{
        //					str = CoreConstStrings.strDelay;
        //				}
        //				else if (taskCode == TaskCode.Wait)
        //				{
        //					str = CoreConstStrings.strWait;
        //				}
        //			}
        //			else if (taskCode == TaskCode.KeySpecificTask)
        //			{
        //				str = CoreConstStrings.strKeysSpecificTask;
        //			}
        //			else if (taskCode == TaskCode.ExecutePLCLogicBlock)
        //			{
        //				str = CoreConstStrings.strExecutePLCLogicBlock;
        //			}
        //			else if (taskCode == TaskCode.CopyPLCBlockToRecipe)
        //			{
        //				str = CoreConstStrings.strCopyPLCToPrizm;
        //			}
        //		}
        //		else if (taskCode <= TaskCode.GoToPopUpScreen)
        //		{
        //			if (taskCode == TaskCode.CopyRecipeToPLCBlock)
        //			{
        //				str = CoreConstStrings.strCopyPrizmBlockToPrizmOrPLCBlock;
        //			}
        //			else if (taskCode == TaskCode.CopyRTCToPLCBlock)
        //			{
        //				str = CoreConstStrings.strCopyRTCToPLC;
        //			}
        //			else if (taskCode == TaskCode.GoToPopUpScreen)
        //			{
        //				str = CoreConstStrings.strGoToPopUpScreen;
        //			}
        //		}
        //		else if (taskCode == TaskCode.USBDataLogUpload)
        //		{
        //			str = CoreConstStrings.strUSBDataLogUpload;
        //		}
        //		else if (taskCode == TaskCode.USBHostUpload)
        //		{
        //			str = CoreConstStrings.strUSBHostUpload;
        //		}
        //		else if (taskCode == TaskCode.SDCardUpload)
        //		{
        //			str = CoreConstStrings.strSDCardUpload;
        //		}
        //		return str;
        //	}

        //	public static byte[] GetWord(long temp_variable)
        //	{
        //		byte[] tempVariable = new byte[] { (byte)(temp_variable & (long)255), (byte)((temp_variable & (long)65280) >> 8), (byte)((temp_variable & (long)16711680) >> 16), (byte)((temp_variable & (ulong)-16777216) >> 24) };
        //		return tempVariable;
        //	}

        //	public static bool HardwareVersion_AvailableForPLC(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 909:
        //			case 910:
        //			case 913:
        //			case 914:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 911:
        //			case 912:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				switch (productID)
        //				{
        //					case 931:
        //					case 932:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 941:
        //							case 942:
        //							{
        //								flag1 = true;
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //	}

        //	public static byte HIBYTE(int iValue)
        //	{
        //		return Convert.ToByte(iValue >> 8 & 255);
        //	}

        //	public static ArrayList IECFlashAnimationTagValidation(ArrayList TagList)
        //	{
        //		ArrayList arrayLists = new ArrayList();
        //		for (int i = 0; i < TagList.Count; i++)
        //		{
        //			if (((CommonConstants.Prizm3TagStructure)TagList[i])._PLCCode == 0)
        //			{
        //				string item = ((CommonConstants.Prizm3TagStructure)TagList[i])._StratonDataType;
        //				if (item != null)
        //				{
        //					if (!(item == "BOOL") && !(item == "USINT") && !(item == "BYTE") && !(item == "UINT") && !(item == "WORD"))
        //					{
        //						goto Label0;
        //					}
        //					arrayLists.Add(TagList[i]);
        //				}
        //			Label0:
        //			}
        //			if (((CommonConstants.Prizm3TagStructure)TagList[i])._PLCCode != 0)
        //			{
        //				arrayLists.Add(TagList[i]);
        //			}
        //		}
        //		return arrayLists;
        //	}

        //	public static ArrayList IECTasksTagValidation(string taskName, ArrayList registerTagInfo)
        //	{
        //		int i;
        //		ArrayList arrayLists;
        //		ArrayList arrayLists1 = new ArrayList();
        //		string item = taskName;
        //		if (item != null)
        //		{
        //			switch (item)
        //			{
        //				case "Write Value to Tag":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label1;
        //									}
        //								}
        //							}
        //						Label1:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Add a Constant Value to a Tag":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label2;
        //									}
        //								}
        //							}
        //						Label2:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Subtract a Constant Value from a Tag":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label3;
        //									}
        //								}
        //							}
        //						Label3:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Add Tag B to Tag A":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label4;
        //									}
        //								}
        //							}
        //						Label4:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Subtract Tag B from Tag A":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label5;
        //									}
        //								}
        //							}
        //						Label5:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Copy Tag B to Tag A":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "BOOL":
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label6;
        //									}
        //								}
        //							}
        //						Label6:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Swap Tag A and Tag B":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "BOOL":
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label7;
        //									}
        //								}
        //							}
        //						Label7:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Switch Screen From Tag":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label8;
        //									}
        //								}
        //							}
        //						Label8:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Copy HMI Block to HMI/PLC Block":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label9;
        //									}
        //								}
        //							}
        //						Label9:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Copy HMI/PLC Block to HMI Block":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "LREAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label10;
        //									}
        //								}
        //							}
        //						Label10:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "USB Data Log Upload":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "DINT":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label11;
        //									}
        //								}
        //							}
        //						Label11:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Data Log Upload":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "DINT":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label12;
        //									}
        //								}
        //							}
        //						Label12:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Copy Tag to LED":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label13;
        //									}
        //								}
        //							}
        //						Label13:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Wait While":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "BOOL":
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label14;
        //									}
        //								}
        //							}
        //						Label14:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Key's Specific Task":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "BOOL":
        //									case "SINT":
        //									case "USINT":
        //									case "BYTE":
        //									case "INT":
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "DINT":
        //									case "TIME":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label15;
        //									}
        //								}
        //							}
        //						Label15:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Alarms":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								if (!(item == "UINT") && !(item == "WORD"))
        //								{
        //									goto Label16;
        //								}
        //								arrayLists1.Add(registerTagInfo[i]);
        //							}
        //						Label16:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "Data Logger":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //							if (item != null)
        //							{
        //								switch (item)
        //								{
        //									case "UINT":
        //									case "WORD":
        //									case "UDINT":
        //									case "DWORD":
        //									case "REAL":
        //									case "BOOL":
        //									case "TIME":
        //									case "INT":
        //									case "DINT":
        //									{
        //										arrayLists1.Add(registerTagInfo[i]);
        //										goto Label18;
        //									}
        //								}
        //							}
        //						Label18:
        //						}
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode != 0)
        //						{
        //							arrayLists1.Add(registerTagInfo[i]);
        //						}
        //					}
        //					break;
        //				}
        //				case "USB Host Upload":
        //				case "SD Card Upload":
        //				{
        //					arrayLists1.Clear();
        //					for (i = 0; i < registerTagInfo.Count; i++)
        //					{
        //						if (((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._PLCCode == 0)
        //						{
        //							if (!((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._IsTagSystem)
        //							{
        //								item = ((CommonConstants.Prizm3TagStructure)registerTagInfo[i])._StratonDataType;
        //								if (item != null)
        //								{
        //									switch (item)
        //									{
        //										case "USINT":
        //										case "BYTE":
        //										case "UINT":
        //										case "WORD":
        //										case "UDINT":
        //										case "DWORD":
        //										{
        //											arrayLists1.Add(registerTagInfo[i]);
        //											goto Label19;
        //										}
        //									}
        //								}
        //							Label19:
        //							}
        //						}
        //					}
        //					break;
        //				}
        //				default:
        //				{
        //					arrayLists1 = registerTagInfo;
        //					arrayLists = arrayLists1;
        //					return arrayLists;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			arrayLists1 = registerTagInfo;
        //			arrayLists = arrayLists1;
        //			return arrayLists;
        //		}
        //		arrayLists = arrayLists1;
        //		return arrayLists;
        //	}

        //	public static string IntArrayToString(int[] pIntVal)
        //	{
        //		char[] chr = new char[(int)pIntVal.Length];
        //		for (int i = 0; i < (int)pIntVal.Length; i++)
        //		{
        //			chr[i] = Convert.ToChar(pIntVal[i]);
        //		}
        //		return new string(chr);
        //	}

        //	public static bool InternalCheckIsWow64()
        //	{
        //		bool flag;
        //		bool flag1;
        //		if ((Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor < 1 ? Environment.OSVersion.Version.Major < 6 : false))
        //		{
        //			flag1 = false;
        //		}
        //		else
        //		{
        //			Process currentProcess = Process.GetCurrentProcess();
        //			try
        //			{
        //				flag1 = (CommonConstants.IsWow64Process(currentProcess.Handle, out flag) ? flag : false);
        //			}
        //			finally
        //			{
        //				if (currentProcess != null)
        //				{
        //					((IDisposable)currentProcess).Dispose();
        //				}
        //			}
        //		}
        //		return flag1;
        //	}

        //	public static bool Is4030MTHorizontalProduct(int pProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int num = pProductID;
        //		if (num > 1303)
        //		{
        //			switch (num)
        //			{
        //				case 1621:
        //				case 1623:
        //				case 1624:
        //				{
        //					break;
        //				}
        //				case 1622:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (num)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (num)
        //			{
        //				case 1211:
        //				case 1213:
        //				case 1215:
        //				case 1216:
        //				case 1217:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				case 1226:
        //				{
        //					break;
        //				}
        //				case 1212:
        //				case 1214:
        //				case 1221:
        //				case 1222:
        //				case 1223:
        //				case 1224:
        //				case 1225:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (num)
        //					{
        //						case 1301:
        //						case 1302:
        //						case 1303:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool Is4030MTVerticalProduct(int pProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int num = pProductID;
        //		switch (num)
        //		{
        //			case 1212:
        //			case 1214:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 1213:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				switch (num)
        //				{
        //					case 1221:
        //					case 1222:
        //					case 1223:
        //					case 1224:
        //					case 1225:
        //					case 1227:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					case 1226:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						if (num == 1622)
        //						{
        //							flag1 = true;
        //							flag = flag1;
        //							return flag;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //	}

        //	public static bool IsAccessLvlsupported(int ProductID)
        //	{
        //		return ((ProductID == 1282 || CommonConstants.IsProductSupportedFP5043(ProductID) || CommonConstants.IsProductSupportedFP5070(ProductID) || CommonConstants.IsProductSupportedFP5121(ProductID) ? false : !CommonConstants.IsProductSupportedFP3series(ProductID)) ? false : true);
        //	}

        //	public static bool IsAddressContainsDecimalPoint(string pStr)
        //	{
        //		bool flag;
        //		int num = 0;
        //		while (true)
        //		{
        //			if (num >= pStr.Length)
        //			{
        //				flag = false;
        //				break;
        //			}
        //			else if ((pStr[num] == '.' || pStr[num] == 'E' ? false : pStr[num] != 'f'))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				flag = true;
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsAlarmSupportedByDestinationModel(int pModelID)
        //	{
        //		return (CommonConstants.IsProductFlexiPanels(pModelID) ? true : false);
        //	}

        //	public static bool IsArraySupported(int ProductID)
        //	{
        //		return (!CommonConstants.IsProductMX257_Based(ProductID) ? false : true);
        //	}

        //	public static bool IsBitWizardObject(DrawingObjects pShapeID)
        //	{
        //		return ((pShapeID == DrawingObjects.NEXT || pShapeID == DrawingObjects.GOTO || pShapeID == DrawingObjects.GOTOPOPUPSCREEN || pShapeID == DrawingObjects.HIDEPOPUPSCREEN || pShapeID == DrawingObjects.PREV || pShapeID == DrawingObjects.WRITEVALUETOTAG || pShapeID == DrawingObjects.ADDVALUETOTAG || pShapeID == DrawingObjects.SUBTRACTVALUEFROMTAG || pShapeID == DrawingObjects.HIDEPOPUPSCREEN || pShapeID == DrawingObjects.ADDTAGS || pShapeID == DrawingObjects.SUBTRACTTAGS || pShapeID == DrawingObjects.BITBUTTON || pShapeID == DrawingObjects.SET || pShapeID == DrawingObjects.RESET || pShapeID == DrawingObjects.MOMENTARY || pShapeID == DrawingObjects.TOGGLE || pShapeID == DrawingObjects.HOLDON || pShapeID == DrawingObjects.HOLDOFF || pShapeID == DrawingObjects.BITLAMP || pShapeID == DrawingObjects.NEXT_ALARM || pShapeID == DrawingObjects.PREV_ALARM || pShapeID == DrawingObjects.ACKNOWLEDGE_ALARM || pShapeID == DrawingObjects.ACKNOWLEDGE_ALL_ALARMS || pShapeID == DrawingObjects.COPY_PLCBLOCKTOPRIZMBLOCK ? false : pShapeID != DrawingObjects.COPY_PRIZMBLOCKTOPLCBLOCK) ? false : true);
        //	}

        //	public static bool IsBothTagOf4Bytes(ArrayList pTagInfoList, string strTagNameA, string strTagNameB)
        //	{
        //		int num = 0;
        //		int item = 0;
        //		int item1 = 0;
        //		num = 0;
        //		while (num < pTagInfoList.Count)
        //		{
        //			if (!(((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._TagName == strTagNameA))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				item = ((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._NoOfBytes;
        //				break;
        //			}
        //		}
        //		num = 0;
        //		while (num < pTagInfoList.Count)
        //		{
        //			if (!(((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._TagName == strTagNameB))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				item1 = ((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._NoOfBytes;
        //				break;
        //			}
        //		}
        //		return ((item != 4 ? true : item1 != 4) ? false : true);
        //	}

        //	public static bool IsBothTagOf8Bytes(ArrayList pTagInfoList, string strTagNameA, string strTagNameB)
        //	{
        //		int num = 0;
        //		int item = 0;
        //		int item1 = 0;
        //		num = 0;
        //		while (num < pTagInfoList.Count)
        //		{
        //			if (!(((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._TagName == strTagNameA))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				item = ((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._NoOfBytes;
        //				break;
        //			}
        //		}
        //		num = 0;
        //		while (num < pTagInfoList.Count)
        //		{
        //			if (!(((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._TagName == strTagNameB))
        //			{
        //				num++;
        //			}
        //			else
        //			{
        //				item1 = ((CommonConstants.Prizm3TagStructure)pTagInfoList[num])._NoOfBytes;
        //				break;
        //			}
        //		}
        //		return ((item != 8 ? true : item1 != 8) ? false : true);
        //	}

        //	public static bool IsCom1Supported(int ProductID)
        //	{
        //		bool flag;
        //		flag = ((int)CommonConstants.dsRecentProjectList.Tables["Unitinformation"].Select(string.Concat("ModelNo='", ProductID, "' and COM1='True'")).Length <= 0 ? false : true);
        //		return flag;
        //	}

        //	public static bool IsCOM1SupportedByDestinationModel()
        //	{
        //		bool flag;
        //		flag = (!CommonConstants.PortValuesObject._lstDestPorts.Contains(Port.COM1) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsCOM1SupportedBySourceModel()
        //	{
        //		bool flag;
        //		flag = (!CommonConstants.PortValuesObject._lstSourcePorts.Contains(Port.COM1) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsCom2Supported(int ProductID)
        //	{
        //		bool flag;
        //		flag = ((int)CommonConstants.dsRecentProjectList.Tables["Unitinformation"].Select(string.Concat("ModelNo='", ProductID, "' and COM2='True'")).Length <= 0 ? false : true);
        //		return flag;
        //	}

        //	public static bool IsCOM2SupportedByDestinationModel()
        //	{
        //		bool flag;
        //		flag = (!CommonConstants.PortValuesObject._lstDestPorts.Contains(Port.COM2) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsCOM2SupportedBySourceModel()
        //	{
        //		bool flag;
        //		flag = (!CommonConstants.PortValuesObject._lstSourcePorts.Contains(Port.COM2) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsCom3Supported(int ProductID)
        //	{
        //		bool flag;
        //		flag = ((int)CommonConstants.dsRecentProjectList.Tables["Unitinformation"].Select(string.Concat("ModelNo='", ProductID, "' and Ethernet='True'")).Length <= 0 ? false : true);
        //		return flag;
        //	}

        //	public static bool IsEmailScreen1(int pScreenNumber)
        //	{
        //		return false;
        //	}

        //	public static bool IsEthernetSupportedByDestinationModel()
        //	{
        //		bool flag;
        //		flag = (!CommonConstants.PortValuesObject._lstDestPorts.Contains(Port.Ethernet) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsEthernetSupportedBySourceModel()
        //	{
        //		bool flag;
        //		flag = (!CommonConstants.PortValuesObject._lstSourcePorts.Contains(Port.Ethernet) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsFactoryScreen(int pScreenNumber)
        //	{
        //		return ((pScreenNumber < 64901 ? true : pScreenNumber > 64980) ? false : true);
        //	}

        //	public static bool IsFileNameContains_T(string strFileName)
        //	{
        //		bool flag;
        //		if (strFileName.Length > 1)
        //		{
        //			if (strFileName.Substring(strFileName.Length - 2, 2) == "_T")
        //			{
        //				flag = true;
        //				return flag;
        //			}
        //		}
        //		flag = false;
        //		return flag;
        //	}

        //	public static bool IsFixedGridSizeProduct(int piModelNo)
        //	{
        //		bool flag;
        //		if ((piModelNo == 505 || piModelNo == 881 || piModelNo == 882 || piModelNo == 883 || piModelNo == 884 || piModelNo == 885 || piModelNo == 886 || piModelNo == 887 || piModelNo == 888 || piModelNo == 501 || piModelNo == 502 || piModelNo == 503 || piModelNo == 504 ? false : piModelNo != 821))
        //		{
        //			flag = (!CommonConstants.IsProductIsTextBased(piModelNo) ? false : true);
        //		}
        //		else
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsfolderPresent(string FolderPath, ref string _NewFolderPath, ref bool Ispresent)
        //	{
        //		int num = 0;
        //		string str = FolderPath.Remove(FolderPath.Length - 3, 3);
        //		while (Directory.Exists(FolderPath))
        //		{
        //			int num1 = num + 1;
        //			num = num1;
        //			num = num1;
        //			CommonConstants._NewProjectnumber = num;
        //			CommonConstants._NewProjectFolderName = string.Concat(str, num);
        //			FolderPath = CommonConstants._NewProjectFolderName;
        //			_NewFolderPath = FolderPath;
        //		}
        //		return Ispresent;
        //	}

        //	public static bool IsFP4030MTVerticalProduct(int ProductId)
        //	{
        //		bool flag;
        //		string str = "VerticalproductList.xml";
        //		string str1 = "Product";
        //		DataSet dataSet = new DataSet();
        //		FileStream fileStream = null;
        //		try
        //		{
        //			if (File.Exists(str))
        //			{
        //				fileStream = new FileStream(str, FileMode.Open);
        //				dataSet.ReadXml(fileStream);
        //				DataTable item = dataSet.Tables[str1];
        //				int num = 0;
        //				while (num < item.Rows.Count)
        //				{
        //					if (Convert.ToInt32(item.Rows[num][1].ToString().Trim()) != ProductId)
        //					{
        //						num++;
        //					}
        //					else
        //					{
        //						fileStream.Close();
        //						flag = true;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		catch (Exception exception)
        //		{
        //			if (fileStream != null)
        //			{
        //				fileStream.Close();
        //			}
        //			flag = true;
        //			return flag;
        //		}
        //		if (fileStream != null)
        //		{
        //			fileStream.Close();
        //		}
        //		flag = false;
        //		return flag;
        //	}

        //	public static bool IsKaspro_FlexiProduct(int ProdutID)
        //	{
        //		bool flag = false;
        //		int produtID = ProdutID;
        //		switch (produtID)
        //		{
        //			case 1421:
        //			case 1422:
        //			{
        //			Label0:
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				switch (produtID)
        //				{
        //					case 1431:
        //					case 1432:
        //					{
        //						goto Label0;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsLadderScreen(int pScreenNumber)
        //	{
        //		return ((pScreenNumber < 50000 ? true : pScreenNumber > 60000) ? false : true);
        //	}

        //	public static bool IsPLCSupported(int ProductId, int PLCCode, string strPort)
        //	{
        //		string[] strArrays;
        //		bool flag = false;
        //		string str = string.Concat("Model", ProductId.ToString());
        //		string str1 = PLCCode.ToString();
        //		if (str1.Length == 1)
        //		{
        //			str1 = string.Concat("0", str1);
        //		}
        //		if (CommonConstants.g_Support_IEC_Ladder)
        //		{
        //			DataTable item = CommonConstants.dsReadPLCSupportedModelList_IEC.Tables[str];
        //			strArrays = new string[] { "PLCCode='", str1, "' and ", strPort, "='True'" };
        //			if ((int)item.Select(string.Concat(strArrays)).Length > 0)
        //			{
        //				flag = true;
        //			}
        //		}
        //		else
        //		{
        //			DataTable dataTable = CommonConstants.dsReadPLCSupportedModelList_Native.Tables[str];
        //			strArrays = new string[] { "PLCCode='", str1, "' and ", strPort, "='True'" };
        //			if ((int)dataTable.Select(string.Concat(strArrays)).Length > 0)
        //			{
        //				flag = true;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsPLCSupportedFor5Series(int ProductId, int PLCCode)
        //	{
        //		bool flag;
        //		if (CommonConstants.IsProductMX257_Based(ProductId))
        //		{
        //			if (PLCCode == 184)
        //			{
        //				flag = false;
        //				return flag;
        //			}
        //		}
        //		flag = true;
        //		return flag;
        //	}

        //	public static bool isPLCSupportsReconnectControl(int pProtocol)
        //	{
        //		return (pProtocol == 191 ? false : true);
        //	}

        //	public static bool IsPopUpScreen(int pActiveScreenNumber)
        //	{
        //		return ((pActiveScreenNumber < 65001 ? true : pActiveScreenNumber > 65534) ? false : true);
        //	}

        //	public static bool IsProduct_Compatible_FL50(int ProdutID)
        //	{
        //		return ((ProdutID == 914 || ProdutID == 942 ? false : ProdutID != 910) ? false : true);
        //	}

        //	public static bool IsProduct4035_EthernetProducts(int ProdutID)
        //	{
        //		bool flag = false;
        //		int produtID = ProdutID;
        //		switch (produtID)
        //		{
        //			case 1232:
        //			case 1233:
        //			{
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				if (produtID == 1333)
        //				{
        //					goto case 1233;
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductAdvancedType(int ProdutID)
        //	{
        //		bool flag = false;
        //		int produtID = ProdutID;
        //		if (produtID == 1331 || produtID == 1351)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductCompatible_FL010(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 913)
        //		{
        //			if (productID == 931 || productID == 941)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			if (productID == 909 || productID == 913)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4020(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1207)
        //		{
        //			switch (produtID)
        //			{
        //				case 1150:
        //				case 1151:
        //				case 1152:
        //				case 1153:
        //				case 1154:
        //				case 1155:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1171:
        //						case 1172:
        //						case 1173:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1203:
        //								case 1204:
        //								case 1205:
        //								case 1206:
        //								case 1207:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID > 1600)
        //		{
        //			if (produtID == 1603 || produtID == 1907)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1400:
        //				case 1401:
        //				case 1402:
        //				case 1403:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1600)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4030(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1303)
        //		{
        //			if (produtID > 1229)
        //			{
        //				switch (produtID)
        //				{
        //					case 1261:
        //					case 1262:
        //					case 1263:
        //					case 1264:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1301:
        //							case 1302:
        //							case 1303:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1102:
        //					case 1103:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1200:
        //							case 1209:
        //							case 1210:
        //							case 1211:
        //							case 1212:
        //							case 1213:
        //							case 1214:
        //							case 1215:
        //							case 1216:
        //							case 1217:
        //							case 1218:
        //							case 1219:
        //							case 1220:
        //							case 1221:
        //							case 1222:
        //							case 1223:
        //							case 1224:
        //							case 1225:
        //							case 1226:
        //							case 1227:
        //							case 1228:
        //							case 1229:
        //							{
        //								break;
        //							}
        //							case 1201:
        //							case 1202:
        //							case 1203:
        //							case 1204:
        //							case 1205:
        //							case 1206:
        //							case 1207:
        //							case 1208:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1532)
        //		{
        //			switch (produtID)
        //			{
        //				case 1612:
        //				case 1613:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1621:
        //						case 1622:
        //						case 1623:
        //						case 1624:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1908:
        //								case 1909:
        //								case 1910:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1411:
        //				case 1412:
        //				case 1413:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1531:
        //						case 1532:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4030MR(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1229)
        //		{
        //			if (produtID != 1200)
        //			{
        //				switch (produtID)
        //				{
        //					case 1209:
        //					case 1210:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1228:
        //							case 1229:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1532)
        //		{
        //			switch (produtID)
        //			{
        //				case 1612:
        //				case 1613:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1908)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1261:
        //				case 1262:
        //				case 1263:
        //				case 1264:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1531:
        //						case 1532:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4030MT(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID > 1303)
        //		{
        //			switch (produtID)
        //			{
        //				case 1621:
        //				case 1622:
        //				case 1623:
        //				case 1624:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1211:
        //				case 1212:
        //				case 1213:
        //				case 1214:
        //				case 1215:
        //				case 1216:
        //				case 1217:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				case 1221:
        //				case 1222:
        //				case 1223:
        //				case 1224:
        //				case 1225:
        //				case 1226:
        //				case 1227:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1301:
        //						case 1302:
        //						case 1303:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4035(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1551)
        //		{
        //			if (produtID > 1333)
        //			{
        //				if (produtID > 1373)
        //				{
        //					switch (produtID)
        //					{
        //						case 1421:
        //						case 1422:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (produtID == 1543 || produtID == 1551)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					if (produtID == 1343 || produtID == 1373)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //			else if (produtID > 1243)
        //			{
        //				switch (produtID)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					case 1274:
        //					case 1280:
        //					case 1281:
        //					{
        //						break;
        //					}
        //					case 1275:
        //					case 1276:
        //					case 1277:
        //					case 1278:
        //					case 1279:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1330:
        //							case 1331:
        //							case 1333:
        //							{
        //								break;
        //							}
        //							case 1332:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1105)
        //			{
        //				switch (produtID)
        //				{
        //					case 1230:
        //					case 1231:
        //					case 1232:
        //					case 1233:
        //					case 1234:
        //					case 1240:
        //					case 1241:
        //					case 1242:
        //					case 1243:
        //					{
        //						break;
        //					}
        //					case 1235:
        //					case 1236:
        //					case 1237:
        //					case 1238:
        //					case 1239:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1681)
        //		{
        //			if (produtID > 1630)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1672:
        //							case 1673:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								if (produtID == 1681)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 1573 || produtID == 1630)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1701:
        //				case 1702:
        //				case 1703:
        //				case 1704:
        //				case 1711:
        //				case 1712:
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				case 1721:
        //				case 1722:
        //				case 1723:
        //				case 1724:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1705:
        //				case 1706:
        //				case 1707:
        //				case 1708:
        //				case 1709:
        //				case 1710:
        //				case 1716:
        //				case 1717:
        //				case 1718:
        //				case 1719:
        //				case 1720:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (produtID == 1803)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4043(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1573)
        //		{
        //			if (produtID > 1343)
        //			{
        //				if (produtID == 1543 || produtID == 1573)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1240:
        //					case 1241:
        //					case 1242:
        //					case 1243:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1343)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1704)
        //		{
        //			if (produtID != 1642)
        //			{
        //				switch (produtID)
        //				{
        //					case 1701:
        //					case 1702:
        //					case 1703:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID != 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1911)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4057(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1432)
        //		{
        //			if (produtID <= 1274)
        //			{
        //				switch (produtID)
        //				{
        //					case 1106:
        //					case 1108:
        //					{
        //						break;
        //					}
        //					case 1107:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1250:
        //							case 1251:
        //							case 1252:
        //							case 1253:
        //							case 1254:
        //							case 1255:
        //							case 1256:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (produtID)
        //								{
        //									case 1270:
        //									case 1271:
        //									case 1274:
        //									{
        //										break;
        //									}
        //									case 1272:
        //									case 1273:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1280)
        //			{
        //				switch (produtID)
        //				{
        //					case 1350:
        //					case 1351:
        //					case 1354:
        //					{
        //						break;
        //					}
        //					case 1352:
        //					case 1353:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1431:
        //							case 1432:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1712)
        //		{
        //			switch (produtID)
        //			{
        //				case 1721:
        //				case 1722:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1903:
        //						case 1905:
        //						{
        //							break;
        //						}
        //						case 1904:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 1912)
        //							{
        //								flag1 = true;
        //								flag = flag1;
        //								return flag;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1571 && produtID != 1650)
        //		{
        //			switch (produtID)
        //			{
        //				case 1711:
        //				case 1712:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4070(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID > 1725)
        //		{
        //			if (produtID > 1823)
        //			{
        //				switch (produtID)
        //				{
        //					case 1903:
        //					case 1904:
        //					case 1905:
        //					case 1906:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1912)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 1813 || produtID == 1823)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1373)
        //		{
        //			switch (produtID)
        //			{
        //				case 1270:
        //				case 1271:
        //				case 1272:
        //				case 1273:
        //				case 1274:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1373)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (produtID != 1672)
        //		{
        //			switch (produtID)
        //			{
        //				case 1711:
        //				case 1712:
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				case 1721:
        //				case 1722:
        //				case 1723:
        //				case 1724:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1716:
        //				case 1717:
        //				case 1718:
        //				case 1719:
        //				case 1720:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4084(int ProdutID)
        //	{
        //		bool flag = false;
        //		switch (ProdutID)
        //		{
        //			case 1109:
        //			case 1110:
        //			{
        //				flag = true;
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWith4121(int ProdutID)
        //	{
        //		bool flag = false;
        //		int produtID = ProdutID;
        //		switch (produtID)
        //		{
        //			case 1280:
        //			case 1281:
        //			case 1282:
        //			{
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				if (produtID == 1681)
        //				{
        //					goto case 1282;
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductCompatibleWithFP3035(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 1130:
        //			case 1132:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 1131:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				switch (productID)
        //				{
        //					case 1634:
        //					case 1635:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //	}

        //	public static bool IsProductEv4(int ProdutID)
        //	{
        //		bool flag;
        //		if (!CommonConstants.IsProductFlexiPanels(ProdutID))
        //		{
        //			flag = (!CommonConstants.IsProductHMIOnly(ProdutID) ? false : true);
        //		}
        //		else
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductFL005ExpandablePLCSeries(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID != 920)
        //		{
        //			switch (productID)
        //			{
        //				case 947:
        //				case 948:
        //				case 957:
        //				case 958:
        //				case 959:
        //				case 960:
        //				case 961:
        //				case 962:
        //				case 963:
        //				{
        //					break;
        //				}
        //				case 949:
        //				case 950:
        //				case 951:
        //				case 952:
        //				case 953:
        //				case 954:
        //				case 955:
        //				case 956:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 970:
        //						case 973:
        //						case 974:
        //						case 976:
        //						case 977:
        //						{
        //							break;
        //						}
        //						case 971:
        //						case 972:
        //						case 975:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductFL005MicroPLCBase(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		switch (ProductID)
        //		{
        //			case 920:
        //			case 927:
        //			case 928:
        //			case 945:
        //			case 946:
        //			case 947:
        //			case 948:
        //			case 951:
        //			case 952:
        //			case 953:
        //			case 954:
        //			case 955:
        //			case 956:
        //			case 957:
        //			case 958:
        //			case 959:
        //			case 960:
        //			case 961:
        //			case 962:
        //			case 963:
        //			case 970:
        //			case 971:
        //			case 972:
        //			case 973:
        //			case 974:
        //			case 975:
        //			case 976:
        //			case 977:
        //			case 981:
        //			case 982:
        //			case 983:
        //			case 984:
        //			case 985:
        //			case 986:
        //			case 987:
        //			case 988:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 921:
        //			case 922:
        //			case 923:
        //			case 924:
        //			case 925:
        //			case 926:
        //			case 929:
        //			case 930:
        //			case 931:
        //			case 932:
        //			case 933:
        //			case 934:
        //			case 935:
        //			case 936:
        //			case 937:
        //			case 938:
        //			case 939:
        //			case 940:
        //			case 941:
        //			case 942:
        //			case 943:
        //			case 944:
        //			case 949:
        //			case 950:
        //			case 964:
        //			case 965:
        //			case 966:
        //			case 967:
        //			case 968:
        //			case 969:
        //			case 978:
        //			case 979:
        //			case 980:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //	}

        //	public static bool IsProductFL100Special(int ProductID)
        //	{
        //		bool flag = false;
        //		if (ProductID == 919)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductFlexiPanels(int ProdutID)
        //	{
        //		bool flag = false;
        //		if (!(ProdutID < 1100 ? true : ProdutID > 2040))
        //		{
        //			flag = true;
        //		}
        //		else if (ProdutID == 2001)
        //		{
        //			flag = true;
        //		}
        //		else if (ProdutID == 2002)
        //		{
        //			flag = true;
        //		}
        //		else if (ProdutID == 2003)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductFreeScale(int ProdutID)
        //	{
        //		return ((CommonConstants.IsProductCompatibleWith4043(ProdutID) || CommonConstants.IsProductCompatibleWith4070(ProdutID) ? false : !CommonConstants.IsProductCompatibleWith4121(ProdutID)) ? false : true);
        //	}

        //	public static bool IsProductGateway(int ProductId)
        //	{
        //		return ((ProductId == 2001 || ProductId == 2002 ? false : ProductId != 2003) ? false : true);
        //	}

        //	public static bool IsProductGWY_K22(int ProdutID)
        //	{
        //		return (ProdutID != 2021 ? false : true);
        //	}

        //	public static bool IsProductHMIOnly(int ProdutID)
        //	{
        //		bool flag = false;
        //		flag = (!CommonConstants.IsProductSupportedFP3035(ProdutID) ? false : true);
        //		return flag;
        //	}

        //	public static bool IsProductIsTextAndGraphicsBased(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1303)
        //		{
        //			if (produtID > 1229)
        //			{
        //				switch (produtID)
        //				{
        //					case 1261:
        //					case 1262:
        //					case 1263:
        //					case 1264:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1301:
        //							case 1302:
        //							case 1303:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1102:
        //					case 1103:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1200:
        //							case 1209:
        //							case 1210:
        //							case 1211:
        //							case 1212:
        //							case 1213:
        //							case 1214:
        //							case 1215:
        //							case 1216:
        //							case 1217:
        //							case 1218:
        //							case 1219:
        //							case 1220:
        //							case 1221:
        //							case 1222:
        //							case 1223:
        //							case 1224:
        //							case 1225:
        //							case 1226:
        //							case 1227:
        //							case 1228:
        //							case 1229:
        //							{
        //								break;
        //							}
        //							case 1201:
        //							case 1202:
        //							case 1203:
        //							case 1204:
        //							case 1205:
        //							case 1206:
        //							case 1207:
        //							case 1208:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1532)
        //		{
        //			switch (produtID)
        //			{
        //				case 1612:
        //				case 1613:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1621:
        //						case 1622:
        //						case 1623:
        //						case 1624:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1908:
        //								case 1909:
        //								case 1910:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1411:
        //				case 1412:
        //				case 1413:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1531:
        //						case 1532:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductIsTextBased(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1207)
        //		{
        //			switch (produtID)
        //			{
        //				case 1150:
        //				case 1151:
        //				case 1152:
        //				case 1153:
        //				case 1154:
        //				case 1155:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1171:
        //						case 1172:
        //						case 1173:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1203:
        //								case 1204:
        //								case 1205:
        //								case 1206:
        //								case 1207:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID > 1600)
        //		{
        //			if (produtID == 1603 || produtID == 1907)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1400:
        //				case 1401:
        //				case 1402:
        //				case 1403:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1600)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductLowerType(int ProdutID)
        //	{
        //		bool flag = false;
        //		int produtID = ProdutID;
        //		if (produtID == 1330 || produtID == 1350)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductLPCBased(int ProdutID)
        //	{
        //		return ((CommonConstants.IsProductCompatibleWith4020(ProdutID) || CommonConstants.IsProductCompatibleWith4030(ProdutID) || CommonConstants.IsProductPLC(ProdutID) || ProdutID == 2001 || ProdutID == 2002 || CommonConstants.IsProductCompatibleWithFP3035(ProdutID) ? false : !CommonConstants.IsProductGWY_K22(ProdutID)) ? false : true);
        //	}

        //	public static bool IsProductMX257_Based(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1573)
        //		{
        //			if (productID <= 1282)
        //			{
        //				if (productID != 1234)
        //				{
        //					switch (productID)
        //					{
        //						case 1240:
        //						case 1241:
        //						case 1242:
        //						case 1243:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case 1270:
        //								case 1271:
        //								case 1272:
        //								case 1273:
        //								case 1274:
        //								case 1280:
        //								case 1281:
        //								case 1282:
        //								{
        //									break;
        //								}
        //								case 1275:
        //								case 1276:
        //								case 1277:
        //								case 1278:
        //								case 1279:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (productID > 1373)
        //			{
        //				if (productID == 1543 || productID == 1573)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				if (productID == 1343 || productID == 1373)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (productID <= 1725)
        //		{
        //			if (productID <= 1673)
        //			{
        //				switch (productID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1672:
        //							case 1673:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (productID != 1681)
        //			{
        //				switch (productID)
        //				{
        //					case 1701:
        //					case 1702:
        //					case 1703:
        //					case 1704:
        //					case 1711:
        //					case 1712:
        //					case 1713:
        //					case 1714:
        //					case 1715:
        //					case 1721:
        //					case 1722:
        //					case 1723:
        //					case 1724:
        //					case 1725:
        //					{
        //						break;
        //					}
        //					case 1705:
        //					case 1706:
        //					case 1707:
        //					case 1708:
        //					case 1709:
        //					case 1710:
        //					case 1716:
        //					case 1717:
        //					case 1718:
        //					case 1719:
        //					case 1720:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1813)
        //		{
        //			if (productID == 1803 || productID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (productID != 1823)
        //		{
        //			switch (productID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductMXSpecialCase_Based(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 918:
        //			case 919:
        //			{
        //			Label0:
        //				flag1 = true;
        //				break;
        //			}
        //			default:
        //			{
        //				if (productID != 944)
        //				{
        //					switch (productID)
        //					{
        //						case 978:
        //						{
        //							goto Label0;
        //						}
        //						case 979:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						case 980:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //				flag1 = true;
        //				break;
        //			}
        //		}
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductMXSpecialCase_OldModel(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID != 918)
        //		{
        //			if (productID != 944)
        //			{
        //				switch (productID)
        //				{
        //					case 978:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					case 979:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					case 980:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			flag1 = true;
        //			flag = flag1;
        //			return flag;
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductPLC(int ProdutID)
        //	{
        //		return ((ProdutID == 909 || ProdutID == 910 || ProdutID == 913 || ProdutID == 912 || ProdutID == 915 || ProdutID == 914 || ProdutID == 920 || ProdutID == 970 || ProdutID == 918 || ProdutID == 919 || ProdutID == 927 || ProdutID == 928 || ProdutID == 951 || ProdutID == 952 || ProdutID == 953 || ProdutID == 954 || ProdutID == 955 || ProdutID == 956 || ProdutID == 963 || ProdutID == 957 || ProdutID == 958 || ProdutID == 959 || ProdutID == 960 || ProdutID == 961 || ProdutID == 962 || ProdutID == 980 || ProdutID == 981 || ProdutID == 982 || ProdutID == 983 || ProdutID == 984 || ProdutID == 985 || ProdutID == 986 || ProdutID == 987 || ProdutID == 988 || ProdutID == 971 || ProdutID == 972 || ProdutID == 973 || ProdutID == 974 || ProdutID == 975 || ProdutID == 976 || ProdutID == 977 || ProdutID == 978 || ProdutID == 931 || ProdutID == 932 || ProdutID == 917 || ProdutID == 941 || ProdutID == 942 || ProdutID == 943 || ProdutID == 944 || ProdutID == 945 || ProdutID == 946 || ProdutID == 947 ? false : ProdutID != 948) ? false : true);
        //	}

        //	public static bool IsProductRotatedAntiClockWise(int productID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int num = productID;
        //		if (num <= 1227)
        //		{
        //			switch (num)
        //			{
        //				case 1212:
        //				case 1214:
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				case 1213:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (num)
        //					{
        //						case 1221:
        //						case 1222:
        //						case 1223:
        //						case 1224:
        //						case 1225:
        //						case 1227:
        //						{
        //							flag1 = true;
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (num == 1256 || num == 1622)
        //		{
        //			flag1 = true;
        //			flag = flag1;
        //			return flag;
        //		}
        //		if (CommonConstants._isProductVertical)
        //		{
        //			flag1 = true;
        //		}
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductRotatedClockWise(int productID)
        //	{
        //		bool flag = false;
        //		if (productID == 1256)
        //		{
        //			flag = true;
        //		}
        //		else if (CommonConstants._isProductVertical)
        //		{
        //			flag = ((CommonConstants.Is4030MTVerticalProduct(CommonConstants.ProductDataInfo.iProductID) || CommonConstants.ProductDataInfo.iProductID == 1301 || CommonConstants.ProductDataInfo.iProductID == 1302 ? false : CommonConstants.ProductDataInfo.iProductID != 1303) ? true : false);
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupport_IOInterrupt_Block1(int ProductId)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productId = ProductId;
        //		switch (productId)
        //		{
        //			case 909:
        //			case 912:
        //			case 913:
        //			case 915:
        //			case 917:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 910:
        //			case 911:
        //			case 914:
        //			case 916:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				if (productId == 931)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				switch (productId)
        //				{
        //					case 941:
        //					case 943:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					case 942:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //	}

        //	public static bool IsProductSupport_IOInterrupt_Block2(int ProductId)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productId = ProductId;
        //		switch (productId)
        //		{
        //			case 909:
        //			case 912:
        //			case 913:
        //			case 915:
        //			case 917:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 910:
        //			case 911:
        //			case 914:
        //			case 916:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				if (productId == 931)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				switch (productId)
        //				{
        //					case 941:
        //					case 943:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					case 942:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //	}

        //	public static bool IsProductSupported(int ProductId, ref int EquivVertiProductID)
        //	{
        //		bool flag;
        //		string str = "VerticalproductList.xml";
        //		string str1 = "Product";
        //		DataSet dataSet = new DataSet();
        //		FileStream fileStream = null;
        //		try
        //		{
        //			if (File.Exists(str))
        //			{
        //				fileStream = new FileStream(str, FileMode.Open);
        //				dataSet.ReadXml(fileStream);
        //				DataTable item = dataSet.Tables[str1];
        //				int num = 0;
        //				while (num < item.Rows.Count)
        //				{
        //					if (Convert.ToInt32(item.Rows[num][0].ToString().Trim()) != ProductId)
        //					{
        //						num++;
        //					}
        //					else
        //					{
        //						EquivVertiProductID = Convert.ToInt32(item.Rows[num][1].ToString().Trim());
        //						fileStream.Close();
        //						flag = true;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		catch (Exception exception)
        //		{
        //			if (fileStream != null)
        //			{
        //				fileStream.Close();
        //			}
        //			flag = true;
        //			return flag;
        //		}
        //		if (fileStream != null)
        //		{
        //			fileStream.Close();
        //		}
        //		flag = false;
        //		return flag;
        //	}

        //	public static bool IsProductSupported(int ProductId)
        //	{
        //		bool flag;
        //		string str = "ProductId.xml";
        //		string str1 = "Product";
        //		DataSet dataSet = new DataSet();
        //		FileStream fileStream = null;
        //		if (File.Exists(str))
        //		{
        //			try
        //			{
        //				fileStream = new FileStream(str, FileMode.Open);
        //				dataSet.ReadXml(fileStream);
        //				DataTable item = dataSet.Tables[str1];
        //				int num = 0;
        //				while (num < item.Rows.Count)
        //				{
        //					if (Convert.ToInt32(item.Rows[num][0].ToString().Trim()) != ProductId)
        //					{
        //						num++;
        //					}
        //					else
        //					{
        //						fileStream.Close();
        //						flag = true;
        //						return flag;
        //					}
        //				}
        //			}
        //			catch (Exception exception)
        //			{
        //				if (fileStream != null)
        //				{
        //					fileStream.Close();
        //				}
        //				flag = true;
        //				return flag;
        //			}
        //			if (fileStream != null)
        //			{
        //				fileStream.Close();
        //			}
        //			flag = false;
        //		}
        //		else
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP3020series(int ProductID)
        //	{
        //		bool flag = false;
        //		if (ProductID == 1155)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP3035(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 1130:
        //			case 1132:
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			case 1131:
        //			{
        //				flag = flag1;
        //				return flag;
        //			}
        //			default:
        //			{
        //				switch (productID)
        //				{
        //					case 1634:
        //					case 1635:
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //	}

        //	public static bool IsProductSupportedFP3series(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1803)
        //		{
        //			switch (productID)
        //			{
        //				case 1701:
        //				case 1702:
        //				case 1703:
        //				case 1704:
        //				case 1711:
        //				case 1712:
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				case 1721:
        //				case 1722:
        //				case 1723:
        //				case 1724:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1705:
        //				case 1706:
        //				case 1707:
        //				case 1708:
        //				case 1709:
        //				case 1710:
        //				case 1716:
        //				case 1717:
        //				case 1718:
        //				case 1719:
        //				case 1720:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1803)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (productID != 1813 && productID != 1823)
        //		{
        //			switch (productID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP4020MR(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1173)
        //		{
        //			if (productID == 1600 || productID == 1603 || productID == 1907)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1150:
        //				case 1151:
        //				case 1152:
        //				case 1153:
        //				case 1154:
        //				case 1155:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1171:
        //						case 1172:
        //						case 1173:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP4030MR(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1229)
        //		{
        //			if (productID != 1200)
        //			{
        //				switch (productID)
        //				{
        //					case 1209:
        //					case 1210:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1228:
        //							case 1229:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID > 1532)
        //		{
        //			switch (productID)
        //			{
        //				case 1612:
        //				case 1613:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 1908)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1261:
        //				case 1262:
        //				case 1263:
        //				case 1264:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1531:
        //						case 1532:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP4030MT(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1303)
        //		{
        //			switch (productID)
        //			{
        //				case 1621:
        //				case 1622:
        //				case 1623:
        //				case 1624:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1211:
        //				case 1212:
        //				case 1213:
        //				case 1214:
        //				case 1215:
        //				case 1216:
        //				case 1217:
        //				case 1218:
        //				case 1219:
        //				case 1220:
        //				case 1221:
        //				case 1222:
        //				case 1223:
        //				case 1224:
        //				case 1225:
        //				case 1226:
        //				case 1227:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1301:
        //						case 1302:
        //						case 1303:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP4035(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1333)
        //		{
        //			if (productID == 1551 || productID == 1630)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1230:
        //				case 1231:
        //				case 1232:
        //				case 1233:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1330:
        //						case 1331:
        //						case 1333:
        //						{
        //							break;
        //						}
        //						case 1332:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP4057(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1354)
        //		{
        //			if (productID == 1571 || productID == 1650)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			switch (productID)
        //			{
        //				case 1250:
        //				case 1251:
        //				case 1252:
        //				case 1253:
        //				case 1254:
        //				case 1255:
        //				case 1256:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1350:
        //						case 1351:
        //						case 1354:
        //						{
        //							break;
        //						}
        //						case 1352:
        //						case 1353:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP5043(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1712)
        //		{
        //			if (productID > 1543)
        //			{
        //				switch (productID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1701:
        //							case 1702:
        //							case 1703:
        //							case 1704:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								if (productID == 1712)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case 1240:
        //					case 1241:
        //					case 1242:
        //					case 1243:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1343 || productID == 1543)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1725)
        //		{
        //			if (productID == 1715 || productID == 1722 || productID == 1725)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (productID != 1803)
        //		{
        //			switch (productID)
        //			{
        //				case 1901:
        //				case 1902:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 1911)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP5043ConfigureEthernetScreen(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID > 1643)
        //		{
        //			if (productID > 1715)
        //			{
        //				if (productID == 1725 || productID == 1803 || productID == 1902)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case 1703:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1715)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1343)
        //		{
        //			switch (productID)
        //			{
        //				case 1242:
        //				case 1243:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 1343)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (productID != 1543)
        //		{
        //			switch (productID)
        //			{
        //				case 1642:
        //				case 1643:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP5070(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1673)
        //		{
        //			if (productID <= 1373)
        //			{
        //				switch (productID)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					case 1274:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1373)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (productID != 1573)
        //			{
        //				switch (productID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1813)
        //		{
        //			switch (productID)
        //			{
        //				case 1711:
        //				case 1712:
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				case 1721:
        //				case 1722:
        //				case 1723:
        //				case 1724:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1716:
        //				case 1717:
        //				case 1718:
        //				case 1719:
        //				case 1720:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1813)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (productID != 1823)
        //		{
        //			switch (productID)
        //			{
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 1912)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP5070ConfigureEthernetScreen(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1673)
        //		{
        //			if (productID <= 1373)
        //			{
        //				switch (productID)
        //				{
        //					case 1272:
        //					case 1273:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1373)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (productID != 1573)
        //			{
        //				switch (productID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1725)
        //		{
        //			switch (productID)
        //			{
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (productID != 1813 && productID != 1823)
        //		{
        //			switch (productID)
        //			{
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP5121(int ProductID)
        //	{
        //		bool flag = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 1280:
        //			case 1281:
        //			case 1282:
        //			{
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				if (productID == 1681)
        //				{
        //					goto case 1282;
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportedFP5121ConfigureEthernetScreen(int ProductID)
        //	{
        //		bool flag = false;
        //		int productID = ProductID;
        //		if (productID == 1281 || productID == 1681)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportHHInvertersProtocol(int ProductID)
        //	{
        //		bool flag = false;
        //		int productID = ProductID;
        //		switch (productID)
        //		{
        //			case 971:
        //			case 972:
        //			case 973:
        //			case 974:
        //			case 975:
        //			case 976:
        //			case 977:
        //			case 978:
        //			{
        //			Label0:
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				switch (productID)
        //				{
        //					case 1901:
        //					case 1902:
        //					case 1903:
        //					case 1904:
        //					case 1905:
        //					case 1906:
        //					case 1907:
        //					case 1908:
        //					case 1909:
        //					case 1910:
        //					case 1911:
        //					case 1912:
        //					{
        //						goto Label0;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupports_ExpansionPort(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1412)
        //		{
        //			if (productID <= 1240)
        //			{
        //				if (productID <= 980)
        //				{
        //					if (productID > 932)
        //					{
        //						switch (productID)
        //						{
        //							case 941:
        //							case 942:
        //							case 944:
        //							case 947:
        //							case 948:
        //							case 957:
        //							case 958:
        //							case 959:
        //							case 960:
        //							case 961:
        //							case 962:
        //							case 963:
        //							{
        //								break;
        //							}
        //							case 943:
        //							case 945:
        //							case 946:
        //							case 949:
        //							case 950:
        //							case 951:
        //							case 952:
        //							case 953:
        //							case 954:
        //							case 955:
        //							case 956:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								switch (productID)
        //								{
        //									case 970:
        //									case 973:
        //									case 974:
        //									case 976:
        //									case 977:
        //									case 978:
        //									case 980:
        //									{
        //										break;
        //									}
        //									case 971:
        //									case 972:
        //									case 975:
        //									case 979:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //					}
        //					else
        //					{
        //						switch (productID)
        //						{
        //							case 909:
        //							case 910:
        //							case 913:
        //							case 914:
        //							case 917:
        //							case 918:
        //							case 920:
        //							{
        //								break;
        //							}
        //							case 911:
        //							case 912:
        //							case 915:
        //							case 916:
        //							case 919:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								switch (productID)
        //								{
        //									case 931:
        //									case 932:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //					}
        //				}
        //				else if (productID > 1230)
        //				{
        //					if (productID == 1233 || productID == 1240)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //				else
        //				{
        //					if (productID == 1209 || productID == 1230)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //			else if (productID > 1333)
        //			{
        //				if (productID > 1351)
        //				{
        //					if (productID == 1354 || productID == 1373 || productID == 1412)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //				else
        //				{
        //					if (productID == 1343 || productID == 1351)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //			else if (productID > 1256)
        //			{
        //				switch (productID)
        //				{
        //					case 1270:
        //					case 1273:
        //					case 1274:
        //					{
        //						break;
        //					}
        //					case 1271:
        //					case 1272:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1331:
        //							case 1333:
        //							{
        //								break;
        //							}
        //							case 1332:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (productID != 1243)
        //			{
        //				switch (productID)
        //				{
        //					case 1250:
        //					case 1254:
        //					case 1255:
        //					case 1256:
        //					{
        //						break;
        //					}
        //					case 1251:
        //					case 1252:
        //					case 1253:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1630)
        //		{
        //			if (productID <= 1543)
        //			{
        //				if (productID > 1431)
        //				{
        //					if (productID == 1531 || productID == 1543)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //				else
        //				{
        //					if (productID == 1421 || productID == 1431)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //			else if (productID > 1573)
        //			{
        //				if (productID == 1612 || productID == 1630)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else if (productID != 1551)
        //			{
        //				switch (productID)
        //				{
        //					case 1571:
        //					case 1573:
        //					{
        //						break;
        //					}
        //					case 1572:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1704)
        //		{
        //			if (productID <= 1650)
        //			{
        //				if (productID == 1643 || productID == 1650)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else if (productID != 1673)
        //			{
        //				switch (productID)
        //				{
        //					case 1702:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					case 1703:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1715)
        //		{
        //			if (productID == 1712 || productID == 1715)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (productID != 1722 && productID != 1725)
        //		{
        //			switch (productID)
        //			{
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupports_LocalIO(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1303)
        //		{
        //			if (productID > 1173)
        //			{
        //				switch (productID)
        //				{
        //					case 1210:
        //					case 1215:
        //					case 1216:
        //					case 1217:
        //					case 1219:
        //					case 1220:
        //					case 1222:
        //					case 1223:
        //					case 1224:
        //					case 1225:
        //					case 1226:
        //					case 1227:
        //					case 1228:
        //					case 1229:
        //					{
        //						break;
        //					}
        //					case 1211:
        //					case 1212:
        //					case 1213:
        //					case 1214:
        //					case 1218:
        //					case 1221:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1261:
        //							case 1262:
        //							case 1263:
        //							case 1264:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (productID)
        //								{
        //									case 1301:
        //									case 1302:
        //									case 1303:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case 1151:
        //					case 1152:
        //					case 1153:
        //					case 1154:
        //					case 1155:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (productID)
        //						{
        //							case 1171:
        //							case 1172:
        //							case 1173:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1603)
        //		{
        //			if (productID == 1532 || productID == 1603)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (productID != 1613)
        //		{
        //			switch (productID)
        //			{
        //				case 1623:
        //				case 1624:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1907:
        //						case 1908:
        //						case 1909:
        //						case 1910:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupports_RetainHSCounter(int ProductID)
        //	{
        //		bool flag = false;
        //		if (!CommonConstants.IsProductSupports_LocalIO(ProductID))
        //		{
        //			int productID = ProductID;
        //			switch (productID)
        //			{
        //				case 912:
        //				case 913:
        //				case 915:
        //				case 917:
        //				{
        //				Label1:
        //					flag = true;
        //					goto case 916;
        //				}
        //				case 914:
        //				case 916:
        //				{
        //				Label0:
        //					break;
        //				}
        //				default:
        //				{
        //					if (productID == 931)
        //					{
        //						goto case 917;
        //					}
        //					switch (productID)
        //					{
        //						case 941:
        //						case 943:
        //						{
        //							goto Label1;
        //						}
        //						default:
        //						{
        //							goto Label0;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupports16GrayScale(int ProdutID)
        //	{
        //		bool flag = false;
        //		if (ProdutID == 1106)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupports2Color(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1303)
        //		{
        //			if (produtID > 1229)
        //			{
        //				switch (produtID)
        //				{
        //					case 1261:
        //					case 1262:
        //					case 1263:
        //					case 1264:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1301:
        //							case 1302:
        //							case 1303:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1102:
        //					case 1103:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1200:
        //							case 1209:
        //							case 1210:
        //							case 1211:
        //							case 1212:
        //							case 1213:
        //							case 1214:
        //							case 1215:
        //							case 1216:
        //							case 1217:
        //							case 1218:
        //							case 1219:
        //							case 1220:
        //							case 1221:
        //							case 1222:
        //							case 1223:
        //							case 1224:
        //							case 1225:
        //							case 1226:
        //							case 1227:
        //							case 1228:
        //							case 1229:
        //							{
        //								break;
        //							}
        //							case 1201:
        //							case 1202:
        //							case 1203:
        //							case 1204:
        //							case 1205:
        //							case 1206:
        //							case 1207:
        //							case 1208:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1532)
        //		{
        //			switch (produtID)
        //			{
        //				case 1612:
        //				case 1613:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1621:
        //						case 1622:
        //						case 1623:
        //						case 1624:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1908:
        //								case 1909:
        //								case 1910:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			switch (produtID)
        //			{
        //				case 1411:
        //				case 1412:
        //				case 1413:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1531:
        //						case 1532:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsColor(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1432)
        //		{
        //			if (produtID <= 1256)
        //			{
        //				if (produtID <= 970)
        //				{
        //					switch (produtID)
        //					{
        //						case 918:
        //						case 919:
        //						case 920:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (produtID == 944 || produtID == 970)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //				else if (produtID > 1105)
        //				{
        //					switch (produtID)
        //					{
        //						case 1130:
        //						case 1132:
        //						{
        //							break;
        //						}
        //						case 1131:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1230:
        //								case 1231:
        //								case 1232:
        //								case 1233:
        //								case 1234:
        //								case 1240:
        //								case 1241:
        //								case 1242:
        //								case 1243:
        //								case 1250:
        //								case 1251:
        //								case 1252:
        //								case 1253:
        //								case 1254:
        //								case 1255:
        //								case 1256:
        //								{
        //									break;
        //								}
        //								case 1235:
        //								case 1236:
        //								case 1237:
        //								case 1238:
        //								case 1239:
        //								case 1244:
        //								case 1245:
        //								case 1246:
        //								case 1247:
        //								case 1248:
        //								case 1249:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 978:
        //						case 980:
        //						{
        //							break;
        //						}
        //						case 979:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 1105)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID <= 1343)
        //			{
        //				switch (produtID)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					case 1274:
        //					case 1280:
        //					case 1281:
        //					case 1282:
        //					{
        //						break;
        //					}
        //					case 1275:
        //					case 1276:
        //					case 1277:
        //					case 1278:
        //					case 1279:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1330:
        //							case 1331:
        //							case 1333:
        //							{
        //								break;
        //							}
        //							case 1332:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								if (produtID == 1343)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID > 1373)
        //			{
        //				switch (produtID)
        //				{
        //					case 1421:
        //					case 1422:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1431:
        //							case 1432:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1350:
        //					case 1351:
        //					case 1354:
        //					{
        //						break;
        //					}
        //					case 1352:
        //					case 1353:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						if (produtID == 1373)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1650)
        //		{
        //			if (produtID <= 1573)
        //			{
        //				if (produtID != 1543 && produtID != 1551)
        //				{
        //					switch (produtID)
        //					{
        //						case 1571:
        //						case 1573:
        //						{
        //							break;
        //						}
        //						case 1572:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID > 1635)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1650)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID != 1630)
        //			{
        //				switch (produtID)
        //				{
        //					case 1634:
        //					case 1635:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			switch (produtID)
        //			{
        //				case 1672:
        //				case 1673:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1681)
        //					{
        //						break;
        //					}
        //					switch (produtID)
        //					{
        //						case 1701:
        //						case 1702:
        //						case 1703:
        //						case 1704:
        //						case 1711:
        //						case 1712:
        //						case 1713:
        //						case 1714:
        //						case 1715:
        //						case 1721:
        //						case 1722:
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1705:
        //						case 1706:
        //						case 1707:
        //						case 1708:
        //						case 1709:
        //						case 1710:
        //						case 1716:
        //						case 1717:
        //						case 1718:
        //						case 1719:
        //						case 1720:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsCOMPorts(int pProductID, string strCOMPort)
        //	{
        //		bool flag = false;
        //		string upper = strCOMPort.ToUpper();
        //		DataRow[] dataRowArray = CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Select(string.Concat("ModelNo='", pProductID, "'"));
        //		DataRow[] dataRowArray1 = dataRowArray;
        //		for (int i = 0; i < (int)dataRowArray1.Length; i++)
        //		{
        //			DataRow dataRow = dataRowArray1[i];
        //			flag = Convert.ToBoolean(dataRow[upper].ToString().ToLower());
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportsDataLogger(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1422)
        //		{
        //			if (produtID <= 1256)
        //			{
        //				if (produtID > 970)
        //				{
        //					switch (produtID)
        //					{
        //						case 978:
        //						case 980:
        //						{
        //							break;
        //						}
        //						case 979:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1105:
        //								case 1106:
        //								case 1108:
        //								case 1109:
        //								case 1110:
        //								{
        //									break;
        //								}
        //								case 1107:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									switch (produtID)
        //									{
        //										case 1230:
        //										case 1231:
        //										case 1232:
        //										case 1233:
        //										case 1234:
        //										case 1240:
        //										case 1241:
        //										case 1242:
        //										case 1243:
        //										case 1250:
        //										case 1251:
        //										case 1252:
        //										case 1253:
        //										case 1254:
        //										case 1255:
        //										case 1256:
        //										{
        //											break;
        //										}
        //										case 1235:
        //										case 1236:
        //										case 1237:
        //										case 1238:
        //										case 1239:
        //										case 1244:
        //										case 1245:
        //										case 1246:
        //										case 1247:
        //										case 1248:
        //										case 1249:
        //										{
        //											flag = flag1;
        //											return flag;
        //										}
        //										default:
        //										{
        //											flag = flag1;
        //											return flag;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 918:
        //						case 920:
        //						{
        //							break;
        //						}
        //						case 919:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 944 || produtID == 970)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID <= 1343)
        //			{
        //				switch (produtID)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					case 1274:
        //					case 1280:
        //					case 1281:
        //					case 1282:
        //					{
        //						break;
        //					}
        //					case 1275:
        //					case 1276:
        //					case 1277:
        //					case 1278:
        //					case 1279:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1331:
        //							case 1333:
        //							{
        //								break;
        //							}
        //							case 1332:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								if (produtID == 1343)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID <= 1354)
        //			{
        //				if (produtID == 1351 || produtID == 1354)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else if (produtID != 1373)
        //			{
        //				switch (produtID)
        //				{
        //					case 1421:
        //					case 1422:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1650)
        //		{
        //			if (produtID <= 1551)
        //			{
        //				switch (produtID)
        //				{
        //					case 1431:
        //					case 1432:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1543 || produtID == 1551)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID > 1630)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1650)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1571:
        //					case 1573:
        //					{
        //						break;
        //					}
        //					case 1572:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						if (produtID == 1630)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			switch (produtID)
        //			{
        //				case 1672:
        //				case 1673:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1681)
        //					{
        //						break;
        //					}
        //					switch (produtID)
        //					{
        //						case 1701:
        //						case 1702:
        //						case 1703:
        //						case 1704:
        //						case 1711:
        //						case 1712:
        //						case 1713:
        //						case 1714:
        //						case 1715:
        //						case 1721:
        //						case 1722:
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1705:
        //						case 1706:
        //						case 1707:
        //						case 1708:
        //						case 1709:
        //						case 1710:
        //						case 1716:
        //						case 1717:
        //						case 1718:
        //						case 1719:
        //						case 1720:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsDataLoggerExternal(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1715)
        //		{
        //			if (produtID > 970)
        //			{
        //				switch (produtID)
        //				{
        //					case 1703:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1713:
        //							case 1715:
        //							{
        //								break;
        //							}
        //							case 1714:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 920 || produtID == 970)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1723:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1724:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (produtID == 1803)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsDwnldSerialParams(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1624)
        //		{
        //			if (produtID <= 1226)
        //			{
        //				if (produtID > 963)
        //				{
        //					switch (produtID)
        //					{
        //						case 970:
        //						case 980:
        //						case 981:
        //						case 982:
        //						case 983:
        //						case 984:
        //						case 985:
        //						case 986:
        //						case 987:
        //						case 988:
        //						{
        //							break;
        //						}
        //						case 971:
        //						case 972:
        //						case 973:
        //						case 974:
        //						case 975:
        //						case 976:
        //						case 977:
        //						case 978:
        //						case 979:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1213:
        //								case 1214:
        //								case 1216:
        //								case 1218:
        //								case 1220:
        //								{
        //									break;
        //								}
        //								case 1215:
        //								case 1217:
        //								case 1219:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									if (produtID == 1226)
        //									{
        //										flag1 = true;
        //										flag = flag1;
        //										return flag;
        //									}
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 918:
        //						case 920:
        //						{
        //							break;
        //						}
        //						case 919:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 927:
        //								case 928:
        //								case 944:
        //								case 945:
        //								case 946:
        //								case 947:
        //								case 948:
        //								case 951:
        //								case 952:
        //								case 953:
        //								case 954:
        //								case 955:
        //								case 956:
        //								case 957:
        //								case 958:
        //								case 959:
        //								case 960:
        //								case 961:
        //								case 962:
        //								case 963:
        //								{
        //									break;
        //								}
        //								case 929:
        //								case 930:
        //								case 931:
        //								case 932:
        //								case 933:
        //								case 934:
        //								case 935:
        //								case 936:
        //								case 937:
        //								case 938:
        //								case 939:
        //								case 940:
        //								case 941:
        //								case 942:
        //								case 943:
        //								case 949:
        //								case 950:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID > 1273)
        //			{
        //				switch (produtID)
        //				{
        //					case 1280:
        //					case 1281:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1301:
        //							case 1302:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								if (produtID == 1624)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1240:
        //					case 1241:
        //					case 1242:
        //					case 1243:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1270:
        //							case 1271:
        //							case 1272:
        //							case 1273:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			if (produtID <= 1673)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1672:
        //							case 1673:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1681)
        //			{
        //				switch (produtID)
        //				{
        //					case 1701:
        //					case 1702:
        //					case 1703:
        //					case 1704:
        //					case 1711:
        //					case 1712:
        //					case 1713:
        //					case 1715:
        //					{
        //						break;
        //					}
        //					case 1705:
        //					case 1706:
        //					case 1707:
        //					case 1708:
        //					case 1709:
        //					case 1710:
        //					case 1714:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1721:
        //							case 1722:
        //							case 1723:
        //							case 1725:
        //							{
        //								break;
        //							}
        //							case 1724:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1909:
        //				case 1910:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (produtID == 2021)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsEmail(int pProductID)
        //	{
        //		return false;
        //	}

        //	public static bool IsProductSupportsEnronModbusProtocol(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1573)
        //		{
        //			if (produtID <= 1281)
        //			{
        //				switch (produtID)
        //				{
        //					case 1240:
        //					case 1241:
        //					case 1242:
        //					case 1243:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1270:
        //							case 1271:
        //							case 1272:
        //							case 1273:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (produtID)
        //								{
        //									case 1280:
        //									case 1281:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID > 1373)
        //			{
        //				if (produtID == 1543 || produtID == 1573)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				if (produtID == 1343 || produtID == 1373)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			if (produtID <= 1673)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1672:
        //							case 1673:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1681)
        //			{
        //				switch (produtID)
        //				{
        //					case 1701:
        //					case 1702:
        //					case 1703:
        //					case 1704:
        //					case 1711:
        //					case 1712:
        //					case 1713:
        //					case 1714:
        //					case 1715:
        //					case 1721:
        //					case 1722:
        //					case 1723:
        //					case 1724:
        //					case 1725:
        //					{
        //						break;
        //					}
        //					case 1705:
        //					case 1706:
        //					case 1707:
        //					case 1708:
        //					case 1709:
        //					case 1710:
        //					case 1716:
        //					case 1717:
        //					case 1718:
        //					case 1719:
        //					case 1720:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsEthernet(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1343)
        //		{
        //			if (produtID <= 980)
        //			{
        //				if (produtID > 932)
        //				{
        //					switch (produtID)
        //					{
        //						case 942:
        //						case 944:
        //						{
        //							break;
        //						}
        //						case 943:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 970)
        //							{
        //								break;
        //							}
        //							switch (produtID)
        //							{
        //								case 978:
        //								case 980:
        //								{
        //									break;
        //								}
        //								case 979:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else if (produtID != 910)
        //				{
        //					switch (produtID)
        //					{
        //						case 914:
        //						case 918:
        //						case 919:
        //						case 920:
        //						{
        //							break;
        //						}
        //						case 915:
        //						case 916:
        //						case 917:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 932)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID <= 1254)
        //			{
        //				switch (produtID)
        //				{
        //					case 1232:
        //					case 1233:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1242:
        //							case 1243:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (produtID)
        //								{
        //									case 1253:
        //									case 1254:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID > 1282)
        //			{
        //				if (produtID == 1333 || produtID == 1343)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1272:
        //					case 1273:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1281:
        //							case 1282:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1681)
        //		{
        //			if (produtID <= 1543)
        //			{
        //				if (produtID == 1354 || produtID == 1373 || produtID == 1543)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else if (produtID > 1643)
        //			{
        //				switch (produtID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1681)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID != 1573)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			switch (produtID)
        //			{
        //				case 1703:
        //				case 1704:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1713:
        //						case 1714:
        //						case 1715:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1723:
        //								case 1724:
        //								case 1725:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsEthernetMultiNode(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1543)
        //		{
        //			if (produtID > 1243)
        //			{
        //				if (produtID > 1282)
        //				{
        //					if (produtID == 1343 || produtID == 1373 || produtID == 1543)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 1272:
        //						case 1273:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1281:
        //								case 1282:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID <= 944)
        //			{
        //				switch (produtID)
        //				{
        //					case 918:
        //					case 919:
        //					case 920:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 944)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID != 970)
        //			{
        //				switch (produtID)
        //				{
        //					case 978:
        //					case 980:
        //					{
        //						break;
        //					}
        //					case 979:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1242:
        //							case 1243:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1704)
        //		{
        //			if (produtID > 1643)
        //			{
        //				switch (produtID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1681)
        //						{
        //							break;
        //						}
        //						switch (produtID)
        //						{
        //							case 1703:
        //							case 1704:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1573)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (produtID == 1803)
        //							{
        //								flag1 = true;
        //								flag = flag1;
        //								return flag;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsEthernetSameNodeAddr(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1543)
        //		{
        //			if (produtID > 1243)
        //			{
        //				if (produtID > 1282)
        //				{
        //					if (produtID == 1343 || produtID == 1373 || produtID == 1543)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 1272:
        //						case 1273:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1281:
        //								case 1282:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID <= 944)
        //			{
        //				switch (produtID)
        //				{
        //					case 918:
        //					case 919:
        //					case 920:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 944)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID != 970)
        //			{
        //				switch (produtID)
        //				{
        //					case 978:
        //					case 980:
        //					{
        //						break;
        //					}
        //					case 979:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1242:
        //							case 1243:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1704)
        //		{
        //			if (produtID > 1643)
        //			{
        //				switch (produtID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1681)
        //						{
        //							break;
        //						}
        //						switch (produtID)
        //						{
        //							case 1703:
        //							case 1704:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1573)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1713:
        //				case 1715:
        //				{
        //					break;
        //				}
        //				case 1714:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1723:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1724:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 1803)
        //							{
        //								flag1 = true;
        //								flag = flag1;
        //								return flag;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool isProductSupportsFTP(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1715)
        //		{
        //			if (produtID > 970)
        //			{
        //				switch (produtID)
        //				{
        //					case 1703:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1713:
        //							case 1715:
        //							{
        //								break;
        //							}
        //							case 1714:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 920 || produtID == 970)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1723:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1724:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (produtID == 1803)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool isProductSupportsFTPCOM2(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1725)
        //		{
        //			switch (produtID)
        //			{
        //				case 1713:
        //				case 1715:
        //				{
        //					break;
        //				}
        //				case 1714:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1723:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1724:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool isProductSupportsFTPSDCard(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1715)
        //		{
        //			if (produtID > 970)
        //			{
        //				switch (produtID)
        //				{
        //					case 1703:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1713:
        //							case 1715:
        //							{
        //								break;
        //							}
        //							case 1714:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 920 || produtID == 970)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1803)
        //		{
        //			switch (produtID)
        //			{
        //				case 1723:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1724:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (produtID == 1803)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (produtID != 1813 && produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool isProductSupportsFTPUSB(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1725)
        //		{
        //			if (produtID > 944)
        //			{
        //				switch (produtID)
        //				{
        //					case 1701:
        //					case 1702:
        //					case 1703:
        //					case 1704:
        //					case 1711:
        //					case 1712:
        //					case 1713:
        //					case 1715:
        //					{
        //						break;
        //					}
        //					case 1705:
        //					case 1706:
        //					case 1707:
        //					case 1708:
        //					case 1709:
        //					case 1710:
        //					case 1714:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1721:
        //							case 1722:
        //							case 1723:
        //							case 1725:
        //							{
        //								break;
        //							}
        //							case 1724:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 918 || produtID == 944)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsFunctionKeys(int ProdutID)
        //	{
        //		bool flag;
        //		if (!CommonConstants.IsProductSupportsKeypadOnly(ProdutID))
        //		{
        //			flag = (!CommonConstants.IsProductSupportsKeypadAndTouchscreen(ProdutID) ? false : true);
        //		}
        //		else
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportsHisAlarm(int ProdutID)
        //	{
        //		bool flag = true;
        //		if ((CommonConstants.IsProductPLC(ProdutID) ? true : CommonConstants.IsProductWithoutRTC(ProdutID)))
        //		{
        //			flag = false;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportsIEC(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1573)
        //		{
        //			if (productID <= 1331)
        //			{
        //				if (productID > 1173)
        //				{
        //					switch (productID)
        //					{
        //						case 1215:
        //						case 1216:
        //						case 1217:
        //						case 1218:
        //						case 1219:
        //						case 1220:
        //						case 1221:
        //						case 1222:
        //						case 1223:
        //						case 1224:
        //						case 1225:
        //						case 1226:
        //						case 1227:
        //						case 1228:
        //						case 1229:
        //						case 1230:
        //						case 1231:
        //						case 1232:
        //						case 1233:
        //						case 1234:
        //						case 1240:
        //						case 1241:
        //						case 1242:
        //						case 1243:
        //						case 1250:
        //						case 1251:
        //						case 1253:
        //						case 1254:
        //						case 1256:
        //						case 1261:
        //						case 1262:
        //						case 1263:
        //						case 1264:
        //						case 1270:
        //						case 1271:
        //						case 1272:
        //						case 1273:
        //						case 1274:
        //						case 1280:
        //						case 1281:
        //						{
        //							break;
        //						}
        //						case 1235:
        //						case 1236:
        //						case 1237:
        //						case 1238:
        //						case 1239:
        //						case 1244:
        //						case 1245:
        //						case 1246:
        //						case 1247:
        //						case 1248:
        //						case 1249:
        //						case 1252:
        //						case 1255:
        //						case 1257:
        //						case 1258:
        //						case 1259:
        //						case 1260:
        //						case 1265:
        //						case 1266:
        //						case 1267:
        //						case 1268:
        //						case 1269:
        //						case 1275:
        //						case 1276:
        //						case 1277:
        //						case 1278:
        //						case 1279:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case 1301:
        //								case 1302:
        //								case 1303:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									switch (productID)
        //									{
        //										case 1330:
        //										case 1331:
        //										{
        //											break;
        //										}
        //										default:
        //										{
        //											flag = flag1;
        //											return flag;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (productID)
        //					{
        //						case 918:
        //						case 920:
        //						case 927:
        //						case 928:
        //						case 944:
        //						case 945:
        //						case 946:
        //						case 947:
        //						case 948:
        //						case 951:
        //						case 952:
        //						case 953:
        //						case 954:
        //						case 955:
        //						case 956:
        //						case 957:
        //						case 958:
        //						case 959:
        //						case 960:
        //						case 961:
        //						case 962:
        //						case 963:
        //						case 970:
        //						case 971:
        //						case 972:
        //						case 973:
        //						case 974:
        //						case 975:
        //						case 976:
        //						case 977:
        //						case 978:
        //						case 980:
        //						case 981:
        //						case 982:
        //						case 983:
        //						case 984:
        //						case 985:
        //						case 986:
        //						case 987:
        //						case 988:
        //						{
        //							break;
        //						}
        //						case 919:
        //						case 921:
        //						case 922:
        //						case 923:
        //						case 924:
        //						case 925:
        //						case 926:
        //						case 929:
        //						case 930:
        //						case 931:
        //						case 932:
        //						case 933:
        //						case 934:
        //						case 935:
        //						case 936:
        //						case 937:
        //						case 938:
        //						case 939:
        //						case 940:
        //						case 941:
        //						case 942:
        //						case 943:
        //						case 949:
        //						case 950:
        //						case 964:
        //						case 965:
        //						case 966:
        //						case 967:
        //						case 968:
        //						case 969:
        //						case 979:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (productID == 1155)
        //							{
        //								break;
        //							}
        //							switch (productID)
        //							{
        //								case 1171:
        //								case 1172:
        //								case 1173:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (productID <= 1373)
        //			{
        //				if (productID != 1343)
        //				{
        //					switch (productID)
        //					{
        //						case 1350:
        //						case 1351:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (productID == 1373)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (productID != 1543 && productID != 1551)
        //			{
        //				switch (productID)
        //				{
        //					case 1571:
        //					case 1573:
        //					{
        //						break;
        //					}
        //					case 1572:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1681)
        //		{
        //			if (productID <= 1643)
        //			{
        //				switch (productID)
        //				{
        //					case 1623:
        //					case 1624:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1630)
        //						{
        //							break;
        //						}
        //						switch (productID)
        //						{
        //							case 1642:
        //							case 1643:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (productID != 1650)
        //			{
        //				switch (productID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1681)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1813)
        //		{
        //			switch (productID)
        //			{
        //				case 1701:
        //				case 1702:
        //				case 1703:
        //				case 1704:
        //				case 1711:
        //				case 1712:
        //				case 1713:
        //				case 1714:
        //				case 1715:
        //				case 1721:
        //				case 1722:
        //				case 1723:
        //				case 1724:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				case 1705:
        //				case 1706:
        //				case 1707:
        //				case 1708:
        //				case 1709:
        //				case 1710:
        //				case 1716:
        //				case 1717:
        //				case 1718:
        //				case 1719:
        //				case 1720:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 1803 || productID == 1813)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (productID != 1823)
        //		{
        //			switch (productID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1909:
        //				case 1910:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					if (productID == 2003)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsKeyenceProtocol(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1373)
        //		{
        //			if (produtID <= 1243)
        //			{
        //				if (produtID <= 1220)
        //				{
        //					if (produtID != 1200)
        //					{
        //						switch (produtID)
        //						{
        //							case 1209:
        //							case 1210:
        //							case 1211:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (produtID)
        //								{
        //									case 1216:
        //									case 1218:
        //									case 1220:
        //									{
        //										break;
        //									}
        //									case 1217:
        //									case 1219:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //					}
        //				}
        //				else if (produtID != 1226 && produtID != 1229)
        //				{
        //					switch (produtID)
        //					{
        //						case 1240:
        //						case 1241:
        //						case 1242:
        //						case 1243:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID > 1281)
        //			{
        //				switch (produtID)
        //				{
        //					case 1301:
        //					case 1302:
        //					case 1303:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1343 || produtID == 1373)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID != 1261)
        //			{
        //				switch (produtID)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1280:
        //							case 1281:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1643)
        //		{
        //			if (produtID > 1573)
        //			{
        //				switch (produtID)
        //				{
        //					case 1612:
        //					case 1613:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1621:
        //							case 1623:
        //							case 1624:
        //							{
        //								break;
        //							}
        //							case 1622:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								switch (produtID)
        //								{
        //									case 1642:
        //									case 1643:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1531:
        //					case 1532:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1543 || produtID == 1573)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			switch (produtID)
        //			{
        //				case 1672:
        //				case 1673:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1681)
        //					{
        //						break;
        //					}
        //					switch (produtID)
        //					{
        //						case 1701:
        //						case 1702:
        //						case 1703:
        //						case 1704:
        //						case 1711:
        //						case 1712:
        //						case 1713:
        //						case 1714:
        //						case 1715:
        //						case 1721:
        //						case 1722:
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1705:
        //						case 1706:
        //						case 1707:
        //						case 1708:
        //						case 1709:
        //						case 1710:
        //						case 1716:
        //						case 1717:
        //						case 1718:
        //						case 1719:
        //						case 1720:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsKeypadAndTouchscreen(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID > 1354)
        //		{
        //			if (produtID > 1551)
        //			{
        //				if (produtID == 1571 || produtID == 1630 || produtID == 1650)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1421:
        //					case 1422:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1431:
        //							case 1432:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								if (produtID == 1551)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1256)
        //		{
        //			switch (produtID)
        //			{
        //				case 1105:
        //				case 1106:
        //				case 1108:
        //				case 1109:
        //				case 1110:
        //				{
        //					break;
        //				}
        //				case 1107:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1230:
        //						case 1231:
        //						case 1232:
        //						case 1233:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1250:
        //								case 1251:
        //								case 1252:
        //								case 1253:
        //								case 1254:
        //								case 1255:
        //								case 1256:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1274)
        //		{
        //			switch (produtID)
        //			{
        //				case 1330:
        //				case 1331:
        //				case 1333:
        //				{
        //					break;
        //				}
        //				case 1332:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1350:
        //						case 1351:
        //						case 1354:
        //						{
        //							break;
        //						}
        //						case 1352:
        //						case 1353:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsKeypadOnly(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1264)
        //		{
        //			if (produtID <= 1173)
        //			{
        //				switch (produtID)
        //				{
        //					case 1102:
        //					case 1103:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1150:
        //							case 1151:
        //							case 1152:
        //							case 1153:
        //							case 1154:
        //							case 1155:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								switch (produtID)
        //								{
        //									case 1171:
        //									case 1172:
        //									case 1173:
        //									{
        //										break;
        //									}
        //									default:
        //									{
        //										flag = flag1;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID <= 1229)
        //			{
        //				switch (produtID)
        //				{
        //					case 1200:
        //					case 1203:
        //					case 1204:
        //					case 1205:
        //					case 1206:
        //					case 1207:
        //					case 1209:
        //					case 1210:
        //					{
        //						break;
        //					}
        //					case 1201:
        //					case 1202:
        //					case 1208:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1228:
        //							case 1229:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID != 1234)
        //			{
        //				switch (produtID)
        //				{
        //					case 1261:
        //					case 1262:
        //					case 1263:
        //					case 1264:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1532)
        //		{
        //			if (produtID > 1603)
        //			{
        //				switch (produtID)
        //				{
        //					case 1612:
        //					case 1613:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1907:
        //							case 1908:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				if (produtID == 1600 || produtID == 1603)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (produtID > 1403)
        //		{
        //			switch (produtID)
        //			{
        //				case 1411:
        //				case 1412:
        //				case 1413:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1531:
        //						case 1532:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1282)
        //		{
        //			switch (produtID)
        //			{
        //				case 1400:
        //				case 1401:
        //				case 1402:
        //				case 1403:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsLadder(int ProdutID)
        //	{
        //		bool flag;
        //		if (!CommonConstants.IsProductGWY_K22(ProdutID))
        //		{
        //			flag = (((CommonConstants.IsProductPLC(ProdutID) || CommonConstants.IsProductFlexiPanels(ProdutID)) && !CommonConstants.IsProductSupportedFP3035(ProdutID) ? CommonConstants.IsProductFL100Special(ProdutID) : true) ? false : true);
        //		}
        //		else
        //		{
        //			flag = false;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportsOnlyUSB(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1373)
        //		{
        //			if (produtID > 1282)
        //			{
        //				if (produtID == 1343 || produtID == 1373)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1240:
        //					case 1241:
        //					case 1242:
        //					case 1243:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1270:
        //							case 1271:
        //							case 1272:
        //							case 1273:
        //							case 1274:
        //							case 1280:
        //							case 1281:
        //							case 1282:
        //							{
        //								break;
        //							}
        //							case 1275:
        //							case 1276:
        //							case 1277:
        //							case 1278:
        //							case 1279:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1573)
        //		{
        //			switch (produtID)
        //			{
        //				case 1642:
        //				case 1643:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1672:
        //						case 1673:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (produtID == 1681)
        //							{
        //								flag1 = true;
        //								flag = flag1;
        //								return flag;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else
        //		{
        //			if (produtID == 1543 || produtID == 1573)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool isProductSupportsReconnectControl(int pProductID)
        //	{
        //		bool flag;
        //		int num = pProductID;
        //		if (num > 1243)
        //		{
        //			if (num > 1281)
        //			{
        //				switch (num)
        //				{
        //					case 1701:
        //					case 1703:
        //					{
        //						break;
        //					}
        //					case 1702:
        //					{
        //						flag = false;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (num)
        //						{
        //							case 1711:
        //							case 1713:
        //							{
        //								break;
        //							}
        //							case 1712:
        //							{
        //								flag = false;
        //								return flag;
        //							}
        //							default:
        //							{
        //								switch (num)
        //								{
        //									case 1721:
        //									case 1723:
        //									{
        //										break;
        //									}
        //									case 1722:
        //									{
        //										flag = false;
        //										return flag;
        //									}
        //									default:
        //									{
        //										flag = false;
        //										return flag;
        //									}
        //								}
        //								break;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (num)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (num)
        //						{
        //							case 1280:
        //							case 1281:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag = false;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (num > 928)
        //		{
        //			switch (num)
        //			{
        //				case 951:
        //				case 952:
        //				case 953:
        //				case 954:
        //				case 955:
        //				case 956:
        //				case 957:
        //				case 958:
        //				case 959:
        //				case 960:
        //				case 961:
        //				case 962:
        //				case 963:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (num)
        //					{
        //						case 1240:
        //						case 1241:
        //						case 1242:
        //						case 1243:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = false;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (num != 918)
        //		{
        //			switch (num)
        //			{
        //				case 927:
        //				case 928:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = false;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag = true;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsTouchscreenOnly(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1543)
        //		{
        //			if (produtID <= 1281)
        //			{
        //				if (produtID > 1243)
        //				{
        //					switch (produtID)
        //					{
        //						case 1270:
        //						case 1271:
        //						case 1272:
        //						case 1273:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1280:
        //								case 1281:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 1130:
        //						case 1132:
        //						{
        //							break;
        //						}
        //						case 1131:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (produtID)
        //							{
        //								case 1211:
        //								case 1212:
        //								case 1213:
        //								case 1214:
        //								case 1215:
        //								case 1216:
        //								case 1217:
        //								case 1218:
        //								case 1219:
        //								case 1220:
        //								case 1221:
        //								case 1222:
        //								case 1223:
        //								case 1224:
        //								case 1225:
        //								case 1226:
        //								case 1227:
        //								case 1240:
        //								case 1241:
        //								case 1242:
        //								case 1243:
        //								{
        //									break;
        //								}
        //								case 1228:
        //								case 1229:
        //								case 1230:
        //								case 1231:
        //								case 1232:
        //								case 1233:
        //								case 1234:
        //								case 1235:
        //								case 1236:
        //								case 1237:
        //								case 1238:
        //								case 1239:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID > 1343)
        //			{
        //				if (produtID == 1373 || produtID == 1543)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1301:
        //					case 1302:
        //					case 1303:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1343)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1643)
        //		{
        //			if (produtID > 1681)
        //			{
        //				switch (produtID)
        //				{
        //					case 1701:
        //					case 1702:
        //					case 1703:
        //					case 1704:
        //					case 1711:
        //					case 1712:
        //					case 1713:
        //					case 1714:
        //					case 1715:
        //					case 1721:
        //					case 1722:
        //					case 1723:
        //					case 1724:
        //					case 1725:
        //					{
        //						break;
        //					}
        //					case 1705:
        //					case 1706:
        //					case 1707:
        //					case 1708:
        //					case 1709:
        //					case 1710:
        //					case 1716:
        //					case 1717:
        //					case 1718:
        //					case 1719:
        //					case 1720:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1901:
        //							case 1902:
        //							case 1903:
        //							case 1904:
        //							case 1905:
        //							case 1906:
        //							case 1909:
        //							case 1910:
        //							case 1911:
        //							case 1912:
        //							{
        //								break;
        //							}
        //							case 1907:
        //							case 1908:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1681)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID > 1624)
        //		{
        //			switch (produtID)
        //			{
        //				case 1634:
        //				case 1635:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1642:
        //						case 1643:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1573)
        //		{
        //			switch (produtID)
        //			{
        //				case 1621:
        //				case 1622:
        //				case 1623:
        //				case 1624:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportsTrend(int productId)
        //	{
        //		return ((productId == 1218 || CommonConstants.IsProductSupportedFP3035(productId) || CommonConstants.IsProductSupportedFP4020MR(productId) || CommonConstants.IsProductSupportedFP4030MT(productId) ? false : !CommonConstants.IsProductSupportedFP4030MR(productId)) ? true : false);
        //	}

        //	public static bool IsProductSupportStringDataType(int ProductID)
        //	{
        //		bool flag;
        //		if (!CommonConstants.g_Support_IEC_Ladder)
        //		{
        //			flag = false;
        //		}
        //		else if (CommonConstants.IsProductMX257_Based(ProductID))
        //		{
        //			flag = true;
        //		}
        //		else if (CommonConstants.IsProductMXSpecialCase_OldModel(ProductID))
        //		{
        //			flag = true;
        //		}
        //		else if (CommonConstants.IsProductFL005MicroPLCBase(ProductID))
        //		{
        //			flag = true;
        //		}
        //		else if (ProductID != 920)
        //		{
        //			flag = (CommonConstants.ProductDataInfo.iProductID != 2003 ? false : true);
        //		}
        //		else
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductSupportsWebScreens(int pProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int num = pProductID;
        //		if (num <= 1573)
        //		{
        //			if (num <= 1273)
        //			{
        //				if (num > 944)
        //				{
        //					switch (num)
        //					{
        //						case 978:
        //						case 980:
        //						{
        //							break;
        //						}
        //						case 979:
        //						{
        //							flag1 = false;
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							switch (num)
        //							{
        //								case 1242:
        //								case 1243:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									switch (num)
        //									{
        //										case 1272:
        //										case 1273:
        //										{
        //											break;
        //										}
        //										default:
        //										{
        //											flag1 = false;
        //											flag = flag1;
        //											return flag;
        //										}
        //									}
        //									break;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (num)
        //					{
        //						case 918:
        //						case 919:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (num == 944)
        //							{
        //								break;
        //							}
        //							flag1 = false;
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (num > 1343)
        //			{
        //				if (num == 1373 || num == 1543 || num == 1573)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag1 = false;
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (num)
        //				{
        //					case 1281:
        //					case 1282:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (num == 1343)
        //						{
        //							break;
        //						}
        //						flag1 = false;
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (num <= 1715)
        //		{
        //			if (num <= 1673)
        //			{
        //				switch (num)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (num)
        //						{
        //							case 1672:
        //							case 1673:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag1 = false;
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (num != 1681)
        //			{
        //				switch (num)
        //				{
        //					case 1703:
        //					case 1704:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						switch (num)
        //						{
        //							case 1713:
        //							case 1714:
        //							case 1715:
        //							{
        //								break;
        //							}
        //							default:
        //							{
        //								flag1 = false;
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //		}
        //		else if (num <= 1803)
        //		{
        //			switch (num)
        //			{
        //				case 1723:
        //				case 1724:
        //				case 1725:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (num == 1803)
        //					{
        //						break;
        //					}
        //					flag1 = false;
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (num != 1813 && num != 1823)
        //		{
        //			switch (num)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag1 = false;
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag1 = false;
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsProductSupportTrueTypeFont(int pProductID)
        //	{
        //		bool flag = false;
        //		if ((CommonConstants.IsProductSupportedFP4035(pProductID) || CommonConstants.IsProductSupportedFP4057(pProductID) || CommonConstants.IsProductSupportedFP5043(pProductID) || CommonConstants.IsProductSupportedFP5070(pProductID) || CommonConstants.IsProductSupportedFP5121(pProductID) ? true : CommonConstants.IsProductSupportedFP3035(pProductID)))
        //		{
        //			flag = true;
        //		}
        //		if (CommonConstants.ProductDataInfo.iProductID == 1234)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductUSBPortReset(int ProdutID)
        //	{
        //		bool flag = true;
        //		if ((CommonConstants.IsProductSupportedFP4035(ProdutID) || CommonConstants.IsProductSupportedFP4057(ProdutID) || CommonConstants.IsProductGateway(ProdutID) || ProdutID == 913 || ProdutID == 912 || ProdutID == 915 || ProdutID == 914 || ProdutID == 917 || ProdutID == 941 || ProdutID == 942 ? true : ProdutID == 943))
        //		{
        //			if (ProdutID != 2003)
        //			{
        //				flag = false;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsProductWithLocalIO()
        //	{
        //		return ((CommonConstants.ProductIdentifier == 1215 || CommonConstants.ProductIdentifier == 1216 || CommonConstants.ProductIdentifier == 1219 || CommonConstants.ProductIdentifier == 1220 || CommonConstants.ProductIdentifier == 1217 || CommonConstants.ProductIdentifier == 1909 || CommonConstants.ProductIdentifier == 1910 || CommonConstants.ProductIdentifier == 1222 || CommonConstants.ProductIdentifier == 1223 || CommonConstants.ProductIdentifier == 1224 || CommonConstants.ProductIdentifier == 1225 || CommonConstants.ProductIdentifier == 1228 || CommonConstants.ProductIdentifier == 1229 || CommonConstants.ProductIdentifier == 1261 || CommonConstants.ProductIdentifier == 1262 || CommonConstants.ProductIdentifier == 1263 || CommonConstants.ProductIdentifier == 1264 || CommonConstants.ProductIdentifier == 1226 || CommonConstants.ProductIdentifier == 1227 || CommonConstants.ProductIdentifier == 1623 || CommonConstants.ProductIdentifier == 1624 || CommonConstants.ProductIdentifier == 1301 || CommonConstants.ProductIdentifier == 1302 || CommonConstants.ProductIdentifier == 1171 || CommonConstants.ProductIdentifier == 1172 || CommonConstants.ProductIdentifier == 1173 || CommonConstants.ProductIdentifier == 1303 || CommonConstants.IsProductFL005MicroPLCBase(CommonConstants.ProductIdentifier) ? false : !CommonConstants.IsProductSupportedFP3020series(CommonConstants.ProductIdentifier)) ? false : true);
        //	}

        //	public static bool IsProductWithoutRTC(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID != 1330 && produtID != 1350)
        //		{
        //			switch (produtID)
        //			{
        //				case 2001:
        //				case 2002:
        //				case 2003:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsScreenSaverSupported(int pProductID)
        //	{
        //		int num = 0;
        //		DataRow[] dataRowArray = CommonConstants.dsRecentProjectList.Tables["UnitInformation"].Select(string.Concat("ModelNo='", pProductID, "'"));
        //		DataRow[] dataRowArray1 = dataRowArray;
        //		for (int i = 0; i < (int)dataRowArray1.Length; i++)
        //		{
        //			DataRow dataRow = dataRowArray1[i];
        //			num = Convert.ToInt32(dataRow["ScreenSaver"]);
        //		}
        //		return (num != 1 ? false : true);
        //	}

        //	public static bool IsSDCardLogUpload_TaskSupported(int ProductId)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productId = ProductId;
        //		if (productId <= 1714)
        //		{
        //			if (productId <= 970)
        //			{
        //				if (productId == 920 || productId == 970)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else if (productId != 1703)
        //			{
        //				switch (productId)
        //				{
        //					case 1713:
        //					case 1714:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productId <= 1803)
        //		{
        //			switch (productId)
        //			{
        //				case 1723:
        //				case 1724:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (productId == 1803)
        //					{
        //						break;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		else if (productId != 1813 && productId != 1823)
        //		{
        //			switch (productId)
        //			{
        //				case 1902:
        //				case 1904:
        //				case 1906:
        //				{
        //					break;
        //				}
        //				case 1903:
        //				case 1905:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsSDCardUploadTaskSupported(int ProductId)
        //	{
        //		bool flag = false;
        //		int productId = ProductId;
        //		if (productId == 920 || productId == 970)
        //		{
        //			flag = true;
        //		}
        //		return flag;
        //	}

        //	public static bool IsSpecialProduct(int ProductId)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productId = ProductId;
        //		if (productId <= 1228)
        //		{
        //			if (productId > 1154)
        //			{
        //				if (productId == 1217 || productId == 1228)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				if (productId == 917 || productId == 1154)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //		}
        //		else if (productId > 1252)
        //		{
        //			if (productId == 1255 || productId == 1274 || productId == 1282)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else
        //		{
        //			if (productId == 1234 || productId == 1252)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsTemplateScreen(int pActiveScreenNumber)
        //	{
        //		return ((pActiveScreenNumber < 64991 ? true : pActiveScreenNumber > 65000) ? false : true);
        //	}

        //	public static bool IsToshiba_FlexiProduct(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1351)
        //		{
        //			switch (produtID)
        //			{
        //				case 1330:
        //				case 1331:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (produtID)
        //					{
        //						case 1350:
        //						case 1351:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID != 1543 && produtID != 1551)
        //		{
        //			switch (produtID)
        //			{
        //				case 1571:
        //				case 1573:
        //				{
        //					break;
        //				}
        //				case 1572:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsTouchScreenObject(int pProductId)
        //	{
        //		return (pProductId <= 509 ? false : true);
        //	}

        //	public static bool IsUSBHostSupported(int ProdutID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int produtID = ProdutID;
        //		if (produtID <= 1422)
        //		{
        //			if (produtID <= 1256)
        //			{
        //				if (produtID > 970)
        //				{
        //					switch (produtID)
        //					{
        //						case 978:
        //						case 980:
        //						{
        //							break;
        //						}
        //						case 979:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							if (produtID == 1154)
        //							{
        //								break;
        //							}
        //							switch (produtID)
        //							{
        //								case 1230:
        //								case 1231:
        //								case 1232:
        //								case 1233:
        //								case 1234:
        //								case 1240:
        //								case 1241:
        //								case 1242:
        //								case 1243:
        //								case 1250:
        //								case 1251:
        //								case 1252:
        //								case 1253:
        //								case 1254:
        //								case 1255:
        //								case 1256:
        //								{
        //									break;
        //								}
        //								case 1235:
        //								case 1236:
        //								case 1237:
        //								case 1238:
        //								case 1239:
        //								case 1244:
        //								case 1245:
        //								case 1246:
        //								case 1247:
        //								case 1248:
        //								case 1249:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					switch (produtID)
        //					{
        //						case 918:
        //						case 919:
        //						case 920:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							if (produtID == 944 || produtID == 970)
        //							{
        //								break;
        //							}
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //				}
        //			}
        //			else if (produtID <= 1343)
        //			{
        //				switch (produtID)
        //				{
        //					case 1270:
        //					case 1271:
        //					case 1272:
        //					case 1273:
        //					case 1274:
        //					case 1280:
        //					case 1281:
        //					case 1282:
        //					{
        //						break;
        //					}
        //					case 1275:
        //					case 1276:
        //					case 1277:
        //					case 1278:
        //					case 1279:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						switch (produtID)
        //						{
        //							case 1331:
        //							case 1333:
        //							{
        //								break;
        //							}
        //							case 1332:
        //							{
        //								flag = flag1;
        //								return flag;
        //							}
        //							default:
        //							{
        //								if (produtID == 1343)
        //								{
        //									flag1 = true;
        //									flag = flag1;
        //									return flag;
        //								}
        //								flag = flag1;
        //								return flag;
        //							}
        //						}
        //						break;
        //					}
        //				}
        //			}
        //			else if (produtID <= 1354)
        //			{
        //				if (produtID == 1351 || produtID == 1354)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else if (produtID != 1373)
        //			{
        //				switch (produtID)
        //				{
        //					case 1421:
        //					case 1422:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1650)
        //		{
        //			if (produtID <= 1551)
        //			{
        //				switch (produtID)
        //				{
        //					case 1431:
        //					case 1432:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1543 || produtID == 1551)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (produtID > 1630)
        //			{
        //				switch (produtID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (produtID == 1650)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else
        //			{
        //				switch (produtID)
        //				{
        //					case 1571:
        //					case 1573:
        //					{
        //						break;
        //					}
        //					case 1572:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //					default:
        //					{
        //						if (produtID == 1630)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (produtID <= 1725)
        //		{
        //			switch (produtID)
        //			{
        //				case 1672:
        //				case 1673:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					if (produtID == 1681)
        //					{
        //						break;
        //					}
        //					switch (produtID)
        //					{
        //						case 1701:
        //						case 1702:
        //						case 1703:
        //						case 1704:
        //						case 1711:
        //						case 1712:
        //						case 1713:
        //						case 1714:
        //						case 1715:
        //						case 1721:
        //						case 1722:
        //						case 1723:
        //						case 1724:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1705:
        //						case 1706:
        //						case 1707:
        //						case 1708:
        //						case 1709:
        //						case 1710:
        //						case 1716:
        //						case 1717:
        //						case 1718:
        //						case 1719:
        //						case 1720:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (produtID <= 1813)
        //		{
        //			if (produtID == 1803 || produtID == 1813)
        //			{
        //				flag1 = true;
        //				flag = flag1;
        //				return flag;
        //			}
        //			flag = flag1;
        //			return flag;
        //		}
        //		else if (produtID != 1823)
        //		{
        //			switch (produtID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsUSBLogUpload_TaskSupported(int ProdutID)
        //	{
        //		bool flag;
        //		if (CommonConstants.IsUSBHostSupported(ProdutID))
        //		{
        //			if (CommonConstants.IsProductSupportsDataLogger(ProdutID))
        //			{
        //				flag = true;
        //				return flag;
        //			}
        //		}
        //		flag = false;
        //		return flag;
        //	}

        //	public static bool IsValidExtensionForBitmap(string pstrExtension)
        //	{
        //		return ((pstrExtension == CommonConstants.OnlyDotBmP || pstrExtension == CommonConstants.OnlyDotBMP || pstrExtension == CommonConstants.OnlyDotbmp || pstrExtension == CommonConstants.OnlyDotbMp || pstrExtension == CommonConstants.OnlyDotBmp || pstrExtension == CommonConstants.OnlyDotbmP || pstrExtension == CommonConstants.OnlyDotBMp ? false : !(pstrExtension == CommonConstants.OnlyDotbMP)) ? false : true);
        //	}

        //	public static bool IsValidPressedKeyTask(KeyTaskCode pkeyCode)
        //	{
        //		bool flag;
        //		KeyTaskCode keyTaskCode = pkeyCode;
        //		switch (keyTaskCode)
        //		{
        //			case KeyTaskCode.IncreaseValueByOne:
        //			case KeyTaskCode.DecreaseValueByOne:
        //			case KeyTaskCode.IncreaseDigitByOne:
        //			case KeyTaskCode.DecreaseDigitByOne:
        //			{
        //			Label0:
        //				flag = true;
        //				break;
        //			}
        //			default:
        //			{
        //				switch (keyTaskCode)
        //				{
        //					case KeyTaskCode.PreviousAlarm:
        //					case KeyTaskCode.NextAlarm:
        //					case KeyTaskCode.PreviousHISAlarm:
        //					case KeyTaskCode.NextHISAlarm:
        //					{
        //						goto Label0;
        //					}
        //					case KeyTaskCode.StartLogger:
        //					case KeyTaskCode.StopLogger:
        //					case KeyTaskCode.ClearLogMemory:
        //					{
        //						flag = false;
        //						break;
        //					}
        //					default:
        //					{
        //						if (keyTaskCode == KeyTaskCode.RefreshTrendWindow)
        //						{
        //							goto Label0;
        //						}
        //						goto case KeyTaskCode.ClearLogMemory;
        //					}
        //				}
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool IsVerticalProduct(int ProductId)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productId = ProductId;
        //		if (productId <= 1227)
        //		{
        //			switch (productId)
        //			{
        //				case 1212:
        //				case 1214:
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				case 1213:
        //				{
        //					break;
        //				}
        //				default:
        //				{
        //					switch (productId)
        //					{
        //						case 1221:
        //						case 1222:
        //						case 1223:
        //						case 1224:
        //						case 1225:
        //						case 1227:
        //						{
        //							flag1 = true;
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (productId == 1256 || productId == 1622)
        //		{
        //			flag1 = true;
        //			flag = flag1;
        //			return flag;
        //		}
        //		if (CommonConstants._isProductVertical)
        //		{
        //			flag1 = true;
        //		}
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static bool IsWebScreen(int pScreenNumber)
        //	{
        //		return ((pScreenNumber < 64000 ? true : pScreenNumber > 64900) ? false : true);
        //	}

        //	[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
        //	private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

        //	public static byte LOBYTE(int iValue)
        //	{
        //		return Convert.ToByte(iValue & 255);
        //	}

        //	public static int MAKEINT(byte[] pTemparr)
        //	{
        //		int num = (pTemparr[3] & 255) << 24 | (pTemparr[2] & 255) << 16 | (pTemparr[1] & 255) << 8 | pTemparr[0] & 255;
        //		return num;
        //	}

        //	public static uint MAKEUINT(byte[] pTemparr)
        //	{
        //		uint num = (uint)((pTemparr[3] & 255) << 24 | (pTemparr[2] & 255) << 16 | (pTemparr[1] & 255) << 8 | pTemparr[0] & 255);
        //		return num;
        //	}

        //	public static ushort MAKEUSHORT(byte a, byte b)
        //	{
        //		return (ushort)(a | b << 8);
        //	}

        //	public static ushort MAKEUWORD(byte a, byte b)
        //	{
        //		uint num = Convert.ToUInt16(b);
        //		return Convert.ToUInt16(num << 8 | a);
        //	}

        //	public static short MAKEWORD(byte a, byte b)
        //	{
        //		return (short)(a | b << 8);
        //	}

        //	public static ushort MAKEWORD(byte[] pTemparr)
        //	{
        //		uint num = Convert.ToUInt16(pTemparr[1]) << 8;
        //		return Convert.ToUInt16(num | pTemparr[0]);
        //	}

        //	public static bool OIS42PlusApplicationConversion(int ProductID)
        //	{
        //		bool flag = false;
        //		switch (ProductID)
        //		{
        //			case 1623:
        //			{
        //				flag = true;
        //				break;
        //			}
        //			case 1624:
        //			{
        //				flag = true;
        //				break;
        //			}
        //		}
        //		return flag;
        //	}

        //	public static bool ProductsupportLREL(int ProductID)
        //	{
        //		bool flag;
        //		bool flag1 = false;
        //		int productID = ProductID;
        //		if (productID <= 1543)
        //		{
        //			if (productID <= 1273)
        //			{
        //				if (productID > 978)
        //				{
        //					switch (productID)
        //					{
        //						case 1240:
        //						case 1241:
        //						case 1242:
        //						case 1243:
        //						{
        //							break;
        //						}
        //						default:
        //						{
        //							switch (productID)
        //							{
        //								case 1270:
        //								case 1271:
        //								case 1272:
        //								case 1273:
        //								{
        //									break;
        //								}
        //								default:
        //								{
        //									flag = flag1;
        //									return flag;
        //								}
        //							}
        //							break;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					if (productID == 918 || productID == 978)
        //					{
        //						flag1 = true;
        //						flag = flag1;
        //						return flag;
        //					}
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //			else if (productID > 1343)
        //			{
        //				if (productID == 1373 || productID == 1543)
        //				{
        //					flag1 = true;
        //					flag = flag1;
        //					return flag;
        //				}
        //				flag = flag1;
        //				return flag;
        //			}
        //			else
        //			{
        //				switch (productID)
        //				{
        //					case 1280:
        //					case 1281:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1343)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1681)
        //		{
        //			if (productID > 1643)
        //			{
        //				switch (productID)
        //				{
        //					case 1672:
        //					case 1673:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						if (productID == 1681)
        //						{
        //							break;
        //						}
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //			else if (productID != 1573)
        //			{
        //				switch (productID)
        //				{
        //					case 1642:
        //					case 1643:
        //					{
        //						break;
        //					}
        //					default:
        //					{
        //						flag = flag1;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		else if (productID <= 1725)
        //		{
        //			switch (productID)
        //			{
        //				case 1701:
        //				case 1702:
        //				case 1703:
        //				case 1704:
        //				case 1711:
        //				case 1712:
        //				case 1713:
        //				case 1715:
        //				{
        //					break;
        //				}
        //				case 1705:
        //				case 1706:
        //				case 1707:
        //				case 1708:
        //				case 1709:
        //				case 1710:
        //				case 1714:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					switch (productID)
        //					{
        //						case 1721:
        //						case 1722:
        //						case 1723:
        //						case 1725:
        //						{
        //							break;
        //						}
        //						case 1724:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //						default:
        //						{
        //							flag = flag1;
        //							return flag;
        //						}
        //					}
        //					break;
        //				}
        //			}
        //		}
        //		else if (productID != 1803 && productID != 1813)
        //		{
        //			switch (productID)
        //			{
        //				case 1901:
        //				case 1902:
        //				case 1903:
        //				case 1904:
        //				case 1905:
        //				case 1906:
        //				case 1911:
        //				case 1912:
        //				{
        //					break;
        //				}
        //				case 1907:
        //				case 1908:
        //				case 1909:
        //				case 1910:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //				default:
        //				{
        //					flag = flag1;
        //					return flag;
        //				}
        //			}
        //		}
        //		flag1 = true;
        //		flag = flag1;
        //		return flag;
        //	}

        //	public static object RawDeserialize(byte[] rawdatas, Type anytype)
        //	{
        //		object obj;
        //		int num = Marshal.SizeOf(anytype);
        //		if (num <= (int)rawdatas.Length)
        //		{
        //			IntPtr intPtr = Marshal.AllocHGlobal(num);
        //			Marshal.Copy(rawdatas, 0, intPtr, num);
        //			object structure = Marshal.PtrToStructure(intPtr, anytype);
        //			Marshal.FreeHGlobal(intPtr);
        //			obj = structure;
        //		}
        //		else
        //		{
        //			obj = null;
        //		}
        //		return obj;
        //	}

        //	public static byte[] RawSerialize(object anything)
        //	{
        //		int num = Marshal.SizeOf(anything);
        //		IntPtr intPtr = Marshal.AllocHGlobal(num);
        //		Marshal.StructureToPtr(anything, intPtr, false);
        //		byte[] numArray = new byte[num];
        //		Marshal.Copy(intPtr, numArray, 0, num);
        //		Marshal.FreeHGlobal(intPtr);
        //		return numArray;
        //	}

        //	public static int ReadWord(FileStream pFileStream)
        //	{
        //		byte[] numArray = new byte[2];
        //		pFileStream.Read(numArray, 0, 2);
        //		return CommonConstants.MAKEWORD(numArray);
        //	}

        //	private static int ReturnColorIndex(int pColor)
        //	{
        //		int num;
        //		int num1 = 0;
        //		int num2 = -1;
        //		int num3 = 0;
        //		int num4 = 255;
        //		int num5 = 240;
        //		int r = Color.FromArgb(pColor).R;
        //		int g = Color.FromArgb(pColor).G;
        //		int b = Color.FromArgb(pColor).B;
        //		if (r == num4)
        //		{
        //			r = num5;
        //		}
        //		if (g == num4)
        //		{
        //			g = num5;
        //		}
        //		if (b == num4)
        //		{
        //			b = num5;
        //		}
        //		num1 = num4;
        //		num2 = 0;
        //		while (true)
        //		{
        //			if ((num2 > num1 ? true : num2 >= CommonConstants.ProductDataInfo.ColorArray.Length / 3))
        //			{
        //				break;
        //			}
        //			if (r == CommonConstants.ProductDataInfo.ColorArray[num2, 0] && g == CommonConstants.ProductDataInfo.ColorArray[num2, 1] && b == CommonConstants.ProductDataInfo.ColorArray[num2, 2])
        //			{
        //				num3 = 1;
        //			}
        //			if (num3 != 1)
        //			{
        //				num2++;
        //			}
        //			else
        //			{
        //				break;
        //			}
        //		}
        //		if (num3 != 0)
        //		{
        //			num = (num2 != CommonConstants.ProductDataInfo.ColorArray.Length / 3 ? num2 : num2 - 1);
        //		}
        //		else
        //		{
        //			num = -1;
        //		}
        //		return num;
        //	}

        //	public static byte ReverseByte(byte pbtValue)
        //	{
        //		int num = Convert.ToInt32(pbtValue);
        //		int num1 = num & 15;
        //		num >>= 4;
        //		num = num + (num1 << 4);
        //		return Convert.ToByte(num);
        //	}

        //	public static void SelBlockType(int selIECBlockType1)
        //	{
        //		CommonConstants.selIECBlockType = selIECBlockType1;
        //	}

        //	[DllImport("gdi32.dll", CharSet=CharSet.None, ExactSpelling=false)]
        //	private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        //	public static void SetActiveFormObject(Form pFormObject)
        //	{
        //		CommonConstants._commconstantarrFormObject.Clear();
        //		CommonConstants._commconstantarrFormObject.Add(pFormObject);
        //	}

        //	public static void SetBlockType(int m_lBlockType)
        //	{
        //		CommonConstants.m_gBlockType = m_lBlockType;
        //	}

        //	public static int SetExpansion_ProductID(int ModuleType, int ProductID)
        //	{
        //		int num = 0;
        //		string empty = string.Empty;
        //		DataSet dataSet = new DataSet();
        //		if (CommonConstants.IsProductPLC(ProductID))
        //		{
        //			dataSet.ReadXml("LdrConfigPLC.xml");
        //		}
        //		else if ((ProductID == 1702 || ProductID == 1704 || ProductID == 1712 || ProductID == 1715 || ProductID == 1722 ? false : ProductID != 1725))
        //		{
        //			dataSet.ReadXml(CommonConstants.IOExpansionFile);
        //		}
        //		else
        //		{
        //			dataSet.ReadXml("LdrIOFlexi3XX.xml");
        //		}
        //		DataTable item = dataSet.Tables["Slot_One"];
        //		int num1 = 1;
        //		while (num1 < item.Rows.Count)
        //		{
        //			if (num1 != ModuleType)
        //			{
        //				num1++;
        //			}
        //			else
        //			{
        //				empty = item.Rows[num1]["ExpProduct_ID"].ToString();
        //				num = Convert.ToInt32(empty);
        //				break;
        //			}
        //		}
        //		return num;
        //	}

        //	public static string SetMonitoringDealy()
        //	{
        //		char[] chrArray;
        //		StreamReader streamReader = new StreamReader("MonitoringDealyUSB.txt");
        //		string str = "";
        //		string str1 = "";
        //		string[] strArrays = new string[2];
        //		str = streamReader.ReadLine();
        //		str1 = streamReader.ReadLine();
        //		if (!CommonConstants.g_Support_IEC_Ladder)
        //		{
        //			chrArray = new char[] { '=' };
        //			strArrays = str1.Split(chrArray);
        //		}
        //		else
        //		{
        //			chrArray = new char[] { '=' };
        //			strArrays = str.Split(chrArray);
        //		}
        //		return strArrays[1];
        //	}

        //	public static void SetPatternIndex(object pPattern, ref byte pPatternIndex)
        //	{
        //		if (Convert.ToInt32(pPattern) == 0)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.NOFILL);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 14)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.ONE_BLACK_ONE_WHITE);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 16)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.THREE_BLACK_ONE_WHITE);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 7)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.ONE_BLACK_THREE_WHITE);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 12)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.ONE_WHITE_ONE_BLACK);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 25)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.HORIZONTAL);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 24)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.VERTICAL);
        //		}
        //		else if (Convert.ToInt32(pPattern) == 48)
        //		{
        //			pPatternIndex = Convert.ToByte(PatternBrush.CROSS);
        //		}
        //	}

        //	public static void SetTTFFontInfo(ref CommonConstants.FontInfo pFont)
        //	{
        //		pFont._fPtSzForFont = 0;
        //		pFont._fFontHeight = 12;
        //		pFont._fFontWidth = 0;
        //		pFont._fEscapement = 0;
        //		pFont._fOrientation = 0;
        //		pFont._fWeight = 400;
        //		pFont._fItalic = 0;
        //		pFont._fUnderline = 0;
        //		pFont._fStrikeOut = 0;
        //		pFont._fCharSet = 129;
        //		pFont._fOutPrecision = 0;
        //		pFont._fClipPrecision = 0;
        //		pFont._fQuality = 0;
        //		pFont._fPitchFamily = 0;
        //		pFont._fLenOfFaceName = 5;
        //	}

        //	public static string ShortArrayToString(short[] pShortVal)
        //	{
        //		char[] chr = new char[(int)pShortVal.Length];
        //		for (int i = 0; i < (int)pShortVal.Length; i++)
        //		{
        //			chr[i] = Convert.ToChar(pShortVal[i]);
        //		}
        //		return new string(chr);
        //	}

        //	public static void StringToByteArray(string pStrVal, byte[] pByteVal)
        //	{
        //		char[] chrArray = new char[pStrVal.Length];
        //		pStrVal.CopyTo(0, chrArray, 0, pStrVal.Length);
        //		for (int i = 0; i < pStrVal.Length; i++)
        //		{
        //			pByteVal[i] = Convert.ToByte(chrArray[i]);
        //		}
        //	}

        //	public static void StringToIntArray(string pStrVal, int[] pIntVal)
        //	{
        //		char[] chrArray = new char[pStrVal.Length];
        //		pStrVal.CopyTo(0, chrArray, 0, pStrVal.Length);
        //		for (int i = 0; i < pStrVal.Length; i++)
        //		{
        //			pIntVal[i] = Convert.ToUInt16(chrArray[i]);
        //		}
        //	}

        //	public static void StringToShortArray(string pStrVal, short[] pShortVal)
        //	{
        //		char[] chrArray = new char[pStrVal.Length];
        //		pStrVal.CopyTo(0, chrArray, 0, pStrVal.Length);
        //		for (int i = 0; i < pStrVal.Length; i++)
        //		{
        //			pShortVal[i] = Convert.ToInt16(chrArray[i]);
        //		}
        //	}

        //	public static void StringToUShortArray(string pStrVal, ushort[] pShortVal)
        //	{
        //		char[] chrArray = new char[pStrVal.Length];
        //		pStrVal.CopyTo(0, chrArray, 0, pStrVal.Length);
        //		for (int i = 0; i < pStrVal.Length; i++)
        //		{
        //			pShortVal[i] = Convert.ToUInt16(chrArray[i]);
        //		}
        //	}

        //	public static List<T> ToList<T>(ArrayList arrayList)
        //	{
        //		List<T> ts = new List<T>(arrayList.Count);
        //		foreach (T t in arrayList)
        //		{
        //			ts.Add(t);
        //		}
        //		return ts;
        //	}

        //	public static string UShortArrayToString(ushort[] pShortVal)
        //	{
        //		char[] chr = new char[(int)pShortVal.Length];
        //		for (int i = 0; i < (int)pShortVal.Length; i++)
        //		{
        //			chr[i] = Convert.ToChar(pShortVal[i]);
        //		}
        //		return new string(chr);
        //	}

        //	public static void Validation_RetentiveMemory(string currentAddr, string prevAddr)
        //	{
        //		if (currentAddr[0] == CommonConstants.Retentive_Prefix)
        //		{
        //			if (prevAddr != string.Empty)
        //			{
        //				if (currentAddr != prevAddr)
        //				{
        //					if ((CommonConstants.IsProductFL005MicroPLCBase(CommonConstants.ProductDataInfo.iProductID) || CommonConstants.IsProductFL005ExpandablePLCSeries(CommonConstants.ProductDataInfo.iProductID) || CommonConstants.IsProductGateway(CommonConstants.ProductDataInfo.iProductID) || CommonConstants.ProductDataInfo.iProductID == 1171 || CommonConstants.ProductDataInfo.iProductID == 1172 ? false : CommonConstants.ProductDataInfo.iProductID != 1173))
        //					{
        //						MessageBox.Show(CommonConstants.Retentive_MemoryLifeCycle, CommonConstants.Retentive_MemoryLifeCycle_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //					}
        //					else
        //					{
        //						MessageBox.Show(CommonConstants.Retentive_MemoryLifeCycle_2, CommonConstants.Retentive_MemoryLifeCycle_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //					}
        //				}
        //			}
        //		}
        //	}

        //	public static bool VerticalProductRemoveList(int ProductId)
        //	{
        //		bool flag;
        //		string str = "VerticalproductList.xml";
        //		string str1 = "ListToRemove";
        //		DataSet dataSet = new DataSet();
        //		FileStream fileStream = null;
        //		try
        //		{
        //			if (File.Exists(str))
        //			{
        //				fileStream = new FileStream(str, FileMode.Open);
        //				dataSet.ReadXml(fileStream);
        //				DataTable item = dataSet.Tables[str1];
        //				int num = 0;
        //				while (num < item.Rows.Count)
        //				{
        //					if (Convert.ToInt32(item.Rows[num][0].ToString().Trim()) != ProductId)
        //					{
        //						num++;
        //					}
        //					else
        //					{
        //						fileStream.Close();
        //						flag = true;
        //						return flag;
        //					}
        //				}
        //			}
        //		}
        //		catch (Exception exception)
        //		{
        //			if (fileStream != null)
        //			{
        //				fileStream.Close();
        //			}
        //			flag = true;
        //			return flag;
        //		}
        //		if (fileStream != null)
        //		{
        //			fileStream.Close();
        //		}
        //		flag = false;
        //		return flag;
        //	}

        //	[Serializable]
        //	public struct AccessLevelUserData
        //	{
        //		public int _userID;

        //		public string _userName;

        //		public string _userPassword;

        //		public int _userAccessLevel;

        //		public string _userDsc;
        //	}

        //	public struct AlarmConfigInfo
        //	{
        //		public short almConfigInfoTotalBytes;

        //		public byte almType;

        //		public byte almAutoAck;

        //		public ushort almScanTime;

        //		public byte almActIfBufferFull;

        //		public string almErrText;

        //		public byte almErrTextLength;
        //	}

        //	[Serializable]
        //	public struct AlarmGroup
        //	{
        //		public byte GroupNumber;

        //		public byte adjbyte;

        //		public byte[] DirectAddressTag;

        //		public byte[] IndirectAddressTag;
        //	}

        //	[Serializable]
        //	public struct AlarmInfo
        //	{
        //		public ushort AlarmNumber;

        //		public byte BitAlarmNumber;

        //		public byte GroupNumber;

        //		public byte NoOfLanguges;

        //		public byte[] LanguageIndex;

        //		public string[] AlarmText;

        //		public bool AlarmActions;

        //		public int TagId;

        //		public uint AlarmId;

        //		public string AlarmName;

        //		public byte AlarmAttribute;

        //		public byte IsAlarmAssign;

        //		public byte Print;

        //		public byte Severity;

        //		public byte AutoAck;

        //		public byte[] DirectAddressAutoAckTag;

        //		public byte[] IndirectAddressAutoAckTag;

        //		public int AutoAckTagValue;

        //		public int AutoAckTagId;

        //		public byte ConditionAlarmFlag;

        //		public byte ConditionalOpretor;

        //		public byte CompareWith;

        //		public ushort CondConstantValue;

        //		public byte[] DirectAddressTag;

        //		public byte[] IndirectAddressTag;
        //	}

        //	public struct AlarmInformation
        //	{
        //		public string _AlarmAssign;

        //		public string _AlarmNumber;

        //		public string _AlarmTag;

        //		public string _Severity;

        //		public string _Print;

        //		public string _AlarmText;
        //	}

        //	public struct AlarmParameters
        //	{
        //		public int LanguageId;

        //		public string _strY;

        //		public string _strN;

        //		public string _strYes;

        //		public string _strNo;
        //	}

        //	[Serializable]
        //	public struct AnalogInputConfiguration
        //	{
        //		public byte InputChannelNo;

        //		public byte InputChannelTypeIndex;

        //		public byte InputChannelDependentTypeIndex;

        //		public byte InputChannelIndex;

        //		public byte InputDataRegister1TypeIndex;

        //		public byte InputCalibration;

        //		public short InputDataRegister1Value;

        //		public byte InputDataRegister2TypeIndex;

        //		public byte InputChannelTypeChar;

        //		public short InputDataRegister2Value;

        //		public byte InputDataRegister3TypeIndex;

        //		public byte AdjustingByte1;

        //		public short InputDataRegister3Value;

        //		public byte InputDataRegister4TypeIndex;

        //		public byte NormalizationFactor;

        //		public short InputDataRegister4Value;

        //		public byte BaudRate;

        //		public byte Parity;

        //		public byte DataBit;

        //		public byte StopBit;

        //		public byte DeviceId;

        //		public byte[] ReservedBytes;
        //	}

        //	[Serializable]
        //	public struct AnalogOutputConfiguration
        //	{
        //		public byte OutputChannelNo;

        //		public byte OutputChannelTypeIndex;

        //		public byte OutputChannelDependentTypeIndex;

        //		public byte OutputChannelIndex;

        //		public byte OutputDataRegister1TypeIndex;

        //		public byte OutputCalibration;

        //		public short OutputDataRegister1Value;

        //		public byte OutputDataRegister2TypeIndex;

        //		public byte OutputChannelTypeChar;

        //		public short OutputDataRegister2Value;

        //		public byte OutputDataRegister3TypeIndex;

        //		public byte AdjustingByte3;

        //		public short OutputDataRegister3Value;

        //		public byte OutputDataRegister4TypeIndex;

        //		public byte AdjustingByte4;

        //		public short OutputDataRegister4Value;

        //		public byte[] ReservedBytes;
        //	}

        //	public struct ApplicationInformation
        //	{
        //		public string _Title;

        //		public string _Author;

        //		public string DateLastEdited;

        //		public string TimeLastEdited;
        //	}

        //	public struct ApplicationTaskListInformation
        //	{
        //		public List<string> _PowerOnTasks;

        //		public List<string> _GlobalTasks;
        //	}

        //	public struct ASCIITagInfo
        //	{
        //		public int TagID;

        //		public int AsciiLen;
        //	}

        //	[Serializable]
        //	public struct AssociatedScreenInfo
        //	{
        //		public uint BaseScreenNo;

        //		public List<int> AssoTemplateList;

        //		public List<string> AssoTemplateNames;

        //		public AssociatedScreenInfo(int i)
        //		{
        //			this.BaseScreenNo = 0;
        //			this.AssoTemplateList = new List<int>();
        //			this.AssoTemplateNames = new List<string>();
        //		}
        //	}

        //	public struct BitmapFileStructure
        //	{
        //		public short _lengthOfBitMapFileNameWithExtension;

        //		public string _bitmapFileNameWithExtension;

        //		public string _httpStatusCode;

        //		public string _contentType;

        //		public string _contentLength;

        //		public string _responseHeader;

        //		public string _expiresHeader;

        //		public uint _bitmapFileOffSet;
        //	}

        //	public struct BlockList
        //	{
        //		public byte _btPort;

        //		public ushort _usNodeAddress;

        //		public List<ushort> _usBlockNumber;
        //	}

        //	public struct BlockNameInfo
        //	{
        //		public string OriginalName;

        //		public string NewName;
        //	}

        //	public struct BreakPointInfo
        //	{
        //		public uint ObjectID;

        //		public int ScreenNumber;

        //		public long StepAddress;
        //	}

        //	public struct BroadcastSettings
        //	{
        //		public ushort _usBroadcastEnableBit;

        //		public byte _btTypeNoOfRegister;

        //		public byte _btAdjustingbyte1;

        //		public ushort _usNoOfRegisterData;

        //		public byte _btModbusTagType;

        //		public byte _btAdjustingbyte2;

        //		public ushort _usModbusTagStartAddr;

        //		public ushort _usPrizmTagStartAddr;
        //	}

        //	public struct ButtonLocation
        //	{
        //		public int X;

        //		public int Y;
        //	}

        //	public struct ChildInfo
        //	{
        //		public string Name;

        //		public int level;
        //	}

        //	public struct ChildNodeInfo
        //	{
        //		public string Name;

        //		public string ParentName;

        //		public int ScreenNumber;
        //	}

        //	public struct ColorValues
        //	{
        //		public int _iNoOfSourceSupportedColor;

        //		public int _iNoOfDestSupportedColor;
        //	}

        //	[Serializable]
        //	public struct CondTaskInfo
        //	{
        //		public short sBytes4CondTask;

        //		public byte btTaskCode;

        //		public byte btAdjByte;

        //		public short sNoOfState;

        //		public byte[] btIndirectAddrOfTask;

        //		public byte btOpnVal;

        //		public short sOpdNo;

        //		public byte btReservedByte;
        //	}

        //	public struct CounterAddressListInfo
        //	{
        //		public string TagAddress;

        //		public double @value;

        //		public int OperandType;

        //		public int TagAddressCount;

        //		public uint intObjectId;

        //		public string Prefix;
        //	}

        //	public struct DataMonitorTagInfo
        //	{
        //		public string Name;

        //		public string DataType;

        //		public string Value;

        //		public int ReadOnly;
        //	}

        //	public struct DebugStepInfo
        //	{
        //		public int runNo;

        //		public int Row;

        //		public int Column;

        //		public string Operand;

        //		public int StepNo;

        //		public long startAddress;

        //		public uint ObjectID;
        //	}

        //	public struct DefTagInfo
        //	{
        //		public int Type;

        //		public string Address;

        //		public int bytes;

        //		public int bitNumber;
        //	}

        //	public struct DelTagInfo
        //	{
        //		public int TagID;

        //		public string TagName;
        //	}

        //	public struct DMComInfo
        //	{
        //		public uint address;

        //		public uint offset;
        //	}

        //	public struct DMVarInfo
        //	{
        //		public string Name;

        //		public uint address;
        //	}

        //	public struct DoubleData
        //	{
        //		public double dbValue;
        //	}

        //	public struct EditedVMDBTag
        //	{
        //		public CommonConstants.Prizm3TagStructure prizm3tag;

        //		public string _prevTagnm;

        //		public string _prevInitialval;
        //	}

        //	[Serializable]
        //	public struct ErrorInfo
        //	{
        //		public ushort usScreenNumber;

        //		public string strScreenName;

        //		public uint uiErrorSourceID;

        //		public string strErrorSourceName;

        //		public byte btErrorType;

        //		public byte btWarningType;

        //		public string strScreenNotDefined;
        //	}

        //	public struct EthernetSettings
        //	{
        //		public bool _DHCP;

        //		public decimal _DownloadPort;

        //		public string _IPAdderess;

        //		public string _SubnetMask;

        //		public string _DefaultGateway;

        //		public decimal _MonitoringPort;
        //	}

        //	public struct EthernetSettingsDwndlble
        //	{
        //		public byte _DHCP;

        //		public int _DownloadPort;

        //		public uint _IPAdderess;

        //		public uint _SubnetMask;

        //		public uint _DefaultGateway;

        //		public int _MonitoringPort;
        //	}

        //	public struct EthernetSettingsSaveFormat
        //	{
        //		public byte _DHCP;

        //		public int _DownloadPort;

        //		public uint _IPAdderess;

        //		public uint _SubnetMask;

        //		public uint _DefaultGateway;
        //	}

        //	public struct EthernetSettingsUploadFormat
        //	{
        //		public uint _IPAddress;

        //		public uint _SubnetMask;

        //		public uint _DefaultGateway;

        //		public int _DownloadPort;

        //		public byte _DHCP;
        //	}

        //	public struct ExportObjectsInfo
        //	{
        //		public bool _ExportWholeProject;

        //		public bool _ViewErrorLog;

        //		public bool _IsAscii;

        //		public string _FileName;

        //		public string _ProjectName;

        //		public List<int> _ScreenNo;

        //		public List<string> _ScreenName;
        //	}

        //	public struct ExportTagInfo
        //	{
        //		public bool _SelectAllNodeTags;

        //		public ArrayList _NodeName;

        //		public bool _SystemTags;

        //		public bool _UsedTags;

        //		public bool _Ascii;

        //		public bool _OverWrite;

        //		public string _FileName;

        //		public List<int> _TagId;

        //		public int _TagNameColumn;

        //		public int _NodeName1Column;

        //		public int _TagAddressColumn;

        //		public int _TagTypeColumn;

        //		public int _TagPrefixColumn;

        //		public int _BytesColumn;

        //		public int _NodeName2Column;

        //		public int _ProtocolColumn;

        //		public int _ModelColumn;

        //		public int _PortColumn;

        //		public int _TagPortcolumn;

        //		public bool _ViewErrorLog;

        //		public int _SlotNumberColumn;

        //		public int _NativePrefixColumn;

        //		public int _NativeAddressColumn;

        //		public int _NativeAddrValColumn;

        //		public int _StratonBlockTypeColumn;

        //		public int _StratonBlockNameColumn;

        //		public int _StratonInitialValueColumn;

        //		public int _IsRetentiveColumn;

        //		public int _Com1MappingAddressColumn;

        //		public int _Com2MappingAddressColumn;

        //		public int _Com3MappingAddressColumn;

        //		public int _StringLength;

        //		public int _DimensionColumn;

        //		public int _arrTagInfoColumn;

        //		public int _ModbusSlaveIDColumn;

        //		public bool _AllTags;

        //		public bool _UnusedTags;

        //		public bool _UserdefinedTags;

        //		public bool _UserdefUsedTags;

        //		public bool _UserdefUnusedTags;

        //		public bool _CheckedExportListTags;
        //	}

        //	public struct FindInfo
        //	{
        //		public uint ObjectID;

        //		public int RungNo;

        //		public int LineNo;

        //		public string BlockName;

        //		public string InstName;
        //	}

        //	[Serializable]
        //	public struct FontInfo
        //	{
        //		public short _fPtSzForFont;

        //		public int _fFontHeight;

        //		public int _fFontWidth;

        //		public int _fEscapement;

        //		public int _fOrientation;

        //		public int _fWeight;

        //		public byte _fItalic;

        //		public byte _fUnderline;

        //		public byte _fStrikeOut;

        //		public byte _fCharSet;

        //		public byte _fOutPrecision;

        //		public byte _fClipPrecision;

        //		public byte _fQuality;

        //		public byte _fPitchFamily;

        //		public byte _fLenOfFaceName;

        //		public byte _fFontAdjByte;
        //	}

        //	public struct ForNextInfo
        //	{
        //		public uint ObjectID;

        //		public int RungNo;
        //	}

        //	[Serializable]
        //	public struct G9SPNodeInfo
        //	{
        //		public int _iNodeId;

        //		public string _strName;

        //		public ushort _usAddress;

        //		public byte _btType;

        //		public byte _btPort;

        //		public string _strProtocol;

        //		public string _strModel;

        //		public byte _btHasTag;

        //		public string _strPortName;

        //		public byte _btPLCCode;

        //		public byte _btPLCModel;

        //		public byte _btRegLength;

        //		public byte _btSpecialData1;

        //		public byte _btSpecialData2;

        //		public byte _btSpecialData3;

        //		public ushort _usTotalBlocks;

        //		public uint _uiEthernetIpAddress;

        //		public ushort _usEthernetPortNumber;

        //		public ushort _usEthernetScanTime;

        //		public ushort _usEthernetResponseTimeOut;

        //		public byte _btBaudRate;

        //		public byte _btParity;

        //		public byte _btDataBits;

        //		public byte _btStopBits;

        //		public byte _btRetryCount;

        //		public ushort _usInterframeDelay;

        //		public ushort _usResponseTime;

        //		public byte _btInterByteDelay;

        //		public byte _btFloatFormat;

        //		public byte _btIntFormat;

        //		public byte _btExpansionType;

        //		public byte _btIntFourFormat;

        //		public byte _btFormatPort3;

        //		public string _strGSMMobileNo;

        //		public byte _btGSMMobileNoLength;

        //		public byte _btG9SPSrcNetwork;

        //		public byte _btG9SPSrcNode;

        //		public byte _btG9SPSrcID;

        //		public byte _btG9SPDestNetwork;

        //		public byte _btG9SPDestNode;

        //		public byte _btG9SPDestID;

        //		public byte _btReserved;

        //		public byte _dwnldSerialParams;

        //		public byte _btReconnectCntrl;

        //		public int _ReconnectTag;
        //	}

        //	public struct GlobalAlarmProperties
        //	{
        //		public byte AlmActIfBufferFull;

        //		public byte AutoAckAlm;

        //		public string AlmErrText;

        //		public int AlmHistBufferSize;

        //		public bool LogAlmText;

        //		public int AlmScanTime;

        //		public byte AlmType;
        //	}

        //	[Serializable]
        //	public struct GSMSettingsSaveFormat
        //	{
        //		public byte _btBaudRate;

        //		public byte _btParity;

        //		public byte _btDataBits;

        //		public byte _btStopBits;

        //		public byte _btRetryCount;

        //		public ushort _usInterframeDelay;

        //		public ushort _usResponseTime;

        //		public byte _btInterByteDelay;

        //		public byte _mobileNoLength;

        //		public byte[] _mobileNumber;
        //	}

        //	public struct GwyBlockInfo
        //	{
        //		public int iCountWords;

        //		public int iCountRepCycle;

        //		public int iComPort_Source;

        //		public int iPlc_Code_Source;

        //		public int iPlc_Model_Source;

        //		public int iNodeAddress_Source;

        //		public string strPrefix_Source;

        //		public string strSuffix_Source;

        //		public string strAddress_Source;

        //		public int iComPort_Dest;

        //		public int iPlc_Code_Dest;

        //		public int iPlc_Model_Dest;

        //		public int iNodeAddress_Dest;

        //		public string strPrefix_Dest;

        //		public string strSuffix_Dest;

        //		public string strAddress_Dest;

        //		public string strComment;
        //	}

        //	public struct GwyContolWordInfo
        //	{
        //		public int iComPort;

        //		public int iPlc_Code;

        //		public int iNodeAddress;

        //		public string strAddress;

        //		public string strPrefix;

        //		public string strSuffix;
        //	}

        //	public struct GwyDnld_MultiNodeInfo
        //	{
        //		public int iSrcCom1Bytes;

        //		public byte bSrcCom1BlkCount;

        //		public byte[] btArr1SrcCom1;

        //		public byte[] btArr2SrcCom1;

        //		public int iSrcCom2Bytes;

        //		public byte bSrcCom2BlkCount;

        //		public byte[] btArr1SrcCom2;

        //		public byte[] btArr2SrcCom2;
        //	}

        //	public struct GwyErrorBitInfo
        //	{
        //		public int iComPort_Source;

        //		public int iPlc_Code_Source;

        //		public int iNodeAddress_Source;

        //		public string strAddress_Source;

        //		public string strPrefix_Source;

        //		public string strSuffix_Source;

        //		public int iComPort_Dest;

        //		public int iPlc_Code_Dest;

        //		public int iNodeAddress_Dest;

        //		public string strAddress_Dest;

        //		public string strPrefix_Dest;

        //		public string strSuffix_Dest;
        //	}

        //	public struct GwyPLCInfo
        //	{
        //		public string strName_PlcSource;

        //		public string strName_ModelSource;

        //		public int RegLen_Source;

        //		public int Plc_Code_Source;

        //		public int ModelNo_Source;

        //		public string strName_PlcDest;

        //		public string strName_ModelDest;

        //		public int RegLen_Dest;

        //		public int Plc_Code_Dest;

        //		public int ModelNo_Dest;
        //	}

        //	public struct GwyPLCPortInfo
        //	{
        //		public int iPort;

        //		public int iNodeAddress;

        //		public int iPlc_Code;

        //		public int iPlc_Model;

        //		public int iRegLen;

        //		public int strProtocolName;
        //	}

        //	public struct GwyRegCoilInfo
        //	{
        //		public string strName;

        //		public string strPrefix;

        //		public string strSuffix;

        //		public string strType;

        //		public string strRWType;

        //		public string strBlockSize;

        //		public int NoParts;

        //		public int Len_Part1;

        //		public string strDataType_Part1;

        //		public string strLoLimit_Part1;

        //		public string strHiLimit_Part1;

        //		public int Len_Part2;

        //		public string strDataType_Part2;

        //		public string strLoLimit_Part2;

        //		public string strHiLimit_Part2;
        //	}

        //	public struct HTMLFileStructure
        //	{
        //		public short _lengthOfScreenNameWithExtension;

        //		public string _screenNameWithExtension;

        //		public string _httpStatusCode;

        //		public string _contentType;

        //		public string _contentLength;

        //		public string _responseHeader;

        //		public string _expiresHeader;

        //		public uint _screenFileOffSet;
        //	}

        //	public struct ImportNodeData
        //	{
        //		public string _NodeName;

        //		public string _Protocol;

        //		public string _Model;

        //		public string _Port;

        //		public int _NodeError;

        //		public int _slaveID;
        //	}

        //	public struct ImportObj
        //	{
        //		public string _ScreenName;

        //		public string ShapeType;

        //		public string[][] _LanguageText;

        //		public uint _ObjId;
        //	}

        //	public struct ImportObjectsInfo
        //	{
        //		public bool _IsAscii;

        //		public bool _ViewErrorLog;

        //		public string _FileName;

        //		public string _ProjectName;
        //	}

        //	public enum ImportScreenErrSrcType
        //	{
        //		OBJECT,
        //		OBJECT_TASK,
        //		SCREEN_TASK,
        //		SCREEN_KEY_TASK
        //	}

        //	public enum ImportScreenWarnings
        //	{
        //		NONE,
        //		OBJECT_REMOVED,
        //		DATALOGGER_ABSENT_TREND,
        //		TAG_NODE_NOT_SUPPORTED,
        //		TAG_NODE_NOT_ADDED,
        //		TAG_NODE_ADDRESS_PRESENT,
        //		TAG_NOT_SUPPORTED,
        //		TAG_NOT_IMPORTED
        //	}

        //	public struct ImportTagData
        //	{
        //		public string _TagName;

        //		public string _TagType;

        //		public string _TagPrefix;

        //		public string _TagAddress;

        //		public string _TagPort;

        //		public string _TagNodeName;

        //		public int _TagBytes;

        //		public byte _LowHigh;

        //		public int _TagError;

        //		public string _stratonDataType;

        //		public string _NativePrefix;

        //		public string _NativeAddr;

        //		public string _NativeAddrVal;

        //		public byte _SlotNo;

        //		public byte _StratonBlockType;

        //		public string _StratonBlockName;

        //		public string _StratonInitialValue;

        //		public byte _IsRetentive;

        //		public string _Com1MappingAddress;

        //		public string _Com2MappingAddress;

        //		public string _Com3MappingAddress;

        //		public string _StringLength;

        //		public string _Dimension;

        //		public string _ArrTagInfo;
        //	}

        //	public struct ImportTagInfo
        //	{
        //		public bool _CreateDuplicates;

        //		public bool _AutogenerateTagName;

        //		public bool _ViewLogFile;

        //		public int _TagNameColumn;

        //		public int _NodeName1Column;

        //		public int _TagAddressColumn;

        //		public int _TagTypeColumn;

        //		public int _TagPrefixColumn;

        //		public int _BytesColumn;

        //		public int _NodeName2Column;

        //		public int _ProtocolColumn;

        //		public int _ModelColumn;

        //		public int _PortColumn;

        //		public int _TagPortcolumn;

        //		public string _ImportFileName;

        //		public bool _IsAscii;

        //		public int _SlotNumberColumn;

        //		public int _NativePrefixColumn;

        //		public int _NativeAddressColumn;

        //		public int _NativeAddrValColumn;

        //		public int _StratonBlockTypeColumn;

        //		public int _StratonBlockNameColumn;

        //		public int _StratonInitialValueColumn;

        //		public int _IsRetentiveColumn;

        //		public int _Com1MappingAddressColumn;

        //		public int _Com2MappingAddressColumn;

        //		public int _Com3MappingAddressColumn;

        //		public int _StringLength;

        //		public int _dimensionColumn;

        //		public int _arrTagInfoColumn;

        //		public int _modbusSlaveIDColumn;
        //	}

        //	public struct ImportTaskInfo
        //	{
        //		public bool bImportPWOnTasks;

        //		public bool bImportGTasks;

        //		public bool bImportScrTasks;

        //		public int CountPowerOnTasks;

        //		public int CountGlobalTasks;

        //		public bool bImportScrBeforeSHTasks;

        //		public bool bImportScrWhileSHTasks;

        //		public bool bImportScrAfterHTasks;

        //		public string strProjectFilePath;

        //		public string strSourceScreen;

        //		public string strDestScreen;

        //		public int taskAddedPOn;

        //		public int taskAddedG;

        //		public int taskAddedScrBSh;

        //		public int taskAddedScrWSh;

        //		public int taskAddedScrAh;

        //		public int numberSourceScreen;

        //		public int CountScreenTasksBSH;

        //		public int CountScreenTasksWSH;

        //		public int CountScreenTasksAH;

        //		public bool errorPLCTasks;
        //	}

        //	[Serializable]
        //	public struct ImportWarningInfo
        //	{
        //		public string ScreenNumber;

        //		public string ScreenName;

        //		public CommonConstants.ImportScreenErrSrcType ErrorSourceType;

        //		public string ShapeName;

        //		public string ShapeObjID;

        //		public string ObjProperty;

        //		public string TaskName;

        //		public string KeyNumber;

        //		public string KeyTaskType;

        //		public string warningDescp;
        //	}

        //	public struct InstanceInfo
        //	{
        //		public string strName;

        //		public string strGroup;

        //		public string strUDFBname;
        //	}

        //	public struct JSFileStructure
        //	{
        //		public short _lengthOfJSFileWithExtension;

        //		public string _jsFileNameWithExtension;

        //		public string _httpStatusCode;

        //		public string _contentType;

        //		public string _contentLength;

        //		public string _responseHeader;

        //		public string _expiresHeader;

        //		public uint _jsFileOffSet;
        //	}

        //	public struct KeysInformation
        //	{
        //		public string _KeyName;

        //		public List<string> _PressTasks;

        //		public List<string> _PressedTasks;

        //		public List<string> _ReleaseTasks;
        //	}

        //	public struct KeyValues
        //	{
        //		public int _iNoOfSourceKeys;

        //		public int _iNoOfDestKeys;

        //		public int _iSelectedKeyNo;
        //	}

        //	public struct LadderAddressListInfo
        //	{
        //		public int intOperandType;

        //		public int intOperandNumber;

        //		public string strOperandText;

        //		public int intObjectID;

        //		public string Instruction_Name;

        //		public InstructionType InstType;

        //		public System.Drawing.Rectangle objRect;

        //		public int intShapeID;

        //		public int intBytes;
        //	}

        //	public struct LadderBlockListInfo
        //	{
        //		public ushort ScreenNumber;

        //		public int BlockType;

        //		public string BlockName;

        //		public int NumberOfBlocks;
        //	}

        //	public struct LadderBlockPrintInfo
        //	{
        //		public int ScreenNumber;

        //		public string BlockName;

        //		public int PrintWidth;

        //		public int PrintHeight;

        //		public int IECBlockType;

        //		public bool bQualifier;

        //		public string strQualifier;

        //		public string strCode;

        //		public int Lang;
        //	}

        //	public struct LadderCompilationRungInfo
        //	{
        //		public int InstructuonType;

        //		public int VLinkVariable;

        //		public string Operand;

        //		public int FW_Link;

        //		public int BK_Link;

        //		public int FW_End_Position;

        //		public int BK_End_Position;

        //		public int Leading_Trailing_Count;

        //		public int AddressTypeRegister;

        //		public int Constant;

        //		public int SourceTagIndex;

        //		public int DestinationTagIndex;

        //		public int ThirdOprandTagIndex;

        //		public int TableSize;

        //		public int Datatype;

        //		public int ByteFormat;

        //		public string Constant_String;

        //		public int ByteOrderValue;
        //	}

        //	public struct LadderInstructionListInfo
        //	{
        //		public int intBytes;

        //		public string Instruction_Name;

        //		public InstructionType InstType;
        //	}

        //	public struct LadderMonitorTagInfo
        //	{
        //		public string TagAddress;

        //		public double @value;

        //		public int OperandType;

        //		public int DataType;

        //		public int DataSize;

        //		public InstructionType InstType;
        //	}

        //	public struct LadderOperandInfo
        //	{
        //		public int intFieldType;

        //		public int intOperandType;

        //		public int intOperandNumber;

        //		public string strOperandText;

        //		public string strOperandName;

        //		public System.Drawing.Rectangle objOperandTextRect;

        //		public int intTxtFontSize;

        //		public int intObjectID;

        //		public InstructionType InstType;

        //		public int intShapeID;

        //		public int intBytes;

        //		public int intDataType;
        //	}

        //	[Serializable]
        //	public struct LadderSaveBlockInfo
        //	{
        //		public int BlockType;

        //		public int TotalRungs;

        //		public int TotalColumns;

        //		public int Commenttextcolor;

        //		public int CommentBackcolor;

        //		public int Rung1Backcolor;

        //		public int Rung2Backcolor;
        //	}

        //	[Serializable]
        //	public struct LadderSaveFileHeaderInfo
        //	{
        //		public int Version;

        //		public int TotalLadderBlocks;
        //	}

        //	[Serializable]
        //	public struct LadderSaveRungInfo
        //	{
        //		public bool ShowCommentArea;

        //		public int NoRungCells;

        //		public int HeightCommentArea;
        //	}

        //	[Serializable]
        //	public struct LadderScreenInfo
        //	{
        //		public RungManager RungManager;

        //		public string LadderBlockName;

        //		public int LadderBlockTypeIndex;

        //		public int LadderCommentTextColor;

        //		public int LadderCommentBackColor;

        //		public System.Drawing.Font LadderCommentFont;

        //		public int BlockType;
        //	}

        //	[Serializable]
        //	public struct LadderSettingsInfo
        //	{
        //		public bool ShowRegisterEntryMessage;

        //		public int ColorContactOnState;

        //		public int ColorContactOffState;

        //		public int ColorFunctionBlock;

        //		public int ColorOperandVale;

        //		public int ColorLadderBackGroundArea;

        //		public int ColorLeftMarginRung1;

        //		public int ColorLeftMarginRung2;

        //		public int ColorActiveRung;

        //		public int TagNameLen_Display;

        //		public bool b_chkhaltmode_dnld;

        //		public bool b_chkrunmode_dnld;

        //		public bool b_chkcleanmemory_dnld;

        //		public bool b_chkPLCMemory_dnld;

        //		public bool b_chkApplication_dnld;

        //		public bool b_chkLadder_dnld;

        //		public bool b_chkData_dnld;

        //		public int Comm_Mode;

        //		public int Comm_Type;

        //		public int Com_Port;

        //		public int Color_BOOL;

        //		public int Color_BYTE;

        //		public int Color_WORD;

        //		public int Color_DWORD;

        //		public int Color_INT;

        //		public int Color_SINT;

        //		public int Color_DINT;

        //		public int Color_USINT;

        //		public int Color_UDINT;

        //		public int Color_REAL;

        //		public int Color_UINT;

        //		public int Color_TIME;

        //		public bool b_ForceIO;

        //		public bool b_ShowNewInst_DefaultTagSel;
        //	}

        //	public struct LadderTagAddressInfo
        //	{
        //		public int intFieldType;

        //		public int intOperandType;

        //		public int intOperandNumber;

        //		public string strOperandText;

        //		public System.Drawing.Rectangle objOperandTextRect;

        //		public int intTxtFontSize;

        //		public int intTableSize;

        //		public int intObjectID;

        //		public InstructionType InstType;

        //		public int intShapeID;

        //		public int intBytes;
        //	}

        //	public struct LadderTagInfo
        //	{
        //		public byte Type;

        //		public string Prefix;

        //		public byte ReadWrite;

        //		public byte Bytes;

        //		public int MinLimit;

        //		public int MaxLimit;

        //		public int DataType;
        //	}

        //	public struct LadderUploadInfo
        //	{
        //		public int InstrType;

        //		public int VerticalLink;

        //		public string strOprand1;

        //		public string strOprand2;

        //		public string strOprand3;

        //		public string strOprand4;

        //		public int AddressTypeOprand1;

        //		public int AddressTypeOprand2;

        //		public int AddressTypeOprand3;

        //		public long Constant1;

        //		public long Constant2;

        //		public long Constant3;

        //		public float floatConstant1;

        //		public float floatConstant2;

        //		public float floatConstant3;

        //		public string Constant_String;

        //		public int IndexType1;

        //		public int IndexType2;

        //		public int IndexType3;

        //		public int DataType;

        //		public int DataSize;

        //		public int TableSize;

        //		public int intRow;

        //		public int intColumn;

        //		public int ByteOrderValue;
        //	}

        //	public struct LanguageInformation
        //	{
        //		public string LanguageName;

        //		public int LanguageId;

        //		public bool KeyboardLayout;
        //	}

        //	public struct level2TypeInfo
        //	{
        //		public int Type;

        //		public System.Drawing.Rectangle rectInfo;
        //	}

        //	[Serializable]
        //	public class LOGFONT
        //	{
        //		public const int LF_FACESIZE = 32;

        //		public int lfHeight;

        //		public int lfWidth;

        //		public int lfEscapement;

        //		public int lfOrientation;

        //		public int lfWeight;

        //		public byte lfItalic;

        //		public byte lfUnderline;

        //		public byte lfStrikeOut;

        //		public byte lfCharSet;

        //		public byte lfOutPrecision;

        //		public byte lfClipPrecision;

        //		public byte lfQuality;

        //		public byte lfPitchAndFamily;

        //		public string lfFaceName;

        //		public LOGFONT()
        //		{
        //		}
        //	}

        //	[Serializable]
        //	public struct LoggedDataCSVFormat
        //	{
        //		public int _GroupNumber;

        //		public StringBuilder _LoggedData;

        //		public LoggedDataCSVFormat(int GroupNumber)
        //		{
        //			this._GroupNumber = GroupNumber;
        //			this._LoggedData = new StringBuilder();
        //		}
        //	}

        //	public struct LoggerGroupInfo
        //	{
        //		public byte LoggerMode;

        //		public byte LoggerFrequencyHour;

        //		public byte LoggerFrequencyMinute;

        //		public byte LoggerFrequencySecond;

        //		public byte Reserved1;

        //		public byte LoggerStartTimeHour;

        //		public byte LoggerStartTimeMinute;

        //		public byte LoggerStartTimeSecond;

        //		public byte LoggerStopTimeHour;

        //		public byte LoggerStopTimeMinute;

        //		public byte LoggerStopTimeSecond;

        //		public byte DataType;

        //		public byte LoggedTags;

        //		public byte Reserved3;

        //		public byte LoggingMode;

        //		public byte FileSendAtEveryHour;

        //		public byte FileSendAtEveryMinute;

        //		public byte FileSendAtEverySecond;

        //		public string LoggingFileName;
        //	}

        //	public struct LoggerGroupInfoExternal
        //	{
        //		public byte LoggerMode;

        //		public byte LoggerFrequencyHour;

        //		public byte LoggerFrequencyMinute;

        //		public byte LoggerFrequencySecond;

        //		public byte Reserved1;

        //		public byte LoggerStartTimeHour;

        //		public byte LoggerStartTimeMinute;

        //		public byte LoggerStartTimeSecond;

        //		public byte LoggerStopTimeHour;

        //		public byte LoggerStopTimeMinute;

        //		public byte LoggerStopTimeSecond;

        //		public byte DataType;

        //		public byte LoggedTags;

        //		public byte Reserved3;

        //		public byte LoggingMode;

        //		public string LoggingFileName;
        //	}

        //	[Serializable]
        //	public struct MemoryStatus
        //	{
        //		public int _totalNodes;

        //		public int _totalScreens;

        //		public int _totalKeys;

        //		public int _totalAlarms;

        //		public int _totalPowerOntasks;

        //		public int _totalGlobaltasks;

        //		public int _totalLogger;

        //		public int _totalBlockToBeRead;

        //		public int _totalTagName;

        //		public int _totalOther;

        //		public int _nodeBytes;

        //		public int _screenBytes;

        //		public int _keyBytes;

        //		public int _alarmBytes;

        //		public int _powerOntaskBytes;

        //		public int _globaltaskBytes;

        //		public int _loggerBytes;

        //		public int _blockToBeReadBytes;

        //		public int _tagNameBytes;

        //		public int _otherBytes;

        //		public int _historicalAlarmBytes;

        //		public float _availableMemory;

        //		public int _usedMemory;

        //		public int _allotedloggerDataMemory;

        //		public float _availableLadderMemory;

        //		public float _usedLadderMemory;

        //		public uint _ethernetsettings;
        //	}

        //	public struct memStatusVarInfo
        //	{
        //		public string DataType;

        //		public int Count;

        //		public int memUsed;
        //	}

        //	public struct MITQSettings
        //	{
        //		public byte Port;

        //		public byte NodeAddress;

        //		public byte NetworkNumber;

        //		public byte PCNumber;

        //		public ushort DestModuleIONo;

        //		public byte DestModuleStNo;
        //	}

        //	public struct ModbusComData
        //	{
        //		public int _noOfBytesCom;

        //		public byte _comPort;

        //		public bool _IsNodePresent;

        //		public int _totalNoofHR;

        //		public int _totalNoofHRBlocks;

        //		public int _totalNoOfCoils;

        //		public int _totalNoOfCoilBlocks;
        //	}

        //	public struct ModelDataInfo
        //	{
        //		public int iPrizmId;

        //		public int iHIOId;

        //		public string strModelSeries;

        //		public string strModel;

        //		public string strDigitalInputs;

        //		public string strAnalogInputs;

        //		public string strDigitalOutputs;

        //		public string strAnalogOutputs;

        //		public string strDigitalOutputType;

        //		public string strAnalogInputType;

        //		public string strAnalogOutputType;

        //		public string strAnalogIOConfiguration;

        //		public string strNote;

        //		public int iModelNo;

        //		public bool blCOM1;

        //		public bool blCOM2;

        //		public bool blEthernet;

        //		public bool blUSB;

        //		public bool blExpansionPort;

        //		public int iConversionID;

        //		public int iVersionID;

        //		public bool blTouchGrid;

        //		public bool blGridConfiguration;

        //		public bool blOverlappingAllowed;

        //		public int iMemory;

        //		public int iProductTypeID;

        //		public int iNoOfCharactersToPrint;

        //		public int iScrColumns;

        //		public bool blScrColumnsReadOnlyFlag;

        //		public int iBootId;

        //		public int iApplicationmemory;

        //		public int iHistoricalAlarmsMemory;
        //	}

        //	[Serializable]
        //	public struct NodeInfo
        //	{
        //		public int _iNodeId;

        //		public string _strName;

        //		public ushort _usAddress;

        //		public byte _btType;

        //		public byte _btPort;

        //		public string _strProtocol;

        //		public string _strModel;

        //		public byte _btHasTag;

        //		public string _strPortName;

        //		public byte _btPLCCode;

        //		public byte _btPLCModel;

        //		public byte _btRegLength;

        //		public byte _btSpecialData1;

        //		public byte _btSpecialData2;

        //		public byte _btSpecialData3;

        //		public ushort _usTotalBlocks;

        //		public uint _uiEthernetIpAddress;

        //		public ushort _usEthernetPortNumber;

        //		public ushort _usEthernetScanTime;

        //		public ushort _usEthernetResponseTimeOut;

        //		public byte _btBaudRate;

        //		public byte _btParity;

        //		public byte _btDataBits;

        //		public byte _btStopBits;

        //		public byte _btRetryCount;

        //		public ushort _usInterframeDelay;

        //		public ushort _usResponseTime;

        //		public byte _btInterByteDelay;

        //		public byte _btFloatFormat;

        //		public byte _btIntFormat;

        //		public byte _btExpansionType;

        //		public byte _btIntFourFormat;

        //		public byte _btFormatPort3;

        //		public string _strGSMMobileNo;

        //		public byte _btGSMMobileNoLength;

        //		public byte _modbusSlaveID;

        //		public byte _dwnldSerialParams;

        //		public byte _btReconnectCntrl;

        //		public int _ReconnectTag;

        //		public byte _btFloatFormatfor8byte;
        //	}

        //	public struct NodeInformation
        //	{
        //		public string _NodeAddr;

        //		public string _NodeName;

        //		public string _Port;

        //		public string _Protocol;
        //	}

        //	public struct OffMonitorData
        //	{
        //		public string _tagName;

        //		public string _blockName;

        //		public uint _loopID;

        //		public int _blockType;
        //	}

        //	public struct PLCGeneralInfo
        //	{
        //		public string _Prefix;

        //		public string _Suffix;
        //	}

        //	public struct PlcModuleHeaderInfo
        //	{
        //		public int intNoModules;

        //		public int BaseModuleType;

        //		public int intTotalBytesForModuleInfo;

        //		public int intRAM_OffsetXW;

        //		public int intRAM_OffsetYW;

        //		public int intRAM_OffsetMW;

        //		public int intNumRegisterXW;

        //		public int intNumRegisterYW;

        //		public int intNumRegisterMW;
        //	}

        //	public struct PlcModuleInfo
        //	{
        //		public int intModuleNO;

        //		public int intBytesForModule;

        //		public int intModuleAddress;

        //		public int intModuleFirmwareRevision;

        //		public int intModuleType;

        //		public int intRAM_OffsetXW;

        //		public int intRAM_OffsetYW;

        //		public int intRAM_OffsetMW;

        //		public int intNumberOfXCOils;

        //		public int intNumberOfYCOils;

        //		public int intNumRegisterXW;

        //		public int intNumRegisterYW;

        //		public int intNumRegisterMW;

        //		public int intNumberOfMCoils;
        //	}

        //	public struct PLCRangeInfo
        //	{
        //		public string _Prefix;

        //		public string _Suffix;

        //		public string _RegMinRange;

        //		public string _RegMaxRange;

        //		public string _RegMinRange2;

        //		public string _RegMaxRange2;

        //		public int _RegNo;

        //		public int NoOfParts;

        //		public string _DataTypePart1;

        //		public string _DataTypePart2;

        //		public int _LengthPart1;

        //		public int _LengthPart2;

        //		public ushort _BlockSize;
        //	}

        //	public struct PortValues
        //	{
        //		public int _iNoOfSourcePorts;

        //		public int _iNoOfDestPorts;

        //		public bool _blSetDefaultParameters;

        //		public bool _blCopyFromSource;

        //		public List<Port> _lstSourcePorts;

        //		public List<Port> _lstDestPorts;

        //		public List<PortActions> _lstPortActions;
        //	}

        //	public struct PrinterPortSettings
        //	{
        //		public byte _BoudRate;

        //		public byte _NoofDataBits;

        //		public byte _Parity;

        //		public byte _NoofColumns;

        //		public byte _TerminatingCharacter;
        //	}

        //	[Serializable]
        //	public struct PrintPropertiesInfo
        //	{
        //		public byte PrintingStatus;

        //		public ushort GroupBytes;

        //		public byte TagTypeForPageLines;

        //		public int TagIdForPageLines;

        //		public byte PageLines;

        //		public byte PaperSize;

        //		public ushort LeftMargin;

        //		public ushort RightMargin;

        //		public ushort TopMargin;

        //		public ushort BottomMargin;

        //		public byte HeaderDateDisplay;

        //		public byte TimeColumnDisplay;

        //		public byte TimeColumnWidth;

        //		public byte FooterDateDisplay;

        //		public int NoofTagstobePrinted;

        //		public string HeaderLine1;

        //		public string HeaderLine2;

        //		public string HeaderLine3;

        //		public string HeaderLine4;

        //		public string FooterLine1;

        //		public string FooterLine2;

        //		public string FooterLine3;

        //		public string FooterLine4;

        //		public string PowerFailure;

        //		public string CommBreak;

        //		public string PowerUp;
        //	}

        //	[Serializable]
        //	public struct PrintTagsInformation
        //	{
        //		public string HeaderName;

        //		public ushort ColumnWidth;

        //		public byte Format;

        //		public byte DecimalPointLocation;

        //		public byte DataType;

        //		public byte GroupofTag;

        //		public byte TagIndex;

        //		public int TagId;
        //	}

        //	public struct Prizm3BlockStructure
        //	{
        //		public byte _BlockByteSize;

        //		public int _BlockLengthOfStartingAddress;

        //		public string _BlockStartingAddress;

        //		public byte _BlockBlockSize;

        //		public int _BlockNoOfTags;

        //		public byte _BlockBlockTypes;

        //		public string _BlockPrefix;

        //		public string _BlockSuffix;
        //	}

        //	public struct Prizm3TagStructure
        //	{
        //		public ushort _TagSize;

        //		public byte _TagBy;

        //		public byte _TagType;

        //		public byte _ReadWrite;

        //		public byte _BitNumber;

        //		public byte _NoOfBytes;

        //		public byte _LowHigh;

        //		public string _TagAddress;

        //		public string _TagName;

        //		public string _NodeName;

        //		public int _RegMinRange;

        //		public int _RegMinRange2;

        //		public int _RegMaxRange;

        //		public int _RegMaxRange2;

        //		public int _TagValue;

        //		public int _TagValue2;

        //		public bool _IsTagUsed;

        //		public bool _IsTagSystem;

        //		public bool _IsSpecialRangePLCTag;

        //		public bool _IsSpecialCharPresentInTagAddr;

        //		public int _NodeID;

        //		public int _ComID;

        //		public int _PLCCode;

        //		public int _TagTagID;

        //		public string _strPrefix;

        //		public string _strSuffix;

        //		public string _blockStartAdderess;

        //		public int _blockNumber;

        //		public ushort _blockSize;

        //		public int _TagNumber;

        //		public int _RegNo;

        //		public int _NoofParts;

        //		public string _DataTypePart1;

        //		public string _DataTypePart2;

        //		public int _LengthPart1;

        //		public int _LengthPart2;

        //		public string _StratonDataType;

        //		public byte _StratonBlockType;

        //		public string _StratonInitialValue;

        //		public int _StratonDataTypeStringLength;

        //		public byte _IsExpansionTag;

        //		public byte _IsRetentiveRegister;

        //		public string _StratonGroupName;

        //		public byte _slotNo;

        //		public string _nativePrefix;

        //		public string _nativeAddrVal;

        //		public string _nativeAddress;

        //		public byte IsLocalIOTag;

        //		public bool _IsNodeStatusTag;

        //		public byte _StatusNodePort;

        //		public ushort _StatusNodeAddr;

        //		public string _Dimension;

        //		public string _ArrTagInfo;

        //		public string _StructureName;

        //		public string _StructureObjName;

        //		public bool _ForExport;

        //		public int _TagGroup;
        //	}

        //	public struct PrizmUnitInformation
        //	{
        //		public string _BaudRate;

        //		public string _Parity;

        //		public string _NoOfBytes;

        //		public string _NoOfcolumns;

        //		public string _TerminatingCharacter;
        //	}

        //	public struct ProductConversionDestinationAppParameters
        //	{
        //		public float ResolutionX;

        //		public float ResolutionY;

        //		public int ModelNo;

        //		public string Name;

        //		public bool PortCom1;

        //		public bool PortCom2;

        //		public bool PortEth;

        //		public int SlotCount;

        //		public CommonConstants.ProductData objDestProductData;
        //	}

        //	public struct ProductConversionParameters
        //	{
        //		public CommonConstants.ProductConversionDestinationAppParameters destination;

        //		public CommonConstants.ProductConversionSourceAppParameters source;

        //		public CommonConstants.ProductConversionPreferences preferences;
        //	}

        //	public struct ProductConversionPreferences
        //	{
        //		public bool scalingOfObjects;
        //	}

        //	public struct ProductConversionSourceAppParameters
        //	{
        //		public float ResolutionX;

        //		public float ResolutionY;

        //		public int ModelNo;

        //		public string Name;

        //		public bool PortCom1;

        //		public bool PortCom2;

        //		public bool PortEth;

        //		public int ProjectID;

        //		public int SlotCount;
        //	}

        //	public struct ProductData
        //	{
        //		public int DataColorSupported;

        //		public bool PatternSupported;

        //		public bool IsColorReverse;

        //		public int ScreenWidth;

        //		public int ScreenHeight;

        //		public byte[,] ColorArray;

        //		public string[] ScreenObjects;

        //		public byte UnitType;

        //		public bool BlSnapToGrid;

        //		public System.Drawing.Size SzAlphanumericGridSize;

        //		public System.Drawing.Size SzTouchGridSize;

        //		public bool ShowAlphanumericGrid;

        //		public bool ShowTouchGrid;

        //		public bool IsOverlapAllowed;

        //		public int iProductID;

        //		public int iPopUpScreenWidth;

        //		public int iPopUpScreenHeight;

        //		public byte btPrizmVersion;

        //		public byte Orientation;

        //		public ushort NoOfCharactersToPrint;

        //		public ushort ScreenColumns;

        //		public bool ScrColumnsReadOnlyFlag;

        //		public uint _accesslevlscr;
        //	}

        //	public struct PropertyGridAddTagData
        //	{
        //		public string _strNodeName;

        //		public int _iNodeId;

        //		public string _strRegCoilType;

        //		public string _strTagAddress;

        //		public string _strTagName;
        //	}

        //	public struct PropertyGridNodeInfo
        //	{
        //		public string _strNodeName;

        //		public int _iNodeId;
        //	}

        //	public struct PropertyGridRegCoilType
        //	{
        //		public int _iNodeId;

        //		public List<string> _lstStrRegCoilType;

        //		public List<string> _lstStrPrefix;

        //		public List<int> _lstIntMinRange;

        //		public List<int> _lstIntMaxRange;

        //		public List<string> _lstStrPrefixRange;

        //		public List<string> _lstStrBitRegType;
        //	}

        //	[Serializable]
        //	public struct Range
        //	{
        //		public uint iLowLimit;

        //		public uint iHighLimit;
        //	}

        //	[Serializable]
        //	public struct ReadHeader
        //	{
        //		public string IsAlarmAssign;

        //		public string AlarmNumber;

        //		public string AlarmName;

        //		public string BitNumber;

        //		public string[] AlarmText;

        //		public string TagName;

        //		public string AlarmCondition;

        //		public string TagAddress;

        //		public string AlarmAttribute;

        //		public string ConditionalOperator;

        //		public string Severity;

        //		public string History;

        //		public string AcknowledgeTag;

        //		public string Print;

        //		public string Language1;

        //		public string Language2;
        //	}

        //	[Serializable]
        //	public struct ReadImportAlarmData
        //	{
        //		public byte IsAlarmAssign;

        //		public uint AlarmID;

        //		public string AlarmName;

        //		public byte BitNumber;

        //		public byte NoOfLanguages;

        //		public byte[] LanguageIndex;

        //		public string[] AlarmText;

        //		public string TagName;

        //		public int TagId;

        //		public byte ConditionAlarmFlag;

        //		public byte GroupNumber;

        //		public byte AlarmAttribute;

        //		public string AlarmActions;

        //		public byte Severity;

        //		public byte AutoAck;

        //		public string AutoAckTag;

        //		public int AutoAckTagId;

        //		public byte History;

        //		public byte Print;

        //		public byte[] DirectAddressAutoAckTag;

        //		public byte[] IndirectAddressAutoAckTag;

        //		public int AutoAckTagValue;

        //		public byte ConditionalOperator;

        //		public byte CompareWith;

        //		public ushort CondConstantValue;

        //		public byte[] DirectAddressTag;

        //		public byte[] IndirectAddressTag;
        //	}

        //	public struct ResolutionValues
        //	{
        //		public bool _blHeightKeepSameSize;

        //		public bool _blWidthKeepSameSize;

        //		public bool _blHeightDelOutsideObject;

        //		public bool _blWidthDelOutsideObject;

        //		public bool _blHeightScaleObjSize;

        //		public bool _blWidthScaleObjSize;

        //		public bool _blHeightScaleTextSize;

        //		public bool _blWidthScaleTextSize;
        //	}

        //	[Serializable]
        //	public struct ScreenInfo
        //	{
        //		public ushort usScrNumber;

        //		public string strScrName;

        //		public short sPassword;

        //		public byte btScrType;

        //		public byte btScrProperties;

        //		public string strDescription;

        //		public byte btBGColor;

        //		public short sTopLeftX;

        //		public short sTopLeftY;

        //		public ushort sHeight;

        //		public ushort sWidth;

        //		public byte btAssocitedScrNumber;

        //		public byte btNewlyCreatedScreen;

        //		public bool blTaskAssociate;

        //		public byte btBookmarks;

        //		public bool blDataEntryObjectPresent;

        //		public List<int> lstAssociatedScreenList;

        //		public ushort sScreenPrintColumns;

        //		public ushort sCharactersToPrint;

        //		public bool useTemplate;

        //		public ushort NoofTemplates;

        //		public ushort NoofLocalKeys;

        //		public byte btWaitForPLC;

        //		public string[] TemplatesList;

        //		public uint accessLevelScr;
        //	}

        //	public struct ScreenInformation
        //	{
        //		public string _ScreenNumber;

        //		public string _ScreenName;
        //	}

        //	public struct ScreenLadderTaskInfo
        //	{
        //		public int ScreenNo;

        //		public string ScreenName;

        //		public string BlockName;

        //		public bool IsUsedInBeforeShowingTasks;

        //		public bool IsUsedInWhileShowingTasks;

        //		public bool IsUsedInAfterHidingTasks;
        //	}

        //	public struct ScreenList
        //	{
        //		public int _screenNumber;

        //		public string _screenName;
        //	}

        //	public struct ScreenMemoryStatus
        //	{
        //		public int _noOfObject;

        //		public byte _noofDataEntries;

        //		public int _Blocks_to_be_Read;

        //		public short _ScreenKeysCount;

        //		public byte _BeforeShowingTaskCount;

        //		public short _WhileShowingTaskCount;

        //		public short _AfterHidingTaskCount;

        //		public int _ObjectsSize;

        //		public short _BlockSize;

        //		public int _ScreenKeysSize;

        //		public short _BeforeShowingTaskSize;

        //		public short _WhileShowingTaskSize;

        //		public short _AfterHidingTaskSize;

        //		public int _ScreenBytes;
        //	}

        //	public struct ScreenSaverSettings
        //	{
        //		public string strUserName;

        //		public string strPassword;
        //	}

        //	public struct ScreenTagIDList
        //	{
        //		public int screenNumber;

        //		public int TagID;
        //	}

        //	public struct ScreenTaskUsage
        //	{
        //		public ClassIdentification iClassIdentification;

        //		public int iEntityId;

        //		public string strEntityName;

        //		public string strInstructionName;

        //		public int screenTaskType;

        //		public int screenTaskID;

        //		public int ScreenNumber;

        //		public string ObjectText;

        //		public string Coordinate;

        //		public string TaskName;
        //	}

        public struct SfcItemInfo
        {
            public int ItemType;

            public int StepNumber;

            public int TransNumber;

            public int CommentNumber;

            public int LangP1;

            public int LangP0;

            public int LangN;

            public int LangConition;

            public string CodeAction;

            public string CodeP1;

            public string CodeP0;

            public string CodeN;

            public string CodeNotes;

            public string CodeCondition;

            public string CodeComment;
        }

        //	public struct StratonExpSlotWrFormat
        //	{
        //		public ushort _NoOfBytes;

        //		public byte _SlotNo;

        //		public byte _ExpId;

        //		public ushort _ExpXWInfoSize;

        //		public ushort _ExpYWInfoSize;

        //		public ushort _ExpMWInfoSize;

        //		public ArrayList _ExpXWInfo;

        //		public ArrayList _ExpYWInfo;

        //		public ArrayList _ExpMWInfo;
        //	}

        //	public struct StratonExpTagWrFormat
        //	{
        //		public ushort NoOfBytes;

        //		public byte TagType;

        //		public ushort RegNo;

        //		public uint SymAddr;

        //		public ArrayList CoilTags;
        //	}

        //	public struct StratonModbusMappingData
        //	{
        //		public byte _comPort;

        //		public byte _prefix;

        //		public string _modbusTagAddress;

        //		public byte _stratonTag;

        //		public string _stratonTagName;

        //		public string _dataType;

        //		public byte _IsExpansionTag;

        //		public byte _noOfBytes;

        //		public byte _IsRetentiveRegTag;

        //		public int _OffsetString;
        //	}

        //	public struct StratonTagData
        //	{
        //		public ushort _noOfBytes;

        //		public byte _hmiTagType;

        //		public ushort _regNumber;

        //		public ushort _coilNumber;

        //		public uint _symAddr;
        //	}

        //	public struct StratonTagStructure
        //	{
        //		public byte _blockType;

        //		public string _initalValue;

        //		public byte _dataType;

        //		public byte _stringLength;

        //		public byte _IsExpansionTag;

        //		public byte _IsRetentiveRegister;

        //		public byte _reserved5;

        //		public byte _IsSystemTag;

        //		public byte _slotNo;

        //		public string _nativePrefix;

        //		public string _nativeAddrVal;

        //		public string _nativeAddress;

        //		public string _Dimension;

        //		public string _ArrTagInfo;

        //		public string _StructureName;

        //		public string _StructureObjName;
        //	}

        //	public struct stReconnectNode
        //	{
        //		public ushort _usAddr;

        //		public byte[] _BitAddr;

        //		public stReconnectNode(int pSizeArr)
        //		{
        //			this._usAddr = 0;
        //			this._BitAddr = new byte[pSizeArr];
        //		}
        //	}

        //	public struct structFTPInfo
        //	{
        //		public byte _btFTPConfig;

        //		public byte _btGroupNumber;

        //		public byte _btSourceMedia;

        //		public byte _btDestMedia;

        //		public byte _btAPN;

        //		public byte _btUsername;

        //		public byte _btPassword;

        //		public byte _btServerAddr;

        //		public byte _btdestPath;

        //		public byte _btSendFileAtEvery;

        //		public byte _SourceMedia;

        //		public byte _DestinationMedia;

        //		public byte _btFileSendHour;

        //		public byte _btFileSendMinute;

        //		public byte _btFileSendSeconds;

        //		public string _strGroupNumber;

        //		public string _strAPN;

        //		public string _strUsername;

        //		public string _strPassword;

        //		public string _strServerAddr;

        //		public string _strdestPath;

        //		public int _tagIDEnableBit;

        //		public int _tagIDResendBit;

        //		public int _tagIDGrpNumber;

        //		public int _tagIDSourceMedia;

        //		public int _tagIDDestMedia;

        //		public int _tagIDAPN;

        //		public int _tagIDUserName;

        //		public int _tagIDPassword;

        //		public int _tagIDServerAddr;

        //		public int _tagIDDestPath;

        //		public int _tagIDFileSendAtEvery;

        //		public int _tagIDMediaStatus;

        //		public int _tagIDNetConnStatus;

        //		public int _tagIDFTPStatus;

        //		public int _tagIDFileSendStatus;

        //		public int _tagIDFTPBlkStatus;
        //	}

        //	public class StructReconnectNodeComparer : IComparer
        //	{
        //		public StructReconnectNodeComparer()
        //		{
        //		}

        //		public int Compare(object x, object y)
        //		{
        //			int num;
        //			if ((!(x is CommonConstants.stReconnectNode) ? false : y is CommonConstants.stReconnectNode))
        //			{
        //				CommonConstants.stReconnectNode _stReconnectNode = (CommonConstants.stReconnectNode)x;
        //				CommonConstants.stReconnectNode _stReconnectNode1 = (CommonConstants.stReconnectNode)y;
        //				num = _stReconnectNode._usAddr.CompareTo(_stReconnectNode1._usAddr);
        //			}
        //			else
        //			{
        //				num = 0;
        //			}
        //			return num;
        //		}
        //	}

        //	public struct SubParameters
        //	{
        //		public string VariableName;

        //		public string DataType;

        //		public bool IOType;
        //	}

        //	public struct TagByteLowHighbitData
        //	{
        //		public byte TagByte;

        //		public byte TagLowHighByte;

        //		public string TagAddress;
        //	}

        //	public struct TagInfo
        //	{
        //		public string _TagAddress;

        //		public string _strDriver;

        //		public string _strProtocol;
        //	}

        //	public struct TagInformation
        //	{
        //		public string _TagAdderess;

        //		public string _NodeName;

        //		public string _Bytes;

        //		public string _TagName;
        //	}

        //	public struct TagObjInfo
        //	{
        //		public string Type;

        //		public string Name;
        //	}

        //	[Serializable]
        //	public struct TagSelectionFilters
        //	{
        //		public bool _blHideSystemTags;

        //		public bool _blHideUnusedTags;

        //		public ArrayList _arrPorts;

        //		public ArrayList _arrNodenames;

        //		public ArrayList _arrBlocks;

        //		public ArrayList _arrCategory;

        //		public ArrayList _arrDatatypes;

        //		public ArrayList _arrAttributes;

        //		public ArrayList _arrTagGroups;

        //		public TagSelectionFilters(bool phidesystags)
        //		{
        //			this._blHideSystemTags = phidesystags;
        //			this._blHideUnusedTags = false;
        //			this._arrPorts = null;
        //			this._arrNodenames = null;
        //			this._arrBlocks = null;
        //			this._arrCategory = null;
        //			this._arrDatatypes = null;
        //			this._arrAttributes = null;
        //			this._arrTagGroups = null;
        //		}
        //	}

        //	[Serializable]
        //	public struct TagUsageInformation
        //	{
        //		public ClassIdentification iClassIdentification;

        //		public int iEntityId;

        //		public string strEntityName;

        //		public string strInstructionName;

        //		public int intRungNumber;

        //		public int intLineNumber;

        //		public int screenTaskType;

        //		public int screenTaskID;
        //	}

        //	[Serializable]
        //	public struct TaskListInfo4AS
        //	{
        //		public int iTouchKeyByte;

        //		public short sTopLeftX;

        //		public short sBottomRtY;

        //		public short sBottomRtX;

        //		public short sTopLeftY;

        //		public short sNoOfPressTasks;

        //		public short sNoOfPressedTasks;

        //		public short sNoOfReleaseTasks;

        //		public short sTotalPressTasks;

        //		public short sTotalPressedTasks;

        //		public short sTotalReleaseTasks;
        //	}

        //	[Serializable]
        //	public struct TaskListInfo4ES
        //	{
        //		public byte btStateNo;

        //		public byte btAdjByte;

        //		public ushort usLowLimit;

        //		public ushort usHighLimit;

        //		public short sTouchKeyByte;

        //		public short sNoOfPressTasks;

        //		public short sNoOfPressedTasks;

        //		public short sNoOfReleaseTasks;

        //		public short sTotalPressTasks;

        //		public short sTotalPressedTasks;

        //		public short sTotalReleaseTasks;
        //	}

        //	public struct TaskListInformation
        //	{
        //		public string _ScreenNumber;

        //		public List<string> _BwShowingTk;

        //		public List<string> _WhShowingTk;

        //		public List<string> _AfHidingTk;
        //	}

        //	[Serializable]
        //	public class TEXTMETRIC
        //	{
        //		public int tmHeight;

        //		public int tmAscent;

        //		public int tmDescent;

        //		public int tmInternalLeading;

        //		public int tmExternalLeading;

        //		public int tmAveCharWidth;

        //		public int tmMaxCharWidth;

        //		public int tmWeight;

        //		public byte tmItalic;

        //		public byte tmUnderlined;

        //		public byte tmStruckOut;

        //		public byte tmFirstChar;

        //		public byte tmLastChar;

        //		public byte tmDefaultChar;

        //		public byte tmBreakChar;

        //		public byte tmPitchAndFamily;

        //		public byte tmCharSet;

        //		public int tmOverhang;

        //		public int tmDigitizedAspectX;

        //		public int tmDigitizedAspectY;

        //		public TEXTMETRIC()
        //		{
        //		}
        //	}

        //	public struct TimerAddressListInfo
        //	{
        //		public string TagAddress;

        //		public double @value;

        //		public int OperandType;

        //		public int TagAddressCount;

        //		public uint intObjectId;

        //		public string Prefix;
        //	}

        //	public struct TreeNodeInfo
        //	{
        //		public string Name;

        //		public TreeNode Node;
        //	}

        //	[Serializable]
        //	public struct UndefinedTagAttrib
        //	{
        //		public int _TagId;

        //		public string _TagAdderess;

        //		public string _TagName;
        //	}

        //	public struct UniversalSerialDriver
        //	{
        //		public byte _btStartAddrType;

        //		public byte _btStartAddrAdjust;

        //		public ushort _usStartAddress;

        //		public byte _btNoOfBytesType;

        //		public byte _btNoOfBytesAdjust;

        //		public ushort _usNoOfBytesAddress;

        //		public byte _btTransSignature;

        //		public byte _btNoOfBytesSTXEnable;

        //		public byte _btNoOfBytesSTX;

        //		public ushort _usSTXFrame0;

        //		public ushort _usSTXFrame1;

        //		public ushort _usSTXFrame2;

        //		public ushort _usSTXFrame3;

        //		public ushort _usSTXFrame4;

        //		public byte _btNoOfBytesETXEnable;

        //		public byte _btNoOfBytesETX;

        //		public ushort _usETXFrame0;

        //		public ushort _usETXFrame1;

        //		public ushort _usETXFrame2;

        //		public ushort _usETXFrame3;

        //		public ushort _usETXFrame4;

        //		public byte _btChecksumTransEnable;

        //		public byte _btChecksumType1;

        //		public byte _btChecksumType2;

        //		public byte _btChecksumTransTimeout;

        //		public byte _btSilentIntervalSTXEnable;

        //		public byte _btSilentIntervalSTXAdjust;

        //		public byte _btSilentIntervalSTXType;

        //		public ushort _usSilentIntervalSTX;

        //		public byte _btStartTransEnable;

        //		public byte _btStartTransType;

        //		public byte _btStartTransAdjust;

        //		public ushort _usStartTransAddr;

        //		public byte _btTransComplEnable;

        //		public byte _btTransComplType;

        //		public byte _btTransComplAdjust;

        //		public ushort _usTransComplAddr;

        //		public byte[] ReservedBytes;

        //		public byte _btRxEnableBit;

        //		public byte _btRecStartAddrType;

        //		public byte _btRecStartAddrAdjust;

        //		public ushort _usRecStartAddress;

        //		public byte _btRecAsByteRecEnable;

        //		public byte _btRecNoOfBytesSTXEnable;

        //		public byte _btRecNoOfBytesSTX;

        //		public ushort _usRecSTXFrame0;

        //		public ushort _usRecSTXFrame1;

        //		public ushort _usRecSTXFrame2;

        //		public ushort _usRecSTXFrame3;

        //		public ushort _usRecSTXFrame4;

        //		public byte _btRecOnBitEnable;

        //		public byte _btRecOnBitType;

        //		public byte _btRecOnBitAdjust;

        //		public ushort _usRecOnBitAddress;

        //		public byte _btRecAfterBreakOfEnable;

        //		public byte _btRecAfterBreakOfType;

        //		public byte _btRecAfterBreakOfAdjust;

        //		public ushort _usRecAfterBreakOfAddress;

        //		public byte _btRecNoOfByteEnable;

        //		public byte _btRecNoOfByteType;

        //		public byte _btRecNoOfByteAdjust;

        //		public ushort _usRecNoOfByteAddress;

        //		public byte _btRecNoOfBytesETXEnable;

        //		public byte _btRecNoOfBytesETX;

        //		public ushort _usRecETXFrame0;

        //		public ushort _usRecETXFrame1;

        //		public ushort _usRecETXFrame2;

        //		public ushort _usRecETXFrame3;

        //		public ushort _usRecETXFrame4;

        //		public byte _btRecComplEnable;

        //		public byte _btRecComplType;

        //		public byte _btRecComplAdjust;

        //		public ushort _usRecComplAddress;

        //		public byte _btRecChecksumEnable;

        //		public byte _btRecChecksumType1;

        //		public byte _btRecChecksumType2;

        //		public byte _btRecCheksumErrBitEnable;

        //		public byte _btRecCheksumErrBitType;

        //		public byte _btRecCheksumErrBitAdjust;

        //		public ushort _usRecCheksumErrBitAddress;

        //		public byte _btSettRespToutMSecEnable;

        //		public byte _btSettRespToutMSecNoOfBytesType;

        //		public byte _btSettRespToutMSecAdjust;

        //		public ushort _usSettRespToutMSecAddress;

        //		public byte _btSettRespToutBitEnable;

        //		public byte _btSettRespToutBitNoOfBytesType;

        //		public byte _btSettRespToutBitAdjust;

        //		public ushort _usSettRespToutBitAddress;

        //		public byte _btSettIRDelayEnable;

        //		public byte _btSettIRDelayNoOfBytesType;

        //		public byte _btSettIRDelayAdjust;

        //		public ushort _usSettIRDelayAddress;

        //		public byte _btSettRetryCountEnable;

        //		public byte _btSettRetryCountNoOfBytesType;

        //		public byte _btSettRetryCountAdjust;

        //		public ushort _usSettRetryCountAddress;

        //		public byte[] ReservedBytes2;
        //	}

        //	public struct UniversalSerialDriverIEC
        //	{
        //		public byte _btStartAddrType;

        //		public byte _btStartAddrAdjust;

        //		public ushort _usStartAddress;

        //		public int _transStartTagID;

        //		public byte _btNoOfBytesType;

        //		public byte _btNoOfBytesAdjust;

        //		public ushort _usNoOfBytesAddress;

        //		public byte _transNoOfBytesConstOrTag;

        //		public int _transNoOfBytesTagID;

        //		public byte _btTransSignature;

        //		public byte _btNoOfBytesSTXEnable;

        //		public byte _btNoOfBytesSTX;

        //		public ushort _usSTXFrame0;

        //		public ushort _usSTXFrame1;

        //		public ushort _usSTXFrame2;

        //		public ushort _usSTXFrame3;

        //		public ushort _usSTXFrame4;

        //		public byte _btNoOfBytesETXEnable;

        //		public byte _btNoOfBytesETX;

        //		public ushort _usETXFrame0;

        //		public ushort _usETXFrame1;

        //		public ushort _usETXFrame2;

        //		public ushort _usETXFrame3;

        //		public ushort _usETXFrame4;

        //		public byte _btChecksumTransEnable;

        //		public byte _btChecksumType1;

        //		public byte _btChecksumType2;

        //		public byte _btChecksumTransTimeout;

        //		public byte _btSilentIntervalSTXEnable;

        //		public byte _btSilentIntervalSTXAdjust;

        //		public byte _btSilentIntervalSTXType;

        //		public ushort _usSilentIntervalSTX;

        //		public byte _silentIntervalConstOrTag;

        //		public int _silentIntervalTagID;

        //		public byte _btStartTransEnable;

        //		public byte _btStartTransType;

        //		public byte _btStartTransAdjust;

        //		public ushort _usStartTransAddr;

        //		public int _transStartBitTagID;

        //		public byte _btTransComplEnable;

        //		public byte _btTransComplType;

        //		public byte _btTransComplAdjust;

        //		public ushort _usTransComplAddr;

        //		public int _transCmpltBitTagID;

        //		public byte[] ReservedBytes;

        //		public byte _btRxEnableBit;

        //		public byte _btRecStartAddrType;

        //		public byte _btRecStartAddrAdjust;

        //		public ushort _usRecStartAddress;

        //		public int _RecStartTagID;

        //		public byte _btRecAsByteRecEnable;

        //		public byte _btRecNoOfBytesSTXEnable;

        //		public byte _btRecNoOfBytesSTX;

        //		public ushort _usRecSTXFrame0;

        //		public ushort _usRecSTXFrame1;

        //		public ushort _usRecSTXFrame2;

        //		public ushort _usRecSTXFrame3;

        //		public ushort _usRecSTXFrame4;

        //		public byte _btRecOnBitEnable;

        //		public byte _btRecOnBitType;

        //		public byte _btRecOnBitAdjust;

        //		public ushort _usRecOnBitAddress;

        //		public int _RecOnBitTagID;

        //		public byte _btRecAfterBreakOfEnable;

        //		public byte _btRecAfterBreakOfType;

        //		public byte _btRecAfterBreakOfAdjust;

        //		public ushort _usRecAfterBreakOfAddress;

        //		public byte _RecAfterBreakOfConstOrTag;

        //		public int _RecAfterBreakOfTagID;

        //		public byte _btRecNoOfByteEnable;

        //		public byte _btRecNoOfByteType;

        //		public byte _btRecNoOfByteAdjust;

        //		public ushort _usRecNoOfByteAddress;

        //		public byte _RecNoOfByteConstOrTag;

        //		public int _RecNoOfByteTagID;

        //		public byte _btRecNoOfBytesETXEnable;

        //		public byte _btRecNoOfBytesETX;

        //		public ushort _usRecETXFrame0;

        //		public ushort _usRecETXFrame1;

        //		public ushort _usRecETXFrame2;

        //		public ushort _usRecETXFrame3;

        //		public ushort _usRecETXFrame4;

        //		public byte _btRecComplEnable;

        //		public byte _btRecComplType;

        //		public byte _btRecComplAdjust;

        //		public ushort _usRecComplAddress;

        //		public int _RecComplTagID;

        //		public byte _btRecChecksumEnable;

        //		public byte _btRecChecksumType1;

        //		public byte _btRecChecksumType2;

        //		public byte _btRecCheksumErrBitEnable;

        //		public byte _btRecCheksumErrBitType;

        //		public byte _btRecCheksumErrBitAdjust;

        //		public ushort _usRecCheksumErrBitAddress;

        //		public int _RecChcksumErrBitTagID;

        //		public byte _btSettRespToutMSecEnable;

        //		public byte _btSettRespToutMSecNoOfBytesType;

        //		public byte _btSettRespToutMSecAdjust;

        //		public ushort _usSettRespToutMSecAddress;

        //		public byte _SettRespToutMSecConstOrTag;

        //		public int _SettRespToutMSecTagID;

        //		public byte _btSettRespToutBitEnable;

        //		public byte _btSettRespToutBitNoOfBytesType;

        //		public byte _btSettRespToutBitAdjust;

        //		public ushort _usSettRespToutBitAddress;

        //		public int _SettRespToutBitTagID;

        //		public byte _btSettIRDelayEnable;

        //		public byte _btSettIRDelayNoOfBytesType;

        //		public byte _btSettIRDelayAdjust;

        //		public ushort _usSettIRDelayAddress;

        //		public byte _SettIRDelayConstOrTag;

        //		public int _SettIRDelayTagID;

        //		public byte _btSettRetryCountEnable;

        //		public byte _btSettRetryCountNoOfBytesType;

        //		public byte _btSettRetryCountAdjust;

        //		public ushort _usSettRetryCountAddress;

        //		public byte _SettRetryCountConstOrTag;

        //		public int _SettRetryCountTagID;

        //		public byte[] ReservedBytes2;
        //	}

        //	[Serializable]
        //	public struct UserData
        //	{
        //		public int _userID;

        //		public string _userName;

        //		public string _userDescription;

        //		public string _userPassword;

        //		public string _userEmail;

        //		public string _userTelephone;

        //		public string _userSecretQuestion;

        //		public string _userAnswer;

        //		public byte _userAccessLevel;

        //		public byte _userType;

        //		public bool _userConfigure;

        //		public bool _userExecute;

        //		public bool _userPermToDownload;

        //		public bool _userPermToUpload;

        //		public bool _userPermToConfLadderLogic;

        //		public bool _userPermToCreateNewProject;
        //	}

        //	public struct USSDriverInfo
        //	{
        //		public byte _btPort;

        //		public byte _btNodeAddress;

        //		public ushort _usControlWord1;

        //		public ushort _usControlWord2;

        //		public ushort _usControlWord3;

        //		public ushort _usStatusWord1;

        //		public ushort _usStatusWord2;

        //		public byte[] _btReserved;
        //	}

        //	public struct XMLFileStructure
        //	{
        //		public short _lengthOfXMLFileNameWithExtension;

        //		public string _xmlFileNameWithExtension;

        //		public string _httpStatusCode;

        //		public string _contentType;

        //		public string _contentLength;

        //		public string _responseHeader;

        //		public string _expiresHeader;

        //		public uint _xmlFileOffSet;
        //	}

        //	[Serializable]
        //	public struct XYPlotInfoForFirmwareStructure
        //	{
        //		public int TotalobjectBytes;

        //		public int NoOfPoint;

        //		public byte ActionAftMemFull;

        //		public byte XAxisDataType;

        //		public byte YAxisDataType;

        //		public byte[] _xDirectTagAddr;

        //		public byte[] _xIndirectTagAddr;

        //		public byte[] _yDirectTagAddr;

        //		public byte[] _yIndirectTagAddr;

        //		public byte[] _DirectTagAddrStartCoil;

        //		public byte[] _IndirectTagAddrStartCoil;

        //		public byte[] _DirectTagAddrStopCoil;

        //		public byte[] _IndirectTagAddrStopCoil;

        //		public byte[] _DirectTagAddrArrayIndex;

        //		public byte[] _IndirectTagAddrArrayIndex;

        //		public List<byte[]> _DirectTagAddrArray_X;

        //		public List<byte[]> _IndirectTagAddrArray_X;

        //		public List<byte[]> _DirectTagAddrArray_Y;

        //		public List<byte[]> _IndirectTagAddrArray_Y;
        //	}
    }
}