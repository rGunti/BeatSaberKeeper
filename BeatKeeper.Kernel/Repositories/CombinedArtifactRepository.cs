using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeatKeeper.Kernel.Entities;

namespace BeatKeeper.Kernel.Repositories
{
    public class CombinedArtifactRepository : IRepository<Artifact>
    {
        private readonly ArtifactRepository[] _repositories;

        public CombinedArtifactRepository(params ArtifactRepository[] repositories)
        {
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
            File.Delete(entity.FullPath);
        }
    }
}