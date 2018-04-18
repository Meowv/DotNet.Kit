using System.Web;
using System.Xml;

namespace DotNet.Kit
{
    public class XmlHelper
    {
        /// <summary>
        /// XML文件的物理路径
        /// </summary>
        private string _filePath = string.Empty;

        /// <summary>
        /// Xml文档
        /// </summary>
        private XmlDocument _xml;

        /// <summary>
        /// XML的根节点
        /// </summary>
        private XmlElement _element;

        /// <summary>
        /// 创建XML的根节点
        /// </summary>
        private void CreateXMLElement()
        {
            //创建一个XML对象
            _xml = new XmlDocument();

            //加载XML文件
            if (FileHelper.IsExistFile(_filePath))
            {
                _xml.Load(_filePath);
            }

            //为XML的根节点赋值
            _element = _xml.DocumentElement;
        }

        /// <summary>
        /// 创建根节点对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <returns></returns>
        private static XmlElement CreateRootElement(string xmlFilePath)
        {
            string filePath = HttpContext.Current.Server.MapPath(xmlFilePath);

            var xmlDocument = new XmlDocument();

            xmlDocument.Load(filePath);

            return xmlDocument.DocumentElement;
        }

        /// <summary>
        /// 实例化XmlHelper对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        public XmlHelper(string xmlFilePath)
        {
            //获取XML文件的绝对路径
            _filePath = HttpContext.Current.Server.MapPath(xmlFilePath);
        }

        /// <summary>
        /// 获取指定XPath表达式的节点对象
        /// </summary>
        /// <param name="xPath">
        /// XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <returns></returns>
        public XmlNode GetNode(string xPath)
        {
            CreateXMLElement();
            return _element.SelectSingleNode(xPath);
        }

        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <returns></returns>
        public string GetValue(string xPath)
        {
            CreateXMLElement();
            return _element.SelectSingleNode(xPath).InnerText;
        }

        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        /// <returns></returns>
        public string GetAttributeValue(string xPath, string attributeName)
        {
            CreateXMLElement();
            return _element.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }

        /// <summary>
        /// 新增节点,将任意节点插入到当前Xml文件中
        /// </summary>
        /// <param name="xmlNode">要插入的Xml节点</param>
        public void AppendNode(XmlNode xmlNode)
        {
            CreateXMLElement();

            XmlNode node = _xml.ImportNode(xmlNode, true);
            _element.AppendChild(node);
        }

        /// <summary>
        /// 删除指定XPath表达式的节点
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public void RemoveNode(string xPath)
        {
            CreateXMLElement();

            XmlNode node = _xml.SelectSingleNode(xPath);
            _element.RemoveChild(node);
        }

        /// <summary>
        /// 保存XML文件
        /// </summary>
        public void Save()
        {
            CreateXMLElement();
            _xml.Save(_filePath);
        }

        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <returns></returns>
        public static string GetValue(string xmlFilePath, string xPath)
        {
            var rootElement = CreateRootElement(xmlFilePath);

            return rootElement.SelectSingleNode(xPath).InnerText;
        }

        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        /// <returns></returns>
        public static string GetAttributeValue(string xmlFilePath, string xPath, string attributeName)
        {
            var rootElement = CreateRootElement(xmlFilePath);

            return rootElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
    }
}