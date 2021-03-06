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
Let's start with the renderer material sorting, then start a UI to choose which shape to generate. It is also worthwhile to see about learning what face is making contact with the floor, or just how to differentiate the faces via scripting. My first thought is to check which vertices are touching the floor and derive what face contains those points. Instead of UI, I'll first update the generate script to include functions for each shape.

I should probably start a trello board to keep track of progress. (github does this in the projects tab!)

Materials: if you have a material you want to put on an object in game via scripting, you can add that material to a "Resources" folder in your Assets folder. Then you set the material to `Resources.Load("nameofmaterial") as Material`. You can do this with other assets as well. In my case, I am generating objects in-game so there is not an object in the hierarchy or inspector window to which I can add a material. You can't put a material on what doesn't exist. Keep in mind, the Resources folder loads everything in the folder during the build, so only use what is necessary in the Resources folder. Unused items will still load in the build.

Generating: I updated the generation script to include all shapes, and for some odd reason, the D6 is generated twice on key down. The procedural D6 script doesn't seem off, but I'll go over it some more. All other shapes generate one at a time. It isn't the number I am pressing. I switch D8 and D6 to see if it was the keyboard, but D6 still generates twice on both `GetKeyDown(KeyCode.Alpha2)` and `Alpha.3`.

Faces: One thought is to put colliders on each face and determine which face touches the floor by learning which collider touches the floor. It is cheesy, and maybe too much work. I'd like to think you could find what vertices or triangles are colliding though without the extra colliders. `Collision.GetContacts` may be the place to start. It returns contact points, but I don't know what that really means.

### Day 020
I'm first attempting to solve the D6 spawn issue. I'm going to replace my mesh data with data from dmccooey.com/polyhedra. Actually, every item is spawning twice, but some of them are small enough to clip through the floor. It may be the `GetKeyDown()` method that is spawning two at a time. It is now likely not the key method. I'm thinking now that I'm doubling the creation by making an empty game object that makes a game object. I can't confirm if that is what is happening yet. **SOLVED:** there were two copies of the Generate Object script in the scene.

I'm still thinking about gathering vertices to determine the face that should play audio. ProceduralD4 calls `MakeD4()` which shuffles through a list of vector 3 vertices. That list of vertices can be references to D4MeshData's `int[][] faceTriangles`. The first 3 vertices returned by `MakeD4` represents the 3 vertices contained in `faceTriangles[0]`, the 4th-6th = `faceTriangles[1]`, etc. Each shape has a list of vertices equal to the number of vertices per face time the number of faces. D12, for instances, has 5 vertices per face and 12 faces, equalling 60 total Vector 3 points in the vertices list. This information is hopefully handy. Can I find out which 3-5 vertices are making contact with the ground to determine which face is making contact with the ground?

### Day 021
I'm starting with cleaning up shape scripts to be 1 script per shape. Well, I copied and pasted the entire meshdata script of one shape into the procedural script of the same shape and it worked without a hitch...auspicious or foreboding? Now that I've finished that, I can start working on iterating the name of each new shape in my generation script. The generation script is slightly more clean now. I have 6 counters, 1 for each shape, that count up as more of their respective shapes are generated. There is one method now `generateDn()` that has a large if/else if chain, but I'm having to re-use a lot of similar code. I wonder how I can clean things up more. Variables are helpful, but still result in needing 6 options. I can't figure out how to make the `AddComponent<ProceduralD4>()` variable. In this case `ProceduralD4` is a type, but I can't make a variable a type of type. There is something I'm not seeing that can make this easier, a little break from it will do me good. Anyway, it works, it names what object is generated with how many are in the scene already, starting with 0 of course. I'll eventually want to tie the code to Vive input rather than keyboard. I think I want to trigger a popup display when the trigger and surface are pressed together and have the user move their thumb around the surface to decide what to generate. That is for a later day.

I still need to look at rotation to determine which face is up on each shape. Let's start on that! Boy oh boy do I have an immediate issue. Objects can spin infinitely in any direction so any calculations with have to use modulo operations. I fear this will cause trouble. I'm sort of researching best methods and quick ways to calculate what angle my shape is at and it might be quaternions that help, but I could probably use a rotation matrix as well. I really think there is a simpler way I'm overlooking, but I'll think on it and come back later to work it out.

### Day 022
I was technically wrong about the rotation issue on day 021. In game, while rotating, the numbers will not exceed 360 degrees. This has to do with Unity using quanternions and translating them into Euler angles for human-readable numbers. Anyway, onward with solving the rotation issue!

I have no idea what I am doing.

The rotation thing seems too convoluted. Getting colliding vertices seems bizarrely difficult...idk.

