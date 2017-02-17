using System;
using System.IO;
using System.Linq;


namespace Cipher {

    class Program {

        private static string encryptionIndicator = "_encrypted";
        private static int menuOptionCount = Message.MenuOptions().Count;

        static void Main(string[] args) {
            // Uncomment 'SaveArraySampleToDisk' method to generate your own salt array.
            // Cipher.SaveArraySampleToDisk();

            // Comment the Start method before generating your own salt array.
            Start();
        }

        private static Boolean IsNumeric(string input) {
            int value;
            bool isNumeric = false;

            if (int.TryParse(input, out value)) {
                isNumeric = true;
            }

            return isNumeric;
        }

        private static void Start() {
            // Display main menu.
            Message.MainMenu();

            // Get user selection.
            int userSelection = 0;

            do {
                Console.Write(" Please make a selection: ");

                // Get user selection and run menu.
                userSelection = Convert.ToInt32(Console.ReadLine());

                switch (userSelection) {
                    case 1:
                        EncryptDocument();
                        Message.MainMenu(); ;
                        break;
                    case 2:
                        DecryptDocumemt();
                        Message.MainMenu(); ;
                        break;
                    case 3:
                        EncryptDirectory();
                        Message.MainMenu(); ;
                        break;
                    case 4:
                        DecryptDirectory();
                        Message.MainMenu(); ;
                        break;
                }

            } while (userSelection != 0  && userSelection <= menuOptionCount);
        }

        private static void EncryptDirectory() {
            // Display messages.
            Message.SubMenu(" Directory Encryption");
            Message.Action(" Enter the directory full path: ");

            // Get the directory information.
            string directory = Console.ReadLine();
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            // Get all the files in the directory that do not contain the encryption indicator in their name.
            var files = directoryInfo.GetFiles().Where(x => !x.Name.Contains(encryptionIndicator));

            // Set password.
            Message.DisplayPasswordChoice();
            string diplayPassword = Console.ReadLine();
            Message.EnterPassword(action: "encryption");
            var password = Utility.GetPassword(diplayPassword);

            // Encrypting file.
            foreach (FileInfo file in files) {
                var plainTextFileName = directory + "\\" + file.Name;
                var encryptedFileName = Path.GetFileNameWithoutExtension(file.Name) + encryptionIndicator + Path.GetExtension(file.Name);
                EncryptFile(plainTextFileName, encryptedFileName, password);
            }

            Message.PressKeyToContinue(" Directory files encrypted.");
        }

        private static void DecryptDirectory() {
            // Display messages.
            Message.SubMenu(" Directory Decryption");
            Message.Action(" Enter the directory full path: ");

            // Get the directory information.
            string directory = Console.ReadLine();
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            // Get all the files in the directory that do not contain the encryption indicator in their name.
            var files = directoryInfo.GetFiles().Where(x => x.Name.Contains(encryptionIndicator));

            // Set password.
            Message.DisplayPasswordChoice();
            string diplayPassword = Console.ReadLine();
            Message.EnterPassword(action: "decryption");
            var password = Utility.GetPassword(diplayPassword);

            // Decrypt file.
            foreach (FileInfo file in files) {
                var encryptedFile = directory + "\\" + file.Name;
                var plainTextFile = directory + "\\" + Path.GetFileName(file.Name).Replace(encryptionIndicator, "");
                DecryptFile(encryptedFile, plainTextFile, password);
            }

            Message.PressKeyToContinue(" Directory files decrypted.");
        }

