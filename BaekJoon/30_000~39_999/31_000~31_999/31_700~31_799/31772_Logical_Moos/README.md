# C#

## 난이도 : 플레티넘 5

## 알고리즘 분류
  - 구현
  - 누적 합

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 1024 MB

## 문제
Farmer John has a boolean statement that is N keywords long (1 ≤ N < 2 x 10^5, N odd). Only true or false appear in odd positions, while only and and or appear in even positions.
A phrase of the form x OPERATOR y, where x and y are either true or false, and OPERATOR is and or or, evaluates as follows:<br>

  - x  and  y: This evaluates to true if both x and y are true, and false otherwise.
  - x  or  y: This evaluates to true if either x or y is true, and false otherwise.

When evaluating the statement, FJ has to take the order of precedence in Moo Language into account. Similar to C++, and takes priority over or. More specifically, to evaluate the statement, repeat the following step until the statement consists of only one keyword.<br/>

  1. If the statement contains an and, choose any of them and replace the phrase surrounding it with its evaluation.
  2. Otherwise, the statement contains an or. Choose any of them and replace the phrase surrounding it with its evaluation.

It may be proven that if multiple phrases can be evaluated during a given step, it does not matter which one is chosen; the statement will always evaluate to the same value.<br/>
FJ has Q (1 ≤ Q ≤ 2 x 10^5) queries. In each query, he gives you two integers l and r (1 ≤ l ≤ r ≤ N, l and r are both odd), and deletes the segment from keyword l to keyword r inclusive. In turn, he wishes to replace the segment he just deleted with just one simple true or false so that the whole statement evaluates to a certain boolean value. Help FJ determine if it's possible!<br/>

## 입력
The first line contains N and Q.<br/>
The next line contains N strings, a valid boolean statement.<br/>
The following Q lines contain two integers l and r, and a string true or false, denoting whether he wants the whole statement to evaluate to true or false.<br/>

## 출력
Output a string of length Q, where the i'th character is Y if the i'th query is possible, otherwise N.<br/>

## 예제 입력
5 7<br/>
false and true or true<br/>
1 1 false<br/>
1 3 true<br/>
1 5 false<br/>
3 3 true<br/>
3 3 false<br/>
5 5 false<br/>
5 5 true<br/>

## 예제 출력
NYYYNYY<br/>

## 힌트
Let's analyze the first query:<br/>
If we were to replace delete the segment [1, 1] and replace it with true, then the whole statement becomes:

	true and true or true

We evaluate the and keyword from at position 2 and obtain<br/>

	true or true

Since we have no and keywords left, we have to evaluate the or keyword. After evaluation, all that is left is<br/>

	true

It can be shown that if we were to replace the segment with false, the statement will still evaluate to true, so we output N since the statement cannot possibly evaluate to false.<br/>
For the second query, we can replace the segment [1, 3] with true and the whole statement will evaluate to true, so we output Y.<br/>
For the third query, since [1, 5] is the whole statement, we can replace it with anything, so we output Y.<br/>

## 풀이
연산에 우선순위가 and를 먼저 연산하고 이후에 or을 연산 한다.<br/>
그래서 and 연산끼리 그룹을 짓는다.<br/>
and는 피연산자 양쪽이 모두 true여야지만 true를 반환한다.<br/>
만약 false를 하나라도 포함하면 해당 and 영역은 무조건 false를 반환한다.<br/>
그래서 false의 시작, 끝 위치를 그룹별로 저장한다.<br/>
그리고 해당 입력되는 구간이 false영역을 모두 덮지 못하면 해당 and 그룹은 무조건 false이다.<br/>


이후 or 영역도 and처럼 따로 찾는다.<br/>
그리고 or은 피연산자 모두 false여야지만 false를 반환한다.<br/>
그래서 true가 시작, 끝 위치를 찾는다.<br/>
입력되는 구간이 true 영역을 모두 덮지 못하면 해당 반환 값은 무조건 true가 된다.<br/>
이제 영역에 맞춰 정답이 되는지 판별하면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/31418