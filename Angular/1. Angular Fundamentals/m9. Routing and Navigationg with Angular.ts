<<h2>> Introduction

(ignore)
cart component is fetched from course-resources
 cartservice.ts is also available here



Notes: 
Routing allows to navigate to different components based on URL








 
 <<h2>> Adding Routing to an Existing Project

cmd> ng generate module app-routing --flat

1. above cmd creates app-routing.module.ts at /src/app
2. app-routing -> standard way to organize routes (at project creation CLI would create this, if routing =yes is choosen)
3. --flat  -> creates new module next to app.module.ts


==> app-routing.module.ts
1. import RouterModule,Routes 
2. routes array of type Routes. All routes to be added here
3. NgModule: decorator marks the class as Angular Module
3. imports:
     RouterModule.forRoot(routes) sets up the router with the provided routes.
     forRoot() is used in the root module of the app.
4. exports:
    Exports RouterModule so that routing directives (like routerLink) can be used in components that import this module.

## src\app\app-routing.module.ts

import { NgModule } from '@angular/core';
import {RouterModule,Routes} from '@angular/router';

const routes : Routes =[];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }



1. <router-outlet> tag navigates to different components and render HTML routes based on URL
## src\app\app.component.html

 <bot-site-header></bot-site-header>
<router-outlet></router-outlet>









 <<h2>> Creating Routes for Navigation
1. Order of routing matters, as Angular check the route one by one
2. Routes are added to routes array

Output images:
route localhost:4200/home
route localhost:4200/catalog
route localhost:4200/cart


