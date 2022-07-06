module Tests


open Jakarlse.Validation.API
open Jakarlse.Validation.Luhn.Types
open LanguageExt
open Xunit


[<Theory>]
[<InlineData("4539319503436467")>]
[<InlineData("4539319503436467      ")>]
[<InlineData("0004539319503436467")>]
[<InlineData("    4539319503436467")>]
[<InlineData("    4539319503436467      ")>]
let ``Returns Fin.Succ for valid inputs that pass Luhn validation`` testString =
    let result = Luhn ( testString , ValidationMode.CheckDigitExcluded )
    
    Assert.IsAssignableFrom<Fin<bool>>( result ) |> ignore
    Assert.True( result.IsSucc )
    
    result.Iter( fun x -> Assert.True( x ) )


[<Theory>]
[<InlineData("5539319503436467")>]
[<InlineData("5539319503436467    ")>]
[<InlineData("    5539319503436467")>]
[<InlineData("000005539319503436467")>]
[<InlineData("      000005539319503436467")>]
let ``Returns Fin.Succ for valid inputs that do not pass Luhn validation`` testString =
    let result = Luhn ( testString , ValidationMode.CheckDigitExcluded )

    Assert.IsAssignableFrom<Fin<bool>>( result ) |> ignore
    Assert.True( result.IsSucc )
    
    result.Iter( fun x -> Assert.False( x ) )
    

[<Theory>]
[<InlineData("4539 3195 0343 6467")>]
[<InlineData("/4539319503436467")>]
[<InlineData("-4539319503436467")>]
let ``Returns Fin.Error for invalid inputs`` testString =
    let result = Luhn ( testString , ValidationMode.CheckDigitExcluded )
    
    Assert.IsAssignableFrom<Fin<bool>>( result ) |> ignore
    Assert.True( result.IsFail )