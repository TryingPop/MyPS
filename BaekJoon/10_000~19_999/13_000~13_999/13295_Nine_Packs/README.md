# C#

## 난이도 : 골드 4

## 알고리즘 분류
  - 다이나믹 프로그래밍
  - 배낭 문제

## 제한조건
  - 시간 제한 : 2초
  - 메모리 제한 : 512 MB

## 문제
“It’s like how hot dogs come in packs of ten, and buns come in packs of eight or twelve — you have to buy nine packs to make it come out even.”<br/>
This is a quote from the 1986 movie, “True Stories”, and it’s true; well, almost true. You could buy four packs of 10 hotdogs and five packs of 8 buns. That would give you exactly 40 of each. However, you can make things even with fewer packs if you buy two packs of 10 hotdogs, along with a pack of 8 buns and another pack of 12 buns. That would give you 20 of each, using only 4 total packs.<br/>
For this problem, you’ll determine the fewest packs you need to buy to make hotdogs and buns come out even, given a selection of different bun and hotdog packs available for purchase.<br/>


## 입력
The first input line starts with an integer, H, the number of hotdog packs available. This is followed by H integers, h1 . . . hH, the number of hotdogs in each pack. The second input line starts with an integer, B, giving the number of bun packs available. This is followed by B integers, b1 . . . bB, indicating the number of buns in each pack. The values H and B are between 0 and 100, inclusive, and the sizes of the packs are between 1 and 1 000, inclusive. Every available pack is listed individually. For example, if there were five eight-bun packs available for purchase, the list of bun packs would contain five copies of the number eight.<br/>


## 출력
If it’s not possible to purchase an equal number of one or more hotdogs and buns, just output “impossible”. Otherwise, output the smallest number of total packs you can buy (counting both hotdog and bun packs) to get exactly the same number of hotdogs and buns.<br/>


## 예제 입력
4 10 10 10 10<br/>
10 8 8 8 12 12 12 8 8 12 12<br/>


## 예제 출력
4<br/>


## 풀이
핫도그의 갯수와 빵의 갯수가 최대 1000개이므로 배낭 방법이 유효하다.<br/>
먼저 핫도그로 가능한 개수를 만들 수 있는지 만들 수 있다면 최소 필요한 최소 핫도그의 갯수를 배낭방법으로 찾는다.<br/>
그리고 빵도 핫도그 처럼 만들 수 있는 개수와 최소 빵의 갯수를 똑같이 배낭 방법으로 찾는다.<br/>
그리고 두 개다 만들 수 있는 무게인 경우 최소 봉지의 합이 최소인 값을 찾으면 정답이 된다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/13295