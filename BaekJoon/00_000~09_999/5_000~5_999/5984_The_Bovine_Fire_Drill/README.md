# C#

## 난이도 : 브론즈 1

## 알고리즘 분류
  - 구현
  - 시뮬레이션

## 제한조건
  - 시간 제한 : 1초
  - 메모리 제한 : 128 MB

## 문제
The N (3 <= N <= 250) cows (conveniently labeled cow_1..cow_N) sat in a perfect circle  around the camp fire in chairs numbered chair_1..chair_N as Farmer John told them stories of the old days (cow_i sits in chair_i, of course). At the conclusion of one story, FJ suggested they perform a Bovine Fire Drill.<br/>
In a Bovine Fire Drill, one cow at a time walks around the circle from her chair to a potentially new chair while the other cows chant "Fire, fire, fire." When it is cow_i's turn to move, she rises and moves clockwise to the i'th chair encountered in that direction (so if it was cow_3's turn, she would rise from chair_3 and move to chair_6 if N >= 6).<br/>
When cow_i arrives at her new chair, she taps any cow sitting there on the shoulder; that cow rises to make room for cow_i, who sits down. This process continues until a cow lands in an empty chair or until a cow is asked to move for a second time. In either of those cases, the game ends. Cow 1 always starts, so it is her chair that will be the empty one.<br/>
Only rarely do all the cows get to participate, as the game termination conditions arise naturally and frequently from the properties of whole numbers. The final cow to move (whether she ends the game by sitting in cow_1's chair or by tapping the shoulder of a cow who has already moved) receives a special treat of extra tender grass.<br/>
Help FJ plan in advance to learn which cow will get the tender grass.<br/>
For example, consider five cows sitting around the blazing campfire:<br/>


	  2   -   3
	 (         )
	  1 - 5 - 4


First, cow 1 walks one space and taps cow 2, who rises. (The * denotes the empty chair.)<br/>


	    2
	 1   -   3
	 (         )
	  * - 5 - 4


Cow 2 walks two spaces and taps cow 4, who begins her journey:<br/>


	  1  -    3
	 (         )
	  * -  5 - 2
	          4



Cow 4 will walk four spaces and tap cow 3:<br/>


	             3
	  1   -    4  
	 (         )
	  * -  5 - 2


Finally, cow 3 will walk three spaces to chair_1, which is empty and thus terminates the drill.<br/>


	  1   -    4  
	 (         )
	  3 -  5 - 2


Cow 3 receives tender spring grass as the other cows clap and cheer.<br/>


## 입력
  - Line 1: A single integer: N


## 출력
  - Line 1: The number of the cow who ends the drill


## 예제 입력
5<br/>


## 예제 출력
3<br/>


## 풀이
N이 250으로 작고, 이동을 보면 언제 첫 번째 땅에 도달하는지 확인하는 문제다.<br/>
해당 조건을 구현해도 두번 이동하는 경우 탈출하기에 많아야 서로 다른 N명이 이동해도 N + 1번째에 탈출하고 이는 N + 1을 넘지 않는다.<br/>


그래서 조건대로 구현한 뒤 시뮬레이션 돌리면 된다.<br/>
먼저 일어날 사람이 일어난다.<br/>
그리고 해당 사람이 이동한다.<br/>
마지막으로 탈출조건을 확인하면 된다.<br/>


처음 시도에 탈출하지 않게 주의한다면 탈출 조건 확인을 먼저한다.<br/>
그리고 자리 일어나는 연산을 한다.<br/>
다음으로 사람이 이동하는 연산을 하게 순서를 바꿔도 상관없어 해당 방법으로 구현했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/5984