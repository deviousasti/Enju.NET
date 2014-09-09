using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace Enju
{
    [XmlType("cons")]
    public class Construct : Structure
    {
        #region Properties

        [XmlAttribute("cat")]
        public string Category { get; set; }

        [XmlAttribute("xcat"), DefaultValue("")]
        public string ExtendedCategory { get; set; }

        [XmlAttribute("head"), EditorBrowsable(EditorBrowsableState.Never)]
        public string HeadId { get; set; }

        [XmlAttribute("sem_head"), EditorBrowsable(EditorBrowsableState.Never)]
        public string SemanticHeadId { get; set; }

        [XmlAttribute("schema")]
        public string Schema { get; set; }

        [XmlElement(Order = 1, ElementName = "cons")]
        public Construct First { get; set; }

        [XmlElement(Order = 2, ElementName = "cons")]
        public Construct Second { get; set; }

        [XmlElement(Order = 3, ElementName = "tok")]
        public Token Token { get; set; }

        #endregion

        #region Methods

        #region Shortcut Methods

        [XmlIgnore]
        public int Depth { get; private set; }

        [XmlIgnore]
        public Sentence Root { get; set; }

        [XmlIgnore]
        public Construct Parent { get; protected set; }

        internal void Resolve(Sentence root, Construct parent)
        {
            this.Root = root;
            this.Parent = parent;
            this.Depth = (parent == null) ? 0 : parent.Depth + 1;

            foreach (var subc in Children)
                subc.Resolve(root, this);

            if (HasToken)
            {
                Token.Root = root;
                Token.Parent = this;
            }
        }

        [XmlIgnore]
        public Construct Head
        {
            get
            {
                return Root.FindById(HeadId);
            }
        }

        [XmlIgnore]
        public Construct SemanticHead
        {
            get
            {
                return Root.FindById(SemanticHeadId);
            }
        }


        [XmlIgnore]
        public IEnumerable<Construct> Children
        {
            get
            {
                if (First != null)
                    yield return First;

                if (Second != null)
                    yield return Second;

                yield break;
            }
        }

        [XmlIgnore]
        public string FullCategory
        {
            get { return String.IsNullOrEmpty(ExtendedCategory) ? Category : String.Format("{0}-{1}", Category, ExtendedCategory); }
        }

        [XmlIgnore]
        public IEnumerable<Construct> Descendants
        {
            get
            {
                return Children.Expand(c => c.Children);
            }
        }

        [XmlIgnore]
        public IEnumerable<Construct> DescendantsAndSelf
        {
            get
            {
                return EnumerableEx.Return(this).Concat(Descendants);
            }
        }


        [XmlIgnore]
        public IEnumerable<Construct> Ancestors
        {
            get
            {
                return EnumerableEx.Generate(this, c => c.Parent != null, c => c.Parent, c => c);
            }
        }


        [XmlIgnore]
        public bool HasToken
        {
            get { return Token != null; }
        }
        #endregion

        #region Search

        public bool IsAnyOf(params string[] categories)
        {
            return categories.Any(c => Is(c));
        }

        public bool Is(string category, params string[] subcategories)
        {
            if (category != Category)
                return false;

            if (subcategories.Length == 0)
                return true;

            if (!ExtendedCategory.Contains(' '))
                return subcategories[0] == ExtendedCategory;

            var exts = ExtendedCategory.Split(' ');

            return subcategories.All(c => exts.Contains(c));
        }

        public Construct Find(string category, params string[] subcategories)
        {
            return FindAll(category, subcategories).FirstOrDefault();
        }

        public IEnumerable<Construct> FindAll(string category, params string[] subcategories)
        {
            return DescendantsAndSelf.Where(c => c.Is(category, subcategories));
        }

        public Construct FindById(string id)
        {
            return DescendantsAndSelf.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Token> FindTokens()
        {
            return DescendantsAndSelf.Where(c => c.HasToken).Select(c => c.Token);
        }

        public IEnumerable<Token> FindTokens(string part_of_speech)
        {
            return FindTokens().Where(c => c.Is(part_of_speech));
        }

        public Token FindToken()
        {
            return HasToken ? Token : FindTokens().First();
        }

        public Token FindToken(string part_of_speech)
        {
            return FindTokens(part_of_speech).FirstOrDefault();
        }

        public bool IsChildOf(Construct construct)
        {
            return Ancestors.Contains(construct);
        }

        public bool IsParentOf(Construct construct)
        {
            return Descendants.Contains(construct);
        }

        #endregion

        #region String

        public string FullText
        {
            get
            {
                if (this.HasToken)
                    return this.Token.Text;

                return string.Join(" ", FindTokens().OrderBy(s => s.Id).Select(s => s.Text));
            }
        }

        public string ToString(int refdepth)
        {
            return String.Format("({0} {1})",
                FullCategory,
                HasToken ? Token.ToString() : string.Join(", ", Children.Select(c => c.ToString(refdepth)))
                );
        }

        public override string ToString()
        {
            return ToString(Depth);
        }




        #endregion

        #endregion
    }
}
