using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 23
이름 : 배성훈
내용 : 오큰수
    문제번호 : 17298번

    히스토그램으로 설명하면 <0>, <1>, <2>, <3>, <4>, <5>, <6> 모두 쉽게 설명된다...
    순수히 글로만 적으니 매우 길어졌다;

    <0>. 바로 뒤에 원소가 없는 경우
        즉 마지막 원소의 오큰수는 항상 -1
    
    /// 이후 증가하는 경우 감소하는 경우를 생각했다

    <1>. arr이 순 증가하는 경우
        즉, 모든 i에 대해 arr[i] < arr[i + 1]랑 같은 말이다
        그러므로 i번째의 오큰 수는 arr[i + 1]이 된다
        단, 마지막 항은 -1이다

        이 말은 arr[i] < arr[i + 1]이면 i번째 오큰 수는 arr[i + 1]이다
        이 경우 오큰수는 증가하는 경우에서 큰값이 된다.

    <2>. arr이 감소하는 경우
        즉, i < j 에대해 arr[i] >= arr[j]이다
        그러면 arr[i] 값이 i, i + 1, i + 2, i + 3, ... 에서 가장 큰 수이므로 
        모든 i에 대해 오큰수는 -1이다 

    /// 여기서 오큰수를 갖는 값이 제한되어져 있을지 추론했다

    <3>. arr이 감소하다가 마지막에만 증가하는 경우
        즉, 0에서 end - 1까지 감소하다가 end - 1 에서 end인 경우만 증가하는 경우
    
        부등식으로 표현하면 다음과 같다
        arr[0] <= arr[1] <= arr[2] <= arr[3] <= ... <= arr[end - 2] <= arr[end -1]
        arr[end - 1] < arr[end]

        그러면 <0>에의해 end 의 오큰수는 -1,
        <1>에 의해 end - 1의 오큰수는 arr[end]가 된다
    
        0, 1, 2, ..., end - 2의 오큰수를 찾으면 된다
    
        상황을 (1), (2)로 분할해서 보자

            (1) 0, 1, 2, ..., end - 2 사이에 arr[a1 - 1] > arr[end] > arr[a1]인 a1이 하나도 존재하지 않는다는 경우,
            이말은 모든 0, 1, 2, ..., end - 2 사이 값인 b1에 대해 arr[b1] >= arr[end]라는 말과 동형이고,
        
            arr이 0, 1, 2, ..., end - 1 사이에서 감소이므로 arr[b1] >= arr[b1 + 1] >= arr[b1 + 2] >= ...>= arr[end - 2] >= arr[end - 1] 이므로
            b1의 오른쪽에는 arr[b1]보다 큰 수가 존재하지 않음을 알 수 있다

            따라서 이 경우 0, 1, 2, ..., end - 2의 오큰수는 모두 -1이다!

            (2) 0, 1, 2, ..., end - 2 사이에 적당한 a1 값이 적어도 1개 존재해
            arr[end] > arr[a1]이라 가정하자

            그러면 집합 A := { a | 0 <= a <= end - 2 and arr[end] > arr[a] }라 정의하면(:= 는 새롭게 정의한다는 약어?)
            가정에 의해 A는 공집합이 아닌 자연수의 부분집합이다!
            자연수의 정렬성에의해 A의 최소값이 존재한다(대학수학......)

            b1을 A의 최소가 되는 인덱스라 하자!
                만약 b1 = 0인 경우 arr이 0, 1, 2, ..., end - 1 에서 감소이므로
                arr[end] > arr[b1] = arr[0] >= arr[1] >= arr[2] >= ... arr[end - 1]이 성립하고
                0, 1, 2, ... end - 2의 각 항에 대해 오른쪽에 유일하게 큰 수 arr[end]가 존재한다
                따라서 0, 1, 2, ... end - 2의 오큰수는 arr[end]이다
        
                b1 != 0인 경우 
                b1이 최소값이고 감소수열이므로
                arr[b1 - 1] >= arr[end] > arr[b1]으로 표현 가능하다

                arr이 0, 1, 2, ..., end - 1 에서 감소하므로
                arr[0] >= arr[1] >= arr[2] >= ... >= arr[b1 - 1] >= arr[end] > arr[b1] >= arr[b1 + 1] >= ... >= arr[end - 1] 부등식이 된다
    
                그러면 b1, b1 + 1, ..., end - 1 항에서는 오른쪽에 자신보다 큰 arr[end]이 유일하게 존재하고,
                0, 1, 2, ..., b1 - 1 의 항에 대해서는 오른쪽에 자신보다 큰 수는 존재하지 않는다

                따라서 b1, b1 + 1, b1 + 2, ..., end - 2의 오큰수는 arr[end]이고
                0, 1, 2, ..., b1 - 1의 경우 해당 수보다 큰 수가 오른쪽에 없기에 오큰수는 -1이 된다
    
        (1), (2) 이외의 상황은 존재할 수 없다
        오큰수를 보면 -1 또는 증가하는 경우 큰값이 된다

    <4>. arr이 감소하다가 증가하는 경우
        끝항을 1개 더 늘려서 arr[end -1] < arr[end] < arr[end + 1] 이고 0, 1, 2, ... end - 1에서는 감소하는 경우를 보자
        end + 1 항은 <0>에 의해 오큰수가 -1
        end - 1, end 항은 <1>에의해 오큰수가 각각 arr[end], arr[end + 1]이된다

        그리고 나머지 0, 1, 2, ..., end - 2는

        앞과 마찬가지로 0, 1, 2, ..., end - 2에서 
        arr[a1 + 1] >= arr[end] > arr[a1],
        arr[a2 + 1] >= arr[end + 1] > arr[a2]인 서로 다른 a1, a2가 존재한다고 가정하자

        b1을 A1 = { a | 0 <= a1 <= end - 1 and arr[end] > arr[a] } 조건을 만족하는 최소값이라 하고
        b2는 A2 = { a | 0 <= a2 <= end - 1 and arr[end + 1] > arr[a] } 조건을 만족하는 최소값이라 하자(존재성 보장은 <3>과 같다)
        <3>의 이유와 마찬가지로 a1, a2 대신 서로 다른 b1, b2로 놓고 풀어도 된다(b1, b2가 같은 경우면 <3>에서 확인했다)

        arr[end] < arr[end + 1] 이므로
        arr[b2 + 1] >= arr[end + 1] > arr[end] > arr[b1]이고
        arr[b2 + 1] > arr[b1]이다

        그리고 arr이 0, 1, 2, ... end - 1에서 감소하므로 b2 + 1 > b1이 된다
        즉, b2 >= b1이다 그런데 b1 != b2이므로 b2 > b1이 성립한다

        arr 이 0, 1, 2, ..., end - 1에서는 감소하므로
        arr[0] >= arr[1] >= arr[2] >= ... >= arr[b2 - 1] >= arr[end + 1] 
            > arr[b2] >= arr[b2 + 1] >= ... >= arr[b1 - 1] >= arr[end] 
            > arr[b1] >= arr[b1 + 1] >= ... >= arr[end - 1]
        이 성립한다
    
        그러면 0, 1, 2, ..., b2 - 1 항에 대해서는 오른쪽에 자신보다 큰 수는 존재하지 않아 오큰수는 -1이다
        b2, b2 + 1, b2 + 2, ..., b1 - 1항에 대해서는 오른쪽에 자신보다 큰 수는 arr[end + 1]이 유일하므로 오큰수는 arr[end + 1]이다
        마지막으로 b1, b1 + 1, b1 + 2, ..., end -1항에 대해서 오른쪽에 자신보다 큰 수는 arr[end + 1], arr[end] 뿐이고, 가장 왼쪽에 있는건 arr[end]이므로 오큰수는 arr[end]이다

        이렇게 귀납적으로 하나씩 늘려가며 적용하고 수학적 귀납법을 이용하면 해당 상황에 오큰수들을 모두 찾을 수 있고
        모든 오큰수는 증가하는 경우 큰값이거나 -1임을 알 수 있다

    <5>. arr이 증가하다가 감소하는 경우를 보자
        arr[0] < arr[1] < arr[2]이고
        2, ... , end까지는 감소한다고 가정하자
    
        <1>에 의해 0, 1의 값은 각각 arr[1], arr[2]이다
        그리고 2의 경우 <2>에 의해 -1이된다
        마찬가지로 오큰수는 증가하는 경우 큰값이거나 -1이다

    arr이 유한 수열이므로 arr은 항이 인접한 순 증가 수열이나 감소수열로 분할이 가능하다
    즉 <0>, <1>, <2>, <3>, <4>, <5>을 적절히 섞어 모든 수열을 표현할 수 있다
    
    따라서 오큰수는 -1 또는 증가하는 경우 큰값이 오큰수가 된다

    여기까지만 수학적으로 풀고 뒤에서 탐색하며 증가하는 경우 큰값만 보존하며 연산을 진행했다
    현재는 주석 처리된 calc 스택 사용하는 코드부분이다
    이 경우 순 감소하는 수열인 경우에 한해 시간 복잡도가 O(N^2)이 되어 시간초과가 나타났다

    이에 주어진 조건을 빠뜨리지 않았나 의문을 가지며 
    <0> <1> <2> <3> <4> <5>를 섞어 <6>의 경우를 만들었다

    <6> arr이 감소 증가 감소 증가 하는 경우
        idx = { 0, 1, 2, 3, 4, 5, 6, 7, 8 }
        arr = { 8, 4, 2, 5, 7, 5, 1, 3, 9 }
        0, 1, 2에서 감소
        2, 3, 4에서 증가
        4, 5, 6에서 감소
        6, 7, 8에서 증가하게 했다

        3, 4, 7, 8항에서 갖는 값인
        5, 7, 3, 9과 -1이 오큰수 집합이다
        
        실제로 0, 1, 2, ... , 8항에 대해 오큰수는
        9, 5, 5, 7, 9, 6, 3, 9, -1이다
        
        여기서 정방향 기준으로 감소 증가 하는 연속인 부분수열로 나눌 수 있어 보인다 
        4번을 기준으로 구간을 나눠보자

        (1) 감소 + 증가
            0, 1, 2, 3, 4 (idx)
            9, 5, 5, 7, 9 (arr)

