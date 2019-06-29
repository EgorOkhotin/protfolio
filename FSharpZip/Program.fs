// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics



let isExactDivide number divider = ((number % divider) = 0)

let getDividers number = 
  let isExact num = isExactDivide number num
  let rec dividers (current:int) : list<int>= 
    if current = number then [current]
    else
        if isExact current then (current :: (dividers (current+1)))
        else (dividers (current+1))
  dividers 1

let rainDrops number : string= 
    let dividers = getDividers number
    let exist n = dividers |> List.exists (fun x -> x = n)
    (if exist 3 then "Pling" else "") +
    (if exist 5 then "Plang" else "") +
    (if exist 7 then "Plong" else "")


let isArmstrongNumber number = 
    let rec getCapacity (current:int) :int= 
        if((number/current) = 0) then 1
        else 1 + getCapacity(current * 10)
    let calculatePow num power :int= 
        let toFloat n = (float)n
        Convert.ToInt32(Math.Pow(toFloat num, toFloat power))
    let getNumbersPowSum = 
        let capacity = (getCapacity 10) 
        let rec calculateSum current = 
            if(current/10 = 0) then calculatePow current capacity
            else (calculatePow (current % 10) capacity) + calculateSum(current / 10)
        calculateSum number 
    if((getNumbersPowSum) = number) then true
    else false


let collatzConjecture number = 
    let isEven n = n % 2 = 0
    let toEven n = n*3 + 1
    let rec getSetpsCount current = 
        if(current = 1) then 1
        else 
            if(isEven current) then 1 + getSetpsCount(current/2)
            else 1 + getSetpsCount(toEven current)
    if isEven number then getSetpsCount number
    else getSetpsCount (toEven number)

let Hamming first second = 
    let toChars s = Seq.toList s
    let firstList = toChars first
    let secondList = toChars second
    let rec compareLists (l1:seq<char>) (l2:seq<char>) current : int= 
        if current >= Seq.length l1 then 0
        else 
            if (Seq.item current l1) = (Seq.item current l2) then 0 + compareLists l1 l2 (current+1)
            else 1 + compareLists l1 l2 (current+1)
    compareLists firstList secondList 0

let dartz (x:float,y:float) : int= 
    let getRadius = (Math.Sqrt(x*x + y*y))
    let inTheOuterZone radius = (radius>5.0)&&(radius <= 10.0)
    let inTheMiddleZone radius = (radius>1.0)&&(radius<=5.0)
    let notInTarget radius = (radius > 10.0)
    let getPoints radius = 
        if notInTarget radius then 0
        else 
            if inTheOuterZone radius then 1
            else
                if inTheMiddleZone radius then 5
                else 10
    getPoints getRadius
    
        
[<EntryPoint>]
let main argv =
    //(getDividers 28)
    //|> List.iter (printf "%i ")
    printfn "%i " (dartz (0.5,0.5))
    0 // return an integer exit code
