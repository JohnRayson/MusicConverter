# MusicConverter
A converter to move files into the correct format (.ogg and .mp3) and place them in the correct directory structure for the DukeBox project

# Progress
Yes, you guessed it - its not finished.
It currently takes files from a directory (recursively) and migrates them into another directory also creating the structure <directory>\Artist\Album\[fileType]\[#] - [title].[fileType]
It only works if the source file is .mp3, and only creates .ogg files.
The track information is also indexed in a sqlite db
Most of the work is actually done by ffmpeg converter.

#Issues / Unknowns
I have no idea what it will do if its not a "perfect" track as the source.....

