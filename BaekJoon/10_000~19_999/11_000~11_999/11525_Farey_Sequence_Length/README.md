# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 수학
  - 정수론
  - 누적 합
  - 오일러 피 함수

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
Given a positive integer, N, the sequence of all fractions a / b with (0 < a ≤ b), (1 < b ≤ N) and a and b relatively prime, listed in increasing order, is called the Farey Sequence of order N.<br/>
For example, the Farey Sequence of order 6 is:<br/>
0/1, 1/6, 1/5, 1/4, 1/3, 2/5, 1/2, 3/5, 2/3, 3/4, 4/5, 5/6, 1/1<br/>
For this problem, you will write a program to compute the length of the Farey sequence of order N (input).<br/>


## 입력
The first line of input contains a single integer P, (1 ≤ P ≤ 10000), which is the number of data sets that follow. Each data set should be processed identically and independently.<br/>
Each data set consists of a single line of input. It contains the data set number, K, followed by the order N, N (2 ≤ N ≤ 10000), of the Farey Sequence whose length is to be found.<br/>


## 출력
For each data set there is a single line of output. The single output line consists of the data set number, K, followed by a single space followed by the length of the Farey Sequence as a decimal integer.<br/>


## 예제 입력
4<br/>
1 6<br/>
2 15<br/>
3 57<br/>
4 9999<br/>


## 예제 출력
1 13<br/>
2 73<br/>
3 1001<br/>
4 30393487<br/>


## 풀이
백준 문제 홈페이지에서는 문자 ≤ 가 깨져서 기존 시험 문제를 찾아봤다.<br/>


문제에서 요구하는 것은 p/q인 서로 다른 값의 갯수를 찾는 문제다.<br/>
만약 arr[i] = val를 ? / i 를 기약 분수로 만들었을때 분모의 값이 i인 가능한 ? 갯수를 val라고 하자.<br/>
그러면 n에 대해 우리가 찾는 값은 i ≤ n인 i에 대해 ∑arr[i]가 된다.<br/>
이는 ? / i가 기약분수로 만들었을 때 분모가 i인 경우는 ?와 i의 최대공약수가 1인 경우와 동치이다.<br/>
그리고 갯수는 오일러 피함수 φ(i)의 값과 같다.<br/>
그래서 arr[i] = φ(i)가 된다.<br/>
범위가 1만으로 작아 오일러 피함수 정의에 맞춰 찾았다.<br/>


그리고 누적합을 이용해 ret[n] = ∑arr[i]배열에 저장했다.<br/>
이후에는 쿼리가 주어지면 해당 ret배열의 값을 O(1)에 확인하며 제출했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/11525