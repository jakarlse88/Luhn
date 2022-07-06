module Jakarlse.Validation.API


open Jakarlse.Validation.Luhn.Types
open Jakarlse.Validation.Luhn.Core
open LanguageExt
open System.Text.RegularExpressions


let Luhn( input : string , mode : ValidationMode ) =
    let input' = input.Trim()

    if Regex.IsMatch( input' , "\D" ) then
        Fin<bool>.Fail "Illegal character(s) in input string"
    else
        match validate mode input' with
        | Error errorValue -> errorValue  |> Fin<bool>.Fail
        | Ok resultValue   -> resultValue |> Fin<bool>.Succ
