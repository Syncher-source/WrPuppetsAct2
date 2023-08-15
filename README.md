# WrPuppets Act 2

## A [KISS](https://en.wikipedia.org/wiki/KISS_principle) project for full body tracked VR communities
Features a dumb C unix server 99.9% written by ChatGPT and an Unreal Engine windows client, supports [vrm avatars](https://vrm-consortium.org/en/), choose them at [VRoid Hub](https://hub.vroid.com/en) , create them from scratch or convert them from [VRChat/Unity humanoid](https://github.com/esperecyan/VRMConverterForVRChat)

Teaser (click to play):

[![Teaser](https://github.com/Syncher-source/WrPuppets/blob/main/README_images/Trailer.png)](https://www.youtube.com/watch?v=cemIqeZxMQc)


## Requirements
The client runs on windows 10/11 supporting any OpenXR/SteamVR hardware, the server is a udp echoing C program and can run on any unix systems.
A demo default server is set in the config of the release and currently up and running at: wrpuppets.duckdns.org on a raspeberry pi.

## Set-up OpenXR hadrware
This project was tested with Valve Index controllers, an HTC Vive Pro, HTC Vive, Quest, HTC wands, Quest controllers and 3 vive trackers. Setup the openxr under SteamVR Settings > Developer

![openxr](https://github.com/Syncher-source/WrPuppets/blob/main/README_images/openxr.jpeg)

and setup roles for trackers under SteamVR Settings > Controllers > MANAGE VIVE TRACKERS

![trackers](https://github.com/Syncher-source/WrPuppets/blob/main/README_images/trackers.png)

## Download an avatar and run WrPuppets 
Click on the following image to see a video showing how to download and configure an avatar in wrpuppets, you should set a random unique number between 1 and 1023 as an id (click to play):

[![firstrun](https://github.com/Syncher-source/WrPuppets/blob/main/README_images/NotScaled.png)](https://youtu.be/HV2_aYSd7Ks)

## Design assumptions and project aim
The main idea is to allow a private community of people to setup their own server and their own world/stage: a space ship, a medieval castle, a music hall, whatever and to role play the characters i.e. the avatars, the puppets, or just vibe together listening to the music or whatever you want to do with your homies in VR.
The project was made to be very simple but also to actually work supported by the best 3d tech. i.e. the Unreal Engine, there is no cyber security or drm of any kind of and the server can be easily broken by a malicious user.
They aim for the project are private communities of people already knowing and trusting each other and willing to share their VR experiences in a cooperative way so everybody is expected to respect the rules to make it work.
Avatars are shared among the users with a standard file format, VRM, without any copy protection.
It was developed in less than 300 hours, is more a proof of concept than a real project BUT it works, still is full of bugs and need optimizations.


## Adding your own avatars
Add more .vrm files to WrPuppets\Content\Puppets to add more avatars, make sure all the players share the same files in their local directory

Edit the "puppet" field in: WrPuppets\Content\config.json to specify the number of the avatar you want to use

Avatar file should use a number between 1 and 1023 as a file name, example: 27.vrm, on the same server are not allow same ids so each user should choose a different one, rn if you want more users with the same avatar just made a copy of it and change its name.


## Changing the server address
Edit the field "server_name" in  WrPuppets\Content\config.json to specify your own server, you can use an ip to skip domain name resolution deleting "server_name" and adding "server_address" = "192.168.1.1"


## Changing default map
Drop custom_map.pak into WrPuppets\Content\Paks folder to replace the default map


## Export .vrm from Unity VRChat
Depending on how much complex is the VRChat avatar the process can be quick and automatic or long and manual.
You will need to use tools to export it in .vrm like:
https://github.com/vrm-c/UniVRM/releases/tag/v0.99.2
or better:
https://pokemori.booth.pm/items/1025226

It is also necessary to use vroid bones naming convention, to apply that just add this tool to the Asset folder:
[BoneRenamer.cs](https://github.com/Syncher-source/WrPuppetsAct2/blob/main/unityconverter/BoneRenamer.cs)
then select from top menù WrPuppets/RenameBones, assign the avatar root node to both fields and clik "Rename Bones"
Check this video guide for the conversion of a basic avatar: 1 mesh, simple materials, no blend shapes (click to play):

[![Converter](https://github.com/Syncher-source/WrPuppetsAct2/blob/main/README_images/VRChat2WrPuppets.png)](https://www.youtube.com/watch?v=KiH02wgepzg)


Check [esperecyan's project](https://github.com/esperecyan/VRMConverterForVRChat) to support more export features like morph targets aka as blend shapes or manipulate meshes.


## Bulding your own map
You need to download WrPuppets source project and replace Content/Theater/Map/StarterMap using Unreal Engine.
After building and packaging the project a pakchunk1000-Windows.pak file will be created, rename it custom_map.pak and drop it in the WrPuppets\Content\Paks runtime folder to replace the default map.

WrPuppets has no special requirements or binding to the map


## Source code client
To access the source code for the client you need to subscribe to Unreal Engine license: https://www.unrealengine.com/en-US/license then check it out [here](https://github.com/Syncher-source/WrPuppets-Act2-Source)

The VRM support is due to the amazing work of [Ruyo](https://github.com/ruyo/VRM4U) 

UDP support is from [Getnamo](https://github.com/getnamo/UDP-Unreal)

and Lipsync is based on [Meta's library](https://developer.oculus.com/downloads/package/oculus-lipsync-unreal)

## Complete walkthrough of the client
If you wanna dig into it, here is a video describing in it with a good overview (click to play):

[![Walkthrough](https://github.com/Syncher-source/WrPuppetsAct2/blob/main/README_images/Walkthrough.png)](https://youtu.be/mp1W59Tc3lE)


## Source code server
The server source code is [here](https://github.com/Syncher-source/WrPuppetsAct2/blob/main/server/server.c), compiles with a simple gcc command on both arm64 and x64: 
```bash
gcc server.c -o wrpuppets_server 
```
and runs without arguments:
```bash
.\wrpuppets_server 
```
the server receives packets on port 50027 so if you plan to set it up behind a firewall or a router make sure to [route the port](https://setuprouter.com/)

If you are without a public static ip address, you can check [duckdns.org](https://www.duckdns.org/install.jsp) for a free dns


## Contacts
Discord: \_VentiSette\_\_


## Thanks
Thanks to Mahne to inspire me with her passion and love for VR, it made me start all of this

Thanks to Paull78 and to Gihthetree to help debug this project, supporting me and my madness

Thanks to all the VR dance and music community! I love you all!


## Weird notes
The whole project was developed using a wireless keyboard/mousepad i.e. without a mouse for no particular reason

