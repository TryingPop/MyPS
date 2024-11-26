# C#

## 난이도 : 골드 1

## 알고리즘 분류
  - 문자열
  - 애드 혹
  - 누적 합

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 1024 MB

## 문제
Bessie finds a string s of length at most 2 x 10^5 containing only the three characters 'C', 'O', and 'W'. She wants to know if it's possible to turn this string into a single 'C' (her favorite letter) using the following operations:<br/>

  1. Choose two adjacent equal letters and delete them.
  2. Choose one letter and replace it with the other two letters in either order.

Finding the answer on the string itself isn't enough for Bessie, so she wants to know the answer for Q (1 ≤ Q ≤ 2 x 10^5) substrings of s.<br/>


## 입력
The first line contains s.<br/>
The next line contains Q.<br/>
The next Q lines each contain two integers l and r (1 ≤ l ≤ r ≤ |s|, where |s| denotes the length of s).<br/>


## 출력
A string of length Q, with the i-th character being 'Y' if the i-th substring can be reduced and 'N' otherwise.<br/>


## 예제 입력
COW<br/>
6<br/>
1 1<br/>
1 2<br/>
1 3<br/>
2 2<br/>
2 3<br/>
3 3<br/>

## 예제 출력
YNNNYN<br/>


## 힌트
The answer to the first query is yes because the first character of s is already equal to 'C'.<br/>
The answer to the fifth query is yes because the substring OW from the second to the third character of s can be converted into 'C' in two operations:<br/>

	   OW
	-> CWW
	-> C

No other substring of this example string COW can be reduced to 'C'<br/>


## 풀이
문자열에서 'C'의 개수를 C, 'O'의 개수를 O, 'W'의 개수를 W라 하자.<br/>
그러면 O + W가 홀수인 경우 어떻게 변환해도 O + W는 홀수이다.<br/>
그래서 O, W를 모두 제거할 수 없다. 이에 O + W는 짝수여야만한다.<br/>


또한 C + O가 짝수인 경우 C + O는 C를 하나로 줄일 수가 없다.<br/>
그래서 C + O는 홀수여야 한다.<br/>


따라서 C + O는 홀수여야하고, O + W는 짝수여야한다.<br/>
C, O, W 홀짝 경우를 비교해보면 C + W는 홀수이고, O + W는 짝수여야 한다로 바꿀 수 있다.<br/>


이제 C + W가 홀수이고, O + W가 짝수인 경우로 해집합을 만들어본다.<br/>
먼저 O를 모두 제거한다. 그러면 W, C로 이루어진 문자열이 남고 W는 짝수개, C는 홀수개가 남을 수 밖에 없다.<br/>
이후 C가 하나만 남을때까지 연산해주면 된다.<br/>


그래서 구간에 C + W가 홀수이고, O + W가 짝수인지 확인해주면 된다.<br/>
누적합으로 C, O, W 갯수를 누적해주면 O(1)에 갯수를 판별할 수 있다.<br/>
그런데 홀짝 판별만 하고 C + W가 홀수, O + W 짝수인지 판별하기 위해<br/>
C = 1, O = 2, W = 3으로 한 뒤 비트 연산자 ^를 이용하면 C + W, O + W의 홀짝 판별을 쉽게 할 수 있다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/24979