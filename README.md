# Unreal Engine 4 Save Game Converter (SAV)
This library and simple console tool for it will convert the generic UE4 save game file into a JSON for easier analysis. The file extension is usually SAV and the file should begin with the string GVAS.

Back convertion is theoretically possible, but is not implemented.

Due to limitations of how UE4 serializes the data, some data types might be missing, and might fail deserialization for some games.

For example, a lot of less-frequently used primitive types (non-4 byte ints, double, etc).

**Usage:**

  `GvasConverter.exe [savefile.sav]`
  
**How It Works**

The converter will parse the binary SAV file and, if successful, will generate a JSON file without altering the original SAV file.

The generated JSON is only a representation of the data contained in the SAV file. The JSON structure and attribute names are intended only as placeholders for the data values and do not reflect any structure underlying the SAV or the game itself.

This JSON file can be loaded into a text editor so that one may analyze the structures and values in a readable form. Hex addresses are included in the JSON output to facilitate modification of the original SAV file.

**FAQ**

**Q: How do I know if the save file will work?**  
A: While there's no harm in trying incompatible files, compatible files should begin with the string GVAS. This can be seen by opening the SAV file in a text editor.



**Q: I'm receiving errors when I run the converter, is there anything I can do?**  
A: If the errors are not related to locating your SAV file then the file is either too old, too new or contains unsupported structures. If this is the case than no, there is nothing you can do aside from fork this repository and update the converter.

**Q: How do I change a value in the original SAV file?**  
A: You can use a hex editor, such as [HxD](https://mh-nexus.de/en/hxd/) to change values in the SAV file.  

&nbsp;&nbsp;1. Backup your original SAV file.  
&nbsp;&nbsp;2. Locate the value wish to change using the JSON file.  
&nbsp;&nbsp;3. Copy the hex address of the value, not including 0x.  
&nbsp;&nbsp;&nbsp;&nbsp;i. For Example: "Address": "0x0000c6e0" copy "000c6e0"  
&nbsp;&nbsp;4. Open the SAV file in a hex editor.  
&nbsp;&nbsp;5. Use the go to command (Ctrl+G) paste the hex address.  
&nbsp;&nbsp;6. Locate the current value at that location.  
&nbsp;&nbsp;7. Update the value.  
&nbsp;&nbsp;8. Save the file.

It's important to note that values saved in binary form are generally not simply decimal (base 10) numbers and must be converted to binary/hex before entering into a binary file. The easiest way to do this is to find another property with the desired value in the SAV file and take note of it's hex/binary representation.

**Q: Will you be updating this converter?**  
A: Most likely not, but others are certainly welcome to.



