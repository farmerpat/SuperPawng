SuperPawng
==========

Description
-----------
**pong+++**

TODO
----

- [ ]convert tabs to whitespace
- [ ]properly handle the ball colliding with top/bottom of paddles
- [ ]handle player score condition
- [ ]display score
- [ ]determine victory conditions. implement
- [ ]migrate images to ps
- [ ]finish intro music
- [ ]start level music
- [ ]clean up the code

Possibilities
-------------

- projectiles
    - in rarer cases, projectile will be homing
    - in some cases, projectile will be the temporary freeze effect
    - in others, it will be the displacement beam
    - in others, it will temporarily annihilate the enemy
        - or totally annihilate blocks
    - maybe pc players can switch between projectiles w/ spacebar
        - the homing beams should stay rare (like a crit)
- power-up delivery
    - instead of dropping like arkanoid, shoot them toward player/enemy
    - consider a robotic arm or cannon of some kind
        - extends from the top-center
- handhelds
    - force landscape view
    - swipe up-down to move paddle
    - tap to fire projectile
- AI
    - make AI randomly do the wrong thing in easy-mode
    - we could randomly set the AI's collisionTolerance for difficulty
- Levels
    - place Arkanoid-style colums in the center of the screen
    - change board to be narrower on AI's side when difficulty is high
        - or just make it much wider on player's