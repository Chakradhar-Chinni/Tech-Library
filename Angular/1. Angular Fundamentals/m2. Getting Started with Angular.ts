<<h2>>
Angular 16 is used for this course. Released in 2023

old web frameworks : click on "add to cart" . webPage refreshes and loads everything again
modern frameworks: cart gets updated without much changes


<<h2>>
routing takes care of such things
component - a web page item 
angular application would have hierarchies, like many child scenarios



<<h2>>
Type Script is used for its type safety.

Type sctipt:
let name : string;
let age : int;

JavaScript
let name
let age


Javascript doesn't provide typesafety, but TS does





<<h2>>
Major Typescript components
Static Typing
Interfaces
Class Properties
Public/Private accessibility


<<h2>>
Setting up development environment
Visual Studio Code
nodejs (nvm)
Angular CLI

install nodejs
https://github.com/coreybutler/nvm-windows/releases - download nvm-setup.exe
cmd > nvm install 18.10.0
after installing, 
cmd > nvm use 18.10.0
check nodejsversion: node -v or node --version



install angular
npm install -g @angular/cli@16.0.0

create new project: ng new joes-robot-shop

open project in vs code and start the app using: npm start

website will be opened at localhost:4200 (or another port number)

nice landing page will be shown on first view

initial looks:
index.html has <app-root> element which is a angular component. check its mapping in app.component.ts. like below. It has its own html, css page. 
over-write .html file with <h1> tag and notice it auto changes on website

## /src/app/app.component.ts
import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
}


##/./app.component.html
<h1> Hello World> 

