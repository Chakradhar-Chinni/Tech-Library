
<<h2>> Interpolation Expressions
output image: Interploation demo

{{ }} interplolation syntax can evaluate simple expression, like example below. It's better to not use it for complex calculations


##/src/app/app.component.html
    
<bot-site-header></bot-site-header>
<p> 2+2= {{2+2}} </p>
<bot-home>  </bot-home>
<bot-catalog> </bot-catalog>






    
<<h2>> Binding to component data with Interpolation
output image: One product on Catalog page

1. product.model.ts - Interface IProduct is created
2. catalog.component.ts
    - product variable of type Iproduct is created. 
    - constructor initializes the data for product
3. catalog.component.html
    Interpolation ({{ }}) is used to bind data from the component to the view.



## /src/app/catalog/product.model.ts
export interface IProduct
{
  id: number;
  description: string;
  name: string;
  imageName: string;
  category: string;
  price: number;
  discount: number;
}



## /src/app/catalog/catalog.component.ts
import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';

@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   product: IProduct;

   constructor()
   {
     this.product = {
      id: 1,
      description: "A friendly robot with two eyes and a smile -- great for domestic use",
      name: "Friendly Bot",
      imageName: "head-friendly.png",
      category: "Heads",
      price: 945.0,
      discount: 0.2,
     };
   }
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
          <img src="{{ '/assets/images/robot-parts/' + product.imageName }}" alt="head-friendly.png" />
          <div class="product-info">
            <div class="name"> {{ product.name}} </div>
            <div class="description"> {{product.description}} </div>
            <div class="category">Part Type: {{product.category}} </div>
          </div>
        </div>
        <div class="price">
          <div>${{product.price.toFixed(2)}}</div>
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>




<<h2>> Using Attribute Bindings and Functions
output image: One product on Catalog page

-- Attribute bindings uses square brackets [] 
-- it creates only one way binding from component file to template(html). 2 way binding is not possible. For example, data modified by user on website in a text box will not be sent back to component

1. component.html (using attributes)
    - img uses attributes instead of interpolation
    - [src] [alt]  are kept in [] as per attribute syntax
    - the image will be displayed on image just like previous interpolation implementation
2. component.html (using javascript functions in attributes)
    - img uses getImageUrl() in the component.ts file to retrieve image url path
    - this approach is useful when dealing with business rules


1.
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
          <img [src]=" '/assets/images/robot-parts/'+product.imageName " [alt]="product.name" />
          <div class="product-info">
            <div class="name"> {{ product.name}} </div>
            <div class="description"> {{product.description}} </div>
            <div class="category">Part Type: {{product.category}} </div>
          </div>
        </div>
        <div class="price">
          <div>${{product.price.toFixed(2)}}</div>
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>


2.
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
          <img [src]=" getImageUrl(product)" [alt]="product.name" />
          <div class="product-info">
            <div class="name"> {{ product.name}} </div>
            <div class="description"> {{product.description}} </div>
            <div class="category">Part Type: {{product.category}} </div>
          </div>
        </div>
        <div class="price">
          <div>${{product.price.toFixed(2)}}</div>
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>


## /src/app/catalog/catalog.component.ts
import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';

@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   product: IProduct;

   constructor()
   {
     this.product = {
      id: 1,
      description: "A friendly robot with two eyes and a smile -- great for domestic use",
      name: "Friendly Bot",
      imageName: "head-friendly.png",
      category: "Heads",
      price: 945.0,
      discount: 0.2,
     };
   }

   getImageUrl(product : IProduct)
   {
    return '/assets/images/robot-parts/'+product.imageName;
   }
}













<<h2>> Repeating data with *ngFor
output image: Many products on Catalog page (4 images)

1. catalog.component.ts
    - Iproduct is made as array  Iproduct[]
    - in constructos, this.products has lot of items (code file is not big :))

2. catalog.component.html
    - <li class="product-item" *ngFor = "let product of products" >
    - *ngFor is added to list so all the array items can be repeated
    


## /src/app/catalog.component.ts
import { Component, OnInit } from '@angular/core';
import {IProduct } from './product.model';

