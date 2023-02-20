namespace Mixins

type MixinSettings() =
    inherit System.Attribute()

    member val TargetClass : string = "" with get, set
    member val TargetMethod : string = "" with get, set
    member val Replace : bool = false with get, set