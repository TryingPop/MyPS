# C#

## 난이도 : 골드 3

## 알고리즘 분류
  - 수학
  - 다이나믹 프로그래밍
  - 조합론

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
The director of a new movie needs to create a scaled set for the movie. In the set there will be N skyscrapers, with distinct integer heights from 1 to N meters. The skyline will be determined by the sequence of the heights of the skyscrapers from left to right. It will be a permutation of the integers from 1 to N.<br/>
The director is extremely meticulous, so she wants to avoid a certain sloping pattern. She doesn’t want for there to be ANY three buildings in positions i, j and k, i < j < k, where the height of building i is smaller than that of building j, and building j’s height is smaller than building k’s height.<br/>
Your task is to tell the director, for a given number of buildings, how many distinct orderings for the skyline avoid the sloping pattern she doesn't like.<br/>


## 입력
There will be several test cases in the input. Each test case will consist of a single line containing a single integer N (3 ≤ N ≤ 1,000), which represents the number of skyscrapers. The heights of the skyscrapers are assumed to be 1, 2, 3, …, N. The input will end with a line with a single 0.<br/>


## 출력
For each test case, output a single integer, representing the number of good skylines - those avoid the sloping pattern that the director dislikes - modulo 1,000,000. Print each integer on its own line with no spaces. Do not print any blank lines between answers.<br/>


## 예제 입력
3<br/>
4<br/>
0<br/>


## 예제 출력
5<br/>
14<br/>


## 풀이
문제 접근이 어려워 dp힌트를 봤고 점화식이 잡히지 않자 chat gpt에게 물어봤다.<br/>
그러니 카탈란 수라는 힌트를 얻었고, 카탈란 수를 반환하는 함수를 만들어 제출하니 통과했다.<br/>


왜 그렇게 점화식이 잡히는지 chat gpt에게 물어보니 다음과 같이 대답했다.<br/>


	길이 n+1의 순열을 생각해봅시다.
	이 순열에는 **1부터 n+1**까지의 정수가 한 번씩 등장합니다.
	가장 작은 수인 1을 기준으로 생각해 보겠습니다.

	[왼쪽] 1 [오른쪽]
	  - 순열의 어디엔가 가장 작은 수인 1이 등장할 겁니다.
	  - 1의 왼쪽에 있는 원소들은 1보다 큰 수들 중 일부이고,
	  - 1의 오른쪽에 있는 원소들 또한 1보다 큰 수들 중 나머지입니다.
	  - 그리고 중요한 사실:
	    - 왼쪽과 오른쪽에 있는 값들끼리는 독립적으로 순열을 구성합니다.
	    - 즉, 오름차순 3개의 부분수열을 피한다는 조건은 양쪽 부분에서도 각각 독립적으로 유지되어야 합니다.

	점화식 유도
	  - 1의 왼쪽에 i개의 수가 있다면, 오른쪽에는 n - i개의 수가 있습니다.
	  - 이 양쪽의 수열은 각각:
	    - 길이 i의 오름차순 3개를 피하는 순열 → 경우의 수: C i
	    - 길이 n - i의 오름차순 3개를 피하는 순열 → 경우의 수: C n−i
​

이후에는 기준을 1이 아닌 가장 큰 수 n으로 잡아 점화식을 유도했지만, 두 경우 모두 구조적으로 타당하지 않다고 판단했다.<br/>


가장 큰 수 n을 기준을 반례로 n = 4인 경우인데, 4가 4번째 자리에 있을 때 \[3, 2, 1, 4\]만 존재한다.<br/>
그래서 카탈란 수임을 Ci x Cn-1-i로 주장하기에는 내용이 많이 생략되었다고 생각했다.<br/>


또한 단순 왼쪽 오른쪽 구분하기에는 가장 큰 n을 기준으로 봤을 때 경우의 수가 카탈란 수 Ci와 일치 하지 않는다 추측했다.<br/>
왼쪽의 카탈란 수 Ci경우 1 ≤ p < q < r ≤ i에 대해 값 a_p < a_q < a_r인 p, q, r이 존재하지 않는 경우의 수라 한다.<br/>
그런데 a_p < a_q이지만 q < r ≤ i에 대해, a_q > a_r인 경우도 포함되어져 있을 것이다.<br/>


