using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisarisInfrastructure
{
    public class Tags
    {
        public DisarisContext _context { get; set; }

        public Tags(DisarisContext context)
        {
            _context = context;
        }

        public async Task AddTag(string tagName, string content)
        {
            _context.Add(new Tag { Name = tagName, Content = content });

            await _context.SaveChangesAsync();
        }

        public async Task<Tag> GetTag(string tagName)
        {
            var tag = await _context.Tags.FindAsync(tagName);

            if (tag == null)
                return null;

            return tag;
        }

        public async Task<int> DeleteTag(string tagName)
        {
            var tag = await _context.Tags.FindAsync(tagName);

            if (tag == null)
                return 0;

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
