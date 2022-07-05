module internal Luhn.Core


open FSharp.Collections
open FSharp.Core
open Luhn.Types


let private processDigit shouldProcess i =
    if shouldProcess then 
        let i' = i * 2
    
        if i' > 9
            then i' - 9
            else i'
    
    else
        i
        
        
let private applyFnAndPrepend i m fn =
    let i' = fn i
    i' :: m
        
        
let private processDigits mode input =
    let rec loop input' shouldProcess acc =
        match input' with
        | [ ]     -> acc
        | [ x ]   -> processDigit shouldProcess 
                     |> applyFnAndPrepend x acc
                     |> loop [] ( not shouldProcess ) 
        | x :: xs -> processDigit shouldProcess
                     |> applyFnAndPrepend x acc
                     |> loop xs ( not shouldProcess )
                     
    loop input ( mode = ValidationMode.CheckDigitIncluded ) []
    
    
let private mod10 i =
    i % 10 = 0
    
    
let private luhn ( input : string ) mode =
    let processDigits' = processDigits mode
    
    input
        |> Seq.map ( fun x -> x.ToString() |> int )
        |> Seq.rev
        |> Seq.toList 
        |> processDigits' 
        |> List.reduce ( fun acc i -> acc + i )
        |> mod10
        
        
let validate mode input =
    try
        luhn input mode
        |> Result.Ok
    with ex ->
        Result.Error ex.Message 