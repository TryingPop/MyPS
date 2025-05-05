# C#

## 난이도 : 골드 5

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 배낭 문제

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
A composition of an integer n is an ordered set of integers which sum to n. Two compositions with the same elements but in different orders are considered different (this distinguishes compositions from partitions). For example, all the compositions of the first few integers are:<br/>

	1: {1}
	2: {1+1, 2}
	3: {1+1+1, 1+2, 2+1, 3}
	4: {1+1+1+1, 1+1+2, 1+2+1, 1+3, 2+1+1, 2+2, 3+1, 4}


Note that 1+2 and 2+1 each count as distinct compositions of 3. As you may have suspected, there are 2(n-1) compositions of n.<br/>
In this problem, we set conditions on the elements of the compositions of n. A composition misses a set S if no element of the composition is in the set S. For example, the compositions of the first few integers which miss the set of even integers are:<br/>

	1: {1}
	2: {1+1}
	3: {1+1+1, 3}
	4: {1+1+1+1, 1+3, 3+1}

No odd integer can have a composition missing the set of odd integers and any composition of an even integer consisting of only even integers must be 2 times a composition of n/2.<br/>
For this problem you will write a program to compute the number of compositions of an input integer n which miss the elements of the arithmetic sequence {m + i*k | i = 0,1,…}<br/>


## 입력
The first line of input contains a single decimal integer P, (1 ≤ P ≤ 10000), which is the number of data sets that follow. Each data set should be processed identically and independently.<br/>
Each data set consists of a single line of input. It contains the data set number, K, followed by the three space separated integers n, m and k with (1 ≤ n ≤ 30) and (0 ≤ m < k < 30).<br/>


## 출력
For each data set there is one line of output. The single output line consists of the data set number, K, followed by a single space followed by the number of compositions of n which miss the specified sequence.<br/>


## 예제 입력
3<br/>
1 10 0 2<br/>
2 15 1 4<br/>
3 28 3 7<br/>


## 예제 출력
1 55<br/>
2 235<br/>
3 18848806<br/>


## 풀이
dp[i] = val를 i번 수를 만드는 경우의 수라 한다.<br/>
연산을 위해 dp[0] = 1로 한다.<br/>
그러면 뒤에 사용할 수 있는 수를 k라 하면 dp[j] = ∑dp[j - k]를 얻는다.<br/>
해당 경우 뒤에 k가 모두 다르므로 겹치는 경우는 없고 모든 경우를 나타낼 수 있다.<br/>
해당 경우 각 케이스마다 많아야 O(nk)이고 nk ≤ 900이므로 매우 빠르게 풀린다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/11528