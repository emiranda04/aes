using System;
using System.IO;

namespace Cipher {

    class Utility {

        public static string directory = "/Users/emiranda/Documents/code/c#/cipher_files/";

        /// <summary>
        /// Allows user to choose to display the password on the screen as is being typed.
        /// </summary>
        /// <param name="displayPassword">
        /// Indicates whether or not password displays on the screen.
        /// </param>
        /// <returns>
        /// A password string.
        /// </returns>
        public static string GetPassword(string displayPassword) {
            string password = "";

            if (displayPassword == "y" || displayPassword == "") {
                password = Console.ReadLine();
            }

            if (displayPassword == "n") {
                while (true) {
                    var pressedKey = System.Console.ReadKey(true);

                    // Get all typed keys until 'Enter' is pressed.
                    if (pressedKey.Key == ConsoleKey.Enter) {
                        Console.WriteLine("");
                        break;
                    }

                    password += pressedKey.KeyChar;
                }
            }


            return password;
        }

        public static void SaveFileToDisk(string fileName, byte[] fileContents) {
            File.WriteAllBytes(fileName, fileContents);
        }
    }
}
