using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace Enju
{
    [XmlType("tok")]
    public class Token : Structure
    {
        #region Properties

        [XmlAttribute("cat")]
        public string Category { get; set; }

        [XmlAttribute("pos")]
        public string PartOfSpeech { get; set; }

        [XmlAttribute("base")]
        public string Base { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("pred")]
        public string Predicate { get; set; }

        [XmlText]
        public string Text { get; set; }

        [XmlAttribute("arg1")]
        public string Arg1Id { get; set; }

        [XmlAttribute("arg2")]
        public string Arg2Id { get; set; }

        [XmlAttribute("tense")]
        public string Tense { get; set; }

        [XmlAttribute("aspect"), DefaultValue("none")]
        public string Aspect { get; set; }

        [XmlAttribute("voice")]
        public string Voice { get; set; }

        #endregion

        #region Methods

        [XmlIgnore]
        internal Sentence Root { get; set; }

        [XmlIgnore]
        public Construct Parent { get; internal set; }

        [XmlIgnore]
        public Construct PrimaryRelation
        {
            get { return Root.FindById(this.Arg1Id); }
        }

        [XmlIgnore]
        public Construct SecondaryRelation
        {
            get { return Root.FindById(this.Arg2Id); }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", PartOfSpeech, Text);
        }

        public bool Is(string partOfSpeech)
        {
            return PartOfSpeech.StartsWith(partOfSpeech);
        }

        #endregion

    }
}
