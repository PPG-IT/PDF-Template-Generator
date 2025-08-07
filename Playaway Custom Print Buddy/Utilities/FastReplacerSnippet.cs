using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playaway_Custom_Print_Buddy.Utilities
{
    public class FastReplacerSnippet
    {
        private Dictionary<string, string> _replacements;

        public FastReplacerSnippet()
        {
            _replacements = new Dictionary<string, string>();
        }

        public void AddReplacement(string oldValue, string newValue)
        {
            _replacements[oldValue] = newValue;
        }

        public void RemoveReplacement(string oldValue)
        {
            _replacements.Remove(oldValue);
        }

        public void ClearReplacements()
        {
            _replacements.Clear();
        }

        public string ProcessText(string sourceText)
        {
            string result = sourceText;
            foreach (var replacement in _replacements)
            {
                result = FastReplacer.ReplaceAll(result, replacement.Key, replacement.Value);
            }
            return result;
        }

        public void ProcessTextInPlace(ref string sourceText)
        {
            foreach (var replacement in _replacements)
            {
                FastReplacer.ReplaceAllInPlace(ref sourceText, replacement.Key, replacement.Value);
            }
        }

        public Dictionary<string, string> GetReplacements()
        {
            return new Dictionary<string, string>(_replacements);
        }

        public void SetReplacements(Dictionary<string, string> replacements)
        {
            _replacements = new Dictionary<string, string>(replacements);
        }

        public bool HasReplacement(string oldValue)
        {
            return _replacements.ContainsKey(oldValue);
        }

        public string GetReplacement(string oldValue)
        {
            return _replacements.TryGetValue(oldValue, out string value) ? value : null;
        }

        public int ReplacementCount => _replacements.Count;

        public void LoadFromString(string replacementData, char pairSeparator = '\n', char valueSeparator = '=')
        {
            _replacements.Clear();
            string[] pairs = replacementData.Split(pairSeparator);
            foreach (string pair in pairs)
            {
                if (string.IsNullOrWhiteSpace(pair))
                    continue;

                string[] parts = pair.Split(new char[] { valueSeparator }, 2);
                if (parts.Length == 2)
                {
                    _replacements[parts[0]] = parts[1];
                }
            }
        }

        public string SaveToString(char pairSeparator = '\n', char valueSeparator = '=')
        {
            StringBuilder sb = new StringBuilder();
            foreach (var replacement in _replacements)
            {
                sb.Append(replacement.Key);
                sb.Append(valueSeparator);
                sb.Append(replacement.Value);
                sb.Append(pairSeparator);
            }
            return sb.ToString();
        }
    }
} 