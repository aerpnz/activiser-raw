using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Globalization;

namespace Microsoft.Build.Extras
{
    /// <summary>
    /// The AssemblyInfo task provides a way to manipulate the content of AssemblyInfo files at build time. It works with
    /// C#, VB, and J# AssemblyInfo files.
    /// </summary>
    /// <remarks>
    /// 	<para>The primary use of the AssemblyInfo task is to set assembly version numbers
    ///     at build time. The typical way to use it is to add the
    ///     Microsoft.VersionNumber.Targets file to your project file, and to then specify
    ///     properties in your project file to control the assembly version numbers.</para>
    /// 	<para>Version numbers are of the form A.B.C.D, where:</para>
    /// 	<list type="bullet">
    /// 		<item>A is the major version</item>
    /// 		<item>B is the minor version</item>
    /// 		<item>C is the build number</item>
    /// 		<item>D is the revision</item>
    /// 	</list>
    /// 	<para>Typically the major and minor versions are fixed and do not change over the
    ///     course of multiple daily builds. For example, Visual Studio 2005 has a major and
    ///     minor version of 8.0. The build number is frequently set to increment on a daily
    ///     basis, either starting at 1 and continuing from there, or as some representation of
    ///     the date of the build. For Visual Studio 2005, the build numbers are of the form
    ///     YYMMDD. The revision is typically used to differentiate between multiple builds on
    ///     the same day, usually starting at 1 and incrementing for each build.</para>
    /// 	<para>
    ///         To get the standard Visual Studio-style version simply add the
    ///         Microsoft.VersionNumber.Targets file to your project. To override the default
    ///         version numbers, such as the major and minor version, you can set the
    ///         appropriate properties. For more information see the
    ///         <see cref="AssemblyMajorVersion">AssemblyMajorVersion</see> and
    ///         <see cref="AssemblyMinorVersion">AssemblyMinorVersion</see> items.
    ///     </para>
    /// </remarks>
    /// <seealso cref="AssemblyMajorVersion"/>
    /// <seealso cref="AssemblyMinorVersion"/>
    public class AssemblyInfo : Microsoft.Build.Utilities.Task
    {
        private struct AssemblyVersionSettings
        {
            public string MajorVersion;
            public string MinorVersion;
            public string BuildNumber;
            public string Revision;
            public string Version;
            public IncrementMethod BuildNumberType;
            public IncrementMethod RevisionType;
            public string BuildNumberFormat;
            public string RevisionFormat;
        }

        #region AssemblyVersion
        private AssemblyVersionSettings assemblyVersionSettings;
        /// <summary>
        /// The major version of the assembly.
        /// </summary>
        /// <remarks>
        /// 	<para>To change the assembly major version set this to the specific major version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "8".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyMajorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyMajorVersion&gt;8&lt;/AssemblyMajorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyMajorVersion
        {
            get { return assemblyVersionSettings.MajorVersion; }
            set { assemblyVersionSettings.MajorVersion = value; }
        }

        /// <summary>
        /// The minor version of the assembly.
        /// </summary>
        /// <remarks>
        /// 	<para>To change the assembly minor version set this to the specific minor version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "0".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyMinorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyMinorVersion&gt;0&lt;/AssemblyMinorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyMinorVersion
        {
            get { return assemblyVersionSettings.MinorVersion; }
            set { assemblyVersionSettings.MinorVersion = value; }
        }

        /// <summary>
        /// The build number of the assembly.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         To change the assembly build number set this to the specific build number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyBuildNumberType">AssemblyBuildNumberType</see> and
        ///         <see cref="AssemblyBuildNumberFormat">AssemblyBuildNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        /// 	<para>
        ///         To force the build number to a specific value when using the
        ///         Microsoft.VersionNumber.Targets, use the <em>AssemblyBuildNumber</em> property,
        ///         and set the <em>AssemblyBuildNumberFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyBuildNumber&gt;0&lt;/AssemblyBuildNumber&gt;
        /// &lt;AssemblyBuildNumberType&gt;DirectSet&lt;/AssemblyBuildNumberType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        /// <seealso cref="AssemblyBuildNumberFormat"/>
        public string AssemblyBuildNumber
        {
            get { return assemblyVersionSettings.BuildNumber; }
            set { assemblyVersionSettings.BuildNumber = value; }
        }

        /// <summary>
        /// The revision of the assembly.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         To change the assembly revision set this to the specific revision number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyRevisionType">AssemblyRevisionNumberType</see> and
        ///         <see cref="AssemblyRevisionFormat">AssemblyRevisionNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        /// 	<para>
        ///         To force the revision number to a specific value when using the
        ///         Microsoft.VersionNumber.Targets, set the <em>AssemblyRevision</em> property to
        ///         the value and set the <em>AssemblyRevisionFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyRevision&gt;0&lt;/AssemblyRevision&gt;
        /// &lt;AssemblyRevisionType&gt;DirectSet&lt;/AssemblyRevisionType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionType"/>
        /// <seealso cref="AssemblyRevisionFormat"/>
        public string AssemblyRevision
        {
            get { return assemblyVersionSettings.Revision; }
            set { assemblyVersionSettings.Revision = value; }
        }

