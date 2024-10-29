namespace Counter.Views
{
    public partial class CounterPage : ContentPage
    {
        public CounterPage()
        {
            InitializeComponent();
            BindingContext = new Models.Count();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is Models.Count count)
            {
                // Sprawdza czy licznik ma nazwe
                if (string.IsNullOrWhiteSpace(count.Title))
                {
                    
                    await DisplayAlert("ERROR", "Enter counter title", "OK");
                    return;
                }

                // Zapis do pliku
                var fileName = Path.Combine(FileSystem.AppDataDirectory, $"{Guid.NewGuid()}.counter.txt");
                count.Filename = fileName;
                File.WriteAllText(fileName, $"{count.Title}|{count.Value}");

                
                await Shell.Current.GoToAsync("//AllCounterPage");
            }
        }
    }
}