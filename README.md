# KSP Logitech RGB Control

**KSP Community Forum Thread Yet - to add**

**[Old Forum Thread by Kaeltis](http://forum.kerbalspaceprogram.com/index.php?/topic/137859-11-ksp-logitech-rgb-control-v101-2016-22-04/)**

I will try update and maintain this for future versions of KSP, currently at 1.9. This was left off at version 1.5 by @battlemoose and I will attempt to update it from now on.

Allows Kerbal Space Program to modify the lighting on your Logitech RGB Keyboard.
Lights up your keyboard to make playing Kerbal Space Program a lot easier.

**This is a fork of @battlemoose's [ksp-logirgb](https://github.com/battlemoose/ksp-logirgb/) which is a fork of @Kaeltis' [ksp-logirgb plugin](https://github.com/Kaeltis/ksp-logirgb) which in turn is a fork of @cguckes' [ksp-chroma plugin](https://github.com/cguckes/ksp-chroma). Thanks to both for their significant work.**

Thanks to @RandyTheDev for major contributions and new features, including multiple keyboard layout support. See the [changelog here](https://github.com/battlemoose/ksp-logirgb/pull/1).

The mod is still very beta, so let me know if you experience any difficulties when using it.

*All product and company names are trademarks™ or registered® trademarks of their respective holders. Use of them does not imply any affiliation with or endorsement by them.*

##### MiniAVC

This mod includes version checking using [MiniAVC](http://forum.kerbalspaceprogram.com/threads/79745). If you opt-in, it will use the internet to check whether there is a new version available. Data is only read from the internet and no personal information is sent. For a more comprehensive version checking experience, please download the [KSP-AVC Plugin](http://forum.kerbalspaceprogram.com/threads/79745).

## Features

- Function keys 1 to 0 are only lit, if the underlying action group actually does anything. The keys are displayed in two different colors, depending on whether the action group is toggled or not.
- The keys for SAS, RCS, Gears, Lights and the Brakes are lit up in different colors, indicating if the respective system was activated or not.
- The amount of resources in the current stage is displayed on your keypad and the keys to the left of it (PrtScr, ScrLk, ..., PageDown)
- The color of the movement keys varies slightly depending on whether you're in precision or normal steering mode
- The keys for timewarp control are lit either red for physics timewarp or green for normal timewarp
- Automatic keyboard layout detection will automatically adapt to your operating system's keyboard layout, even if you change it mid-flight!

## Full list of game effects

- Stylized Kerbal Space Program logo on every scene that does not contain any noteworthy keyboard interaction (pressing Escape to go to the menu not being noteworthy enough to light up the key)
- In the vessel editor, different kinds of keysets are lit up according to the current editor mode.
- Control keys and toggleable function keys are lit up in different colors, showing whether the function is switched on or off during flight.
- Reduced keyset lit for EVA mode
- Resource gauges displayed on the keypad and the keys above the UpDownLeftRight keys.

## Installation

1. Unzip the release archive and place the KSPLogiRGB folder in your KSP GameData directory.
2. Start KSP and witness the awesomeness of highlighted function keys while kerbaling through space
 
## Todo

- Make the whole keyboard red, if the vessel is not steerable any more.
- Add CatastrophicFailure event and SplashedDown event animation.
