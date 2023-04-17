using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lot
{
    public string LotID { get; set; } = ""; //Lot 아이디
    //InspectionStationID "" "2135" "E1364"; 
    public string InspectionStationID1 { get; set; } = "";
    public string InspectionStationID2 { get; set; } = ""; 
    public string InspectionStationID3 { get; set; } = ""; 
    public string SetupID { get; set; } = ""; //공정정보
    public string StepID { get; set; } = ""; //공정정보


    //ArrayList waferList = new ArrayList(); //웨이퍼 리스트
    List<Wafer> waferList = new List<Wafer>();

    //인수 있는 생성자 
    public Lot(string LotID)
    {
        this.LotID = LotID;
    }

    //waferList에 wafer 객체 추가
    public void AddWafer(Wafer w)
    {
        waferList.Add(w);
    }

    //waferList i번째 객체 반환
    public Wafer WaferAt(int i)
    {
        return waferList[i] as Wafer;
    }
}
