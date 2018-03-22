using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;


public class questionHandler : MonoBehaviour {

	public string filename;
	public Text question;
	public Text options;
	private List<Question> questions = new List<Question>();

	private class Question {
		public string question;
		public string[] options;
	}

	// Use this for initialization
	void Start () {
		Load (filename);
		displayQuestion (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void displayQuestion(int id) {
		question.text = questions[id].question;
		options.text = "";
		for(int i = 0; i < questions[id].options.Length; i++) {
			options.text = options.text + questions[id].options [i] + "\n";
		}
	}

	private void loadQuestion(string[] data) {
		Question q = new Question ();
		q.question = data [0];
		q.options = new string[data.Length-1];
		for (int i = 1; i < data.Length; i++) {
			string letter = "O";
			switch (i) {
			case 1:
				letter = "A";
				break;
			case 2:
				letter = "B";
				break;
			case 3:
				letter = "C";
				break;
			}
			q.options[i-1] = letter + ") "+data[i];
		}
		questions.Add(q);

	}

	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();

					if (line != null)
					{
						string[] entries = line.Split('\\');
						if (entries.Length > 0)
							loadQuestion(entries);
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}
}
