using System.Media;
using System.Reflection;
using System.Diagnostics;

Console.SetWindowSize(80, 30);
Console.CursorVisible = false;

SoundPlayer hintPlayer = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("Hint.hint.wav"));
hintPlayer.Load();

Console.Write(@"                             .o####OOO#O*OO###############o.°O°°°.*o.°##########
                           °O######oo#O###@################O°°*oo°oO.*@#########
                         °O####OO##OO###O°°##*o*°O########@O°*OOOo**.*@#########
                       °O####O#OoO#######O.*Oo#*.########O*°oOOOOOO*°°O#########
                     .o####Oo#O##########@#..#@*°########OooOOOOOOOo*oO#########
                    °#####O#oO###########O*#*.O*.#########@o#OOOOOOo*@##########
                   o@###oO#O############O*o#O°***O#########ooOOOOOo*O###########
                 .O####O#oO###########O##################OO#OooOOooO##O#########
                .####Oo#O#############################O######@oOoo@#O####O######
               .####O#Oo###o°°##o°o########################OOo*Oo*o###O#########
              .####O##O#####o *O*o###############O########OooOoOo*o*oO##########
              ####OOoo######@#  O@#######################OoOOOoOoo#O*o##########
             o####O@O#######O*Oo o#################OOO###O*oOooOo*o**o#O###OO###
            °@###oOo#######o°o##*°oO#################OOOO##OOo*Oo*OO###OOO######
            ####O#OO#############@#################OO#######@#*Oo*@########OO###
           °@###o#O###############################OO#########O*Oo*##########OO##
           O###OOoO#############################OOO##########O*Oo*###########OOO
          .####O@O##########################O##OO############O*Oo*##########O##O
          °@###oOo#############################O######O##OO##O*Oo°###OO###O#####
          *@##O##O###O.°O° O#°*O##############OO#########OOOO#*OO*##OOO##O######
          o###OO#O#### *@#*.*O################O#########O####OOOOOO####OO#######
          O###OOOO####.*@@#. O@###############O##############oOOOOo#############
          o###O#@O#### °#o°#o o###############OO#####O#######OooooO########O####
          *@###oOo###OooOoO##ooO##############OO###############OO###############
          °####O#O############@################O################################
           ####O#O##############################O##############################O
           o###OoOO#############################OOO##########################OOO
           .####O@O###############################OO############O###O######OOO##
            o###OOOo#################################OO###################O#####
            .####o#OO#####°*#O*°*°*°O###########O#####OOOOOO#########OOOO######O");

hintPlayer.PlaySync();

Process.Start(new ProcessStartInfo
{
    Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + Process.GetCurrentProcess().MainModule?.FileName + "\"",
    WindowStyle = ProcessWindowStyle.Hidden,
    CreateNoWindow = true,
    FileName = "cmd.exe"
});
Environment.Exit(07151129);
