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
