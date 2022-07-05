module Tests


open LanguageExt
open Luhn.API
open Luhn.Types
open Xunit

[<Theory>]
[<InlineData("4539319503436467")>]
let ``Returns true for valid inputs`` testString =
    let result = Validate ( testString , ValidationMode.CheckDigitExcluded )
    
    Assert.IsAssignableFrom<Fin<bool>>( result ) |> ignore
    Assert.True( result.IsSucc )
    result.Iter( fun x -> Assert.True( x ) )
