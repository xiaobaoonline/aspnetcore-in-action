using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter07_Samples.Pages
{
    public class AntiForgeryDemoModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Content("提交成功");
            }
            return Content("校验不通过");
        }

        [BindProperty]
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string Name { get; set; }
    }
}
