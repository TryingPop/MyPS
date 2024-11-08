# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 이분 탐색
  - 중간에서 만나기

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 256 MB

## 문제
N개의 정수로 이루어진 수열이 있을 때, 크기가 양수인 부분수열 중에서 그 수열의 원소를 다 더한 값이 S가 되는 경우의 수를 구하는 프로그램을 작성하시오.<br/>

## 입력
첫째 줄에 정수의 개수를 나타내는 N과 정수 S가 주어진다. (1 ≤ N ≤ 40, |S| ≤ 1,000,000) 둘째 줄에 N개의 정수가 빈 칸을 사이에 두고 주어진다. 주어지는 정수의 절댓값은 100,000을 넘지 않는다.<br/>

## 출력
첫째 줄에 합이 S가 되는 부분수열의 개수를 출력한다.<br/>

## 예제 입력
5 0<br/>
-7 -3 -2 5 8<br/>

## 예제 출력
1<br/>

## 풀이
단순히 모든 경우를 확인하면 각 원소의 포함유무 2^N경우의 수가 나온다.<br/>
N 이 40까지 가므로 10^12 < 2^40 이다.<br/>
집합을 반으로 나눠 부분 수열의 합으로 나올 수 있는 모든 경우를 각각 확인하면,<br/>
각각 2^20 ≒ 1_000_000 번 연산을 하고 약 200만이 된다.<br/>
그리고 나눠서 부분 수열의 합들의 두 집합을 A, B라 하자.<br/>
각 k ∈ A 에 -k ∈ B가 있으면 0이 되는 경우가 존재한다.<br/>
그리고 k에 대해 0을 만드는 개수는 A에 있는 k의 개수를 ka, B에 있는 -k의 개수를 kb라 하면 ka x kb가 된다.<br/>
이렇게 서로 다른 k의 모든 경우를 확인하며 0이되는 개수를 누적해주면 정답이 된다.<br/>
개수와 값을 저장해야하므로 dictionary 자료구조를 이용해 풀었다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/1208