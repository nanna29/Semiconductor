using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Diagnostics.SymbolStore;
using System.ComponentModel;
//using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;

public class Parse
{
    public Parse() { }

    public void parse()
    {
        string path = "C:\\Users\\itfarm\\Desktop\\NY\\content.txt";
        Parse pasre = new Parse();

        Lot lot = pasre.LotParse(path);
        Wafer wafer = pasre.WaferParse(path);

        //lot에 wafer 객체 추가
        lot.AddWafer(wafer);

        Dictionary<int, Die> die = pasre.DieParse(path);
        //wafer에 die 추가
        for (int i = 0; i < die.Count; i++)
        {
            wafer.AddDie(die[i]); //die 객체 1~nnn 개까지 추가
        }


        Dictionary<int, Defect> defect = pasre.DefectParse(path);
        //wafer에 defect 추가
        for (int k = 0; k < defect.Count; k++)
        {
            wafer.AddDefect(defect[k]); //defect 객체 1~nnn 개까지 추가
        }

        //파싱 후, 그리기 작업
        wafer.Draw();

        Console.WriteLine(die.Count); //418
        Console.WriteLine(defect.Count); //234
    }

    //Lot
    public Lot LotParse(string path)
    {
        Lot lot = new Lot();

        //문자열 파싱
        StreamReader rdr = new StreamReader(path);

        while (rdr.Peek() >= 0)
        {
            // 첫 라인을 읽어서 문자열로 변환
            string str = rdr.ReadLine();

            // " 제거
            str = str.Replace("\"", "");

            // " " 기준으로 문자열 분리
            string[] strArr = str.Split(new string[] { " " }, StringSplitOptions.None);

            switch (strArr[0])
            {
                case "InspectionStationID":
                    lot.InspectionStationID1 = strArr[1];
                    lot.InspectionStationID2 = strArr[2];
                    lot.InspectionStationID3 = strArr[3].Trim(';');
                    break;

                case "LotID":
                    lot.LotID = strArr[1].Trim(';');
                    break;

                case "SetupID":
                    lot.SetupID = strArr[1];
                    break;

                case "StepID":
                    lot.StepID = strArr[1].Trim(';');
                    break;
            }

        }
        rdr.Close();

        return lot;

    }

    //Wafer
    Wafer wafer =  new Wafer();
    public Wafer WaferParse(string path)
    {
        //문자열 파싱
        StreamReader rdr = new StreamReader(path);

        while (rdr.Peek() >= 0)
        {
            // 첫 라인을 읽어서 문자열로 변환
            string s = rdr.ReadLine();

            // ; 제거
            s = s.Replace(";", "");

            // " " 기준으로 문자열 분리
            string[] strArr = s.Split(new string[] { " " }, StringSplitOptions.None);

            switch (strArr[0])
            {
                case "SampleType":
                    wafer.SampleType = strArr[1];
                    break;

                case "ResultTimestamp":
                    //2003이 아니라 03이라 오류발생, 20붙임
                    string[] temp = strArr[1].Split(new string[] { "-" }, StringSplitOptions.None);
                    temp[2] = "20" + temp[2];
                    strArr[1] = temp[0] + "-" + temp[1] + "-" + temp[2] + " " + strArr[2];

                    DateTime.TryParse(strArr[1], out DateTime tempDate);
                    wafer.ResultTimestamp = tempDate;
                    break;

                case "FileTimestamp":
                    //2003이 아니라 03이라 오류발생, 20붙임
                    string[] temp2 = strArr[1].Split(new string[] { "-" }, StringSplitOptions.None);
                    temp2[2] = "20" + temp2[2];
                    strArr[1] = temp2[0] + "-" + temp2[1] + "-" + temp2[2] + " " + strArr[2];

                    DateTime.TryParse(strArr[1], out DateTime tempDate2);
                    wafer.FileTimestamp = tempDate2;
                    break;

                case "SampleSize":
                    wafer.SampleSize = Convert.ToInt32(strArr[2]);
                    break;

                case "SampleOrientationMarkType":
                    wafer.SampleOrientationMarkType = strArr[1];
                    break;

                case "OrientationMarkLocation":
                    wafer.OrientationMarkLocation = strArr[1];
                    break;

                case "WaferID":
                    wafer.WaferID = strArr[1].Trim('"');
                    break;

                case "Slot":
                    wafer.Slot = Convert.ToInt32(strArr[1]);
                    break;
            }
        }

        rdr.Close();

        return wafer;
    }

