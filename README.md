# Dissertation
Unity VR Audio Dissertation Materials
---
### Day 001
Set up a dissertation repo with .gitignore for Unity and Windows. Installed Unity Hub and the latest version of Unity (2019.1.5f1). Created a Unity project using the 3D with Extras template. Followed instructions [here](https://thoughtbot.com/blog/how-to-git-with-unity) to set up git with Unity, ammend Unity's version control, and use git large file storage. I built a 6-plane room and a cube in the main scene. I still need to access a VR kit, download the SDK, apply that to my Unity project.

### Day 002
Learning about the Unity audio system. I can record a microphone input (`Microphone` class) and the VR environment output (`AudioRenderer` class). This means a user can record into the VR environment, manipulate that audio, then record the manipulated audio output. Sampling and re-sampling seems doable. Sample data is accessible through `AudioClip.GetData` method, filling an array with floats from -1.0f to 1.0f for each sample in the audio clip. I think I can playback a clip from a specific sample using `AudioSource.timeSamples`. I'll have to try the example Unity code to understand more. I'm going to sample a Max 8 birds audio clip, place it in the Unity scene, and learn how to play back from any position.

To play back from a specific sample, set `AudioSource.timeSamples` to the sample on which you would like to start before calling `AudioSource.Play();`. To add a delay to the `Play()` method, provide an argument in samples inside the parentheses. I have made a scene that plays an audio file from the beginning when the user presses 'Enter', and pauses/unpauses when the user presses 'Space'.  

### Day 003
Today I think I will learn how to scrub/seek through an audio file in Unity. I am going to try to use the mouse x position as the `AudioSource.timeSamples` integer to see if I can scrub through an audio file. With some mild scaling, during `Update()` I can declare the mouse x position to be the sample from which to play. I hit 'Enter' to initiate playing, and the audio loops on the sample where mouse position x is, and scrubs through the audio file as I move the mouse left and right. Can I make the sample window larger? I don't know how to yet, because `AudioSource.timeSamples` is an int. My thought is to use it as an initial starting point but have the audio play only 512 samples from that point. So that point + 511 samples would be the play length on loop. I still need to work on this.
