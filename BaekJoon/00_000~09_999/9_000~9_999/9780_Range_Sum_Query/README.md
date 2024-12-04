# C#

## 난이도 : 실버 4

## 알고리즘 분류
  - 누적 합

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
Given a list L containing n integers, find the Range Sum Query (RSQ) between index i and j, inclusive, i.e. RSQ(i, j) = L[i] + L[i+1] + L[i+2] + ... + L[j].<br/>


## 입력
The input starts with an integer t in the first line that denotes the number of test cases in this problem (1 ≤ t ≤ 5).<br/>
Each test case starts with a blank line, followed by a line that contains 2 integers: n and q (1 ≤ n, q ≤ 100,000).<br/>
Then, the next line contains n non-negative integers up to 1,000,000,000.<br/>
Then q lines follow.<br/>
Each line contains two integers, i and j (0 ≤ i, j < 10,000).<br/>


## 출력
For each query, print a line containing the value of RSQ(i, j). Separate two test cases with a blank line.<br/>


## 예제 입력
2<br/>
<br/>
5 2<br/>
1 2 3 4 5<br/>
4 4<br/>
1 3<br/>
<br/>
10 5<br/>
10 9 7 20 14 23 14 27 38 77<br/>
8 9<br/>
7 9<br/>
6 9<br/>
5 9<br/>
4 9<br/>

## 예제 출력
5<br/>
9<br/>
<br/>
115<br/>
142<br/>
156<br/>
179<br/>
193<br/>


## 풀이
전체 누적합 sum에 구한 뒤 i에서 j까지의 누적합은 sum[j] - sum[i - 1]로 O(1)에 찾을 수 있다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/9780