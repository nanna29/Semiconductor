using Microsoft.Win32;
using Semiconductor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Collections;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Drawing;
using Image = System.Windows.Controls.Image;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;

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

        // ButtonCommand 의 인스턴스 속성인 DisplayPathCommand 속성을 선언
        public ButtonCommand DisplayPathCommand { get; private set; }

        // ViewModel 생성자에는 DisplayPathCommand 인스턴스를 ButtonCommand로 할당할 때
        // 수행할 DisplayPath() 함수 입력
        public ViewModel()
        {
            DisplayPathCommand = new ButtonCommand(DisplayPath);

            /*
            DieLists = new ObservableCollection<Die>
            {
                    new Die
                    { XSampleCenterLocation =1,
                        YSampleCenterLocation =1,
                        XDiePitch =1,
                        YDiePitch =1,
                        XSampleTestPlan = 1,
                        YSampleTestPlan =1
                    }
            };*/
            //AddDie();
        }

        
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


                WaferID = wafer.WaferID;
                FileTimestamp = wafer.FileTimestamp;
                ResultTimestamp = wafer.ResultTimestamp;




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

            foreach (Die die in wafer.GetDieList())
            {
                //윈도우 좌표로 변환
                nTL_X = ((int)die.TL_X / 250) + 400 - 11;
                nTL_Y = (800 - (int)(die.TL_Y / 250)) - 400 + 82;
                nBR_X = ((int)die.BR_X / 250) + 400 - 11;
                nBR_Y = (800 - (int)(die.BR_Y / 250)) - 400 + 82;
 
                writeableBmp.DrawRectangle(nTL_X, nTL_Y, nBR_X, nBR_Y, Colors.Black);
            }

            foreach (Defect defect in wafer.GetDefectList())
            {
                //윈도우 좌표로 변환
                nTL_X = (int)defect.BL_X / 250 + 400 - 11;
                nTL_Y = -(int)defect.BL_Y / 250 + 400 + 82;

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

        public List<Defect> AddDefect(Wafer wafer)
        {

            List<Defect> DefectLists = new List<Defect>();
            for (int i = 0; i < wafer.GetDefectList().Count; i++)
            {
                DefectLists.Add(new Defect
                {

                    DEFECTID = wafer.DefectAt(i).DEFECTID,
                    XREL = wafer.DefectAt(i).XREL,
                    YREL = wafer.DefectAt(i).YREL,
                    XINDEX = wafer.DefectAt(i).XINDEX,
                    YINDEX = wafer.DefectAt(i).YINDEX,

                });

            }
            return DefectLists;
        }

    }
}
