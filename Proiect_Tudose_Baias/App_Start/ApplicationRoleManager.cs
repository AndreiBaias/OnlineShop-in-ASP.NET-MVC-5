using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Proiect_Tudose_Baias.Models;

namespace Proiect_Tudose_Baias
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) :
        base(store)
        { }

        public static ApplicationRoleManager
        Create(IdentityFactoryOptions<ApplicationRoleManager> options,
        IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());

            return new ApplicationRoleManager(roleStore);
        }
    }

}