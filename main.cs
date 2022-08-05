using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Diagnostics;  
using System.Runtime.InteropServices;

class MainProgram {
	
    [DllImport("kernel32.dll", ExactSpelling = true)]  
    private static extern IntPtr GetConsoleWindow();  
    private static IntPtr ThisConsole = GetConsoleWindow();  
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);  
    private const int HIDE = 0;  
    private const int MAXIMIZE = 3;  
    private const int MINIMIZE = 6;  
    private const int RESTORE = 9;  
	
	static void Main(string[] args) {
		
		// Set as startup
		RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); 
		key.SetValue("Askel-CMD", Application.ExecutablePath);
		
		// Fullscreen
		Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);  
        ShowWindow(ThisConsole, MAXIMIZE);  
		
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
		
		Console.WriteLine("Home - Type 'h' for help");
		
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
		Console.Clear();
		
		Console.WriteLine("FileManager - Type 'h' for help");
		
		string choice = Console.ReadLine();
		
		if(choice == "cf") {
			CreateFile();
		}
		else if(choice == "cfo") {
			CreateFolder();
		}
		else if(choice == "dfo") {
			DeleteFolder();
		}
		else if(choice == "df") {
			DeleteFile();
		}
		else if(choice == "of") {
			OpenFile();
		}
		else if(choice == "e") {
			Input();
		}
		else if(choice == "h") {
			Console.WriteLine(" \n cf = Create File \n cfo = Create Folder \n dfo = Delete Folder \n df = Delete File \n of = Open File \n e = exit \n");
			Thread.Sleep(6000);
			FileManager();
		}
		else { 
			Console.WriteLine("Invalid input..."); 
			System.Environment.Exit(1);
		}
	}
	
	// CreateFile Function
	public static void CreateFile() {
		
		Console.WriteLine("What do you want to name your folder?");
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
		FileManager();
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
		FileManager();
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
		FileManager();	
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
		FileManager();	
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
		FileManager();
	}
}
