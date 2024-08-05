using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_site1.Domain.Entities
{
    public class Administrator : IEnumerable<Administrator>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }  // One-to-One relationship

        public IEnumerator<Administrator> GetEnumerator()
        {
            yield return this; //  Возвращаем  текущий  объект  Product
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
