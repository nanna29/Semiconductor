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

public class Ui
{
    
    public static void Main(string[] args)
    {
        //문자열 파싱
        StreamReader sr = new StreamReader("C:\\Users\\itfarm\\Desktop\\NY\\temp.txt");
        while (sr.Peek() >= 0)
        {
            // 1. 첫 라인을 읽어서 문자열로 변환
            String s = sr.ReadLine().ToString();
            //Console.WriteLine(s);

            s = ToBlankMerge(s);

            // " " 기준으로 문자열 분리
            String[] s2 = s.Split(" ", StringSplitOptions.None);

            //Console.WriteLine(s2[0]); //키값 출력

            foreach (String a in s2)
            {
               Console.WriteLine(a);
            }
            Wafer wafer = new();
            switch (s2[0])
            {
                case "FileVersion":
                    break;

                case "FileTimestamp":
                    break;

                case "InspectionStationID":
                    Console.WriteLine(s2[1]);
                    break;

                case "SampleType":
                    wafer.SampleType = s2[1];
                    break;


            }

            
        }
        sr.Close();

        static String ToBlankMerge(String input)
        {
            String result = "";

            for (int i = 0; i < input.Length; i++)
            {

                
                char code = input[i]; //한글자씩
                if (code == '"')
                {                    
                    if(code=='"')                       
                        continue;
                }
            }
            return result;
        }
        

        /*
        //다이 객체 3개 생성
        Die die = new Die(1, 0);
        Die die1 = new Die(2, 0);
        Die die2 = new Die(3, 0);

        //wafer 객체 생성
        Wafer wafer1 = new("1313_5LO");
        
        //웨이퍼에 다이 추가
        wafer1.AddDie(die);
        wafer1.AddDie(die1);
        wafer1.AddDie(die2);

        //Lot 객체 생성
        Lot lot = new("EKGA96_13");

        //lot에 웨이퍼 추가
        lot.AddWafer(wafer1);
        Console.WriteLine(lot.WaferAt(0).DieAt(1).XSampleTestPlan); //2
        Console.WriteLine(lot.WaferAt(0).WaferID); //1313_5LO


        //다이 객체 3개 생성
        Die die3 = new Die(4, 0);
        Die die4 = new Die(5, 0);
        Die die5 = new Die(6, 0);

        //wafer 객체 생성
        Wafer wafer2 = new("1414_5LO");

        //웨이퍼에 다이 추가
        wafer2.AddDie(die3);
        wafer2.AddDie(die4);
        wafer2.AddDie(die5);

        //lot에 웨이퍼 추가
        lot.AddWafer(wafer2);
        Console.WriteLine(lot.WaferAt(1).DieAt(1).XSampleTestPlan); //5
        Console.WriteLine(lot.WaferAt(0).WaferID); //1414_5LO



        int XSampleTestPlan = -6;
        int YSampleTestPlan = -4;
        double XSampleCenterLocation = 4;
        double YSampleCenterLocation = 5;
        double XDiePitch = 6;
        double YDiePitch = 10;

        int XREL=-6;
        int YREL=-4;
        double XINDEX= 1.1;
        double YINDEX= 0.7;

        Die die6 = new Die(XSampleCenterLocation, YSampleCenterLocation, 
            XDiePitch, YDiePitch, XSampleTestPlan, YSampleTestPlan);


        Wafer wafer = new();
        wafer.AddDie(die6);

        foreach (Die a in wafer.GetDieList())
        {
            if (a.XSampleTestPlan == XREL && a.YSampleTestPlan == YREL)
            {
                //die 실제 좌표계 값 가져와서 defect 실제 좌표 처리하기
                Defect defect = new Defect(XREL, YREL, XINDEX, YINDEX, a.BL_X, a.BL_Y);
                Console.WriteLine(defect.BL_X);
                Console.WriteLine(defect.BL_Y);
            }
            else
            {
                continue;
            }
        }
           
        

        Console.WriteLine(die6.BL_X); //4
        Console.WriteLine(die6.BL_Y); //5

        Console.WriteLine(die6.TL_X); //4
        Console.WriteLine(die6.TL_Y); //15

        Console.WriteLine(die6.TR_X); //10
        Console.WriteLine(die6.TR_Y); //15

        Console.WriteLine(die6.BR_X); //10
        Console.WriteLine(die6.BR_Y); //5*/




        /*//텍스트 파일 파싱
        StreamReader str = new StreamReader("C:\\Users\\itfarm\\" +
            "Desktop" + "\\NY\\ItFarm\\temp.txt");

        while (str.Peek() >= 0)
        {
            // 1. 첫 라인을 읽어서 문자열로 변환
            String s = str.ReadLine().ToString();

            // " " 기준으로 문자열 분리
            String[] s2 = s.Split(" ", StringSplitOptions.None);

            foreach (string j in s2)
            {

                if (j.Contains('"'))
                {
                    continue;
                }
                else
                    Console.WriteLine(j);
            }

        }
        str.Close();*/
    }
}
