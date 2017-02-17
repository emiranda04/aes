using System;
using System.Collections.Generic;

namespace Cipher {

    class Message {

        /// <summary>
        /// Create menu choices into a dictionary.
        /// </summary>
        /// <returns>
        /// Dictionary containing the menu choices.
        /// </returns>
        public static Dictionary<int, string> MenuOptions() {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(1, "Encrypt File");
            dictionary.Add(2, "Decrypt File");
            dictionary.Add(3, "Encrypt Directory Files");
            dictionary.Add(4, "Decrypt Directory Files");

            return dictionary;
        }

        /// <summary>
        /// Use the menu choice dictionary to format the main menu option
        /// </summary>
        public static void MainMenu() {

            var d = MenuOptions();

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(" =======================================");
            Console.WriteLine(" Encryption/Decryption Menu");
            Console.WriteLine(" =======================================");

            foreach (KeyValuePair<int, string> pair in d) {
                Console.WriteLine(" {0}. {1}", pair.Key, pair.Value);
            }

            Console.WriteLine("");
            Console.WriteLine(" 0. Exit App");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        public static void SubMenu(string subMenuName) {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(" =======================================");
            Console.WriteLine(subMenuName);
            Console.WriteLine(" =======================================");
        }

        public static void Action(string action) {
            Console.WriteLine("");
            Console.Write(action);
        }

        public static void ProcessingCipher(string action) {
            Console.Write(action + " ...");
        }

        public static void DisplayPasswordChoice() {
            Console.Write(" Do you want to display the password on the screen (y/n)?: ");
        }

        public static void EnterPassword(string action) {
            Console.Write(" Enter the " + action + " password: ");
        }

        public static void PressKeyToContinue(string message) {
            Console.WriteLine("");
            Console.WriteLine(message);
            Console.Write(" Press the Enter key to continue");
            Console.ReadLine();
        }

        public static void FileAlreadyExists() {
            Console.WriteLine(" This file exists in the directory and it will not be processed.");
            Console.WriteLine(" Change the name of the file or move it to a different directory.");
            Console.Write(" Press the Enter key to continue");
            Console.ReadLine();
        }
    }
}
