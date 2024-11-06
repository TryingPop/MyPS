using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 26
이름 : 배성훈
내용 : 찾기
    문제번호 : 1786번

    문자열 매칭 문제이다
    처음에 문제에서 의도하는 기능을 제대로 이해 못해서 틀렸다
    예를들어 banana banana가 있고 ana가 몇 번 나왔는지 확인하는 문제라고 하면,
        처음에는 b'ana'에서 ana를 찾고 뒤에 na는 무시하고 다음 문장으로 가서 똑같이 b'ana'na 찾고 2개 있다고 하고 끝냈다
        그런데 문제에서 요구한 것은 b'ana'에서 한개, 그리고 ban'ana' 2개, 뒤도 마찬가지로 해서 총 4개 있다고 요구했다.
    그래서 해당 조건에 맞춰 풀었다

    주된 아이디어는 패턴의 문자마다 해당 단계까지만 갖고 바로 다음 단계에서 다른 경우 되돌아야 하는 최대 인덱스를 부여하는 것(KMP 알고리즘)과
    투 포인트 알고리즘이다!
        예를들어 ABCDABEABCDABCD라는 문자가 있다고 보자
        ABCD AB E ABCD ABCD로 나눠서 보자
        (인덱스는 0번에서 시작한다!)

        A'B'에서 B != A이고, AB'C' 에서 C != A이다
        마찬가지로 ABC'D' 에서 D != A이다
        그래서 해당 단계에서 다른경우 맨 처음으로 가야한다고 되돌아가야하는 인덱스 값을 0으로 준다
        
            ABCD
            0000

        이제 ABCD'A'를 알아보자 해당 4번 인덱스의 A는 0번 인덱스의 A와 같다
        그래서 A까지 같다면 다음 검사해야할껀 B인지 검색해야한다
        그래서 4번 인덱스에는 1이 담긴다
            ABCD A
            0000 1

        그리고 ABCDA'B' 즉, 5번 인덱스의 'B'는 4번 인덱스의 A에 영향을 받는다!
        4번 인덱스가 1번을 가리키므로 1번 인덱스 'B'와 비교하면 된다 5번 인덱스와 1번 인덱스가 같으므로
        5번 인덱스 'B'에는 1(4번 인덱스의 값) + 1(현재 같다는 의미) = 2가 담기게된다
        즉, 'AB'CD 'AB' AB가 같으니 2번인덱스를 검색하라는 의미이다!
            ABCD AB
            0000 12

        ABCDAB'E' 즉, 6번 인덱스 'E'의 경우 2번 인덱스 'C' 랑 비교한다 그런데, 다르다!
        그러면 1번 인덱스에 담긴 값 0번 인덱스의 값 'A'와 비교한다 E != A이다
            1번 인덱스에 담긴 값을 비교하는 이유는 13번 인덱스 부분에서 다룬다, 여기서는 그냥 그렇다고 넘어가자!

        이는 6번 인덱스에서 다른 경우 맨 처음부터 다시 비교해야 한다
        그래서 6번 인덱스에는 0이 담긴다
            ABCD AB E
            0000 12 0

        이제 7번 인덱스 'A'이다
        앞에 값이 0이므로 0번 인덱스와 비교하는데 같으므로 1이 담긴다
            ABCD AB E A
            0000 12 0 1

        8번인덱스도 6번 인덱스처럼 하고, 9, 10, 11, 12번 인덱스도 비슷한 방법으로 하면
        다음과 같이 채워진다
            ABCD AB E ABCDAB
            0000 12 0 123456

        이제 ABCDABEABCDAB'C' 즉, 13번 인덱스 'C'를 보자
        앞에서 6번 인덱스의 값을 가지므로 6번 인덱스의 값 'E'와 비교한다
        이는 문자열이 'ABCDAB' E 'ABCDAB' C에서
        0, 1, 2, 3, 4, 5번 인덱스의 값과 7, 8, 9, 10, 11, 12번 인덱스의 값이 같아서 13번 인덱스 C와 6번 인덱스 E를 비교한다는 의미이다!
        그런데 C != E이다
        그렇지만 11, 12번 인덱스 AB는 맨 앞의 0, 1번 인덱스 AB와 같다
        그래서 2번 인덱스를 탐색하게 가리켜야한다
        이는 5(6 - 1)번 인덱스에 담긴 값이 보관하고 있다
        그래서 2번 인덱스와 13번 인덱스의 값이 같으므로 13번에는 2 + 1 = 3이 담기게된다
            ABCD AB E ABCDAB C
            0000 12 0 123456 3

        마지막 14번 인덱스 D는 앞에 3번 값이 담겨 있으므로 3번 인덱스와 비교하면 된다
        같으므로 3 + 1이 담긴다
            ABCD AB E ABCDAB CD
            0000 12 0 123456 34

        그래서 밑에 인덱스를보관하는 배열로 jump로 뒀다!

    점프를 채우는 방법에 투 포인트 알고리즘이 들어가 있다
        현재 문자열 인덱스를 가리키는 포인터 1개, 그리고 되돌아 가야하는 인덱스를 가리키는 포인터 1개
    for문으로 while문을 대처할 수 있다 이 경우 for문의 i는 curPos이다
    
    이제 문자열 탐색과정을 보면서 jump 배열이 어떻게 쓰이는지 보자
        ABCDABEABCDABCD는 길어 다른 예제를 보자
        주어진 예시로 대체한다
            텍스트 : ABC ABCDAB ABCDABCDABDE
            패턴 : ABCDABD

        패턴의 점프 배열은
            ABCDABD
            0000120

        ABC ABCDAB ABCDABCDABDE
        |
        0
        A
        O

        ABC ABCDAB ABCDABCDABDE
         |
         1
         B
         O

        ABC ABCDAB ABCDABCDABDE
          |
          2
          C
          O

        위와 같은 과정을 거치면서 3번과 공백 문자에서 막힐 것이다
        ABC ABCDAB ABCDABCDABDE
           |
           3
           D
           X

        'D'와 ' '는 다르다 그래서 0= jump[3 - 1]번 인덱스가 가리키는 값 'A'와 같은지 비교한다
        처음 원소와 다르므로 다음으로 넘어간다
        ABC ABCDAB ABCDABCDABDE
           |
           0 = jump[3 - 1]
           A
           X

        
        'A'와 다르므로 해당 문자는 시작지점이 될 수 없어 넘긴다
        ABC ABCDAB ABCDABCDABDE
            |
            0
            A
            O

        다시 탐색을 진행하면 다음과 같고 6에서 ' ' != 'D'이므로 막힌다
        ABC ABCDAB ABCDABCDABDE
             ||||||
             123456
             BCDABD
             OOOOOX

        그러면 jump[6 - 1] = 2번 인덱스의 값 'C'와 ' '비교한다
        ABC ABCDAB ABCDABCDABDE        
                  |
                  2
                  C
                  X

        그리고 jump[2 - 1] = 0번 인덱스랑 비교한다
        ABC ABCDAB ABCDABCDABDE        
                  |
                  0
                  A
                  X

        0번 인덱스와 다르므로 해당 텍스트의 문자는 시작점이 될 수 없고 다음 문자로 넘어간다
        다시 0번부터 다시 비교해 간다
        ABC ABCDAB ABCDABCDABDE
                   |||||||
                   0123456
                   ABCDABD
                   OOOOOOX

        그러면 다시 텍스트의 18번째 'C'에서 막힐 것이다
        그러면 jump[6 - 1] = 2번 인덱스의 값 'C'와 텍스트의 18번째 'C'를 비교한다
        같으면 다음을 의미한다 
        ABC ABCDAB ABCDABCDABDE
                       |||
                       012
                       ABC
                       OOO

        그리고 다시 탐색해간다
        ABC ABCDAB ABCDABCDABDE
                       |||||||
                       0123456
                       ABCDABD
                       OOOOOOO

        문자열의 길이만큼 O가 나왔으므로 결과값 1개를 추가한다
        
        그리고 다음 탐색은 다시 0번부터 하는게 아닌! 문제조건에 맞춰
        jump[6] = 0번이 가리키는 값으로 돌아와야한다
        해당 예제는 다시 돌아오는게 안보인다
        ABC ABCDAB ABCDABCDABDE
                              |
                              0
                              A
                              X
        

    또 다른 예로 
        텍스트 : banana
        패턴 : ana를 보자

        ana의 점프 배열은
            ana
            001

        패턴의 0번에서 다르므로 텍스트의 다음 문자로 간다
        banana
        |
        0
        a
        X

        텍스트의 a부터 시작하면 다음과 같다
        banana
         |||
         012
         ana
         OOO

        ana를 하나 찾았다 그러면 다음 탐색은 jump[2] = 1번 인덱스부터 맞는지 확인해야 한다
        이를 표현하면 다음과 같다
        banana
           ||
           an
           01
           OO

        banana
             |
             a
             2
             O

    그리고 출력형태에 맞춰 다 같은 경우 해당 인덱스를 기록하고 출력형태에 맞춰 변형해서 아래와 같은 코드가 들어가 있다
    마찬가지로 문자열 탐색에도 투 포인터 알고리즘이 들어가 있다
        텍스트를 가리키는 포인터 1개(해당 포인터는 for문), jump를 가리키는 포인터 1개(따로 변수 둬서 for문 안에서 돌고 있다)
