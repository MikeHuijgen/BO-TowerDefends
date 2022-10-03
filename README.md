# BO-TowerDefends

## Tower Defense Mechanic
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

