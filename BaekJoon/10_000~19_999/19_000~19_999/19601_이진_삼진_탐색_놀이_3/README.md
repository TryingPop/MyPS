# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 수학
  - 이분 탐색
  - 재귀
  - 삼분 탐색

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 256 MB

## 문제
오늘도 서준이는 아빠와 함께 알고리즘 놀이를 하고 있다. 서준이는 이진 탐색, 아빠는 삼진 탐색 놀이를 하고 있다.<br/>
서로 다른 정수가 오름차순으로 정렬된 크기가 N인 배열 A가 있다. 이진 탐색, 삼진 탐색으로 배열 A의 i번째 원소 Ai를 찾을 때, Ai를 찾기 전에 참조해야 하는 배열 A의 원소 개수를 각각 Bi , Ti 라고 하자. 서준이는 아빠로 부터 Bi , Ti 최대값을 공백을 사이에 두고 한줄에 출력하는 Q개의 질의를 받았다. n과 Q가 커서 괴로워 하는 우리 서준이를 도와주자.<br/>
크기가 N인 배열에서 이진 탐색 알고리즘 의사 코드는 다음과 같다.<br/>

	binary_search(A[0..N-1], value, left, right) {
	    mid = (left + right) / 2
	    if (A[mid] == value)
	        return mid
	    else if (value < A[mid])
	        return binary_search(A, value, left, mid - 1)
	    else
	        return binary_search(A, value, mid + 1, right)
	}

크기가 N인 배열에서 삼진 탐색 알고리즘 의사 코드는 다음과 같다.<br/>

	ternary_search(A[0..N-1], value, left, right) {
	    left_third = left + (right - left) / 3
	    right_third = right - (right - left) / 3
	    if (A[left_third] == value) 
	        return left_third
	    else if (A[right_third] == value)
	        return right_third
	    else if (value < A[left_third])
	        return ternary_search(A, value, left, left_third - 1)
	    else if (value < A[right_third])
	        return ternary_search(A, value, left_third + 1, right_third - 1)
	    else
	        return ternary_search(A, value, right_third + 1, right)
	}


## 입력
첫째 줄에 쿼리의 수 Q(1 ≤ Q ≤ 500,000)가 주어진다. 둘째 줄부터 Q + 1줄까지 배열 A의 크기 N이 주어진다.<br/>


## 출력
첫번째 쿼리부터 Q번째 쿼리까지 각각의 쿼리 결과를 한줄씩 출력한다.<br/>


## 제한
  - 1 ≤ N ≤ 2^63 − 1
  - 1 ≤ Q ≤ 500,000


## 예제 입력
1<br/>
5<br/>


## 예제 출력
2 2<br/>


## 힌트
n=5인 1개의 쿼리가 주어졌다. A는 서로 다른 정수가 오름차순으로 정렬된 크기가 5인 배열이다. 배열 B는 1 2 0 1 2가 된다. A0를 찾기 전에 A2를, A1을 찾기 전에 A2 A0를, A3를 찾기 전에 A2를, A4를 찾기 전에 A2 A3를 참조 한다. A2는 바로 찾는다. 배열 T는 2 0 2 1 2가 된다. A0를 찾기 전에 A1 A3를, A2을 찾기 전에 A1 A3를, A3를 찾기 전에 A1을, A4를 찾기 전에 A1 A3를 참조 한다. A1은 바로 찾는다. Bi의 최대값은 2, Ti의 최대값도 2이다.<br/>


## 풀이
경우의 수를 찾아 풀었다.<br/>
이분 탐색의 경우 2배수로 들어간다.<br/>
삼분 탐색의 경우 1 ~ 40까지 해본 결과 규칙성이 존재했고, 작동원리를 보니 성립함을 확인했다.<br/>

## 문제 링크
https://www.acmicpc.net/problem/19601