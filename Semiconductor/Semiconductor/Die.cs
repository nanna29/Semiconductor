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

    //인수 있는 생성자
    public Die(int XSampleTestPlan, int YSampleTestPlan)
    {
        this.XSampleTestPlan = XSampleTestPlan;
        this.YSampleTestPlan = YSampleTestPlan;
    }
}