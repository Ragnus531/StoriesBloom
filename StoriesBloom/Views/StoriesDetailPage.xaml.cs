using CommunityToolkit.Maui.Views;

namespace StoriesBloom.Views;

public partial class StoriesDetailPage : ContentPage
{
	public StoriesDetailPage(StoriesDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Expander_ExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
		Expander exp = sender as Expander;
		Image img = null;

		if(exp == prologueExpander)
		{
			img = arrowImagePrologue;
        }
		else if(exp == chapter1Expander)
		{
            img = arrowImageChapter1;
        }
        else if (exp == chapter2Expander)
        {
            img = arrowImageChapter2;
        }
        else if (exp == chapter3Expander)
        {
            img = arrowImageChapter3;
        }
        else if (exp == chapter4Expander)
        {
            img = arrowImageChapter4;
        }
        else if (exp == chapter5Expander)
        {
            img = arrowImageChapter5;
        }
        else if (exp == epilogueExpander)
        {
            img = arrowImageEpilogue;
        }
        else if (exp == twistExpander)
        {
            img = arrowImageTwist;
        }


        //var ele = (Image)exp.FindByName("arrowImage");
        if (exp.IsExpanded)
		{
            img.Source = ImageSource.FromFile("arrow_down.svg");
        }
		else
		{
            img.Source = ImageSource.FromFile("arrow_up.svg");
        }
    }
}
