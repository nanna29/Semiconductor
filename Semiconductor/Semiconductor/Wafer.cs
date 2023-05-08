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

public class Wafer
{
    public string WaferID { get; set; } = ""; //웨이퍼 아이디
    public int Slot { get; set; } = 0; //몇번째 슬롯인지
    public string SampleOrientationMarkType { get; set; } = ""; //웨이퍼 깎아놓은 모양
    public string OrientationMarkLocation { get; set; } = ""; //웨이퍼 돌아간 각도
    public int SampleSize { get; set; } = 0; //웨이퍼 사이즈
    public DateTime FileTimestamp { get; set; } //검사 시작 시간
    public DateTime ResultTimestamp { get; set; } //검사 종료 시간
    public string SampleType { get; set; } = ""; 

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

    //그리기
    public void Draw()
    {        
        int nTL_X;
        int nTL_Y;
        int nBR_X;
        int nBR_Y;   

        WriteableBitmap writeableBmp = BitmapFactory.New(800, 800);
        writeableBmp.Clear(Colors.White);
        writeableBmp.FillEllipseCentered(400, 400, 400, 400, Colors.Gray);
        writeableBmp.DrawRectangle(0,4,5,0, Colors.Blue);

        foreach (Die die in dieList)
        {
            //윈도우 좌표로 변환
            nTL_X = (int)die.TL_X / 250 + 400;
            nTL_Y = -(int)(die.TL_Y / 250 ) + 400;
            nBR_X = (int)die.BR_X / 250 + 400;
            nBR_Y = -(int)(die.BR_Y / 250 ) + 400;

            //추가 좌표이동 X
            writeableBmp.DrawRectangle(nTL_X, nTL_Y, nBR_X, nBR_Y, Colors.Black);

            //추가 좌표이동 O (x: -11, Y: +82)
            writeableBmp.DrawRectangle(nTL_X - (int)die.XDiePitch / 250, nTL_Y + (int)die.YDiePitch / 250, nBR_X - (int)die.XDiePitch / 250, nBR_Y + (int)die.YDiePitch / 250, Colors.Blue);
        }
        

        foreach(Defect defect in defectList)
        {
            //추가 좌표이동 X
            nTL_X = (int)defect.BL_X / 250 + 400;
            nTL_Y = -(int)defect.BL_Y / 250 + 400;
            writeableBmp.DrawRectangle(nTL_X, nTL_Y, nTL_X + 3, nTL_Y + 3, Colors.Orange);

            //추가 좌표이동 O (x: -11, Y: +82)
            writeableBmp.DrawRectangle(nTL_X - 11, nTL_Y + 82, nTL_X - 11 + 3, nTL_Y + 82 + 3, Colors.Red);
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
    }
}