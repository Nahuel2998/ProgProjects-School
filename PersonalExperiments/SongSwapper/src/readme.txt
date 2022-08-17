hola

- - [ TLDR ] - -
no fuck you read it all

- - [ How ] - -
Program grabs a text file with the round info and does a thing
yeah

- - [ Actually How ] - -
It first checks if there's any restrictions for this round
For example, if a participant can't get another's songs because they'd guess each other too easily
Or something like a forced pair

After that it makes a list with everyone's names and shuffles it (with the restrictions in mind)
And that's basically the order for the round

Then it creates a folder for each participant (with their name) in Result/
Inside that folder will be the songs each participant got

The program downloads each song, strips the metadata, and renames them to a "Receiver_number" format, keeping the original order
For youtube songs, it uses yt-dlp to download the vid and take out the audio only (these don't ever have metadata)
For newgrounds links and direct links, it downloads them directly then strips the metadata
If either downloading or stripping the metadata fails, it instead creates a text file with the link inside
(Stripping the metadata only ever fails if the downloaded file wasn't an audio file, so it works as a verification)

It also creates a who_got_who.txt file inside of Result/ detailing, well, who got who

- - [ What ] - -
( Result/ )
Each folder contains the songs each participant(name of the folder) got
You can then just upload the entire folder to dropbox (or just send them over discord since on my tests they were usually below 8mb)

( Result/who_got_who.txt )
who_got_who.txt contains the order/pairings for this round

For Example:
Nar -> Nome -> Star -> Hyper
Here Hyper gets Star's songs, Star gets Nome's, Nome gets Nar's, and Nar gets Hyper's
This notation has already been used by Olli so I'll trust you get it

If there was any pair in that round they'll be listed in individual lines below (since they're not part of the main circle)
Strophox#2091 <-> Olli
This means Strophox#2091 got Olli's songs, and Olli got Strophox#2091's songs

( example_songs.txt )
Naturally, the text file the program uses needs an specific format (more on that later)
This file is an example input file for the program

( libs/ )
External programs the program uses to download the songs and strip the metadata
yeah kinda don't touch these

( src/SongSwapper.pl )
The sourcecode for this program
In case you don't trust me not deleting your system32
Kinda only useful if you can somehow read Perl code
free spaghetti

- - [ But Why ] - -
I was bored
This was made to let the host also participate in the round
The host only has to make the text file and give it to the program, the program handles the downloading songs part and assignments
They don't have to see who gets who till the end of the round, thus also letting them guess who they got

- - [ Use ] - -
Make the text file
Drag the text file over the program
G

If you just open it without dragging a text file on it it'll either stay open forever or close instantly

- - [ The GUI ] - -
haha

- - [ The Output ] - -
It should kinda be self-explanatory

If there's any obvious problem with the restrictions the program tells you that and dies
It also tells you the amount of participants on the round and the amount of pairs
(Unless there are too many restrictions it shouldn't make pairs itself)
It then says hola
When it gets the songs it says doing...
If it succeeds it says did
If it doesn't it says fuck
It purposely doesn't say what went wrong with what song so the host doesn't get spoiled yeah
After it's done it says done.

- - [ The Text File ] - -
alright
time for the

(( General ))
To define participants, follow the following format:

Name
link
link
link

The first line will be the participant's name, and the following lines will be the links to their songs
Participants are separated by empty lines
Leave exactly one empty line between each participant if you don't want to try and break it
Example in the example_songs.txt file

(( Restrictions ))
You can define restrictions at the start of the file

Simply start the file with something like "Restrictions" (It really just checks if the line contains 'restrict' so while it contains that it's fine)
It has to be the first line though or otherwise they'll be taken as a participant

After that you can place a few lines defining restrictions (without empty lines between them)
There should be at least two empty lines (preferably just two) between the restrictions and the first participant

( Restrictions Syntax )
This line is here to make the spacing look prettier

Nar -> Nome
This says Nome must get Nar's songs (rigged)
Do not try to make a chain with this like Nar -> Nome -> Star since I didn't support that yeah
If you do want to do that just put them in separate lines:
Nar -> Nome
Nome -> Star

Nar <-> Nome
This forms a pair between Nar and Nome, Nar gets Nome's songs and Nome gets Nar's songs

Nar !> Nome
This means Nome can't get Nar's songs

Nar !> Nome | Star
You can also use this syntax to say neither Nome or Star can get Nar's songs
Supports as many participants as you want after the arrow (Nar !> Nome | Star | Hyper | Elte)

Nar | Nome !> Star
And this is a thing as well, meaning Star can't get Nar or Nome's songs
Also supports as many participants as you want before the arrow (Nar | Nome | Star !> Hyper)

Nar | Nome !> Star | Hyper
And yeah you can mix the two but can you really read this lma
Neither Star or Hyper can get Nar or Nome

Nar <!> Nome
This means Nar can't get Nome's songs and Nome can't get Nar's songs
No you can't use | here that'd be unreadable

(The space before and after each symbol is important, otherwise it'll look ugly)

Pairs
You can also just write pairs (it checks whether the line contains pairs) for a pairs-only round
The number of participants has to be even, otherwise there can't be pairs
You can't use -> if you use this option, use <-> instead to define forced pairs
Both <!> and !> work well with this option

- - [ That's ] - -
that's

- - [ Final Note ] - -
If you try to break it, it probably will
kinda don't

- - [ Known Things That Kinda Break It ] - -
Your restrictions are the only thing that can break it (I think)

If you try to make a closed circle yourself, it'll repeat a participant in the list
Nar -> Nome
Nome -> Star
Star -> Nar
If these were the only participants in the round, you suck because you're just rigging everything
But yeah because of the way it's implemented, one of the three will be repeated in the final thing
Which means two participants will get songs from the same participant, and some participant won't have their songs shared
I know what causes this so I might fix this in the future
but for now kinda don't
I don't think anyone wants two circles either