### Day 023
Keeping at it. Going to find out if I can name each face as it is created and perhaps get other attributes or something from it. Check out normals link Jesse sent.

### Day 024
Still working on it. I'm first going to try finding the normals of a colliding face and see what I can do with that information.

I have something. I can find the contact points on my object and I can get my object's vertices and translate them to global coordinates. Now I need to match them to compare vertices to find what face is touching. I use a `Vector3[]` for each face on the D4 defined by `int[][] faceTrianglesD4`. Then I transform the `Vector3[]` into global position and turn it into a `string`. The code is awkward and has 3 vertices for each face that I have to compare to the three `collision.GetContact().point`s after converting them to `strings`. I am sure there is a better way of managing the data I'm using, but cannot seem to figure it out yet. I think using a `List<T>` to hold the data and comparing the contact points with the list might be the way to go. Again, I'm not sure how that works. I'll look up how to use and compare lists so I don't have 60+ lines of code for the D20 just defining faces.

### Day 025
What if I use a for loop to add vertices to a list while creating the object?
Holy shit, I finally did it. My method is super convoluted, because I don't know how to make it modular yet. I have a `Vector3[]` for each face of an object. I convert each of the 3 indices of each `Vector3[]` index from local to global positions as strings in `OnCollisionStay()` while also converting collision contact points to strings. I declare a variable for a face, f1 for instance, containing the 3 string indices of each `Vector3[]` index and search for the face that contains all three collision contact point strings. This is the simplest way I could figure out doing if statements, by searching for strings containing other strings.

Now that the logic is in place, I need to set up the audio source component for the object and try to get a different audio file to play based on which face is touching the floor.

### Day 026
Meeting Jesse soon. Going to troubleshoot the camera issues I was having. I'm pretty sure I just need to reset the cam position, but we'll see.
Indeed, the cam just needed a reset. My D4 script is now more modular. Next time I'm in I need to apply the same techniques to my other scripts. Then I can load all of my shapes up with audio and start making some darn music!

### Day 027
Updated Dn scripts to their respective modular versions. Biggest issue I had was with the D12. The D12 has 5 vertices per face so I assumed using the same method of finding each contact point (5 in this case), I could compare to global positions of D12 vertices. Nothing was happening, though. So I checked each contact point and found out there are only 4 contact points in the collision. So now I'm comparing a face with 5 vertices to 4 vertices in contact with the ground. It seems stable enough, but not ideal. I expect more issues, honestly. But as long as it is working now, I'm happy. I wonder if the D12 has faces that aren't flat.

I'm having garbage collection issues. When I spawn several objects, the frame rate and audio skips and slows down. There are some tutorials from Unity addressing similar issues that I should look into more. The Unity profiler shows that each shapes `OnCollisionStay()` process is running up the garbage. This is definitely a must-fix. Jesse may know more. I'll work on it next time.

### Day 028
In an effort to control garbage, I'm going to put samples to play `OnCollisionEnter()`. That didn't work.

I've spent a long time trying to figure out how to play samples based on which face is touching the ground. However, this is not exactly the best way to play music. Some problems from this first iteration include: Stuttering audio and frame rate drops when new objects are added to the scene, objects touching the ground play audio when another object comes in contact with them (false collision registration), and audio not always stopping when objects are lifted and dropped in the same place and don't always replay audio when landing.

Actually, the stuttering might be caused by the `Pause()` method. I'm going to switch to `Stop()` and `PlayOneShot()` for some new ideas.

I did some experimentation today with garbage collection, pause vs. stop, and collision tags and layers. What seems to be slowing things down is the reactivation of a collision when two objects collide and either is touching the floor. That will cause audio to replay and stutter, which seems to slow the frame rate. I need a solution for this. I also need to evaluate what I will do with this particular scene. There is more musical interaction to add that I haven't even touched on and to change how I'm working now would be premature. After the Cinema for the Ears concert last Monday, I'm getting intrigued with the visual elements generating playback somehow, but that is another scene for another piece. Start considering the collision solutions, add some effects interactions, and get in the game and make some music. After that, I'll have a good idea how to proceed, but working on a piece with what I have is incredibly beneficial to the overall outcome of this project.

### Day 029
After meeting with Jesse last week, I decided to start working on time in the scene. My first iteration is a cylinder that scales in x and z axes, forming a circle across the plane. Each object that enters the time cylinder trigger plays whatever audio is currently loaded in its audio source (which is already determined in `OnCollisionStay()`). To make the time cylinder change size, I used Unity's built-in animator on the object. Right now, the time cylinder scene works similarly to the previous scenes, but the cylinder trigger plays each object after it comes to a rest. This is freeing. Before the passage of a time loop, I would have to pick up an object and roll it to make the audio continue. This scene hase constant playback which is based on position of objects and the speed of the cylinder's growth. I'm going to add a second cylinder to the scene that plays at a different speed, then try to add layer functionality to filter out on what each cylinder triggers playback.