        /// <summary>
        /// The complete version of the assembly.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         Use AssemblyVersion to directly set the entire version number with a single
        ///         parameter. For example, if you know you want your version to be "1.2.3.4", you
        ///         can set AssemblyVersion to this instead of having to use each of the individual
        ///         <see cref="AssemblyMajorVersion">AssemblyMajorVersion</see>,
        ///         <see cref="AssemblyMinorVersion">AssemblyMinorVersion</see>,
        ///         <see cref="AssemblyBuildNumber">AssemblyBuildNumber</see>, and
        ///         <see cref="AssemblyRevision">AssemblyRevision</see> properties.
        ///     </para>
        /// 	<para>Note that the other four properties override this one. For example, If you
        ///     set AssemblyVersion to "1.2.3.4" and then set AssemblyMinorVersion to 6, the resulting
        ///     version will be "1.6.3.4".</para>
        /// 	<para>
        ///         This property is an input only. If you want to know what the final version
        ///         generated was, use the
        ///         <see cref="MaxAssemblyVersion">MaxAssemblyVersion</see> output property
        ///         instead.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file the best way to specify
        ///     this is to set the <em>AssemblyVersion</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyVersion&gt;1.2.3.4&lt;/AssemblyVersion&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="MaxAssemblyVersion"/>
        public string AssemblyVersion
        {
            get { return assemblyVersionSettings.Version; }
            set { assemblyVersionSettings.Version = value; }
        }

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyBuildNumber">AssemblyBuildNumber</see> property.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         The AssemblyBuildNumber can be set using several different methods. The
        ///         AssemblyBuildNumberType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file the default setting is
        ///     DateFormat. To override this set the <em>AssemblyBuildNumberType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyBuildNumberType&gt;DateFormat&lt;/AssemblyBuildNumberType&gt;
        /// &lt;AssemblyBuildNumberFormat&gt;yyMMdd&lt;/AssemblyBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyBuildNumberType
        {
            get { return rawAssemblyBuildNumberType; }
            set { rawAssemblyBuildNumberType = value; }
        }
        private string rawAssemblyBuildNumberType;

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyRevision">AssemblyRevision</see> property.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         The AssemblyRevision can be set using several different methods. The
        ///         AssemblyRevisionType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file the default setting is
        ///     AutoIncrement. To override this set the <em>AssemblyRevisionType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyRevisionType&gt;AutoIncrement&lt;/AssemblyRevisionType&gt;
        /// &lt;AssemblyRevisionFormat&gt;00&lt;/AssemblyRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyRevisionType
        {
            get { return rawAssemblyRevisionType; }
            set { rawAssemblyRevisionType = value; }
        }
        private string rawAssemblyRevisionType;

        /// <summary>
        /// The format string to apply when converting the build number to a text string.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         Use this property to control the formatting of the build number when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the build
        ///         number. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyBuildNumberType&gt;DateFormat&lt;/AssemblyBuildNumberType&gt;
        /// &lt;AssemblyBuildNumberFormat&gt;yyMMdd&lt;/AssemblyBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        public string AssemblyBuildNumberFormat
        {
            get { return assemblyVersionSettings.BuildNumberFormat; }
            set { assemblyVersionSettings.BuildNumberFormat = value; }
        }

        /// <summary>
        /// The format string to apply when converting the revision to a text string.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         Use this property to control the formatting of the revision when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the
        ///         revision. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyRevisionType&gt;AutoIncrement&lt;/AssemblyRevisionType&gt;
        /// &lt;AssemblyRevisionFormat&gt;00&lt;/AssemblyRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        public string AssemblyRevisionFormat
        {
            get { return assemblyVersionSettings.RevisionFormat; }
            set { assemblyVersionSettings.RevisionFormat = value; }
        }

        /// <summary>Returns the largest assembly version set by the task.</summary>
        /// <remarks>
        /// 	<para>Use this property to find out the largest assembly version that was generated
        ///     by the task. If only one assemblyinfo.* file was specified as an input, this will
        ///     be the resulting assembly version for that file. If more than one assemblyinfo.*
        ///     file was specified, this will be the largest build number generated.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file this value is placed in
        ///     <em>MaxAssemblyVersion</em> property after the UpdateAssemblyInfoFiles target is
        ///     run.</para>
        /// </remarks>
        [Output]
        public string MaxAssemblyVersion
        {
            get { return maxAssemblyVersion; }
            set { maxAssemblyVersion = value; }
        }
        private string maxAssemblyVersion;
        #endregion

        #region AssemblyFileVersion
        private AssemblyVersionSettings assemblyFileVersionSettings;

        /// <summary>
        /// The major version of the assembly file.
        /// </summary>
        /// <remarks>
        /// 	<para>To change the assembly file major version set this to the specific major version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "8".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyFileMajorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileMajorVersion&gt;8&lt;/AssemblyFileMajorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyFileMajorVersion
        {
            get { return assemblyFileVersionSettings.MajorVersion; }
            set { assemblyFileVersionSettings.MajorVersion = value; }
        }

        /// <summary>
        /// The minor version of the assembly file.
        /// </summary>
        /// <remarks>
        /// 	<para>To change the assembly file minor version set this to the specific minor version
        ///     you want. For example, for Visual Studio 2005 build 8.0.50727.42 this is set to
        ///     "0".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyFileMinorVersion</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileMinorVersion&gt;0&lt;/AssemblyFileMinorVersion&gt;
        ///     </code>
        /// </example>
        public string AssemblyFileMinorVersion
        {
            get { return assemblyFileVersionSettings.MinorVersion; }
            set { assemblyFileVersionSettings.MinorVersion = value; }
        }

        /// <summary>
        /// The build number of the assembly file.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         To change the assembly file build number set this to the specific build number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyFileBuildNumberType">AssemblyFileBuildNumberType</see> and
        ///         <see cref="AssemblyFileBuildNumberFormat">AssemblyFileBuildNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        /// 	<para>
        ///         To force the build number to a specific value when using the
        ///         Microsoft.VersionNumber.Targets, use the <em>AssemblyFileBuildNumber</em> property,
        ///         and set the <em>AssemblyFileBuildNumberFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileBuildNumber&gt;0&lt;/AssemblyFileBuildNumber&gt;
        /// &lt;AssemblyFileBuildNumberType&gt;DirectSet&lt;/AssemblyFileBuildNumberType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileBuildNumberType"/>
        /// <seealso cref="AssemblyFileBuildNumberFormat"/>
        public string AssemblyFileBuildNumber
        {
            get { return assemblyFileVersionSettings.BuildNumber; }
            set { assemblyFileVersionSettings.BuildNumber = value; }
        }