## src\app\app-routing.module.ts
import { NgModule } from '@angular/core';
import {RouterModule,Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import {CatalogComponent} from './catalog/catalog.component';
import {CartComponent} from './cart/cart.component';

const routes : Routes =[
  {path: 'home',component:HomeComponent},
  {path: 'catalog',component:CatalogComponent},
  {path: 'cart',component:CartComponent}

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }














<<h2>> Creating Redirect Routes

1. routes array has new route with 'redirectTo'
2. pathMatch property
    {path:'',redirectTo:'/home',pathMatch:'full'}
      - http://localhost:4200/ → ✅ Redirects to /home
      - URL: http://localhost:4200/abc → ❌ No redirect (because the full path is not empty)
    {path:'',redirectTo:'/home',pathMatch:'prefix'}
      - URL: http://localhost:4200/ → ✅ Redirects to /home
      - URL: http://localhost:4200/anything → ✅ Also redirects to /home (which is usually not what you want)
      - 'prefix' can cause infinite loops. Not Recommended. As order of routing matters, if this route is kept at top, everything would be re-directed to /home because it '' is prefix of every string in JS
      
Notes:
   '/about'.startsWith('') // true
    True because, The empty string is a valid prefix of any string and matches the position before first character

## src\app\app-routing.module.ts
import { NgModule } from '@angular/core';
import {RouterModule,Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import {CatalogComponent} from './catalog/catalog.component';
import {CartComponent} from './cart/cart.component';

const routes : Routes =[
  {path: 'home',component:HomeComponent},
  {path: 'catalog',component:CatalogComponent},
  {path: 'cart',component:CartComponent},
  {path:'',redirectTo:'/home',pathMatch:'full'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }












<<h2>> Linking to Routes on HTML pages
1. html would have href, angular uses routerLink with in <a> tag

 ## src\app\site-header\site-header.component.html
<div class="container">
  <div class="left">
    <img class="logo" src="/assets/images/logo.png" alt="Logo" />
    <a routerLink="/home">Home</a>
    <a routerLink="/catalog">Catalog</a>
    <div class="cart">
      <a routerLink="/cart">Cart</a>
    </div>
  </div>
  <div class="right">
    <a href="">Sign In</a>
    <a href="" class="cta">Register</a>
  </div>
</div>











<<h2>> Navigating to routes from .ts files

Scenario: when Buy is clicked, user should be taken to carts page

1. import {Router} from angular/router
2. inject router as a service to the constructor
3. router.navigate() to go to cart page


## src\app\catalog\catalog.component.ts
import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';
import { ProductService } from './product.service';
import { CartService } from '../cart/cart.service';
import {Router} from '@angular/router'; //new


@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   products: any;
   filter : string = '';
   //cart : IProduct[] = [];
    // private cartSvc: CartService = inject(CartService);

   constructor(private cartSvc: CartService,private productSvc: ProductService, private router: Router) //new
   {

   }


   ngOnInit()
   {
    this.productSvc.getProducts().subscribe(products =>{
      this.products = products;
    })
   }

   addToCart(product: IProduct)
   {
    this.cartSvc.add(product)
    this.router.navigate(['/cart']) //new
   }

   getdiscountedClasses(product : IProduct)
   {
    return{ strikethrough:product.discount>0 };
        /*
        if(product.discount > 0)
            {
              return 'strikethrough';
            }
            else
            {
              return '';
            }
        */
   }



   getFilteredProducts()
   {
    return this.filter === ''
    ? this.products
    : this.products.filter((product:any) => product.category === this.filter)
   }
}







to learn: using snapshots with routing


<<h2>> Defining and accessing Route parameters

1. home.component.html
   - After adding routerLink they respond.
   - for example, after clicking robot arms website goes to catalog page & filters arms, url would be http://localhost:4200/catalog/Arms [Image: route: catalog/arms]
   - filters in the catalog page donot update url because filters are using (click) event in catalog.component.html [Image: clicked Bases but url shows /Arms]
   
2. catalog.component.html
   - to solve this routerLink should be used instead of (click) [Image: clicked Torsos and url also responds and shows Torsos]

3. app-routing.module.ts
   - catalog/:filter it accepts additional filters after catalog

4. catalog.component.ts
   - Activate route imported.
   - subscribed to params

## src\app\home\home.component.html
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
      <a routerLink= "/catalog/Heads" class="part"> //new
        <img src="/assets/images/robot-parts/head-shredder.png" alt="Robot Heads" />
        <div>ROBOT HEADS</div>
      </a>
    </li>
    <li> 
      <a routerLink= "/catalog/Arms" class="part"> //new
        <img src="/assets/images/robot-parts/arm-articulated-claw.png" alt="Robot Arms" />
        <div>ROBOT ARMS</div>
      </a>
    </li>
    <li>
      <a routerLink= "/catalog/Torsos" class="part"> //new
        <img src="/assets/images/robot-parts/torso-gauged.png" alt="Robot Torsos" />
        <div>ROBOT TORSOS</div>
      </a>
    </li>
    <li>
      <a routerLink= "/catalog/Bases" class="part"> //new
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





## src\app\catalog\catalog.component.html
 <div class="container">
  <div class="filters">
    <a class="button " routerLink="/catalog/Heads">Heads</a> //new 
    <a class="button"  routerLink="/catalog/Arms">Arms</a> //new
    <a class="button"  routerLink="/catalog/Torsos">Torsos</a> //new
    <a class="button"  routerLink="/catalog/Bases">Bases</a> //new
    <a class="button"  routerLink="/catalog/Arms"  >All</a> //new
  </div> 

  <ul class="products">
    <li class="product-item" *ngFor = "let product of getFilteredProducts()" >
      <bot-product-details
      [product] = "product"
      (buy)="addToCart(product)"
      ></bot-product-details>
    </li>
  </ul>
</div>




## src\app\app-routing.module.ts
import { NgModule } from '@angular/core';
import {RouterModule,Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import {CatalogComponent} from './catalog/catalog.component';
import {CartComponent} from './cart/cart.component';

const routes : Routes =[
  {path: 'home',component:HomeComponent},
  {path: 'catalog/:filter',component:CatalogComponent}, //new
  {path: 'cart',component:CartComponent},
  {path:'',redirectTo:'/home',pathMatch:'full'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }





## src\app\catalog\catalog.component.ts
import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';
import { ProductService } from './product.service';
import { CartService } from '../cart/cart.service';
import {ActivatedRoute, Router} from '@angular/router';


@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   products: any;
   filter : string = '';
   //cart : IProduct[] = [];
    // private cartSvc: CartService = inject(CartService);

   constructor(
              private cartSvc: CartService,
              private productSvc: ProductService,
              private router: Router,
              private route: ActivatedRoute //new
              ) {  }


   ngOnInit()
   {
    this.productSvc.getProducts().subscribe(products =>{
      this.products = products;
    })
    this.route.params.subscribe((params)=>{ //new
      this.filter = params['filter'] ?? '';
    })
   }

   addToCart(product: IProduct)
   {
    this.cartSvc.add(product)
    this.router.navigate(['/cart'])
   }

   getdiscountedClasses(product : IProduct)
   {
    return{ strikethrough:product.discount>0 };
        /*
        if(product.discount > 0)
            {
              return 'strikethrough';
            }
            else
            {
              return '';
            }
        */
   }



   getFilteredProducts()
   {
    return this.filter === ''
    ? this.products
    : this.products.filter((product:any) => product.category === this.filter)
   }
}





<<h2>> Accessing Query String Parameters
Output image: Heads Catalog using [queryParams]
enhancing routing using query parameters (previous module is enhanced very well using query parameters)

1. app-routing.module.ts
   - path:catalog. query paramaters are optional.

2. home.component.html
   - [queryParams] are added on <a> tag along with [routerLink]

3. catalog.component.html
  - [queryParams] are added on <a> tag along with [routerLink]
  - for All button, [queryParams] are not added so this route navigates to /catalog page. 

4. catalog.component.ts
  - .subscribe() uses queryParams instead of Params  


## src\app\app-routing.module.ts
import { NgModule } from '@angular/core';
import {RouterModule,Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import {CatalogComponent} from './catalog/catalog.component';
import {CartComponent} from './cart/cart.component';

const routes : Routes =[
  {path: 'home',component:HomeComponent},
  {path: 'catalog', component:CatalogComponent}, //new
  {path: 'cart',component:CartComponent},
  {path:'',redirectTo:'/home',pathMatch:'full'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }




## src\app\home\home.component.html
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
      <a routerLink= "/catalog" [queryParams]="{filter:'Heads'}" class="part"> //new
        <img src="/assets/images/robot-parts/head-shredder.png" alt="Robot Heads" />
        <div>ROBOT HEADS</div>
      </a>
    </li>
    <li>
      <a routerLink= "/catalog" [queryParams]="{filter:'Arms'}"  class="part"> //new
        <img src="/assets/images/robot-parts/arm-articulated-claw.png" alt="Robot Arms" />
        <div>ROBOT ARMS</div>
      </a>
    </li>
    <li>
      <a routerLink= "/catalog/Torsos" [queryParams]="{filter:'Torsos'}" class="part"> //new
        <img src="/assets/images/robot-parts/torso-gauged.png" alt="Robot Torsos" />
        <div>ROBOT TORSOS</div>
      </a>
    </li>
    <li>
      <a routerLink= "/catalog/Bases" [queryParams]="{filter:'Bases'}" class="part"> //new
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

 

## src\app\catalog\catalog.component.html
<div class="container">
  <div class="filters">
    <a class="button " routerLink="/catalog" [queryParams]="{filter:'Heads'}">Heads</a>
    <a class="button"  routerLink="/catalog" [queryParams]="{filter:'Arms'}">Arms</a>
    <a class="button"  routerLink="/catalog" [queryParams]="{filter:'Torsos'}">Torsos</a>
    <a class="button"  routerLink="/catalog" [queryParams]="{filter:'Bases'}">Bases</a>
    <a class="button"  routerLink="/catalog" >All</a>
  </div>

  <ul class="products">
    <li class="product-item" *ngFor = "let product of getFilteredProducts()" >
      <bot-product-details
      [product] = "product"
      (buy)="addToCart(product)"
      ></bot-product-details>
    </li>
  </ul>
</div>


 
## src\app\catalog\catalog.component.ts
import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';
import { ProductService } from './product.service';
import { CartService } from '../cart/cart.service';
import {ActivatedRoute, Router} from '@angular/router';


@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   products: any;
   filter : string = '';
   //cart : IProduct[] = [];
    // private cartSvc: CartService = inject(CartService);

   constructor(
              private cartSvc: CartService,
              private productSvc: ProductService,
              private router: Router,
              private route: ActivatedRoute
              ) {  }


   ngOnInit()
   {
    this.productSvc.getProducts().subscribe(products =>{
      this.products = products;
    })
    this.route.queryParams.subscribe((params)=>{ //new
      this.filter = params['filter'] ?? '';
    })
   }

   addToCart(product: IProduct)
   {
    this.cartSvc.add(product)
    this.router.navigate(['/cart'])
   }

   getdiscountedClasses(product : IProduct)
   {
    return{ strikethrough:product.discount>0 };
        /*
        if(product.discount > 0)
            {
              return 'strikethrough';
            }
            else
            {
              return '';
            }
        */
   }


   getFilteredProducts()
   {
    return this.filter === ''
    ? this.products
    : this.products.filter((product:any) => product.category === this.filter)
   }
}



<<h2>> Styling Active Links
Output Image: routerLink for styling

1. in <a> tag use routerLinkActive="active"
2. active is the css class name in styles.css (global styles)


## src\app\catalog\catalog.component.html
<div class="container">
  <div class="filters">
    <a routerLinkActive="active" class="button"  routerLink="/catalog" [queryParams]="{filter:'Heads'}">Heads</a> //new 
    <a routerLinkActive="active" class="button"  routerLink="/catalog" [queryParams]="{filter:'Arms'}">Arms</a> //new
    <a routerLinkActive="active" class="button"  routerLink="/catalog" [queryParams]="{filter:'Torsos'}">Torsos</a> //new
    <a routerLinkActive="active" class="button"  routerLink="/catalog" [queryParams]="{filter:'Bases'}">Bases</a> //new
    <a routerLinkActive="active" class="button"  routerLink="/catalog" >All</a> //new
  </div>

  <ul class="products">
    <li class="product-item" *ngFor = "let product of getFilteredProducts()" >
      <bot-product-details
      [product] = "product"
      (buy)="addToCart(product)"
      ></bot-product-details>
    </li>
  </ul>
</div>

 


 ## src\app\site-header\site-header.component.html
 <div class="container">
  <div class="left">
    <img class="logo" src="/assets/images/logo.png" alt="Logo" />
    <a routerLinkActive="active" routerLink="/home">Home</a> //new
    <a routerLinkActive="active" routerLink="/catalog">Catalog</a> //new
    <div class="cart"> 
      <a routerLinkActive="active" routerLink="/cart">Cart</a> //new
    </div>
  </div>
  <div class="right">
    <a href="">Sign In</a>
    <a href="" class="cta">Register</a>
  </div>
</div>


 ## styles.css
a {
  color: #444;
  text-decoration: none;
}





