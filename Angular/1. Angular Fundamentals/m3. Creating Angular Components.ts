
<<h2>> Creating First Angular Component
cmd> ng generate compoenent home 

1)) home component is created at /src/app/home/  with the following files
1) home.component.css
2) home.component.html
3) home.component.spec.ts
4) home.component.ts
    - specifies the 'app-home' as home component selector, has the .html file used by home component
    - [styleUrls] is in array - multiple css files can be used

2)) /src/app/app.module.ts
   Home component is auto imported & declared in NgModule - CLI does this automatically, good thing
   In this file, declarations must be done otherwise app doesn't know where to look for our components

## /home/home.component.css
 blank

## /home/home.component.html
<p>home works!</p>


## /home/home.component.spec.ts
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


## /home/home.component.ts
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

## /src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }









<<h2>> Application prefix for components

in /home/component.ts
selector: 'app-home' is created by CLI. THe prrefix app comes from /angular.json file, like below.

"prefix": "app",

update the prefix value to "bot" and observe the new components will have prefix as bot instead of home

after updating "prefix": "bot", and deleting home component. regenerate the home component and notice the change below

## /home/home.component.ts
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'bot-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

Prefix should be always unique, even if 3rd party packages are used. Angular reserved keywords shouldn't be used for prefix.








<<h2>> Using Inline styles

1) Template, style are using inline html, css. so, .html & .css file of 'bot-home' component can be deleted.
2) Inline template, style is not preferred unless its just 1 oe 2 lines of code. 
3) This is just to understand, inline is also possible

import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'bot-home',
  template: `<p> Inline home works </p>`,
  styles: [`
    p{
      color:blue;
    }
  `],
})









<<h2>> Accessing and Displaying Images
Angular.json will have assets array

"assets": [
              "src/favicon.ico",
              "src/assets"
            ],

1. any images or static files path can be mentioned here and referenced in code
2. Apart from src/, new paths can be added to project folder and added to assets array
3. while referencing in code, directly use /assets
  3.1 HTML ex: <img src="/assets/images/robot-parts/torso-gauged.png" alt="Robot Torsos" />
  3.2 CSS ex:   background-image: url("/assets/images/hero-banner.png");


## /src/home/home.component.html
<div class="container">
  <div class="hero"></div>

  <div class="promoted">
    <img src="/assets/images/robot-parts/head-friendly.png" alt="Friendly Robot Head" />
    <div class="promo-text">
      <div class="promo-main-text">DISPELL THE ROBOT APOCALYPSE MYTH</div>
      <div class="promo-sub-text cta">
        <div>SAVE 20% ON OUR FRIENDLIEST</div>
        <div>ROBOT HEADS</div>
      </div>
    </div>
    <img src="/assets/images/robot-parts/head-big-eye.png" alt="Big Eye Head" />
  </div>

  <ul class="robot-parts-cta">
    <li>
      <a class="part">
        <img src="/assets/images/robot-parts/head-shredder.png" alt="Robot Heads" />
        <div>ROBOT HEADS</div>
      </a>
    </li>
    <li>
      <a class="part">
        <img src="/assets/images/robot-parts/arm-articulated-claw.png" alt="Robot Arms" />
        <div>ROBOT ARMS</div>
      </a>
    </li>
    <li>
      <a class="part">
        <img src="/assets/images/robot-parts/torso-gauged.png" alt="Robot Torsos" />
        <div>ROBOT TORSOS</div>
      </a>
    </li>
    <li>
      <a class="part">
        <img src="/assets/images/robot-parts/base-spring.png" alt="Robot Bases" />
        <div>ROBOT BASES</div>
      </a>
    </li>
  </ul>

  <div class="white-paper">
    <img src="/assets/images/robot-apocalypse.png" alt="Robot Apocalyse" />
    <div class="text">
      <div>
        <div class="header-text cta">Will they kill us all?</div>
        <div class="sub-text">
          <p>10 Myths About the</p>
          <p>Robot Apocalyse</p>
        </div>
      </div>
      <div class="large-text">WHITE PAPER</div>
      <a href="" class="learn-more">Learn More</a>
    </div>
  </div>
</div>



## /src/home/home.component.css
.container {
  display: flex;
  flex-direction: column;
}

