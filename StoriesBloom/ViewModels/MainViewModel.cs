﻿namespace StoriesBloom.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    readonly StoryDataService _dataService;


    [ObservableProperty]
    ObservableCollection<StoryDetail> items;

    [ObservableProperty]
    ObservableCollection<StoryInfo> storiesInfo;

    [ObservableProperty]
    ObservableCollection<StoryInfo> storiesInfo2;

    public MainViewModel(StoryDataService dataService)
    {
        _dataService= dataService;
        InitializeTales();

    }

    private void InitializeTales()
    {
        StoriesInfo = new ObservableCollection<StoryInfo>
               {
                   new StoryInfo { Name = "Cinderella", ReadTime = new TimeSpan(0, 30, 0), Image = "cinderella.jpg" },
                   new StoryInfo { Name = "Snow White", ReadTime = new TimeSpan(0, 25, 0),  Image = "snow.jpg" },
                   new StoryInfo { Name = "Rapunzel", ReadTime = new TimeSpan(0, 20, 0),  Image = "rapunzel.jpg" },
                   new StoryInfo { Name = "Little Red Riding Hood", ReadTime = new TimeSpan(0, 15, 0),  Image = "hood.jpg" },
                   new StoryInfo { Name = "Beauty and the Beast", ReadTime = new TimeSpan(0, 35, 0),  Image = "beauty.jpg" }
               };
        StoriesInfo2 = new ObservableCollection<StoryInfo>
               {
                   new StoryInfo { Name = "Snow White", ReadTime = new TimeSpan(0, 25, 0),  Image = "snow.jpg" },
                   new StoryInfo { Name = "Rapunzel", ReadTime = new TimeSpan(0, 20, 0),  Image = "rapunzel.jpg" },
                   new StoryInfo { Name = "Little Red Riding Hood", ReadTime = new TimeSpan(0, 15, 0),  Image = "hood.jpg" },
                   new StoryInfo { Name = "Beauty and the Beast", ReadTime = new TimeSpan(0, 35, 0),  Image = "beauty.jpg" },
                   new StoryInfo { Name = "Cinderella", ReadTime = new TimeSpan(0, 30, 0),  Image = "cinderella.jpg" }
               };

    }

    [RelayCommand]
    public async Task LoadMore()
    {
        var items = await _dataService.GetStories();

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    public async Task LoadDataAsync()
    {
        Items = new ObservableCollection<StoryDetail>(await _dataService.GetStories());
    }


    [RelayCommand]
    private async void GoToDetails(StoryDetail item)
    {
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", item }
        });
    }
}
