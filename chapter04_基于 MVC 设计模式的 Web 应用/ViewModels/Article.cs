using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class Article
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public List<ArticleSection> Sections { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
