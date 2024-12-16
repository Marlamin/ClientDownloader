using System.Collections.Generic;

namespace ClientDownloader
{

    public static class MFILMap
    {
        public static Dictionary<string, (string folder, string file)> Entries = new();

        static MFILMap()
        {
            // TODO: Handle difference between Beta/PTR/Retail? MPQs are probably similar, need to confirm.

            var edgeSuite = "http://dist.blizzard.com.edgesuite.net";
            var edgeSuiteWoWPod = edgeSuite + "/wow-pod/";
            var edgeSuiteWoWPodRetail12911 = edgeSuite + "/wow-pod-retail/NA/12911.streaming/";
            var edgeSuiteWoWPodPTR15050Direct = edgeSuite + "/wow-pod-ptr/NA/15050.direct/";
            var edgeSuiteWoWPodPTRStreaming = edgeSuite + "/wow-pod/ptr/streaming/";

            var beta12635to13277 = "wowb-13316-F8273658453AC6F0AA8E4B3F053F3605.mfil"; // For 4.0.0.12635 Beta to 4.0.3.13277 Beta.

            // 4.0.0.11927 (non-streaming)
            // 4.0.0.12025 (non-streaming)
            // 4.0.0.12065 (non-streaming)
            // 4.0.0.12122 (non-streaming)
            // 4.0.0.12164 (non-streaming)
            // 4.0.0.12232 (non-streaming)
            // 4.0.0.12266 (non-streaming)
            // 4.0.0.12319 (non-streaming)
            // 4.0.0.12479 (non-streaming)
            // 4.0.0.12539 (non-streaming)
            // 4.0.0.12604 (non-streaming)
            Entries.Add("4.0.0.12635", (edgeSuiteWoWPod, "wow-12635-AC33369C269F573F9E271A6AE4826B44.mfil")); // First streaming
            // 4.0.0.12644 (non-streaming)
            Entries.Add("4.0.0.12694", (edgeSuiteWoWPod, "wow-12694-E4AA1A6453BAE6A5F7AB6D72BE078503.mfil"));
            Entries.Add("4.0.0.12759", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.1.12803", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.0.12824 (PTR)
            Entries.Add("4.0.1.12857", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.0.12911", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.1.12941 (PTR)
            Entries.Add("4.0.1.12942", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.1.12984", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.1.13033 (PTR)
            Entries.Add("4.0.1.13066", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.1.13082 (PTR)
            Entries.Add("4.0.3.13117", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.1.13131 (PTR)
            // TODO: 4.0.1.13156 (PTR)
            Entries.Add("4.0.1.13164", (edgeSuiteWoWPodRetail12911, "wow-13164-F957D8BA4CEB6DA5176924823EB84DC6.mfil"));
            Entries.Add("4.0.3.13189", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.3.13195", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.3.13202", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.1.13205", (edgeSuiteWoWPodRetail12911, "wow-13205-AF144C9A42AE61E3409B0B61ED9A1431.mfil"));
            Entries.Add("4.0.3.13221", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.3.13224 (PTR)
            Entries.Add("4.0.3.13241", (edgeSuiteWoWPod, beta12635to13277));
            // TODO: 4.0.3.13245 (PTR)
            Entries.Add("4.0.3.13277", (edgeSuiteWoWPod, beta12635to13277));
            Entries.Add("4.0.3.13287", (edgeSuiteWoWPodRetail12911, "wow-13287-5A72F40D9DB758770FDD93F8028F7FE0.mfil"));
            Entries.Add("4.0.3.13316", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil"));
            Entries.Add("4.0.3.13329", (edgeSuiteWoWPodRetail12911, "wow-13329-0658AFCDDF4D212A872B108F25947FA4.mfil")); // 4.0.3a
            Entries.Add("4.0.6.13449", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.0.6.13482", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.0.6.13529", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.0.6.13561", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.0.6.13596", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil"));
            Entries.Add("4.0.6.13623", (edgeSuiteWoWPodRetail12911, "wow-13623-564D07B657C3DD8DDAEAF5B8C08536F3.mfil")); // 4.0.6a
            Entries.Add("4.1.0.13682", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13698", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13707", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13726", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13750", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13793", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13812", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13850", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            // TODO: 4.1.0.13860 (PTR), only a binary can be found on the web, no other information.
            Entries.Add("4.1.0.13875", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.13914", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil"));
            Entries.Add("4.2.0.14002", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.1.0.14007", (edgeSuiteWoWPodRetail12911, "wow-15005-FC50178B8E64FB01EB625628534881B4.mfil"));
            Entries.Add("4.2.0.14040", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14107", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14133", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14179", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14199", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14241", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14265", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14288", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14299", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14313", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.0.14316", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil"));
            Entries.Add("4.2.0.14333", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil"));
            Entries.Add("4.2.0.14480", (edgeSuiteWoWPodRetail12911, "wow-15005-FC50178B8E64FB01EB625628534881B4.mfil"));
            Entries.Add("4.2.2.14492", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.2.14505", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.2.14522", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.2.14534", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.2.2.14545", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil"));
            Entries.Add("4.3.0.14732", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14791", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14809", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14849", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14890", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14899", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14911", (edgeSuiteWoWPodPTRStreaming, "wowt-14911-6782F82C7AD806552F22ACDD6DF827EE.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14942", (edgeSuiteWoWPodPTRStreaming, "wowt-14942-CEF55A80CF9E62676453BACB0C2C2E05.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14946", (edgeSuiteWoWPodRetail12911, "wow-14946-E98616EC2137375CAC8291D208910663.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14966", (edgeSuiteWoWPodPTRStreaming, "wowt-14966-40B80FC90C5F85FC096F7A2F2718726A.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14976", (edgeSuiteWoWPodPTRStreaming, "wowt-14976-21614E7AF60E525DF642F0CD99B2FEED.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14980", (edgeSuiteWoWPodPTRStreaming, "wowt-14980-A9D53EFBCD7C6DC4075D55A8383FB2AD.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14995", (edgeSuiteWoWPodPTRStreaming, "wowt-14995-3CE1ED3E8C82B817D95DF82427EFD7E0.mfil")); // TODO: Binaries
            Entries.Add("4.3.0.14995", (edgeSuiteWoWPodRetail12911, "wow-15005-FC50178B8E64FB01EB625628534881B4.mfil")); // TODO: Binaries
            // TODO: 4.3.0.15050 (Retail) (4.3.0a) wow-pod-retail\NA\15050.direct\wow-15595-1C2E37918CF0DA8026178526559CA53F.mfil // 4.3.0a
            // TODO: 4.3.2.15148 (PTR) wow-pod\public-test\15050.direct\wowt-15148-1428DDB563809F9BE3D30CD6A9882C87.mfil
            // TODO: 4.3.2.15171 (PTR) wow-pod\public-test\15050.direct\wowt-15171-C90366C4D2486D614A7C833F22D399E6.mfil
            // TODO: 4.3.2.15201 (PTR) wow-pod\public-test\15050.direct\wowt-15201-DCC97C333B2BCE05681A825B6D08C7CA.mfil
            // TODO: 4.3.2.15211 (PTR/Retail) wow-pod\public-test\15050.direct\wowt-15531-3A579212DAB168D4992BB7955F659BCB.mfil
            // TODO: 4.3.3.15314 (PTR) wow-pod\public-test\15050.direct\wowt-15314-B4DB1A05F07C9D01FE7FBE6A6AD036B8.mfil
            // TODO: 4.3.3.15338 (PTR) wow-pod\public-test\15050.direct\wowt-15338-41119C49129EF66B8693DEC15D21621D.mfil
            // TODO: 4.3.3.15354 (PTR/Retail) wow-pod\public-test\15050.direct\wowt-15531-3A579212DAB168D4992BB7955F659BCB.mfil
            // TODO: 4.3.4.15499 (PTR) wow-pod\public-test\15050.direct\wowt-15499-BEF6B71229265F16BAC82BF858918443.mfil
            // TODO: 4.3.4.15531 (PTR) wow-pod\public-test\15050.direct\wowt-15531-3A579212DAB168D4992BB7955F659BCB.mfil
            // TODO: 4.3.4.15595 (PTR/Retail) wow-pod-retail\NA\15050.direct\wow-15595-1C2E37918CF0DA8026178526559CA53F.mfil
        }
    }
}
