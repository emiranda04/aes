using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cipher {

    class Utility {

        public static string directory = @"c:\code\cypher_files";

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
