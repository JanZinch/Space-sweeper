namespace SimpleSQL.Demos
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// This script shows how you can use encryption in your database.
    /// Encryption is handled behind the scenes as you read and write data to
    /// your class objects.
	/// </summary>
	public class Encryption : MonoBehaviour
	{
		// reference to our db manager object
		public SimpleSQL.SimpleSQLManager dbManager;

		// input fields
		public InputField toEncryptField;

		// reference to our output text object
		public Text outputText;

		void Start()
		{
			Debug.Log(Application.persistentDataPath);

			// clear out the output text since we are using GUI in this example
			outputText.text = "";

			// reset the GUI and reload
			ResetGUI();
		}

		private void ResetGUI()
		{
			// Reset the temporary GUI variables
			toEncryptField.text = "";

			outputText.text = "";
		}

		/// <summary>
		/// Encrypts the value of the input string
		/// </summary>
		public void Encrypt()
		{
			var clearText = toEncryptField.text;

			// set up a new record and pass in the clear text.
			// this will be encrypted automatically at the class level.
			var model = new EncryptedData();

            // set the decrypted text using the class method.
            // we have to use a method so that DecryptedText is
            // not picked up by reflection.
			model.SetDecryptedText(clearText);

            // insert into the database
			dbManager.Insert(model);

            // show the result
			ResetGUI();
			outputText.text = $"'{clearText}' inserted and encrypted.";
		}

        /// <summary>
        /// Reads all records in database and shows the decrypted values.
        /// The DescryptedText field is automatically populated when the
        /// database is read.
        /// </summary>
        public void ReadAndDecrypt()
        {
			ResetGUI();

			var results = dbManager.Query<EncryptedData>("SELECT * FROM EncryptedData");

            foreach (var result in results)
            {
				outputText.text += $"{result.ID}: {result.DecryptedText}\n";
            }
        }
	}
}
