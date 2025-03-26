# C#

## 난이도 : 브론즈 1

## 알고리즘 분류
  - 구현
  - 그리디 알고리즘

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 512 MB

## 문제
Given a permutation A = (a_1, a_2, ..., a_N) of the integers 1, 2, ..., N, we define the greedily increasing subsequence (GIS) in the following way.<br/>
Let g_1 = a_1. For every i > 1, let g_i be the leftmost integer in A that is strictly larger than g_{i-1}. If there for a given i is no such integer, we say that the GIS of the sequence is the sequence (g_1, g_2, ..., g_{i - 1}).<br/>
Your task is to, given a permutation A, compute the GIS of A.<br/>


## 입력
The first line of input contains an integer 1 ≤ N ≤ 10^6, the number of elements of the permutation A. The next line contains N distinct integers between 1 and N, the elements a_1, ..., a_N of the permutation A.<br/>


## 출력
First, output a line containing the length l of the GIS of A. Then, output l integers, containing (in order) the elements of the GIS.<br/>


## 예제 입력
7<br/>
2 3 1 5 4 7 6<br/>


## 예제 출력
4<br/>
2 3 5 7<br/>


## 힌트
In this case, we have the permutation  2, 3, 1, 5, 4, 7, 6. First, we have g_1 = 2. The leftmost integer larger than 2 is 3, so g_2 = 3. The leftmost integer larger than 3 is 5 (1 is too small), so g_3 = 5. The leftmost integer larger than 5 is 7, so g_4 = 7. Finally, there is no integer larger than 7. Thus, the GIS of 2, 3, 1, 5, 4, 7, 6 is 2, 3, 5, 7.


## 풀이
힌트대로 구현했다.<br/>
GIS의 1항은 기존 수열의 1항이다.<br/>
이후 2항부터는 GIS의 끝항보다 큰 수를 먼저 채워넣으면 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/17931