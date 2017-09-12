using OuterSpace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLParser;

namespace OuterSpace.Game.Loaders
{
    public class EnemyShipParser : XmlFileReader
    {
        List<AiModel> _aiModelList = null;

        public EnemyShipParser(string filename) : base(filename) { }

        public List<AiModel> GetEnemiesList()
        {
            if (_aiModelList != null)
                return _aiModelList;
            _aiModelList = new List<AiModel>();
            XmlNodeList nodeList =  _xmlDocument.GetElementsByTagName("Ai");
            foreach(XmlNode node in nodeList)
            {
                _aiModelList.Add
                (
                    new AiModel
                    {
                        ID = Int32.Parse(node["ID"].InnerText),
                        Texture = node["Texture"].InnerText,
                        Strength = Int32.Parse(node["Strength"].InnerText),
                        HeadingRange = Int32.Parse(node["HeadingRange"].InnerText),
                        RandomStart = node["RandomStart"].InnerText,
                        Weapon = Int32.Parse(node["WeaponType"].InnerText),
                        FireFrequency = Int32.Parse(node["FireFrequency"].InnerText),
                        ScanRange = Double.Parse(node["Scanrange"].InnerText),
                        Speed = Int32.Parse(node["Speed"].InnerText),
                        HitBarShow = bool.Parse(node["ShowHitbar"].InnerText),
                        Width = Double.Parse(node["Width"].InnerText),
                        Height = Double.Parse(node["Height"].InnerText)
                    }
                );
            }

            return _aiModelList;
        }

        public List<AiModel> GetEnemiesListForceRead()
        {
            _aiModelList.Clear();
            _aiModelList = null;
            return GetEnemiesList();
        }
    }
}
