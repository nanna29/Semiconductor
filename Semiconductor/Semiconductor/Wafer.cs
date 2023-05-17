using Semiconductor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

public class Wafer : Notifier
{
    public string WaferID { get; set; } = ""; //웨이퍼 아이디
    public int Slot { get; set; } = 0; //몇번째 슬롯인지
    public string SampleOrientationMarkType { get; set; } = ""; //웨이퍼 깎아놓은 모양
    public string OrientationMarkLocation { get; set; } = ""; //웨이퍼 돌아간 각도
    public int SampleSize { get; set; } = 0; //웨이퍼 사이즈
    public DateTime FileTimestamp { get; set; } //검사 시작 시간
    public DateTime ResultTimestamp { get; set; } //검사 종료 시간
    public string SampleType { get; set; } = "";

    /*
    public int nTL_X { get; set; } = 0;
    public int nTL_Y { get; set; } = 0;
    public int nBR_X { get; set; } = 0;
    public int nBR_Y { get; set; } = 0;*/


    List<Die> dieList = new List<Die>();
    List<Defect> defectList = new List<Defect>();

    //인수 없는 생성자
    public Wafer() {}

    //dieList에 die 객체 추가
    public void AddDie(Die e) {
        dieList.Add(e);
    } 

    //deilist 반환
    public List<Die> GetDieList()
    {
        return dieList;
    }

    //deilist i번째 객체 반환
    public Die DieAt(int i)
    {
        return dieList[i] as Die;
    }

    //defectList에 defect 객체 추가
    public void AddDefect(Defect d) {
        defectList.Add(d);
    } 

    //defectList 반환
    public List<Defect> GetDefectList()
    {
        return defectList;
    }

    //defectList i번째 객체 반환
    public Defect DefectAt(int i)
    {
        return defectList[i] as Defect;
    }

    //(해당 기능 view model로 이동해서 주석처리함)
    /*
    //그리기
    public void GetCoordinate()
    {
        WriteableBitmap writeableBmp = BitmapFactory.New(800, 800);
        writeableBmp.Clear(Colors.White);
        writeableBmp.FillEllipseCentered(400, 400, 400, 400, Colors.Gray);

        foreach (Die die in dieList)
        {
            //윈도우 좌표로 변환
            nTL_X = ((int)die.TL_X / 250) + 400 - 11;
            nTL_Y = (800 - (int)(die.TL_Y / 250)) - 400 + 82;
            nBR_X = ((int)die.BR_X / 250) + 400 - 11;
            nBR_Y = (800 - (int)(die.BR_Y / 250)) - 400 + 82;

            //애초에 0,0이 진짜 0,0이 아니라, X: 23... Y:102.... 인 곳에서 시작이라서, 기준점 400,400을 0,0 으로 떙겨서 쓸거면, 걔네 좌표도
            //0,0에서 시작되게 보정해줘야함 X는 x축쪽으로 끌고오면 -값, y는 +값 (내려옴)
            writeableBmp.DrawRectangle(nTL_X, nTL_Y, nBR_X, nBR_Y, Colors.Black);

        }


        foreach (Defect defect in defectList)
        {
            //추가 좌표이동 X
            nTL_X = (int)defect.BL_X / 250 + 400 - 11;
            nTL_Y = -(int)defect.BL_Y / 250 + 400 + 82;

            writeableBmp.DrawRectangle(nTL_X, nTL_Y, nTL_X + 3, nTL_Y + 3, Colors.Red);
        }

        //png 파일로 저장하기
        void CreateThumbnail(string filename, BitmapSource image5)
        {
            if (filename != string.Empty)
            {
                using (FileStream stream5 = new FileStream(filename, FileMode.Create))
                {
                    PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                    encoder5.Frames.Add(BitmapFrame.Create(image5));
                    encoder5.Save(stream5);
                }
            }
        }

        CreateThumbnail("result2.png", writeableBmp.Clone());
    }*/

}


