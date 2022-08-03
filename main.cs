using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;

class MainProgram {
	
	static void Main(string[] args) {
		
		// Variables 
		int countDown = 1;
		string welcomeMessage = "*********************************************\n*                                           *\n*                                           *\n*                 Askel-CMD!                *\n*                                           *\n*                                           *\n*                              By Cryptoler *\n*********************************************";
		
		// Generate Readme file    
		try    
		{    
			// Check if file already exists. If yes, delete it.     
			if (File.Exists("README.md"))    
			{    
				File.Delete("README.md");    
			}    
			
			// Create the file     
			using (FileStream fs = File.Create("README.md"))     
			{    
				// Add some text to file    
			Byte[] title = new UTF8Encoding(true).GetBytes("Askel-CMD by Cryptoler \n\n\n If you get an error trying to run the program, install .NET with one of the links below: \n\n\n WINDOWS 64-BIT: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.302-windows-x64-installer \n LINUX: https://docs.microsoft.com/dotnet/core/install/linux?WT.mc_id=dotnet-35129-website \n MacOS: https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.302-macos-x64-installer \n Thank you for installing Askel-CMD!");    
				fs.Write(title, 0, title.Length);     
			}    
			
		}    
		catch (Exception Ex)    
		{    
			Console.WriteLine(Ex.ToString());    
		}
		
		// Clear Console
		Console.Clear();
		
		// Welcome 
		
		if(countDown == 1 || countDown > 0) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(welcomeMessage + "\n");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Make sure you read the README.md file before using this software. \n Continue? (y/n)");
			countDown--;
			string continue1 = Console.ReadLine();
			countDown--;
			
			if (continue1 == "y") {
				countDown--;
				Input();
			}
			else if(continue1 == "n") {
				System.Environment.Exit(1);
			}
		}
	}
	
	public static void Input() {
		
		Console.Clear();
		
		Console.WriteLine("Type 'h' for help");
		
		string input = Console.ReadLine();
		
		// c = chrome
		// -y = youtube
		
		if(input == "c") {	
			// Open Chrome
			Process.Start("chrome.exe");
			Input();
		}

		else if(input == "c -y") {
			// Open youtube in chrome
			Process.Start("chrome.exe", "http://www.youtube.com");
			Input();
		}
		
		else if(input == "fm") {
			// Go to FileManager
			FileManager();
		}
		
		else if(input == "h") {
			Console.WriteLine("\nc = Open Chrome \nc -y = Open Youtube in Chrome \nfm = File Manager (Create, Delete and Open Files) \ne = Exit");
			Thread.Sleep(6000);
			Input();
		}
		
		else if(input == "e") {
			Console.WriteLine("Exiting...");
			System.Environment.Exit(1);
		}
		
		else {
			// Not recognised 
			Console.WriteLine("Command not recognised!");
			Main(null);
		}
	}
	
	public static void FileManager() {
		
		Console.WriteLine("\n What do you want to do? \n (1) Create File \n (2) Create Folder \n (3) Delete Folder \n (4) Delete File \n (5) Open File \n (6) Go Back \n");
		
		string choice = Console.ReadLine();
		
		if(choice == "1") {
			CreateFile();
		}
		else if(choice == "2") {
			CreateFolder();
		}
		else if(choice == "3") {
			DeleteFolder();
		}
		else if(choice == "4") {
			DeleteFile();
		}
		else if(choice == "5") {
			OpenFile();
		}
		else if(choice == "6") {
			Input();
		}
		else { 
			Console.WriteLine("Invalid input..."); 
			System.Environment.Exit(1);
		}
	}
	
	// CreateFile Function
	public static void CreateFile() {
		
		Console.WriteLine("\n What do you want to name the folder?");
		string folderName = Console.ReadLine();
		
		// Get current directory
		string folderPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
		
		// Get folder location
		string filePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "/" + folderName;
		
		Console.WriteLine("\n What do you want to name your file?");	
		string Filename = Console.ReadLine();
		
		// Create Folder
		string folder = System.IO.Path.Combine(folderPath, folderName);
		System.IO.Directory.CreateDirectory(folder);
		
		// Create File in folder location
		string file = System.IO.Path.Combine(filePath, Filename);
		System.IO.File.Create(file).Dispose();
		
		// Go back
		Input();
	}
	
	public static void CreateFolder() {
		
		Console.WriteLine("\n What do you want to name the folder?");
		string folderName = Console.ReadLine();
		
		// Get current directory
		string folderPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
		
		// Create Folder
		string folder = System.IO.Path.Combine(folderPath, folderName);
		System.IO.Directory.CreateDirectory(folder);
		
		// Go back
		Input();
	}
	
	// DeleteFile Function
	public static void DeleteFile() {
		
		// Get File Location
		Console.WriteLine("\n In what folder is the file located?");
		string fileLocation = Console.ReadLine();
		
		// Get File Name
		Console.WriteLine("\n What is the name of the file?");
		string fileName = Console.ReadLine();
		
		// Delete File
		string file = System.IO.Path.Combine(fileLocation, fileName);
		System.IO.File.Delete(file);
		
		// Go back
		Input();	
	}
	
	public static void DeleteFolder() {
		
		// Get Current Location
		string folderLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
		
		// Get Folder Name
		Console.WriteLine("\n What is the name of the folder?");
		string folderName = Console.ReadLine();
		
		// Delete File
		string folder = System.IO.Path.Combine(folderLocation, folderName);
		System.IO.Directory.Delete(folder);
		
		// Go back
		Input();	
	}
	
	// OpenFile Function
	public static void OpenFile() {
		
		// Get File Location
		Console.WriteLine("\n In what folder is the file located?");
		string fileLocation = Console.ReadLine();
		
		// Get File Name
		Console.WriteLine("\n What is the name of the file?");
		string fileName = Console.ReadLine();
		
		string folderPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
		string filePath = folderPath + "\\" + fileLocation + "\\" + fileName;
		
		System.Diagnostics.Process.Start(filePath);
		
		// Go back
		Input();
	}
}
