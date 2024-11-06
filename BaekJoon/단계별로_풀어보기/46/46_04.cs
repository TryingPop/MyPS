using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/*
날짜 : 2024. 2. 5
이름 : 배성훈
내용 : 버블 소트
    문제번호 : 1517번

    세그먼트 트리를 이용한 풀이
    ... 어떻게 써야할지 몰라서 찾아봤다
    세그먼트 트리의 과정을 참고한 사이트 https://loosie.tistory.com/328
    
    다음 배열 4 3 1 2의 버블 정렬 과정을 보며 버블 정렬 아이디어를 상기하자
        1번째 원소와 2번째 원소의 크기를 비교한다 즉, 4 3을 비교한다 이 중 큰 것을 뒤로 보낸다
        그래서 4 3 1 2 -> 3 4 1 2
        그리고 2번째 원소와 3번째 원소의 크기를 비교하고 큰 것을 뒤로 보낸다
        그래서 3 4 1 2 -> 3 1 4 2
        다음으로 3번째 원소와 4번째 원소의 크기를 비교하고 큰 것을 뒤로 보낸다
        그래서 3 1 4 2 -> 3 1 2 4
        총 3회 스왑이 일어났다
    
        그리고 다시 1번째 원소로 돌아와서 1번째와 2번째 원소의 크기를 비교한다 즉 3 1을 비교하고 큰 것을 뒤로 보낸다
        그래서 3 1 2 4 -> 1 3 2 4
        그리고 2번째 원소와 3번째 원소의 크기를 비교하고 큰 것을 뒤로 보낸다
        그래서 1 3 2 4 -> 1 2 3 4
        총 2회 스왑이 일어났다
    
        그리고 다시 1번째 원소로 돌아와서 1번째 원소와 2번째 원소를 비교하고 큰 것을 뒤로 보낸다
        그래서 1 2 3 4 -> 1 2 3 4
        0회 스왑이 일어났다

        모든 탐색을 끝냈으므로 버블 정렬이 종료된다
        스왑이 일어난 횟수는 3 + 2 = 5이다

    초기 위치에서 정렬될 때까지 이동 정도를 초기 위치를 이용해 이동 횟수를 보자
    1번의 경우 자기보다 큰 원소가 앞에 4, 3 2개가 존재하고 + 뒤에 자기보다 작은 원소의 개수가 0개 존재해 총 2회이동한다
    2번의 경우 앞에 자기보다 큰 원소 3, 4가 2개 존재하고 + 뒤에 자기보다 작은 원소의 개수 0개가 존재해 총 2회 이동한다
    3번의 경우 앞의 자기보단 큰 원소 4가 1개 존재하고 + 이후 3번 뒤의 자기보다 작은 원소 2개 총 3회 이동한다
    4번의 경우 가장 큰 원소이므로 앞의 원소 중 자기보다 큰 것은 없고 해당 위치에서 작은 원소들의 개수 3회 만큼 이동했다

    해당 횟수를 모두 더하면 10회이다 그리고 한 번 스왑에 2개의 원소가 바뀌므로 이는 2로 나누면 같아진다

    다른 배열 1 3 2 4을 버블소트하는 걸 보자
        1 3 2 4 -> 1 2 3 4 (1회)
    
    1번의 경우 앞에 자기보다 큰 원소가 없고 뒤에 자기보다 작은 원소가 없어 0회 이동
    2번의 경우 앞에 자기보다 큰 원소 3이 1개 존재하고, 뒤에 자기보다 작은 원소는 없어 1회 이동
    3번의 경우 앞에 자기보다 큰 원소 없고, 뒤에 자기보다 작은 원소 2가 존재 1회 이동
    4번의 경우 뒤에 원소가 없으므로 0회 이동

    총 2 / 2 = 1회 이동한다

    그리고 조금만 더 생각해보면 a가 b보다 크다는 b는 a보다 작다로 볼 수 있기에
    앞의 큰 원소만 조사해도 된다!

    그래서 배열이 주어지면,
    앞에 자기보다 작은 원소들의 수를 찾아나가면 된다

    그런데 값을 내림 차순으로 나열하고, 
    앞에 자기보다 큰 원소의 수를 찾아 기록하면 빠르게 찾을 수 있다
    앞의 자기보다 큰 원소의 수를 찾아 기록하는데 세그먼트 트리가 이용된다!

    4 3 1 2를 놓고 보자
    가장 큰 수 4의 경우 자기보다 큰 원소는 없으므로 큰 원소를 조사할 필요가 없다
    그리고 가장 큰원소의 위치(인덱스)에 1을 기록한다
        0 1 2 3     <- idx
        1 0 0 0     <- dp value
        4 3 1 2     <- arr value
    다음 큰 수의 값보다 큰 값이 해당 idx에 1개 존재한다는 의미로 1을 기록한 것이다

    그리고 2번째로 큰 수 3의 위치를 확인하고
    앞의 원소에 대해 해당 값보다 큰게 존재하는지 확인해야한다!
    실제로, 
        0 1 2 3     <- idx
        1 0 0 0     <- dp value
        4 3 1 2     <- arr value
    
    1번 인덱스보다 작은 0에 1이 기록되어져 있다 (0번 arr 값 4는 1번 arr 값 3보다 크다)
    즉, swap이 1번 되었고 결과에 1회 추가한다

    그리고 2번째로 큰 수의 위치에 1을 추가로 기록한다
        0 1 2 3     <- idx
        1 1 0 0     <- dp value
        4 3 1 2     <- arr value

    다음 3번째로 큰 수 2의 위치를 확인하고
    앞의 원소에 대해 해당 값보다 큰게 존재하는지 확인해야한다
    3번 앞에 1이 0과 1에 기록되어져 있다
    총 2개이다 그래서 swap이 추가로 2회 더 발생다고 볼 수 있다

    그리고 3번의 위치에 1을 기록한다
        0 1 2 3     <- idx
        1 1 0 1     <- dp value
        4 3 1 2     <- arr value

    다음 1번째로 큰 수 1의 위치를 확인하고
    앞의 원소에 대해 해당 값보다 큰게 존재하는지 확인한다
    idx 0, 1번을 보면 2개 존재한다
    그래서 swap이 추가로 2번 더 발생했다고 볼 수 잇다
    그리고 dp에 1을 기록한다
        0 1 2 3     <- idx
        1 1 1 1     <- dp value
        4 3 1 2     <- arr value
    더 이상의 원소는 없고
    총 5회 일어났다고 본다 앞의 예제 결과랑 일치한다!

    내림 차순을 이용하면 다음과 같은 dp로 1을 기록하며 찾을 수 있게 된다
    여기서 끝나면 메모리 N만 더사용할 뿐이다!

    그런데 dp를 세그먼트 트리형태로 이용하면 0 ~ 현재 idx - 1까지의 합을 찾으면 1들의 합을 logN의 시간으로 줄일 수 있다!
    해당 코드는 전처리에 else부분과 같다
    (그리고 전처리 first부분은 뒤에 작은 원소들에 초점을 맞춰 푼 형식이다)
*/

