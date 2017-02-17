# AES Utility #


## Purpose ##

Allows to encrypt/decrypt individual files or directory contents.

## Platform ##

The first commit of this app was written for Windows but I decided to port it to Mac since its termnal is very friendly. Also, I get the benefit of coding both in Swift and C# using the same computer without having to switch machines. 

## How to use ##

1. Create a C# console application and add the files to it.

2. Change the directory path.

3. Run the 'SaveArraySampleToDisk' method to create a salt. **Do not** use the salt in the code sample. 

## Improvements ##

- Add exception handling to methods as required.
- Add option to delete plaintext files after they have been encrypted.
- Add option to move encrypted files to a different directory from the plaintext files.
