module Luhn.API


open LanguageExt
open Luhn.Core
open System.Text.RegularExpressions
open Luhn.Types


let Validate( input : string , mode : ValidationMode ) =
    let input' = input.Trim()
    
    if Regex.IsMatch( input' , "\D" ) then
        Fin<bool>.Fail "Illegal character(s) in input string"
    else
        match validate mode input' with
        | Error errorValue -> errorValue |> Fin<bool>.Fail
        | Ok resultValue   -> resultValue 