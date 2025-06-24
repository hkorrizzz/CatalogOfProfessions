
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace first
{
    [Serializable]
    [XmlRoot("Catalog")]
    public class Catalog
    {
        [XmlElement("Type")]
        public List<Type> Type = new List<Type>();
        public Catalog() {

            
        }
        public static Catalog DeserializeFromXml(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Catalog));
            using (StreamReader dataStream = new StreamReader(filePath))
            {
                return (Catalog)xmlSerializer.Deserialize(dataStream);
            }
        }

        public void SerializeToXml(string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Catalog));

            using (StreamWriter dataStream = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(dataStream, this);
            }
        }

        public List<ListOfProfessions> allProfessions()
        {
            List <ListOfProfessions> allProfessionsList = new List <ListOfProfessions>();
            foreach (Type type in Type)
            {
                foreach (ListOfProfessions profession in type.ProfessionsList ) { 
                    allProfessionsList.Add(profession);
                }
            }
            return allProfessionsList;
        } 

        public void DeleteProfession(string action)
        {
            foreach (Type type in Type)
            {
                for (int i = type.ProfessionsList.Count - 1; i >= 0; i--)
                {
                    if (type.ProfessionsList[i].Profession == action)
                    {
                        type.ProfessionsList.RemoveAt(i); 

                    }
                }
            }
        }

        public List<Type> SortDirectOrder()
        {
           return Type.OrderBy(a => a.Name).ToList();
        }

        public List<Type> SortReverseOrder()
        {
            return Type.OrderByDescending(a => a.Name).ToList();
        }
        public List<Type> SearchTypes(string searchText)
        {
            searchText = searchText.ToLower();

            List<Type> matchingTypes = new List<Type>();

            foreach (Type type in Type)
            {
                if (type.Name.ToLower().Contains(searchText))
                {
                    matchingTypes.Add(type);
                }
            }

            return matchingTypes;
        }
    }
}
