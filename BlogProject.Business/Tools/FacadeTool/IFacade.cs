using BlogProject.Business.Tools.LogTool;
using Microsoft.Extensions.Caching.Memory;

namespace BlogProject.Business.Tools.FacadeTool
{
    public interface IFacade
    {
        public IMemoryCache MemoryCache { get; set; }
        public ICustomLogger CustomLogger { get; set; }
    }
}
