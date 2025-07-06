
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


