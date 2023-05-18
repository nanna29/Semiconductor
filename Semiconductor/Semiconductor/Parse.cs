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
using Semiconductor;

public class Parse
{
    public Parse() { }

    public Wafer parse(string Path)
    {
        //Path: viewmodel에서 PathText로 받아온 경로
        string path = Path;
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


        //파싱 후, 그리기 작업 (view model로 이동해서 주석처리함)
        //wafer.GetCoordinate();

        Console.WriteLine(die.Count); //418
        Console.WriteLine(defect.Count); //234

        return wafer;

        
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
                    strArr[1] = strArr[1] + " " + strArr[2];
                    Boolean result1 = DateTime.TryParse(strArr[1], out DateTime tempDate2);
                    if (result1)
                    {
                        wafer.ResultTimestamp = tempDate2;
                    }
                    //2003 에서 20빠진 경우
                    else
                    {
                        string[] temp2 = strArr[1].Split(new string[] { "-" }, StringSplitOptions.None);
                        temp2[2] = "20" + temp2[2];
                        strArr[1] = temp2[0] + "-" + temp2[1] + "-" + temp2[2];

                        DateTime.TryParse(strArr[1], out DateTime tempDate3);
                        wafer.ResultTimestamp = tempDate3;
                    }
                    break;



                case "FileTimestamp":
                    strArr[1] = strArr[1] + " " + strArr[2];
                    Boolean result = DateTime.TryParse(strArr[1], out DateTime tempDate4);
                    if (result)
                    {              
                        wafer.FileTimestamp = tempDate4;
                    }
                    //2003 에서 20빠진 경우
                    else
                    {
                        string[] temp2 = strArr[1].Split(new string[] { "-" }, StringSplitOptions.None);
                        temp2[2] = "20" + temp2[2];
                        strArr[1] = temp2[0] + "-" + temp2[1] + "-" + temp2[2];

                        DateTime.TryParse(strArr[1], out DateTime tempDate5);
                        wafer.FileTimestamp = tempDate5;
                    }           
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

        int DEFECTID = 0;
        double XREL = 0;
        double YREL = 0;
        int XINDEX = 0;
        int YINDEX = 0;

        Boolean result = true;
        while (result)
        {
            // 1. 첫 라인을 읽어서 문자열로 변환
            string s = rdr.ReadLine();

            // " " 기준으로 문자열 분리
            string[] strArr = s.Split(new string[] { " " }, StringSplitOptions.None);

            switch (strArr[0])
            {
                case "DefectList":
                    for (int k = 0; k < File.ReadAllLines(path).Count(); k++)
                    {
                        //읽어오기
                        string tmps = rdr.ReadLine().TrimStart();

                        strArr = tmps.Split(new string[] { " " }, StringSplitOptions.None);

                        //마지막 다이라면 (; 있는지 검사 + 반복문 종료)
                        try
                        {
                            if (strArr[16].Contains(";"))
                            {
                                DEFECTID = Convert.ToInt32(strArr[0]);
                                XREL = Convert.ToDouble(strArr[1]);
                                YREL = Convert.ToDouble(strArr[2]);
                                XINDEX = Convert.ToInt32(strArr[3]);
                                YINDEX = Convert.ToInt32(strArr[4]);

                                Create();

                                result = false;
                                break;
                            }
                            //마지막 다이가 아니라면
                            else
                            {
                                DEFECTID = Convert.ToInt32(strArr[0]);
                                XREL = Convert.ToDouble(strArr[1]);
                                YREL = Convert.ToDouble(strArr[2]);
                                XINDEX = Convert.ToInt32(strArr[3]);
                                YINDEX = Convert.ToInt32(strArr[4]);

                                Create();
                            }
                        }
                        catch (Exception e)
                        {
                            if (strArr[1].Contains(";"))
                            {
                                result = false;
                                break;
                            }
                            else
                            {
                                k--;
                                continue;
                                
                            }

                        }

                        void Create()
                        {
                            foreach (Die a in wafer.GetDieList())
                            {
                                if (a.XSampleTestPlan == XINDEX && a.YSampleTestPlan == YINDEX)
                                {
                                    //die 실제 좌표계 값 가져와서 defect 실제 좌표 처리하기
                                    Defect defect = new Defect(XREL, YREL, XINDEX, YINDEX, a.BL_X, a.BL_Y);
                                    defect.DEFECTID = DEFECTID;

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
