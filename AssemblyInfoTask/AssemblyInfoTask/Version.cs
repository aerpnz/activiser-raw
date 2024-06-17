using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Build.Extras
{
    internal class Version
    {
        private string versionString;
        public string VersionString
        {
            get 
            { 
                return versionString; 
            }
            set 
            {
                ParseVersion(value);
            }
        }

        private string majorVersion;
        public string MajorVersion
        {
            get { return majorVersion; }
            set { majorVersion = value; }
        }

        private string minorVersion;
        public string MinorVersion
        {
            get { return minorVersion; }
            set { minorVersion = value; }
        }

        private string buildNumber;
        public string BuildNumber
        {
            get { return buildNumber; }
            set { buildNumber = value; }
        }

        private string revision;
        public string Revision
        {
            get { return revision; }
            set { revision = value; }
        }

        public Version()
        {
            this.MajorVersion = "1";
            this.MinorVersion = "0";
            this.BuildNumber = "0";
            this.Revision = "0";
        }

        public Version(string version)
        {
            this.ParseVersion(version);
        }

        private void ParseVersion(string version)
        {
            Regex versionPattern = new Regex(@"(?<majorVersion>(\d+|\*))\." + @"(?<minorVersion>(\d+|\*))\." + @"(?<buildNumber>(\d+|\*))\." + @"(?<revision>(\d+|\*))", RegexOptions.Compiled);

            MatchCollection matches = versionPattern.Matches(version);
            if (matches.Count != 1)
            {
                throw new ArgumentException("version", "The specified string is not a valid version number");
            }

            this.MajorVersion = matches[0].Groups["majorVersion"].Value;
            this.MinorVersion = matches[0].Groups["minorVersion"].Value;
            this.BuildNumber = matches[0].Groups["buildNumber"].Value;
            this.Revision = matches[0].Groups["revision"].Value;
            this.versionString = version;   // Very important that this is a little v, not big v, otherwise you get infinite recursion!
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}.{3}", this.MajorVersion, this.MinorVersion, this.BuildNumber, this.Revision);
        }
    }
}
