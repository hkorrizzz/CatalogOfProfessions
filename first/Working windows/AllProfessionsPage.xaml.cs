namespace first
{

    public partial class AllProfessionsPage : ContentPage
    {
        private List<ListOfProfessions> professions;
        private List<ListOfProfessions> filteredProfessions;
        public AllProfessionsPage(List<ListOfProfessions> profession)
        {
            InitializeComponent();

            professions = profession;
            filteredProfessions = professions;
            PopulateButtons();
        }

        private void PopulateButtons()
        {
            ProfessionGrid.Children.Clear();
            ProfessionGrid.RowDefinitions.Clear(); 

            int row = 0;
            int column = 0;
            int numberOfRows = (filteredProfessions.Count + 3) / 4;
            for (int i = 0; i < numberOfRows; i++)
            {
                ProfessionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(180) });
            }

            foreach (ListOfProfessions type in filteredProfessions)
            {
                Frame buttonFrame = new Frame
                {
                    BackgroundColor = Colors.White,
                    Padding = 10,
                    Content = new Label
                    {
                        Text = type.Profession,
                        TextColor = Colors.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 21,
                        LineBreakMode = LineBreakMode.CharacterWrap,
                        MaxLines = 4,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                    }
                };

                buttonFrame.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => NavigateToInfoPage(type))
                });

                ProfessionGrid.Children.Add(buttonFrame);
                Grid.SetColumn(buttonFrame, column);
                Grid.SetRow(buttonFrame, row);

                column++;
                if (column > 3)
                {
                    column = 0;
                    row++;
                }
            }
        }
        private async void NavigateToInfoPage(ListOfProfessions profession)
        {
            await Navigation.PushAsync(new ProfessionInfoPage(profession));
        }   

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string userInput = searchBar.Text;
            string searchText = userInput.ToLower();

            List<ListOfProfessions> matchingProfessions = new List<ListOfProfessions>();

            foreach (ListOfProfessions profession in professions)
            {
                if (profession.SearchAllFrof(searchText))
                {
                    matchingProfessions.Add(profession);
                }
            }

            filteredProfessions = matchingProfessions;
            PopulateButtons();

            if (filteredProfessions.Count == 0)
            {
                noResultsLabel.IsVisible = true;
            }
            else
            {
                noResultsLabel.IsVisible = false;
            }
        }

        private int countClicksSorting = 0;
        private void SortedProfessions(object sender, EventArgs e)
        {
            List<ListOfProfessions> matchingProfessions = new List<ListOfProfessions>();

            switch (countClicksSorting)
            {
                case 0:
                    sortButton.Text = "Отсортировать от Я до А";
                    sortButton.BorderColor = Colors.Goldenrod;

                    matchingProfessions = ListOfProfessions.AllSortDirectOrder(professions);
                    foreach (ListOfProfessions type in professions.OrderBy(a => a.Profession))
                    {
                        matchingProfessions.Add(type);
                    }
                    countClicksSorting = 1;
                    break;

                case 1:
                    sortButton.Text = "Отменить сортировку";
                    sortButton.BorderColor = Colors.Goldenrod;
                    sortButton.BorderColor = Colors.Goldenrod;

                    matchingProfessions = ListOfProfessions.AllSortReverseOrder(professions);

                    countClicksSorting = 2;
                    break;

                case 2:
                    sortButton.Text = "Отсортировать от А до Я";
                    sortButton.BorderColor = Colors.White;
                    foreach (ListOfProfessions type in professions)
                    {
                        matchingProfessions.Add(type);
                    }
                    countClicksSorting = 0;
                    break;
            }
            filteredProfessions = matchingProfessions;
            PopulateButtons();
        }
    }
}