        /// <summary>
        /// The revision of the assembly file.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         To change the assembly file revision set this to the specific revision number you
        ///         want. In most cases you do not want to use this property. Instead, use the
        ///         <see cref="AssemblyFileRevisionType">AssemblyFileRevisionNumberType</see> and
        ///         <see cref="AssemblyFileRevisionFormat">AssemblyFileRevisionNumberFormat</see>
        ///         properties to have this value determined automatically at build time.
        ///     </para>
        /// 	<para>
        ///         To force the revision number to a specific value when using the
        ///         Microsoft.VersionNumber.Targets, set the <em>AssemblyFileRevision</em> property to
        ///         the value and set the <em>AssemblyFileRevisionFormat</em> property to
        ///         <see cref="IncrementMethod">DirectSet</see>.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileRevision&gt;0&lt;/AssemblyFileRevision&gt;
        /// &lt;AssemblyFileRevisionType&gt;DirectSet&lt;/AssemblyFileRevisionType&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyRevisionType"/>
        /// <seealso cref="AssemblyRevisionFormat"/>
        public string AssemblyFileRevision
        {
            get { return assemblyFileVersionSettings.Revision; }
            set { assemblyFileVersionSettings.Revision = value; }
        }


        /// <summary>
        /// The complete version of the assembly file.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         Use AssemblyFileVersion to directly set the entire file version number with a single
        ///         parameter. For example, if you know you want your version to be "1.2.3.4", you
        ///         can set AssemblyVersion to this instead of having to use each of the individual
        ///         <see cref="AssemblyFileMajorVersion">AssemblyFileMajorVersion</see>,
        ///         <see cref="AssemblyFileMinorVersion">AssemblyFileMinorVersion</see>,
        ///         <see cref="AssemblyFileBuildNumber">AssemblyFileBuildNumber</see>, and
        ///         <see cref="AssemblyFileRevision">AssemblyFileRevision</see> properties.
        ///     </para>
        /// 	<para>Note that the other four properties override this one. For example, If you
        ///     set AssemblyFileVersion to "1.2.3.4" and then set AssemblyFileMinorVersion to 6, the resulting
        ///     version will be "1.6.3.4".</para>
        /// 	<para>
        ///         This property is an input only. If you want to know what the final version
        ///         generated was, use the
        ///         <see cref="MaxAssemblyFileVersion">MaxAssemblyFileVersion</see> output property
        ///         instead.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file the best way to specify
        ///     this is to set the <em>AssemblyFileVersion</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileVersion&gt;1.2.3.4&lt;/AssemblyFileVersion&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="MaxAssemblyFileVersion"/>
        public string AssemblyFileVersion
        {
            get { return assemblyFileVersionSettings.Version; }
            set { assemblyFileVersionSettings.Version = value; }
        }

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyFileBuildNumber">AssemblyFileBuildNumber</see> property.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         The AssemblyFileBuildNumber can be set using several different methods. The
        ///         AssemblyFileBuildNumberType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file the default setting is
        ///     DateFormat. To override this set the <em>AssemblyFileBuildNumberType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileBuildNumberType&gt;DateFormat&lt;/AssemblyFileBuildNumberType&gt;
        /// &lt;AssemblyFileBuildNumberFormat&gt;yyMMdd&lt;/AssemblyFileBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileBuildNumberFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyFileBuildNumberType
        {
            get { return rawAssemblyFileBuildNumberType; }
            set { rawAssemblyFileBuildNumberType = value; }
        }
        private string rawAssemblyFileBuildNumberType;

        /// <summary>
        /// The type of update to use when setting the <see cref="AssemblyFileRevision">AssemblyFileRevision</see> property.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         The AssemblyFileRevision can be set using several different methods. The
        ///         AssemblyFileRevisionType property is used to select the desired method. The
        ///         supported types are defined in the
        ///         <see cref="IncrementMethod">IncrementMethod</see> enumeration.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file the default setting is
        ///     AutoIncrement. To override this set the <em>AssemblyFileRevisionType</em>
        ///     property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileRevisionType&gt;AutoIncrement&lt;/AssemblyFileRevisionType&gt;
        /// &lt;AssemblyFileRevisionFormat&gt;00&lt;/AssemblyFileRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileRevisionFormat"/>
        /// <seealso cref="IncrementMethod"/>
        public string AssemblyFileRevisionType
        {
            get { return rawAssemblyFileRevisionType; }
            set { rawAssemblyFileRevisionType = value; }
        }
        private string rawAssemblyFileRevisionType;

        /// <summary>
        /// The format string to apply when converting the file build number to a text string.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         Use this property to control the formatting of the file build number when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the file build
        ///         number. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileBuildNumberType&gt;DateFormat&lt;/AssemblyFileBuildNumberType&gt;
        /// &lt;AssemblyFileBuildNumberFormat&gt;yyMMdd&lt;/AssemblyFileBuildNumberFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyFileBuildNumberType"/>
        public string AssemblyFileBuildNumberFormat
        {
            get { return assemblyFileVersionSettings.BuildNumberFormat; }
            set { assemblyFileVersionSettings.BuildNumberFormat = value; }
        }

        /// <summary>
        /// The format string to apply when converting the file revision to a text string.
        /// </summary>
        /// <remarks>
        /// 	<para>
        ///         Use this property to control the formatting of the file revision when it is
        ///         converted from a number to a string. This is particularly useful when used in
        ///         conjunction with the <see cref="IncrementMethod">DateFormat</see> or
        ///         <see cref="IncrementMethod">AutoIncrement</see> methods of setting the file
        ///         revision. Any valid .NET formatting string can be specified.
        ///     </para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyFileRevisionType&gt;AutoIncrement&lt;/AssemblyFileRevisionType&gt;
        /// &lt;AssemblyFileRevisionFormat&gt;00&lt;/AssemblyFileRevisionFormat&gt;
        ///     </code>
        /// </example>
        /// <seealso cref="AssemblyBuildNumberType"/>
        public string AssemblyFileRevisionFormat
        {
            get { return assemblyFileVersionSettings.RevisionFormat; }
            set { assemblyFileVersionSettings.RevisionFormat = value; }
        }