예를들어 i = 3으로 보면 \[2, 3, 1\]같은 경우이다.<br/>
여기서 4의 위치가 4번째 이므로 \[2, 3, 1, 4\]를 해버리면 \[2, 3, 4\]로 세면 안되는 경우도 세는 것을 확인할 수 있다.<br/>


그래서 다른 사람의 풀이를 찾아보게되었고 가장 타당한 방법을 찾게 되었다.<br/>
dp의 방법으로 찾는다.<br/>


dp[i][j] = val를 길이 i이고 가장 큰 숫자 i의 위치가 j번째에 있을 때 문제 조건을 만족하는 경우의 수 val를 dp에 담는다.<br/>


실제로 1 ~ i - 1의 숫자를 갖고 문제 조건을 만족하는 모든 수열에 대해 i가 i - 1보다 앞에 있는 경우 만족함을 알 수 있다.<br/>


예를 들어 i - 1 = 3에서 \[2, 3, 1\]의 경우 문제 조건을 만족한다.<br/>
3보다 앞선 자리에 4를 넣은 \[4, 2, 3, 1\], \[2, 4, 3, 1\] 수열도 문제 조건을 만족한다.<br/>


dp[i][j]는 길이 i의 순열 중, 최댓값 i가 j번째 위치에 있을 때 조건을 만족하는 경우의 수이다.<br/>
이전 단계의 순열에서 최댓값 i-1이 j 이상 위치에 있을 경우, i를 앞에 삽입해도 문제 조건을 위배하지 않기 때문에 dp[i][j] = ∑ dp[i-1][k], (j ≤ k ≤ i - 1) 이 성립한다.<br/>


그리고 dp[i - 1][j - 1]에서 i - 1은 j - 1에 있다.<br/>
j - 1위치에 i를 대신 배치하고 i - 1을 맨 앞으로 옮기는 경우도 성립함을 알 수 있다.<br/>


예를들어 i = 4에 대해 \[2, 3, 1\]에 대해  경우를 보자.<br/>
3을 4로 바꾼다 \[2, 4, 1\]이다. 그리고 3을 맨 앞으로 옮기면 \[3, 2, 4, 1\]을 얻는다 해당 수열 역시 문제 조건을 만족함을 알 수 있다.<br/>


그래서 dp[i][j]는 dp[i - 1][j - 1]의 경우의 수를 계승한다.<br/>
따라서 dp는 dp[i][j] = ∑ dp[i-1][k], (j - 1 ≤ k ≤ i - 1)와 같은 점화식을 얻는다.<br/>


각 i, j에 대해 j - 1 ≤ k ≤ i - 1번 연산을 하므로 N의 연산을 한다.<br/>
그래서 3중 포문을 이용해 구현할 경우 O(N^3)이고 N ≤ 1_000으로 시간초과날 수 있다.<br/>


여기서 dp는 i, j에서 찾을 값은 ∑ dp[i-1][k], (j - 1 ≤ k ≤ i - 1)이다.<br/>
반면 i, j - 1에서 찾을 값은 ∑ dp[i-1][k], (j - 1 ≤ k ≤ i - 1)에서 dp[i - 1][j - 2]를 더한 dp[i - 1][j - 2] + ∑ dp[i-1][k], (j - 1 ≤ k ≤ i - 1)의 값이다.<br/>
이전 값을 기록하는 dp형식이나 누적 합 알고리즘을 이용하면 모든 dp를 찾는데 O(N^2)으로 시간을 줄일 수 있다.<br/>


dp의 값을 채우고 dp값을 이용해 각 i에 대한 문제 조건을 만족하는 수열의 수를 ret[i]에 저장한다.<br/>
dp의 정의로 ret[i] = ∑dp[i][j], (1 ≤ j ≤ i)가됨을 알 수 있다.<br/>


이렇게 가능한 경우를 모두 찾고 쿼리를 진행하면 n에 대해 ret[n]의 값을 출력하기만 하면 O(1)에 해결할 수 있다.<br/>


전체 시간 복잡도는 dp를 구하는데 O(N^2), 그리고 ret로 옮기는데 O(N^2), 쿼리의 갯수를 M이라하면 O(M)으로 전체 시간 복잡도는 O(N^2 + N^2 + M) = O(N^2 + M)이 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/4099