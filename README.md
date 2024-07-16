# Web-site1

Архитектура приложения основана на принципах Чистой архитектуры (Clean Architecture):
1. Слои приложения:
## Presentation (MVC): Это слой, который отвечает за взаимодействие с пользователем. Он содержит контроллеры (например, ProductController), представления (например, Index.cshtml, Create.cshtml) и модели представления (ViewModels).
## Application: Этот слой содержит бизнес-логику приложения. Он взаимодействует с слоем Domain и Infrastructure. В этом слое обычно находятся сервисы (например, ProductService), которые обрабатывают запросы из Presentation и выполняют необходимые операции с данными.
## Domain: Этот слой содержит логику предметной области (Domain Logic). Он определяет модели данных (например, Product, Shirt, Dress), репозитории (например, IRepository<T>) и сервисы (например, IProductService). Этот слой должен быть независимым от конкретных технологий и инфраструктуры.
## Infrastructure: Этот слой отвечает за взаимодействие с инфраструктурой, например, с базой данных. Он содержит конкретные реализации репозиториев (например, ProductRepository), которые взаимодействуют с базой данных и предоставляют данные слою Application.
2. Взаимодействие между слоями:
#### Presentation -> Application: Контроллеры в Presentation вызывают сервисы в Application для обработки запросов пользователя.
#### Application -> Domain: Сервисы в Application используют репозитории и сервисы из Domain для работы с данными и бизнес-логикой.
#### Application -> Infrastructure: Сервисы в Application используют конкретные реализации репозиториев из Infrastructure для взаимодействия с базой данных.
3. Пример работы приложения:
Пользователь отправляет запрос на главную страницу (например, /).
ASP.NET Core MVC обрабатывает запрос и вызывает метод Index контроллера ProductController.
ProductController вызывает метод GetAllProductsAsync сервиса ProductService из слоя Application.
ProductService использует репозиторий ProductRepository из слоя Infrastructure для получения списка товаров из базы данных.
ProductRepository выполняет запрос SQL к базе данных и возвращает список товаров в слой Application.
ProductService возвращает список товаров в ProductController.
ProductController передает список товаров в представление Index.cshtml.
Index.cshtml отображает список товаров на странице.
