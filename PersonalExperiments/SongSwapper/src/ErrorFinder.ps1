# Find text files
$errorSongs = @( Get-ChildItem Result\*\*.txt -Name );

for ( $i = 0; $i -lt $errorSongs.count; $i++ )
{
  $errorSongs[$i] = @( $errorSongs[$i].Split('_') );
  $errorSongs[$i][1] = $errorSongs[$i][1].Split('.')[0];
}

# Save wheels/pairs
$wheelsArr = @( Get-Content Result\who_got_who.txt );

for ( $i = 0; $i -lt $wheelsArr.count; $i++ )
{ $wheelsArr[$i] = @( $wheelsArr[$i] -split " <?-> " ); }


# Search for the culprit and print
"Songs with errors:"
foreach ( $errSong in $errorSongs )
{
  foreach ( $wheel in $wheelsArr )
  {
    if ( $wheel -contains $errSong[0] )
    { "-> $($wheel[[Array]::IndexOf($wheel, $errSong[0]) - 1])'s $($errSong[1])th song"; }
  }
}
"[Enter to quit]"
Read-Host;
