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
    public DateTime FileTimestamp { get; set; } //검사 시작 시간
    public DateTime ResultTimestamp { get; set; } //검사 종료 시간
    public string SampleType { get; set; } = ""; 

    //ArrayList dieList = new ArrayList(); //다이 리스트
    //ArrayList defectList = new ArrayList(); //defect 리스트

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
}