using System.Windows;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();

    }


    private void Jallooo_MouseUp(object sender, MouseButtonEventArgs e)
    {
        Test.Text = "test";
    }
}
