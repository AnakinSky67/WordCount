using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        class function
        {
            List<string> Cout = new List<string>();
            public int Countchar(string x)
            {
                int charnum = 0;
                char[] x1 = x.ToCharArray();
                for (int i = 0; i < x1.Length; i++)
                {
                    if (x[i] >= 0 && x[i] < 127)
                        charnum++;
                }
                return charnum;
            }
            public int Countwords(string x)
            {
                int wordsnum = 0;
                int lettercount = 0;
                char[] x2 = x.ToCharArray();
                if (x[0] != ' ')
                {
                    for (int h = 0; h <= 3; h++)
                    {
                        if (x[h] >= 48 && x[h] <= 57 || x[h] == ' ')
                            break;
                        else if (x[h] >= 97 && x[h] <= 122 || x[h] >= 65 && x[h] <= 90)
                        {
                            lettercount++;
                            if (lettercount == 4)
                                wordsnum++;
                        }
                    }
                }
            
                lettercount = 0;
                for (int j = 0; j < x2.Length; j++)
                {
                    if (x[j] == ' ' || x[j] == '\n')
                    {
                        lettercount = 0;
                        for (int k = j + 1; k < x2.Length; k++)
                        {
                            if (k - j <= 4 && x[k] >= 48 && x[k] <= 57 || x[k] == ' ')
                                break;
                            else if (x[k] >= 97 && x[k] <= 122 || x[k] >= 65 && x[k] <= 90)
                            {
                                lettercount++;
                                if (lettercount == 4)
                                    wordsnum++;
                            }
                        }
                    }
                }
                return wordsnum;

            }
            public int Countlines(string x, string path)
            {
                char[] x3 = x.ToCharArray();
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    int Countline = 1;
                    for (int i = 0; i < x.Length; i++)
                    {
                        if (x[i] == '\n')
                        {
                            Countline++;
                            if ((line = sr.ReadLine()).Trim() == string.Empty)
                            {
                                Countline--;
                            }
                        }

                    }
                    return Countline;
                }
            }
            public int CountMostwords(string path, int m, int n)
            {
                List<string> list = new List<string>();
                List<string> list1 = new List<string>();
                Dictionary<string, int> frequencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                Dictionary<string, int> frequencies1 = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                string s = "";
                if (File.Exists(path))
                {
                    StreamReader sr = new StreamReader(path, Encoding.Default);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] wordsArr1 = Regex.Split(line.ToLower(), "\\s*[^0-9a-zA-Z]+");
                        foreach (string word in wordsArr1)
                        {
                            if (Regex.IsMatch(word, "^[a-zA-Z]{4,}[a-zA-Z0-9]*"))
                            {
                                list.Add(word);
                                list1.Add(word);
                            }
                        }

                    }
                    sr.Close();

                    for (int i = 0; i <= list.Count - 1; i++)
                    {
                        int j;
                        for (s = list[i], j = 0; j < 0; j++)
                        {
                            s += " " + list[i + j + 1];
                        }
                        if (frequencies.ContainsKey(s))
                        {
                            frequencies[s]++;
                        }
                        else
                            frequencies[s] = 1;
                    }
                    int o = 0;
                    Console.WriteLine("单词频率前" + n +"的单词 :");
                    Dictionary<string, int> item = frequencies.OrderByDescending(r => r.Value).ThenBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
                    foreach (KeyValuePair<string, int> entry in item)
                    {
                        o++;
                        if (o > n)
                            break;
                        string word = entry.Key;
                        int frequency = entry.Value;
                        Console.WriteLine(+ o + ": " + word + "  :  " + frequency + "次 ");
                        Cout.Add(+ o + ": " + word + "  :  " + frequency + "次 ");
                    }
                    for (int i = 0; i <= list1.Count - m; i++)
                    {
                        int j;
                        for (s = list1[i], j = 0; j < m - 1; j++)
                        {
                            s += " " + list1[i + j + 1];
                        }
                        if (frequencies1.ContainsKey(s))
                        {
                            frequencies1[s]++;
                        }
                        else
                            frequencies1[s] = 1;
                    }
                    Dictionary<string, int> item1 = frequencies1.OrderByDescending(r => r.Value).ThenBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
                    Console.WriteLine("输出长度为" + m + "的单词组：");
                    Cout.Add("输出长度为" + m + "的单词组：");
                    foreach (var dic1 in item1)
                    {
                        Console.Write(" {0}  {1}次\n", dic1.Key, dic1.Value);
                        Cout.Add(dic1.Key.ToString() + "  " + dic1.Value + "次");
                    }
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            public void WriteFile(string path)
            {
                    FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                    StreamWriter sr = new StreamWriter(fs);
                    foreach (string w in Cout)
                    {
                        sr.WriteLine(w);
                    }
                    sr.Close();
                    fs.Close();
            }
        }
            static void Main(string[] args)
            {
                function A = new function();
                string path = @"E:\Visual Studio 2019\代码文件\新建文本文档.txt";
                //FileStream fs = new FileStream(path, FileMode.Open);
                string a = File.ReadAllText(@"E:\Visual Studio 2019\代码文件\新建文本文档.txt");

            // fs.Read(a, 0, zipdata.Length);
            Console.WriteLine("letters: {0}", A.Countchar(a));
            Console.WriteLine("words: {0}", A.Countwords(a));
            Console.WriteLine("lines: {0}", A.Countlines(a,path));
            Console.WriteLine(A.CountMostwords(path,2,10));
            Console.ReadKey();
            }

    }
 }

