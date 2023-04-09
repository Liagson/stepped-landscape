# Stepped landscape generator
A procedural generated landscape formed with irregular polygons. Farewell rectangular grids!
![SS1](https://raw.githubusercontent.com/Liagson/stepped-landscape/main/Pictures/screenshot1.png)

# Algorithm

1. [A voronoi diagram](https://en.wikipedia.org/wiki/Voronoi_diagram) is generated though a random set of points. 
2. The cells of the diagram are given a height set by a Perlin noise. If the `cliffs` bool is set on the `MapGenerator` class then we switch from two perlin noise maps through the value of a third noise map.
3. The diagram cells are turned into a mesh through triangulation. **It really helps that these cells are always convex**
4. Walls are added from the voronoi cell sides. The walls go down to the bottom of the scene.
5. Trees are added on the positions of the set of points of Step 1 (at random + not under water + not on the snowy parts). Trees are set through the `forests` bool on `MapGenerator`

**Note:** This is actually my fourth attempt to make pretty landscapes with unity ([1st](https://github.com/Liagson/procedural-landscape-generator), [2nd](https://github.com/Liagson/godus-style-landscape-generator) and [3rd](https://github.com/Liagson/procedural-landscape-generator-v2) attempts) and might not be the last, so check my repos in a few years :)

# Missing Asset

**To show trees you will need to add a prefab to your unity project with a tree**. The ref to that asset should be given to the `Forest` class. Because i am using one that's not free I cannot add it to this repository. for the screenshots I am using Tree6.006 [from here](https://assetstore.unity.com/packages/3d/vegetation/trees/low-poly-trees-seasons-67486).

# Acknowledgements
Some of the stuff added in this repo comes from other devs:

* The Voronoi library [comes from here](https://github.com/RudyTheDev/SharpVoronoiLib). I wouldn't have done this project without it!
* The Perlin noise implementation [comes from here](https://github.com/SebLague/Procedural-Landmass-Generation). Thanks again Sebastian :)
* The Water shader [comes from here](https://assetstore.unity.com/packages/tools/particles-effects/lowpoly-water-107563#description). It's free!
* The Triangulator class comes from a unity wiki post that [no longer exists](http://wiki.unity3d.com/index.php?title=Triangulator).
