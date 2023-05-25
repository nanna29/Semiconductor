using Semiconductor;

public class Defect
{
    public int DEFECTID { get; set; } = 0; //defect id

    public double XREL { get; set; } = 0; //찾은 다이 내의 결함의 x좌표
    public double YREL { get; set; } = 0; //찾은 다이 내의 결함의 x좌표
    public int XINDEX { get; set; } = 0; //결함 있는 다이의 좌표
    public int YINDEX { get; set; } = 0; //결함 있는 다이의 좌표

    public double BL_X { get; set; } = 0; //실제 좌표계에서 X위치
    public double BL_Y { get; set; } = 0; //실제 좌표계에서 Y위치


    public Defect() { }
    //인수 있는 생성자
    public Defect(double XREL, double YREL, int XINDEX, int YINDEX, double DieBL_X, double DieBL_Y)
    {
        this.XREL = XREL;
        this.YREL = YREL;

        this.XINDEX = XINDEX;
        this.YINDEX = YINDEX;

        this.BL_X = DieBL_X + XREL; //다이의 실제 좌표값(좌측하단) + 다이 내의 결함의 x좌표 = 실제 좌표계 값
        this.BL_Y = DieBL_Y + YREL; //다이의 실제 좌표값(좌측하단) + 다이 내의 결함의 y좌표 = 실제 좌표계 값
    }
}