using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 단어만들기
    문제번호 : 1148번

    구현, 문자열 문제다
    단어를 기억하는게 아닌 단어에 쓰인 알파벳 수를 기억했다
    그리고, 보드판의 알파벳 수를 기록해서 보드판의 알파벳 수보다 작은 문자열만 된다고 했다
    그리고 되는 경우 해당 단어의 알파벳 부분에 1씩 추가해줬다 (중앙에 왔을 때, 쓰일 수 있는 문자)
    그러니 이상없이 152ms에 통과했다

    문자열 입력 받는 부분을 sr.Read()로 
    알파벳만 따온다면 속도랑 메모리가 조금은 개선될거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0498
    {

        static void Main498(string[] args)
        {

            int[,] arr = new int[200_000, 26];
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = 0;
            while (true)
            {

                // 문자열 입력받기, 쓰인 알파벳 수만 확인한다
                string str = sr.ReadLine();
                if (str[0] == '-') break;
                for (int i = 0; i < str.Length; i++)
                {

                    int idx = str[i] - 'A';
                    arr[len, idx]++;
                }

                len++;
            }

            int[] calc1 = new int[26];          // 최대 개수
            bool[] calc2 = new bool[26];        // 해당문자 썼는지 확인
            int[] ret = new int[26];            // 만족하는 문자열 수

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 8);
            while (true)
            {

                string str = sr.ReadLine();
                if (str[0] == '#') break;
                for (int i = 0; i < str.Length; i++)
                {

                    int idx = str[i] - 'A';
                    calc1[idx]++;
                }

                for(int i = 0; i < len; i++)
                {

                    // 매치하는 문자열 찾기
                    bool mismatch = false;
                    for (int j = 0; j < 26; j++)
                    {

                        if (arr[i, j] <= calc1[j]) continue;

                        mismatch = true;
                        break;
                    }

                    if (mismatch) continue;

                    for (int j = 0; j < 26; j++)
                    {

                        if (arr[i, j] == 0 || calc2[j]) continue;
                        calc2[j] = true;
                        ret[j]++;
                    }

                    for (int j = 0; j < 26; j++)
                    {

                        calc2[j] = false;
                    }
                }

                // 출력부분
                int min = len;
                int max = 0;
                for (int i = 0; i < 26; i++)
                {

                    if (calc1[i] == 0) continue;
                    min = ret[i] < min ? ret[i] : min;
                    max = max < ret[i] ? ret[i] : max;
                }

                for (int i = 0; i < 26; i++)
                {

                    if (calc1[i] == 0 || ret[i] > min) continue;
                    sw.Write((char)(i + 'A'));
                }

                sw.Write($" {min} ");

                for (int i = 0; i < 26; i++)
                {

                    if (calc1[i] == 0 || ret[i] < max)
                    {

                        calc1[i] = 0;
                        ret[i] = 0;
                        continue; 
                    }
                    sw.Write((char)(i + 'A'));
                    calc1[i] = 0;
                    ret[i] = 0;
                }

                sw.Write($" {max}\n");
            }

            sw.Close();
            sr.Close();
        }
    }

#if other
using System.Text.RegularExpressions;

var reader = new StreamReader(Console.OpenStandardInput());
var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

var wordChars = new int[200000][];
int wordCount = 0;
string line;
while ((line = reader.ReadLine()) != "-")
{
    var wordOccurs = new int[26];
    foreach (var c in line)
        wordOccurs[c - 'A']++;

    wordChars[wordCount++] = wordOccurs;
}

while ((line = reader.ReadLine()) != "#")
{
    var boardOccurs = new int[26];
    foreach (var c in line)
        boardOccurs[c - 'A']++;

    int min = 200001;
    int max = 0;
    var buildables = new int[26];

    foreach (var c in line)
    {
        if (buildables[c - 'A'] != 0)
            continue;
        
        for (int w = 0; w < wordCount; w++)
        {
            if (wordChars[w][c - 'A'] == 0)
                continue;

            bool buildable = true;
            for (int i = 0; i < 26; i++)
                if (boardOccurs[i] < wordChars[w][i])
                {
                    buildable = false;
                    break;
                }

            if (buildable)
                buildables[c - 'A']++;
        }

        if (boardOccurs[c - 'A'] != 0)
        {
            min = Math.Min(min, buildables[c - 'A']);
            max = Math.Max(max, buildables[c - 'A']);
        }
    }

    for(int i = 0; i < 26; i++)
        if (boardOccurs[i] > 0 && buildables[i] == min)
            writer.Write((char)('A' + i));
    writer.Write(" " + min + " ");

    for(int i = 0; i < 26; i++)
        if (boardOccurs[i] > 0 && buildables[i] == max)
            writer.Write((char)('A' + i));
    writer.Write(" " + max + "\n");
}

reader.Close();
writer.Close();
#endif
}
