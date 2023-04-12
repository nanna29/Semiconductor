using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lot
{
    string LotID { get; set; } = ""; //Lot 아이디

    ArrayList waferList = new ArrayList();

    //List<Wafer> waferList = null;

    //List<Wafer> waferList = new List<Wafer>();
    //인수 없는 생성자
    public Lot() { }

    //인수 있는 생성자 
    public Lot(string LotID, ArrayList waferList)
    {
        this.LotID = LotID;
        this.waferList = waferList;
    }

    //waferList에 wafer 객체 추가
    public void addWafer(Wafer w)
    {
        waferList.Add(w);
    }

    //waferList 반환
    public ArrayList getWaferList()
    {
        return waferList;
    }

    //waferList i번째 객체 반환
    public Wafer WaferAt(int i)
    {
        return waferList[i] as Wafer;
    }
}
