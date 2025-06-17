# C#

## 난이도 : 실버 1

## 알고리즘 분류
  - 구현
  - 문자열

## 제한조건
  - 시간 제한 : 3초
  - 메모리 제한 : 512 MB

## 문제
Daisy loves playing games with words. Recently, she has been playing the following Deletive Editing word game with Daniel.<br/>
Daisy picks a word, for example, "DETERMINED". On each game turn, Daniel calls out a letter, for example, 'E', and Daisy removes the first occurrence of this letter from the word, getting "DTERMINED". On the next turn, Daniel calls out a letter again, for example, 'D', and Daisy removes its first occurrence, getting "TERMINED". They continue with 'I', getting "TERMNED", with 'N', getting "TERMED", and with 'D', getting "TERME". Now, if Daniel calls out the letter 'E', Daisy gets "TRME", but there is no way she can get the word "TERM" if they start playing with the word "DETERMINED".<br/>
Daisy is curious if she can get the final word of her choice, starting from the given initial word, by playing this game for zero or more turns. Your task it help her to figure this out.<br/>


## 입력
The first line of the input contains an integer n --- the number of test cases (1 ≤ n ≤ 10,000). The following n lines contain test cases.<br/>
Each test case consists of two words s and t separated by a space. Each word consists of at least one and at most 30 uppercase English letters; s is the Daisy's initial word for the game; t is the final word that Daisy would like to get at the end of the game.<br/>


## 출력
Output 
n lines to the output --- a single line for each test case. Output "YES" if it is possible for Daisy to get from the initial word s to the final word t by playing the Deletive Editing game. Output "NO" otherwise.<br/>


## 예제 입력
6<br/>
DETERMINED TRME<br/>
DETERMINED TERM<br/>
PSEUDOPSEUDOHYPOPARATHYROIDISM PEPA<br/>
DEINSTITUTIONALIZATION DONATION<br/>
CONTEST CODE<br/>
SOLUTION SOLUTION<br/>

## 예제 출력
YES<br/>
NO<br/>
NO<br/>
YES<br/>
NO<br/>
YES<br/>

## 풀이
문자를 앞에서부터 제거해간다.<br/>
그래서 만들 수 있는 문자열을 보면 앞에서 살아남은 문자는 원본의 뒤에 있는 해당 문자들은 모두 살아있다.<br/>
만약 ABABCABCD문자열에서 왼쪽에서 두 번째로 먼저 나온 문자 A(B'A'BC)가 쓰였다고 하면 A(C'A'BCD)도 만들어진 문자열에 있어야 한다.<br/>
이는 뒤집어서 생각하면 뒤에서 없는 문자열은 앞에도 없음과 동치이다.<br/>


그래서 뒤에서부터 비교하는데, 맨끝에 해당 문자가 만들어진 문자끝과 일치하면 두 문자 모두 제거하고 진행한다.<br/>
반면 일치하지 않으면 해당 원본 문자의 마지막 문자는 쓰일 수 없는 문자가 되고, 원본 문자의 마지막만 제거한다.<br/>
이렇게 두 포인터로 만들어진 모든 문자가 제거되는지 확인했다.<br/>


## 문제 링크
https://www.acmicpc.net/problem/25358