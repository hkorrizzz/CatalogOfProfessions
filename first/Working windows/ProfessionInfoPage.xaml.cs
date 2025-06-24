

namespace first;

public partial class ProfessionInfoPage : ContentPage
{
	public ProfessionInfoPage(ListOfProfessions info)
	{
		InitializeComponent();
		BindingContext = info;
      
    }
}