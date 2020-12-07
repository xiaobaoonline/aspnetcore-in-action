using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter13_Samples
{
	public class RewriteTextFileRequests : IRule
	{
		public void ApplyRule(RewriteContext context)
		{
			var request = context.HttpContext.Request;

			if (request.Path.Value.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
			{
				context.Result = RuleResult.SkipRemainingRules;
				request.Path = "/file.txt";
			}
		}
	}
}
