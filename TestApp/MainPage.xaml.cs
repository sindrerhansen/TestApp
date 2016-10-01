using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int teller = 0;
        MainViewModel mvm = new MainViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = mvm;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.Content = "I am pressed "+teller.ToString()+" times";
            teller++;
            mvm.Tekst = teller.ToString();
        }
    }
}
