# TrialProject

## What is this about?

Hi there. This is a TestProject for a 2D Shooter Platformer game. Outside of a few sprites there is nothing special about this repository. 
You could see this as a collection of tests, about trial and error, in order to someday make a full game. Of course the full game and all its assets will most likely be on a private repository.
I will document progress about the tests here, along a bunch of task lists so I don't forget what I was supposed to be doing, provided lazyness doesn't win again.

### 12 of June 2019: Initial Commit

Finally got around actually using GitBash and uploading the project here. This initial commit has the basics: Scripts, default assets, and a lot of bugfixes.
Making jumps and collisions work together was surprisingly difficult. For some reason Tilemaps don't like it when you slide down a wall to the floor, so much in fact they will stop you from jumping ever again.
The solution ended up being to do the same collision checks for OnCollisionEnter2d and OnCollisionStay2d. This caused the jump, which uses AddForce at this point (iirc) to double is magnitude. Basically if AddForce has a 100 value, when OnCollisionStay2d and OnCollisionEnter2d are both acting it causes the value to behave as if it was 200 instead. 
I wonder if it is because it is the player script who calls for the jump...

As for the rest added a shooting mechanic complete with bullets and a gun(barrel), a camera that follows you around and doesn't flips when you do and a simple level.

Tiles that work are those that are made of the same tilemap and uses a modified tile collider whose name I don't remember.

The player detects when it is grounded when its Y contacts return a 1, and it works as long as the surface is squared. In fact it works with rounded surfaces as well but it causes the jump to be wonky due to the lack of any checking for rounded surfaces.

Future Work:

- [ ] Check the name of the tilemap type and add it here
- [ ] Add UI elements
- [ ] Add a persistent background
- [ ] Make the "enemy" script and have it react and die to bullets
- [ ] Add animations from a sprite sheet