I built a second cylinder that travels faster than the first. I had to rewrite the trigger script to put `Stop()` instead of `Pause()` before `Play()` to have the sample play from its beginning as it enters the cylinder. Next time, I'll mess with layers, but what would really be great is being able to move the cylinder wherever I want to in the scene. Perhaps eventually changing it to a sphere that grows in all 3 directions would make that work well. Eventually, the timer should be able to be spawned in a scene wherever I want and have whatever speed I want. That way I can use several in 1 scene, and have them use layers that trigger different samples for each time object.

### Day 030
I'm going to mess with the time cylinders and make them each unique. I'll see what layering can do for me. I think if I use layers on the time cylinders, I can decide to use different sample sets in each object.
So, actually, I set the time cylinder to a specific layer, then put the `OnTriggerEnter()` on the D*n*. If the trigger's layer is *a* then use *x* array of audio clips, if the trigger's layer is *b* then use *y* array of audio clips. I'm able to do this by declaring a `faceVal` variable that is taken from a for loop in `OnCollision Stay` where *i* = whatever face is touching the ground = the position in the array that defines what clip is loaded. This all seems to work fine.

I put some new sounds in the scene, percussive field recordings, and they sound OK in moderation, but I want more from them. Perhaps that is where using mixers and effects comes into play. The next two things I need to work on are the registry that knows everything about every object in the scene and how to save a scene as a preset in-game.

### Day 031
I'm here to work on building some code that will be dynamic and allow me to add sound files via drag and drop and then have the code name them and attach them to objects automatically. I started a bit, didn't accomplish much, but I did see some videos on how to edit Unity's inspector and read about C# collections, dictionaries, and lists.

### Day 032
I'm just going to work on audio script best practices today. I can't seem to get audio clip arrays to work still smh. I'll try again tomorrow.

### Day 033
Making one audio script that every D*n* can use is, I think, the best way to go. I'll start with making one that can change clips easily and is dynamic, prepared for adding/removing clips to the pool.

I've got something. Right now the "AudioClipScript" is handling a jagged array of string values that correlate to audio clip names in the Resources folder in the Assets folder. I can't figure out how to get jagged arrays to work the way I want them to, but why do I need them? I could just add a new array for each genre of sample and store them in separate banks. I've got either way working just fine, but it still isn't as dynamic as I may need. I want random clips assigned to each bank/layer. Although, musically, random may not be the best way forward. Next time, I should see how much of the same code each script is using and try to separate the same stuff into one script and consolidate everything else. Then I'll have some nice prefabs to play with.

### Day 034
I tried to improve the audio script, but was unsuccessful. I'm getting closer. It is tough to decide how I should be handling audio right now. I think the array for each sample genre works well enough, I just feel like it could be better. Which makes me think, "Why not just use Wwise?" I suppose I've given myself the challenge of using Unity exclusively to test its limits, but I don't really know if I'm testing any limits here. I'd like some examples of professional audio scripts as references.

### Day 035
I accidentally figured it out. I was trying to `GetComponent<ClipArray>()` but had not attached ClipArray.cs to my Game Object. Now that it is a component of the D4, it can be got. Now, can I procedurally place every audio clip I have into the serialized arrays without having to drag and drop each file individually?

### Day 036
Make a texture showing time on the floor. Work on scaling objects, spawning in hand, spawning w/o keyboard, using a folder of audio clips to populate arrays.

I'll start with the texture, then research audio clips from folder.

I made a new floor for the scene using a cylinder with a mesh collider on it instead of a plane. I was having issues with objects falling through the plane floor, so this should help. The cylinder shape also helps in that the top is a circle, which matches the time cylinders' animations. Using draw.io, I made a texture out of rainbow colored concentric circles to make a grid as a visual timing system for users. I placed the grid texture into a material for the new cylindrical floor and it looks great! I've got a cool looking rainbow color scheme to identify some form of meter.

`UnityWebRequestMultimedia.GetAudioClip` seems like a reasonable place to start learning how to load audio clips from a folder. Try this next time.

### Day 037
Time to learn about unity web request. The web request example I tried worked, but it takes an exact file location and type. I'm now working with the System.IO library to use `Directory.EnumerateFile` and `Directory.EnumerateDirectories` to sift through a main directory filled with sub-directories, filled with .wav files. Once I crack that, I can stuff the .wav file results into a `List<AudioClip>[]` and pull clips based on sub-directory and file name. I'm getting close to it.

