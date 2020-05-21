Sometimes, it's important to understand what the software do before asking me why the game is broken after that :3

First, when you use the mod generated from that software, you'll no longer have access to normal songs !
It's because the game can't handle a playlist of more than 255 songs.
I'm looking for solutions like modding DLC instead, but I can't find anything related to Layered FS DLC modding on Switch...
Don't worry, Atmosphere let you by default to boot games without mods just by pressing L while booting.
Of course, if you delete the mod of your SD Card, everything will go back to normal.

When you convert a song, it will use the ID.txt inside the folder of the platform you want to convert it, and will use it to place your new song inside the playlist with that song ID number.
The only way to delete the song of the playlist, is to open the stage_param.dat file in HEX editor, and delete the corresponding offsets. 

If you just want to cancel the last song you converted, a backup file is always created.
The original file is a file ready to create a new playlist. by swaping file in your switch (using some homebrews for exemple) you can easily switch between your created playlists.

If you really need to edit by yourself your playlist data via Hex Editing, check songStruct.jpg.
One song is highlighted, Green is the songID