        /// <summary>Returns the largest assembly file version set by the task.</summary>
        /// <remarks>
        /// 	<para>Use this property to find out the largest assembly file version that was generated
        ///     by the task. If only one assemblyinfo.* file was specified as an input, this will
        ///     be the resulting assembly file version for that file. If more than one assemblyinfo.*
        ///     file was specified, this will be the largest build number generated.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file this value is placed in
        ///     <em>MaxAssemblyFileVersion</em> property after the UpdateAssemblyInfoFiles target is
        ///     run.</para>
        /// </remarks>
        [Output]
        public string MaxAssemblyFileVersion
        {
            get { return maxAssemblyFileVersion; }
            set { maxAssemblyFileVersion = value; }
        }
        private string maxAssemblyFileVersion;       
        #endregion

        #region General Information
        private string assemblyTitle;
        /// <summary>The title of the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyTitleAttributeClassTopic.asp">
        ///     assembly title</a> set this to the specific title you want. For example, for Visual
        ///     Studio 2005 this is set to "Microsoft® Visual Studio® 2005".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyTitle</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild" title="[New Example]">
        /// &lt;AssemblyTitle&gt;Microsoft® Visual Studio® 2005&lt;/AssemblyTitle&gt;
        ///     </code>
        /// </example>
        public string AssemblyTitle
        {
            get { return assemblyTitle; }
            set { assemblyTitle = value; }
        }

        private string assemblyDescription;
        /// <summary>The description of the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyDescriptionAttributeClassTopic.asp">
        ///     assembly description</a> set this to the specific description you want. For
        ///     example, for Visual Studio 2005 this is set to "Microsoft Visual Studio
        ///     2005".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyDescription</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyDescription&gt;Microsoft Visual Studio 2005&lt;/AssemblyDescription&gt;
        ///     </code>
        /// </example>
        public string AssemblyDescription
        {
            get { return assemblyDescription; }
            set { assemblyDescription = value; }
        }

        private string assemblyConfiguration;
        /// <summary>The configuration of the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemreflectionassemblyconfigurationattributeclasstopic.asp">
        ///     assembly configuration text</a> set this to the specific configuration text you
        ///     want.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyConfiguration</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyConfiguration&gt;Debug&lt;/AssemblyConfiguration&gt;
        ///     </code>
        /// </example>
        public string AssemblyConfiguration
        {
            get { return assemblyConfiguration; }
            set { assemblyConfiguration = value; }
        }

        private string assemblyCompany;
        /// <summary>The company that created the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyCompanyAttributeClassTopic.asp">
        ///     assembly company</a> set this to the specific company name you want. For example,
        ///     for Visual Studio 2005 this is set to "Microsoft Corporation".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyCompany</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyCompany&gt;Microsoft Corporation&lt;/AssemblyCompany&gt;
        ///     </code>
        /// </example>
        public string AssemblyCompany
        {
            get { return assemblyCompany; }
            set { assemblyCompany = value; }
        }

        private string assemblyProduct;
        /// <summary>The product name of the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyCompanyAttributeClassTopic.asp">
        ///     assembly company</a> set this to the specific company name you want. For example,
        ///     for Visual Studio 2005 assemblies this is set to "Microsoft® Visual Studio®
        ///     2005".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyProduct</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyProduct&gt;Microsoft® Visual Studio® 2005&lt;/AssemblyProduct&gt;
        ///     </code>
        /// </example>
        public string AssemblyProduct
        {
            get { return assemblyProduct; }
            set { assemblyProduct = value; }
        }

        private string assemblyCopyright;
        /// <summary>The copyright information for the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyCopyrightAttributeClassTopic.asp">
        ///     assembly copyright</a> set this to the specific copyright text you want. For
        ///     example, for Visual Studio 2005 assemblies this is set to "© Microsoft Corporation.
        ///     All rights reserved.".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyCopyright</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyCopyright&gt;© Microsoft Corporation. All rights reserved.&lt;/AssemblyCopyright&gt;
        ///     </code>
        /// </example>
        public string AssemblyCopyright
        {
            get { return assemblyCopyright; }
            set { assemblyCopyright = value; }
        }

        private string assemblyTrademark;
        /// <summary>The trademark information for the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyTrademarkAttributeClassTopic.asp">
        ///     assembly trademark</a> set this to the specific trademark text you want.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyTrademark</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyTrademark&gt;Microsoft Corporation&lt;/AssemblyTrademark&gt;
        ///     </code>
        /// </example>
        public string AssemblyTrademark
        {
            get { return assemblyTrademark; }
            set { assemblyTrademark = value; }
        }

        private string assemblyCulture;
        /// <summary>The culture information for the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     <a href="http://msdn.microsoft.com/library/en-us/cpref/html/frlrfSystemReflectionAssemblyCultureAttributeClassTopic.asp">
        ///     assembly culture</a> set this to the specific culture text you want. For example,
        ///     for the English satellite resources this is set to "en".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyCulture</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyCulture&gt;en&lt;/AssemblyCulture&gt;
        ///     </code>
        /// </example>
        public string AssemblyCulture
        {
            get { return assemblyCulture; }
            set { assemblyCulture = value; }
        }

        /// <summary>The GUID for the assembly.</summary>
        /// <remarks>
        /// 	<para>To change the
        ///     GUID for the assembly set this to the specific GUID you want.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyGuid</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyGuid&gt;56269a04-c55a-4c5a-92ba-dfdb569bc708&lt;/AssemblyGuid&gt;
        ///     </code>
        /// </example>
        public string AssemblyGuid
        {
            get { return assemblyGuid; }
            set { assemblyGuid = value; }
        }
        private string assemblyGuid;
        #endregion

