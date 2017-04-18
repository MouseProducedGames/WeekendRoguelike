# WeekendRoguelike
A free- and open-source Roguelike in the classic style done as a casual, weekend/free time project. Sometimes it receives more work if I have insomnia.

WeekendRoguelike is data-driven, and the game can be modded by editing the text files in the Data folder. Currently, modding capability is limited, as the engine itself is limited.

# Days of Progress
3 Days

# Tagline for the day
Data-driven from Day 1.

# Current Status
* As a classic Roguelike, all map objects are represented using text symbols.
  * Please do not resize the console; that may cause the game to crash if it attempts to draw at an invalid position.
* Choose a race and a class, then hunt down that wandering zombie!
  * Races are Dwarf, Elf, Halfling, Human, and Orc.
  * Classes are Assassin, Barbarian, Rogue, and Warrior.
    * The lists can also be navigated with the arrow keys and number pad.
    * Use enter to confirm your choice.
  * Your character is represented by a letter and colour depending on their class.
    * The assassin are rogue are yellow, because they're in it for the gold.
    * The barbarian is red; red like the blood of the weak on the ground!
    * The fighter is gray, like steel.
    * The letter is the first letter of the class.
  * Move and attack using the number pad with numlock on.
  * Your stats are at the bottom of the screen.
    * (He)alth: If your character runs out of this, they die.
    * (St)rength: How much damage you do, and how well you resist damage.
    * (Ag)ility: How well you dodge.
    * (Co)ordination: How accurately you hit.
* There is one zombie on the map.
  * The zombie is the 'z' symbol.
  * The zombie moves directly towards you; this may cause it to run into walls and go nowhere.
    * if it moves into your square, it will attack.
* The map is 80x25 (width x length).
  * The map is generated using a data-driven room and corridor dungeon factory.
  * Wall tiles block movement; you can squeese through diagonal cracks in the walls.
  * Line of sight data exists, but sight checks are not yet implemented.
  * There are invisible walls at the edges of the map.
