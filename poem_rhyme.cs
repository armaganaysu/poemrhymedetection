using System;
using System.IO;
using System.Text.RegularExpressions;

namespace poem_rhyme
{
    class Program
    {
        static string ReverseString(string s)
        {
            // Convert to char array, then call Array.Reverse.
            // ... Finally use string constructor on array.
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        static string longestcommonstr(String X, String Y, int m, int n)
        {
            
            int[,] LCSuff = new int[m + 1, n + 1];

            int len = 0;

            int row = 0, col = 0;

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        LCSuff[i, j] = 0;

                    else if (X[i - 1] == Y[j - 1])
                    {
                        LCSuff[i, j] = LCSuff[i - 1, j - 1] + 1;
                        if (len < LCSuff[i, j])
                        {
                            len = LCSuff[i, j];
                            row = i;
                            col = j;
                        }
                    }
                    else
                        LCSuff[i, j] = 0;
                }
            }

            

           
            String resultStr = "";

           
            while (LCSuff[row, col] != 0)
            {
                resultStr = X[row - 1] + resultStr; 
                --len;

               
                row--;
                col--;
            }

            
           
            
            return resultStr;
        }
        static void Main(string[] args)
        {
            // Storing poem from text file to array
            StreamReader f = File.OpenText("D:\\poem.txt");
            string str = "";
            int linenum = 0;
            do
            {
                str = f.ReadLine();
                linenum++;
            } while (!f.EndOfStream);
            f.Close();
            f = File.OpenText("D:\\poem.txt");
            string[] poem = new string[linenum];
            string[] reverse_poem = new string[linenum];
            int l = 0;
            while (!f.EndOfStream)
            {
                poem[l] = f.ReadLine();
                reverse_poem[l] = poem[l];
                l++;
            }
            f.Close();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Poem:");
            Console.ResetColor();

            for (int z = 0; z < poem.Length; z++)
            {
                Console.WriteLine(poem[z]);
            }
            

            // Creating a jagged array to hold the words in the poem
            string[][] poem_words = new string[poem.Length][];
            string[] poem_line_words = null;
            for (int i = 0; i < poem.Length; i++)
            {
                poem_line_words = poem[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                poem_words[i] = new string[poem_line_words.Length];
                poem_words[i] = poem_line_words;
            }


           

            Console.WriteLine();
            for (int z = 0; z < poem_words.Length; z++)
            {
                Array.Reverse(poem_words[z]);
            }

            // Reversing poem for additional rhyme control.
            for (int z = 0; z < reverse_poem.Length; z++)
            {
                reverse_poem[z]=ReverseString(reverse_poem[z]);
            }


            int m = 0;
            string[] phrase_rhyme_words = new string[100];
            string[] word_rhyme_words = new string[100];
            bool isPhraseRhyme = false;
            bool isWordRhyme = false;
            for (int i = 0; i < poem.Length; i++)
            {

                int ctr = 0;
                for (int j = 1; j < poem.Length; j++)
                {
                    string temp = "";
                    
                    for (int z = 0; z < Math.Min(poem_words[i].Length, poem_words[j].Length); z++)
                    {
                        if ((i != j) && (poem_words[i][z]) == (poem_words[j][z])) 
                        {
                            ctr++;
                            temp = poem_words[i][z]+" " + temp;
                                               
                        }
                        else
                        {                         
                            break;
                        }

                    }
                    
                        if (ctr > 1)
                        {
                        if (Array.IndexOf(phrase_rhyme_words, temp) == -1)
                        {
                            isPhraseRhyme = true;
                            phrase_rhyme_words[m] = temp;
                        }
                        }

                        else if (ctr == 1)
                        {
                            
                            if (Array.IndexOf(word_rhyme_words, temp) == -1)
                            {
                                isWordRhyme = true;
                                word_rhyme_words[m] = temp;
                            }
                            
                        }
                    
                    m++;
                }
            }
            if (isPhraseRhyme)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Phrase rhyme :");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int i = 0; i < phrase_rhyme_words.Length; i++)
                {
                    if (phrase_rhyme_words[i] != null && phrase_rhyme_words[i] != String.Empty && Array.IndexOf(word_rhyme_words, phrase_rhyme_words[i]) == -1)
                    {
                        Console.WriteLine(phrase_rhyme_words[i]);
                    }
                }
                Console.ResetColor();
            }

            
            if (isWordRhyme)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Word rhyme :");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                for (int i = 0; i < word_rhyme_words.Length; i++)
                {
                    if (word_rhyme_words[i] != null && word_rhyme_words[i] != String.Empty)
                    {
                        Console.WriteLine(word_rhyme_words[i]);
                    }
                }
                Console.ResetColor();
            }

