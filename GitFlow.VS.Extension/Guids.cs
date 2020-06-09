using System;

namespace GitFlowWithPRVS.Extension
{
    static class GuidList
    {
        public const string GuidGitFlowVsExtensionPkgString = "DDD4D5A1-DC39-46EF-A0B7-F7C813DF2B38";
        public const string GuidGitFlowVsExtensionCmdSetString = "B9275284-B14F-44AC-93D9-59D19B25A7F9";

        public static readonly Guid GuidGitFlowVsExtensionCmdSet = new Guid(GuidGitFlowVsExtensionCmdSetString);

        public const string GitFlowPage = "75EC888F-7549-4D9C-9967-4869A9AEDDDD";
        public const string GitFlowActionSection = "6DACB452-3D08-492E-892B-E54E87456888";
        public const string GitFlowFeaturesSection = "91A711C0-CF48-4354-9005-266E0CF9A4EC";
        public const string GitFlowBugfixesSection = "9DB1FDD4-F8A6-42C7-8B2B-809653E3D51B";
        public const string GitFlowInitSection = "DB36B641-8E55-43A5-A48B-6C7A0A6D4C7C";
        public const string GitFlowInstallSection = "AC65A7C4-09B7-4106-94F9-6B50FFDD3F60";
		public const string GitFlowReleasesSection = "A5FDCC07-1924-4AB5-BA2D-F0E28EF116F7";
	};
}