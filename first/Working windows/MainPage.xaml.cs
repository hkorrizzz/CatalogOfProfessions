using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Maui.Controls;


namespace first
{
    public partial class MainPage : ContentPage
    {
        Catalog catalog;
        private List<Type> _filteredTypes; 
        public MainPage()
        {
            InitializeComponent();

            catalog = Catalog.DeserializeFromXml("C:\\Users\\sanyh\\source\\repos\\first\\first\\FileHandler\\DataXML.xml");
            BackgroundImageProfession();
            _filteredTypes = new List<Type>(catalog.Type.ToList()); 
            PopulateButtons();
        }


        private void BackgroundImageProfession()
        {
            foreach (Type prof in catalog.Type)
            {
                foreach (ListOfProfessions professions in prof.ProfessionsList)
                {
                    professions.Image = prof.backgroundImage;
                }
            }
        }

        private void PopulateButtons()
        {
            TypeGrid.Children.Clear();
            TypeGrid.RowDefinitions.Clear();

            int row = 0;
            int column = 0;
            int numberOfRows = (_filteredTypes.Count + 2) / 3;
            for (int i = 0; i < numberOfRows; i++)
            {
                TypeGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(180) });
            }

            foreach (Type type in _filteredTypes)
            {
                Button button = new Button
                {
                    ImageSource = new FileImageSource { File = type.ImageUrl },
                    ContentLayout = new Button.ButtonContentLayout(Button.ButtonContentLayout.ImagePosition.Bottom, 10),
                    BackgroundColor = Colors.Transparent,
                    Command = new Command(() => NavigateToDetailPage(type)),
                };

                TypeGrid.Add(button);
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);

                column++;
                if (column > 2)
                {
                    column = 0;
                    row++;
                }
            }
        }
        private async void NavigateToDetailPage(Type profession)
        {
            await Navigation.PushAsync(new ProfessionDetailPage(profession));
        }

        private async void AllProfessionsSearch(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AllProfessionsPage(catalog.allProfessions()));
        }
    

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string userInput = searchBar.Text;
            List<Type> matchingTypes = catalog.SearchTypes(userInput);

            _filteredTypes = matchingTypes;
            PopulateButtons();

            if (_filteredTypes.Count == 0)
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
            List<Type> matchingTypes = new List<Type>();

            switch (countClicksSorting)
            {
                case 0:
                    sortButton.Text = "Отсортировать от Я до А";
                    sortButton.BorderColor = Colors.Goldenrod;
                    matchingTypes = catalog.SortDirectOrder();
                    countClicksSorting = 1;
                    break;

                case 1:
                    sortButton.Text = "Отменить сортировку";
                    sortButton.BorderColor = Colors.Goldenrod;
                    matchingTypes = catalog.SortReverseOrder();

                    countClicksSorting = 2;
                    break;

                case 2:
                    sortButton.Text = "Отсортировать от А до Я";
                    sortButton.BorderColor = Colors.White;
                    matchingTypes = catalog.Type.ToList();
                    countClicksSorting = 0;
                    break;
            }
            _filteredTypes = matchingTypes;
            PopulateButtons();
        }


        private async void AddProfessionButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProfessionPage( catalog));
        }

        private async void DeleteProfessionButton(object sender, EventArgs e)
        {
            List <string> profs = new List<string>();
            foreach (ListOfProfessions prof in catalog.allProfessions()) { profs.Add(prof.Profession); }

            string action = await DisplayActionSheet("Выберите профессию: ", "Отмена", null, profs.ToArray());

            if (profs.Contains(action)) {
                bool confirm = await DisplayAlert("Подтверждение", $"Вы точно хотите удалить профессию '{action}'?", "Да", "Нет");

                if (confirm)
                {
                    catalog.DeleteProfession(action); 
                    catalog.SerializeToXml("C:\\Users\\sanyh\\source\\repos\\first\\first\\FileHandler\\DataXML.xml");
                    await DisplayAlert("Успех", $"Профессия '{action}' была успешно удалена.", "ОК");
                }
                else
                {
                    await DisplayAlert("Отменено", $"Удаление профессии '{action}' отменено.", "ОК");
                }
            }

        }
        
    }
}
