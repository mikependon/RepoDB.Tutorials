module FSharpRepoDB

type Person ( id: int64,
    name: string,
    age: int,
    address: string,
    isActive: bool ) =
    member this.Id = id
    member this.Name = name
    member this.Age = age
    member this.Address = address
    member this.IsActive = isActive