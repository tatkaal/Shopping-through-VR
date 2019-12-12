# VR shop with scanned 3D models
A virtual shopping experience in virtual reality, developed with Unity and dell visor using SteamVR.

## Configuration: activate Cortana
The application uses Cortana's voice recognition in conjunction with the built-in microphone on dell visor to search for items. These options are deactivated by default in Windows 10 and must be activated beforehand:

* Windows 10 Settings -> "Privacy (position, camera)":
    * "Voice recognition":
        * Switch on "Online speech recognition"
        * Note: In older versions of Windows 10, the setting was called "Speech recognition, ink and input" -> "Get to know each other" (https://lexa-it.de/wp-content/uploads/2015/08/Datenschutz-Ein Settings. png).
    * "Microphone":
        * Activate "Allow access to the microphone on this device"
        * Turn on "Allow apps to access your microphone"

## Hints
* The folder ** Articles ** must be in the main directory of the executable, NOT elsewhere.
* There are only 26 products. With the search word ** "everything" ** the debug mode is activated, which shows 100+ article monitors for testing in order to test the rotation of the article wall.
* If the speech recognition does not work, you can search for a replacement with the keyboard.
* The import of 3D models takes a few seconds and can slow down the application. Repeated importing uses a cache to speed up the process.