### Day 038
Time to finish what I started. I should be able to use the Directory class to add wave files to my lists. I've got my script enumerating directories and files, searching for any sub-directory and any wave file in all directories. It is finding what I am asking for. I think it is also adding it to a list, but if not, I will fix that soon. Then I need to use web request handlers on the wave file paths to convert them to audioclips.

Gosh, I was so close. I had the unity web request working, but now it can't connect for some reason. Not really sure what the problem is. I'll check back tomorrow, and probably get help from Jesse.

I think part of what is happening is the way the file names are being recording has both forward and back slashes, e.g. Assets/Audio\hammer.wav.

### Day 039
I'm trying to format the path so all of the slashes are facing the same way. I think Unity web request needs a full path name to work, so just having Asset/Audio\bangs\bang.wav won't work. I've made some progress with `Replace("\\","\");` and `Application.dataPath`, but am done for the day.

### Day 040
So, I need to make a modular path to the audio folder because a harcoded-string will only work on one computer. I'm going to try to get `Application.dataPath` to work for me in a modular manner, and research what it takes to open folders/files on any device playing the build of the scene.

I'm incredibly close. The file paths are acting fine now that I have added "/" to the end of each string, which forces the directory path to follow "/" format instead of "\". The coroutine is working as planned and I have a `List<AudioClip>[]` so I can call an audio clip like so, `clippyArray[0][0]`. The problem, I think, is that I am asking to change the `AudioSource.clip` to `clippyArray[0][0]` before `clippyArray[0][0]`'s lists are populated. I keep getting errors that say the Index was out of range. So if I call for `clippyArray[0][0]`, and it is out of range, that means it doesn't exist yet. I don't know how to arrange them yet. Real quick, I'm going to try to make an if statement and see what happens. Holy shit that worked. I made a `clipReady` bool set to false that turns true after the coroutine finishes. If it is true then, `clippyArray` can be called as the new audio clip! Finally, I can read audio from a folder!

### Day 041
Now with audio folder reading out of the way, I can focus on spawning objects using the vive controller instead of the keyboard. Ideally, I can spawn any D*n* into my hand in a held state.

I've started working on the script to spawn and am taking things in order of code. First, click menu, then pop up a GUI menu, select D*n*, spawn D*n*. I just built a scrollable list of selections, but the scaling is off so I'll have to read how that works, but honestly, I'm going to put it off until later. I think getting functionality is the best step right now.

The button appears on click, and spawns a D4 on click, but there are issues. I cannot see the button in VR, possibly a screen space/world space issue with the canvas. I cannot interact with the button using the Vive controller, just the mouse. Solving these two issues will go a long way.

### Day 042
First, I need to make the list visible to the Vive headset. Then, I need to use vive controllers to interact with the list, probably with raycasting.

Well, I finally looked up Vive's input development guide, and was quickly shown how to make an interactable menu in a few easy steps. **ALWAYS READ THE DOCUMENTATION.** I now have a menu that opens and closes after pressing the menu button on the controller, a raycast that highlights menu items, and triggers that select menu items. Right now, I can only spawn D4s in the middle of the room, but changing that comes next.

### Day 043
Work on scaling objects, spawning in hand, make prefabs of all objects to show of at seminar. Work on Saving first though, work on trackpad ui.

Based on my notes above, I will first learn to save the scene. Then I will make all prefabs spawnable, then the trackpad ui.

I can save a scene, but only one object in the scene. I've been trying to figure out how to expand this knowledge to saving everything in a scene. So every time I add a D*n* to the scene, I make it a child of a parent, empty "Dn Parent". Then I iterate through every child that parent has and save its name and transform position, hopefully to reload. I'm getting close, I think, to cracking this one, but it may take more time yet. Tomorrow, I should try to get all prefabs instantiating and fix the scrolling on the menu, just for presentation's sake. Then ask the devs how they save full scenes, and see if I can get some general advice.

### Day 044
Start with getting prefabs up to date, then work on the menu.

For the demo today, I have all prefabs spawnable, and removed save/load from the menu to ignore scrolling. I'll still want to ask about saving probably.

### Day 045
Make a capsule around the level that destroys gameobjects that pass through it. Done.
Make a dictionary that holds all new instances of D*n* prefabs and removes them when they are destroyed.

I'm working on it, but can't seem to reference and instantiate classes correctly in my code. Once I crack that egg, I'll be able to have a dictionary of every D*n* in the scene and keep track of their positions, possibly save their info.

