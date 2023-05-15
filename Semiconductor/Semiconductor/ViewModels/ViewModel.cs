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
namespace Semiconductor
{


    
    public class ViewModel : Notifier
    {
        
        Grid grid = new Grid();
        Rectangle rectangle = new Rectangle();

        Wafer wafer = new Wafer();

        //파일 선택시, 해당 파일 텍스트 박스에 넣는 코드 (command binding 이용)

        //xaml textbox 텍스트 속성 Binding
        string pathText;    
        public string PathText
        {
            get { return pathText; }
            set
            {
                pathText = value;
                OnPropertyChanged("PathText");

            }
        }




        // ButtonCommand 의 인스턴스 속성인 DisplayPathCommand 속성을 선언
        public ButtonCommand DisplayPathCommand { get; private set; }

        // ViewModel 생성자에는 DisplayPathCommand 인스턴스를 ButtonCommand로 할당할 때
        // 수행할 DisplayPath 함수 입력
        public ViewModel()
        {
            DisplayPathCommand = new ButtonCommand(DisplayPath);
        }

        //파일 열기 동작하는 코드
        public void DisplayPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                PathText = openFileDialog.FileName;
                //해당 경로로 데이터 파싱 진행
                Parse ui = new Parse();
                Wafer wafer= ui.parse(PathText);

                coordinate(wafer);     
            }

        }


        private int nTL_X;

        public int NTL_X
        {

            get { return nTL_X; }
            set
            {
                nTL_X = value;
                OnPropertyChanged("NTL_X");
            }
        }

        private int nTL_Y;

        public int NTL_Y
        {
            get { return nTL_Y; }
            set
            {
                nTL_Y = value;
                OnPropertyChanged("NTL_Y");
            }
        }

        private int nBR_X;

        public int NBR_X
        {
            get { return nBR_X; }
            set
            {
                nBR_X = value;
                OnPropertyChanged("NBR_X");
            }
        }

        private int nBR_Y;

        public int NBR_Y
        {
            get { return nBR_Y; }
            set
            {
                nBR_Y = value;
                OnPropertyChanged("NBR_Y");
            }
        }

        public BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
        {
            BitmapImage bmImage = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(wbm));
                encoder.Save(stream);
                bmImage.BeginInit();
                bmImage.CacheOption = BitmapCacheOption.OnLoad;
                bmImage.StreamSource = stream;
                bmImage.EndInit();
                bmImage.Freeze();
            }
            return bmImage;
        }

        private Image WaferImage;
        public Image waferImage
        {
            get
            {
                return WaferImage;
            }
            set
            {
                WaferImage = value;
                OnPropertyChanged("waferImage");
            }
        }

        public void coordinate(Wafer wafer)
        {

            WriteableBitmap writeableBmp = BitmapFactory.New(800, 800);
            writeableBmp.Clear(Colors.White);
            writeableBmp.FillEllipseCentered(400, 400, 400, 400, Colors.Gray);
            WaferImage = new Image();

            foreach (Die die in wafer.GetDieList())
            {
                //윈도우 좌표로 변환
                nTL_X = ((int)die.TL_X / 250) + 400 - 11;
                nTL_Y = (800 - (int)(die.TL_Y / 250)) - 400 + 82;
                nBR_X = ((int)die.BR_X / 250) + 400 - 11;
                nBR_Y = (800 - (int)(die.BR_Y / 250)) - 400 + 82;


                writeableBmp.DrawRectangle(nTL_X, nTL_Y, nBR_X, nBR_Y, Colors.Black);
                //BitmapImage bitmap = ConvertWriteableBitmapToBitmapImage(writeableBmp);
                
                WaferImage.Source = writeableBmp;
            }

            foreach (Defect defect in wafer.GetDefectList())
            {
                //추가 좌표이동 X
                nTL_X = (int)defect.BL_X / 250 + 400 - 11;
                nTL_Y = -(int)defect.BL_Y / 250 + 400 + 82;

            }
        }



    }
}