namespace BaekJoon._46
{
    internal class _46_04
    {

        static void Main4(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            (int idx, int value)[] arr = new (int idx, int value)[len];
            for (int i = 0; i < len; i++)
            {

                // 10억에 50만개까지 들어올 수 있어서 쪼개서 입력받는다
                int num = 0;
                int c;
                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    num *= 10;
                    num += c - '0';
                }
                arr[i] = (i, num);
            }

            sr.Close();

#if first
            // value에 맞춰 오름차순 value가 같은 경우 인덱스가 작은 것을 먼저 나오게 한다
            Array.Sort(arr, (x, y) =>
            {

                int result = x.value.CompareTo(y.value);
                if (result == 0) result = x.idx.CompareTo(y.idx);
                return result;
            });
#else

            // value에 맞춰 내림차순, value가 같은 경우 인덱스가 큰 것을 앞에 있게 한다
            // 인덱스가 같은 경우 swap을 안해야 한다!
            Array.Sort(arr, (x, y) =>
            {

                int result = y.value.CompareTo(x.value);
                if (result == 0) result = y.idx.CompareTo(x.idx);
                return result;
            });
#endif

            // 세그먼트 트리 제작
            int log = (int)Math.Ceiling(Math.Log2(len)) + 1;
            int size = 1 << log;

            // Seg
            int[] seg = new int[size];
            // 1 + 2 + 3 + ... + 499_999 = (500_000 * 499_999) * 0.5 > 25 만 * 25만 = 625억 > int.MaxValue
            long result = 0;
#if first
            
            for (int i = 0; i < len; i++)
            {

                // 뒤에 작은 것들의 원소를 센다
                // 해당 개수가 swap이 일어난 횟수와 같다
                int idx = arr[i].idx;
                result += GetValue(seg, 0, len - 1, idx + 1, len - 1);
                ChangeArr(seg, 0, len - 1, idx, 1);
            }
#else

            for (int i = 0; i <len; i++)
            {

                // 앞에 큰 것의 원소를 센다
                // 해당 개수가 swap이 일어난 횟수와 같다
                int idx = arr[i].idx;
                result += GetValue(seg, 0, len - 1, 0, idx - 1);
                ChangeArr(seg, 0, len - 1, idx, 1);
            }
#endif
            Console.WriteLine(result);
        }

        static void ChangeArr(int[] _seg, int _start, int _end, int _changeIdx, int _changeValue, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = _changeValue;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_changeIdx > mid) ChangeArr(_seg, mid + 1, _end, _changeIdx, _changeValue, _idx * 2 + 1);
            else ChangeArr(_seg, _start, mid, _changeIdx, _changeValue, _idx * 2);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int GetValue(int[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_start > _chkEnd || _chkStart > _end) return 0;
            else if (_start >= _chkStart && _end <= _chkEnd) return _seg[_idx - 1];

            int mid = (_start + _end) / 2;
            int l = GetValue(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            int r = GetValue(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);

            return l + r;
        }
    }
}