### Day 046
After showing my demo, I had some good questions from the crew. One that stuck with me was adding a top-down map view of the scene, so I'm going to add that today. Then I'll get back to cracking the serialization problem.

The second camera is harder than expected in VR. I'm not sure how to do it.

### Day 047
Spawn in hand, scale objects while playing, moving terrain, saving progress, spawn time spheres, mini map, update spawn ui/ux, add wav during play. Added those to my scrum board.

Today, I will make objects spawn in hand. Well, my objects are way to big for this to be convenient at all. If I use smaller objects, it will make sense, but right now they just appear around the player. Better than nothing.
I also made a time cylinder today with variable sizes and speeds. I'm having a problem with the grabbing feature though. I have a parent sphere to show central location and a child sphere that expands and triggers playback. While a user is inside the expanded sphere, pressing the trigger will grab the entire object. This is not ideal. I only want to be able to grab the central sphere. I've tried switching parents, I tried adding a parent for the both of them, but nothing seems to work. I need to sort this and make it a prefab with parameters that account for what layer it is on, how big it is, and how fast it goes when spawned.

### Day 048
See if I can figure out the sphere problem. Then check the scrum board.

I solved the problem. I have an empty object called TimeAndSpace that holds a TimeSphere and a SpaceSphere. The TimeSphere's position is set to the SpaceSphere's position in `LateUpdate()`. Now, when I grab the SpaceSphere, the TimeSphere's collider is not interfering, and the TimeSphere always follows the SpaceSphere! I have set the TimeSphere to have variable sizes, speeds, offsets, and direction. I need to make them spawn next.

### Day 049
Work on TimeAndSpace spawn, layer attribute, and remove offset (which is a redundancy to min and max sizes).

I got spawning and layer attributes working. They could both be better. I removed the offsets and played around to hear what the timespheres do differently from the cylinders. After a few minutes, it became apparent that I need to build height into the level. 2D triggering like this happens in electroplankton. Height is going to contribute in meaningful ways. Think about how to incorporate it. Maybe remove gravity from the D*n*'s? But then how wil we know what side they're on? You'll figure something out.

### Day 050
So, on the topic of height in the game, I could make a large globe for the player to walk on instead of a floor. That way as the time spheres pulse outward, they can pass through the globe and contact D*n* on the other side. This sort of simulates height, but also on the globe I can add hills and valleys to exaggerate the importance of height. Just a thought.

I could also encourage stacking D*n*. Or have a way to elevate the segment of ground the D*n* is on, like a column that the player can extrude up or down.

Today I need to make the reverse time function properly and maybe move some code from ProceduralD*n* to the general audio script. There is probably a better way to organize what I have.

I've got an idea, but not sure that it can work. I'll mull it over. Basically, look at the name of the D*n*, then declare a variable of the class based on name. It isn't working the way I think it should. I think there is much to optimize in all of my code.

### Day 051
Working on UGUI for timespheres so I can make an etude. I'll add UI for copying, deleting, editing the spheres, which includes sliders for min/max sizes and speed and a checkbox for reverse option.

The UGUI exists, I need to connect it to the spheres so that they affect each copy individually, not all spheres at once. I'll also look at making the time menu hide the other menu so nothing is overlapping. I'm having to use sliders for the layer options because I couldn't figure out dropdown in VR, but it should be fine. I'm not adding the copy feature yet. If I can figure it out while learning how the menu script will work, then I'll add it.

### Day 052
I'm so damn determined to get the programming structures for this project correct. I'm going to spend today trying to use object-oriented techniques to make all of my objects better. It will slow me down, sure, but in the long run this is what I need and will help with other problems I having. I'm just reading through Microsoft's C# overview to learn these techniques and they seem, obviously, incredibly helpful. 

I'm so impatient. I can't afford the time this takes right now. I'm going to brute force my way into getting the ui menu for the spheres to work so I can write some music.

A very aggravating day. Suffice it to say, nothing works like I want. 

### Day 053
Back at it again. Should I do nested class with the space and time spheres or brute force? I'm going to think on how to brute force first, then I'll see about nested classes if that doesn't work.

I worked on getting the ui to just show up for most of the time today. I finally have it showing up again. The sliders only work for the first timeandspacesphere in the scene, and only for speed and reverse. I can delete that one, but cannot spawn a new one. I need to understand how to use these in a modular way or it will never work.

### Day 054
Let's just have a nice day where I work on a slider communicating with a time sphere.

I'm at least where I started on Tuesday. I've got a UI that doesn't affect size, and only affects the one time sphere. I'll have a meeting with Jesse tomorrow and hope he can help.

