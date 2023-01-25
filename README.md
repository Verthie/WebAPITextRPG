# WebAPI TextRPG
 Projekt Web API - Programowanie obiektowe
 
## Elementy Zawarte w projekcie:
- 4 kontrolery
- CRUD
- Modele DTO
- Baza danych w JSON
- Interfejsy do pobierania danych
- Walidacja Danych
- Obsługa wyjątków
- Połączenie z relacyjną bazą danych w technologii SQLite - 1 relacja jeden-do-jednego, 1 relacja jeden-do-wielu, 1 relacja wiele-do-wielu
- Zostały wykonane testy API w Postman - 4 przykładowe testy zostały zaprezentowane na końcu dokumentu

## Założenia i zasady działania projektu:
- Projektem jest symulacja tekstowego RPG
- Całość została wykonana w technologii C# .NET
- Program umożliwia użytkownikowi rejestrację i logowanie z wykorzystaniem autoryzacji i szyfrowania haseł
- Aspektem głównym programu jest wyświetlanie, tworzenie, modyfikowanie i usuwanie postaci użytkownika (relacja jeden-do-wielu)
- Postaciom można przypisywać broń (relacja jeden-do-jednego) jak i czary (relacja wiele-do-wielu)
- Postacie mogą brać udział w walkach pomiędzy sobą jeden na jednego albo w większej ilości
- Wszystkie wykonywane ataki wyświetlane są w postaci tekstowej opisującej postać atakującą, postać atakowaną oraz ilość zadanych obrażeń
- Ostatnia osoba która zwycięży otrzymuje punkt zwycięstwa ci którzy polegli otrzymują punkt przegranej
- Wyniki wszystkich postaci posortowane są względem ilości zwycięstw oraz walk
- Wszystkie metody GET, POST, PUT, DELETE dla broni jak i postaci wymagają autoryzacji
- Natomiast symulowanie walk pomiędzy postaciami oraz wyświetlanie tabeli wyników nie wymaga autoryzacji
- W bazie znajduje się obecnie 9 postaci (id 1-9), 9 broni przypisanych każdej postaci (id 1-9), 4 czary (id 1-4) oraz 3 użytkowników (id 1-3)

## Przykłady użycia:
W celu wyświetlenia listy postaci nalężących do użytkownika "testuser" należy wykonać podane kroki:
1. wykorzystać metodę /Auth/Login do zalogowania użytkownika - dane logowania to: username: "testuser", password: "123456"
2. pobrać token z odpowiedzi wysłanej przez metodę (data: {token}) i przeprowadzić autoryzację poprzez wpisanie "bearer {token}" w formularzu autoryzacji
3. wykorzystać metodę /api/Character/GetAll w celu pozyskania listy postaci nalężących do użytkownika

Wynik takiej operacji
<img src="/Images/Wyświetlanie postaci Swagger.png">

W celu symulacji bitwy pomiędzy postaciami należy wykonać podane kroki:
1. wykorzystać metodę /Fight
2. podać listę Id postaci, które mają stoczyć walkę

Przykładowy wynik takiej operacji dla postaci o Id: 1,3,6
<img src="/Images/Symulacja walki.png">

## Testy wykonane w Postman:
 Wyświetlanie tabeli wyników (nie wymaga autoryzacji):
<img src="/Images/Wyświetlanie tabeli wyników Postman - test bez autoryzacji.png">

Logowanie użytkownika (nie wymaga autoryzacji):
<img src="/Images/Logowanie użytkownika Postman - test bez autoryzacji.png">

Wyświetlanie listy postaci nalężących do danego użytkownika (wymaga autoryzacji):
<img src="/Images/Wyświetlanie listy postaci należących do użytkownika - test wymagający autoryzacji.png">

Tworzenie postaci przypisanej do użytkownika (wymaga autoryzacji):
<img src="/Images/Tworzenie postaci przez użytkownika - test wymagający autoryzacji.png">
