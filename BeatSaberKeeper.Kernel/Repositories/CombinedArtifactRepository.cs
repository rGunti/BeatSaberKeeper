using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeatSaberKeeper.Kernel.Entities;
using Serilog;

namespace BeatSaberKeeper.Kernel.Repositories
{
    public class CombinedArtifactRepository : IRepository<Artifact>
    {
        private readonly ArtifactRepository[] _repositories;

        public CombinedArtifactRepository(params ArtifactRepository[] repositories)
        {
            Log.Verbose($"Constructing new Combined Artifact Repository for {repositories.Length} repositories");
            _repositories = repositories;
        }

        public IEnumerable<Artifact> GetAll()
        {
            return _repositories.SelectMany(r => r.GetAll());
        }

        public Artifact Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Artifact entity)
        {
            Log.Debug($"Deleting artifact {entity.Name} ({entity.FullPath})");
            File.Delete(entity.FullPath);
        }
    }
}