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
    public class LevelParser : XmlFileReader
    {
        public LevelParser(string filename) : base(filename) { }
        private List<LevelModel> _levelModelList;

        public List<LevelModel> GetLevelsList()
        {
            if (_levelModelList != null)
                return _levelModelList;
            _levelModelList = new List<LevelModel>();
            XmlNodeList nodeList = _xmlDocument.GetElementsByTagName("Levels");
            nodeList = nodeList[0].ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                XmlNodeList ChildNodeList = node.ChildNodes;
                string val = node["ID"].InnerText;
                _levelModelList.Add
                (
                    new LevelModel
                    {
                        ID = Int32.Parse(node["ID"].InnerText),
                        Chapter = node["Chapter"].InnerText,
                        AiTypes = AddEnemiesFromLevelNode(ChildNodeList),
                        Speed = Int32.Parse(node["Speed"].InnerText),
                        LevelStartAnimationType = Int32.Parse(node["LevelStartAnimationType"].InnerText),
                        LevelEndAnimationType = Int32.Parse(node["LevelEndAnimationType"].InnerText),
                        LevelBackgroundType = Int32.Parse(node["LevelBackgroundType"].InnerText)
                    }
                );
            }

            return _levelModelList;
        }

        public List<int> AddEnemiesFromLevelNode(XmlNodeList nodeList)
        {

            List<int> aiList = new List<int>();
            foreach (XmlNode node in nodeList)
                    if (node.Name == "Ai")
                        aiList.Add(Int32.Parse(node.InnerText));
            return aiList;
        }

        public List<LevelModel> GetLevelsListForceRead()
        {
            _levelModelList.Clear();
            _levelModelList = null;
            return GetLevelsList();
        }
    }
}
