using System;
using System.Collections.Generic;
using System.Linq;

namespace Enju
{
    public static class EnjuExtensions
    {
        public static Construct Deepest(this IEnumerable<Construct> cons)
        {
            if(cons.IsEmpty())
                return null;

            return cons.MaxBy(c => c.Depth).First();
        }

        public static Construct Shallowest(this IEnumerable<Construct> cons)
        {
            if (cons.IsEmpty())
                return null;

            return cons.MinBy(c => c.Depth).First();
        }

        public static IEnumerable<T> InOrder<T>(this IEnumerable<T> cons) where T : Structure
        {
            return cons.OrderBy(c => c.Id);
        }

        public static IEnumerable<Construct> Edges(this IEnumerable<Construct> cons)
        {
            return cons.Where(c => c.HasToken);
        }

        public static IEnumerable<Token> Filter(this IEnumerable<Token> tokens, params string[] words)
        {
            return tokens.Where(t => !words.Contains(t.Base, StringComparer.CurrentCultureIgnoreCase));
        }

        public static string[] ToText(this IEnumerable<Construct> list)
        {
            return list.Select(c => c.FullText).ToArray();
        }

        public static string[] ToTextArray(this IEnumerable<Token> list)
        {
            return list.Select(c => c.Text).ToArray();
        }

        public static string Join(this IEnumerable<string> list)
        {
            return string.Join(" ", list);
        }
    }
}
