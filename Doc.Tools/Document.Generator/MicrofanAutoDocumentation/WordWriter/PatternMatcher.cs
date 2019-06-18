using ContentBlock.Models.ContentBlocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Document.Generator.Dll.WordWriter.MarkDown
{
    public class PatternMatcher
    {
        public Regex regex;
        public Ranges<int> matches = new Ranges<int>();
        public bool Flag = false;

        public Pattern pattern;
        public static Dictionary<Pattern, Regex> Patterns = new Dictionary<Pattern, Regex>()
            {
                { Pattern.Bold, new Regex("(?<!\\*)(\\*\\*)([^\\ +].+?)(\\*\\*)") },
                { Pattern.Asterisk, new Regex("(?<!\\*)[^\\*]?(\\*)([^\\*].+?)(\\*)[^\\*]") },
                { Pattern.Italic, new Regex("(?<!`)[^`]?(`)([^`].+?)(\\`)[^`]?") },
                { Pattern.Underline, new Regex("(?<!_)[^_]?(_)([^_].+?)(_)[^_]?") },
                {Pattern.BulletList,new Regex("(?<!\\$)(\\$\\$)([^\\ +].+?)(\\$\\$)") },

        };




        public PatternMatcher(Pattern pattern)
        {
            this.pattern = pattern;
            regex = Patterns[pattern];
        }

        public bool HasMatches()
        {
            return matches.Count() > 0;
        }

        public bool ContainsValue(int num)
        {
            return matches.ContainsValue(num);
        }
        public void SetFlagFor(int pos)
        {
            if (ContainsValue(pos))
            {
                Flag = true;
            }
            else
            {
                Flag = false;
            }
        }

        internal void FindMatches(string content, ref Ranges<int> Tokens)
        {
            if (pattern == Pattern.Bold || pattern == Pattern.Italic|| pattern == Pattern.Underline || pattern == Pattern.Asterisk || pattern == Pattern.BulletList )
            {
                MatchCollection mc = regex.Matches(content);
                int num = 0;

                foreach (Match m in mc)
                {
                    num++;

                    int sToken = m.Groups[1].Index;
                    int match = m.Groups[2].Index;
                    int eToken = m.Groups[3].Index;
                    int endStr = m.Groups[3].Index + m.Groups[3].Length;

                    Tokens.Add(new Range<int>()
                    {
                        Minimum = sToken,
                        Maximum = match - 1
                    });

                    matches.Add(new Range<int>()
                    {
                        Minimum = match,
                        Maximum = eToken - 1
                    });

                    Tokens.Add(new Range<int>()
                    {
                        Minimum = eToken,
                        Maximum = endStr - 1
                    });
                }
            }
        }
    }


    public class Range<T> where T : IComparable<T>
    {
        public T Minimum { get; set; }
        public T Maximum { get; set; }

        public override string ToString() { return String.Format("[{0} - {1}]", Minimum, Maximum); }

        public Boolean IsValid() { return Minimum.CompareTo(Maximum) <= 0; }

        public Boolean ContainsValue(T value)
        {
            return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
        }
    }

    public class Ranges<T> where T : IComparable<T>
    {
        private List<Range<T>> rangelist = new List<Range<T>>();

        public void Add(Range<T> range)
        {
            rangelist.Add(range);
        }

        public int Count()
        {
            return rangelist.Count;
        }

        public Boolean ContainsValue(T value)
        {
            foreach (Range<T> range in rangelist)
            {
                if (range.ContainsValue(value)) return true;
            }

            return false;
        }
    }
}