@Component({
  selector: 'bot-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
   products: IProduct[];

   constructor()
   {
    this.products =
    [
      {
        id: 1,
        description:
          "A robot head with an unusually large eye and teloscpic neck -- excellent for exploring high spaces.",
        name: "Large Cyclops",
        imageName: "head-big-eye.png",
        category: "Heads",
        price: 1220.5,
        discount: 0.2,
      },
      {
        id: 17,
        description: "A spring base - great for reaching high places.",
        name: "Spring Base",
        imageName: "base-spring.png",
        category: "Bases",
        price: 1190.5,
        discount: 0,
      },
      {
        id: 6,
        description:
          "An articulated arm with a claw -- great for reaching around corners or working in tight spaces.",
        name: "Articulated Arm",
        imageName: "arm-articulated-claw.png",
        category: "Arms",
        price: 275,
        discount: 0,
      },
      {
        id: 2,
        description:
          "A friendly robot head with two eyes and a smile -- great for domestic use.",
        name: "Friendly Bot",
        imageName: "head-friendly.png",
        category: "Heads",
        price: 945.0,
        discount: 0.2,
      },
      {
        id: 3,
        description:
          "A large three-eyed head with a shredder for a mouth -- great for crushing light medals or shredding documents.",
        name: "Shredder",
        imageName: "head-shredder.png",
        category: "Heads",
        price: 1275.5,
        discount: 0,
      },
      {
        id: 16,
        description:
          "A single-wheeled base with an accelerometer capable of higher speeds and navigating rougher terrain than the two-wheeled variety.",
        name: "Single Wheeled Base",
        imageName: "base-single-wheel.png",
        category: "Bases",
        price: 1190.5,
        discount: 0.1,
      },
      {
        id: 13,
        description: "A simple torso with a pouch for carrying items.",
        name: "Pouch Torso",
        imageName: "torso-pouch.png",
        category: "Torsos",
        price: 785,
        discount: 0,
      },
      {
        id: 7,
        description:
          "An arm with two independent claws -- great when you need an extra hand. Need four hands? Equip your bot with two of these arms.",
        name: "Two Clawed Arm",
        imageName: "arm-dual-claw.png",
        category: "Arms",
        price: 285,
        discount: 0,
      },

      {
        id: 4,
        description: "A simple single-eyed head -- simple and inexpensive.",
        name: "Small Cyclops",
        imageName: "head-single-eye.png",
        category: "Heads",
        price: 750.0,
        discount: 0,
      },
      {
        id: 9,
        description:
          "An arm with a propeller -- good for propulsion or as a cooling fan.",
        name: "Propeller Arm",
        imageName: "arm-propeller.png",
        category: "Arms",
        price: 230,
        discount: 0.1,
      },
      {
        id: 15,
        description: "A rocket base capable of high speed, controlled flight.",
        name: "Rocket Base",
        imageName: "base-rocket.png",
        category: "Bases",
        price: 1520.5,
        discount: 0,
      },
      {
        id: 10,
        description: "A short and stubby arm with a claw -- simple, but cheap.",
        name: "Stubby Claw Arm",
        imageName: "arm-stubby-claw.png",
        category: "Arms",
        price: 125,
        discount: 0,
      },
      {
        id: 11,
        description:
          "A torso that can bend slightly at the waist and equiped with a heat guage.",
        name: "Flexible Gauged Torso",
        imageName: "torso-flexible-gauged.png",
        category: "Torsos",
        price: 1575,
        discount: 0,
      },
      {
        id: 14,
        description: "A two wheeled base with an accelerometer for stability.",
        name: "Double Wheeled Base",
        imageName: "base-double-wheel.png",
        category: "Bases",
        price: 895,
        discount: 0,
      },
      {
        id: 5,
        description:
          "A robot head with three oscillating eyes -- excellent for surveillance.",
        name: "Surveillance",
        imageName: "head-surveillance.png",
        category: "Heads",
        price: 1255.5,
        discount: 0,
      },
      {
        id: 8,
        description: "A telescoping arm with a grabber.",
        name: "Grabber Arm",
        imageName: "arm-grabber.png",
        category: "Arms",
        price: 205.5,
        discount: 0,
      },
      {
        id: 12,
        description: "A less flexible torso with a battery gauge.",
        name: "Gauged Torso",
        imageName: "torso-gauged.png",
        category: "Torsos",
        price: 1385,
        discount: 0,
      },
      {
        id: 18,
        description:
          "An inexpensive three-wheeled base. only capable of slow speeds and can only function on smooth surfaces.",
        name: "Triple Wheeled Base",
        imageName: "base-triple-wheel.png",
        category: "Bases",
        price: 700.5,
        discount: 0,
      },
    ];
   }

   getImageUrl(product : IProduct)
   {
    return '/assets/images/robot-parts/'+product.imageName;
  }
}




## /src/app/catalog.component.html
<div class="container">
  <div class="filters">
    <a class="button">Heads</a>
    <a class="button">Arms</a>
    <a class="button">Torsos</a>
    <a class="button">Bases</a>
    <a class="button">All</a>
  </div>

  <ul class="products">
    <li class="product-item" *ngFor = "let product of products" >
      <div class="product">
        <div class="product-details">
          <img [src]=" getImageUrl(product)" [alt]="product.name" />
          <div class="product-info">
            <div class="name"> {{ product.name}} </div>
            <div class="description"> {{product.description}} </div>
            <div class="category">Part Type: {{product.category}} </div>
          </div>
        </div>
        <div class="price">
          <div>${{product.price.toFixed(2)}}</div>
          <button class="cta">Buy</button>
        </div>
      </div>
    </li>
  </ul>
</div>

















































