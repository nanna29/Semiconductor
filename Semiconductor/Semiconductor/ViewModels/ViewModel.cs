using Microsoft.Win32;
using Semiconductor.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Semiconductor
{
    public class ViewModel : Notifier
    {
        //파일 선택시, 해당 파일 텍스트 박스에 넣는 코드 (command binding 이용)

        //xaml textbox 텍스트 속성 Binding
        private string pathText;
        public string PathText
        {
            get { return pathText; }
            set
            {
                pathText = value;
                OnPropertyChanged("PathText");

            }
        }

        //Wafer 정보 바인딩
        private string waferID;
        public string WaferID
        {
            get { return waferID; }
            set
            {
                waferID = value;
                OnPropertyChanged("WaferID");

            }
        }

        private DateTime fileTimestamp;
        public DateTime FileTimestamp
        {
            get { return fileTimestamp; }
            set
            {
                fileTimestamp = value;
                OnPropertyChanged("FileTimestamp");

            }
        }

        private DateTime resultTimestamp;
        public DateTime ResultTimestamp
        {
            get { return resultTimestamp; }
            set
            {
                resultTimestamp = value;
                OnPropertyChanged("ResultTimestamp");

            }
        }

        //xaml image source 속성 Binding
        private WriteableBitmap wBmp;
        public WriteableBitmap WBmp2
        {
            get
            { return wBmp; }
            set
            {
                wBmp = value;
                OnPropertyChanged("WBmp2");
            }
        }

        //defect datagird 클릭시
        private Defect selectedDefect;
        public Defect SelectedDefect
        {
            get { return selectedDefect; }
            set
            {
                selectedDefect = value;
                OnPropertyChanged("SelectedDefect");
            }
        }

        // ButtonCommand 의 인스턴스 속성인 DisplayPathCommand 속성을 선언
        public ButtonCommand DisplayPathCommand { get; private set; }

        // ViewModel 생성자에는 DisplayPathCommand 인스턴스를 ButtonCommand로 할당할 때
        // 수행할 DisplayPath() 함수 입력
        public ViewModel()
        {
            DisplayPathCommand = new ButtonCommand(DisplayPath);
        }

        //datagird binding 시킬 리스트
        private List<Die> dieLists;
        public List<Die> DieLists
        {
            get
            { return dieLists; }
            set
            {
                dieLists = value;
                OnPropertyChanged("DieLists");
            }
        }

        //datagird binding 시킬 리스트
        private List<Defect> defectLists;
        public List<Defect> DefectLists
        {
            get
            { return defectLists; }
            set
            {
                defectLists = value;
                OnPropertyChanged("DefectLists");
            }
        }

        public void DisplayPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                //해당 경로(PathText)로 데이터 파싱 진행
                PathText = openFileDialog.FileName;
                Parse ui = new Parse();

                //파싱 진행 후, 윈도우 좌표 수정 위해 wafer return 받음
                //ui.parse() 함수가 wafer 리턴
                Wafer wafer = ui.parse(PathText);

                //Coordinate 함수: 그려진 WriteableBitmap return 함
                //WBmp2에 Coordinate 에서 return 받은 WriteableBitmap 넣어서 값 갱신시킴 (image source Binding)
                WBmp2 = Coordinate(wafer);

                //wafer info return
                WaferID = wafer.WaferID;
                FileTimestamp = wafer.FileTimestamp;
                ResultTimestamp = wafer.ResultTimestamp;

                //리스트 return
                DieLists = AddDie(wafer);
                DefectLists = AddDefect(wafer);
            }

        }

        public int nTL_X { get; set; } = 0;
        public int nTL_Y { get; set; } = 0;
        public int nBR_X { get; set; } = 0;
        public int nBR_Y { get; set; } = 0;

        //윈도우 좌표계로 전환 + WriteableBitmap 그리는 함수
        public WriteableBitmap Coordinate(Wafer wafer)
        {
            WriteableBitmap writeableBmp = BitmapFactory.New(800, 800);
            writeableBmp.Clear(Colors.White);
            writeableBmp.FillEllipseCentered(400, 400, 400, 400, Colors.Gray);

            //보정 좌표 계산 (0,0 다이 기준)
            int XDiepitch = (int)wafer.DieAt(0).XDiePitch / (wafer.SampleSize * 1000 / 800);
            int YDiepitch = (int)wafer.DieAt(0).YDiePitch / (wafer.SampleSize * 1000 / 800);

            foreach (Die die in wafer.GetDieList())
            {
                //윈도우 좌표로 변환
                nTL_X = ((int)die.TL_X / 250) + 400 - XDiepitch;
                nTL_Y = (800 - (int)(die.TL_Y / 250)) - 400 + YDiepitch;
                nBR_X = ((int)die.BR_X / 250) + 400 - XDiepitch;
                nBR_Y = (800 - (int)(die.BR_Y / 250)) - 400 + YDiepitch;

                writeableBmp.DrawRectangle(nTL_X, nTL_Y, nBR_X, nBR_Y, Colors.Black);
            }

            foreach (Defect defect in wafer.GetDefectList())
            {
                //윈도우 좌표로 변환
                nTL_X = (int)defect.BL_X / 250 + 400 - XDiepitch;
                nTL_Y = -(int)defect.BL_Y / 250 + 400 + YDiepitch;

                writeableBmp.DrawRectangle(nTL_X, nTL_Y, nTL_X + 3, nTL_Y + 3, Colors.Red);
            }


            /*
            //png 파일로 저장하기
            void CreateThumbnail(string filename, BitmapSource image5)
            {
                if (filename != string.Empty)
                {
                   using (FileStream stream5 = new FileStream(filename, FileMode.Create))
                   {
                       PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                       encoder5.Frames.Add(BitmapFrame.Create(image5));
                       encoder5.Save(stream5);
                   }
                }
            }
            CreateThumbnail("result.png", writeableBmp.Clone());*/



            //그려진 WriteableBitmap return
            return writeableBmp;
        }


        //datagrid에 die info 전달위한 리스트 작성
        public List<Die> AddDie(Wafer wafer)
        {

            List<Die> DieLists = new List<Die>();
            for (int i = 0; i < wafer.GetDieList().Count; i++)
            {
                DieLists.Add(new Die
                {

                    XSampleCenterLocation = wafer.DieAt(i).XSampleCenterLocation,
                    YSampleCenterLocation = wafer.DieAt(i).YSampleCenterLocation,
                    XDiePitch = wafer.DieAt(i).XDiePitch,
                    YDiePitch = wafer.DieAt(i).YDiePitch,
                    XSampleTestPlan = wafer.DieAt(i).XSampleTestPlan,
                    YSampleTestPlan = wafer.DieAt(i).YSampleTestPlan

                });

            }
            return DieLists;
        }

        //datagrid에 defect info 전달위한 리스트 작성
        public List<Defect> AddDefect(Wafer wafer)
        {
            List<Defect> DefectLists = new List<Defect>();

            //보정 좌표 계산 (0,0 다이 기준)
            int XDiepitch = (int)wafer.DieAt(0).XDiePitch / (wafer.SampleSize * 1000 / 800);
            int YDiepitch = (int)wafer.DieAt(0).YDiePitch / (wafer.SampleSize * 1000 / 800);
            
            for (int i = 0; i < wafer.GetDefectList().Count; i++)
            {
                DefectLists.Add(new Defect
                {
                    DEFECTID = wafer.DefectAt(i).DEFECTID,
                    XREL = wafer.DefectAt(i).XREL,
                    YREL = wafer.DefectAt(i).YREL,
                    XINDEX = wafer.DefectAt(i).XINDEX,
                    YINDEX = wafer.DefectAt(i).YINDEX,
                    BL_X = ((int)wafer.DefectAt(i).BL_X / 250) + 400 - XDiepitch,
                    BL_Y = -(int)wafer.DefectAt(i).BL_Y / 250 + 400 + YDiepitch

                });
            }
            return DefectLists;
        }

    }
}
