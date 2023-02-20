namespace Mixins

type class153mod(value: class142) =
    inherit class153(value)

    [<MixinSettings(TargetClass = "class153", TargetMethod = "clockNow", Replace = true)>]
    static member clockNow() =
        let mutable currentTimeMillis = java.lang.System.currentTimeMillis()
        if currentTimeMillis < class286.field2687 then
            class286.field2688 <- class286.field2688 + (class286.field2687 - currentTimeMillis)
        class286.field2687 <- currentTimeMillis
        class286.field2688 + currentTimeMillis