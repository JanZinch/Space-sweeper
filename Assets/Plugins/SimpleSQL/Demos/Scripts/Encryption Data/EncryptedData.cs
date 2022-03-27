namespace SimpleSQL.Demos
{
    using SimpleSQL;

    public class EncryptedData
    {
        /// <summary>
        /// Set this to a strong password that only you know
        /// </summary>
        private const string password = "ABCDEFG1234567890";

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// This property stores the encrypted text.
        /// It also automatically decryptes the value and
        /// stores in the readonly field DecryptedText.
        /// </summary>
        private string _encryptedText;
        public string EncryptedText
        {
            get
            {
                return _encryptedText;
            }
            set
            {
                _encryptedText = value;
                _decryptedText = Cipher.Decrypt(_encryptedText, password);
            }
        }

        /// <summary>
        /// This property is hidden from SimpleSQL's ORM reflection
        /// since we do not have a get method. This property is readonly
        /// for displaying the clear text value of our encrypted field.
        /// To set the clear text value (and have it automatically encrypted)
        /// use the SetDecryptedText method.
        /// </summary>
        private string _decryptedText;
        public string DecryptedText
        {
            get
            {
                return _decryptedText;
            }
        }

        /// <summary>
        /// This method sets the DecryptedText field, updating the
        /// EncryptedText. Use this instead of setting the DecryptedText
        /// field manually.
        /// </summary>
        /// <param name="clearText">The text to encrypt</param>
        public void SetDecryptedText(string clearText)
        {
            EncryptedText = Cipher.Encrypt(clearText, password);
        }
    }
}
