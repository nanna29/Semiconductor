using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Wafer
{
    public string WaferID { get; set; } = ""; //웨이퍼 아이디
    public int Slot { get; set; } = 0; //몇번째 슬롯인지
    public string SampleOrientationMarkType { get; set; } = ""; //웨이퍼 깎아놓은 모양
    public string OrientationMarkLocation { get; set; } = ""; //웨이퍼 돌아간 각도
    public int SampleSize { get; set; } = 0; //웨이퍼 사이즈
    public string FileTimestamp { get; set; } = ""; //검사 시작 시간
    public string ResultTimestamp { get; set; } = ""; //검사 종료 시간
    public string InspectionStationID { get; set; } = ""; //웨이퍼 점검 장비 ID
    public string SampleType { get; set; } = ""; //검사한게 뭔지 (wafer, glass...??)

    ArrayList dieList = new ArrayList(); //다이 리스트
    ArrayList defectList = new ArrayList(); //defect 리스트

    //인수 없는 생성자
    public Wafer() { }

    //인수 있는 생성자
    public Wafer(string WaferID, int Slot, string SampleType, ArrayList dieList)
    {
        this.WaferID = WaferID;
        this.Slot = Slot;
        this.SampleType = SampleType;
        this.dieList = dieList; // 다이 리스트 생성자???
    }

    //dieList에 die 객체 추가
    public void addDie(Die e)
    {
        dieList.Add(e);
    }

    //defectList에 die 객체 추가
    public void AddDefect(Defect d)
    {
        defectList.Add(d);
    }

    //deilist 반환
    public ArrayList GetDieList()
    {
        return dieList;
    }

    //deilist i번째 객체 반환
    public Die DieAt(int i)
    {
        return dieList[i] as Die;
    }
}