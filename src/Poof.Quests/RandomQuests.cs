using System;
using System.Collections.Generic;
using System.Reflection;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Xml;

namespace Poof.PrivateQuests
{
    /// <summary>
    /// A list of random quests as xml, with the given count
    /// </summary>
    public sealed class RandomQuests : ListEnvelope<IXML>
    {
        /// <summary>
        /// A list of random quests as xml, with the given count
        /// </summary>
        public RandomQuests(int count) : base(() =>
            {
                var assembly = Assembly.GetAssembly(typeof(RandomQuests));
                var names = assembly.GetManifestResourceNames();
                if(names.Length < count)
                {
                    throw new InvalidOperationException($"Unable to retrieve a count of '{count}' quests, " +
                        $"because there are only '{names.Length}' available.");
                }
                var quests = new List<IXML>();
                var knownIds = new List<int>();
                var random = new Random();
                while(quests.Count < count)
                {
                    var index = random.Next(names.Length);
                    if(!knownIds.Contains(index))
                    {
                        knownIds.Add(index);
                        quests.Add(
                            new XMLCursor(
                                new InputOf(
                                    assembly.GetManifestResourceStream(names[index])
                                )
                            )
                        );
                    }
                }
                return quests;
            }, 
            false
        )
        { }
    }
}
