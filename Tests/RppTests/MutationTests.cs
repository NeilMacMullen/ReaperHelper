using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rpp;

namespace RppTests
{
    [TestClass]
    public class MutationTests
    {
      
        [TestMethod]
        public void InnerCanBeFound()
        {
            var input =
                @"<A
  line 1
  line 2
  <B header
    line B1
    line B2
  >
  line3
  line4
>
";
            var project =
                ReaperProjectParser.ParseProjectFromLines(
                    new LineSource(input));
            project
                .ElementsAndDescendants()
                .OfType<ProjectElement>()
                .Count(a => a.HeaderType == "B")
                .Should()
                .Be(1);
        }

        [TestMethod]
        public void AllSourcesFound()
        {
           
            var project =
                ReaperProjectParser.ParseProjectFromLines(
                    new LineSource(TestData.TestFile));

            var sourceFiles = project
                .ElementsAndDescendants()
                .OfType<ProjectSource>()
                .ToArray();
            sourceFiles.Length.Should()
                .Be(3);
        }

        [TestMethod]
        public void SourceCanBeReplaced()
        {
            var input = @"< REAPER_PROJECT 0.1 ""5.60 / x64"" 1516466972
 <ITEM
      <SOURCE MP3
        FILE ""C:\Users\Neil\Downloads\wikiloops_jam_127409.mp3"" 1
      >
    >
>
";
            var project =
                ReaperProjectParser.ParseProjectFromLines(
                    new LineSource(input));

            var sourceFiles = project
                .ElementsAndDescendants()
                .OfType<ProjectSource>()
                .ToArray();
            sourceFiles.Length.Should()
                .Be(1);
            var source = sourceFiles.First();
            source.File.Should()
                .Be(
                    @"C:\Users\Neil\Downloads\wikiloops_jam_127409.mp3");
            var newFile = source.WithFile("replacementFile");
            var newProject = project.Replace(source, newFile) as ProjectElement;
            var newSources = newProject.ElementsAndDescendants()
                .OfType<ProjectSource>()
                .ToArray();
            newSources.Length.Should()
                .Be(1);
            newSources.First()
                .File
                .Should()
                .Be("replacementFile");
            newSources.First()
                .Trailing
                .Should()
                .Be("1");
        }

    }

    public static class TestData
    {

