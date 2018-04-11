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
	public int questionId = 0;
	public GameObject zombieController;
	public GameObject zombieA;
	public GameObject zombieB;
	public GameObject zombieC;
	public GameObject zombieD;
	public GameObject end;
	public GameObject background;
	public GameObject nextRound;
	public GameObject playerStartLoc;
	public GameObject player;
	public bool isOver = false;

	private float timer = 0;
	private bool runTime = false;
	private int prevId = 0;
	private List<Question> questions = new List<Question>();

	private class Question {
		public string question;
		public string[] options;
	}

	// Use this for initialization
	void Start () {
		Load (filename);
		displayQuestion (questionId);
	}
	
	// Update is called once per frame
	void Update () {
			if (prevId != questionId) {
				displayQuestion (questionId);
				prevId = questionId;
			}
		if (runTime) {
			timer += Time.deltaTime;
			if (timer > 2) {
				nextRound.SetActive (false);
				runTime = false;
			}
		}
	}

	public void loadNextQuestion(){
		player.transform.position = playerStartLoc.transform.position;
		nextRound.SetActive (true);
		timer = 0;
		runTime = true;
		zombieA.SendMessage ("revive");
		zombieB.SendMessage ("revive");
		zombieC.SendMessage ("revive");
		zombieD.SendMessage ("revive");

		if (!isOver) {
			questionId++;
			if (questionId >= questions.Count) {
				questionId = 0;
			}
		}
	}

	private void displayQuestion(int id) {
		question.text = questions[id].question;
		options.text = "";
		System.Random rnd = new System.Random ();
		string[] randomOptions = new string[4];
		List<String> optionsQueue = new List<String>();
		for(int i = 0; i < 4; i++) {
			optionsQueue.Add (questions [id].options [i]);
		}
		int correct = -1;
		for (int i = 0; i < 4; i++) {
			int index = rnd.Next (0, 4 - i);
			if (index == 0 && correct < 0) {
				correct = i;
			}
			randomOptions [i] = optionsQueue [index];
			optionsQueue.Remove (optionsQueue [index]);
		}
		for (int i = 0; i < 4; i++) {
			string letter = "O";
			switch (i) {
			case 0:
				letter = "A";
				break;
			case 1:
				letter = "B";
				break;
			case 2:
				letter = "C";
				break;
			case 3:
				letter = "D";
				break;
			}
			options.text = options.text + letter + ") " + randomOptions[i] + "\n";
		}
		loadTargets(correct);
	}

	private void loadTargets(int correct) {
		zombieController.SendMessage ("StartWave", 1.0F);
		Debug.Log("Correct " + correct);
		zombieA.SendMessage ("isCorrect", false);
		zombieB.SendMessage ("isCorrect", false);
		zombieC.SendMessage ("isCorrect", false);
		zombieD.SendMessage ("isCorrect", false);

		switch (correct) {
		case 0:
			zombieA.SendMessage ("isCorrect", true);
			break;
		case 1:
			zombieB.SendMessage ("isCorrect", true);
			break;
		case 2:
			zombieC.SendMessage ("isCorrect", true);
			break;
		case 3:
			zombieD.SendMessage ("isCorrect", true);
			break;
		}
	}

	private void loadQuestion(string[] data) {
		Question q = new Question ();
		q.question = data [0];
		q.options = new string[data.Length-1];
		for (int i = 1; i < data.Length; i++) {
			q.options[i-1] = data[i];
		}
		questions.Add(q);

	}

	private bool Load(string fileName)
	{
		Debug.Log ("Reading file " + filename);
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
						Debug.Log("Reading line: " + line);
						string[] entries = line.Split('\\');
						if (entries.Length > 0)
							loadQuestion(entries);
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				Debug.Log("Done reading.");
				theReader.Close();
				return true;
			}
			Debug.Log("Done.");
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Debug.Log ("Error " + e.Message);
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
	}

	public void endGame() {
		isOver = true;
		end.SetActive (true);
		background.SetActive (true);
		zombieController.SendMessage ("EndWave", false);
	}
}