.hero {
  background-image: url("/assets/images/hero-banner.png");
  background-repeat: no-repeat;
  height: 300px;
  background-size: cover;
  background-position: center center;
  text-align: center;
  margin-left: -8px;
  margin-right: -8px;
}

.promoted {
  display: flex;
  justify-content: space-between;
  margin: 25px;
  border-top: 2px solid #888;
  border-bottom: 2px solid #888;
  padding: 10px 150px 10px 150px;
}

.promoted img {
  width: 150px;
  height: 150px;
}

.promo-text {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  margin: 40px 0 40px 0;
}

.promo-main-text {
  font-size: 24px;
  text-align: center;
}

.promo-sub-text {
  font-size: 20px;
  text-align: center;
}

.robot-parts-cta {
  display: flex;
  justify-content: space-between;
  margin: 0px 100px 25px 100px;
  padding: 10px 0;
}

.part {
  display: flex;
  flex-direction: column;
  font-size: 18px;
  text-align: center;
  cursor: pointer;
}

.part img {
  width: 180px;
  margin-bottom: 10px;
  padding: 10px;
  background-color: #888;
}

.white-paper {
  display: flex;
  height: 100%;
  margin: 0 25px;
}

.white-paper img {
  width: 75%;
}

.white-paper .text {
  display: flex;
  width: 25%;
  flex-direction: column;
  background-color: #333;
  text-align: center;
  justify-content: space-between;
}

.white-paper .header-text {
  margin-top: 50px;
  font-size: 30px;
}

.white-paper .sub-text {
  font-size: 20px;
  color: #5cadd2;
  margin-top: 10px;
  padding: 0 15%;
}

.white-paper .sub-text p {
  margin: 0;
}

.white-paper .large-text {
  border-top: 2px solid #666;
  border-bottom: 2px solid #666;
  margin-top: -50px;
  padding: 20px 0;
  font-size: 35px;
  color: #fff;
}

.white-paper .learn-more {
  background-color: #d25ca1;
  color: white;
  padding: 15px 25px;
  text-decoration: none;
}










<<h2>> Component Life Cycle hooks

These are special methods that Angular calls at specific points in a component's life—from creation to destruction. They allow developers to tap into key moments to run custom logic, such as initializing data, reacting to changes, or cleaning up resources.

THey get triggered at different phases of component existence, like Creation, Change detection, View rendering, Destruction


Commonly used hooks:
OnInit - logic initialization 
OnChanges
OnDestroy - to rpevent memory leaks


Example:  import the required interfaces and add them to method
import { Component, OnInit, OnDestroy } from '@angular/core';
@Component({
  selector: 'app-example',
  template: `<p>Example works!</p>`
})
export class ExampleComponent implements OnInit, OnDestroy {

  ngOnInit() {
    console.log('Component initialized');
  }

  ngOnDestroy() {
    console.log('Component destroyed');
  }
}

 the **Angluar Lifecycle Hooks with Examples** table 

+---------------------------+--------------------------------------------------+-----------------------------------------------+
|          Hook             |                    Purpose                       |              Example Use Case                 |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngOnInit()                | Called once after the component is initialized   | Fetch data from an API                        |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngOnChanges(changes)      | Called when any @Input() property changes        | React to changes in input-bound properties    |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngDoCheck()               | Called during every change detection run         | Implement custom change detection logic       |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngAfterContentInit()      | Called after content is projected                | Access projected content via <ng-content>     |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngAfterContentChecked()   | Called after every check of projected content    | Respond to changes in projected content       |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngAfterViewInit()         | Called after the component’s view is initialized| Access child components or DOM elements       |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngAfterViewChecked()      | Called after every check of the component’s view| Perform actions after view updates            |
+---------------------------+--------------------------------------------------+-----------------------------------------------+
| ngOnDestroy()             | Called just before the component is destroyed    | Clean up resources like subscriptions/timers  |
+---------------------------+--------------------------------------------------+-----------------------------------------------+













<<h2>> Creating Additional Components

creating catalog component, site-header component

cmd> ng generate component catalog
cmd> ng generate component site-header


for each catalog 4 files will be created as below

