# Tile Game by Pascal Pieper
Instructions:
Start TileGame.sln with either Visual Studio or Rider. 
Build the solution with NetCore3 or Netframework 5.

All exercises are seperated in-game and setup to showcase the exact functionalites that were required.
Most of the level genereation can be found under Level> Levelgenerator.cs Level.cs and LevelTemplate.cs
There are multiple options for map creation to choose from. When creating a bigger map than the screen can draw, use the mouse wheel or the gui on the bottom left to adjust the view.


The main loop is located in Main->GameWindow and Program


I used this project to test serveral patterns and DI concepts:
The factory pattern with Assembly reflection was used to create new tiles with different attributes.
Additionally the Command pattern was used for the map creation to create the map piece by piece.
The Flyweight pattern was used in the Assetmanager to prevent texture from being loaded multiple times.

 




The following dependencies are required and have been used:
SFML.Net: https://github.com/SFML/SFML.Net - zlib/png license
Dear IMGui.NET: https://github.com/mellinoe/ImGui.NET - MIT license
Dear IMGui.Net Wrapper Saffron2D: https://github.com/saffronjam/Saffron2D - MIT license





