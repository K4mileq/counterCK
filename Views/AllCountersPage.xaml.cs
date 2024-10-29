namespace Counter.Views
{
    public partial class AllCountersPage : ContentPage
    {
        public System.Collections.ObjectModel.ObservableCollection<Models.Count> Counters { get; set; } = new();

        public AllCountersPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCounters();
        }

        private void LoadCounters()
        {
            // £adowanie zapisanych liczników z plików
            Counters.Clear();
            var files = Directory.GetFiles(FileSystem.AppDataDirectory, "*.counter.txt");
            foreach (var file in files)
            {
                var data = File.ReadAllText(file).Split('|');
                if (data.Length == 2 && int.TryParse(data[1], out int value))
                {
                    Counters.Add(new Models.Count
                    {
                        Title = data[0],
                        Value = value,
                        Filename = file
                    });
                }
            }
        }

        private void IncrementButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var count = button.BindingContext as Models.Count;
            count.Value++;
            AutoSaveCount(count);
        }

        private void DecrementButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var count = button.BindingContext as Models.Count;
            count.Value--;
            AutoSaveCount(count);
        }

        private void AutoSaveCount(Models.Count count)
        {
            // Automatyczny zapis
            File.WriteAllText(count.Filename, $"{count.Title}|{count.Value}");
        }

        //private void SaveButton_Clicked(object sender, EventArgs e)
        //{
        //    var button = sender as Button;
        //    var count = button.BindingContext as Models.Count;
        //    File.WriteAllText(count.Filename, $"{count.Title}|{count.Value}|{count.BackgroundColor}");
        //}

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var count = button.BindingContext as Models.Count;

            // Usuñ licznik
            if (File.Exists(count.Filename))
            {
                File.Delete(count.Filename);
                Counters.Remove(count);
            }
        }

        private async void AddCounter_Clicked(object sender, EventArgs e)
        {

            await Shell.Current.GoToAsync("///CounterPage");
        }
    }
}