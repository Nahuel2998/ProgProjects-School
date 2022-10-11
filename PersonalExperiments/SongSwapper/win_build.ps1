pp -o build/SongSwapper.exe -M Bundle::LWP -x src/SongSwapper.pl --xargs src/example_songs.txt
Copy-Item -Path src/readme.txt -Destination build
Copy-Item -Path src/example_songs.txt -Destination build
Copy-Item -Path src/ErrorFinder.ps1 -Destination build
Copy-Item -Path src/SongSwapper.pl -Destination build/src/SongSwapper.pl
Copy-Item -Path src/libs -Destination build -Recurse
"[Enter to quit]"
Read-Host;
