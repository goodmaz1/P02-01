# PR02.01
## Kinematic Seek, Flee &amp; Arrive Project


The exact same thing as DV02.01, just with an audio clip for fun. The readme from DV02.01 has been copied below:


Dark Blue Box (TARDIS) - stationary target

Orange Dalek - SEEK function

Red Dalek    - FLEE function

White Dalek  - Arrive function

Blue Dalek   - Arrive function


So the reason this took me so long is because I tried to fix the bug where the white and blue Daleks (the Arrive ones) get close to the target, stop, and then move away after a second...only to come back, stop, and move away again, oscillating on an endless loop. I *know* it's because I'm changing the acceleration applied by a fractional amount each time update is called, and over time that builds up and sends the units back in the opposite direction, but trying to set the velocities to zero once it reaches the target means that if I move the target, it doesn't have any velocity to start moving again.

Honestly? It's been two/three weeks of ripping my hair out over this bug and I"m done with it. It's mostly good enough

NOTE: I get a curl error 56 for some reason, but after an hour+ of searching, that seems unrelated.

NOTE: Also, whenever there's a collision between two objects (usually two daleks), there's a chance that one of them just shoots off into infinity, never to be seen again - even if I constrain the y-position. Looked into that too - no idea why. Yay Unity Physics.

NOTE (Final): This is the same project for both the Development & the Project for M01-Dv/Pr02.01 (This just plays an audio clip bc why not?)