        private static void EncryptDocument() {

            // Display message.
            Message.SubMenu(" File Encryption");
            Message.Action(" Enter the document name with its extension: ");

            // Get plaintext file and read it into a byte array.
            string plaintextFileName = Console.ReadLine();
            string plaintextFile = Path.Combine(Utility.directory, plaintextFileName);
            byte[] plaintextByteArray = File.ReadAllBytes(plaintextFile);

            // Check if source file should be deleted after encryption.
            string deleteFile = DeleteFileChoice(fileName: plaintextFileName, action: "encryption");
            Console.WriteLine("");

            // Display password choice.
            Message.DisplayPasswordChoice();
            string diplayPassword = Console.ReadLine();
            Message.EnterPassword(action: "encrypting");

            // Get password.
            var password = Utility.GetPassword(diplayPassword);
 
            // Encrypt file and store it into a byte array.
            Message.ProcessingCipher(" Encrypting document");
            byte[] encryptedBytes = Cipher.Encrypt(plaintextByteArray, password);

            // Save encrypted document to disk.
            var encryptedFileName = Path.GetFileNameWithoutExtension(plaintextFileName) + encryptionIndicator + Path.GetExtension(plaintextFileName);
            var encryptedFile = Path.Combine(Utility.directory, encryptedFileName);
            Utility.SaveFileToDisk(fileName: encryptedFile, fileContents: encryptedBytes);

            // Check if source file needs to be deleted.
            if (deleteFile == "y") {
                Utility.DeleteFile(plaintextFile);
            }

            Message.PressKeyToContinue(" File encrypted.");
        }

        private static void DecryptDocumemt() {

            // Display message.
            Message.SubMenu(" File Decryption");
            Message.Action(" Enter the document name with its extension: ");

            // Get encrypted file.
            string encryptedFileName = Console.ReadLine();
            string encryptedFile = Path.Combine(Utility.directory, encryptedFileName);
            var decryptedFileName = encryptedFileName.Replace(encryptionIndicator, "");
            var decryptedFile = Path.Combine(Utility.directory, decryptedFileName);

            // If file exists, cancel decryption.

            if (File.Exists(decryptedFile)) {
                Message.FileAlreadyExists();
                Message.MainMenu();
                return;
            }

            // Read encrypted file array.
            byte[] bytesToBeDecrypted = File.ReadAllBytes(encryptedFile);

            // Check if source file should be deleted after decryption.
            string deleteFile = DeleteFileChoice(fileName: encryptedFileName, action: "decryption");
            Console.WriteLine("");

            // Display password choice.
            Message.DisplayPasswordChoice();
            string diplayPassword = Console.ReadLine();
            Message.EnterPassword(action: "decrypting");

            // Get password.
            var password = Utility.GetPassword(diplayPassword);

            // Decrypt file and store it into a byte array.
            Message.ProcessingCipher(" Decrypting document");
            byte[] decryptedBytes = Cipher.Decrypt(bytesToBeDecrypted, password);

            // Save decrypted document to disk.
            Utility.SaveFileToDisk(fileName: decryptedFile, fileContents: decryptedBytes);

            // Check if source file needs to be deleted.
            if (deleteFile == "y") {
                Utility.DeleteFile(encryptedFile);
            }

            Message.PressKeyToContinue(" File decrypted.");
        }

        private static void EncryptFile(string plainTextFile, string encryptedFileName, string password) {
            // Read encrypted file array and rncrypt it.
            byte[] plaintextByteArray = File.ReadAllBytes(plainTextFile);
            byte[] encryptedBytes = Cipher.Encrypt(plaintextByteArray, password);
            var encryptedFile = Path.Combine(Utility.directory, encryptedFileName);

            // Save data to disk.
            Utility.SaveFileToDisk(fileName: encryptedFile, fileContents: encryptedBytes);
        }

        private static void DecryptFile(string encryptedFile, string plainTextFile, string password) {
            // Read encrypted file array and decrypt it.
            byte[] encryptedBytes = File.ReadAllBytes(encryptedFile);
            byte[] decryptedBytes = Cipher.Decrypt(encryptedBytes, password);

            var decryptedFileName = encryptedFile.Replace(encryptionIndicator, "");
            var decryptedFile = Path.Combine(Utility.directory, decryptedFileName);

            // Save data to disk.
            Utility.SaveFileToDisk(fileName: decryptedFile, fileContents: decryptedBytes);
        }

        private static string DeleteFileChoice(string fileName, string action) {

            Console.Write(" Delete '{0}' after {1} (y/n)? ", fileName, action);

            string deleteFile = Console.ReadLine();

            // Only a 'yes' response will delete the file.
            if (deleteFile.ToLower() == "y") {
                Console.Write(" File will be deleted!");
            } else {
                Console.Write(" File will not be deleted.");
            }

            return deleteFile;

        }
    }
}
