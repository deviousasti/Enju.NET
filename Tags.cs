using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enju
{
    public static class Category
    {

        public const string SubordinateConjunction = "SCX";

        public const string SubordinationConjunction = "SC";
        public const string Punctuation = "PN";

        //Phrases
        public const string Sentence = "S";
        public const string SubordinatePhrase = "SCP";
        public const string NounPhrase = "NP";
        public const string AdjectivePhrase = "ADJP";
        public const string PrepositionalPhrase = "PP";
        public const string VerbPhrase = "VP";
        public const string AdverbPhrase = "ADVP";

        public const string NounProper = "NX";
        public const string DependentPhrase = "DP";
        public const string WhAdverb = "WRB";
        public const string CopulativeVerb = "VX";
        public const string LexicalVerb = "VB";
        public const string Preposition = "P";
        public const string Noun = "N";
        public const string WhQuestion = "WH";
    }

    public class SubCategory
    {
        public const string None = "";
        public const string Imperative = "IMP";
        public const string Coordinating = "COOD";

    }

    public static class PoS
    {
        public const string CoordinatingConjunction = "CC";
        public const string CardinalNumber = "CD";

        public const string Determiner = "DT";
        public const string ExistentialThere = "EX";

        public const string ForeignWord = "FW";
        public const string SubordinatingConjunction = "IN";
        public const string Preposition = "IN";


        public const string Noun = "N";
        public const string NounSingular = "NN";
        public const string NounProper = "NNP";
        public const string Adjective = "JJ";
        public const string Verb = "VB";
        public const string Adverb = "RB";
        public const string Verb3PSingularPresent = "VBZ";
        public const string VerbNon3PSingularPresent = "VBP";
        public const string VerbPastTense = "VBD";
        public const string VerbPastParticiple = "VBN";
        public const string VerbPresentParticiple = "VBG";
        public const string VerbGenrund = "VBG";
        public const string AuxillaryVerbModal = "MD";
    }
}
