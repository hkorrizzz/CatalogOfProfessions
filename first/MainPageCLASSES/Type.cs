using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace first
{

    [Serializable]
    public class Type
    {
        private string name;
        [XmlElement("Name")]
        public string Name
        {
            get { return name; }
            set { if (value != String.Empty) name = value; } 
        }

        private string description;
        [XmlElement("Description")]
        public string Description
        {
            get { return description; }
            set { if (value != String.Empty) description = value; }
        }

        private string imageUrl;
        [XmlElement("Image")]
        public string ImageUrl
        {
            get { return imageUrl; }
            set { if (value.EndsWith(".bmp") || value.EndsWith(".png") || value.EndsWith(".jpg")) imageUrl = value;  }
        }

        private string BackgroundImage;
        [XmlElement("backgroundImage")]
        public string backgroundImage
        {
            get { return BackgroundImage; }
            set { if (value.EndsWith(".bmp") || value.EndsWith(".png") || value.EndsWith(".jpg")) BackgroundImage = value; }
        }

        [XmlElement("Professions")]
        public List<ListOfProfessions> ProfessionsList = new List<ListOfProfessions>();

        public Type() { }

        public void AddProfession (ListOfProfessions profession)
        {
            this.ProfessionsList.Add(profession);
        }
        public List<ListOfProfessions> SearchProfessions(string searchText)
        {
            searchText = searchText.ToLower();

            List<ListOfProfessions> matchingTypes = new List<ListOfProfessions>();

            foreach (ListOfProfessions prof in ProfessionsList)
            {
                if (prof.Profession.ToLower().Contains(searchText))
                {
                    matchingTypes.Add(prof);
                }
            }
            return matchingTypes;
        }

        public List<ListOfProfessions> ProfessionSortDirectOrder()
        {
            return ProfessionsList.OrderBy(a => a.Profession).ToList();
        }

        public List<ListOfProfessions> ProfessionSortReverseOrder()
        {
            return ProfessionsList.OrderByDescending(a => a.Profession).ToList();
        }

    }
}
