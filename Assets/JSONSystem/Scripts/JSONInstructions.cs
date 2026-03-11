/*Mohey JSON System Instructions

This system allows you to manage multiple JSON files simultaneously using Unity's ScriptableObject.

How to Use

1- Create a new ScriptableObject to store the data you want to save as JSON (Refer to the example in  the ExampleScene/JSONReaders.

2- Define the variables to be serialized and saved as JSON.

3- Update the menuName in the [CreateAssetMenu] attribute to ensure you can create instances via the Unity Editor.

4- Add the JSONReader component to your scene, referencing your ScriptableObject through the Inspector.

5- Configure Action on Awake:
Select whether the JSONReader should Save, Load, or do Nothing on Awake.
Use "Save" to create the JSON file initially, then switch to Load to pull data from the file.

6- Manual Save/Load:
You can manually control saving and loading using the SaveData() and LoadData() methods.

7 - Modify the jsonFilepath in JSONReader if you need to change where JSON files are stored.

Note: The JSON file is named based on the ScriptableObject name, so ensure it’s descriptive.
*/