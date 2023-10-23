using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoriesBloom.ViewModels
{
    public partial class StoriesCategoryViewModel : BaseViewModel
    {
        public ICommand ClearCommand { private set; get; }

        [ObservableProperty]
        ObservableCollection<string> categories;

        public StoriesCategoryViewModel()
        {
            categories = new ObservableCollection<string>()
            {
                "dsa",
                "fdsfe"
            };

            ClearCommand = new Command(
                execute: () =>
                {
                    Console.WriteLine("dsadsa");
                });
        }

        [RelayCommand]
        private async void GoToDetails()
        {
            await Shell.Current.GoToAsync(nameof(StoriesViewModel), true, new Dictionary<string, object>
            {
                { "Item", null }
            });
        }
    }
}
