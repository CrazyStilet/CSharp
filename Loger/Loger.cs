using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loger.Class
{
    class Loger
    {
        public enum Type { Exeption, Error };
        static string str;
        public static string FormatString(string message,Type type)
        {
            try
            {
                using(FileStream file=new FileStream("initialize.ini",FileMode.Open,FileAccess.Read))
                {
                    using(StreamReader strR=new StreamReader(file,Encoding.Default))
                    {
                        str = null;
                        string[] strM = strR.ReadToEnd().Split('\n');
                        for(int i = 0; i < strM.Length; i++)
                        {
                            DateTime now = DateTime.Now;
                            switch(strM[i])
                            {
                            case "Data":
                            case "Data\r":
                                str += "<" + now.Day + "." + now.Month + "." + now.Year + ">";
                                break;
                            case "Time":
                            case "Time\r":
                                str += "<" + now.Hour + "." + now.Minute + "." + now.Second + ">";
                                break;
                            case "TypeMessage":
                            case "TypeMessage\r":
                                str += "<" + type.ToString() + ">";
                                break;
                            case "Message":
                            case "Message\r":
                                str += "<" + message + ">";
                                break;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return str;
        }
        public static void Write(Exception e)
        {
            using(FileStream file=new FileStream("log.txt",FileMode.Append,FileAccess.Write))
            {
                using(StreamWriter strW=new StreamWriter(file,Encoding.Default))
                {
                    strW.WriteLine(FormatString(e.Message,Type.Exeption));
                }
            }
        }
        public static void Write(string message)
        {
            using(FileStream file=new FileStream("log.txt",FileMode.Append,FileAccess.Write))
            {
                using(StreamWriter strW = new StreamWriter(file, Encoding.Default))
                {
                    strW.WriteLine(FormatString(message,Type.Error));
                }
            }
        }
        public static void Write()
        {
            using(FileStream file=new FileStream("log.txt",FileMode.Append,FileAccess.Write))
            {
                using(StreamWriter strW=new StreamWriter(file,Encoding.Default))
                {
                    //FormatString();
                    //strW.WriteLine();
                }
            }
        }
        public static void Truncate()
        {
            using(FileStream file = new FileStream("log.txt", FileMode.Truncate, FileAccess.ReadWrite,FileShare.ReadWrite))
            {
            }
        }
    }
}
