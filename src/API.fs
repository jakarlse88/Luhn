module Luhn.API


open LanguageExt
open Luhn.Core
open Luhn.Types
open System.Text.RegularExpressions


let Validate( input : string , mode : ValidationMode ) =
    let input' = input.Trim()
    
    if Regex.IsMatch( input' , "\D" ) then
        Fin<bool>.Fail "Illegal character(s) in input string"
    else
        match validate mode input' with
        | Error errorValue -> errorValue  |> Fin<bool>.Fail
        | Ok resultValue   -> resultValue |> Fin<bool>.Succ