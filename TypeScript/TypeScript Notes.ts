
let id: number=5
let language: string = 'Type Script'
let isPublished: boolean = true
let x: any = 'Hello'
x = 10

let age: number
age =23

let ids: number[] = [1,2,3,4,5]
let any: any[] = [21,true,'script']

//Tuple
let person: [number,boolean,string] = [19,false,'Youtube']

//Tuple Array
let employee: [number,string][]

employee=[
    [1,'james'], [2,'brad'], [3,'jon']
]


//Union (helps variables to hold 2 data types, ex: id can be string or number)
let pid: number | string
pid=12
pid='A13qe'

//Enums 
// Enums defines a set of named constants
// TypeScript provides numeric and string-based enums

enum Dir1
{
    Up,
    Down,
    Left,
    Right, 
}

console.log(Dir1.Up);  
console.log(Dir1.Down);  
console.log(Dir1.Left);  
console.log(Dir1.Right);  

enum Dir2
{
    Up=1,
    Down,
    Left,
    Right, 
}
console.log(Dir2.Up);  
console.log(Dir2.Down);  
console.log(Dir2.Left);  
console.log(Dir2.Right);   

enum Dir3
{
    Up='Move up',
    Down='Move Down',
    Left='Move Left',
    Right='Move Right',
}

console.log(Dir3.Up);  
console.log(Dir3.Down);  
console.log(Dir3.Left);  
console.log(Dir3.Right);


//OBJECTS
type user = {
    id: number
    name: string
}

const user: User = { 
    id:1,
    name: 'John'
}

//Type Assertion
let cid: any =1
// let customerId = <number>cid
let customerId = cid as number 

//Functions

function addNum(x, y)  // x, y can be of any type
{ 
    return x+y; 
}

function addNum(x, y): number { // return type for this function is number
    return x+y;
}

function addNum(x:number, y:number) // x, y can be number only
{
    return x+y;
}

function addNum(x:number, y:number): number // x, y, return type of function can be number only
{
    return x+y;
}


// Function Interface (looks similar to structures-in-c)

interface UserInterface
{  
    id: number   
    name: string   
}

const user1: UserInterface ={
    id: 12,
    name: 'Jane',
}
console.log(user1.id);
console.log(user1.name);
``````
interface UserInterface
{  
    Rid: number   
    age?: number // ? denotes optional
}

const user1: UserInterface ={ 
    Rid: 12,
}
console.log(user1.Rid); 
console.log(user1.age); // prints undefined as it is not defined


//Classes 
In classes, constructors are automatically called


class demo
{
    constructor(){ 
        console.log("Hello")
    }
}

const obj1 = new demo()
const obj2 = new demo() 
````````
class Person
{
    id: number
    name: string

    constructor(id:number, name:string){ 
        this.id = id  // this indicates current class
        this.name=name  
    }
}

const brad = new Person(1, 'Brad Traversy') //obj1
const mike = new Person(2, 'Mike Jordan')  //obj2

//Data Modifiers

