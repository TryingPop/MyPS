# C#

## 난이도 : 골드 3

## 알고리즘 분류
  - 자료 구조
  - 그리디 알고리즘
  - 정렬
  - 누적합

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
Have you ever heard of Just Odd Inventions, Ltd.? This company is known for their “just odd inventions.” We call it JOI, Ltd. in this problem. JOI, Ltd. has invented its newest product “Just Long Neckties”. There are N + 1 types of neckties, numbered 1 to N + 1. The length of the i-th necktie (1 ≤ i ≤ N + 1) is Ai.<br/>
The company gathered their employees to hold a try-on party. N employees participate in the party, and the j-th employee (1 ≤ j ≤ N) initially wears a necktie of length Bj.<br/>
The try-on party is held following this procedure:<br/>
  1. CEO of JOI, Ltd. chooses a necktie, which is not used at the party.
  2. Then, each employee chooses one of the remaining neckties to try on. No two employees choose the same necktie.
  3. Finally, each employee takes off the necktie which (s)he initially wears and puts the selected necktie on.

If an employee initially wearing a necktie of length b tries a necktie of length a, (s)he feels strangeness of max{a − b, 0}. The oddity of the try-on party is defined as the maximum strangeness among the employees.<br/>
We also define Ck as the minimum oddity of the try-on party if CEO of JOI, Ltd. chooses the k-th necktie.<br/>
Write a program which, given the lengths of the neckties used at the party and the neckties each employee initially wears, calculates the values of C1,C2, . . . ,CN+1.<br/>

## 입력
Read the following data from the standard input. Given values are all integers.<br/>

	N
	A1 . . . AN+1
	B1 . . . BN

## 출력
Write one line to the standard output. The output should contain the values of C1,C2, . . . ,CN+1, separated by a space.<br/>

## 제한
  - 1 ≤ N ≤ 200 000.
  - 1 ≤ Ai ≤ 1 000 000 000 (1 ≤ i ≤ N + 1).
  - 1 ≤ Bj ≤ 1 000 000 000 (1 ≤ j ≤ N).

## 예제 입력
3<br/>
4 3 7 6<br/>
2 6 4<br/>

## 예제 출력
2 2 1 1<br/>

## 힌트
Here is an example of a try-on party:<br/>
  - CEO of JOI, Ltd. chooses the 4th necktie. This necktie is not used in the party.
  - The employee 1 chooses the 1st necktie, the employee 2 chooses the 2nd necktie, the employee 3 chooses the 3rd necktie.
  - Each employee tries the necktie they choose on.

In this case, strangeness of each employee is 2, 0, 3 in order. Therefore, the oddity of the party is 3. It is possible to decrease the oddity to 1 if the employees choose different neckties. One of the example is:<br/>
  - CEO of JOI, Ltd. chooses the 4th necktie. This necktie is not used in the party.
  - The employee 1 chooses the 2nd necktie, the employee 2 chooses the 3rd necktie, the employee 3 chooses the 1st necktie.
  - Each employee tries the necktie they choose on.

In this case, strangeness of each employee is 1, 1, 0 in order. Therefore, the oddity of the party is 1. This is the minimum possible oddity when CEO of JOI, Ltd. chooses the 4th necktie, so C4 = 1.<br/>

## 풀이
상사가 넥타이를 1개 먼저 선택하고 남은 n명의 사원이 n개의 넥타이를 선택할 때,<br/>
이상함이 최소가 되는건 다음과 같다.<br/>
ai를 i번째로 짧은 넥타이를 입은 넥타이 길이라하고, bi를 i번째로 짧은 선택할 넥타이 길이라 하자.<br/>
그러면 ai 를 입은 사람이 bi를 택하는게 이상함이 가장 작다.<br/>
만약 a1 - b2를 택했다고 가정하자 그러면 a1 <= a2이고, b1 <= b2가 자명하다.<br/>
대소 관계로 a1 - b1 <= a1 - b2이고 동시에 a2 - b2 <= a1 - b2가 성립한다.<br/>
이는 이상함의 최대값이 증가할 수도 있다.<br/>
반면 a2 - b1 <= a1 - b1과 a2 - b2 <= a2 - b2이기에 최대값이 증가함에 영향을 안끼친다.<br/>
이렇게 n명의 사원과 n개의 넥타이를 매칭시키면된다.<br/>


또한 상사가 각 n에 대해 넥타이를 선택할 때 이상함의 최대값을 각각 찾아야한다.<br/>
내림차순 정렬된 순서에 대해 생각하자 i 번째까지 택하고 i + 1 번째를 택한다고 보면,<br/>
왼쪽은 1번에서 i - 1 번째까지의 최대값이고 여기에 i 번째를 추가해 비교하면 된다.<br/>


오른쪽도 비슷하게 내림차순으로 정렬된 순서 i 번째를 택하고 i - 1번째를 보면,<br/>
오른쪽은 n + 1번에서 i + 1번째까지의 최대값을 알고 i 번째를 추가해 비교하면 된다.<br/>
이는 누적합 개념을 이용하면 상사가 선택에 따라 찾는 시간을 O(1)에 최대 이상함을 찾을 수 있게 한다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/18431