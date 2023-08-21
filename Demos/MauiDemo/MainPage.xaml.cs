using WpfDemo;

namespace MauiDemo;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();

        var store = App.Services.GetRequiredService<MainWindowStore>();

        entry1.Text = store.Input1;
        entry2.Text = store.Input2;

        entry1.TextChanged += (sender, args) =>
        {
            store.Input1 = entry1.Text;
        };

        entry2.TextChanged += (sender, args) =>
        {
            store.Input2 = entry2.Text;
        };
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

