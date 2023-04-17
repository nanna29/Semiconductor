using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Die
{
    public double XSampleCenterLocation { get; set; } = 0; //기준 다이 찾을때 점찍는 X좌표
    public double YSampleCenterLocation { get; set; } = 0; //기준 다이 찾을때 점찍는 Y좌표
    public double XDiePitch { get; set; } = 0; //기준 다이 찾을 때 가로 폭
    public double YDiePitch { get; set; } = 0; //기준 다이 찾을 때 세로 폭
    public int XSampleTestPlan { get; set; } = 0; //다이 X좌표
    public int YSampleTestPlan { get; set; } = 0; //다이 Y좌표

    public double BL_X { get; set; } = 0;
    public double BL_Y { get; set; } = 0;

    public double TL_X { get; set; } = 0;
    public double TL_Y { get; set; } = 0;

    public double TR_X { get; set; } = 0;
    public double TR_Y { get; set; } = 0;

    public double BR_X { get; set; } = 0;
    public double BR_Y { get; set; } = 0;

    //인수 있는 생성자
    public Die(double XSampleCenterLocation, double YSampleCenterLocation,
        double XDiePitch, double YDiePitch, int XSampleTestPlan, int YSampleTestPlan)
    {
        this.XSampleTestPlan = XSampleTestPlan;
        this.YSampleTestPlan = YSampleTestPlan;

        this.BL_X = XSampleCenterLocation + XDiePitch * XSampleTestPlan;
        this.BL_Y = YSampleCenterLocation + YDiePitch * YSampleTestPlan;

        this.TL_X = XSampleCenterLocation + XDiePitch * XSampleTestPlan;
        this.TL_Y = YSampleCenterLocation + YDiePitch * (1 + YSampleTestPlan);

        this.TR_X = XSampleCenterLocation + XDiePitch * (1 + XSampleTestPlan);
        this.TR_Y = YSampleCenterLocation + YDiePitch * (1 + YSampleTestPlan);

        this.BR_X = XSampleCenterLocation + XDiePitch * (1 + XSampleTestPlan);
        this.BR_Y = YSampleCenterLocation + YDiePitch * YSampleTestPlan;
    }



    

}