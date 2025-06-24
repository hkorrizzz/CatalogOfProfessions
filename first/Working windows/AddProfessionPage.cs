using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace first
{
    public class AddProfessionPage : ContentPage 
    {
        private Entry professionEntry;
        private Editor informationEditor;
        private int indexType = -1;
        private Catalog catalog;

        public AddProfessionPage(Catalog _catalog)
        {
            catalog = _catalog;
            Title = "Добавить профессию";

            StackLayout stackLayout = new StackLayout { Padding = 50, Spacing = 20 };

            Picker picker = new Picker { Title = "Выберите классификацию профессии:", TitleColor = Colors.White, FontSize = 20 };
            foreach (Type type in catalog.Type.ToList())
            {
                picker.Items.Add(type.Name);
            }

            picker.SelectedIndexChanged += PickerSelectedIndexChanged;

            stackLayout.Children.Add(picker);

            Label professionLabel = new Label
            {
                Text = "Название профессии:",
                FontSize = 20
            };

            professionEntry = new Entry
            {
                Placeholder = "Введите название профессии",
                FontSize = 20
            };

            Label informationLabel = new Label
            {
                Text = "Описание профессии:",
                FontSize = 20
            };

            informationEditor = new Editor
            {
                Placeholder = "Введите описание профессии",
                FontSize = 20,
                HeightRequest = 300,
                
            };

            Button addButton = new Button
            {
                Text = "Добавить",
                TextColor = Colors.Black,
                FontSize = 20,
                VerticalOptions = LayoutOptions.End
            };

            addButton.Clicked += AddButtonClicked;

            stackLayout.Children.Add(professionLabel);
            stackLayout.Children.Add(professionEntry);
            stackLayout.Children.Add(informationLabel);
            stackLayout.Children.Add(informationEditor);
            stackLayout.Children.Add(addButton);

            Content = stackLayout;
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            indexType = picker.SelectedIndex;
        }

        private async void AddButtonClicked(object sender, EventArgs e)
        {
            string profession = professionEntry.Text;
            string information = informationEditor.Text;

            if (!string.IsNullOrEmpty(profession) && !string.IsNullOrEmpty(information))
            {
                ListOfProfessions newProfession = new ListOfProfessions
                {
                    Profession = profession,
                    Information = information
                };

                if (indexType >= 0 && indexType < catalog.Type.Count)
                {
                    catalog.Type[indexType].AddProfession(newProfession);
                    catalog.SerializeToXml("C:\\Users\\sanyh\\source\\repos\\first\\first\\FileHandler\\DataXML.xml");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Выберите корректный тип профессии", "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "OK");
            }
        }
    }
}
