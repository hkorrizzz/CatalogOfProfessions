using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace first
{
    [Serializable]
    public class ListOfProfessions
    {
        private string profession;
        [XmlElement("Profession")]
        public string Profession
        {
            get { return profession; }
            set { if (value != String.Empty) profession = value; }
        }

        private string information;
        [XmlElement("Information")]
        public string Information
        {
            get { return information; }
            set { if (value != String.Empty) information = value; }
        }

        public string image;
        public string Image
        {
            get { return image; }
            set { if (value.EndsWith(".bmp") || value.EndsWith(".png") || value.EndsWith(".jpg")) image = value; }
        }
        public ListOfProfessions() { }

        public bool SearchAllFrof(string searchText) {
            
            if (this.Profession.ToLower().Contains(searchText))
            {
                return true;
            }
            return false;
        }

        public static List<ListOfProfessions> AllSortReverseOrder(List<ListOfProfessions> professions)
        {
            return professions.OrderByDescending(p => p.Profession).ToList();
        }

        public static List<ListOfProfessions> AllSortDirectOrder(List<ListOfProfessions> professions)
        {
            return professions.OrderBy(a => a.Profession).ToList();
        }
    }
}
