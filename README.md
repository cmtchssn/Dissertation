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

---

### Jesse Meeting 001
Two goals: "Hello, World!" the Native Audio Plugin SDK and build more cubes to learn about interactions with Unity's Audio Scripting API. How does audio pass to another cube? Eventually, you'll want to get modular with your object building, so think about preparing your code to be used as a library that will, in three or so lines, be able to make several thousand objects with several thousand audio clips, etc.

Additionally, get your hands on a VR development kit with some controllers and put a character in the scene.

### Jesse Meeting 002
Had a good meeting demoing the plugins for Jesse. Showed off the Mixer and all its abilities, C++ projects, GUIs for plugins, etc. Agrees that I should start with a stable release of Unity and get the open source native audio plugins from Unity working in my own project.