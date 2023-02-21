using java.net;

namespace Mixins
{
    public abstract class class290Mod : class290
    {
        //[MixinSettings(TargetClass = "class290", TargetMethod = "loadWorlds", Replace =true)]
        internal static bool loadWorldsMod()
        {
            if (ClanChannel.World_request == null)
            {
                var url = "http://www.runescape.com/g=oldscape/slr.ws?order=LPWM";
                //var url = new URL(VerticalAlignment.field1594)
                System.Console.WriteLine(new URL(VerticalAlignment.field1594));
                ClanChannel.World_request = class245.urlRequester.request(new URL(url));
            }
            else if (ClanChannel.World_request.isDone())
            {
                Buffer buffer = new Buffer(ClanChannel.World_request.getResponse());
                buffer.readInt();
                World.World_count = buffer.readUnsignedShort();
                class88.World_worlds = new World[World.World_count];
                for (int i = 0; i < World.World_count; ++i)
                {
                    World[] world_worlds = class88.World_worlds;
                    int n = i;
                    World world = new World();
                    world_worlds[n] = world;
                    World world2 = world;
                    world2.id = buffer.readUnsignedShort();
                    world2.properties = buffer.readInt();
                    world2.host = buffer.readStringCp1252NullTerminated();
                    world2.activity = buffer.readStringCp1252NullTerminated();
                    world2.location = buffer.readUnsignedByte();
                    world2.population = buffer.readShort();
                    world2.index = i;
                }
                class156.sortWorlds(
                    class88.World_worlds,
                    0,
                    class88.World_worlds.Length - 1,
                    World.World_sortOption1,
                    World.World_sortOption2
                );
                ClanChannel.World_request = null;
                return true;
            }

            return false;
        }
    }
}
