# Starwars - Resupply Stops

The application consumes [SWApi](https://swapi.co/) API to get a list of starships and provides a calculator to discover how many resupply stops are required to cover a given distance.

The solution is composed of one backend API and one web application

Concepts applied:

- TDD
- DDD
- Clean Code
- SOLID

## Local Deployment 

- Download the repository
- Run Docker compose
- Access the API Swagger documentation http://localhost:88888/swaager
- Access the application [http://localhost:4200](http://localhost:4200)


## Backend
Was develop using:
- asp.net core
- Xunit
- Moq
- Refit
- Swagger
- Visual Studio 2019

## Frontend
Was developed using:
- Angular 8
- Angular Material
  - Custom theme: https://materialtheme.arcsine.dev/
- Jasmine/karma
- Vs Code

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.3.22.

### Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

### Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

### Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