    //Die
    public Dictionary<int, Die> DieParse(string path)
    {
        StreamReader rdr = new StreamReader(path);

        //다이 객체들 담아줄 dic 생성
        Dictionary<int, Die> dieDic = new Dictionary<int, Die>();

        double XSampleCenterLocation = 0;
        double YSampleCenterLocation = 0;
        double XDiePitch = 0;
        double YDiePitch = 0;
        int XSampleTestPlan = 0;
        int YSampleTestPlan = 0;

        for (int i = 0; i < File.ReadAllLines(path).Count(); i++)
        {
            // 첫 라인을 읽어서 문자열로 변환
            string s = rdr.ReadLine();

            // " " 기준으로 문자열 분리
            string[] strArr = s.Split(new string[] { " " }, StringSplitOptions.None);

            switch (strArr[0])
            {
                case "SampleCenterLocation":
                    XSampleCenterLocation = Convert.ToDouble(strArr[1]);
                    YSampleCenterLocation = Convert.ToDouble(strArr[2].Trim(';'));
                    break;

                case "DiePitch":
                    XDiePitch = Convert.ToDouble(strArr[1]);
                    YDiePitch = Convert.ToDouble(strArr[2].Trim(';'));
                    break;


                case "SampleTestPlan":
                    int count = Convert.ToInt32(strArr[1]);
                    for (int k = 0; k < count + 1; k++)
                    {
                        //숫자부터 다시 읽어오기
                        string tmps = rdr.ReadLine();

                        //x좌표 y좌표 분리
                        strArr = tmps.Split(new string[] { " " }, StringSplitOptions.None);

                        //마지막 defect (; 있는지 검사 + 반복문 종료)
                        if (strArr[1].Contains(";"))
                        {
                            XSampleTestPlan = Convert.ToInt32(strArr[0]);
                            YSampleTestPlan = Convert.ToInt32(strArr[1].Trim(';'));

                            i++;

                            Create();

                            break;
                        }

                        //마지막 다이가 아니라면
                        else
                        {
                            XSampleTestPlan = Convert.ToInt32(strArr[0]);
                            YSampleTestPlan = Convert.ToInt32(strArr[1]);

                            i++;

                            Create();
                        }

                        void Create()
                        {
                            //다이 객체 생성
                            Die die = new Die(XSampleCenterLocation, YSampleCenterLocation, XDiePitch,
                               YDiePitch, XSampleTestPlan, YSampleTestPlan);

                            //dic에 추가 (키는 0부터 시작)
                            dieDic.Add(k, die);
                        }
                    }
                    break;
            }
        }
        rdr.Close();

        return dieDic;
    }

    //Defect
    public Dictionary<int, Defect> DefectParse(string path)
    {
        StreamReader rdr = new StreamReader(path);

        //defect 객체 담아줄 dic 생성
        Dictionary<int, Defect> defectDic = new Dictionary<int, Defect>();

        int DefectRecordSpec = 0;
        int XREL = 0;
        int YREL = 0;
        double XINDEX = 0;
        double YINDEX = 0;

        //밑에 summarySpec~eof 4줄 빼주기 (반복문 헛도는거 방지)
        for (int i = 0; i < File.ReadAllLines(path).Count() - 4; i++)
        {
            // 1. 첫 라인을 읽어서 문자열로 변환
            string s = rdr.ReadLine();

            // " " 기준으로 문자열 분리
            string[] strArr = s.Split(new string[] { " " }, StringSplitOptions.None);

            switch (strArr[0])
            {
                case "DefectList":

                    for (int k = 0; k < 234 + 1; k++)
                    {
                        //읽어오기
                        string tmps = rdr.ReadLine().TrimStart();

                        strArr = tmps.Split(new string[] { " " }, StringSplitOptions.None);

                        //마지막 다이라면 (; 있는지 검사 + 반복문 종료)
                        if (strArr[16].Contains(";"))
                        {
                            DefectRecordSpec = Convert.ToInt32(strArr[0]);
                            XREL = Convert.ToInt32(strArr[3]);
                            YREL = Convert.ToInt32(strArr[4]);
                            XINDEX = Convert.ToDouble(strArr[5]);
                            YINDEX = Convert.ToDouble(strArr[6]);

                            i++;

                            Create();

                            break;
                        }

                        //마지막 다이가 아니라면
                        else
                        {
                            DefectRecordSpec = Convert.ToInt32(strArr[0]);
                            XREL = Convert.ToInt32(strArr[3]);
                            YREL = Convert.ToInt32(strArr[4]);
                            XINDEX = Convert.ToDouble(strArr[5]);
                            YINDEX = Convert.ToDouble(strArr[6]);

                            i++;

                            Create();
                        }

                        void Create()
                        {

                            foreach (Die a in wafer.GetDieList())
                            {
                                if (a.XSampleTestPlan == XREL && a.YSampleTestPlan == YREL)
                                {
                                    //die 실제 좌표계 값 가져와서 defect 실제 좌표 처리하기
                                    Defect defect = new Defect(XREL, YREL, XINDEX, YINDEX, a.BL_X, a.BL_Y);
                                    defect.DefectRecordSpec = DefectRecordSpec;

                                    //차례로 defectDic에 키값과 저장하기
                                    defectDic.Add(k, defect);
                                }
                            }
                        }

                    }
                    break;
            }
        }
        rdr.Close();

        return defectDic;
    }
}
