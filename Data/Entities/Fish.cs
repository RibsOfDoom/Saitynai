using System.ComponentModel.DataAnnotations;

namespace L1_Zvejyba.Data.Entities
{
    public class Fish
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // 1 - Ziema
        // 2 - Pavasarus
        // 3 - Vasara
        // 4 - Ruduo
        public int Season {  get; set; }

        /// <summary>
        /// Fish catching hours in military time 0-23
        /// </summary>
        public int TimeFrom { get; set; }
        public int TimeTo { get; set; }

        public Body body { get; set; }
        public string bodyName { get; set; }
    }
}
