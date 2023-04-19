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
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Ui
{
    //read: 한 글자씩 읽기
    //readline: 한 줄씩 읽기

    public static void Main(string[] args)
    {
        string path = "C:\\Users\\itfarm\\Desktop\\NY\\content(2).txt";
        Ui ui = new Ui();
        //ui.LotParse(path);
        //ui.WaferParse(path);
        DieParse(path);
        //ui.DefectParse(path);

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

            //"" 삭제
            str = str.Replace("\"", "");

            // " " 기준으로 문자열 분리
            string[] strArr = str.Split(" ", StringSplitOptions.None);

            foreach (string a in strArr)
            {
                //Console.WriteLine(a);
            }

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

        //Console.WriteLine(lot.InspectionStationID1);
        //Console.WriteLine(lot.InspectionStationID2);
        //Console.WriteLine(lot.InspectionStationID3);
        //Console.WriteLine(lot.LotID);
        //Console.WriteLine(lot.SetupID);
        //Console.WriteLine(lot.StepID);

        return lot;

    }
    //Wafer
    public Wafer WaferParse(string path)
    {
        Wafer wafer = new();

        //문자열 파싱
        StreamReader rdr = new StreamReader(path);
        while (rdr.Peek() >= 0)
        {
            // 첫 라인을 읽어서 문자열로 변환
            string s = rdr.ReadLine();

            // ; 제거
            s = s.Replace(";", "");

            // " " 기준으로 문자열 분리
            string[] strArr = s.Split(" ", StringSplitOptions.None);

            switch (strArr[0])
            {
                case "SampleType":
                    wafer.SampleType = strArr[1];
                    break;

                case "ResultTimestamp":
                    //2003이 아니라 03이라 오류발생, 20붙임
                    string[] temp = strArr[1].Split("-", StringSplitOptions.None);
                    temp[2] = "20" + temp[2];
                    strArr[1] = temp[0] + "-" + temp[1] + "-" + temp[2] + " " + strArr[2];

                    DateTime.TryParse(strArr[1], out DateTime tempDate);
                    wafer.ResultTimestamp = tempDate;
                    break;

                case "FileTimestamp":
                    //2003이 아니라 03이라 오류발생, 20붙임
                    string[] temp2 = strArr[1].Split("-", StringSplitOptions.None);
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

        /*
        Console.WriteLine(wafer.SampleType);
        Console.WriteLine(wafer.ResultTimestamp);
        Console.WriteLine(wafer.FileTimestamp);
        Console.WriteLine(wafer.SampleSize);
        Console.WriteLine(wafer.SampleOrientationMarkType);
        Console.WriteLine(wafer.OrientationMarkLocation);
        Console.WriteLine(wafer.WaferID);
        Console.WriteLine(wafer.Slot);
        */

        return wafer;
    }
    //Die
    public static Die DieParse(string path)
    {
        double XSampleCenterLocation = 0;
        double YSampleCenterLocation = 0;
        double XDiePitch = 0;
        double YDiePitch = 0;
        int XSampleTestPlan = 0;
        int YSampleTestPlan = 0;
        StreamReader rdr = new StreamReader(path);
        //Console.WriteLine(rdr.BaseStream.Length);
        while (rdr.Peek() >= 0)
        {


            Die die1 = new(XSampleCenterLocation, YSampleCenterLocation, XDiePitch,
                               YDiePitch, XSampleTestPlan, YSampleTestPlan);
            return die1;

        }
        


        /*  
    for (int i=0; i< 672; i++)
    {

        // 1. 첫 라인을 읽어서 문자열로 변환
        string s = rdr.ReadLine();

        // " " 기준으로 문자열 분리
        string[] strArr = s.Split(" ", StringSplitOptions.None);


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
                    //읽어오기
                    string tmps = rdr.ReadLine();
                    //키값 검사해서 다음줄로 내리기
                    if (strArr[0] == "SampleTestPlan")
                        i++;

                    int dieIndex = 0;
                    strArr = tmps.Split(" ", StringSplitOptions.None);
                    //마지막 다이라면
                    if (strArr[1].Contains(";"))
                    {
                        XSampleTestPlan = Convert.ToInt32(strArr[0]);
                        YSampleTestPlan = Convert.ToInt32(strArr[1].Trim(';'));
                        i++;
                        dieIndex++;
                        break;
                    }
                    //마지막 다이가 아니라면
                    else
                    {
                        XSampleTestPlan = Convert.ToInt32(strArr[0]);
                        YSampleTestPlan = Convert.ToInt32(strArr[1]);

                        dieIndex++;

                        i++;                          
                    }



                    //continue;
                }                   
                break;


        }

    }
    rdr.Close();
    Console.WriteLine(die1.XSampleTestPlan);
    Console.WriteLine(die1.YSampleTestPlan);
    Console.WriteLine(die1.BL_X);
    Console.WriteLine(die1.BL_Y);
    return die1;

    /*
    while (rdr.Peek() >= 0)
    {
        // 1. 첫 라인을 읽어서 문자열로 변환
        string s = rdr.ReadLine();

        //s = ToBlankMerge(s);

        // " " 기준으로 문자열 분리
        string[] strArr = s.Split(" ", StringSplitOptions.None);

        //foreach (string a in strArr)
        //{
        //    Console.WriteLine(a);
        //}

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
                for (int i = 0; i < Convert.ToInt32(strArr[1])+1 ; i++)
                {
                    if (strArr[0] == "SampleTestPlan")
                        continue;
                    XSampleTestPlan = Convert.ToInt32(strArr[0]);
                    YSampleTestPlan = Convert.ToInt32(strArr[1]);
                    Console.WriteLine(XSampleTestPlan+" , "+ YSampleTestPlan);

                    foreach (char code in strArr[1])
                    {
                        if (code == ';')
                            break;
                    }


                }
                break;

        }

    }
    rdr.Close();*/
        //Die die = new(XSampleCenterLocation, YSampleCenterLocation, XDiePitch,
        //YDiePitch, XSampleTestPlan, YSampleTestPlan);


        //Console.WriteLine(XSampleTestPlan + " , " + YSampleTestPlan);


        /*
        Console.WriteLine(die.XSampleCenterLocation); //2340.3730 
        Console.WriteLine(die.YSampleCenterLocation); // 10264.9974;
        Console.WriteLine(die.XDiePitch); //2869.9276
        Console.WriteLine(die.YDiePitch); // 20643.7677;*/


    }
    /*
    public Defect DefectParse(string path)
    {
        Lot lot = new Lot();
        //string InspectionStationID1 = "";
        //string InspectionStationID2 = "";
        //string InspectionStationID3 = "";
        //string LotID = "";
        //string SetupID = "";
        //string StepID = "";

        //문자열 파싱
        StreamReader rdr = new StreamReader(path);
        while (rdr.Peek() >= 0)
        {
            // 1. 첫 라인을 읽어서 문자열로 변환
            string s = rdr.ReadLine();

            //s = ToBlankMerge(s);
            s = s.Replace(";", "");

            // " " 기준으로 문자열 분리
            string[] strArr = s.Split(" ", StringSplitOptions.None);

            //foreach (string a in strArr)
            //{
            //    Console.WriteLine(a);
            //}

            switch (strArr[0])
            {
                case "InspectionStationID":
                    lot.InspectionStationID1 = strArr[1];
                    lot.InspectionStationID2 = strArr[2];
                    lot.InspectionStationID3 = strArr[3];
                    break;

                case "LotID":
                    lot.LotID = strArr[1];
                    break;

                case "SetupID":
                    lot.SetupID = strArr[1];
                    break;

                case "StepID":
                    lot.StepID = strArr[1];
                    break;
            }
        }
        rdr.Close();


        static string ToBlankMerge(string input)
        {
            string result = "";

            for (int i = 0; i < input.Length; i++)
            {


                char code = input[i]; //한글자씩
                if (code == '"')
                {
                    if (code == '"')
                        continue;
                }
            }
            return result;
        }


        Console.WriteLine(lot.LotID);
        Console.WriteLine(lot.SetupID);
        Console.WriteLine(lot.StepID);
        Console.WriteLine(lot.InspectionStationID2);

        return defect;
    }*/



}
