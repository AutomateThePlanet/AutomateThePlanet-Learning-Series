using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubActions.Pages
{
    public record ClientInfo(string FirstName,
                             string LastName,
                             string Username,
                             string Email,
                             string Address1,
                             string Address2,
                             int Country,
                             int State,
                             string Zip,
                             string CardName,
                             string CardNumber,
                             string CardExpiration,
                             string CardCVV);
}
