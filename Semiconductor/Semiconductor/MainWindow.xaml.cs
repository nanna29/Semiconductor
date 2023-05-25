using System.Data;
using System.Windows;

namespace Semiconductor
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }
        /*
        private void VisibleCheck_Checked(object sender, RoutedEventArgs e)
        {
            image.Width = 800;
            image.Height = 1000;
        }*/
    }
   
}
    


