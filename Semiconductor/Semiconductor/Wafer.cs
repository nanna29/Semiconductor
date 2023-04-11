using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Wafer
{
    private string WaferID; //웨이퍼 아이디
    private string Slot; //몇번째 슬롯인지
    private string SampleOrientationMarkType; //웨이퍼 깎아놓은 모양
    private string OrientationMarkLocation; //웨이퍼 돌아간 각도
    private int SampleSize;//웨이퍼 사이즈
    private string FileTimestamp; //검사 시작 시간
    private string ResultTimestamp; //검사 종료 시간
    private string InspectionStationID; //웨이퍼 점검 장비 ID
    private string SampleType; //검사한게 뭔지 (wafer, glass...??)

    ArrayList dieList = new ArrayList(); //다이 리스트
    ArrayList defectList = new ArrayList(); //defect 리스트
}