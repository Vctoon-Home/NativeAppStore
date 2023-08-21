using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowStore store = App.Services.GetRequiredService<MainWindowStore>();

            textbox1.Text = store.Input1;
            textbox2.Text = store.Input2;

            textbox1.TextChanged += (sender, args) =>
            {
                store.Input1 = textbox1.Text;
            };

            textbox2.TextChanged += (sender, args) =>
            {
                store.Input2 = textbox2.Text;
            };
        }
    }
}