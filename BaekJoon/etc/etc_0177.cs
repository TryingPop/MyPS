using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 민식어
    문제번호 : 1599번

    문자열, 정렬 문제이다

    개인 구조체를 만들고 기존 입력된 민식어를 영어로 변형시켜
    영어의 사전식 순서를 빌려 정렬했다
        예를들어 민식어의 k의 경우 c의 위치이므로 k -> c로 변형시켰다
    
    변형 방법에서 그냥 문자열의 replace메서드가 아닌 Stringbuilder 써서 하니 시간이 더 걸린다
        76 ms -> 96 ms

    그리고 정렬되면 기존 문자열을 출력하는 식으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0177
    {

        static void Main177(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            MyString[] arr = new MyString[len];

            for (int i = 0; i < len; i++)
            {

                string str = sr.ReadLine();
                arr[i].GetString(str);
            }

            sr.Close();

            Array.Sort(arr);

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < len; i++)
            {

                sw.WriteLine(arr[i].str);
            }
            sw.Close();
        }

        struct MyString : IComparable<MyString>
        {

            // public static StringBuilder sb;

            public string str;
            private string convertString;

            public void GetString(string _str)
            {

                str = _str;

                convertString = _str;

                convertString = convertString.Replace('k', 'c');
                convertString = convertString.Replace('p', 'q');
                convertString = convertString.Replace('o', 'p');
                convertString = convertString.Replace("ng", "o");

                /*
                // 메모리는 조금 적게쓰나 이 방법이 더 느리다;
                if (sb == null) sb = new(100);

                for (int i = 0; i < str.Length; i++)
                {

                    char c = str[i];

                    if (c == 'k') sb.Append('c');
                    else if (c == 'o') sb.Append('p');
                    else if (c == 'p') sb.Append('q');
                    else sb.Append(c);
                }

                sb.Replace("ng", "o");
                convertString = sb.ToString();
                sb.Clear();
                */
            }

            public int CompareTo(MyString other)
            {

                return convertString.CompareTo(other.convertString);
            }
        }
    }

#if other
using System;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] array = new string[n];
        for (int i = 0; i < n; i++)
        {
            array[i] = Console.ReadLine();
        }
        string s = "abkdeghilmn.oprstuwy";
        Dictionary<char, int> dic = new();
        for (int i = 0; i < 20; i++)
        {
            dic.Add(s[i], i);
        }
        Array.Sort(array, (a, b) =>
        {
            int ap = 0, bp = 0;
            while (ap < a.Length && bp < b.Length)
            {
                int ac = 0, bc = 0;
                if (ap < a.Length - 1 && a[ap] == 'n' & a[ap + 1] == 'g')
                {
                    ac = 11;
                    ap += 2;
                }
                else
                    ac = dic[a[ap++]];
                if (bp < b.Length - 1 && b[bp] == 'n' & b[bp + 1] == 'g')
                {
                    bc = 11;
                    bp += 2;
                }
                else
                    bc = dic[b[bp++]];
                if (ac != bc)
                    return ac - bc;
                if (ap == a.Length)
                    return -1;
                if (bp == b.Length)
                    return 1;
            }
            return 0;
        });
        Console.Write(string.Join('\n', array));
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace no1599try1
{
    
    internal class Program
    {
        

        /// <summary>
        ///     Minsik-lish Language Word
        /// </summary>
        private class Word : IComparable<Word>
        {
            readonly private List<int> letterCode;
            readonly private string english;
            readonly private Dictionary<string, int> letterNumberPairs = new Dictionary<string, int>()
            {
                { "a", 10 },
                { "b", 11 },
                { "k", 12 },
                { "d", 13 },
                { "e", 14 },
                { "g", 15 },
                { "h", 16 },
                { "i", 17 },
                { "l", 18 },
                { "m", 19 },
                { "n", 20 },
                { "ng", 21 },
                { "o", 22 },
                { "p", 23 },
                { "r", 24 },
                { "s", 25 },
                { "t", 26 },
                { "u", 27 },
                { "w", 28 },
                { "y", 29 }
            };

            public Word(string english)
            {
                letterCode = ConvertWordsToNumbers(english);
                this.english = english;
            }

            public string Letter { get => english; }

            public static bool operator >(Word left, Word right)
            {
                return CompareIsLeftBig(left, right);
            }
            public static bool operator <(Word left, Word right)
            {
                return CompareIsLeftBig(right, left);
            }
            public int CompareTo(Word target)
            {
                if (CompareIsLeftBig(target, this)) return -1;
                else return 1;
            }

            private List<int> ConvertWordsToNumbers(string minsiklishLanguage)
            {
                List<int> result = new List<int>();
                // 쪼개는것부터 시작
                bool IsLetterN = false;
                for (int index = 0; index < minsiklishLanguage.Length; ++index)
                {
                    // n 글자가 오면 처리를 미룹니다.
                    if(IsLetterN && minsiklishLanguage[index].Equals('g'))
                    {
                        IsLetterN = false;
                        result.Add(letterNumberPairs["ng"]);
                        continue;
                    }
                    else if (IsLetterN && minsiklishLanguage[index].Equals('n'))
                    {
                        //이전의 n을 집어넣습니다.
                        result.Add(letterNumberPairs["n"]);
                        continue;
                    }
                    else if(IsLetterN)
                    {
                        result.Add(letterNumberPairs["n"]);
                        result.Add(letterNumberPairs[minsiklishLanguage[index].ToString()]);
                        IsLetterN = false;
                        continue;
                    }
                    else if (minsiklishLanguage[index].Equals('n'))
                    {
                        // 다음에 n 넣을 준비
                        IsLetterN = true;
                        continue;
                    }
                    else
                    {
                        result.Add(letterNumberPairs[minsiklishLanguage[index].ToString()]);
                        continue;
                    }
                }
                if (minsiklishLanguage.EndsWith("n")) result.Add(letterNumberPairs["n"]);
                return result;
            }
            //
            // 기준 : 가장 가까운 알파벳의 순서 -> 글자가 크면 true
            private static bool CompareIsLeftBig(Word left, Word right)
            {
                int count = Math.Min(left.letterCode.Count, right.letterCode.Count);
                for(int index = 0; index < count; ++index)
                {
                    if (left.letterCode[index].Equals(right.letterCode[index])) continue;
                    return left.letterCode[index] > right.letterCode[index];
                }
                return left.letterCode.Count > right.letterCode.Count;
            }
        }
        static void Main(string[] args)
        {
            int wordCount = int.Parse(Console.ReadLine());
            Word[] words = new Word[wordCount];
            
            for(int i = 0; i < wordCount; ++i)
            {
                words[i] = new Word(Console.ReadLine());
            }
            Array.Sort(words);
            for(int i = 0; i < wordCount; ++i)
            {
                Console.WriteLine(words[i].Letter);
            }

            Console.ReadLine();
        }
    }
}

#endif
}