`       (2) 감소 + 증가
            4, 5, 6, 7, 8 (idx)
            9, 6, 3, 9, -1(arr)

        항이 0, 1, 2, 3, 4인 구간 (1)의 경우를 보자
        구간 (1)에서 오큰수의 최대값은 7이고 7미만인 수를 구간 (1)의 조건에 맞게 넣으면
        구간 (1)의 오큰수 후보로 모두 해결된다
        반면 큰 수는 구간 (2)의 오큰수 후보와 비교해야한다

        즉 이는 감소 증가를 기준으로 두 구간을 나눌 때
        앞의 구간을 (1), 뒤의 구간을 (2)라 하고
        구간 (1)의 최대 오큰수를 a라고 하자
        그러면 a이하인 구간 (2)의 오큰수는 구간 (1)에 영향을 주지 않는다고 결론을 내릴 수 있다

        이는 역으로 탐색할 시 구간 (2)에서 탐색이 끝나고 
        구간 (1)로 갈때 구간 (1)의 오큰수의 최대값 7보다 작은 
        구간 (2)의 오큰수 3는 모두 버려도 되는 것을 찾을 수 있다

    <6>을 <4>에 적용한다면

    증가 감소 하는 항 dec로 잘랐을 때
    감소 시작하는 항을 dec, 끝나는 항을 decEnd, 
    decEnd뒤에 순 증가구간이 있어 증가 시작하는 항을 inc, 끝나는 항을 incEnd라 하자
    incEnd 뒤에 항이 더 있어도 된다

    즉, 항은 dec, dec + 1, ..., decEnd - 1, decEnd = inc, inc + 1, ..., incEnd - 1, incEnd가되고
    arr[dec] >= arr[dec + 1] >= ... >= arr[decEnd - 1] >= arr[decEnd]
    arr[inc] < arr[inc + 1] < ... < arr[incEnd - 1] < arr[incEnd]
    라 놓고 보자

    그러면 dec를 기준으로 잘랐기에 dec앞에 증가하는 항이 존재한다 즉, dec - 1이 존재하고
    arr[dec - 1] < arr[dec] 이고 arr[dec]는 오큰수 후보가 된다
    그리고 inc 인덱스는 증가 시작하는 구간이므로 inc + 1, inc + 2, ... incEnd는 모두 오큰수의 후보가 된다
    <6>에 의해 arr[inc +1], arr[inc + 2], ... arr[incEnd] 중 arr[dec]보다 작은 애는 모두 버린다

    <4> 의 과정을 보면 arr[a1 - 1] >= arr[inc + 1] > arr[a1]인 a1이 dec, dec + 1, ..., decEnd 사이에 존재하면
    감소하므로 arr[dec] >= arr[a1 - 1] >= arr[inc + 1] 이므로
    decEnd, decEnd -1, ..., a1 + 1, a1 과정이 끝난 후 오큰수 후보에서 arr[inc + 1]를 뺀다는 의미이다!

    그리고 arr[a2 - 1] >= arr[inc + 2] > arr[a2]인 a2가 dec, dec + 1, ... decEnd에 존재하면
    감소하므로 arr[dec] >= arr[a2 - 1] >= arr[inc + 2] 이고
    a1 - 1, a1 - 2, ...., a2과정이 끝난후 오큰수 후보에서 arr[inc + 2]를 뺀다

    이렇게 귀납적으로 하면 시간 복잡도가 O(2N)으로 현저하게 줄어든다!
*/

namespace BaekJoon._31
{
    internal class _31_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            int[] input = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();
            int[] result = new int[len];
            Stack<int> temp = new Stack<int>();
            // Stack<int> calc = new Stack<int>();     // 계산용!
            temp.Push(-1);

            result[len - 1] = -1;

            for (int i = len - 2; i >= 0; i--)
            {

                // 앞에께 뒤에꺼보다 작은 경우 값을 공유?
                if (input[i] < input[i + 1])
                {

                    
                    temp.Push(input[i + 1]);
                    result[i] = input[i + 1];
                }
                else if (result[i + 1] == -1)
                {

                    // 뒤에께 -1이면 즉, 뒤에 해당 수보다 큰 게 없다는 의미이므로 
                    result[i] = -1;
                    continue;
                }
                else
                {


                    while (temp.Count > 0)
                    {

                        if(temp.Peek() > input[i])
                        {

                            result[i] = temp.Peek();
                            break;
                        }
                        else if (temp.Peek() == -1)
                        {

                            result[i] = -1;
                            break;
                        }
                        else
                        {

                            // calc에 보관
                            // calc.Push(temp.Pop());
                            temp.Pop();
                        }
                    }

                    // 다시 집어 넣으면 안된다!
                    // 안쓰는건 버린다
                    // while(calc.Count > 0)
                    // {

                        // temp.Push(calc.Pop());
                    // }
                }
            }


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                // 출력
                for (int i = 0; i < len; i++)
                {

                    sw.Write(result[i]);
                    sw.Write(' ');
                }
            }
        }
    }
}