        public const string TestFile =
          @"<REAPER_PROJECT 0.1 ""5.60/x64"" 1516466972
  RIPPLE 0
  GROUPOVERRIDE 0 0 0
  AUTOXFADE 1
  ENVATTACH 1
  POOLEDENVATTACH 0
  MIXERUIFLAGS 11 48
  PEAKGAIN 1
  FEEDBACK 0
  PANLAW 1
  PROJOFFS 0 0
  MAXPROJLEN 0 600
  GRID 3455 8 1 8 1 0 0 0
  TIMEMODE 1 5 -1 30 0
  VIDEO_CONFIG 0 0 256
  PANMODE 3
  CURSOR 0
  ZOOM 4.10395408163265 0 0
  VZOOMEX 6
  USE_REC_CFG 0
  RECMODE 1
  SMPTESYNC 0 30 100 40 1000 300 0 0 1 0 0
  LOOP 0
  LOOPGRAN 0 4
  RECORD_PATH """" """"
  <RECORD_CFG
  >
  <APPLYFX_CFG
  >
  RENDER_FILE """"
  RENDER_PATTERN """"
  RENDER_FMT 0 2 0
  RENDER_1X 0
  RENDER_RANGE 1 0 0 18 1000
  RENDER_RESAMPLE 3 0 1
  RENDER_ADDTOPROJ 0
  RENDER_STEMS 0
  RENDER_DITHER 0
  TIMELOCKMODE 1
  TEMPOENVLOCKMODE 1
  ITEMMIX 0
  DEFPITCHMODE 589824
  TAKELANE 1
  SAMPLERATE 44100 0 0
  <RENDER_CFG
  >
  LOCK 1
  <METRONOME 6 2
    VOL 0.25 0.125
    FREQ 800 1600 1
    BEATLEN 4
    SAMPLES """" """"
    PATTERN 2863311530 2863311529
  >
  GLOBAL_AUTO -1
  TEMPO 85 4 4
  PLAYRATE 1 0 0.25 4
  SELECTION 0 0
  SELECTION2 0 0
  MASTERAUTOMODE 0
  MASTERTRACKHEIGHT 0
  MASTERPEAKCOL 16576
  MASTERMUTESOLO 0
  MASTERTRACKVIEW 0 0.6667 0.5 0.5 0 0 0
  MASTERHWOUT 0 0 1 0 0 0 0 -1
  MASTER_NCH 2 2
  MASTER_VOLUME 1 0 -1 -1 1
  MASTER_FX 1
  MASTER_SEL 0
  <MASTERPLAYSPEEDENV
    ACT 0 -1
    VIS 0 1 1
    LANEHEIGHT 0 0
    ARM 0
    DEFSHAPE 0 -1 -1
  >
  <TEMPOENVEX
    ACT 0 -1
    VIS 1 0 1
    LANEHEIGHT 0 0
    ARM 0
    DEFSHAPE 1 -1 -1
  >
  <PROJBAY
  >
  <TRACK {CCB4FCDD-3457-4C0A-805C-7F3855E676A1}
    NAME wikiloops_jam_127295
    PEAKCOL 16576
    BEAT -1
    AUTOMODE 0
    VOLPAN 1 0 -1 -1 1
    MUTESOLO 0 0 0
    IPHASE 0
    ISBUS 0 0
    BUSCOMP 0 0
    SHOWINMIX 1 0.6667 0.5 1 0.5 0 0 0
    FREEMODE 0
    SEL 0
    REC 0 0 0 0 0 0 0
    VU 2
    TRACKHEIGHT 0 0
    INQ 0 0 0 0.5 100 0 0 100
    NCHAN 2
    FX 1
    TRACKID {CCB4FCDD-3457-4C0A-805C-7F3855E676A1}
    PERF 0
    MIDIOUT -1
    MAINSEND 1 0
    <ITEM
      POSITION 0
      SNAPOFFS 0
      LENGTH 251.77074829931973
      LOOP 1
      ALLTAKES 0
      FADEIN 1 0.01 0 1 0 0
      FADEOUT 1 0.01 0 1 0 0
      MUTE 0
      SEL 0
      IGUID {BE3CEDC2-36FD-4C3D-A5BF-93784AF4E968}
      IID 4
      NAME wikiloops_jam_127409.mp3
      VOLPAN 1 0 1 -1
      SOFFS 0
      PLAYRATE 1 1 0 -1 0 0.0025
      CHANMODE 0
      GUID {011DD69E-A278-473B-BDB5-576D5F1D5F55}
      <SOURCE MP3
        FILE ""C:\Users\Neil\Downloads\wikiloops_jam_127409.mp3"" 1
      >
    >
  >
  <TRACK {BFFE085C-6072-491D-808D-FB4C2907A991}
    NAME """"
    PEAKCOL 16576
    BEAT -1
    AUTOMODE 0
    VOLPAN 0.86872391523433 0 -1 -1 1
    MUTESOLO 0 0 0
    IPHASE 0
    ISBUS 0 0
    BUSCOMP 0 0
    SHOWINMIX 1 0.6667 0.5 1 0.5 0 0 0
    FREEMODE 0
    SEL 0
    REC 1 0 0 0 0 0 0
    VU 2
    TRACKHEIGHT 0 0
    INQ 0 0 0 0.5 100 0 0 100
    NCHAN 2
    FX 1
    TRACKID {BFFE085C-6072-491D-808D-FB4C2907A991}
    PERF 0
    MIDIOUT -1
    MAINSEND 1 0
    <FXCHAIN
      WNDRECT 2431 492 754 466
      SHOW 2
      LASTSEL 1
      DOCKED 0
      BYPASS 0 0 0
      <VST ""VST: ReaXcomp (Cockos)"" reaxcomp.dll 0 """" 1919252579
        Y3hlcu5e7f4CAAAAAQAAAAAAAAACAAAAAAAAAAIAAAABAAAAAAAAAAIAAAAAAAAABAEAAAEAAAAAAAAA
        OAAAAAQAAAC+ZE98gqpVQAAAAAAAAPA/TJyKd0OzpT9cdgXBWKf5PwAAAAAAAAAAIgAAAMwAAAATAAAAAQAAAOzmH5k9QZJAAAAAAAAA8D8dFaDVZn65Pxpc7dTlkPQ/
        AAAAAAAAAAAUAAAAzAAAABMAAAAGAAAAYhnDkD6lv0AAAAAAAADwP6Dr4Hb4YrQ/DYPIuyv49j8AAAAAAAAAABQAAABWAAAAEQAAAAYAAAAAAAAAAHDXQAAAAAAAAPA/
        FdsEnPArrz8AAAAAAAAAQPp+arx0k8g/CgAAAAwAAAACAAAAAgAAAAEAAAABAAAAAAAAAAAA8D8AAAAAAAAAAAAAAAA=
        AGdyaXo6IHN1cGVyc29saWQgYmFzcywgbmljZSBvcHRvIG1pZHMAAAAAAA==
      >
      PRESETNAME ""griz: supersolid bass, nice opto mids""
      FLOATPOS 0 0 0 0
      FXID {19D39A01-D0F7-4911-8C03-5D17549CAC50}
      WAK 0
      BYPASS 0 0 0
      <VST ""VST: ReaEQ (Cockos)"" reaeq.dll 0 """" 1919247729
        cWVlcu5e7f4CAAAAAQAAAAAAAAACAAAAAAAAAAIAAAABAAAAAAAAAAIAAAAAAAAAqAAAAAEAAAAAABAA
        IQAAAAQAAAAAAAAAAQAAAAAAAAAAAFlAAAAAAAAA8D8AAAAAAAAAQAEIAAAAAQAAAKTxm1SV7HBA+GDU9rhb+T8AAAAAAAAAQAEIAAAAAQAAAAAAAAAAQI9AAAAAAAAA
        8D8AAAAAAAAAQAEBAAAAAQAAAAAAAAAAiLNAJMiYHzjd7T8AAAAAAAAAQAEBAAAAAQAAAAAAAAAAAPA/AAAAAA8CAABhAQAA
        AAAQAAAA
      >
      FLOATPOS 0 0 0 0
      FXID {F209781F-5379-4573-96B9-070A060FD149}
      WAK 0
    >
    <ITEM
      POSITION 0
      SNAPOFFS 0
      LENGTH 253.67548752834466
      LOOP 1
      ALLTAKES 0
      FADEIN 1 0.01 0 1 0 0
      FADEOUT 1 0.01 0 1 0 0
      MUTE 0
      SEL 1
      IGUID {45598987-2BA5-4EB6-87ED-601387456F50}
      IID 7
      NAME 02-180120_1635.wav
      VOLPAN 1 0 1 -1
      SOFFS 0
      PLAYRATE 1 1 0 -1 0 0.0025
      CHANMODE 0
      GUID {2FF4E639-022A-425E-9491-21596BE0BE95}
      RECPASS 2
      <SOURCE WAVE
        FILE ""C:\Users\Neil\Documents\REAPER Media\02-180120_1635.wav""
      >
    >
  >
  <TRACK {F3CC6F63-353B-460A-A1FF-48C73A96B48B}
    NAME """"
    PEAKCOL 16576
    BEAT -1
    AUTOMODE 0
    VOLPAN 1.08125952860623 0 -1 -1 1
    MUTESOLO 0 0 0
    IPHASE 0
    ISBUS 0 0
    BUSCOMP 0 0
    SHOWINMIX 1 0.6667 0.5 1 0.5 0 0 0
    FREEMODE 0
    SEL 1
    REC 1 1 0 0 0 0 0
    VU 2
    TRACKHEIGHT 0 0
    INQ 0 0 0 0.5 100 0 0 100
    NCHAN 2
    FX 1
    TRACKID {F3CC6F63-353B-460A-A1FF-48C73A96B48B}
    PERF 0
    MIDIOUT -1
    MAINSEND 1 0
    <ITEM
      POSITION 0
      SNAPOFFS 0
      LENGTH 253.67548752834466
      LOOP 1
      ALLTAKES 0
      FADEIN 1 0.01 0 1 0 0
      FADEOUT 1 0.01 0 1 0 0
      MUTE 0
      SEL 1
      IGUID {6F036FC0-5728-4AD9-BFCE-F28213756431}
      IID 8
      NAME 03-180120_1635.wav
      VOLPAN 1 0 1 -1
      SOFFS 0
      PLAYRATE 1 1 0 -1 0 0.0025
      CHANMODE 0
      GUID {7191FE81-E12F-4AD6-B0D6-3029663BE040}
      RECPASS 2
      <SOURCE WAVE
        FILE ""C:\Users\Neil\Documents\REAPER Media\03-180120_1635.wav""
      >
    >
  >
>
";


    }

}