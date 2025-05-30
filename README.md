# WebAPI TextRPG
Projekt Web API tekstowego RPG napisanego w C# .NET. Umożliwia rejestrację użytkowników, zarządzanie postaciami, ekwipunkiem i symulację walk. Wykorzystuje SQLite, DTO, autoryzację JWT oraz relacyjne powiązania w bazie danych.

## Funkcje
- Rejestracja i logowanie użytkowników z autoryzacją JWT
- Operacje CRUD na postaciach, broni i czarach zabezpieczone autoryzacją
- Symulacja walk 1v1 i grupowych między postaciami
- Przechowywanie danych w SQLite (relacje 1:1, 1:N, N:M)
- System zwycięstw i rankingów
- Walidacja danych, obsługa wyjątków

## Dostępne Endpointy

### 🔐 AuthController (`/Auth`)
- `POST /Register` – Rejestracja użytkownika
- `POST /Login` – Logowanie użytkownika

### 🧍 CharacterController (`/api/Character`) – wymaga autoryzacji
- `GET /GetAll` – Pobierz wszystkie postacie użytkownika
- `GET /{id}` – Pobierz jedną postać po ID
- `POST /` – Dodaj nową postać
- `PUT /` – Zaktualizuj istniejącą postać
- `DELETE /{id}` – Usuń postać
- `POST /Spell` – Przypisz czar do postaci

### ⚔️ FightController (`/Fight`)
- `POST /` – Rozpocznij walkę (lista postaci)
- `POST /Weapon` – Atak bronią
- `POST /Spell` – Atak czarem
- `GET /Highscore` – Pobierz ranking postaci

### 🗡️ WeaponController (`/Weapon`) – wymaga autoryzacji
- `POST /` – Dodaj broń do postaci

## Jak używać
1. Zaloguj się przez `/Auth/Login` (testowy użytkownik: `testuser`, hasło: `123456`)
2. Uzyskaj token JWT z odpowiedzi
3. Dodaj `Bearer {token}` do nagłówka autoryzacji
4. Używaj endpointów, np. `/api/Character/GetAll`

### Przykład odpowiedzi
<img src="/Images/Wyświetlanie postaci Swagger.png">

## Technologie
- ASP.NET Core Web API (.NET 7+)
- SQLite
- Entity Framework Core
- DTO, automatyczne mapowanie danych
- JWT (JSON Web Tokens)

## Uruchamianie aplikacji

### Wymagania
- .NET 7 SDK lub nowszy: https://dotnet.microsoft.com/download
- Visual Studio 2022+ lub edytor wspierający .NET (np. Rider, VS Code)

### Kroki:

1. **Sklonuj repozytorium:**
```bash
git clone https://github.com/twoj-user/twoj-projekt.git
cd twoj-projekt
```

2. **Uruchom aplikację:**
```bash
dotnet run
```

3. **Otwórz dokumentację Swagger:**
Aplikacja domyślnie uruchamia się pod https://localhost:5001/swagger

## Licencja

Projekt edukacyjny – do swobodnego użytku, edycji i rozbudowy.
