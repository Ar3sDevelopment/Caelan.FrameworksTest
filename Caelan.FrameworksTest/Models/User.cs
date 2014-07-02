using System.ComponentModel.DataAnnotations.Schema;
using Caelan.Frameworks.DAL.Interfaces;

namespace Caelan.FrameworksTest.Models
{
    public class User : IEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