catalog component is created at /src/app/catalog/  with the following files
1) catalog.component.css
2) catalog.component.html
3) catalog.component.spec.ts
4) catalog.component.ts


site-header component is created at /src/app/site-header/  with the following files
1) site-header.component.css
2) site-header.component.html
3) site-header.component.spec.ts
4) site-header.component.ts


catalog component codes

## /src/app/catalog/catalog.component.css
.container {
  display: flex;
  flex-direction: column;
}

.filters {
  display: flex;
  justify-content: space-between;
  padding: 25px 200px;
}

.filters button {
  width: 100px;
}

.products {
  margin: 0 100px;
  border-top: 2px solid #999;
}

.product-item {
  border-bottom: 2px solid #999;
}

/* Product Details */
.product {
  display: flex;
  justify-content: space-between;
  padding: 20px 25px;
}

.product .product-details {
  display: flex;
  align-items: center;
}

.product img {
  width: 125px;
}

.product .product-info {
  margin-left: 25px;
}

.product .name {
  font-size: 22px;
  font-weight: bold;
}

.product .description {
  margin-top: 3px;
  font-size: 18px;
}

.product .category {
  margin-top: 20px;
  color: #777;
}

.product .price {
  display: flex;
  flex-direction: column;
  font-size: 25px;
  justify-content: space-around;
  align-items: center;
  min-width: 190px;
  color: #555;
  border-left: 2px solid #aaa;
  margin-left: 50px;
}

.product .price button {
  padding: 10px;
  width: 100px;
}



## /src/app/catalog/catalog.component.html
<div class="container">
  <div class="filters">
    <a class="button">Heads</a>
    <a class="button">Arms</a>
    <a class="button">Torsos</a>
    <a class="button">Bases</a>
    <a class="button">All</a>
  </div>

  <ul class="products">
    <li class="product-item">
      <div class="product">
        <div class="product-details">
          <img src="" alt="" />
          <div class="product-info">
            <div class="name"></div>
            <div class="description"></div>
            <div class="category">Part Type: </div>
          </div>
        </div>
        <div class="price">
          <div></div>
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>



## /src/app/catalog/catalog.component.spec.ts
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CatalogComponent } from './catalog.component';
describe('CatalogComponent', () => {
  let component: CatalogComponent;
  let fixture: ComponentFixture<CatalogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CatalogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});



## /src/app/catalog/catalog.component.ts
import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}




site-header component codes
## /src/app/site-header/site-header.component.css
.container {
  display: flex;
  justify-content: space-between;
  font-size: 24px;
  margin-bottom: 10px;
  margin: -3px -8px 0 -8px;
  padding: 0 8px 5px 8px;
  border-bottom: 2px solid #818285;
}

.left {
  display: flex;
  align-items: center;
}

.left * {
  margin-right: 25px;
}

.logo {
  height: 65px;
}

.cart {
  display: flex;
  position: relative;
}

.cartCount {
  position: absolute;
  display: flex;
  justify-content: space-around;
  align-items: center;
  width: 20px;
  height: 20px;
  top: -7px;
  right: -15px;
  border-radius: 25px;
  background-color: #f590c4;
  color: white;
  font-size: 14px;
}

.cartCount div {
  margin: auto;
}

.right {
  display: flex;
  align-items: center;
  margin-right: 25px;
}

.right * {
  margin-left: 25px;
}

.right img {
  height: 40px;
  cursor: pointer;
}

.sign-out {
  position: absolute;
  top: 60px;
  right: 30px;
}
## /src/app/site-header/site-header.component.html
<div class="container">
  <div class="left">
    <img class="logo" src="/assets/images/logo.png" alt="Logo" />
    <a>Home</a>
    <a href="">Catalog</a>
    <div class="cart">
      <a href="">Cart</a>
    </div>
  </div>
  <div class="right">
    <a href="">Sign In</a>
    <a href="" class="cta">Register</a>
  </div>
</div>
## /src/app/site-header/site-header.component.spec.ts
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteHeaderComponent } from './site-header.component';

describe('SiteHeaderComponent', () => {
  let component: SiteHeaderComponent;
  let fixture: ComponentFixture<SiteHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

## /src/app/site-header/site-header.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'bot-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.css']
})
export class SiteHeaderComponent {

  constructor() { }

}
