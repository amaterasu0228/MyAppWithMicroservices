# MyAppWithMicroservices 🧩

**MyAppWithMicroservices** — це навчальний проєкт, побудований за архітектурою мікросервісів з використанням ASP.NET Core, C#, Docker та принципів SOLID. Мета проєкту — реалізувати інформаційну систему з авторизацією, реєстрацією користувачів, REST API та масштабованою архітектурою.

---

## ⚙️ Технології

- **C#** (.NET 6+)
- **ASP.NET Core Web API**
- **Docker & Docker Compose**
- **SOLID & Clean Code**
- **Microservices Architecture**
- **HttpClient**
- **JWT (JSON Web Token) – (у планах / реалізовано)**
- **Ocelot / YARP API Gateway**
- **SQL Server / MSSQL**
- **WinForms (.NET Framework) – як клієнт**

---

## 📦 Архітектура проєкту

MyAppWithMicroservices/
├── AuthService.API/ # Мікросервіс авторизації
├── UserService.API/ # Мікросервіс реєстрації / управління користувачами
├── ApiGateway/ # API Gateway (YARP або Ocelot)
├── WinFormsClient/ # WinForms-клієнт (UI)
├── docker-compose.yml # Контейнеризація всіх сервісів


---

## 🔐 Мікросервіси

Кожен мікросервіс має свою відповідальність:
- `AuthService.API`: логін, валідація користувача.
- `UserService.API`: реєстрація, зберігання профілю.
- `ApiGateway`: централізований вхід для клієнтів через HTTP (YARP / Ocelot).
- `WinFormsClient`: взаємодія з API через HttpClient.

---

## Як запустити

### Вручну:
1. Запустити **AuthService.API** (`dotnet run`)
2. Запустити **UserService.API**
3. Запустити **ApiGateway**
4. Запустити **WinFormsClient.exe**

### Через Docker:
```bash
docker-compose up --build
