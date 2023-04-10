# Stepped landscape generator
A procedural generated stepped landscape based on irregular polygons tiling. This approach gives us pleasant shapes that avoid the ugly squareness nature of using a distorted rectangular grid solution.

![SS1](https://raw.githubusercontent.com/Liagson/stepped-landscape/main/Pictures/screenshot1.png)

# Algorithm

1. [A voronoi diagram](https://en.wikipedia.org/wiki/Voronoi_diagram) is generated though a random set of points. 
2. The cells of the diagram are given a height set by [Perlin noise](https://en.wikipedia.org/wiki/Perlin_noise). If the `cliffs` bool is set on the `MapGenerator` class then we switch from two Perlin noise maps through the value of a third noise map.
3. The diagram cells are turned into a mesh through triangulation. **It really helps that these cells are always convex**
4. Walls are added from the voronoi cell sides. The walls go down to the bottom of the scene.
5. Trees are added on the positions of the set of points of Step 1 (at random + not under water + not on the snowy parts). Trees are set through the `forests` bool on `MapGenerator`

# Dependencies you will need
Sorry for not adding everything needed to make this work :(

* SharpVoronoiLib library: You will need to add to your project the [SharpVoronoiLib repo](https://github.com/RudyTheDev/SharpVoronoiLib) to make the project work.

* Missing tree asset: To show trees you will need to add a tree prefab to your unity project. The ref to that asset should be given to the `Forest` class. Because I am using one that's not free I cannot add it to this repository. For the screenshots I am using Tree6.006 [from here](https://assetstore.unity.com/packages/3d/vegetation/trees/low-poly-trees-seasons-67486).

# Acknowledgements
Some of the stuff in this repo comes from other devs:

* The Voronoi library [comes from here](https://github.com/RudyTheDev/SharpVoronoiLib). I wouldn't have done this project without it!
* The Perlin noise implementation [comes from here](https://github.com/SebLague/Procedural-Landmass-Generation). Thanks again Sebastian :)
* The Water shader [comes from here](https://assetstore.unity.com/packages/tools/particles-effects/lowpoly-water-107563#description). It's free!
* The Triangulator class comes from a unity wiki post that [no longer exists](http://wiki.unity3d.com/index.php?title=Triangulator).
