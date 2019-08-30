# Dissertation
Unity VR Audio Dissertation Materials
---
### Day 001
Set up a dissertation repo with .gitignore for Unity and Windows. Installed Unity Hub and the latest version of Unity (2019.3.0a4). Created a Unity project using the 3D with Extras template. Followed instructions [here](https://thoughtbot.com/blog/how-to-git-with-unity) to set up git with Unity, ammend Unity's version control, and use git large file storage. I built a 6-plane room and a cube in the main scene. I still need to access a VR kit, download the SDK, apply that to my Unity project.

### Day 002
Learning about the Unity audio system. I can record a microphone input (`Microphone` class) and the VR environment output (`AudioRenderer` class). This means a user can record into the VR environment, manipulate that audio, then record the manipulated audio output. Sampling and re-sampling seems doable. Sample data is accessible through `AudioClip.GetData` method, filling an array with floats from -1.0f to 1.0f for each sample in the audio clip. I think I can playback a clip from a specific sample using `AudioSource.timeSamples`. I'll have to try the example Unity code to understand more. I'm going to sample a Max 8 birds audio clip, place it in the Unity scene, and learn how to play back from any position.

To play back from a specific sample, set `AudioSource.timeSamples` to the sample on which you would like to start before calling `AudioSource.Play();`. To add a delay to the `Play()` method, provide an argument in samples inside the parentheses. I have made a scene that plays an audio file from the beginning when the user presses 'Enter', and pauses/unpauses when the user presses 'Space'.  

### Day 003
Today I think I will learn how to scrub/seek through an audio file in Unity. I am going to try to use the mouse x position as the `AudioSource.timeSamples` integer to see if I can scrub through an audio file. With some mild scaling, during `Update()` I can declare the mouse x position to be the sample from which to play. I hit 'Enter' to initiate playing, and the audio loops on the sample where mouse position x is, and scrubs through the audio file as I move the mouse left and right. Can I make the sample window larger? I don't know how to yet, because `AudioSource.timeSamples` is an int. My thought is to use it as an initial starting point but have the audio play only 512 samples from that point. So that point + 511 samples would be the play length on loop. I still need to work on this.

### Day 004
Unity Audio Engine video notes:
- Spatial Blend curves quite useful for switching between 3D and 2D on the fly.
- Audio profiler great tool to inspect how sounds are playing frame by frame.
- Audio Mixer for routing, submixing, effects, hierarchical mixing.
- Audio Mixer can use native plugin and custom GUIs.
- Mixer can interpolate between snapshots.
- bitbucket.org/Unity-Technologies/audiodemos
- Native audio plugins sdk <-- Look into that, it may be in C or C++
- Prototype in C# using OnAudioFilterRead
- Real time game parameters can change plugin parameters.
- Procedural generation with modal filter.
- Teleport transmits audio between external applications and Unity. Hear audio in Unity while you work on it in your DAW.
- As of 2015, Unity is working on sound design possibilities in editor and multiple audio listeners.

I downloaded the Unity native audio plugins from [bitbucket](https://bitbucket.org/Unity-Technologies/). Native Plugins are worth building if you know C++, but also worth using their examples if you don't know C++. They have granulators, ring modulators, vocoders, EQ, and reverb. It may be worth more to use their tools to develop VR interactions for parameter controls as compositions than to learn C++ and write my own granulator or equalizer. Of course, if I come up with something I want to do that these plugins can't, then I'll need to write my own plugins. I'll certainly need to investigate how a real-time game parameter can control the plugin parameters before moving much further ahead.

### Day 005
Let's try out the Unity native plugins today! Can I get them working in 2019.3.0a4? They work fine in 2017.4.3f1, but GUI and Substance work differently in 2019 so I'll have to find a work around. I figure the best way to go about it is learn how they Mixers are used/programmed to do the SFX and try to build them in my own 2019 version. Surely that will be more beneficial than getting some demo scenes running in 2019 only to still have to learn how it all works. I think I'll have to set up a dll project in VS2017, copy and paste the headers (.h) and implementations (.cpp) that I want/need and build the dll on my machine. Then, I suppose I should import the dll/plug-ins into Unity.

Next step is to start a new 2019.1.5f1 project, get the dll working inside there, and start trying to map real-time game parameters to the plugin state parameters.

### Day 006
"Dissertation_VR" 2019.1.5f1 project started. I can't believe it, but it works now. I added the audio plugin demo .dll, the demo gui .dll, all scripts and mixers related to the .dlls, and they are working. I had a message that said the plugins were trying to register themselves or were already registered. The messages were red so I assumed it meant the project would fail to load, but to my surprise it worked. I cleared the messages, started the scene again, and no failures were reported in the console, so I assume nothing is wrong. None of my google searching led me to find out what those messages meant or how to resolve them.

Now I will make scripts that change plugin parameters based on object transforms. Reference a mixer, which contains audio effects, in a script and use that reference to change effect parameters. To expose effect paramaters to scripting, right click the parameter on the mixer group and select expose, then you can manually change the scripting name under the exposed parameters menu in the audio mixer tab.

My first script attempt has failed. I will read more about calling transforms in scripts tomorrow.

### Day 007
Let's fix the script! So, for future reference, I should actually read the error messages to solve my problems. It was unnecessary to use `GetComponent<GameObject> ();` on the sphere and audio mixer in my code as they are assigned in the inspector via drag and drop and are not attached to the object with the script component. Let's build a little marble madness level and see what happens.

I've made a box with a ball inside. I want to use WASD to control x and z position of the box and use the mouse for y position of the box. This will mean controlling the box and shaking the ball, which will use the vocoder parameters I exposed previously. The ball falls through the floor, I need to fix that. I think I want to click the box with the mouse and then shake it based on mouse movements, ignoring WASD. The whole thing is janky right now, gotta clean it up tomorrow.

### Day 008
I'm going to take the rigidbody components off the planes, in exchange for box colliders in hopes the physics work correctly. So I've just made a new scene that adds forces to the ball, which rolls around the plane changing vocoder parameters. I'm trying to fine tune the parameters now. Inside Plugin_Vocoder.cpp tells me the definition of each parameter on the plugin. Unity's API should clue me in to how to scale the sphere's rotations and positions to stay within the bounds of the vocoder's parameters.

The latest scene (001) has all transforms scaled correctly, but the parameters aren't necessarily mapped in a meaningful way. Using WASD and the Up and Down Arrow keys to control the ball in the scene is awkward as well. This is where the VR controllers come in handy. With experimentation, meaningful mapping can be derived. For instance, the rotation parameters are just constantly oscillating in this version. With a VR controller, users can deliberately rotate for effect and make conscious decisions about the sound. I need to get my hands on those VR controllers upstairs. Also, that the ball relies on gravity exclusively is a problem. VR controllers take advantage of three dimensions better than keyboards, so with VR users can intuitively control vertical and horizontal positions of the ball moreso than a keyboard mapping. Also, I changed the looping audio to a song and am having way more fun than with the voice.

### Day 009
Start with updating character controls; remove AddForce.

I have a new character controller and camera controller which work like a walking 3D character in an MMORPG. This isn't exactly what I need for a rolling ball, but it is better than the AddForce movement of the previous script. I should probably add another AddForce for moving up and down the Y-Axis, then work on getting some objects in the level that will cause the ball to rotate.

### Day 010
I set up the Vive today. Next, I will try to use it in my game just to see what happens. The game works with the keyboard controls laid out in day 009, but the camera doesn't work the same since the headset is now the camera. Day 011 should focus on adding VR camera and control scripts.

### Day 011
First, I'll download the Vive SDK for Unity, then I'll start working on scripts for camera and controllers. Vive Input Utility from the Unity Asset Store is really what I want. Vive Wave SDK is for mobile VR. There are also Vive Audio and Hand Tracking SDKs, but I'm not looking too far into them yet. There are some drag and drop scripts to make items grabbable and teleportable, but fine tuning is required. I'm kind of beat already from the VR, it is more fatiguing than nauseous so far, likely because I'm seated. I think standing and walking around causing equilibrium imbalance that leads to nausea quicker. So for tomorrow, do some fine tuning, dig into what the scripts mean and do, and make the scene work well in VR. 

### Day 012
I'm going to watch some vids about what the VIU scripts do and how to use them in my own scenes. Finally got things working right. I was trying to add a few Vive component prefabs at a time, but really, I just needed to add Vive Rig prefab and everything works like a charm. Always read the guides/READMEs/tutorials included with your software/hardware. So, I can play with the ball and listen to the music and control the music more intuitively now. This still needs improvement. The scale of the scene is massive when in VR, the sphere is around half my size, the ceiling is unreachable, the walls are massively far apart. I should rescale a new room with interactivity in mind. think of bowling, Newton's cradle, pinball, rotational gusts, etc. It's a real Mario Maker in here now. 

I've got a lot of freedom, so start with the limitations. I do want multiple sound objects, perhaps I should create three types: sphere, cube, cone/pyramid. Each will behave differently, but I can lay the groundwork for mass producing these objects in scenes. I should think three-dimensionally. Consider height as well as length and width. The first addition to the scene will be a big ramp. Objects can speed down the ramp and fly off to land on a bouncy surface.

### Day 013
Investigating procedural meshes for rendering dice in-game. I watched the board to bits youtube series on 3D object generation with procedural meshes and learned a lot. I think I can use it as a reference to generate my own polyhedrons. I'll start with a cube following his instructions, then try 4, 8, 10, 12, 20-sided polyhedrons. Or I'll just check the asset store to download some, as I'm sure they've been made already. I just think the procedural method should be lightweight and flexible. Upon further review, the Unity asset store doesn't have anything adequate for my needs. Procedural generation begins tomorrow.

### Day 014
Coded a procedural cube following Board to Bits' YouTube series. It looks good, if a little large. Nothing scaling can't fix.

### Day 015
Coded a procedural octahedron based on the procedural cube code. Took me a while to get the triangles to face outward, something to do with the order in which the vertices are called and culling. Anyway, got it figured out, but then tried to add mesh colliders to both the cube and octahedron. Updated the code to require the mesh colliders and to use the mesh generated from the shape's data as the mesh in the mesh collider. Added rigidbodies to the objects. They now just fall straight through the floor. Next step is to figure out how to solve that. Rigidbodies + MeshCollider with convex checked is supposed to work, but doesn't. As I tried to change the floor's collider, the sphere I have been using began to float off the ground. Peculiar.

### Day 016
Investigate colliders and make the scene's physics work correctly. Jeezy Petes, it was an order of operations issue. I need to generate the mesh first in `Awake()`, then in `Start()` declare the mesh to be convex. doing it in the reverse order meant the mesh that was declared convex didn't exist and the newly generated mesh wasn't. at least I figured it out. Oddly enough, leaving the ocatahedron at a 0-rotation point allows it to fall into place standing on the southern tip of the die. 

### Day 017
Working on making all faces visible on D10. 6 of 10 are visible, so I thought it was an issue of triangle meshes being drawn backwards or something. The problem with that is, if it were being drawn backwards, the inverse would be visible. As of now, 4 of the kites have no faces inverted or not. I tried to brute force 24 variations of one kite face and no matter the configuration the D10 looked the same every time. When I altered a visible face, one of the two triangles making the face disappeared. I'm going to walk around, maybe ask Jesse for some troubleshooting ideas. I should also rewatch the Board to Bits YouTube series. 

**AAAAAAHHHHHH** I figured it out. The reason I wasn't seeing an inverted face is because my for loop, which makes faces, was only counting up to 6, not 10. Changing that one number solved my 2 hour problem. Note to self: carefully read your code while troubleshooting.

Tomorrow, build a D4, D12, and D20.

P.S.
I'm in the new office, out of the studio. Got all of that stuff transferred and setup today. Feels good.

### Day 018
New GPU from Derick! Let's take it for a spin. After much copying and pasting from dmccooey.com/polyhedra, I have a D4, 6, 8, 10, 12, and 20. All meshes are perfectly sorted. The code leaves a bit to be desired. I've talked to Jesse about moving the `MakeD`*n*`()` methods to the same scripts containing the static data, but I'm going to have to look into making methods work inside static classes. I'm going for a short walk and will tackle that next. After getting static class methods to work in public classes, I should start learning how to spawn each item in-game.

The static methods can be called by non-static classes, but I need more info to make it work. Meanwhile, I learned how to spawn items in-game and it is super cool. I declared a variable for an empty object, then made a simple if statement saying if "n" is pressed, spawn a D4 inside the empty game object at my location. I can still tweak the location and how it spawns, but I'm super pumped about it. Also, I need to require or somehow add the renderer material I'm using to the classes defining and creating the shapes so when I later spawn them they will already have materials.

### Day 019
Let's start with the renderer material sorting, then start a UI to choose which shape to generate. It is also worthwhile to see about learning what face is making contact with the floor, or just how to differentiate the faces via scripting. My first thought is to check which vertices are touching the floor and derive what face contains those points.

---

### Jesse Meeting 001
Two goals: "Hello, World!" the Native Audio Plugin SDK and build more cubes to learn about interactions with Unity's Audio Scripting API. How does audio pass to another cube? Eventually, you'll want to get modular with your object building, so think about preparing your code to be used as a library that will, in three or so lines, be able to make several thousand objects with several thousand audio clips, etc.

Additionally, get your hands on a VR development kit with some controllers and put a character in the scene.

### Jesse Meeting 002
Had a good meeting demoing the plugins for Jesse. Showed off the Mixer and all its abilities, C++ projects, GUIs for plugins, etc. Agrees that I should start with a stable release of Unity and get the open source native audio plugins from Unity working in my own project.

### Jesse Meeting 003
Show him the 3D UI for the Unity vocoder. Eventually, I'll need to define mapping in convenient, easy ways for users. First thing to do this week is develop more interesting levels for the ball to roll around in. Make a vortex that changes the rotation values, add some columns that bounce the ball away pinball style, make the level itself a funnel or cylinder or something. Play around and have fun with some new mechanics that make the scene playable/performable.

### Jesse Meeting 004
Learn to make dice. Record your own sounds (impulse, ambient, rhythmic, etc.). Have fun, explore. Don't narrow vision on one idea yet.

### Jesse Meeting 005
Combining dice creation into 1 function call. Include empty object generation when calling this so you can call them in game. Your goal is to make making music doable in VR. To get there you need to set up all the scripts to enable how much work you can actually do in VR rather than switching between Unity and in-game to make music. Start organizing the way you will work in-game so you can spend more time there playing and making music.

### Jesse Meeting 006
How can my generator files read standardized object files to simplify the process?