### Day 055
I have an idea. I will make a scriptable object with the values from the time menu sliders. Then use the spheres to grab data from the scriptable object when neccessary. This seriously seems like the right thing to do, but my sliders don't change anything now. I don't know how to explain it. If I understood my coroutines better, maybe I could figure it out. I'll look up more on how scriptable objects interact later.

I finally fucking did it!! I use the scriptable object to set the values of any timeSphere in the scene. When I enter a spaceSphere trigger to select it, I find the parent of the spaceSphere and the child at index 0 of the timeAndSpace object to set a bool to true. That means I'm touching this spaceSphere and its sibling is this timeSphere, and the sliders should update this timeSphere only and it freaking works! I only tested 2 timeAndSpace objects so far in the scene, but they worked great! Now, for some reason one of them is triggering audio to play twice, I think is has to do with a triggerexit issue, but I'll deal with that later. Now I can make my first etude! Woohoo!

### Day 056
I'm back at it today. I've got two major changes to make. First, procedural terrain height changes based on perlin noise. Second, a way to connect audio to effects. I also need to add new samples to play with, but they are on my laptop, so I'll have to do that at home, I guess.

Holy shit, these vids are awesome, I can already make noise and color maps, and edit their attributes in the inspector. I'm going to watch all of the vids and then decide how to edit things for myself.

### Day 057
Continuing these vids. I'll only have time for 1 or 2 today.

Super short day, didn't finish ep 5, but made it to 11:30 in the vid.
Gotta blast!

### Day 058
It took me a week, but I'm back. It is go time. Finishing up the noise terrain vids this week. Hopefully it is a full feature by Friday.

Got some great features working. Plugging away at the tutorials. Be back tomorrow for more.

### Day 059
I'm back again today. Time to get to work.

I have height, it looks good, but I think I have to recreate the mesh whenever I move the noise across the floor. I'll do some more tests where I actually move terrain while making music. Another probable issue is finding how high to make the world. It may be best to start with a flat map at 0y then change depths of map mid game. Other than those two immediate issues, this is an excellent addition to the sequencer and will be used to write scores. For writing VR scores, I think adding terrain animations and drop-off points for dice and spheres will work quite well. I'm going to watch some more tutorials in case I miss something good, but for the most part, this feature is working well.

I got the mesh updating/changing everytime I change a map param. There is the possibility of objects falling through the mesh as it is changing. I should find a way to optimize that.

### Day 060
I'm going to run a deep profiler on the game and see where the lag is coming from. I have a hunch it is the constant collider replacement.

I've fiddled around a bit, and decided to try to update mesh and colliders only when their values change. Fortunately, Sebastian Lague has a video on this in the same procedural tutorial playlist. I'll watch E15 tomorrow and get working on it.

### Day 061
Here to do the day. Just going to go through the scriptable object video and see how it helps my project.

So it is now a scriptable object that updates in editor and during play, but I can't animate the values changing over time. I have to find a way to do that. It is possible to have the object read from a JSON file, but I don't know that it will animate it. Maybe I can code some lerps in a data script and have mapgenerator.cs call them and pass them to the scriptable object?

### Day 062
Let's try to do some scripts with lerps. Random ranges and the like.

So everytime I press the button, the land generates. But it isn't actually lerping like I want. It just sort of jumps to the next value. I want slow gradual change. Maybe multiplying Time.deltatime by a very small number is the way to go...That did nothing. I need the map to update at every point of interpolation and can't figure out how yet. 

### Day 063
Experiment with lerping.

I got something working. While a keyboard button is held down, it will lerp. When the button is let up, the currentvalues are set to the new values. Anyway that works. I tried to apply it to the Vive Grip, but it doesn't lerp, so I've got to crack that one.

### Day 064
I need to fix what values equal what in the lerp function. Then I need to get the lerp to happen with VR control button down.

I've got it working quite well now. The seed was being randomized, which I changed, and now there is an even flow between terrain changes. I also added a state that says the terrain is looping to prevent starting a new terrain change during a current one. I also made it work on a button press and a randomized lerp time. It should be much easier to call from a VR controller at this point.

### Day 065
Map terrain lerp to VR controller. Make sure objects don't fall through. If they do, maybe set up a state that says when terrain is lerping, dice are unaffected by gravity or something. Just spitballing.

I've got plenty to fix before this is a good playing experience, but it is at least semi-functional now. Mostly, I need to eliminate the first timeandspace object from being in the game and fix me not having any colliders so that I don't just float 10 feet in the air or sink below the flowing terrain.

### Day 066
I need to film some old stuff today and edit it tomorrow. Then I need to work on getting the player char to exist and follow gravity and stuff.

