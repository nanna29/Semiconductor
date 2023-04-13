using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Defect
{
    //?????
    //XSIZE YSIZE DEFECTAREA DSIZE CLASSNUMBER TEST CLUSTERNUMBER
    //ROUGHBINNUMBER FINEBINNUMBER REVIEWSAMPLE IMAGECOUNT IMAGELIST;

    public int XINDEX { get; set; } = 0; //결함 있는 다이의 좌표
    public int YINDEX { get; set; } = 0;//결함 있는 다이의 좌표
    public double XREL { get; set; } = 0; //찾은 다이 내의 결함의 x좌표
    public double YREL { get; set; } = 0;//찾은 다이 내의 결함의 x좌표
    //+키 (defect id)
    //인수 있는 생성자
    public Defect(int XINDEX, int YINDEX, double XREL, double YREL)
    {
        this.XINDEX = XINDEX;
        this.YINDEX = YINDEX;

        this.XREL = XREL;
        this.YREL = YREL;
    }
}