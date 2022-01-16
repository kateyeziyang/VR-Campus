# 60's and 70's Quad Project
This README is intended for those developing this project.

This version of the project is not VR supported due to COVID-19 but can be changed easily.

Note: In this document and in the code we use both the acronyms NUC (non-user character) and NPC (non-playable character). They are the same thing.

## State of the Project When Given to Us
Refer to: https://cs498vr.cs.illinois.edu/projects/FA19-60s_Main_Quad.html for a complete experience of the project from the last group.

The project had basic mechanics working such as the dialogue system, being able to move around the quad, changing personas, seeing a description of the building, being able to tell which persona you are, and being able to see a mini-map of the quad in your hand. However, the majority of the text that appeared on the screen whether it was from the dialogue, persona's basic information, or the description of the buildings had placeholder text. The quad itself was there with the buildings, trees, sidewalk, and grass but visually, it was jarring and unappealing to the eyes. A lot of the buildings' textures were just images of the building including the sky and tree, while the rest of the textures used looked like they were stretched out or discolored. Also, from a first person point of view, it seemed like height wise the user and NPC's were too small in proportion to the buildings. Overall, it wasn't really immersive. 

## Work That Was Completed
In general, we improved upon the existing project. We didn't add a lot of new features. The project still needs to be improved upon, and expanded with more content. 

* Change the controls from VR to keyboard
	* Movement
	* Talking to NUC's/NPC's
	* Changing personas
* Fixed various textures
	* Tree trunk texture
	* Sidewalk
	* Grass
		* Grass texture also has a custom shader that was made to prevent the tiling effect
	* Outer wall
	* Some of the building textures on the quad
		* Most of the building textures were replaced with a more generic brick texture
			* This was done to place 3D windows, doors, etc. on top instead of having it be in the texture itself
				* See Smith Memorial Hall as an example building that has the windows textured in
			* Some modeling was created to add 3D model windows to the buildings instead of having it be on the textures
		* Some of the trees on the Henry Administration Building texture were photoshopped out, but not all of them
* Fill in various placeholders
	* For the various personas the user can be
	* Dialogue text
	* Building descriptions
* Added normal maps to the buildings
* Prevent the player from going through things
	* Buildings
	* NUC's/NPC's
	* Various objects (trees, lamps, posters, etc.) located on the quad
* Adding music/sound
* Created a new room for the user to enter into to change their persona
	* This also removed the need for the scene called "Main hall" for the user to change their persona in
* Reorganize the file system a bit
* General effort to make the quad look more lively and historical
	* Added a skybox
	* Added flying birds in the sky
	* Added a rug
	* Added a van
	* Added various 60's/70's posters
	* Added lamp posts
	* Fixed the orientation of the union
	* Replaced the duplicated Henry Administration Building with Smith Memorial Hall