*/

namespace BaekJoon._42
{
    internal class _42_01
    {

        static void Main1(string[] args)
        {

            string text, pattern;
            int[] jump;
            List<int> ret;

            Solve();
            void Solve()
            {

                Input();

                jump = SetPattern(pattern);
                KMP(text, pattern, jump, ret);

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{ret.Count}\n");

                for (int i = 0; i < ret.Count; i++)
                {

                    sw.Write($"{ret[i] + 1} ");
                }

                sw.Close();
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                text = sr.ReadLine();
                pattern = sr.ReadLine();

                ret = new();
                sr.Close();
            }

            int[] SetPattern(string _pattern)
            {

                int backPos = 0;
                int[] jump = new int[_pattern.Length];

                for (int curPos = 1; curPos < _pattern.Length; curPos++)
                {

                    while (backPos > 0 && _pattern[backPos] != _pattern[curPos])
                    {

                        backPos = jump[backPos - 1];
                    }

                    if (_pattern[backPos] == _pattern[curPos]) jump[curPos] = ++backPos;
                }

                return jump;
            }

            void KMP(string _text, string _pattern, int[] _jump, List<int> _ret)
            {

                int match = 0;
                _ret.Clear();

                for (int i = 0; i < _text.Length; i++)
                {

                    while (match > 0 && _text[i] != _pattern[match])
                    {

                        match = _jump[match - 1];
                    }

                    if (_text[i] == _pattern[match])
                    {

                        match++;

                        if (match == _pattern.Length)
                        {

                            _ret.Add(i - match + 1);
                            match = _jump[match - 1];
                        }
                    }
                }
            }
#if first
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            string text = sr.ReadLine();

            string pattern = sr.ReadLine();

            sr.Close();

            // 투 포인트로 조사한다;?
            int len = pattern.Length;
            int[] jump = new int[len];

            // 문자열 매칭이 안될 때 되돌아가야하는 인덱스 세팅
            int curPos = 1;
            int back = 0;
            /*
             
            // 해당 부분 더 보기 좋게 작성
            while(curPos < len)
            {

                if (pattern[back] == pattern[curPos])
                {

                    jump[curPos++] = ++back;
                }
                else if (back > 0)
                {

                    back = jump[back - 1];
                }
                else
                {

                    // jump의 초기값이 0이 아닌 경우면 해당 코드를 넣어줘야한다!
                    // jump[curPos] = 0;
                    curPos++;
                }
            }
            */
            while (curPos < len)
            {

                while(true)
                {

                    if (pattern[back] == pattern[curPos])
                    {

                        back++;
                        break;
                    }

                    if (back == 0) break;
                    back = jump[back - 1];
                }

                jump[curPos++] = back;
            }

            int chk = 0;

            Queue<int> q = new Queue<int>();
            // 문자열 탐색
            for (int i = 0; i < text.Length; i++)
            {

                if (text[i] == pattern[chk]) chk++;
                else 
                { 
                    
                    while(chk != 0)
                    {

                        if (text[i] == pattern[jump[chk - 1]])
                        {

                            chk = jump[chk - 1];
                            chk++;
                            break;
                        }

                        chk = jump[chk - 1];
                    }
                }

                if (chk == len) 
                { 

                    chk = jump[chk - 1];
                    q.Enqueue(i - len + 2);
                }
            }


            using (StreamWriter sw = new StreamWriter(Console.OpenStandardOutput()))
            {

                sw.WriteLine(q.Count);

                while(q.Count > 0)
                {

                    sw.Write(q.Dequeue());
                    sw.Write(' ');
                }
            }
#endif
        }
    }
}