        #region Strong Name

        private bool assemblyIncludeSigningInformation;
        /// <summary>Controls whether assembly signing information is replaced in the AssemblyInfo files.</summary>
        /// <remarks>
        /// 	<para>
        ///         This property controls whether the
        ///         <see cref="AssemblyDelaySign">AssemblyDelaySign</see>,
        ///         <see cref="AssemblyKeyFile">AssemblyKeyFile</see> and
        ///         <see cref="AssemblyKeyName">AssemblyKeyName</see> properties are written out to
        ///         the assembly info files. In order for either of those three properties to
        ///         persist, AssemblyIncludeSigningInformation must be set to true.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyIncludeSigningInformation</em> property. By default this is set to
        ///     <em>false</em>.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyIncludeSigningInformation&gt;true&lt;/AssemblyIncludeSigningInformation&gt;
        /// &lt;AssemblyDelaySign&gt;true&lt;/AssemblyDelaySign&gt;
        ///     </code>
        /// </example>
        public bool AssemblyIncludeSigningInformation
        {
            get { return assemblyIncludeSigningInformation; }
            set { assemblyIncludeSigningInformation = value; }
        }

        private string assemblyDelaySign;
        /// <summary>Controls delay signing of the assembly.</summary>
        /// <remarks>
        /// 	<para>To enable delay signing of the assembly set this property to "true".</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyDelaySign</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyDelaySign&gt;false&lt;/AssemblyDelaySign&gt;
        ///     </code>
        /// </example>
        public string AssemblyDelaySign
        {
            get { return assemblyDelaySign; }
            set { assemblyDelaySign = value; }
        }

        private string assemblyKeyFile;
        /// <summary>Specifies the key file used to sign the assembly.</summary>
        /// <remarks>
        /// 	<para>To specify the key file used to sign the compiled assembly set this to the file name of the key file.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyKeyFile</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyKeyFile&gt;c:\key.snk&lt;/AssemblyKeyFile&gt;
        ///     </code>
        /// </example>
        public string AssemblyKeyFile
        {
            get { return assemblyKeyFile; }
            set { assemblyKeyFile = value; }
        }

        private string assemblyKeyName;
        /// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
        /// <remarks>
        /// 	<para>To specify the key used to sign the compiled assembly set this to the name of the key container.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyKeyName</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyKeyName&gt;myContainer&lt;/AssemblyKeyName&gt;
        ///     </code>
        /// </example>
        public string AssemblyKeyName
        {
            get { return assemblyKeyName; }
            set { assemblyKeyName = value; }
        }
        #endregion

        #region COM
        private string comVisible;
        /// <summary>Specifies whether the assembly is visible to COM.</summary>
        /// <remarks>
        /// 	<para>
        ///         To specify whether the assembly shoul be visible to COM set this to true and
        ///         provide a valid GUID using the <see cref="AssemblyGuid">AssemblyGuid</see>
        ///         property. The default value is <em>null</em>.
        ///     </para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file set this using the
        ///     <em>AssemblyComVisible</em> property.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSBuild">
        /// &lt;AssemblyComVisible&gt;myContainer&lt;/AssemblyComVisible&gt;
        ///     </code>
        /// </example>
        public string ComVisible
        {
            get { return comVisible; }
            set { comVisible = value; }
        }

        #endregion

        #region MSBuild Properties
        /// <summary>
        /// Specifies the list of AssemblyInfo files the task should update.
        /// </summary>
        /// <remarks>
        /// 	<para>Use the AssemblyInfoFile property to provide the task with the list of AssemblyInfo files that should
        /// be updated by the task. This can be a mix of VB, C# and J# AssemblyInfo Files.</para>
        /// 	<para>When using the Microsoft.VersionNumber.Targets file add items to the AssemblyInfoFiles item group
        /// to have them processed by the task.</para>
        /// </remarks>
        /// <example>
        /// 	<code lang="MSbuild">
        /// &lt;!-- Add all AssemblyInfo files in all sub-directories to the list of
        /// files that should be processed by the task --&gt;
        /// &lt;ItemGroup&gt;
        ///     &lt;AssemblyInfoFiles&gt;**\AssemblyInfo.*&lt;/AssemblyInfoFiles&gt;
        /// &lt;/ItemGroup&gt;
        ///     </code>
        /// </example>
        [Required]        
        public ITaskItem[] AssemblyInfoFiles
        {
            get { return assemblyInfoFiles; }
            set { assemblyInfoFiles = value; }
        }
        private ITaskItem[] assemblyInfoFiles;
        #endregion

