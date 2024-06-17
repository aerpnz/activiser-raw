using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Build.Extras
{
    internal class AssemblyInfoWrapper
    {
        List<string> rawFileLines = new List<string>();
        Dictionary<string, int> attributeIndex = new Dictionary<string, int>();

        Regex attributeNamePattern = new Regex(@"[aA]ssembly:?\s*(?<attributeName>\w+)\s*\(", RegexOptions.Compiled);
        Regex attributeStringValuePattern = new Regex(@"""(?<attributeValue>.*?)""", RegexOptions.Compiled);
        Regex attributeBooleanValuePattern = new Regex(@"\((?<attributeValue>([tT]rue|[fF]alse))\)", RegexOptions.Compiled);
        Regex singleLineCSharpCommentPattern = new Regex(@"\s*//", RegexOptions.Compiled);
        Regex singleLineVbCommentPattern = new Regex(@"\s*'", RegexOptions.Compiled);
        // The ^\* is so the regex works with J# files that use /** to indicate the actual attribute lines.
        // This does mean that lines like /** in C# will get treated as valid lines, but that's a real borderline case.
        Regex multiLineCSharpCommentStartPattern = new Regex(@"\s*/\*^\*", RegexOptions.Compiled); 
        Regex multiLineCSharpCommentEndPattern = new Regex(@".*?\*/", RegexOptions.Compiled);

        public AssemblyInfoWrapper(string filename)
        {
            StreamReader reader = new StreamReader(File.OpenRead(filename), Encoding.Unicode, true);
            int lineNumber = 0;
            string input;
            MatchCollection matches;
            bool skipLine = false;

            while ((input = reader.ReadLine()) != null)
            {
                rawFileLines.Add(input);

                // Skip single comment lines
                if (singleLineCSharpCommentPattern.IsMatch(input) || singleLineVbCommentPattern.IsMatch(input))
                {
                    lineNumber++;
                    continue;
                }

                // Skip multi-line C# comments
                if (multiLineCSharpCommentStartPattern.IsMatch(input))
                {
                    lineNumber++;
                    skipLine = true;
                    continue;
                }
                // Stop skipping when we're at the end of a C# multiline comment
                if (multiLineCSharpCommentEndPattern.IsMatch(input) && skipLine)
                {
                    lineNumber++;
                    skipLine = false;
                    continue;
                }
                
                // If we're in the middle of a multiline comment, keep going
                if (skipLine)
                {
                    lineNumber++;
                    continue;
                }

                // Check to see if the current line is an attribute on the assembly info.
                // If so we need to keep the line number in our dictionary so we can go
                // back later and get it when this class is accessed through its indexer.
                matches = attributeNamePattern.Matches(input);
                if (matches.Count > 0)
                {
                    attributeIndex.Add(matches[0].Groups["attributeName"].Value, lineNumber);
                }

                lineNumber++;
            }
            reader.Close();
        }

        public string this[string attribute]
        {
            get
            {
                if (!attributeIndex.ContainsKey(attribute))
                {
                    return null;
                }
                else
                {
                    MatchCollection matches;

                    // Try to match string properties first
                    matches = attributeStringValuePattern.Matches(rawFileLines[attributeIndex[attribute]]);
                    if (matches.Count > 0)
                    {
                        return matches[0].Groups["attributeValue"].Value;
                    }

                    // If that fails, try to match a boolean value
                    matches = attributeBooleanValuePattern.Matches(rawFileLines[attributeIndex[attribute]]);
                    if (matches.Count > 0)
                    {
                        return matches[0].Groups["attributeValue"].Value;
                    }

                    return null;                    
                }
            }
            set
            {
                // The set case requires fancy footwork. In this case we actually replace the attribute
                // value in the string using a regex to the value that was passed in.
                if (!attributeIndex.ContainsKey(attribute))
                {
                    throw new ArgumentOutOfRangeException("attribute", String.Format("{0} is not an attribute in the specified AssemblyInfo.cs file", attribute));
                }

                MatchCollection matches;

                // Try setting it as a string property first
                matches = attributeStringValuePattern.Matches(rawFileLines[attributeIndex[attribute]]);
                if (matches.Count > 0)
                {
                    rawFileLines[attributeIndex[attribute]] = attributeStringValuePattern.Replace(rawFileLines[attributeIndex[attribute]], "\"" + value + "\"");
                    return;
                }

                // If that fails try setting it as a boolean property
                matches = attributeBooleanValuePattern.Matches(rawFileLines[attributeIndex[attribute]]);
                if (matches.Count > 0)
                {
                    rawFileLines[attributeIndex[attribute]] = attributeBooleanValuePattern.Replace(rawFileLines[attributeIndex[attribute]], "(" + value + ")");
                    return;
                }
            }
        }

        public void Write(StreamWriter streamWriter)
        {
            foreach (string line in rawFileLines)
            {
                streamWriter.WriteLine(line);
            }
        }

    }
}
