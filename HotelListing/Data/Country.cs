namespace HotelListing.Data
{
    public class Country
    {
        public int Id { get; set; }  //Id or Entitiname Id CountryId EntityFramework gonna understand  taht this is primary key

        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual IList<Hotel> Hotels { get; set; }


    }
}