        #region Methods
        /// <summary>
        /// Executes the AssemblyInfo task.
        /// </summary>
        /// <returns>True if the task was run sucecssfully. False if the task failed.</returns>
        public override bool Execute()
        {
            FileInfo writerInfo = null;
            StreamWriter writer = null;

            // Try and parse all the increment properties to ensure they are valid for the increment enum. If not,
            // bail out.
            if (!this.ParseIncrementProperties())
            {
                return false;
            }

            // Validate that the enum values set match with what was passed into the associated parameters. If not,
            // bail out.
            if (!this.ValidateIncrementProperties())
            {
                return false;
            }

            // Set the max versions before running through the loop
            this.MaxAssemblyVersion = "0.0.0.0";
            this.MaxAssemblyFileVersion = "0.0.0.0";

            foreach (ITaskItem item in AssemblyInfoFiles)
            {
                AssemblyInfoWrapper assemblyInfo = new AssemblyInfoWrapper(item.ItemSpec);
                Version versionToUpdate;

                // Validate that stub file entries exist for any of the properties we've been asked to set.
                if (!this.ValidateFileEntries(assemblyInfo, item.ItemSpec))
                {
                    return false;
                }

                Log.LogMessage(MessageImportance.Low, "Updating assembly info for {0}", item.ItemSpec);

                versionToUpdate = new Version(assemblyInfo["AssemblyVersion"]);
                this.UpdateAssemblyVersion(ref versionToUpdate, assemblyVersionSettings);
                assemblyInfo["AssemblyVersion"] = versionToUpdate.ToString(); ;
                this.UpdateMaxVersion(ref this.maxAssemblyVersion, assemblyInfo["AssemblyVersion"]);

                if (assemblyInfo["AssemblyFileVersion"] == null)
                {
                    Log.LogMessage("Unable to update the AssemblyFileVersion for {0}: No stub entry for AssemblyFileVersion was found in the AssemblyInfo file.", item.ItemSpec);
                }
                else
                {
                    //try
                    //{
                        versionToUpdate = new Version(assemblyInfo["AssemblyFileVersion"]);
                        this.UpdateAssemblyVersion(ref versionToUpdate, assemblyFileVersionSettings);
                        assemblyInfo["AssemblyFileVersion"] = versionToUpdate.ToString();
                        this.UpdateMaxVersion(ref this.maxAssemblyFileVersion, assemblyInfo["AssemblyFileVersion"]);
                    //}
                    //catch(Exception ex)
                    //{
                    //    Log.LogWarning("Unable to update the AssemblyFileVersion for {0}: Exception message: {1}.", item.ItemSpec, ex.Message);
                    //    versionToUpdate = new Version(assemblyInfo["AssemblyVersion"]);
                    //    this.UpdateAssemblyVersion(ref versionToUpdate, assemblyFileVersionSettings);
                    //    assemblyInfo["AssemblyFileVersion"] = versionToUpdate.ToString();
                    //}
                }

                this.UpdateProperty(assemblyInfo, "AssemblyTitle");
                this.UpdateProperty(assemblyInfo, "AssemblyDescription");
                this.UpdateProperty(assemblyInfo, "AssemblyConfiguration");
                this.UpdateProperty(assemblyInfo, "AssemblyCompany");
                this.UpdateProperty(assemblyInfo, "AssemblyProduct");
                this.UpdateProperty(assemblyInfo, "AssemblyCopyright");
                this.UpdateProperty(assemblyInfo, "AssemblyTrademark");
                this.UpdateProperty(assemblyInfo, "AssemblyCulture");
                this.UpdateProperty(assemblyInfo, "AssemblyGuid");
                if (this.AssemblyIncludeSigningInformation)
                {
                    this.UpdateProperty(assemblyInfo, "AssemblyKeyName");
                    this.UpdateProperty(assemblyInfo, "AssemblyKeyFile");
                    this.UpdateProperty(assemblyInfo, "AssemblyDelaySign");
                }
                this.UpdateProperty(assemblyInfo, "ComVisible");
                
                try
                {
                    writerInfo = this.GetTemporaryFileInfo();
                    writer = new StreamWriter(writerInfo.OpenWrite(), Encoding.Unicode);
                    assemblyInfo.Write(writer);
                    writer.Close();
                    File.Copy(writerInfo.FullName, item.ItemSpec, true);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                    if (writerInfo != null)
                    {
                        writerInfo.Delete();
                    }
                }
            }
            return true;
        }
        #endregion

        #region Update Methods
        private void UpdateAssemblyVersion(ref Version versionToUpdate, AssemblyVersionSettings requestedVersion)
        {
            // The string version of the assembly goes first, so the others can override it.
            if (requestedVersion.Version != null)
            {
                Log.LogMessage(MessageImportance.Low, "\tUpdating assembly version to {0}", requestedVersion.Version);
                versionToUpdate.VersionString = requestedVersion.Version;
            }

            if (requestedVersion.MajorVersion != null)
            {
                Log.LogMessage(MessageImportance.Low, "\tUpdating major version to {0}", requestedVersion.MajorVersion);
                versionToUpdate.MajorVersion = requestedVersion.MajorVersion;
            }

            if (requestedVersion.MinorVersion != null)
            {
                Log.LogMessage(MessageImportance.Low, "\tUpdating minor version to {0}", requestedVersion.MinorVersion);
                versionToUpdate.MinorVersion = requestedVersion.MinorVersion;
            }

            // The BuildNumber and Revision updates are closely related when the BuildNumber updates daily and
            // the Revision updates on every build. It's important to ensure that the Revision resets to 0
            // when the BuildNumber flips across to a new day.
            string originalBuildNumber = "";
            bool handleSpecialInteraction = (requestedVersion.BuildNumberType == IncrementMethod.DateString) &&
                                            (requestedVersion.RevisionType == IncrementMethod.AutoIncrement);
            
            if (handleSpecialInteraction)
            {
                originalBuildNumber = versionToUpdate.BuildNumber;
            }

            // Go ahead and update the BuildNumber. After this is done we'll see if it's different than it
            // was when we started, and then handle the Revision as necessary.
            versionToUpdate.BuildNumber = this.UpdateVersionProperty(versionToUpdate.BuildNumber,
                requestedVersion.BuildNumberType,
                requestedVersion.BuildNumber,
                requestedVersion.BuildNumberFormat,
                "\tUpdating build number to {0}");

            // If we're in the special situation of DateString for BuildNumber and AutoIncrement for Revision
            // check and see if the BuildNumber changed, indicating we're on a new day. If so tweak the
            // Revision so when the AutoIncrement on it happens the value will become 0.
            if (handleSpecialInteraction && (originalBuildNumber != versionToUpdate.BuildNumber))
            {
                versionToUpdate.Revision = "-1";
            }

            versionToUpdate.Revision = this.UpdateVersionProperty(versionToUpdate.Revision, requestedVersion.RevisionType,
                requestedVersion.Revision,
                requestedVersion.RevisionFormat,
                "\tUpdating revision number to {0}");

            Log.LogMessage(MessageImportance.Low, "\tFinal assembly version is {0}", versionToUpdate.ToString());
        }

