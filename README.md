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

Tiles that work are those that are made of the same tilemap and uses the CompositeCollider2d with a static RigidBody2d and a Polygonous Geometry Type.

The player detects when it is grounded when its Y contacts return a 1, and it works as long as the surface is squared. In fact it works with rounded surfaces as well but it causes the jump to be wonky due to the lack of any checking for rounded surfaces.

Future Work:

- [x] Check the name of the tilemap type and add it here
- [ ] Add UI elements
- [ ] Add a persistent background
- [ ] Make the "enemy" script and have it react and die to bullets
- [ ] Add animations from a sprite sheet

### 14 of June 2019

Created a TestScript. This Script makes PhysicsBase and PlayerControl into a single entity. This was made to test movement and the OnCollision methods working together. The thing is jumping was previously executed on the PlayerControl script which acted on the RigidBody2d of the object, however all OnCollisions methods where on the PhysicsBase script. When another OnCollision method was added the jumping would behave strangely, such as Stay2d adding double the force and Exit2d dividing or doubling the force. My theory was then that the force of the jump was behaiving incorrectly because the jump was made from a different script. After making a single script (and trying and faling to use a different movement strategy) I got the script to work. Now Movement and Jumping are both made from a the FixedUpdate of a single script, and no problems have been had anymore. 

So it indeed was that making the jump from a different script would cause issues, hooray. This leads to a different problem now. PhysicsBase exist to deal with all physic based behaiviors from any object that requires the use of physics. However not all objects will behave the same, for instance an enemy that moves only on the ground will have no need for a jump. Even more how should the player script be written so that the player controls are only done in it while it is the PhysicsBase script who resolves them? I guess that would be the next problem to solve. One way could be to make an abstract method called "FixedUpdateCalls" that PhysicsBase calls and have objects that extend PhysicsBase to add their movement/jumping methods inside it. That way PhysicsBase and any object below it can work together and the whole collisions code only has to be written once.

In other news, I added a new sprite that works as a background. I added a new tilemap and set it behind the "default" realm. This now allows for prettier scenes. Neat! This still is not a persistent background however, but I did find by some examples that a persistent background is pretty much a UI with the elements on back instead of at the front. Also the enemy now reacts to bullets by logging in a message, next up it should be able to die from them.

- [ ] Add UI elements
- [ ] Add a persistent background
- [ ] Make the "enemy" script and have it react and die to bullets
- [ ] Add animations from a sprite sheet
