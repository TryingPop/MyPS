# C#

## 난이도 : 실버 3

## 알고리즘 분류
  - 그래프 이론
  - 위상 정렬
  - 방향 비순환 그래프
  - 강한 연결 요소

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 1024 MB

## 문제
In the classic blackboard game HangMan you are given a word and attempt to guess the word by choosing a series of letters. In this program, you must determine if the provided letters are contained in the word prior to a body figure being drawn. One body part is added for each letter not contained in the word. The body figure below consists of the head shown by the letter "O", two body parts "|", 2 arms shown by equal signs "=", 2 hands shown by asterisks "*", and legs shown using a forward slash "/"and a back slash "\", 9 parts in all, drawn in order, top to bottom - from left to right, as shown below. Once all the letters of the word are found, the game ends and no further letters are read. The game also ends once the figure is completed. If no body parts are drawn, print the word “SAFE”.<br/>

	  O
	*=|=*
	  |
	 / \


## 입력
The first line contains a single positive integer, k, indicating the number of data sets. Each data set is on one line, and consists of a single word S (all capital letters), an integer N, followed by N distinct capital letters.<br/>


## 출력
The word and resulting hangman figure, according to the series of guesses provided by the letters. The body parts MUST align as shown, with the "*" on the left edge of the screen, and all other parts aligned accordingly. Exactly one blank line must follow each output.<br/>


## 예제 입력
4<br>
APLUS 8 A S E U G P I J<br>
UIL 9 A B C D E F G H J<br>
SALTLICK 9 E F G H I J K M N<br>
GOOGLE 4 G O L E<br>


## 예제 출력
APLUS<br>
&nbsp;&nbsp;O<br>
+=|<br>
<br>
UIL<br>
&nbsp;&nbsp;O<br>
+=|=+<br>
&nbsp;&nbsp;|<br>
&nbsp;/&nbsp;\<br>
<br>
SALTLICK<br>
&nbsp;&nbsp;O<br>
+=|=+<br>
&nbsp;&nbsp;|<br>
<br>
GOOGLE<br>
SAFE<br>


## 풀이
찾을 문자에 포함된 알파벳의 갯수와 알파벳을 기록한다.<br/>
그리고, 포함된 알파벳이 있는지 하나씩 확인하며 있으면 넘어가고 없으면 그린 갯수를 확인한다.<br/>
행맨은 문자열 하나로 표현했고, 갯수를 갯수에 따른 수작업으로 idx 위치를 찾아가며 따로 저장했다.<br/>
그리고 출력에서 해당 idx까지 출력했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/26514