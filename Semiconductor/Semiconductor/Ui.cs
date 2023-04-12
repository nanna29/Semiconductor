using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Ui
{
    public static void Main(string[] args)
    {
        //텍스트 파일 파싱
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
        str.Close();

        //다이 객체 3개 생성
        Die die = new Die(1,0);
        Die die1 = new Die(2,0);
        Die die2 = new Die(3,0);

        //웨이퍼에 다이 추가
        Wafer wafer = new();
        wafer.addDie(die);
        wafer.addDie(die1);
        wafer.addDie(die2);

        //wafer 객체 2개 생성
        Wafer wafer1 = new("1313_5LO", 13, "WAFER", wafer.GetDieList());
        Wafer wafer2 = new("14134_5LO", 132, "WAFER2", wafer.GetDieList());

        //lot에 wafer 2개 추가
        Lot lot = new();
        lot.addWafer(wafer1);
        lot.addWafer(wafer2);

        //Lot 객체 1개 생성
        Lot lt = new("EKGA96_13", lot.getWaferList());
        Console.WriteLine(lot.WaferAt(0).DieAt(1).XSampleTestPlan);

    }
}
