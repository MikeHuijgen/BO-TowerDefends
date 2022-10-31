# BO-TowerDefense
## My tower defense project
### In this project we need to make a tower defense game. My game was inspired on the famous tower defense Bloons TD 6. I wanted to remake Bloons in 7 weeks. For the most part I succeded, there are somethings that are missing. 

## Mechanics
### The basic mechanics are simple. You have your map where you can place your towers. There is a path in the map where the balloons will float on. They follow a waypoint path to the end of the map. If the balloon is in the range of the tower, the tower will attack the balloon. If the balloon dies you wil get gold to buy new towers or upgrades.

### In Bloons TD 6 there are a couple mechanics that are more difficult to make then the basic mechanics. To begin with the layers of the balloon are different. Each layer has his own speed, health, scale, color, and mesh. I use this information to let the balloon know what he needs to do. If the balloon lose lives the layer change. Bloons also have a sort of preset waves. I wanted to create a similair thing. With scriptable objects I made an system that you can drag and drop the layers of the balloons you want in that wave into a list. Every time a balloon spawn in the game, it takes the layer he needs and the amount of balloons that need to spawn in the wave.

## What I learned
- To plan a 7 week project all by myself
- To work better with UI in Unity
- How to work with scriptable objects
- How a object pool works and how i can use it in my game.
- I now know how I can make system that is easy to use and easy to change. 

## Tower Defense Mechanic flowchart
```mermaid
flowchart TD

start((start))-->|In Awake|populatePool
populatePool-->setDeactive(Set all balloons on disabled)
setDeactive-->waveStart{Wait for the wave to start}
waveStart-->nextWave(Next wave started)
nextWave-->inPool(is a balloon not active in the pool)
inPool-->isNotEnabled(Yes)
isNotEnabled-->|Take the balloon information|balloonSetActive(Set the balloon active)
balloonSetActive-->move(Go to the finish)
move-->balloonHit(If the balloon get hit)
balloonHit-->destroyLayer(Change the layer of the balloon)
destroyLayer-->|If there are no more layers|disableBalloon
disableBalloon-->waveStart
waveStart-->|Completed all waves|youWin(You won the game)

