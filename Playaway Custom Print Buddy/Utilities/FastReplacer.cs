using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playaway_Custom_Print_Buddy.Utilities
{
    public class FastReplacer
    {
        public static string ReplaceAllEx(string SourceString, string OldValue, string NewValue)
        {
            return SourceString.Replace(OldValue, NewValue);
        }

        public static void ReplaceAllInPlace(ref string SourceString, string OldValue, string NewValue)
        {
            SourceString = SourceString.Replace(OldValue, NewValue);
        }

        public static void ReplaceAllInPlaceEx(ref string SourceString, string OldValue, string NewValue)
        {
            SourceString = SourceString.Replace(OldValue, NewValue);
        }

        public static string ReplaceAll(string SourceString, string OldValue, string NewValue)
        {
            return SourceString.Replace(OldValue, NewValue);
        }

        public static int CountOccurencesOf(string SourceString, string Value)
        {
            int count = 0;
            int index = 0;
            while ((index = SourceString.IndexOf(Value, index)) != -1)
            {
                count++;
                index += Value.Length;
            }
            return count;
        }

        public static string ReplaceFirstOccurence(string SourceString, string OldValue, string NewValue)
        {
            int index = SourceString.IndexOf(OldValue);
            if (index >= 0)
            {
                return SourceString.Substring(0, index) + NewValue + SourceString.Substring(index + OldValue.Length);
            }
            return SourceString;
        }

        public static string ReplaceLastOccurence(string SourceString, string OldValue, string NewValue)
        {
            int index = SourceString.LastIndexOf(OldValue);
            if (index >= 0)
            {
                return SourceString.Substring(0, index) + NewValue + SourceString.Substring(index + OldValue.Length);
            }
            return SourceString;
        }

        public static string RemoveFirst(string SourceString, string Value)
        {
            int index = SourceString.IndexOf(Value);
            if (index >= 0)
            {
                return SourceString.Remove(index, Value.Length);
            }
            return SourceString;
        }

        public static string RemoveLast(string SourceString, string Value)
        {
            int index = SourceString.LastIndexOf(Value);
            if (index >= 0)
            {
                return SourceString.Remove(index, Value.Length);
            }
            return SourceString;
        }

        public static string RemoveAll(string SourceString, string Value)
        {
            return SourceString.Replace(Value, "");
        }

        public static void RemoveAllInPlace(ref string SourceString, string Value)
        {
            SourceString = SourceString.Replace(Value, "");
        }

        public static string ReplaceByRange(string SourceString, int StartIndex, int EndIndex, string NewValue)
        {
            if (StartIndex < 0 || EndIndex >= SourceString.Length || StartIndex > EndIndex)
                return SourceString;

            return SourceString.Substring(0, StartIndex) + NewValue + SourceString.Substring(EndIndex + 1);
        }

        public static string[] SplitByFirst(string SourceString, string Separator)
        {
            int index = SourceString.IndexOf(Separator);
            if (index >= 0)
            {
                return new string[] { SourceString.Substring(0, index), SourceString.Substring(index + Separator.Length) };
            }
            return new string[] { SourceString };
        }

        public static string[] SplitByLast(string SourceString, string Separator)
        {
            int index = SourceString.LastIndexOf(Separator);
            if (index >= 0)
            {
                return new string[] { SourceString.Substring(0, index), SourceString.Substring(index + Separator.Length) };
            }
            return new string[] { SourceString };
        }

        public static bool ContainsAny(string SourceString, params string[] Values)
        {
            return Values.Any(value => SourceString.Contains(value));
        }

        public static bool ContainsAll(string SourceString, params string[] Values)
        {
            return Values.All(value => SourceString.Contains(value));
        }

        public static string ExtractBetween(string SourceString, string StartMarker, string EndMarker)
        {
            int startIndex = SourceString.IndexOf(StartMarker);
            if (startIndex >= 0)
            {
                startIndex += StartMarker.Length;
                int endIndex = SourceString.IndexOf(EndMarker, startIndex);
                if (endIndex >= 0)
                {
                    return SourceString.Substring(startIndex, endIndex - startIndex);
                }
            }
            return string.Empty;
        }

        public static string TrimToLength(string SourceString, int MaxLength)
        {
            if (SourceString.Length <= MaxLength)
                return SourceString;
            return SourceString.Substring(0, MaxLength);
        }

        public static string PadToLength(string SourceString, int Length, char PaddingChar = ' ')
        {
            if (SourceString.Length >= Length)
                return SourceString;
            return SourceString.PadRight(Length, PaddingChar);
        }
    }
} 