        private void UpdateProperty(AssemblyInfoWrapper assemblyInfo, string propertyName)
        {
            PropertyInfo propInfo = this.GetType().GetProperty(propertyName);
            string value = (string)propInfo.GetValue(this, null);

            if (value != null)
            {
                assemblyInfo[propertyName] = value;
                Log.LogMessage(MessageImportance.Low, "\tUpdating {0} to \"{1}\"", propertyName, value);
            }
        }

        private string UpdateVersionProperty(string versionNumber, IncrementMethod method, string value, string format, string logMessage)
        {
            Log.LogMessage(MessageImportance.Low, "\tUpdate method is {0}", method.ToString());
            if (format == "")
            {
                format = "0";
            }

            switch (method)
            {
                case IncrementMethod.NoIncrement:
                    {
                        if (value == null)
                        {
                            return versionNumber;
                        }
                        Log.LogMessage(MessageImportance.Low, logMessage, value);
                        return value;
                    }
                case IncrementMethod.AutoIncrement:
                    {
                        int newVersionNumber = int.Parse(versionNumber);
                        newVersionNumber++;
                        Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber.ToString(format));
                        return newVersionNumber.ToString(format);
                    }
                case IncrementMethod.DateString:
                    {
                        string newVersionNumber = DateTime.Now.ToString(format);
                        Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber);
                        return newVersionNumber;
                    }
                case IncrementMethod.YearDayString:
                    {
                        DateTime now = DateTime.Now;
                        int newVersionInt = ((now.Year - 2000) * 1000) + now.DayOfYear;
                        string newVersionNumber = newVersionInt.ToString();
                        Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber);
                        return newVersionNumber;
                    }
                case IncrementMethod.YearWeekDayString:
                    {
                        DateTime now = DateTime.Now;
                        Calendar cal = CultureInfo.InvariantCulture.Calendar;
                        int week = cal.GetWeekOfYear(now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                        int newVersionInt = ((now.Year - 2000) * 1000) + (week * 10) + (int) now.DayOfWeek;
                        string newVersionNumber = newVersionInt.ToString();
                        Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber);
                        return newVersionNumber;
                    }
                case IncrementMethod.DaysSinceDate:
                    {
                        DateTime now = DateTime.Now;
                        DateTime since;
                        if (DateTime.TryParse(format, out since))
                        {
                            int newVersionNumberInt = (int)new TimeSpan(now.Ticks - since.Ticks).TotalDays;
                            string newVersionNumber = newVersionNumberInt.ToString();
                            Log.LogMessage(MessageImportance.Low, logMessage, newVersionNumber);
                            return newVersionNumber;
                        }
                        else
                        {
                            throw new ArgumentException("Format string does not specify a valid date");
                        }
                    }
                default:
                    {
                        return "";
                    }
            }
        }
        #endregion

        #region Helpers
        private FileInfo GetTemporaryFileInfo()
        {
            string tempFileName;
            FileInfo myFileInfo;
            try
            {
                tempFileName = Path.GetTempFileName();
                myFileInfo = new FileInfo(tempFileName);
                myFileInfo.Attributes = FileAttributes.Temporary;
            }
            catch (Exception e)
            {
                Log.LogError("Unable to create temporary file: {0}", e.Message);
                return null;
            }

            return myFileInfo;
        }

        private void UpdateMaxVersion(ref string maxVersion, string newVersion)
        {
            if (string.IsNullOrEmpty( newVersion ))
            {
                return;
            }

            bool badMax = false;
            System.Version versionZero = new System.Version(0,0,0,0);
            System.Version candidate = null;
            System.Version max = null;
            try
            {
                candidate = new System.Version(newVersion);
            }
            catch
            {
                candidate = (System.Version) versionZero.Clone();
            }
            try
            {
                max = new System.Version(maxVersion);
            }
            catch
            {
                badMax = true;
                max = (System.Version) versionZero.Clone();
            }

            if ((candidate > max) || badMax)
            {
                maxVersion = candidate.ToString(4);
            }
        }
        #endregion

        #region Validation Methods
        // This converts all the string properties to one of the valid enum values. If any of them fail it logs an error to the console and
        // returns false.
        private bool ParseIncrementProperties()
        {
            string enumNames = String.Join(", ", Enum.GetNames(typeof(IncrementMethod)));

            // Handle AssemblyBuildNumberType
            if (this.AssemblyBuildNumberType == null)
            {
                this.assemblyVersionSettings.BuildNumberType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), this.AssemblyBuildNumberType))
                {
                    Log.LogError("The value specified for AssemblyBuildNumberType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }
                this.assemblyVersionSettings.BuildNumberType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), this.AssemblyBuildNumberType);
            }

            // Handle AssemblyRevisionNumberType
            if (this.AssemblyRevisionType == null)
            {
                this.assemblyVersionSettings.RevisionType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), this.AssemblyRevisionType))
                {
                    Log.LogError("The value specified for AssemblyRevisionType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }
                this.assemblyVersionSettings.RevisionType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), this.AssemblyRevisionType);
            }

            // Handle AssemblyFileBuildNumberType
            if (this.AssemblyFileBuildNumberType == null)
            {
                this.assemblyFileVersionSettings.BuildNumberType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), this.AssemblyFileBuildNumberType))
                {
                    Log.LogError("The value specified for AssemblyFileBuildNumberType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }
                this.assemblyFileVersionSettings.BuildNumberType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), this.AssemblyFileBuildNumberType);
            }

            // Handle AssemblyFileRevisionType
            if (this.AssemblyFileRevisionType == null)
            {
                this.assemblyFileVersionSettings.RevisionType = IncrementMethod.NoIncrement;
            }
            else
            {
                if (!Enum.IsDefined(typeof(IncrementMethod), this.AssemblyFileRevisionType))
                {
                    Log.LogError("The value specified for AssemblyFileRevisionType is invalid. It must be one of: {0}", enumNames);

                    return false;
                }
                this.assemblyFileVersionSettings.RevisionType = (IncrementMethod)Enum.Parse(typeof(IncrementMethod), this.AssemblyFileRevisionType);
            }

            return true;
        }

        private bool ValidateIncrementProperties()
        {
            if ((this.assemblyVersionSettings.BuildNumberType == IncrementMethod.DateString) && (this.assemblyVersionSettings.BuildNumberFormat == null))
            {
                Log.LogError("The version increment method for AssemblyBuildNumber was set to DateString, but AssemblyBuildNumberFormat was not specified. Both properties must be set to use a date string as a build number.");
                return false;
            }

            if ((this.assemblyVersionSettings.RevisionType == IncrementMethod.DateString) && (this.assemblyVersionSettings.RevisionFormat == null))
            {
                Log.LogError("The version increment method for AssemblyRevision was set to DateString, but AssemblyRevisionFormat was not specified. Both properties must be set to use a date string as a revision.");
                return false;
            }

            if ((this.assemblyFileVersionSettings.BuildNumberType == IncrementMethod.DateString) && (this.AssemblyFileBuildNumberFormat == null))
            {
                Log.LogError("The version increment method for AssemblyFileBuildNumber was set to DateString, but AssemblyFileBuildNumberFormat was not specified. Both properties must be set to use a date string as a build number.");
                return false;
            }

            if ((this.assemblyFileVersionSettings.RevisionType == IncrementMethod.DateString) && (this.AssemblyFileRevisionFormat == null))
            {
                Log.LogError("The version increment method for AssemblyFileRevision was set to DateString, but AssemblyFileRevisionFormat was not specified. Both properties must be set to use a date string as a revision.");
                return false;
            }
            return true;
        }

        // There's an inherent limitation to this task in that it can only replace content for attributes
        // already present in the assemblyinfo file. If the stub isn't there then it can't be set. This method
        // goes through and validates that a stub is present in the file for any of the properties that were set
        // on the task.
        private bool ValidateFileEntries(AssemblyInfoWrapper assemblyInfo, string filename)
        {
            if (((this.AssemblyBuildNumber != null) ||
                (this.AssemblyRevision != null) ||
                (this.AssemblyMajorVersion != null) ||
                (this.AssemblyMinorVersion != null)) &&
                (assemblyInfo["AssemblyVersion"] == null))
            {
                Log.LogError("Unable to update the AssemblyVersion for {0}: No stub entry for AssemblyVersion was found in the AssemblyInfo file.", filename);
                return false;
            }

            if (((this.AssemblyFileBuildNumber != null) ||
                (this.AssemblyFileRevision != null) ||
                (this.AssemblyFileMajorVersion != null) ||
                (this.AssemblyFileMinorVersion != null)) &&
                (assemblyInfo["AssemblyFileVersion"] == null))
            {
                Log.LogMessage("Unable to update the AssemblyFileVersion for {0}: No stub entry for AssemblyFileVersion was found in the AssemblyInfo file.", filename);
            }

            if (!this.ValidateFileEntry(this.AssemblyCompany, assemblyInfo, "AssemblyCompany", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyConfiguration, assemblyInfo, "AssemblyConfiguration", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyCopyright, assemblyInfo, "AssemblyCopyright", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyCulture, assemblyInfo, "AssemblyCulture", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyDescription, assemblyInfo, "AssemblyDescription", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyProduct, assemblyInfo, "AssemblyProduct", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyTitle, assemblyInfo, "AssemblyTitle", filename)) return false;
            if (!this.ValidateFileEntry(this.AssemblyTrademark, assemblyInfo, "AssemblyTrademark", filename)) return false;

            if (this.AssemblyIncludeSigningInformation)
            {
                if (!this.ValidateFileEntry(this.AssemblyDelaySign, assemblyInfo, "AssemblyDelaySign", filename)) return false;
                if (!this.ValidateFileEntry(this.AssemblyKeyFile, assemblyInfo, "AssemblyKeyFile", filename)) return false;
                if (!this.ValidateFileEntry(this.AssemblyKeyName, assemblyInfo, "AssemblyKeyName", filename)) return false;
            }
            return true;
        }

        // This validates a single attribute in the file given the value passed into the task, and the file attribute to look up.
        // The filename is only used for making the error message pretty.
        private bool ValidateFileEntry(string taskAttributeValue, AssemblyInfoWrapper assemblyInfo, string fileAttribute, string filename)
        {
            if ((taskAttributeValue != null) && (assemblyInfo[fileAttribute] == null))
            {
                Log.LogError("Unable to update the {0} for {1}: No stub entry for {0} was found in the AssemblyInfo file.", fileAttribute, filename);
                return false;
            }

            return true;
        }
        #endregion
    }

    /// <summary>
    /// Specifies how certain version numbers are incremented by the task.
    /// </summary>
    public enum IncrementMethod
    {
        /// <summary>
        /// Do not auto-increment the number.
        /// </summary>
        NoIncrement = 0,
        /// <summary>
        /// Add one to the current number.
        /// </summary>
        AutoIncrement = 1,
        /// <summary>
        /// Format the current date and time using a formatting string, and use that as the number.
        /// </summary>
        DateString = 2,
        /// <summary>
        /// Format the current date and time using the format YYddd, where YY is the two-digit year and ddd the day of the year.
        /// </summary>
        YearDayString = 3,
        /// <summary>
        /// Format the current date and time using the format YYWWd where YY is the two-digit year, WW the week number in the year, and d the day of the week.
        /// </summary>
        YearWeekDayString = 4,
        /// <summary>
        /// The number of days since a specific base date, specified in the format field.
        /// </summary>
        DaysSinceDate = 5
    }
}