I put of presenting so my documentation will have to wait. I got the player char colliding, will look into getting it to not fall through the floor. Terrain works beautifly, I added new sounds from musique concrete samples and they sound amazing! Everything is basically functioning, with some small tweaks needed, but I'll get to them when I get to them. Next I'll start adding effects and makinge copies of scenes with different FX chains to make different pieces/movements.

### Day 067
Going to work a little more on getting the player to rise with the terrain. 

I got it working! No rigidbody required! just using raycast to set my position equal to the position of the floor the raycast hits when traveling away from me downward. Another Sebastian Lague video victory!

### Day 068
Going to put GenerateObjects.cs onto VR Origin so I can use my transform position to spawn dice in front of me. Then I'll look around at what else needs some tweaking.

I need to get reverse trigger working, I need to still fix the terrain from changing height immediately. I managed to make it crash today, that was cool. Probably not a great thing, but hopefully I can learn why it happened and fix it.

### Day 069 (Nice)
A few things I need to do: set up a scene manager to load/unload scenes as movements to my piece, set up a bunch of mixers with various sfx w/ parameters exposed to have map generator and dice movement control sfx, save several scenes with different sfx chains hooked up, make the map slightly smaller, refine randomized map data, make timeandspace delete-able. Not all of this has to be done today, but I need to keep these goals in mind for my performance in six weeks.

I've got the audio mixer groups set up for one movement, just an echo and chorus, and the params are exposed but I haven't scripted with them yet. I'm still thinking about the best way to go about it since it is tied to the map and dice data. But it sounds decent so far. I'm going to have trouble playing one movement full of nothing but echo sounds, so how can I choose to have echo during one section and bypass at another?

What if the audio effects were set by the time spheres and layers? Data comes from the same place, but spheres determine which mixer to send audio to.

### Day 070
I'm going to start today with putting the raycast in fixedUpdate or the mapGen in fixedUpdate just to see what happens. Neither have rigidbodies but maybe something good will make me not fall through the floor. The other option is to double check my code and find out why elevation changes so quickly when I move the terrain.

IDEA! To finish a piece, I turn off the floor's collider and let everything fall through, destroying the dice and stopping the music.

Got really lost in the weeds today trying to fix the collision issues I'm having with the dice and the floor. Worked on raycasting, but it is so complex that it may not be worth it. Cross product of normals may work, but I don't understand how yet, so that's cool.

### Day 071
I'm going to make the ray origin the mesh normals instead of the transform.origin to see if the rays will cast from the vertices.

I am genuinely amazed that I got this working. I send a ray from the center of the di to the normal vector for each face. When the ray touches items with the mask "Floor" is sets the faceVal variable and selects audio based on that. It took way too long to figure this out, but I am super happy about it! I need to work on the user falling through the floor still, and will get to that tomorrow.

### Day 072
Falling through the floor needs fixing. I think if I improve my raycasting it will work better.

I solved this specific issue by making 2 rays that shoot down toward the floor. The first ray is 2 units above the floor, and the second ray is 100 units above the floor. Now when the map goes over my head, the higher up ray will hit it and move me up. The only problem is one single frame of seeing the map above me. Most people may not notice, but I can tell it is happening. Not sure that I want to fix it yet. I think I should work on the other things I need to perform first.

### Day 073
Going to put VR Origin raycasting in LateUpdate() and see if it works any better. Then, I'm going to figure out how to connect height data to audio effects.

### Day 074
Ignoring whether or not I can fix a single frame and moving on. Today is about connecting audio effects parameters to height data. I'll make three scenes each with different effects mappings.

Well, I got things connected in Update(), but I'm convinced there is a better way to do it, similar to the updatable data script Sebastien Lague wrote. Anyway, it works, the audio effects are mapped, albeit, arbitrarily, but they are mapped and it works.

### Day 075
I really need more audio clips. That is something I'll plan on working on this evening. Until then, I'm going to work on getting audio mixers together with sfx I like for my 3 movements.

### Day 076
Going to map mvt 2 and 3 today. Still need to add a ton of new audio, new skyboxes for each level, and I think changing the colors of the ground for each level is a good idea too.

Reverb pitch shift and mod filter are acting so horribly slow. I don't know if I can keep them. When I use edit in playmode, everything seems to run fine, but using noise data to lerp their values drops every frame in the game. It basically won't run. I should find a new set of effects probably.

### Day 077
I got the snapshots to transition in a coroutine instead of how I was previously working. Things are so much smoother now, and easier to implement. I'll go back and redo the first movement with the new script, and then start on the third movement.

