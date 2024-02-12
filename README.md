# Unity3D Build & Defense V2 (Square Grid)
This version is inspired by the game *Shogun* developed by SEGA. It includes building resources touwers to collect resources, buying different towers and units, moving units by commands, A* pathfinding for units, and behavior tree for enemy AI.

---
## Technical features:
- **Build resource towers and collect resources:**
  There are different types of resources such as stone, crystal and wood. Based on the names of the towers, you can collect the according resources when you build the towers. The resources are collected faster when the towers are placed near the resource nodes (indicating by the + signs next to the tower when you initially place them)
  <img src="https://github.com/ngol0/Unity3D-BuildDefend2/blob/feature/hex-grid/0.gif" width="900" title="build">
  
- **Buy units / towers from built towers:**
  Once towers are built, player can select different towers to buy different units/towers using the resources they've collected.
  <img src="https://github.com/ngol0/Unity3D-BuildDefend2/blob/feature/hex-grid/1.gif" width="900" title="buy">
  
- **Unit movement commands with A-star pathfinding:**
  Each unit can be commanded to either move forward or stop. Unit will find their way to the end goal using A* pathfinding algorithm. 
  <img src="https://github.com/ngol0/Unity3D-BuildDefend2/blob/feature/hex-grid/2.gif" width="900" title="unit movement">
  
- **Unit fighting systems/weapon:**
  Different units have their own way of fighting when encountering an enemy on their way.
  <img src="https://github.com/ngol0/Unity3D-BuildDefend2/blob/feature/hex-grid/3.gif" width="900" title="unit fighting">
  


