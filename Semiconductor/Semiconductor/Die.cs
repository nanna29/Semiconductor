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
        this.XSampleCenterLocation = XSampleCenterLocation;
        this.YSampleCenterLocation = YSampleCenterLocation;

        this.XDiePitch = XDiePitch;
        this.YDiePitch = YDiePitch;

        this.XSampleTestPlan = XSampleTestPlan;
        this.YSampleTestPlan = YSampleTestPlan;

        //실제 좌표 계산
        this.BL_X = Math.Truncate((XSampleCenterLocation + XDiePitch * XSampleTestPlan) * 10000) / 10000;
        this.BL_Y = Math.Truncate((YSampleCenterLocation + YDiePitch * YSampleTestPlan) * 10000) / 10000; 

        this.TL_X = Math.Truncate((XSampleCenterLocation + XDiePitch * XSampleTestPlan) * 10000) / 10000; 
        this.TL_Y = Math.Truncate((YSampleCenterLocation + YDiePitch * (1 + YSampleTestPlan)) * 10000) / 10000;

        this.TR_X = Math.Truncate((XSampleCenterLocation + XDiePitch * (1 + XSampleTestPlan)) * 10000) / 10000;
        this.TR_Y = Math.Truncate((YSampleCenterLocation + YDiePitch * (1 + YSampleTestPlan)) * 10000) / 10000;

        this.BR_X = Math.Truncate((XSampleCenterLocation + XDiePitch * (1 + XSampleTestPlan)) * 10000) / 10000;
        this.BR_Y = Math.Truncate((YSampleCenterLocation + YDiePitch * YSampleTestPlan) * 10000) / 10000; 
    }



    

}