### Day 078
Going to set up first and third movements to use coroutine snapshot lerping. Then I'll add some lightboxes and change the stage colors to each scene. I need to record what I do today to use for editing together to make music.

All three scenes are working well. I need to remove the delete time sphere button since the functionality isn't there.

Time sphere button is hidden, skyboxes and floor color schemes are changed. Biggest change next is getting a second camera to record what I do in VR. I need to play around and record what I make today. Tomorrow, I can edit everything together. Also, I still need to add so many new samples.

### Day 079
Learn to record 2 cameras in Unity. Fixed 3rd person camera angle. Add new audio. Record performing.

Fixed 3rd person cameras. Time to learn to record.

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

### Jesse Meeting 007
Ask about static/non-static method calling. Ask about returning face from collision. 
Find out how to get rid of shape objects, or determine lifespan of objects. Look at rotation for an object to determine face collision. That way you can always determine the Up position and music playback won't rely exclusively on a resting face position. Clean-up and consolidate the Procedural/Data files for the shapes, and the generating methods. Try to make iterations in names of generated shapes. Try to fix lit shaders. Goal is to have shapes that play audio based on what side they are laying on.

### Jesse Meeting 008
We talked about quaternions and rotation matrices. Eventually found something involving checking normals of an object against normals of the collision to determine which face is colliding. This sounds simplest so far and will pursue it next week.

### Jesse Meeting 009
I've got audio based on face collisions! How can I make it more modular? He showed me how to make things modular using for loops, arrays, and more variables. D4 script is looking *and* sounding good. 

### Jesse Meeting 010
Ask about garbage collection, collision reactivation. He was definitely able to solve some problems. There is not really a garbage collection problem, so much as a state problem. Having the scene limit when to play audio before/after/during a collision makes for smoother gameplay. I need to update all scripts to allow these state changes. Then I will make some music. Have objects in the scene control parameters of the Dn objects or have Dn params control other Dn objects. Just explore the interactions knowing that this scene uses the floor as a playhead/plectrum of sorts and define what that means for how the piece plays.

### Jesse Meeting 011
What the hell am I doing? How would you go about writing music for this scene? Looped playback seems tedious and uninteresting. Single sample playback sounds like too much work for the performer. Should I focus on rhythmic themes, harmonic ideas, drones, or what? 

Triggers, different sizes, speeds that cause playback to specific D*n*'s. Use terrains to lead objects to beat areas. Chaotic oscillating planes? Save scenes as recordings to pull up later for performing/composing/editing.

Make a registry that holds every object in the scene, and devise a way to tell any object based on any parameter to play.

Start with the trigger and have fun!

### Jesse Meeting 012
I think it is time to show Jesse what I've got and discuss the direction I should take for the next week. Likely, I should start with connecting to mixers in the scene for some more interesting music, then move on to terrain.

Spawn in hand. Script on camera that makes inspector. Making choices for audio files automatic. You want to drag and drop the audio in and have the code do the rest. Consolidate info about whole system set with an inspector for various values, like base names for audio, default sizes/volume/layer triggers/etc. 

### Jesse Meeting 013
This Null reference exception for the audioclip class I built is bothering me. Does he have insight to solving the problem? Should I not try to use jagged arrays? How can I make the D*n* scripts more modular? Do I need to?

Make a texture showing time on the floor. Work on scaling objects, spawning in hand, spawning w/o keyboard, using a folder of audio clips to populate arrays.

### Jesse Meeting 014
Show off folder reading, new level shape/material, and menu. Discuss what comes next, and some job stuff (moving, freelance, non-academic jobs, pax vs. saving for potential interviews, real life shit).

Keep track of how many wav files in each list.

### Jesse Meeting 015
Ask about cases. Show off certain death. Talk about saving and today's demo.
Use dictionaries to find locations of items, what color zones they are in, and manipulate based on location.

### Jesse Meeting 016
Terrain plan, figuring out how to hook up audio effects. NIME paper submission. How can I command multiple timespheres at once? Some sort of custom conducting mode for each piece?

### Jesse Meeting 017
Going to show of the noise maps and demo the possibilities, but also show what isn't quite working and how I'll be fixing it (hopefully). More importantly, talking about working at Meow Wolf. How do I get a job? What makes me competitive/stand out?

Showed it off, talked a lot about where the rest of the project is heading. I love the idea of using probability of playback by timespheres as a variable based on how many dice exist. It adds an arranged element to help with the repetitive nature of my work. Definitely look into that. Another option is to select areas of the floor in some way to only play back or not play back to give the composer some more control over arrangement, but the probability one speaks more to the indeterminate aspects of what I'm doing.

### Jesse Meetin 018
Get functional, get demonstrable, get power point, get writing