* Improved the NUC's/NPC's and player character
	* Updated all of the models used for the NPC's and player
		* The police officer was found online
		* The others were created with Blender
	* Added animations to all of the models used
		* Have the animations change according to the dialogue for NPC's
		* Player has a walking animation
	* Improving the dialogue
		* Allowed for randomization of dialogue
		* Made dialogue more natural/smooth/coherent between the user and NPC
		* Dialogue does change/reset when the user changes their persona to a new one (it doesn't reset if the user keeps the same persona)
		* Some of the dialogue uses common slang words/phrases from the 60's/70's
	* Changed activation of dialogue
		* Removed the pressure plate
		* User within a certain range of the NPC can talk to them instead
* Allow the user to teleport 
	* User can teleport in and out of the persona changing room
		* The room contains objects that reference the time period
	* User can teleport to various locations on the quad:
		* Union
		* Foellinger
		* English Building
* Imported TextMash for TextMash objects
* General bug fixes


## Explanation of the Coded Features

### NUC's/NPC's
Refer to NPC_Police as a template for adding more NPCs.  A Trigger collider, conversation script, canvas, character model and animator are needed. The conversation scripts are: maleAConv.cs, femaleConv.cs, and officerConv.cs. The officerConv.cs file features how to add branching in the dialogue based on the user's persona. Otherwise maleAConv.cs and femaleConv.cs do not show how the dialogue changes based on the user's persona. Any of the conversation scripts will show up to set up and randomize the dialogue in the SetUp() function. If you add more NPC's, then you will have to update the "num_npc" variable in Persona.cs, otherwise conversations will not reset/update when the user changes their persona! For the user's response options, they are always in the order of good (1), neutral (2), bad (3). 

### Persona Changing System
To change the user's persona, refer to CharacterSelection.cs and Persona.cs files. In particular, CharacterSelection.cs is used for the persona changing room. Currently, we have the user change their persona in a seperate room located by the Union. There is a seperate scene (called "Main hall") that allows the user to do this, but this was from the previous group and still uses VR controls. The various personas the user can be are "Lucas" (young straight black man), "Susan" (old bisexual white woman), and "Greg" (young gay white man). They are refered to in the code with the numbers 0, 1, and 2 respecitvely. 

### Character animations
Animations for characters were taken from Mixamo. All follow the same animation controllers. Animations of the NPC's depends on their happiness/feelings towards the user.

### Other Notes
Note that the collider scripts Colle, Colli, etc are essentially identical scripts, these are left over from the previous group's implementation. Colle refers to the male NPC, Colli refers to the female NPC, and OfficerCollider refers to the police officer NPC.

## Groundwork for the Future
We fully implemented the police officer with a new model, set of animations, and branching dialogue path. Future groups can use that as a reference in creating new NPC's, or chose to expand the dialogue further for the officer. Plus, the characters on the quad currently use the same animations so unless future groups chose to create their own animations, there is no need to. So, future groups should have an easy time expanding the project by adding more characters and dialogue. 

In addition, since we commented out the VR controls in the code and implemented in keyboard controls, it should be fairly simple for the next group to revert everything back to VR controls.

## Plans We Had for the Project That We Didn't Get To
* Fixing the textures on buildings
    * This could mean modeling the windows, doors, decoration, etc. from the images on the outside of the buildings to the building itself
    	* Prefered way, but a bit more time consuming than the second option
    * Or editing out the trees from the textures on buildings, but the textures may still look bad on the buildings anyway if you do this. Refer to the Henry Administration building for to see one side with the trees edited out of the textures, and the other side with the trees still in the textures
* Adding more NPC's
	* Note: If you add more NPC's, then you will have to update the "num_npc" variable in Persona.cs, otherwise conversations will not update when the user changes their persona
* Expanding upon the dialogue system
	* More branching in conversations
	* Longer conversations
	* More depth to them 
	* Update dialogue according to scripts (if given by the Sponsor)
* Adding more personas that the user can be
	* Pre-made personas where the persona's ethncity, sexual orientation, and gender is already chosen
	* Custom-made personas where the user can choose the ethncity, sexual orientation, and gender
	* Make sure to indicate the different personas with the person/hand model representing the user
* Adding more variables to the personas
	* Such as age, socioeconomic status, political alignment, etc. 
	* With the addition of more variables, the more complex the conversation branches need to be with the NPCs
	* And you need to display that info on the screen when the user is changing personas
	* Note: Age is shown while changing personas, but a variable for it isn't actually implemented in Persona.cs file
* Making the player's model correspond to their persona
	* When the project was in VR, the hands did correspond a color that matched their persona (ex. green for Lucas, red for Greg)
	* We were planning on instead having the colors match the persona's skin tone (more or less) to make more sense, and help immerse the user
* Continue making the quad seem more historical
* Making the user interface more appropiate for VR
* Fixed current bugs
	* Can still continue the convo with an NPC without selecting an response by pressing space
	* Sexuality info is missing for the various personas on the screen where the user can change their persona
	* The color of the branches on the leaves in the trees are rainbow colored
* Redoing the file structure more
* Cleaning up code

## Discussions With Sponsor Regarding Where the Project Should Go Next
We never met with the sponsor at all during the whole semester, so we're not sure what the sponsor wants exactly. From what we've been told, in previous semesters the sponsor wasn't very active and seemed to be okay with whatever. It would be good if future groups could get a script of what the sponsor wants the dialogue to actually be, and which people the sponsor wants to see on the quad.  

