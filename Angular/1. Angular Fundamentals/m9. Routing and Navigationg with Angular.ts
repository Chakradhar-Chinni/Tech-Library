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













