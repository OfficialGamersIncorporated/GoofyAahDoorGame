![LogoBanner](https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/b8bcdac9-a824-4915-86cb-f915e1823610)

DOORIA! is a rogue-like game made in one week for Brackeys Game Jam 2024.1 in which you explore rooms behind doors labeled with emoji. The game ranked #15 for most fun out of nearly 800 entries.

Battle bosses and a random assortment of other things that are at least sometimes hostile through room after room, searching for the keys to the final boss.

<p align="center">
 <img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/951ca9f8-3915-4091-8062-d99f0a77bcfc" width="100" height="100" align="center">
</p>

made in Unity ~~2022.3.19f1~~ 2023.2.9f1
The game can be played on itch.io in the web browser or downloaded from the releases page here on GitHub.

<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/906b0d77-7754-4972-9ffd-0d7c3b73efe0" width="16" height="16"> [Play on itch.io](https://pc-hris.itch.io/dooria)

### Interesting technical notes
* The game is made up of many building block components such as a generic AI, hitboxes, hurtboxes, health handlers, etc. that can be mixed and matched to make novel functionality without writing new code.
* The AI script has a handful of values that can be used to make novel and unique movement patterns without writing a new script for every enemy by using simple sin waves.
* The bow weapon shoots projectiles that have AI components attached to them to make them seek targets and move in loops. They also have health components so they can be destroyed.
* Objects with health components have a list of "gibs" (gore bits) that get spawned and spread around them when they die. The emoji room places smaller emoji enemies as the larger enemies gibs so that killing a large emoji will split it into smaller ones without writing any bespoke code.

</br></br></br></br>

<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/b8ee0bae-a70c-402a-943a-eed67ee2094f" width="480" height="270">
<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/0e02da75-f5cc-4d9a-9a36-93c6de9d06a7" width="480" height="270">
<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/6ffb9172-9354-4788-a719-c3466b0417e9" width="480" height="270">
<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/f1c334cc-c95e-4f11-b542-3be519f06433" width="480" height="270">
<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/050f4f96-789e-4e41-bf77-22ae028cc62f" width="480" height="270">
<img src="https://github.com/OfficialGamersIncorporated/GoofyAahDoorGame/assets/32988106/f0e8847c-9e38-4f08-80e6-463206d6c429" width="480" height="270">
