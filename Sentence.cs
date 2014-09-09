using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Enju
{
    [XmlRoot("sentence")]
    public class Sentence : Structure
    {
        #region Properties

        [XmlElement("cons")]
        public Construct RootConstruct { get; set; }

        [XmlAttribute("parse_status")]
        public string ParseStatus { get; set; }

        #endregion

        #region Methods

        Dictionary<string, Construct> lookup;

        public void Resolve()
        {
            Constructs = EnumerableEx.Return(RootConstruct)
                                      .Concat(RootConstruct.Descendants)
                                      .ToArray();
            lookup = Constructs.ToDictionary(c => c.Id);

            RootConstruct.Resolve(this, null);
        }

        [XmlIgnore]
        public Construct[] Constructs
        {
            get;
            private set;

        }

        public bool IsValid
        {
            get { return ParseStatus == "success"; }
        }

        readonly static XmlSerializer Serializer = new XmlSerializer(typeof(Sentence));
        public static Sentence Parse(Stream stream)
        {
            Sentence sentence = (Sentence)Serializer.Deserialize(stream);
            sentence.Resolve();
            return sentence;
        }

        public Construct FindById(string id)
        {
            Construct c;
            lookup.TryGetValue(id, out c);
            return c;
        }

        public Construct Find(string category, params string[] subcategories)
        {
            return FindAll(category, subcategories).FirstOrDefault();
        }

        public IEnumerable<Construct> FindAll(string category, params string[] subcategories)
        {
            return Constructs.Where(c => c.Is(category, subcategories));
        }

        #endregion
    }
}
