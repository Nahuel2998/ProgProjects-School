using System;
using System.IO;
using System.Media;
using System.Reflection;
using CommandLine;

namespace ModSelector
{
    class Program
    {
        private static void Main(string[] args)
        {
            OptionsHolder.Instance.Options = Parser.Default.ParseArguments<Options>(args).Value;
            
            Random random = new Random();
            if (random.Next(21, 63) == 42)
            {
                Console.WriteLine("I don't feel like running.");
                Console.ReadKey();
                return;
            }

            if (DateTime.Now.Minute == 42)
            {
                Console.WriteLine("nice the time");
                Console.ReadKey();
                Console.Clear();
            }

            Console.SetWindowSize(Console.WindowWidth, 42);
            Console.WriteLine(@".°*°...*°**°....°°.°..°.°*******°.....°°°°°....° .°..°****°°°°. ............ ...
..°°...*°**°..°.°°... ...°°°°°°°. °***********°° .....°°°°°°°..  ........... ...
..°°...**°°°... .°°°°°°°°*********OOOOOOOOOOOOOO**********°°°°.°°°.......... ...
..°°...°°°°°°***ooOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOoo**°°.... ...
...°°**ooooOOOOOoOOOOOOOOOOOOOO#OO###############OOOOO###OOo*****ooOOOOO° . ....
*oooOOOOoooOOOOOO#############################OO#####Oo*°........°°°*ooO° ......
OOOOoOOO#########################################Oo*°...°*oOO###OOOOoooO° ......
Oo*°°°..°°*oO##########################OOOOOOo*°...°*oOO######OOOOOOOOOO..°.....
OoOOOOOOo*°°.°*oOOO##OOO###############Oo**°°°°*oOO##############OOOOOOO..°°....
OOOO########Ooo*°°°***oO######################OOOOOooooooooooooOOOOOOOOO..°°. ..
OOOO#####OOOOOOOOOOOO######################OOOOoo*****ooooooooooooOOOOOO.°°°. ..
oOOOOOOOOoo****°°°°**oO####################Oo°......   .........°°°**oOO.°°°. ..
.OOO#OOo*°...       ..°o###################O***ooOO°.**         .°.    °.°°°. ..
 *OOo°   .°. .....   °*°o##################OO#####@.°#O........ °#OOo °*.°°°. ..
 .Oo. .oOO#°°#o..... °##OO######################@@@° °**.....°°.°@@Ooo#o.°°°. ..
. *Oo°°####* °°°...°*°#########################OOOO° .***°°°*o*°oO*oOOOo.°°°. ..
. .OO#OO#@@#°.°°..°*°*OO##########################OOOoo*****°°..°*oOOOO*.°°°. ..
.. *O#Oo**oO* .*ooOO###############################################OOOO*.°°°....
.. .OO##O*o°*O#####################################################OOOO°.°°° ...
... o##############################################################OOOO°.°°° ...
... °##############################################################OOOO°°°°° ...
... .###############################################################OOO.°°°° ...
... .O#####################Oo######################################OOOO.°°°° ...
.... O#####################oO######################################OOOO.°°°° ...
.... o######################O######################################OOOo.°°°. ...
.... *#############################################################OOOo.°°°. ...
.... °############################################################OOOO*.°°°. ...
.... .O###########################################################OOOO*°°°°. ...
..... o##########################################################OOOOO°°°°° ...°
..... .O##########################################O#############OOOOOO°°°°° ...°
...... °########################################O**O############OOOOOO.°°°° ...°
....... *#################OoooOOOOOOooOOOOOOooo**oO############OOOOOOO.*°°° ...°
........ °#################OooooOOOO###OOOOOOO################OOOOOOOo.*°°° ...°
......... .o#################################################OOOOOOOO*.*°°. ..°°
.........   °O#####################O*oOOOO##################OOOOOOOO*.°*°°. ..°°
......... ..  °o####################OOOOO##################OOOOOOOo°*°°*°°....°°
......... ....  °o#######################################OOOOOOOo°°*o.°*°° ...°°
......... ......  .*O##################################OOOOOOO*°°*ooo.**°° ...°°
........  ........   °oO#############################OOOOOOo*°°*o****.*°°° ..°°°
........  ........ ..  .°o#########################OOOOOOo°°**o*°*oo..*°°. ..°°°");
            if (random.Next(0, 600) == 600 || OptionsHolder.Instance.Options.Beato)
            { new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("ModSelector.ahaha.wav")).PlaySync(); }
            if (!OptionsHolder.Instance.Options.NoTrackDos) { UiHandler.Instance.TrackDosPlayer.Load(); }
            if (!OptionsHolder.Instance.Options.NoJunko) { UiHandler.Instance.PureFuriasPlayer.Load(); }
            if (!OptionsHolder.Instance.Options.ShutAru) { UiHandler.Instance.AruPlayer.Load(); }
            
            string[] defaultPaths = {
                $@"{Environment.CurrentDirectory}\steam_settings\TheList.txt", // TheList Path
                $@"{Environment.CurrentDirectory}\steam_settings\unusedMods", // Unused Mods Folder
                $@"{Environment.CurrentDirectory}\steam_settings\mods"  // Used Mods Folder
            };
            string theListPath = OptionsHolder.Instance.Options.TheListPath ?? defaultPaths[0];
            string unusedModsFolder = OptionsHolder.Instance.Options.UnusedModsFolder ?? defaultPaths[1];
            string usedModsFolder = OptionsHolder.Instance.Options.UsedModsFolder ?? defaultPaths[2];

            if (!OptionsHolder.Instance.Options.WorseControl)
            {
                try
                {
                    string[] arrLine = File.ReadAllLines($@"{usedModsFolder}\..\..\ColdClientLoader.ini");
                    if (!arrLine[4].Contains("-bettercontrol"))
                    { arrLine[4] += " -bettercontrol"; }

                    File.WriteAllLines($@"{usedModsFolder}\..\..\ColdClientLoader.ini", arrLine);
                }
                catch (Exception)
                { /* Ignored */ }
            }

            try
            { ModSelector.Instance.InitMods(theListPath, unusedModsFolder, usedModsFolder); }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            
            UiHandler.Instance.TrackDosPlayer.PlayLooping();
            UiHandler.Instance.UnusedModsFolder = unusedModsFolder;
            UiHandler.Instance.UsedModsFolder = usedModsFolder;
            UiHandler.Instance.BuildMenu();
        }
    }

    public class Options
    {
        [Option('m', "modsFolder", HelpText = "Folder where used mods are stored")]
        public string UsedModsFolder { get; set; }
        
        [Option('u', "unusedModsFolder", HelpText = "Folder where unused mods are stored")]
        public string UnusedModsFolder { get; set; }
        
        [Option('l', "listPath", HelpText = "TheList.txt path")]
        public string TheListPath { get; set; }
        
        [Option('b', "beato", HelpText = "ahaha.wav")]
        public bool Beato { get; set; }
        
        [Option("shutAru", HelpText = "Make Aru shut the fuck up")]
        public bool ShutAru { get; set; }
        
        [Option('a', "aruMode", HelpText = "WAAAAA")]
        public bool AruMode { get; set; }
        
        [Option("noJunko", HelpText = "Junko will not show up")]
        public bool NoJunko { get; set; }
        
        [Option("noTrackDos", HelpText = ":(")]
        public bool NoTrackDos { get; set; }
        
        [Option('w', "worseControl")]
        public bool WorseControl { get; set; }
    }
}
