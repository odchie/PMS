using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

using Pms.Common.Variable;

namespace Pms.Common.Helper.DynamicVariable
{
    /// <summary>
    /// This class gets application variables from external source. It is important to IISReset when refreshing the data from this class
    /// </summary>
    /// <typeparam name="SourceItem">Source of the object inheriting</typeparam>
    public abstract class BaseData<SourceItem> where SourceItem : class
    {
        #region Variable
        private const string CACHE_KEY = "Ytrs_2sb";
        private const string PRIMARY_KEY = "Id";
        private const string DYNAMIC_VARIABLE_FILE = "DynamicVariable.xml";
        private string id;
        private string name;
        #endregion

        #region Public properties
        /// <summary>
        /// Id column
        /// </summary>
        public virtual string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Name column
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }
        #endregion

        #region Public static methods
        /// <summary>
        /// Get the sorted instance of the object
        /// </summary>
        /// <param name="sorted">Indicates if sorted or not</param>
        /// <returns></returns>
        public static List<SourceItem> GetAll()
        {
            Type sourceType = typeof(SourceItem);

            object sourceCache = CacheManager.Get(CACHE_KEY);

            if (sourceCache == null)
            {
                const string _nodeQuery = "NodeQuery";
                List<SourceItem> returnList = new List<SourceItem>();
                XmlNodeList sourceNodes = LoadDynamicVariable().SelectNodes(sourceType.GetField(_nodeQuery).GetValue(null).ToString());

                foreach (XmlNode sourceNode in sourceNodes)
                {
                    object sourceItem = Activator.CreateInstance(sourceType);

                    foreach (XmlAttribute attribute in sourceNode.Attributes)
                    {
                        sourceType.GetProperty(attribute.Name).SetValue(sourceItem, sourceNode.Attributes[attribute.Name].Value, null);
                    }

                    returnList.Add((SourceItem)sourceItem);
                }

                CacheManager.Add(sourceType.ToString(), returnList, 240);

                return returnList;
            }
            else
            {
                return (List<SourceItem>)sourceCache;
            }
        }

        /// <summary>
        /// Gets a specific instance of the object by Id.
        /// </summary>
        /// <param name="id">Id of the object to be retrieved</param>
        /// <returns></returns>
        public static SourceItem Get(string id)
        {
            return GetAll().SingleOrDefault<SourceItem>(delegate(SourceItem typeOfSource)
            {
                return typeOfSource.GetType().GetProperty(PRIMARY_KEY).GetValue(typeOfSource, null).ToString() == id;
            });
        }

        /// <summary>
        /// Gets the multiple instance of the object by Id.
        /// </summary>
        /// <param name="id">Id of the object to be retrieved</param>
        /// <returns></returns>
        public static IEnumerable<SourceItem> GetWithMultiple(string id)
        {
            return GetByKey(PRIMARY_KEY, id);
        }

        /// <summary>
        /// Gets a specific instance of the object by key and value.
        /// </summary>
        /// <param name="key">Key of the field</param>
        /// <param name="value">Value of the field</param>
        /// <returns></returns>
        public static IEnumerable<SourceItem> GetByKey(string key, string value)
        {
            return GetAll().Where<SourceItem>(delegate(SourceItem typeOfSource)
            {
                return typeOfSource.GetType().GetProperty(key).GetValue(typeOfSource, null).ToString() == value;
            });
        }
        #endregion

        #region Private procedure
        /// <summary>
        /// Loads the xml document from external source
        /// </summary>
        /// <returns></returns>
        private static XmlDocument LoadDynamicVariable()
        {
            XmlDocument xmlDocument = new XmlDocument();

            using (StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + DYNAMIC_VARIABLE_FILE))
            {
                using (StringReader stringReader = new StringReader(streamReader.ReadToEnd()))
                {
                    xmlDocument.Load(stringReader);
                }
            }

            return xmlDocument;
        }
        #endregion
    }

    /// <summary>
    /// Data for Country
    /// </summary>
    public class ServiceResponse : BaseData<ServiceResponse>
    {
        #region Variable
        public const string NodeQuery = "/Pms/ServiceResponses/ServiceResponse";
        public const string PropertyException = "";
        public const string IdWithRegularExpression_Format = "{0}{1}{2}";
        private string visible;
        #endregion

        #region Public property
        public string Visible  { get; set; }

        public string RegularExpression  { get; set; }

        public string IdWithRegularExpression
        {
            get
            {
                return string.Format(IdWithRegularExpression_Format, base.Id, GeneralString.ComplexDelimiter, RegularExpression);
            }
        }
        #endregion

        #region Public static method
        #endregion

        #region Public method
        #endregion
    }
}