using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Statistics.Class
{
    class Bad_Call:ApplicationException
    {
        int codeError;
        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }
        string messageError;
        public string MessageError
        {
            get { return messageError; }
            set { messageError = value; }
        }
        int numberPhone;
        public int NumberPhone
        {
            get { return numberPhone; }
            set { numberPhone = value; }
        }
        public Bad_Call(int code,string message, int number)
        {
            codeError = code;
            messageError = message;
            numberPhone = number;
        }
        public override string ToString()
        {
            return String.Format("Номер ошибки: {0} {1}, Телефон: {2}", codeError, messageError, numberPhone);
        }
        public void WriteBadCall()
        {
            using(FileStream file = new FileStream("BadCall.txt", FileMode.Append, FileAccess.Write))
            {
                using(StreamWriter strW = new StreamWriter(file, Encoding.Default))
                {
                    string str = codeError + " " + messageError + " " + numberPhone;
                    strW.WriteLine(str);
                }
            }
        }
    }
}
