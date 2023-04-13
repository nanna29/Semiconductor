using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

public class Ui
{
    
    public static void Main(string[] args)
    {
        //다이 객체 3개 생성
        Die die = new Die(1, 0);
        Die die1 = new Die(2, 0);
        Die die2 = new Die(3, 0);

        //wafer 객체 생성
        Wafer wafer1 = new("1313_5LO", 13, "FLAT", "LEFT", 200, "01-18-03 10:01:42",
        "01-18-03 09:56:13", "\"2135\" \"E1364\"", "WAFER");
        
        //웨이퍼에 다이 추가
        wafer1.AddDie(die);
        wafer1.AddDie(die1);
        wafer1.AddDie(die2);

        //Lot 객체 생성
        Lot lot = new("EKGA96_13");

        //lot에 웨이퍼 추가
        lot.AddWafer(wafer1);
        Console.WriteLine(lot.WaferAt(0).DieAt(1).XSampleTestPlan); //2
        Console.WriteLine(lot.WaferAt(0).OrientationMarkLocation); //left



        //다이 객체 3개 생성
        Die die3 = new Die(4, 0);
        Die die4 = new Die(5, 0);
        Die die5 = new Die(6, 0);

        //wafer 객체 생성
        Wafer wafer2 = new("1414_5LO", 10, "FLAT", "Right", 300, "02-10-03 11:12:51",
        "02-10-03 09:40:41", "\"2135\" \"E1364\"", "WAFER");

        //웨이퍼에 다이 추가
        wafer2.AddDie(die3);
        wafer2.AddDie(die4);
        wafer2.AddDie(die5);

        //lot에 웨이퍼 추가
        lot.AddWafer(wafer2);
        Console.WriteLine(lot.WaferAt(1).DieAt(1).XSampleTestPlan); //5
        Console.WriteLine(lot.WaferAt(1).OrientationMarkLocation); //right





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
