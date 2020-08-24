namespace FSharpRepoDB

/// Group your types and other constructs that allow you
/// to represent your domain
module Types =
    //[<CLIMutable>]
    type Person = 
        { Id: int64
          Name: string
          Age: int
          Address: string
          IsActive: bool }