            int n = 0;
            bool isAdditional = false;
            string[] additional_rhyme_words = new string[100];
            string[] commons = new string[poem.Length];
            for (int i = 0; i < poem.Length; i++)
            {
                
                for (int j = 1; j < poem.Length; j++)
                {
                    string temp2 = "";
                    for (int z = 0; z < Math.Min(reverse_poem[i].Length,reverse_poem[j].Length); z++)
                    {
                        if ((i!=j)&&(reverse_poem[i][z]==reverse_poem[j][z]))
                        {
                            
                            temp2= Convert.ToString(reverse_poem[i][z])+temp2 ;
                        }
                        else
                        {                           
                            break;
                        }
                    }

                    if (temp2.Length>=2 )
                    {
                        commons[i] = temp2;
                       
                        if (Array.IndexOf(additional_rhyme_words, temp2) == -1)
                        {
                            isAdditional = true;
                            additional_rhyme_words[n] = temp2;
                        }
                        
                    }
                    n++;
               
                }
            }

            if (isAdditional)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Additional rhyme :");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                for (int i = 0; i < additional_rhyme_words.Length; i++)
                {
                    if (additional_rhyme_words[i] != null && additional_rhyme_words[i] != String.Empty)
                    {
                        if (Array.IndexOf(phrase_rhyme_words, additional_rhyme_words[i]) == -1)
                        {
                            Console.WriteLine(additional_rhyme_words[i]);
                        }
                        
                    }
                }
                Console.ResetColor();
            }

            
            

            Console.WriteLine();
            if (poem.Length%4==0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Rhyme scheme(s) :");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                int quotient = poem.Length / 4;
                for (int i = 0; i < quotient; i++)
                {

                    if ((commons[0 + (i * 3)] == commons[1 + (i * 3)] && commons[0 + (i * 3)] == commons[2 + (i * 3)] && commons[0 + (i * 3)] == commons[3 + (i * 3)]) ||  //AAAA
                            (poem_words[0+(i*3)][0]==poem_words[1+(i*3)][0] && poem_words[0+(i*3)][0] == poem_words[2+(i*3)][0] && poem_words[0 + (i * 3)][0] == poem_words[3 + (i * 3)][0]) ||
                            (longestcommonstr(commons[0 + (i * 3)], commons[1 + (i * 3)], commons[0 + (i * 3)].Length, commons[1 + (i * 3)].Length)) ==
                            (longestcommonstr(commons[2 + (i * 3)], commons[3 + (i * 3)], commons[2 + (i * 3)].Length, commons[3 + (i * 3)].Length)))
                    {
                        
                            Console.WriteLine("Straight Rhyme");
                        
                    }
                    else if (commons[0 + (i * 3)] == commons[2 + (i * 3)] && commons[1 + (i * 3)] == commons[3 + (i * 3)]) // ABAB
                    {
                        
                        
                            Console.WriteLine("Alternating Rhyme");
                        
                        
                    }
                    else if (commons[0 + (i * 3)] == commons[3 + (i * 3)] && commons[1 + (i * 3)] == commons[2 + (i * 3)]) //ABBA
                    {
                        
                        
                            Console.WriteLine("Winding Rhyme");
                        
                        
                    }
                }
                Console.ResetColor();
            }
            else if (poem.Length%3==0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Rhyme scheme(s) :");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                int quotient = poem.Length / 3;
                for (int i = 0; i < quotient; i++)
                {
                    if ((commons[0+(2*i)]==commons[1+(2*i)]&& commons[0 + (2 * i)] == commons[2 + (2 * i)])||
                            (poem_words[0+(2*i)][0]== poem_words[1 + (2 * i)][0] && poem_words[0 + (2 * i)][0] == poem_words[2 + (2 * i)][0]) ||
                            (longestcommonstr(commons[0 + (2 * i)], commons[1 + (2 * i)], commons[0 + (2 * i)].Length, commons[1 + (2 * i)].Length) ==
                            longestcommonstr(commons[1 + (2 * i)], commons[2 + (2 * i)], commons[1 + (2 * i)].Length, commons[2 + (2 * i)].Length)))
                    {
                        Console.WriteLine("Straight Rhyme");
                    }
                    else if (commons[0 + (2 * i)] == commons[2 + (2 * i)] && (commons[0 + (2 * i)] != commons[1 + (2 * i)]))
                    {
                        Console.WriteLine("Hoarse Rhyme");
                    }
                
                }
                Console.ResetColor();
             }

            
        }


    }
}


